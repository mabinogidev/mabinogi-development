using System;
using Avalon.Network.Packets;
using Avalon.Managers.Database;
using Avalon.Structures;
using Avalon.Utility.Debugging;

namespace Avalon.Network.Client
{
    public class Character
    {
        public static void CreateCharacter(byte[] packet, SocketClient sockstate)
        {
            String Name;
            int Class;

            CMSG_CHARACTER_CREATE cpkt = (CMSG_CHARACTER_CREATE)packet;
            Name = cpkt.Name;
            Class = cpkt.Class;

            SMSG_CHARACTER_CREATE spkt = Database.CharacterCreate(Name, Class, sockstate.Account.AID);
            sockstate.Client.PacketQueue.Enqueue(spkt.Stream);
        }

        public static void CharacterDelete(byte[] packet, SocketClient sockstate)
        {
            CMSG_CHARACTER_DELETE cpkt = (CMSG_CHARACTER_DELETE)packet;
            SMSG_CHARACTER_DELETE spkt = Database.CharacterDelete(cpkt.Name, sockstate.Account.AID);
            sockstate.Client.PacketQueue.Enqueue(spkt.Stream);
        }

        public static void CharacterSelect(byte[] packet, SocketClient sockstate)
        {
            String Name;
            int CID = 0;
            bool loginsuccess = true;

            CMSG_CHARACTER_SELECT cpkt = (CMSG_CHARACTER_SELECT)packet;
            Name = cpkt.Name;

            foreach(Mobile character in sockstate.Account.Characters)
            {
                if (character.Name == Name)
                {
                    CID = character.CID;
                    loginsuccess = true;
                }
            }

            if (loginsuccess == true)
            {
                SMSG_CHARACTER_SELECT spkt = new SMSG_CHARACTER_SELECT(Name);
                sockstate.Client.PacketQueue.Enqueue(spkt.Stream);

                SMSG_CHANNEL_SLIME slimePkt = new SMSG_CHANNEL_SLIME();
                sockstate.Client.PacketQueue.Enqueue(slimePkt.Stream);

                sockstate.SelectedChar = CID;
            }
            else
            {
                Logger.Log(Logger.LogLevel.Error, "Hack Detection", "Account: {0} - Invalid Select Character Name: {1}", sockstate.Account.Username, Name);
                sockstate.Disconnect();
            }
        }
    }
}
