using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Structures
{
    // ItemSlot
    // Id
    // Position
    // {
    //     ItemPosition
    //     Bag
    //     Position
    // }
    public struct Position
    {
        public int ItemPosition;
        public int Bag;

        Position(int itempos, int bag)
        {
            ItemPosition = itempos;
            Bag = bag;
        }
    }

    public struct LiscenseData
    {
        public int StageLocation;
        public int StageGroupHash;
        public int Level;

        LiscenseData(int stage, int hash, int level)
        {
            StageLocation = stage;
            StageGroupHash = hash;
            Level = level;
        }
    }

    public enum Type : byte
    {
        head = 0x00,
        hand = 0x01,
        body = 0x02,
        arms = 0x03,
        legs = 0x04,
        foot = 0x05,
        earing = 0x06,
        ring = 0x07,
        support = 0x08,
        necklace = 0x09,
        equipped = 0x10
    }

    public class Item
    {
        public UInt32 Id;
        public int ItemSlot;
        public byte Type;

        public bool Stacked;
        public bool Instance;

        public Position Positioning;
        public LiscenseData Licensing;

        public Item(UInt32 ItemID)
        {
            Id = ItemID;
        }
    }
}
