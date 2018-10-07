using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WL_OA.NET;

namespace WpfApp1.Data.NDAL
{
    /// <summary>
    /// 网络任务管理信息（异步支持）
    /// </summary>
    public class NetTaskInfo
    {
        /// <summary>
        /// 默认超时设置
        /// </summary>
        public const int DEFAULT_TIME_OUT_IN_MS = -1;

        public NetTaskInfo(string requestID, NetHandler handler,
            object callContext = null, int timeoutInMs = DEFAULT_TIME_OUT_IN_MS)
        {
            RequestID = requestID;
            Handler = handler;
            CallContext = callContext;
            TimeoutInMs = timeoutInMs;

            StartCallTimeInMs = (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds;
        }

        /// <summary>
        /// 请求唯一ID
        /// </summary>
        public string RequestID { get; set; }

        /// <summary>
        /// 回调函数
        /// </summary>
        public NetHandler Handler { get; set; }

        /// <summary>
        /// 任务启动时间
        /// </summary>
        public double StartCallTimeInMs { get; set; }
        
        /// <summary>
        /// 超时时间（毫秒），小于0认为是不设置超时
        /// </summary>
        public int TimeoutInMs { get; set; }

        /// <summary>
        /// 请求上下文
        /// </summary>
        public object CallContext { get; set; }
    }

    public class NetTaskManage
    {

    }
}
