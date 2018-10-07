using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

using WL_OA.NET;
using WL_OA.Data.utils;


namespace WpfApp1.Data.NDAL
{
    public class NetworkDAL 
    {
        /// <summary>
        /// 无用任务清理数据间隔
        /// </summary>
        const int USELESS_TASK_CLEAR_INTER = 5 * 1000;

        //private static SimpleUdpClient s_netInstace = new SimpleUdpClient(NetBase.DEFAULT_PORT, DefaultNetResponseHandler);
        private static SimpleUdpClientC s_netInstace = new SimpleUdpClientC(NetBase.DEFAULT_PORT, DefaultNetResponseHandler);

        /// <summary>
        /// 废弃任务清理
        /// </summary>
        private static Timer m_useLessTaskClearTimer = new Timer(new TimerCallback(ClearUselessTask), null, USELESS_TASK_CLEAR_INTER, USELESS_TASK_CLEAR_INTER);

        private static Dictionary<string, NetTaskInfo> s_responseHandlerDic = new Dictionary<string, NetTaskInfo>();

        private static NetHandleResult DefaultNetResponseHandler(SimpleProtocolStruct response,IPEndPoint endpoint)
        {
            var method = response.RequestMethod;

            var requestID = response.RequestID;

            /*
            SAssert.MustTrue(s_responseMethodHandlerDic.ContainsKey(method),
                string.Format("Request {0} has no handler!", method));
                */

            NetTaskInfo taskInfo = null;
            lock (s_responseHandlerDic)
            {
                if (!s_responseHandlerDic.ContainsKey(requestID))
                {
                    return new NetHandleResult(NetHandleResultCode.Failed,
                        string.Format("请求ID = {0}没找到处理函数，可能是超时导致，请求参数为：{1}", requestID, response));
                }

                taskInfo = s_responseHandlerDic[requestID];

                s_responseHandlerDic.Remove(requestID);
            }

            if(taskInfo != null)
            {
                var handler = taskInfo.Handler;

                return handler.Invoke(response, endpoint);
            }
            else
            {
                return new NetHandleResult(NetHandleResultCode.Failed,
                        string.Format("请求ID = {0}没找到处理函数，可能是超时导致，请求参数为：{1}", requestID, response));
            }
        }

        public static void RequestAsync(SimpleProtocolStruct request, NetHandler callBack)
        {
            var taskInfo = new NetTaskInfo(request.RequestID, callBack);

            lock (s_responseHandlerDic)
            {
                SAssert.MustTrue(!s_responseHandlerDic.ContainsKey(request.RequestID),
                    string.Format("出现重复ID请求！！请求参数：{0}", request));

                s_responseHandlerDic.Add(request.RequestID, taskInfo);
            }

            s_netInstace.SendPacket(request);
        }

        public static void RequestAsync<T>(string requestMethod, T param, NetHandler callBack)
            where T : class
        {
            var request = new SimpleProtocolStruct(requestMethod,
                NetClientSession.LoginUserName, JsonHelper.SerializeTo(param),
                NetClientSession.SessionID);

            request.RequestID = NetTaskRequestIDGen.Gen();

            RequestAsync(request, callBack);
        }


        private static void ClearUselessTask(object state)
        {
            var removeList = new List<string>();

            var curTime = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;

            lock(s_responseHandlerDic)
            {
                foreach(var e in s_responseHandlerDic)
                {
                    var taskInfo = e.Value;
                    if(taskInfo.TimeoutInMs > 0 && 
                        curTime - taskInfo.StartCallTimeInMs >= taskInfo.TimeoutInMs)
                    {
                        removeList.Add(e.Key);
                    }
                }

                foreach (var e in removeList)
                {
                    var taskInfo = s_responseHandlerDic[e];
                    s_responseHandlerDic.Remove(e);
                    taskInfo.Handler.Invoke(SimpleProtocolStruct.TimeoutResponseStructInstance, null);
                }
            }
        }
        
    }
}
