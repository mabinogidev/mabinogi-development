using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Structures
{
    /// <summary>
    /// Base class for players.
    /// </summary>
    public class Mobile
    {
        private List<Item> m_Items = new List<Item>();
        public string Name;
        public int Class;
        public int Level;
        public int Stage;
        public int CID;
        public int EXP;
        public int PvpLevel;
        public int PvpExp;
        public int WarLevel;
        public int WarExp;


        public Mobile()
        {
           
        }

        public void AddItem(Item item)
        {
            this.m_Items.Add(item);
        }

        public List<Item> GetItems()
        {
            return this.m_Items;
        }
    }
}
