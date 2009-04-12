using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_CHARACTER_DELETE : Packet
    {
        public enum DeleteState : int
        {
            CHAR_DELETE_OK = 0,
            CHAR_DELETE_EXIST = 1,
            CHAR_DELETE_GUILD_MASTER = 2,
            CHAR_DELETE_GUILD_MEMBER = 3,
            CHAR_DELETE_UNKNOWN = 4

        }

        public SMSG_CHARACTER_DELETE(string Name, int State) : base(35231)
        {
            this.EnsureCapacity((ushort)((Name.Length * 2) + 8));

            m_Stream.Write((int)State);
            m_Stream.Write((ushort)(Name.Length + 1));
            m_Stream.WriteLittleUniFixed(Name, Name.Length);
            m_Stream.Write((ushort)0x0000);
        }
    }
}
