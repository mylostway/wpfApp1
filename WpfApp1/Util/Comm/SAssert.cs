using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Util
{
    /// <summary>
    /// 断言类
    /// </summary>
    public class SAssert
    {
        /// <summary>
        /// 条件须为真
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        public static void IsTrue(bool condition,string msg = "")
        {            
            if (!condition)
            {
                if (string.IsNullOrEmpty(msg)) msg = string.Format("断言错误，条件不为true");
                throw new Exception(msg);
            }
        }
    }
}
