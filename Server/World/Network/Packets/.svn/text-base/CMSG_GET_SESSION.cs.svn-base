using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public class CMSG_GET_SESSION : Packet
    {

        public CMSG_GET_SESSION(byte[] packet) : base(packet) { }

        #region Properties

        public int Session
        {
            get
            {
                m_ReadStream.Seek(6, System.IO.SeekOrigin.Begin);
                m_ReadStream.ReadUInt16();
                return Convert.ToInt32(m_ReadStream.ReadUnicodeStringLE());
            }
        }

        #endregion

        #region Conversions

        public static explicit operator CMSG_GET_SESSION(byte[] packet)
        {
            CMSG_GET_SESSION pkt = new CMSG_GET_SESSION(packet);
            return pkt;
        }

        #endregion

    }
}
