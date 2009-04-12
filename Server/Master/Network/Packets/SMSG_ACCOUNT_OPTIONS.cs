using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_ACCOUNT_OPTIONS : Packet
    {
        // Account Options - Comma Delimited
        // 0     int Donator 
        // 1     int # Character Slots
        // 2     int # Unlocked Characters
        // X     X     Unlocked Characters
        // 0     Terminator
        public SMSG_ACCOUNT_OPTIONS() : base(556) 
        {
            this.EnsureCapacity(34);
        }

        public int[] CharSlot
        {
            set
            {
                m_Stream.Write(value[1]);
                m_Stream.Write((ushort)HashTable.OptionsHash);
                m_Stream.Write((ushort)HashTable.ArrayFlag);
            }
        }

        public int[] CharUnlock
        {
            set
            {
                m_Stream.Write(value[2]);

                for (int i = 0; i < value[2]; ++i)
                {
                    m_Stream.Write(value[3 + i]);
                }
            }
        }
    }
}
