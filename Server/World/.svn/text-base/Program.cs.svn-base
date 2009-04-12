using System;
using System.Collections.Generic;
using Avalon.Network;
using Avalon.Managers.Database;
using Avalon.Configuration;
using Avalon.PrimaryType;
using Avalon.Utility.Time;

namespace Avalon
{
    class Program
    {
        public static Dictionary<int, Character> CharacterList = new Dictionary<int, Character>();
        public static SocketServer AvalonSrv;
        public static Config AvalonCfg;

        static void Main(string[] args)
        {
            AvalonCfg = Config.Load("config.xml");
            AvalonSrv = new SocketServer(AvalonCfg.Server.IPAddress, AvalonCfg.Server.ListenPort);
           
            Database.Initialize();

            AvalonSrv.Listen();

            while (true)
            { 
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
