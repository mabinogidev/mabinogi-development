using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Avalon.Network
{
    public class Client
    {
        #region private members

        public Socket m_socListener;

        #endregion

        public Client(string host, int port)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(host, port);
        }
        
    }
}
