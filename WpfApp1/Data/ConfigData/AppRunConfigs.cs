using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WL_OA.Data;
using WL_OA.Data.utils.cfg;
using WpfApp1.Data.Helpers;

namespace WpfApp1.Data
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
        /// 服务器IP，使用json反序列化配置出来的信息，不能用private set，否则设置不到值
        /// </summary>
        public string ServerHost { get; set; } = "";

        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerPort { get; set; } = 0;


        /// <summary>
        /// 默认超时时间（毫秒）
        /// </summary>
        public int DefaultRequestTimeout { get; set; } = 5000;


        /// <summary>
        /// 默认客户端阻塞等待超时时间（毫秒）
        /// </summary>
        public int DefaultClientWaitTimeout { get; set; } = 5000;


        /// <summary>
        /// 是否单机测试模式（使用Fake数据源）
        /// </summary>
        public bool IsSingleTestMode { get; set; } = false;


        /// <summary>
        /// 是否自动生成测试数据(发到后台测试)
        /// </summary>
        public bool IsCreateDataForTest { get; set; } = false;


        /// <summary>
        /// 是否设定请求超时（当单机调试的时候会很慢，如果设计超时会导致到服务端的请求异常而干扰调试。正式环境设置成true）
        /// </summary>
        public bool IsEnableRequestTimeout { get; set; } = true;


        /// <summary>
        /// 定时向Server请求数据更新的间隔，默认为5分钟(毫秒)
        /// </summary>
        public int RefereshServerDataIntervalInMs { get; set; } = 5 * 60 * 1000;

        /// <summary>
        /// 
        /// </summary>
        public static readonly KeyValueFormatter DefaultKeyValueFormatter = new KeyValueFormatter();
    }
}
