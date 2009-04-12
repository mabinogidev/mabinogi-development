using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Avalon.Network.Packets;
using Avalon.Utility.Conversions;
using Avalon.Managers.Database;
using Avalon.Utility.Debugging;

namespace Avalon.Network.Client
{
    public class Authentication
    {
        public static void FileHash(byte[] packet, SocketClient sockstate)
        {
            CMSG_CLIENT_HASH cpkt = (CMSG_CLIENT_HASH)packet;
            String Hash = cpkt.Hash;
            Hash = cpkt.Hash;

            // Add version check for future release, assumed correct.
            SMSG_CLIENT_HASH spkt = new SMSG_CLIENT_HASH((int)SMSG_CLIENT_HASH.Result.OK);
            sockstate.Client.PacketQueue.Enqueue(spkt.Stream);
        }

        public static void Login(byte[] packet, SocketClient sockstate)
        {
            String Username;
            String Password;

            CMSG_ACCOUNT_LOGIN cpkt = (CMSG_ACCOUNT_LOGIN)packet;
            Username = cpkt.Username;
            Password = cpkt.Password;

            Regex countPattern = new Regex("NHN_P_LOGIN=(.+);");
            Match m1 = countPattern.Match(Password);
            Password = m1.Groups[1].ToString();

            // authenticate
            SMSG_ACCOUNT_LOGIN accPkt = Database.Login(Username, Password, sockstate);
            sockstate.Account.Username = Username;
            sockstate.Client.PacketQueue.Enqueue(accPkt.Stream);

            if (accPkt.LoginSuccess == true)
            {
                Logger.Log(Logger.LogLevel.Access, "Authentication", "Login accepted for user : {0} ", sockstate.Account.Username);
                                
                // send login options
                SMSG_ACCOUNT_OPTIONS optionsPkt = new SMSG_ACCOUNT_OPTIONS();
                optionsPkt.CharSlot = sockstate.Account.Options;
                optionsPkt.CharUnlock = sockstate.Account.Options;
                sockstate.Client.PacketQueue.Enqueue(optionsPkt.Stream);

                // send character list
                List<Structures.Mobile> Characters = Database.CharacterList(sockstate.Account.AID);
                sockstate.Account.Characters = Characters;
                SMSG_CHARACTER_LIST charlistPkt = new SMSG_CHARACTER_LIST(Characters);
                sockstate.Client.PacketQueue.Enqueue(charlistPkt.Stream);
            }
          
        }
    }
}
