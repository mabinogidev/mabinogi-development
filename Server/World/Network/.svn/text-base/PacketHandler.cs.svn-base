using System;
using System.Collections.Generic;
using Avalon.Network.Packets;
using Avalon.Utility.Debugging;
using Avalon.Network.Client;

namespace Avalon.Network
{
    public class PacketHandler
    {
        public delegate void RequestDelegate(byte[] x, SocketClient y);
        public static Dictionary<UInt16, RequestDelegate> OpcodeList = new Dictionary<ushort, RequestDelegate>();

        public static void Initialize()
        {
            OpcodeList.Add(2,     new RequestDelegate(General.AddSession));
            OpcodeList.Add(18479, new RequestDelegate(General.Pong));
            OpcodeList.Add(29280, new RequestDelegate(General.JoinStage));
            OpcodeList.Add(3599,  new RequestDelegate(Player.GetInventory));
            OpcodeList.Add(24782, new RequestDelegate(Player.GetSkillList));

            Logger.Log(Logger.LogLevel.Info, "Packet Monitor", "Monitoring {0} packets.", OpcodeList.Count);
        }
    }
}
