using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Avalon.Network.Packets;
using Avalon.Structures;

namespace Avalon.Managers.Database
{
    public partial class Database
    {
        public static List<Mobile> CharacterList(int AID)
        {
            List<Mobile> Characters = new List<Mobile>();

            SqlDataReader query = Database.Query("select * from character where aid = " + AID + "");
            if (!query.HasRows)
                return null;

            do
            {
                Mobile charobj   = new Mobile();
                charobj.CID      = (int)query["id"];
                charobj.Name     = (string)query["Name"];
                charobj.Class    = (int)query["Class"];
                charobj.Level    = (int)query["Level"];
                charobj.EXP      = (int)query["Exp"];
                charobj.Stage    = (int)query["Stage"];
                charobj.PvpLevel = (int)query["PvpLevel"];
                charobj.PvpExp   = (int)query["PvpExp"];
                charobj.WarLevel = (int)query["WarLevel"];
                charobj.WarExp   = (int)query["WarExp"];

                int CID = (int)query["id"];

                SqlDataReader sqlr = Database.Query("select * from equip where cid = " + CID);

                if (sqlr.HasRows)
                {
                    do
                    {
                        Item nItem = new Item(Convert.ToUInt32(sqlr["ItemID"]));
                        charobj.AddItem(nItem);
                    } while (sqlr.Read());
                }

                Characters.Add(charobj);
            } while (query.Read());


            return Characters;
            
        }

        public static SMSG_CHARACTER_CREATE CharacterCreate(String Name, int Class, int AID )
        {
            Regex pattern = new Regex("^[A-Za-z0-9]{0,16}$");
            SMSG_CHARACTER_CREATE packet;

            if (pattern.Match(Name).Success)
            {
                SqlDataReader sql = Database.Query("SELECT * FROM character WHERE name = '" + Name + "'");
                if (sql.HasRows)
                {
                    Database.Query("INSERT INTO character (aid, name, class) VALUES (" + AID + ", '" + Name + "', " + Class + ")");
                    packet = new SMSG_CHARACTER_CREATE(Name, Class, (int)SMSG_CHARACTER_CREATE.CreateState.CHAR_CREATE_OK);
                }
                else
                {
                    packet = new SMSG_CHARACTER_CREATE(Name, Class, (int)SMSG_CHARACTER_CREATE.CreateState.CHAR_CREATE_EXIST);
                }
            }
            else
            {
                packet = new SMSG_CHARACTER_CREATE(Name, Class, (int)SMSG_CHARACTER_CREATE.CreateState.CHAR_CREATE_INCORRECT);
            }

            return packet;
        }

        public static SMSG_CHARACTER_DELETE CharacterDelete(String Name, int AID)
        {
            SqlDataReader sdr = Database.Query("DELETE FROM character WHERE name = '" + Name + "' AND aid = " + AID);
            
            if(sdr.RecordsAffected > 0)
            {
                return (new SMSG_CHARACTER_DELETE(Name, (int)SMSG_CHARACTER_DELETE.DeleteState.CHAR_DELETE_OK));
            }
            else
            {
                return (new SMSG_CHARACTER_DELETE(Name, (int)SMSG_CHARACTER_DELETE.DeleteState.CHAR_DELETE_UNKNOWN));
            }
        }
    }
}
