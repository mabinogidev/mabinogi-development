using System;
using System.Collections.Generic;
using Avalon.Network.Packets;
using Avalon.Network.Internal;
using Avalon.Structures;
using Avalon.Utility.Debugging;
using Avalon.Managers.Database;

namespace Avalon.Network.Client
{
    public class Channel
    {
        public static void ChannelList(byte[] packet, SocketClient sockstate)
        {
            SMSG_CHANNEL_LIST listPkt = new SMSG_CHANNEL_LIST(Program.SquareList);
            sockstate.Client.PacketQueue.Enqueue(listPkt.Stream);
        }

        public static void ChannelSelect(byte[] packet, SocketClient sockstate)
        {
            CMSG_CHANNEL_SELECT cpkt = (CMSG_CHANNEL_SELECT)packet;
            String SqrName = cpkt.SqrName;

            lock (Program.SquareList)
            {
                for (int i = 0; i < Program.SquareList.Count; ++i)
                {
                    if (Program.SquareList[i].Name == SqrName)
                    {
                        SMSG_CHANNEL_SELECT spkt = new SMSG_CHANNEL_SELECT(Program.SquareList[i], sockstate.SelectedChar);
                        SMSG_SEND_SESSION sessionPkt = new SMSG_SEND_SESSION(sockstate.SelectedChar);

                        try
                        {
                            // notify world
                            Program.SquareList[i].Socket.Client.Socket.Send(sessionPkt.Stream);
                        }
                        catch(Exception)
                        {
                            Logger.Log(Logger.LogLevel.Access, "World Server", "Server {0} not responding", Program.SquareList[i].Name);
                            Program.SquareList.RemoveAt(i);
                        }
                        
                        // notify client
                        sockstate.Client.PacketQueue.Enqueue(spkt.Stream);

                        break;
                    }
                }
            }
            
        }

        public static void ChannelPrevious(byte[] packet, SocketClient sockstate)
        {
            List<Structures.Mobile> Characters = Database.CharacterList(sockstate.Account.AID);
            sockstate.Account.Characters = Characters;
            SMSG_CHARACTER_LIST charlistPkt = new SMSG_CHARACTER_LIST(Characters);
            sockstate.Client.PacketQueue.Enqueue(charlistPkt.Stream);
        }

    }
}
