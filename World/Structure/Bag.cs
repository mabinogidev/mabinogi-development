using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Structure;
using Avalon.Utility.Time;

namespace Avalon.Structure
{
    /// <summary>
    /// Base structure for player bags.
    /// </summary>
    public struct Bag
    {
        private List<Item> m_Items;

        public ushort BagState;
        public int BagNumber;
        public int Expiry;

        public Bag(ushort bagstate, int bagnumber, int expiredate)
        {
            m_Items = new List<Item>();

            BagState = bagstate;
            BagNumber = bagnumber;
            Expiry = expiredate;
        }

        public void AddItem(Item item)
        {
            m_Items.Add(item);
        }
    }
}
