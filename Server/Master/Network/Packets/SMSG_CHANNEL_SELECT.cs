using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Structures;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_CHANNEL_SELECT : Packet
    {
        public SMSG_CHANNEL_SELECT(Square square, int sessionid) : base(47499)
        {
            this.EnsureCapacity(62);
            String sessionname = Convert.ToString(sessionid);

            m_Stream.Write((int)0x00000000);
            m_Stream.Write((ushort)(square.IPAddr.Length + 1));
            m_Stream.WriteAsciiFixed(square.IPAddr, square.IPAddr.Length + 1);
            m_Stream.Write((ushort)(square.Port));
                    
            //Position is static for now
            m_Stream.Write((ushort)(sessionname.Length + 1));
            m_Stream.WriteAsciiFixed(sessionname, sessionname.Length + 1);

        }
    }
}
