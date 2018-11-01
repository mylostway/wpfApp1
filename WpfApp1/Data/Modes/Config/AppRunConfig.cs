using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Data.Modes.Config
{
    
    public class AppRunConfig
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        public static string ServerHost { get; private set; } = "localhost";

        /// <summary>
        /// 服务器端口
        /// </summary>
        public static int ServerPort { get; private set; } = 8907;

        /// <summary>
        /// 是否单机测试模式（使用Fake数据源）
        /// </summary>
        public static bool IsSingleTestMode { get; private set; } = true;
    }
}
