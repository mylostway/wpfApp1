using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WL_OA.Data;
using WpfApp1.Data.NDAL;
using WpfApp1.Panels.functional;

namespace WpfApp1.Panels
{
    static class ExHelper
    {
        public static void GetAsync<T>(this UserControl control, string url, T param = null, HttpResponseHandler callback = null, object context = null)
            where T : class, new()
        {
            WaitingDialog.Show();
            NHttpClientDAL.GetAsync(url, param, callback);
        }

        public static void PostAsync<T>(this UserControl control, string url, T param = null, HttpResponseHandler callback = null, object context = null)
            where T : class, new()
        {
            WaitingDialog.Show();
            NHttpClientDAL.PostAsync(url, param, callback);
        }


        /// <summary>
        /// 获取选择枚举的名称和对应值(和EnumHelper.GetEnumInfoListOnName相比自动添加"不限"选项)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static List<EnumInfo> GetEnumSelectInfoList<T>(this UserControl control,bool isNullAble = true)
        {
            var getList = EnumHelper.GetEnumInfoListOnName<T>();
            getList.Insert(0, new EnumInfo(null, "不限"));
            return getList;
        }



        public static T ToEnumVal<T>(this object objVal)
        {
            if (null == objVal) return default(T);
            var strEnumVal = objVal.ToString();
            return EnumHelper.ToEnumVal<T>(strEnumVal);
        }

    }
}
