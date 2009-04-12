using System;
using System.Collections.Generic;
using System.Text;

namespace Avalon.Configuration
{
	public class DatabaseSettings
	{
        // Default Settings
        public string servername = "GAMENAO";
        public string username = "sa";
        public string password = "puggy12";
        public string database = "LuniaDB";

        public string ConnectionString
        {
            get
            {
                return String.Format("SERVER={0};User ID={1};Password={2};Initial Catalog={3};MultipleActiveResultSets=true;Asynchronous Processing=true;", servername, username, password, database);
            }
        }
	}
}
