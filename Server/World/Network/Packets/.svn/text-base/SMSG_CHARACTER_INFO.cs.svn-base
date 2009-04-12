using System;
using System.Collections.Generic;
using System.Text;
using Avalon.PrimaryType;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_CHARACTER_INFO : Packet
    {

        public SMSG_CHARACTER_INFO(Character charobj) : base(15834)
        {
            //27 00 e0 55 da 3d 02 00  00 00 01 00 00 00 00 00
            //01 00 00 00 00 00 01 00  00 00 00 00 00 00 00 00
            //00 00 00 00 00 00 00    

            //[CHARACTER INFO] 
            //4 bytes - classtype 
            //2 bytes - level
            //4 bytes - xp
            //2 bytes - pvp level
            //4 bytes - pvp xp
            //2 bytes - war level (specialty level)
            //4 bytes - war xp
            //1 bytes - gold
            //1 bytes - silver
            //1 bytes - copper

            //2 bytes - life
            //2 bytes - skillpoint
            //2 bytes - added skillpoints
            //2 bytes - IsSpectator

            this.EnsureCapacity(256);

            m_Stream.Write(charobj.Class);
            m_Stream.Write((ushort)charobj.Level);
            m_Stream.Write(charobj.EXP);
            m_Stream.Write((ushort)charobj.PvpLevel);
            m_Stream.Write(charobj.PvpExp);
            m_Stream.Write((ushort)charobj.WarLevel);
            m_Stream.Write(charobj.WarExp);
            m_Stream.Write(charobj.Money, 0, charobj.Money.Length);
            m_Stream.Write((ushort)charobj.Life);
            m_Stream.Write((ushort)charobj.SkillPoint);
            m_Stream.Write((ushort)charobj.AddedPoints);
            m_Stream.Write((ushort)charobj.Spectator);


        }

    }
}
