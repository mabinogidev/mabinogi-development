using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class CMSG_PING : Packet
    {
        public CMSG_PING(byte[] packet) : base(packet) { }

        public UInt32 Count
        {
            get
            {
                m_ReadStream.Seek(6, System.IO.SeekOrigin.Begin);
                return m_ReadStream.ReadUInt32();
            }
        }

        public UInt32 Time
        {
            get
            {
                return m_ReadStream.ReadUInt32();
            }
        }

        #region Conversions

        public static explicit operator CMSG_PING(byte[] packet)
        {
            CMSG_PING pkt = new CMSG_PING(packet);
            return pkt;
        }

        #endregion
    }
}
