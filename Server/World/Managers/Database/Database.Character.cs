using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Avalon.PrimaryType;
using Avalon.Structure;

namespace Avalon.Managers.Database
{
    public partial class Database
    {
        public static bool LoadCharacter(int CID)
        {
            SqlDataReader query = Database.Query("select * from character where id = " + CID);
            query.Read();
            Character charobj = Program.CharacterList[CID];

            if (!query.HasRows)
                return false;

            charobj.CID = (int)query["id"];
            charobj.Name = (string)query["Name"];
            charobj.Class = (int)query["Class"];
            charobj.Level = (int)query["Level"];
            charobj.EXP = (int)query["Exp"];
            charobj.Stage = (int)query["Stage"];
            charobj.PvpLevel = (int)query["PvpLevel"];
            charobj.PvpExp = (int)query["PvpExp"];
            charobj.WarLevel = (int)query["WarLevel"];
            charobj.WarExp = (int)query["WarExp"];

            // Add Bags
            query = Database.Query("select * from bags where cid = " + CID);
            while (query.Read())
            {
                charobj.AddBag((int)query["slot"], (int)query["type"], (int)query["state"]);
            }

            // Add Items
            query = Database.Query("select * from item where cid = " + CID);
            while (query.Read())
            {
                Item nItem = new Item(Convert.ToUInt32(query["itemid"]));
                charobj.AddItem(nItem, Convert.ToInt32(query["slot"]));
            }

            // Equip Items

            return true;
        }
    }
}
