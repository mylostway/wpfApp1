using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WL_OA.Data;
using WL_OA.Data.utils.cfg;
using WpfApp1.Data.Helpers;

namespace WpfApp1.Base
{
    [Config("Configs/AppRunConfigs.json")]
    public class AppRunConfigs
    {
        static AppRunConfigs()
        {
            Instance = ConfigHelper.Get<AppRunConfigs>();
        }

        public static AppRunConfigs Instance { get; private set; }

        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerHost { get; private set; } = "localhost";

        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerPort { get; private set; } = 8907;


        /// <summary>
        /// 默认超时时间（毫秒）
        /// </summary>
        public int DefaultRequestTimeout { get; private set; } = 5000;


        /// <summary>
        /// 是否单机测试模式（使用Fake数据源）
        /// </summary>
        public bool IsSingleTestMode { get; private set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public static readonly KeyValueFormatter DefaultKeyValueFormatter = new KeyValueFormatter();
    }
}
