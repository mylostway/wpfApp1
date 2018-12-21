using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Data
{
    public static class DynamicDataGridSearchColAttributeEx
    {
        /// <summary>
        /// 解析 DynamicDataGridSearchColAttribute的SearchColumnSettings 字段
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ExplainSearchColumnSettings(this DynamicDataGridSearchColAttribute attr)
        {
            return DynamicDataGridSearchColAttribute.ExplainSearchColumnSettings(attr);
        }
    }

    /// <summary>
    /// 动态绑定数据表列信息特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DynamicDataGridSearchColAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchColumnSettings">查询字段的列表设定，格式约定为 - field1：field1Name|field2：field2Name|...</param>
        /// <param name="keyColumn">关键列名称，默认为空，则取第一列</param>
        public DynamicDataGridSearchColAttribute(string searchColumnSettings, string keyColumn = "")
        {
            SearchColumnSettings = searchColumnSettings;
            KeyColumn = keyColumn;            
        }

        /// <summary>
        /// 关键列名称
        /// </summary>
        public string KeyColumn { get; set; }


        /// <summary>
        /// 查询字段的列表设定，格式约定为 - field1：field1Name|field2：field2Name|...
        /// </summary>
        public string SearchColumnSettings { get; set; }


        /// <summary>
        /// 解析 SearchColumnSettings 字段
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static Dictionary<string,string> ExplainSearchColumnSettings(DynamicDataGridSearchColAttribute attr)
        {
            var ret = new Dictionary<string, string>();

            if (null == attr) return ret;

            var str = attr.SearchColumnSettings;

            if (string.IsNullOrEmpty(str)) return ret;

            var fieldArr = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            if (0 == fieldArr.Length) return ret;

            foreach(var eField in fieldArr)
            {
                var lineArr = eField.Split(new char[] { ':'  }, StringSplitOptions.RemoveEmptyEntries);

                if (2 != lineArr.Length) continue;

                ret.Add(lineArr[0], lineArr[1]);
            }

            return ret;
        }
    }
}
