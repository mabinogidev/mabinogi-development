using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public class CMSG_ACCOUNT_LOGIN : Packet
    {

        public CMSG_ACCOUNT_LOGIN(byte[] packet) : base(packet) { }

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

        #region Properties

        public string Username
        {
            get
            {
                m_ReadStream.Seek(8, System.IO.SeekOrigin.Begin);
                return m_ReadStream.ReadUnicodeStringLE();
            }
        }

        public string Password
        {
            get
            {
                m_ReadStream.Seek(8, System.IO.SeekOrigin.Begin);
                m_ReadStream.ReadUnicodeStringLE();
                m_ReadStream.ReadUInt32();
                m_ReadStream.ReadSByte();
                return m_ReadStream.ReadString();
            }
        }

        #endregion

        #region Conversions

        public static explicit operator CMSG_ACCOUNT_LOGIN(byte[] packet)
        {
            CMSG_ACCOUNT_LOGIN pkt = new CMSG_ACCOUNT_LOGIN(packet);
            return pkt;
        }

        #endregion

    }
}
