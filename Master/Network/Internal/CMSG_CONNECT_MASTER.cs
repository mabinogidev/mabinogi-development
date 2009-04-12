using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Network.Packets;

namespace Avalon.Network.Internal
{
    public sealed class CMSG_CONNECT_MASTER : Packet
    {
        public CMSG_CONNECT_MASTER(byte[] packet) : base(packet) { }


        public string Name
        {
            get
            {
                m_ReadStream.Seek(6, System.IO.SeekOrigin.Begin);
                return m_ReadStream.ReadString();
            }
        }

        public string IPAddr
        {
            get
            {
                
                return m_ReadStream.ReadString();
            }
        }

        public int Port
        {
            get
            {
                return (int)m_ReadStream.ReadUInt16();
            }
        }

        #region Conversions

        public static explicit operator CMSG_CONNECT_MASTER(byte[] packet)
        {
            CMSG_CONNECT_MASTER pkt = new CMSG_CONNECT_MASTER(packet);
            return pkt;
        }

        #endregion
    }
}
