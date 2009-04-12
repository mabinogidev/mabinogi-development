using System;
using System.Net;
using System.Net.Sockets;
using Avalon.Utility.Debugging;
using Avalon.Structures;
using Avalon.Utility.Time;
using Avalon.Network.Client;
using Avalon.Network.Packets;

namespace Avalon.Network
{
    public class SocketClient
    {
        private UInt32 m_LastTime = Time.timeGetTime();
        private int m_PingCount = 0;

        public int SessionID;
        public int SelectedChar;

        // socket is a world server
        public bool WServer = false;
        public Square WSquare;

        public int access = 0;

        public Account Account = new Account();
        private SocketServer m_Client;

        public string IPAddress;

        #region Constructors / Deconstructors

        /// <summary>
        /// Stores a reference to the clients socket.
        /// </summary>
        /// <param name="mainSock"></param>
        public SocketClient(SocketServer mainSock)
        {
            m_Client = mainSock;
        }

        #endregion

        #region Properties

        public SocketServer Client
        {
            get
            {
                return m_Client;
            }
            set
            {
                m_Client = value;
            }
        }

        public UInt32 LastTime
        {
            get
            {
                return m_LastTime;
            }
            set
            {
                m_LastTime = value;
            }
        }

        #endregion

        #region Public Methods

        public void Ping()
        {
            try
            {
                UInt32 diff = Time.LastCheckTime - m_LastTime;
                if (Time.LastCheckTime - m_LastTime > 40000)
                {
                    Logger.Log(Logger.LogLevel.Warning, "Client Timeout", "Socket : {0}", ((IPEndPoint)Client.Socket.RemoteEndPoint).Address.ToString());
                    this.Disconnect();
                }
                else
                {
                    SMSG_PING spkt = new SMSG_PING(m_PingCount, Time.LastCheckTime);
                    this.Client.Socket.Send(spkt.Stream);
                    ++m_PingCount;
                }
            }
            catch (SocketException se)
            {
                Logger.Log(Logger.LogLevel.Warning, "Client", "Socket : {0}", se.Message);
            }
        }

        /// <summary>
        /// Terminates the current socket and removes it from the client list.
        /// </summary>
        public void Disconnect()
        {
            if (m_Client != null && m_Client.Socket.Connected != false)
            {
                lock (Program.AvalonSrv.ClientList)
                {
                    Program.AvalonSrv.ClientList.Remove(this);
                }

                Logger.Log(Logger.LogLevel.Access, "Client", "[{0}] Client Disconnected : {1}", Program.AvalonSrv.ClientList.Count, ((IPEndPoint)m_Client.Socket.RemoteEndPoint).Address.ToString());

                // close the socket
                m_Client.Socket.Shutdown(SocketShutdown.Both);
                m_Client.Socket.Close();
            }
            else
            {
                lock (Program.AvalonSrv.ClientList)
                {
                    Program.AvalonSrv.ClientList.Remove(this);
                }

                // Remove WorldServer
                if (this.WServer == true && Program.SquareList.Contains(WSquare) == true)
                {
                    Program.SquareList.Remove(WSquare);
                }
            }
        }

        #endregion
    }
}
