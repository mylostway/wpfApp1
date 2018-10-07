using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Data.NDAL
{
    /// <summary>
    /// 和服务器沟通登录之后的会话信息
    /// </summary>
    public class NetClientSession
    {
        private NetClientSession(string loginUserName,string sessionID)
        {
            m_loginUserName = loginUserName;
            m_sessionID = sessionID;
        }

        private static NetClientSession s_instance = null;

        /// <summary>
        /// 登录用户名
        /// </summary>
        private string m_loginUserName;
        /// <summary>
        /// sessionID
        /// </summary>
        private string m_sessionID;

        public static void BuildNetSession(string loginUserName, string sessionID)
        {
            if (null != s_instance) 
                throw new Exception(string.Format("当前已登录 {0}，如需转换帐号，请先退出登录！", loginUserName));
            s_instance = new NetClientSession(loginUserName, sessionID);
        }

        public static string LoginUserName
        {
            get
            {
                if (null == s_instance) throw new Exception("请先登录！");
                return s_instance.m_loginUserName;
            }
        }

        public static string SessionID
        {
            get
            {
                if (null == s_instance) throw new Exception("请先登录！");
                return s_instance.m_sessionID;
            }
        }
    }


    public class NetClientSessionManager
    {

    }
}
