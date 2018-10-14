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

        //public Task<HttpResponseMessage> GetAsync(string url)
        //{
        //    return m_client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
        //}

        //public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        //{
        //    return m_client.PostAsync(url, content);
        //}

        public static async void GetAsync<T>(string url,T param = null, HttpResponseHandler callBack = null)
            where T : class
        {
            url = NetHelper.FormatRequestUrl(url);

            var httpResponse = await s_client.GetAsync(url, HttpCompletionOption.ResponseContentRead);

            string responseContent = null;

            if (httpResponse.IsSuccessStatusCode)
            {
                var resultStr = await httpResponse.Content.ReadAsStringAsync();
                responseContent = resultStr;
            }

            var responseMsg = new HttpResponse(responseContent, httpResponse.StatusCode);

            callBack?.Invoke(responseMsg, null);
        }



        public static async void PostAsync<T>(string url, T param = null, HttpResponseHandler callBack = null)
             where T : class
        {
            /*
            var serializeKeyValPair = AppRunConfigs.DefaultKeyValueFormatter.Serialize(typeof(T).Name, param, null);
            
            var requestContent = new FormUrlEncodedContent(serializeKeyValPair);
            
            //requestContent.Headers.ContentType = 
            */

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

            var httpResponse = await s_client.PostAsync(url, content);

            string responseContent = null;

            if (httpResponse.IsSuccessStatusCode)
            {
                string resultStr = await httpResponse.Content.ReadAsStringAsync();

                responseContent = resultStr;
            }

            var responseMsg = new HttpResponse(responseContent, httpResponse.StatusCode);

            callBack?.Invoke(responseMsg, null);
        }
    }
}
