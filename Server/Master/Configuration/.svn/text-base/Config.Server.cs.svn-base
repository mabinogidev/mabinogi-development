using System;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Configuration
{
    public class ServerSettings
    {
        public int ListenPort = 15550;
        public string ServerIP = "127.0.0.1";
        public bool AllowPlayers = true;

        public IPAddress IPAddress
        {
            get
            {
                return IPAddress.Parse(ServerIP);
            }
        }
    }
}
