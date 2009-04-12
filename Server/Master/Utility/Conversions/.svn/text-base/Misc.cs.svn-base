using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Utility.Conversions
{
    public static class Misc
    {
        public static uint ToBigEndian(uint source)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (uint)(source >> 24) |
                             ((source << 8) & 0x00FF0000) |
                             ((source >> 8) & 0x0000FF00) |
                              (source << 24);
            }
            return source;
        }

        /// <summary>
        /// Account Options - Comma Delimited
        /// 0     int Donator 
        /// 1     int # Character Slots
        /// 2     int # Unlocked Characters
        /// X     X     Unlocked Characters
        /// 0     Terminator
        /// </summary>
        public static int[] GetOptions(string options)
        {
            int[] Options = new int[options.Length];

            int myOption = 0;
            int value = 0;
            int length = 0;
            int pos = 0;

            byte[] bArray = new byte[4];

            while (pos < options.Length)
            {
                if (options[pos] == ',')
                {
                    byte[] nArray = new byte[length];
                    Buffer.BlockCopy(bArray, 0, nArray, 0, length);
                    string temp = BitConverter.ToString(nArray).Replace("-0", "");
                    temp = temp.Replace("-", "");

                    value = Convert.ToInt32(temp);

                    Options[myOption] = value;
                    myOption++;

                    length = 0;

                }
                else
                {
                    bArray[length++] = (byte)UInt32.Parse(options[pos].ToString());
                }
                pos++;
            }

            return Options;
        }

        public static string GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

        public static string HexBytes(byte[] data, int index, int length)
        {

            string sDump = (length > 0 ? BitConverter.ToString(data, index, length) : "");
            string[] sDumpHex = sDump.Split('-');
            List<string> lstDump = new List<string>();

            string sHex = "";
            string sAscii = "";
            char cByte = '\0';

            if (sDump.Length > 0)
            {
                for (Int32 iCount = 0; iCount < sDumpHex.Length; iCount++)
                {
                    cByte = Convert.ToChar(data[index + iCount]);
                    sHex += sDumpHex[iCount] + ' ';

                    if (char.IsWhiteSpace(cByte) || char.IsControl(cByte))
                    {
                        cByte = '.';
                    }

                    sAscii += cByte.ToString();
                    if ((iCount + 1) % 16 == 0)
                    {
                        lstDump.Add(sHex + " " + sAscii);
                        sHex = "";
                        sAscii = "";
                    }
                }
                if (sHex.Length > 0)
                {
                    if (sHex.Length < (16 * 3)) sHex += new string(' ', (16 * 3) - sHex.Length);
                    lstDump.Add(sHex + " " + sAscii);
                }
            }
            string retval = "";
            for (Int32 iCount = 0; iCount < lstDump.Count; iCount++)
            {
                retval += lstDump[iCount] + "\n";

            }
            return retval;

        }

        public static string HexBytes(byte[] data)
        {
            int index = 0;
            int length = data.Length;

            string sDump = (length > 0 ? BitConverter.ToString(data, index, length) : "");
            string[] sDumpHex = sDump.Split('-');
            List<string> lstDump = new List<string>();

            string sHex = "";
            string sAscii = "";
            char cByte = '\0';

            if (sDump.Length > 0)
            {
                for (Int32 iCount = 0; iCount < sDumpHex.Length; iCount++)
                {
                    cByte = Convert.ToChar(data[index + iCount]);
                    sHex += sDumpHex[iCount] + ' ';

                    if (char.IsWhiteSpace(cByte) || char.IsControl(cByte))
                    {
                        cByte = '.';
                    }

                    sAscii += cByte.ToString();
                    if ((iCount + 1) % 16 == 0)
                    {
                        lstDump.Add(sHex + " " + sAscii);
                        sHex = "";
                        sAscii = "";
                    }
                }
                if (sHex.Length > 0)
                {
                    if (sHex.Length < (16 * 3)) sHex += new string(' ', (16 * 3) - sHex.Length);
                    lstDump.Add(sHex + " " + sAscii);
                }
            }
            string retval = "";
            for (Int32 iCount = 0; iCount < lstDump.Count; iCount++)
            {
                retval += lstDump[iCount] + "\n";

            }
            return retval;

        }
    }
}
