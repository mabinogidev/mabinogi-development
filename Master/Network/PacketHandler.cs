using System;
using System.Collections.Generic;
using Avalon.Network.Packets;
using Avalon.Network.Client;
using Avalon.Utility.Debugging;


namespace Avalon.Network
{
    public class PacketHandler
    {
        public delegate void RequestDelegate(byte[] x, SocketClient y);
        public static Dictionary<UInt16, RequestDelegate> OpcodeList = new Dictionary<ushort, RequestDelegate>();

        public static void Initialize()
        {
            OpcodeList.Add(1, new RequestDelegate(World.ConnectWorld));
            OpcodeList.Add(18479, new RequestDelegate(General.Ping));
            OpcodeList.Add(56707, new RequestDelegate(General.Disconnect));
            OpcodeList.Add(43585, new RequestDelegate(Authentication.FileHash));
            OpcodeList.Add(47625, new RequestDelegate(Authentication.Login));
            OpcodeList.Add(51566, new RequestDelegate(Character.CreateCharacter));
            OpcodeList.Add(35231, new RequestDelegate(Character.CharacterDelete));
            OpcodeList.Add(13678, new RequestDelegate(Character.CharacterSelect));
            OpcodeList.Add(14864, new RequestDelegate(Channel.ChannelList));
            OpcodeList.Add(31465, new RequestDelegate(Channel.ChannelSelect));
            OpcodeList.Add(62125, new RequestDelegate(Channel.ChannelPrevious));

            Logger.Log(Logger.LogLevel.Info, "Packet Monitor", "Monitoring {0} packets.", OpcodeList.Count);
        }
    }
}
