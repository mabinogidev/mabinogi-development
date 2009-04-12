using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class CMSG_DISCONNECT : Packet
    {
        public CMSG_DISCONNECT() : base(56707, 256)
        {

        }
    }
}
