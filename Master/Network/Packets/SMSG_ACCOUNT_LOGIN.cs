using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_ACCOUNT_LOGIN : Packet
    {
        private bool m_success = false;

        public SMSG_ACCOUNT_LOGIN() : base(47625, 34) { }

        public enum LoginState : ushort
        {
            LOGIN_OK = 0,
            LOGIN_NOT_FOUND = 1,
            LOGIN_BAD_PASSWORD = 2,
            LOGIN_DELETED = 3,
            LOGIN_BLOCKED = 4,
            LOGIN_IN_USE_LOBBY = 5,
            LOGIN_IN_USE_GAME = 6
        }

        public bool LoginSuccess
        {
            get
            {
                return m_success;
            }
        }

        public ushort State
        {
            set
            {
                m_Stream.Seek(6, System.IO.SeekOrigin.Begin);
                m_Stream.Write(value);
                m_Stream.Write((ushort)0x0000);

                if(value == (ushort)LoginState.LOGIN_OK)
                    m_success = true;
            }
        }

        public string Username
        {
            set
            {
                m_Stream.Seek(10, System.IO.SeekOrigin.Begin);
                m_Stream.Write((ushort)(value.Length + 1));
                m_Stream.WriteLittleUniFixed(value, value.Length);
                m_Stream.Write((ushort)0x0000);
            }
        }


    }
}
