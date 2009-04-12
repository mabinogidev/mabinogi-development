using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Avalon.Utility.Debugging;

namespace Avalon.Managers.Database
{
    public partial class Database
    {
        public static string sConnectionString;
        public static System.Data.SqlClient.SqlConnection myConnection;

        public static void Initialize()
        {
            try
            {
                sConnectionString = Program.AvalonCfg.Database.ConnectionString;
                myConnection = new System.Data.SqlClient.SqlConnection(sConnectionString);
                myConnection.Open();
                Logger.Log(Logger.LogLevel.Info, "DBAccess", "Connection opened.");
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.LogLevel.Error, "DBAccess", "Connection Error: " + ex.Message);
                if (ex.InnerException != null)
                    Logger.Log(Logger.LogLevel.Error, "DBAccess", "Connection Sub-Error: " + ex.InnerException.Message);
            }
        }

        public static SqlDataReader Query(String query)
        {
            SqlCommand sqlc;

            try
            {
                sqlc = new SqlCommand(query, myConnection);
                SqlDataReader sdr = sqlc.ExecuteReader();

                sdr.Read();

                if (!sdr.HasRows) return sdr;
                else return sdr;
              
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.LogLevel.Error, "DBAccess", "Error, {0}", ex.Message);
                return null;
            }
        }
    }
}
