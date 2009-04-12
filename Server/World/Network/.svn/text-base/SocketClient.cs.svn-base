using System;
using System.Net;
using System.Net.Sockets;
using Avalon.Utility.Debugging;
using Avalon.PrimaryType;
using Avalon.Utility.Time;
using Avalon.Network.Packets;

namespace Avalon.Network
{
    public class SocketClient
    {
        public Character Character;

        private UInt32 m_LastTime = Time.timeGetTime();
        private int m_PingCount = 0;

        private SocketServer m_Client;

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

        /// <summary>
        /// Terminates the current socket and removes it from the client list.
        /// </summary>
        public void Disconnect()
        {
            if (m_Client.Socket.Connected != false)
            {
                lock (Program.AvalonSrv.ClientList)
                {
                    Program.AvalonSrv.ClientList.Remove(this);
                }

                lock (Program.CharacterList)
                {
                    if (Character != null)
                    {
                        Program.CharacterList.Remove(Character.CID);
                        Character = null;
                    }
                }

                Logger.Log(Logger.LogLevel.Access, "Client", "[{0}] Client Disconnected : {1}", Program.AvalonSrv.ClientList.Count, ((IPEndPoint)m_Client.Socket.RemoteEndPoint).Address.ToString());

                // close the socket
                m_Client.Socket.Shutdown(SocketShutdown.Both);
                m_Client.Socket.Close();
            }

            // Release Session
            if (Character != null)
            {
                lock (Program.CharacterList)
                {
                    if (Program.CharacterList.ContainsKey(Character.CID))
                    {
                        Program.CharacterList.Remove(Character.CID);
                    }
                }
            }
        }

        #endregion
    }
}
