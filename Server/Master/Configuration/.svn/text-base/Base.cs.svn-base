using System;
using System.Xml.Serialization;
using System.IO;

namespace Avalon.Configuration
{
    public class Config
    {
        #region Public Members

        public ConsoleSettings  Console   = new ConsoleSettings();
        public ServerSettings   Server    = new ServerSettings();
        public DatabaseSettings Database  = new DatabaseSettings();

        #endregion

        #region Private Members

        private static XmlSerializer m_xmlSerializer = new XmlSerializer(typeof(Config));
        
        #endregion

        #region Constructors

        /// <summary>
        /// This function reads and stores variables inside an XML file then returns an instance of Config.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Config Load(string filename)
        {
            return (Config)m_xmlSerializer.Deserialize(new StreamReader(filename));
        }

        #endregion
    }
}
