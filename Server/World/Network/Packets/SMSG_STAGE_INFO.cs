using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_STAGE_INFO : Packet
    {

        public SMSG_STAGE_INFO(string name) : base(59901)
        {
            this.EnsureCapacity(62);

            m_Stream.Write((ushort)(name.Length + 1));
            m_Stream.WriteLittleUniNull(name);

            byte[] unknown = { 0x4f, 0x42, 0x9e, 0x52, 0x06, 0xa1, 0x30, 0x03, 0x00, 0x00 };

            m_Stream.Write(unknown, 0, unknown.Length);

        }

    }
}
