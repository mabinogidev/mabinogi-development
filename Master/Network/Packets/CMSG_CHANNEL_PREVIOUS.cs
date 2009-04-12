using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public class CMSG_CHANNEL_PREVIOUS : Packet
    {
        // opcode 62125
        public CMSG_CHANNEL_PREVIOUS(byte[] packet) : base(packet) { }

        #region Conversions

        public static explicit operator CMSG_CHANNEL_PREVIOUS(byte[] packet)
        {
            CMSG_CHANNEL_PREVIOUS pkt = new CMSG_CHANNEL_PREVIOUS(packet);
            return pkt;
        }

        #endregion
    }
}
