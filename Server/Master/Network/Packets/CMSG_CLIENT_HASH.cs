using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public class CMSG_CLIENT_HASH : Packet
    {
        public CMSG_CLIENT_HASH(byte[] packet) : base(packet) { }

        #region Properties

        public string Hash
        {
            get
            {
                m_ReadStream.Seek(18, System.IO.SeekOrigin.Begin);
                String Hash = m_ReadStream.ReadUTF8String();

                return Hash;
            }
        }

        #endregion

        #region Conversions

        public static explicit operator CMSG_CLIENT_HASH(byte[] packet)
        {
            CMSG_CLIENT_HASH pkt = new CMSG_CLIENT_HASH(packet);
            return pkt;
        }

        #endregion

    }
}
