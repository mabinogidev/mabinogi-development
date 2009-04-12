using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Managers.Database;
using Avalon.Network;

namespace Avalon.Structures
{
    public class Square
    {
        public SocketClient Socket;
        public string Name;
        public string IPAddr;
        public int Count;
        public int Status;
        public int Port;

        public Square(string sName, int status, int count, string ipaddr, int port, SocketClient sockstate)
        {
            Socket = sockstate;
            Name = sName;
            Status = status;
            Count = count;
            IPAddr = ipaddr;
            Port = port;
        }
    }
}
