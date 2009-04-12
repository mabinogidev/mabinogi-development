using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Avalon.Utility.Stream;

namespace Avalon.PrimaryType
{
    /// <summary>
    /// enumerated type for all game array hashes
    /// </summary>
    public enum Hash : ushort
    {
        Bag = 57804,
        BankBag = 52713
    }

    public class HashArray : PacketWriter
    {
        private int m_Counter = 0x5362;

        public HashArray(int hash, int count)
        {
            Write((ushort)hash);
            Write(0x393C);
            Write(count);
        }

        public void AddItem(byte[] item)
        {
            Write(m_Counter);
            Write(item, 0, item.Length);
            ++m_Counter;
        }

    }
}
