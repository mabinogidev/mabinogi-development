using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class CMSG_CHARACTER_CREATE : Packet
    {
        public CMSG_CHARACTER_CREATE(byte[] packet) : base(packet) { }

        public string Name
        {
            get
            {
                m_ReadStream.ReadUInt16();
                return m_ReadStream.ReadUnicodeStringLE();
            }
        }

        public int Class
        {
            get
            {   
                return (int)m_ReadStream.ReadUInt32();
            }
        }

        #region Conversions

        public static explicit operator CMSG_CHARACTER_CREATE(byte[] packet)
        {
            CMSG_CHARACTER_CREATE pkt = new CMSG_CHARACTER_CREATE(packet);
            return pkt;
        }

        #endregion
    }
}
