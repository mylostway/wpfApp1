using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Base;

namespace WpfApp1.Data.Helpers
{
    public class NetHelper
    {
        /// <summary>
        /// 请求路径不能有的字符
        /// </summary>
        static readonly char[] PATH_INVALID_CHAR = new char[] { '.', '~' };

        /// <summary>
        /// 格式化请求URL
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string FormatRequestUrl(string path)
        {
            if (path.IndexOf("://") > 0) return path;
            if (path.IndexOfAny(PATH_INVALID_CHAR) > 0) throw new Exception(string.Format("非法的请求路径:{0}", path));
            if (path[0] == '/') return string.Format("http://{0}:{1}{2}", AppRunConfigs.Instance.ServerHost, AppRunConfigs.Instance.ServerPort, path);
            return string.Format("http://{0}:{1}/{2}", AppRunConfigs.Instance.ServerHost, AppRunConfigs.Instance.ServerPort, path);
        }

    }
}
