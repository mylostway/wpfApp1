using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Data.Helpers;

namespace WpfApp1.Base
{
    public class AppRunConfigs
    {
        private AppRunConfigs() { }

        static AppRunConfigs()
        {
            ServerHost = "localhost";
            ServerPort = 62056;
        }

        public static string ServerHost { get; private set; }

        public static int ServerPort { get; private set; }

        public static readonly KeyValueFormatter DefaultKeyValueFormatter = new KeyValueFormatter();
    }
}
