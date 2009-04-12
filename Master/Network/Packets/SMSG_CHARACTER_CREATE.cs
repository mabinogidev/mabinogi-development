using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_CHARACTER_CREATE : Packet
    {
        public enum CreateState : int
        {
            CHAR_CREATE_OK = 0,
            CHAR_CREATE_EXIST = 1,
            CHAR_CREATE_DELETED = 2,
            CHAR_CREATE_INCORRECT = 3,
            CHAR_CREATE_BLOCKED = 4
        }

        public SMSG_CHARACTER_CREATE(string cName, int cClass, int state) : base(51566)
        {
            this.EnsureCapacity( 256 );

            m_Stream.Write((int)state);
            m_Stream.Write((ushort)HashTable.CharCreateHash);
            m_Stream.Write((ushort)HashTable.CharItemHash);

            m_Stream.Write((ushort)(cName.Length + 1));
            m_Stream.WriteLittleUniFixed(cName, cName.Length);
            m_Stream.Write((ushort)0x0000);
            m_Stream.Write(0x7f000008);
            m_Stream.Write(cClass);
            m_Stream.Write((int)1);

            byte[] Unknown = {
                0x00, 0x00, 
                0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 
                0x00, 0x00, 0x00, 0x00, 0xc9, 0x1a, 0x69, 0x17, 
                0xd8, 0x07, 0x0c, 0x00, 0x0e, 0x00, 0x15, 0x00, 
                0x37, 0x00, 0x37, 0x00, 0x00, 0x00, 0x66, 0xd1, 
                0x3c, 0x39, 0x00, 0x00, 0x00, 0x00,
            };

            // Unknown Bytes
            byte[] UnknownLiscense = {
  
                0xf3, 0xe8, 
                0x3c, 0x39,             // Array
                0x0a, 0x00, 0x00, 0x00, // 10 Elements
                
                0x62, 0x53, 0x9e, 0x52, 
                0xdb, 0xa3, 0x8b, 0x00, 
                0x09, 0x00,

                0x63, 0x53, 0x9e, 0x52, 
                0x93, 0xd4, 0x97, 0x01, 
                0x00, 0x00, 
                
                0x64, 0x53, 0x9e, 0x52,
                0x98, 0x97, 0xa6, 0x01, 
                0x00, 0x00, 
                
                0x65, 0x53, 0x9e, 0x52, 
                0x79, 0xe6, 0xe0, 0x01, 
                0x00, 0x00,

                0x66, 0x53, 0x9e, 0x52, 
                0x99, 0xb9, 0xe4, 0x01, 
                0x00, 0x00, 
                
                0x67, 0x53, 0x9e, 0x52, 
                0xf3, 0x9a, 0x0d, 0x02, 
                0x01, 0x00,             // Stage
                
                0x68, 0x53, 0x9e, 0x52, 
                0x36, 0xee, 0x27, 0x02, 
                0x00, 0x00, 
                
                0x69, 0x53, 0x9e, 0x52, 
                0x10, 0xa3, 0xb3, 0x02, 
                0x01, 0x00, 
                
                0x6a, 0x53, 0x9e, 0x52, 
                0x06, 0xa1, 0x30, 0x03,
                0x01, 0x00,
                
                0x6b, 0x53, 0x9e, 0x52, 
                0xa4, 0x96, 0x53, 0x03, 
                0x01, 0x00
            };

            m_Stream.Write(Unknown, 0, Unknown.Length);
            m_Stream.Write(UnknownLiscense, 0, UnknownLiscense.Length);
        }
    }
}
