using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_PING : Packet
    {
        public SMSG_PING(int count, UInt32 time) : base(18479)
        {
            this.EnsureCapacity(32);

            m_Stream.Write((UInt32)count);
            m_Stream.Write((UInt32)time);
            m_Stream.Write((int)0x00000000);
            m_Stream.Write((int)0x00000000);
        }
    }
}
