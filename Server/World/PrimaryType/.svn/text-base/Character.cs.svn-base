using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Structure;
using Avalon.Utility.Time;

namespace Avalon.PrimaryType
{
    /// <summary>
    /// Base class for players.
    /// </summary>
    public class Character
    {
        private List<Item> m_Equipped = new List<Item>();
        private Dictionary<int, Bag> m_Bags = new Dictionary<int, Bag>();

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
        public int BagCount = 0;

        public byte[] Money = { 0x00, 0x00, 0x00 };
        public int Life = 0;
        public int SkillPoint = 0;
        public int AddedPoints = 0;
        public int Spectator = 0;

        public Character(int cid)
        {
            CID = cid;
        }

        public void AddBag(int slot, int type, int state)
        {
            Bag nBag = new Bag((ushort)state, slot, 0);
            m_Bags.Add(slot, nBag);
            ++BagCount;
        }

        public void AddItem(Item item, int bagslot)
        {
            m_Bags[bagslot].AddItem(item);
        }

        public List<Item> GetEquipped()
        {
            return this.m_Equipped;
        }
    }
}
