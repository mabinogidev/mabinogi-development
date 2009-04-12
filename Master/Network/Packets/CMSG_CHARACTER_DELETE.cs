using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class CMSG_CHARACTER_DELETE : Packet
    {

        public CMSG_CHARACTER_DELETE(byte[] packet) : base(packet) { }

        public string Name
        {
            get
            {
                m_ReadStream.ReadUInt16();
                return m_ReadStream.ReadUnicodeStringLE();
            }
        }

        #region Conversions

        public static explicit operator CMSG_CHARACTER_DELETE(byte[] packet)
        {
            CMSG_CHARACTER_DELETE pkt = new CMSG_CHARACTER_DELETE(packet);
            return pkt;
        }

        #endregion
    }
}
