using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Network.Packets;

namespace Avalon.Network.Internal
{
    public sealed class CMSG_ADD_SESSION : Packet
    {
        public CMSG_ADD_SESSION(byte[] packet) : base(packet) { }

        public int cid
        {
            get
            {
                return (int)m_ReadStream.ReadUInt32();
            }
        }

        #region Conversions

        public static explicit operator CMSG_ADD_SESSION(byte[] packet)
        {
            CMSG_ADD_SESSION pkt = new CMSG_ADD_SESSION(packet);
            return pkt;
        }

        #endregion
    }
}
