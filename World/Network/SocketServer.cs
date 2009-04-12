using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using Avalon.Utility.Conversions;
using Avalon.Utility.Debugging;
using Avalon.Utility.Stream;
using Avalon.Network.Client;
using Avalon.Network.Packets;
using Avalon.Network.Internal;

namespace Avalon.Network
{
    public class SocketServer
    {
        public Queue<byte[]> PacketQueue = new Queue<byte[]>();
        public List<SocketClient> ClientList = new List<SocketClient>();
        
        private SocketClient clientState = null;

        private Socket m_MainSock;
        private Socket m_MasterSock;
        private Socket m_SockConnection;

        private int m_MaxPacketSize = 0x1000;
        private int m_HeaderSize = 6;

        private byte[] m_bRecvBuffer = new byte[0x1000];
        private byte[] m_bPacketStream = new byte[0];
        
        private PacketReader pReader;

        private IPAddress m_ServerIP;
        private int m_ListenPort;
        private int m_MasterPort;

        #region Constructors / Deconstructors

        /// <summary>
        /// Stores parameters in local variables m_serverIP and m_listenPort.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public SocketServer(IPAddress address, int port)
        {
            m_ServerIP = address;
            m_ListenPort = port;
            m_MasterPort = Program.AvalonCfg.Master.Port;
        }

