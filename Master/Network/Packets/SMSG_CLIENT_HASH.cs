using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Utility.Time;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_CLIENT_HASH : Packet
    {
        public enum Result : int
        {
            OK = 0,
            FAIL = 1
        }

        public SMSG_CLIENT_HASH(int result) : base(43585, 28)
        {
            m_Stream.Write(result);

            byte[] ServerTime = Time.ServerTime(63730);
            m_Stream.Write(ServerTime, 0, ServerTime.Length);
        }
    }
}
    