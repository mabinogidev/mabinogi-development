using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_ENCRYPTION_KEY : Packet
    {

        public SMSG_ENCRYPTION_KEY(int IV) : base(24177, 10) 
        {
            m_Stream.Write(IV);
        }

    }
}
