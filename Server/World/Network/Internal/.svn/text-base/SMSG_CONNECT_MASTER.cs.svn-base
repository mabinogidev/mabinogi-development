using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Network.Packets;

namespace Avalon.Network.Internal
{
    public sealed class SMSG_CONNECT_MASTER : Packet
    {

        public SMSG_CONNECT_MASTER(string name, string ipaddr, ushort port) : base(1)
        {
            this.EnsureCapacity(32);

            m_Stream.WriteAsciiNull(name);
            m_Stream.WriteAsciiNull(ipaddr);
            m_Stream.Write(port);
        }

    }
}