        /// <summary>
        /// Stores a reference to a placeholder for current objects socket.
        /// </summary>
        /// <param name="newConnection"></param>
        public SocketServer(Socket newConnection)
        {
            m_SockConnection = newConnection;
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Returns a reference to a client socket.
        /// </summary>
        public Socket Socket
        {
            get
            {
                return m_SockConnection;
            }
        }

        #endregion

        #region Public Methods

        public bool Listen()
        {
            try
            {
                // initialize packet factory
                PacketHandler.Initialize();

                m_MasterSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint remoteEP = new IPEndPoint(Program.AvalonCfg.Master.IPAddress, Program.AvalonCfg.Master.Port);
                m_MasterSock.Connect(remoteEP);

                SMSG_CONNECT_MASTER spkt = new SMSG_CONNECT_MASTER(Program.AvalonCfg.Server.Servername, Program.AvalonCfg.Server.ServerIP, (ushort)Program.AvalonCfg.Server.ListenPort);
                m_MasterSock.Send(spkt.Stream);

                SocketServer clientSock = new SocketServer(m_MasterSock);
                SocketClient clientState = new SocketClient(clientSock);

                clientSock.Socket.BeginReceive(clientSock.m_bRecvBuffer, 0, 0x1000, SocketFlags.None, clientSock.OnDataReceive, clientState);

                

                // listen on port 15554
                m_MainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_MainSock.Bind(new IPEndPoint(IPAddress.Any, m_ListenPort));
                m_MainSock.Listen(0x20);
                
                // when a connection is established invoke OnNewConnection()
                m_MainSock.BeginAccept(new AsyncCallback(OnNewConnection), m_MainSock);

                Logger.Log(Logger.LogLevel.Info, "Server Status", "Listening on port " + Program.AvalonCfg.Server.ListenPort);
                return true;
            }
            catch (SocketException se)
            {
                Logger.Log(Logger.LogLevel.Error, "SocketServer", "Listen: {0}", se.Message);
            }

            return false;
        }

        /// <summary>
        /// Invoked when listener recieved a new connection. 
        /// </summary>
        /// <param name="async"></param>
        public void OnNewConnection(IAsyncResult async)
        {
            SocketServer clientSock = new SocketServer(m_MainSock.EndAccept(async));

            try
            {
                // finished processing socket, go back to listening state.
                ((Socket)async.AsyncState).BeginAccept(new AsyncCallback(OnNewConnection), async.AsyncState);

                // initiate socket state object.
                SocketClient clientState = new SocketClient(clientSock);
                lock ( this.ClientList )
                {
                    ClientList.Add(clientState);
                }
                
                // do further processing for recently connected socket.
                clientSock.Socket.BeginReceive(clientSock.m_bRecvBuffer, 0, 0x1000, SocketFlags.None, clientSock.OnDataReceive, clientState);

                Logger.Log(Logger.LogLevel.Access, "Server", "[{0}] Client Connected : {1}", ClientList.Count, ((IPEndPoint)clientSock.Socket.RemoteEndPoint).Address.ToString());
            
                // send encryption key
                Random rand = new Random();
                SMSG_ENCRYPTION_KEY spkt = new SMSG_ENCRYPTION_KEY(rand.Next());
                Console.WriteLine("[Server->Client] Encryption");
                Console.WriteLine(Misc.HexBytes(spkt.Stream));
                clientState.Client.Socket.Send(spkt.Stream);
            }
            catch (ObjectDisposedException)
            {
                Logger.Log(Logger.LogLevel.Access, "Server", "[{0}] Socket has been closed.", ClientList.Count);
                ((Socket)async.AsyncState).BeginAccept(new AsyncCallback(OnNewConnection), async.AsyncState);
            }
            catch (SocketException se)
            {
                Logger.Log(Logger.LogLevel.Error, "Server", "OnNewConnection: {0}", se.Message);
                ((Socket)async.AsyncState).BeginAccept(new AsyncCallback(OnNewConnection), async.AsyncState);
            }
        }

        /// <summary>
        /// Invoked when a client sends data.
        /// </summary>
        /// <param name="async"></param>
        public void OnDataReceive(IAsyncResult async)
        {
            int numRecvBytes = 0;

            try
            {
                clientState = (SocketClient)async.AsyncState;
                numRecvBytes = m_SockConnection.EndReceive(async);

                if (numRecvBytes == 0)
                {
                    clientState.Disconnect();
                    return;
                }
                
                // copy new data and process packet
                lock (clientState)
                {
                    byte[] newData = new byte[numRecvBytes];
                    Buffer.BlockCopy(m_bRecvBuffer, 0, newData, 0, numRecvBytes);
                    m_bPacketStream = newData;

                    // process packets
                    ProcessPacket(m_bPacketStream, clientState);
                    ProcessQueue(clientState);
                }

                // finished receiving/processing data, go back to listening state.
                this.Socket.BeginReceive(m_bRecvBuffer, 0, 0x1000, SocketFlags.None, new AsyncCallback(OnDataReceive), clientState);
                
            }
            catch (ObjectDisposedException)
            {
                clientState.Disconnect();
            }
            catch (SocketException se)
            {
                clientState.Disconnect();
                Logger.Log(Logger.LogLevel.Error, "Server", "{0}", se.Message);
            }


        }

        /// <summary>
        /// ProcessPacket takes a given buffer and imports it to a Packet Factory for
        /// further processing.
        /// </summary>
        /// <param name="buffer"></param>
        public void ProcessPacket(byte[] buffer, SocketClient sockstate)
        {
            int offset = 0;
            pReader = new PacketReader(buffer, buffer.Length, true);
   
            // traverse packet buffer for cached packets
            while ((m_bPacketStream.Length - offset) >= m_HeaderSize)
            {
                // packet information
                pReader.Seek(offset, System.IO.SeekOrigin.Begin);
                UInt16 Size = pReader.ReadUInt16();
                UInt16 Flag = pReader.ReadUInt16();
                UInt16 Opcode = pReader.ReadUInt16();

                if ((Flag == (UInt16)PacketFlag.Master) && (Size < m_MaxPacketSize))
                {
                    byte[] payload = new byte[Size];
                    Buffer.BlockCopy(m_bPacketStream, offset, payload, 0, Size);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[Client->Server]");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(Utility.Conversions.Misc.HexBytes(payload));
                    // find request packet
                    if (PacketHandler.OpcodeList.ContainsKey(Opcode))
                    {
                        PacketHandler.OpcodeList[Opcode](payload, sockstate);
                    }

                    offset += Size;
                }
                else
                {
                    break;
                }
            }

        }

        public void ProcessQueue(SocketClient sockstate)
        {
            while (PacketQueue.Count > 0)
            {
                byte[] packet = PacketQueue.Dequeue();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Server->Client]");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(Utility.Conversions.Misc.HexBytes(packet));
                sockstate.Client.Socket.Send(packet);
            }
        }

        #endregion

    }
}
