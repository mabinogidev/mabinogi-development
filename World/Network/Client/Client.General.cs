using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Avalon.Network.Packets;
using Avalon.Network.Internal;
using Avalon.PrimaryType;
using Avalon.Utility.Debugging;
using Avalon.Managers.Database;

namespace Avalon.Network.Client
{
    public class General
    {
        public static void Pong(byte[] packet, SocketClient sockstate)
        {
            UInt32 Time;
            UInt32 Count;

            CMSG_PING cpkt = (CMSG_PING)packet;
            Count = cpkt.Count;
            Time = cpkt.Time;

            SMSG_PONG spkt = new SMSG_PONG(Count, Time);
            sockstate.Client.PacketQueue.Enqueue(spkt.Stream);
        }

        public static void AddSession(byte[] packet, SocketClient sockstate)
        {
            int cid;

            CMSG_ADD_SESSION cpkt = (CMSG_ADD_SESSION)packet;

            cid = cpkt.cid;

            lock (Program.CharacterList)
            {
                if (!Program.CharacterList.ContainsKey(cid))
                {
                    Program.CharacterList.Add(cid, new Character(cid));
                }
                else
                {
                    Logger.Log(Logger.LogLevel.Hack, "Hack Detection", "Session tampering detected : {0}", ((IPEndPoint)sockstate.Client.Socket.RemoteEndPoint).Address.ToString());
                    sockstate.Disconnect();
                }
            }

        }

        public static void JoinStage(byte[] packet, SocketClient sockstate)
        {
            int sessionid;

            CMSG_GET_SESSION cpkt = (CMSG_GET_SESSION)packet;
            sessionid = cpkt.Session;

            lock (Program.CharacterList)
            {
                if (Program.CharacterList.ContainsKey(sessionid))
                {
                    sockstate.Character = Program.CharacterList[sessionid];
                    if (!Database.LoadCharacter(sessionid))
                    {
                        Logger.Log(Logger.LogLevel.Error, "Client General", "Failed to load character for cid {0}", sessionid);
                    }
                }
                else
                {
                    Logger.Log(Logger.LogLevel.Hack, "Hack Detection", "Session tampering detected : {0}", ((IPEndPoint)sockstate.Client.Socket.RemoteEndPoint).Address.ToString());
                    sockstate.Disconnect();
                    return;
                }
            }

            // Client ignores this packet, even though it's sent on official.
            // SMSG_STAGE_INFO stagePkt = new SMSG_STAGE_INFO(sockstate.Character.Name);
            
            SMSG_CHARACTER_INFO charPkt = new SMSG_CHARACTER_INFO(sockstate.Character);
            SMSG_BAG_INFO bagPkt = new SMSG_BAG_INFO(sockstate);

            sockstate.Client.PacketQueue.Enqueue(charPkt.Stream);
            sockstate.Client.PacketQueue.Enqueue(bagPkt.Stream);

            // Ingored by client
            byte[] one = {
            0x30, 0x00, 0xe0, 0x55, 0x8d, 0xfa, 0xcc, 0xe1, 
            0x3c, 0x39, 0x01, 0x00, 0x00, 0x00, 0x62, 0x53, 
            0xa9, 0xda, 0x02, 0x00, 0x00, 0x00, 0xaf, 0xd5, 
            0x69, 0x17, 0x0f, 0x27, 0x01, 0x00, 0x01, 0x00, 
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
            0xe9, 0xcd, 0x3c, 0x39, 0x00, 0x00, 0x00, 0x00 };


            byte[] two = {
            0xaf, 0x00, 0xe0, 0x55, 0xf3, 0x2c, 0x31, 0xee, 
            0x3c, 0x39, 0x07, 0x00, 0x00, 0x00, 0x62, 0x53, 
            0xd3, 0x13, 0xf2, 0xfb, 0x8f, 0x00, 0xca, 0x5d, 
            0x3e, 0x61, 0x01, 0x00, 0x0a, 0x00, 0x00, 0x00, 
            0x00, 0x00, 0x00, 0x00, 0x00, 0x63, 0x53, 0xd3, 
            0x13, 0x78, 0x8a, 0x05, 0x00, 0xca, 0x5d, 0x3e, 
            0x61, 0x01, 0x01, 0x14, 0x00, 0x00, 0x00, 0x00, 
            0x00, 0x00, 0x00, 0x00, 0x64, 0x53, 0xd3, 0x13, 
            0x90, 0xbb, 0x52, 0x02, 0xca, 0x5d, 0x3e, 0x61, 
            0x01, 0x02, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 
            0x00, 0x00, 0x00, 0x65, 0x53, 0xd3, 0x13, 0x91, 
            0xbb, 0x52, 0x02, 0xca, 0x5d, 0x3e, 0x61, 0x01, 
            0x03, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
            0x00, 0x00, 0x66, 0x53, 0xd3, 0x13, 0x92, 0xbb, 
            0x52, 0x02, 0xca, 0x5d, 0x3e, 0x61, 0x01, 0x04, 
            0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
            0x00, 0x67, 0x53, 0xd3, 0x13, 0x93, 0xbb, 0x52, 
            0x02, 0xca, 0x5d, 0x3e, 0x61, 0x01, 0x05, 0x01, 
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
            0x68, 0x53, 0xd3, 0x13, 0x94, 0xbb, 0x52, 0x02, 
            0xca, 0x5d, 0x3e, 0x61, 0x01, 0x06, 0x01, 0x00, 
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] three = {
            0x0e, 0x00, 0xe0, 0x55, 0x14, 0x74, 0x96, 0x1a, 
            0x3c, 0x39, 0x00, 0x00, 0x00, 0x00 };
            byte[] four = {
            0x0e, 0x00, 0xe0, 0x55, 0x0f, 0x0e, 0xec, 0xc9, 
            0x3c, 0x39, 0x00, 0x00, 0x00, 0x00 };
            byte[] five = {
            0x0e, 0x00, 0xe0, 0x55, 0xb6, 0x19, 0xad, 0x8c, 
            0x8e, 0xce, 0x00, 0x00, 0x00, 0x00 };
            byte[] six = {
            0x12, 0x00, 0xe0, 0x55, 0xed, 0xd2, 0x00, 0x00, 
            0x00, 0x00, 0xea, 0x13, 0x3c, 0x39, 0x00, 0x00, 
            0x00, 0x00 };
            byte[] seven = {
            0x0e, 0x00, 0xe0, 0x55, 0x30, 0xfd, 0xa3, 0x11, 
            0x3c, 0x39, 0x00, 0x00, 0x00, 0x00 };

            sockstate.Client.PacketQueue.Enqueue(one);
            sockstate.Client.PacketQueue.Enqueue(two);
            sockstate.Client.PacketQueue.Enqueue(three);
            sockstate.Client.PacketQueue.Enqueue(four);
            sockstate.Client.PacketQueue.Enqueue(five);
            sockstate.Client.PacketQueue.Enqueue(six);
            sockstate.Client.PacketQueue.Enqueue(seven);

        }
    }
}
