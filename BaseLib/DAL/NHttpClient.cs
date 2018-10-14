using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace BaseLib.DAL
{
    public class NHttpClient
    {
        private NHttpClient() { }

        private static HttpClient m_client = null;

        public static void Init(HttpMessageHandler handler = null)
        {
            m_client = new HttpClient();
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return m_client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
        }

        public Task<HttpResponseMessage> PostAsync(string url,HttpContent content)
        {
            return m_client.PostAsync(url, content);
        }
    }
}
