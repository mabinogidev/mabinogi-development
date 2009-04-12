using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Structures
{
    public class Account
    {
        public List<Mobile> Characters = new List<Mobile>();
        public string Username;
        public string Password;
        public int Access = 0;
        public int AID = 0;
        public int[] Options;
    }
}
