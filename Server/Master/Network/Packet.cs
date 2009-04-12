using System;
using System.Collections.Generic;
using Avalon.Utility.Stream;

namespace Avalon.Network.Packets
{
    public enum PacketFlag : ushort
    {
        Master = 0x55E0
    }

    public enum HashTable : uint
    {
        CharacterHash = 44843,
        EquipmentHash = 53606,
        EquipItemHash = 5075,
        OptionsHash = 34878,
        LiscenseHash = 59635,
        ItemIndex = 21346,
        CharCreateHash = 15834,
        CharItemHash = 48241,
        Positioning = 24010,
        ArrayFlag = 14652
    }

    public abstract class Packet
    {
        #region Private Members

        protected PacketWriter m_Stream;
        protected PacketReader m_ReadStream;
        private ushort m_Opcode;
        private ushort m_Size;
        private ushort m_Flag;

        private bool m_Request = false;

        #endregion

        #region Properties

        public ushort Opcode
        {
            get { return m_Opcode; }
        }

        public bool Request
        {
            get { return m_Request;  }
        }

        #endregion

        #region Public Methods

        public Packet(byte[] buffer)
        {
            m_ReadStream = new PacketReader(buffer, buffer.Length, true);

            m_Size   = m_ReadStream.ReadUInt16();
            m_Flag   = m_ReadStream.ReadUInt16();
            m_Opcode = m_ReadStream.ReadUInt16();
       
            m_Request = true;
        }

        public Packet(ushort opcode)
        {
            m_Opcode = opcode;
        }

        public void EnsureCapacity(ushort length)
        {
            m_Stream = PacketWriter.CreateInstance((length + 6), false);// new PacketWriter( length );
            m_Stream.Write((ushort)(length + (ushort)6));
            m_Stream.Write((ushort)PacketFlag.Master);
            m_Stream.Write(m_Opcode);
        }

        public Packet(ushort opcode, ushort length)
        {
            m_Stream = PacketWriter.CreateInstance((length + 6), true);
            m_Stream.Write(length);
            m_Stream.Write((ushort)PacketFlag.Master);
            m_Stream.Write(opcode);
        }

        public PacketWriter UnderlyingStream
        {
            get
            {
                return m_Stream;
            }
        }

        public byte[] Stream
        {
            get
            {
                return m_Stream.ToArray();
            }
        }

        #endregion
    }
}
