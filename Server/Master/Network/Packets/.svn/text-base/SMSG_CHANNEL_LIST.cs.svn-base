using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Structures;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_CHANNEL_LIST : Packet
    {
        public SMSG_CHANNEL_LIST(List<Square> Square) : base(14864)
        {
            this.EnsureCapacity(256);

            m_Stream.Write((uint)0x00000000);
            m_Stream.Write(0x393cdc25);
            m_Stream.Write(Square.Count);

            for (int i = 0; i < Square.Count; ++i)
            {

                m_Stream.Write((ushort)((int)HashTable.ItemIndex + i));
                m_Stream.Write((ushort)0xa7ad);
                m_Stream.Write((ushort)(Square[i].Name.Length + 1));
                m_Stream.WriteLittleUniFixed(Square[i].Name, Square[i].Name.Length);
                m_Stream.Write((ushort)0x0000);
                m_Stream.Write((int)Square[i].Status);
                m_Stream.Write((int)0);
            }
        }
    }
}
