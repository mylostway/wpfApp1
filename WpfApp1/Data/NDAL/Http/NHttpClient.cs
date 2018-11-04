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
        private NHttpClientDAL() { }

        static NHttpClientDAL()
        {
            s_client = new HttpClient();
        }

        private static HttpClient s_client = null;

        public static async void GetAsync<T>(string url, T param, HttpResponseHandler callBack = null)
            where T : class
        {
            var queryUrl = string.Format("{0}?{1}", url, param);

            GetAsync(queryUrl, callBack);
        }

        public static async void GetAsync(string url, HttpResponseHandler callBack = null)
        {
            url = NetHelper.FormatRequestUrl(url);

            HttpResponse responseMsg = null;

            // 单机测试，使用FakeData生成随机测试数据模拟网络访问
            if (AppRunConfigs.Instance.IsSingleTestMode)
            {
                var genTypes = callBack.Method.GetGenericArguments();
                SAssert.MustTrue(genTypes.Length == 1, string.Format("非法的泛型回调在PostAsync {0}", genTypes));
                callBack?.Invoke(FakeDataHeler.Instance.CreateFakeDataNetResponse(genTypes[0]), null);
                return;
            }

            try
            {
                var httpResponse = await s_client.GetAsync(url, HttpCompletionOption.ResponseContentRead);

                string responseContent = null;

                if (httpResponse.IsSuccessStatusCode)
                {
                    var resultStr = await httpResponse.Content.ReadAsStringAsync();
                    responseContent = resultStr;
                }

                responseMsg = new HttpResponse(responseContent, httpResponse.StatusCode);
            }
            catch(Exception ex)
            {
                responseMsg = new HttpResponse(ex.Message, HttpStatusCode.ExpectationFailed);
            }
            
            callBack?.Invoke(responseMsg, null);
        }



        public static async void PostAsync<T>(string url, T param = null, HttpResponseHandler callBack = null)
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
                callBack?.Invoke(FakeDataHeler.Instance.CreateFakeDataNetResponse(genType,genNum), null);
                return;
            }

            HttpContent requestContent = null;

            if (null != param)
            {
                var strJson = JsonHelper.SerializeTo(param);

                requestContent = new StringContent(strJson);

                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            PostContentAsync(url, requestContent, callBack);
        }


        public static async void PostContentAsync(string url, HttpContent content, HttpResponseHandler callBack)
        {
            url = NetHelper.FormatRequestUrl(url);

            HttpResponse responseMsg = null;

            try
            {
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
