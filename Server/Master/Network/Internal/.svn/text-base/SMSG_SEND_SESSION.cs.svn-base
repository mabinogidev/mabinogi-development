using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Network.Packets;

namespace Avalon.Network.Internal
{
    public sealed class SMSG_SEND_SESSION : Packet
    {
        public SMSG_SEND_SESSION(int CID) : base(2)
        {
            this.EnsureCapacity(32);

            m_Stream.Write(CID);
        }
    }
}
