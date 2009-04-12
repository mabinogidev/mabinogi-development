using System;
using System.Collections.Generic;
using Avalon.Network;
using Avalon.Managers.Database;
using Avalon.Configuration;
using Avalon.Structures;
using Avalon.Utility.Time;

namespace Avalon
{
    class Program
    {
        public static List<Square> SquareList = new List<Square>();
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

                if (Time.timeGetTime() - Time.LastCheckTime > 30000)
                {
                    Time.LastCheckTime = Time.timeGetTime();
                    lock (AvalonSrv.ClientList)
                    {
                        // ping :3 pong :)
                        for (int i = 0; i < AvalonSrv.ClientList.Count; ++i)
                        {
                            //AvalonSrv.ClientList[i].Ping();
                        }
                    }
                }
            }
        }
    }
}
