using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data.SqlClient;
using Avalon.Network.Packets;
using Avalon.Utility.Conversions;
using Avalon.Utility.Debugging;
using Avalon.Network;

namespace Avalon.Managers.Database
{
    public partial class Database
    {
        public static SMSG_ACCOUNT_LOGIN Login(String Username, String Password, SocketClient sockstate)
        {
            SMSG_ACCOUNT_LOGIN loginpacket = new SMSG_ACCOUNT_LOGIN();
            loginpacket.Username = Username;

            string salt;
            SqlDataReader sdr;

            sdr = Database.Query("SELECT salt FROM account WHERE username='" + Username + "'");

            if (sdr == null)
            {
                loginpacket.State = (ushort)SMSG_ACCOUNT_LOGIN.LoginState.LOGIN_NOT_FOUND;
                return loginpacket;
            }
               
            
            if (sdr.HasRows)
            {
                salt        = Convert.ToString(sdr["salt"]);
                Password    = Misc.GetMD5Hash(Password + salt);

                sdr = Database.Query("Select id, access, username, options from account where username='" + Username + "' and password='" + Password + "'");
                
                if (sdr.HasRows)
                {
                    // set account options
                    sockstate.Account.Access        = Convert.ToInt32(sdr["access"]);
                    sockstate.Account.AID           = Convert.ToInt32(sdr["id"]);
                    sockstate.Account.Options       = Misc.GetOptions(Convert.ToString(sdr["options"]));
                    
                    // check online status
                    foreach (SocketClient socks in Program.AvalonSrv.ClientList)
                    {
                        if (socks.Account.Username != null)
                        {
                            if (socks.Account.Username == Username)
                            {
                                Logger.Log(Logger.LogLevel.Access, "Server", "Account in use, disconnecting : {0} ", ((IPEndPoint)socks.Client.Socket.RemoteEndPoint).Address.ToString());
                                loginpacket.State = (ushort)SMSG_ACCOUNT_LOGIN.LoginState.LOGIN_IN_USE_LOBBY;
                                return loginpacket;
                            }
                        }
                    }

                    loginpacket.State = (ushort)SMSG_ACCOUNT_LOGIN.LoginState.LOGIN_OK;
                    return loginpacket;
                
                } 
                else 
                {
                    loginpacket.State = (ushort)SMSG_ACCOUNT_LOGIN.LoginState.LOGIN_BAD_PASSWORD;
                    return loginpacket;
                }
            } 
            else 
            {
                loginpacket.State = (ushort)SMSG_ACCOUNT_LOGIN.LoginState.LOGIN_NOT_FOUND;
                return loginpacket;
            }
        }
    }
}
