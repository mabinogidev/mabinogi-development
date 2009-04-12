using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Avalon.Configuration
{
    public class MasterSettings
    {
        public int Port = 15550;
        public string ServerIP = "127.0.0.1";
        public string Proof = "puggy12";

        public IPAddress IPAddress
        {
            get
            {
                return IPAddress.Parse(ServerIP);
            }
        }
    }
}
