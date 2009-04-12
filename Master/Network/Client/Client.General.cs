using System;
using Avalon.Network.Packets;
using Avalon.Utility.Time;

namespace Avalon.Network.Client
{
    public class General
    {
        public static void Ping(byte[] packet, SocketClient sockstate)
        {
            sockstate.LastTime = Time.timeGetTime();
        }

        public static void Disconnect(byte[] packet, SocketClient sockstate)
        {
            sockstate.Disconnect();
        }
    }
}
