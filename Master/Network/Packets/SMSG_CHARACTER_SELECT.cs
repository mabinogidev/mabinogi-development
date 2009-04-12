using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_CHARACTER_SELECT : Packet
    {
        public SMSG_CHARACTER_SELECT(string Name) : base(13678)
        {
            this.EnsureCapacity((ushort)(Name.Length * 2 + 12));

            m_Stream.Write((uint)0x00000000);

            m_Stream.Write((ushort)(Name.Length + 1));
            m_Stream.WriteLittleUniFixed(Name, Name.Length);
            m_Stream.Write((ushort)0x0000);
            m_Stream.Write((uint)0x00000000);
        }
    }
}
