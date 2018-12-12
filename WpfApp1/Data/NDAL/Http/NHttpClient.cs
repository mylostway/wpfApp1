using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using WL_OA.Data;
using WL_OA.NET;
using WpfApp1.Data.Helpers;
using WpfApp1.Base;

using Newtonsoft.Json;
using WpfApp1.Data.Test;
using WL_OA.Data.utils;
using System.Threading;

namespace WpfApp1.Data.NDAL
{
    public delegate void HttpResponseHandler(HttpResponse res, object context);

    public class HttpResponse
    {
        public HttpResponse() { }

        public HttpResponse(string responseContent = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }

        public HttpStatusCode StatusCode { get; set; }

        public string ResponseContent { get; set; }

        public string ResponseMsg { get; set; }

        #region 保留HttpResponseMessage中的字段，暂时没使用的地方，留作以后扩展

        public HttpContent Content { get; set; }

        public HttpResponseHeaders Headers { get; private set; }

        public HttpRequestMessage RequestMessage { get; set; }

        #endregion

    }

    /// <summary>
    /// Http接入Server
    /// </summary>
    public class NHttpClientDAL 
    {
        /// <summary>
        /// 默认超时时间
        /// </summary>
        static readonly double DEFAULT_REQUEST_TIME_OUT = AppRunConfigs.Instance.DefaultRequestTimeout;

        private NHttpClientDAL() { }

        static NHttpClientDAL()
        {
            s_client = new HttpClient();
            s_client.Timeout = TimeSpan.FromMilliseconds(DEFAULT_REQUEST_TIME_OUT);
        }

        private static HttpClient s_client = null;


        /// <summary>
        /// 设置请求超时
        /// </summary>
        /// <param name="timeout">null代表采用配置值，0代表请求无限时，其他值则为毫秒超时时间</param>
        public static void SetNetDALTimeout(double? timeout = null)
        {
            if (null == timeout)
            {
                s_client.Timeout = TimeSpan.FromMilliseconds(DEFAULT_REQUEST_TIME_OUT);
                return;
            }
            if (0 == timeout) s_client.Timeout = Timeout.InfiniteTimeSpan;
            s_client.Timeout = TimeSpan.FromMilliseconds(timeout.Value);
        }


        public static async Task GetAsync(string url, HttpResponseHandler callBack = null,
            double? timeout = null)
        {
            url = NetHelper.FormatRequestUrl(url);

            HttpResponse responseMsg = null;

            // 单机测试，使用FakeData生成随机测试数据模拟网络访问
            if (AppRunConfigs.Instance.IsSingleTestMode)
            {
                var genTypes = callBack.Method.GetGenericArguments();
                SAssert.MustTrue(genTypes.Length == 1, string.Format("非法的泛型回调在PostAsync {0}", genTypes));
                callBack?.Invoke(FakeDataHelper.Instance.CreateFakeDataNetResponse(genTypes[0]), null);
                return;
            }

            try
            {
                //SetNetDALTimeout(timeout);

                var httpResponse = await s_client.GetAsync(url, HttpCompletionOption.ResponseContentRead);

                string responseContent = null;

                if (httpResponse.IsSuccessStatusCode)
                {
                    var resultStr = await httpResponse.Content.ReadAsStringAsync();
                    responseContent = resultStr;
                }

                responseMsg = new HttpResponse(responseContent, httpResponse.StatusCode);
            }
            catch (Exception ex)
            {
                responseMsg = new HttpResponse(ex.Message, HttpStatusCode.ExpectationFailed);
            }

            callBack?.Invoke(responseMsg, null);
        }

        /// <summary>
        /// 模拟获取数据的随机耗时
        /// </summary>
        const int RANDOM_GEN_DATA_COST_TIME = 10 * 1000;

        /// <summary>
        /// 异步POST请求
        /// TOFIX:待添加超时逻辑
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="callBack"></param>
        public static async Task PostAsync<T>(string url, T param = null, 
            HttpResponseHandler callBack = null,double? timeout = null)
             where T : class,new()
        {
            /*
            var serializeKeyValPair = AppRunConfigs.DefaultKeyValueFormatter.Serialize(typeof(T).Name, param, null);
            
            var requestContent = new FormUrlEncodedContent(serializeKeyValPair);
            
            //requestContent.Headers.ContentType = 
            */

            // 单机测试，使用FakeData生成随机测试数据模拟网络访问
            if (AppRunConfigs.Instance.IsSingleTestMode)
            {
                var genTypes = callBack.Method.GetGenericArguments();                
                SAssert.MustTrue(genTypes.Length == 1,string.Format("非法的泛型回调在PostAsync {0}", genTypes));
                var genType = genTypes[0];
                var genNum = -1;
                if(genType.IsAssignableFrom(typeof(WL_OA.Data.dto.BaseOpResult)))
                {
                    genNum = 1;
                }
                // 生成随机等待时间，模拟网络请求耗时
                var randomGenDataCostTime = FakeDataHelper.Instance.GenRandomInt(RANDOM_GEN_DATA_COST_TIME);
                if (randomGenDataCostTime > RANDOM_GEN_DATA_COST_TIME / 20)
                {
                    await Task.Delay(randomGenDataCostTime);
                }
                // FIXME：目前超时情况下，没有关掉该网络请求，会导致提示了网路请求超时错误之后，仍然会返回数据结果（正确应该中断这次请求！）
                callBack?.Invoke(FakeDataHelper.Instance.CreateFakeDataNetResponse(genType,genNum), null);
                return;
            }

            HttpContent requestContent = null;

            if (null != param)
            {
                var strJson = JsonHelper.SerializeTo(param);

                requestContent = new StringContent(strJson);

                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            await PostContentAsync(url, requestContent, callBack, timeout);
        }


        /// <summary>
        /// 异步POST请求
        /// TOFIX:待添加超时逻辑
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="callBack"></param>
        public static async Task PostContentAsync(string url, HttpContent content, 
            HttpResponseHandler callBack, double? timeout = null)
        {
            url = NetHelper.FormatRequestUrl(url);

            HttpResponse responseMsg = null;

            try
            {
                //SetNetDALTimeout(timeout);

                var httpResponse = await s_client.PostAsync(url, content);

                string responseContent = null;

                if (httpResponse.IsSuccessStatusCode)
                {
                    string resultStr = await httpResponse.Content.ReadAsStringAsync();

                    responseContent = resultStr;
                }

                responseMsg = new HttpResponse(responseContent, httpResponse.StatusCode);
            }
            catch(Exception ex)
            {
                responseMsg = new HttpResponse(ex.ToString(), HttpStatusCode.ExpectationFailed);
            }

            callBack?.Invoke(responseMsg, null);
        }
    }
}
