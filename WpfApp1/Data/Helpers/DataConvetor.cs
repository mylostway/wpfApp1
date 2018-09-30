using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Data
{
    public class DataConvetor
    {
        const string STR_COM_YES = "是";
        const string STR_COM_NO = "否";

        /// <summary>
        /// 转换bool值到可视的（是/否）
        /// </summary>
        /// <param name="bVal"></param>
        /// <returns></returns>
        public static string ConvertBoolToStrSeen(bool bVal)
        {
            return bVal ? STR_COM_YES : STR_COM_NO;
        }


        /// <summary>
        /// 转换字符串(是/否)到bool值
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        public static bool ConvertStrBoolToVal(string strVal)
        {
            if (strVal == STR_COM_YES) return true;
            return false;
        }

    }
}
