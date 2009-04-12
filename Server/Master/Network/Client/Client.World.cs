using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Network.Internal;
using Avalon.Structures;
using Avalon.Utility.Debugging;

namespace Avalon.Network.Client
{
    public class World
    {
        public static void ConnectWorld(byte[] packet, SocketClient sockstate)
        {
            CMSG_CONNECT_MASTER cpkt = (CMSG_CONNECT_MASTER)packet;
            
            string Name = cpkt.Name;
            string IPAddr = cpkt.IPAddr;
            int Port = cpkt.Port;

            Square nSquare = new Square(Name, 1, 0, IPAddr, Port, sockstate);
            Program.SquareList.Add(nSquare);

            Logger.Log(Logger.LogLevel.Access, "Client.World", "World server connected : {0} ", Name);
                

            sockstate.WServer = true;
            sockstate.WSquare = nSquare;
        }
    }
}
