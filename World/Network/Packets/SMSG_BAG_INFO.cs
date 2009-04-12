using System;
using System.Collections.Generic;
using System.Text;
using Avalon.PrimaryType;

namespace Avalon.Network.Packets
{
    public sealed class SMSG_BAG_INFO : Packet
    {
        public SMSG_BAG_INFO(SocketClient sockstate) : base(64141)
        {
            this.EnsureCapacity(256);
            
            // BagState
            // BagNumber
            // ExpireDate

            // 30 00 E0 55 8D FA CC E1 3C 39 01 00 00 00 62 53  0.àU.úIá<9....bS
            // A9 DA 02 00 00 00 AF D5 69 17 0F 27 01 00 01 00  cU...._Oi..'....
            // 00 00 00 00 00 00 00 00 E9 CD 3C 39 00 00 00 00  ........éI<9....

            HashArray hArray = new HashArray((int)Hash.Bag, sockstate.Character.BagCount);

        }
    }
}
