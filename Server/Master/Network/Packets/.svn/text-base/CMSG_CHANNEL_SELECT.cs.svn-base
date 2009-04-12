using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class CMSG_CHANNEL_SELECT : Packet
    {
        public CMSG_CHANNEL_SELECT(byte[] packet) : base(packet) { }

        public string SqrName
        {
            get
            {
                m_ReadStream.ReadUInt16();
                return m_ReadStream.ReadUnicodeStringLE();
            }
        }

        #region Conversions

        public static explicit operator CMSG_CHANNEL_SELECT(byte[] packet)
        {
            CMSG_CHANNEL_SELECT pkt = new CMSG_CHANNEL_SELECT(packet);
            return pkt;
        }

        #endregion
    }
}
