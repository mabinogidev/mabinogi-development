using System;
using System.Collections.Generic;
using System.Text;
using Avalon.Structures;
using Avalon.Utility.Time;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_CHARACTER_LIST : Packet
    {
        public SMSG_CHARACTER_LIST(List<Mobile> Characters) : base(56364)
        {
            this.EnsureCapacity(1024);

            // Unknown Bytes
            byte[] Unknown = {
                    0xbb, 0x03, 0x00, 0x00, 0x01, 0x00, 
                    0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 
                    0x00, 0x00, 0xc9, 0x1a, 0x69, 0x17, 0xd8, 0x07, 
                    0x0b, 0x00, 0x04, 0x00, 0x15, 0x00, 0x2a, 0x00, 
                    0x11, 0x00, 0x00, 0x00
            };

            // Unknown Bytes
            byte[] UnknownLiscense = {
  
                0xf3, 0xe8, 
                0x3c, 0x39,             // Array
                0x0a, 0x00, 0x00, 0x00, // 10 Elements

                0x62, 0x53, 0x9e, 0x52, // Character Index, Character Hash
                0xdb, 0xa3, 0x8b, 0x00, 
                0x09, 0x00,
 
                0x63, 0x53, 0x9e, 0x52, // Character Index, Character Hash
                0x93, 0xd4, 0x97, 0x01, 
                0x00, 0x00, 
                
                0x64, 0x53, 0x9e, 0x52, // Character Index, Character Hash
                0x98, 0x97, 0xa6, 0x01, 
                0x00, 0x00, 
                
                0x65, 0x53, 0x9e, 0x52, // Character Index, Character Hash
                0x0b, 0x2d, 0xbf, 0x01,           
                0x00, 0x00,
           
                0x66, 0x53, 0x9e, 0x52, // Character Index, Character Hash
                0x79, 0xe6, 0xe0, 0x01, 
                0x00, 0x00,
 
                0x67, 0x53, 0x9e, 0x52, // Character Index, Character Hash
                0x99, 0xb9, 0xe4, 0x01, 
                0x07, 0x00,             // Stage

                0x68, 0x53, 0x9e, 0x52, // Character Index, Character Hash
                0x36, 0xee, 0x27, 0x02, 
                0x00, 0x00, 

                0x69, 0x53, 0x9e, 0x52, // Character Index, Character Hash
                0x10, 0xa3, 0xb3, 0x02, 
                0x00, 0x00, 

                0x6a, 0x53, 0x9e, 0x52, // Character Index, Character Hash
                0x06, 0xa1, 0x30, 0x03, 
                0x00, 0x00, 

                0x6b, 0x53, 0x9e, 0x52, // Character Index, Character Hash
                0xb8, 0x8a, 0xc2, 0x03, 
                0x00, 0x00
            };

            int CharCount;
            if (Characters == null)
                CharCount = 0;
            else
                CharCount = Characters.Count;

            m_Stream.Write((ushort)HashTable.CharacterHash);
            m_Stream.Write((ushort)HashTable.ArrayFlag);
            m_Stream.Write(CharCount);

            for (int i = 0; i < CharCount; ++i)
            {
                m_Stream.Write((ushort)((int)HashTable.ItemIndex + i));
                m_Stream.Write((ushort)HashTable.CharItemHash);
                m_Stream.Write((ushort)(Characters[i].Name.Length + 1));
                m_Stream.WriteLittleUniFixed(Characters[i].Name, Characters[i].Name.Length);
                m_Stream.Write((ushort)0x0000);
                m_Stream.Write(0x7f000008);
                m_Stream.Write(Characters[i].Class);
                m_Stream.Write((ushort)Characters[i].Level);
                m_Stream.Write(Characters[i].EXP);
                m_Stream.Write((ushort)Characters[i].PvpLevel);
                m_Stream.Write(Characters[i].PvpExp);
                m_Stream.Write((ushort)Characters[i].WarLevel);
                m_Stream.Write(Characters[i].WarExp);

                // Last Logged Date
                byte[] ServerTime = Time.ServerTime(6857);
                m_Stream.Write(ServerTime, 0, ServerTime.Length); 

                // Equipment
                int itemcount = 0;
                List<Item> ItemList = Characters[i].GetItems();

                if (ItemList == null)
                {
                    itemcount = 0;
                }
                else
                {
                    itemcount = ItemList.Count;
                }

                // Equipment Array
                //      2 Byte Equipment Hash
                //      2 Byte Array Flag
                //      4 Byte Item Count
                m_Stream.Write((ushort)HashTable.EquipmentHash);
                m_Stream.Write((ushort)HashTable.ArrayFlag);
                m_Stream.Write(itemcount);

                // Traversing Equipment Array
                //      2 Byte Item Index
                //      2 Byte Item Hash
                //      4 Byte Item ID
                //      2 Byte Positioning
                //      2 Byte Unknown
                //      1 Byte Slot
                //      4 Byte Unknown
                //      4 Byte Unknown
                //      2 Byte Element Terminator
                for (int j = 0; j < itemcount; ++j)
                {
                    m_Stream.Write((ushort)((int)HashTable.ItemIndex + j));
                    m_Stream.Write((ushort)HashTable.EquipItemHash);
                    m_Stream.Write(ItemList[j].Id);
                    m_Stream.Write((ushort)HashTable.Positioning);
                    m_Stream.Write((ushort)0x613E);
                    m_Stream.Write((byte)j);
                    m_Stream.Write((int)0x00000100);
                    m_Stream.Write((int)0x00000000);
                    m_Stream.Write((ushort)0x0000);
                }

                // Attach Item Liscense
                int Stage = (Characters[i].Stage - 1);
                UnknownLiscense[66] = (byte)Stage;
                m_Stream.Write(UnknownLiscense, 0, UnknownLiscense.Length);

            }
        }
    }
}
