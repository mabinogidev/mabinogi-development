using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Avalon.Utility.Stream;

namespace Avalon.Utility.Time
{
    /// <summary>
    /// includes windows time functions and the allm ServerTime function.
    /// </summary>
    public class Time
    {
        [DllImport("winmm.dll")]
        public static extern uint timeGetTime();
        public static UInt32 LastCheckTime = timeGetTime();

        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public static byte[] ServerTime(int hash)
        {
            PacketWriter Writer = new PacketWriter();

            Writer.Write((ushort)hash);
            Writer.Write((ushort)0x1769);
            Writer.Write((ushort)DateTime.Now.Year);
            Writer.Write((ushort)DateTime.Now.Month);
            Writer.Write((ushort)DateTime.Now.Day);
            Writer.Write((ushort)DateTime.Now.Hour);
            Writer.Write((ushort)DateTime.Now.Minute);
            Writer.Write((ushort)DateTime.Now.Second);
            Writer.Write((ushort)DateTime.Now.Millisecond);

            return Writer.UnderlyingStream.ToArray();
        }

    }
}
