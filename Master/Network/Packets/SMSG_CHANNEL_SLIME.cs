using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_CHANNEL_SLIME : Packet
    {
        public SMSG_CHANNEL_SLIME() : base(50361, 22)
        {
            byte[] payload = { 0x9e, 0xec, 0x3c, 0x39, 0x00, 0x00, 0x00, 0x00, 0x39, 0xff, 0x3c, 0x39, 0x00, 0x00, 0x00, 0x00 };
            m_Stream.Write(payload, 0, payload.Length);
        }
    }
}
