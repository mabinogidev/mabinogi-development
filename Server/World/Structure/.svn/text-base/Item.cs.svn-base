using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Structure
{
    /// <summary>
    /// Base class for all items, this struct contains an enumerated type for itemtype.
    /// </summary>
    public struct Item
    {
        private uint m_ItemID;
      
        public enum Type : byte
        {
            head = 0x00,
            hand = 0x01,
            body = 0x02,
            arms = 0x03,
            legs = 0x04,
            foot = 0x05,
            earing = 0x06,
            ring = 0x07,
            support = 0x08,
            necklace = 0x09,
            equipped = 0x10
        }

        public Item(uint ItemID)
        {
            m_ItemID = ItemID;
        }

        public uint GetItemID()
        {
            return m_ItemID;
        }
    }
}
