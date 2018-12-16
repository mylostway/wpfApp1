using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WL_OA.Data;
using WpfApp1.Data.NDAL;
using WpfApp1.Panels.extend_control;
using WpfApp1.Panels.functional;

namespace WpfApp1.Panels
{
    static class ExHelper
    {
        public static async Task GetAsync(this UserControl control, string url, HttpResponseHandler callback = null, object context = null)
        {
            WaitingDialog.Show();
            await NHttpClientDAL.GetAsync(url, callback);
        }

        public static async void PostAsync<T>(this UserControl control, string url, T param = null, HttpResponseHandler callback = null, object context = null)
            where T : class, new()
        {
            WaitingDialog.Show();
            await NHttpClientDAL.PostAsync(url, param, callback);
        }

        /// <summary>
        /// 获取选择枚举的名称和对应值(和EnumHelper.GetEnumInfoListOnName相比自动添加"不限"选项)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static List<EnumInfo> GetEnumSelectInfoList<T>(this UserControl control,bool isNullAble = true)
        {            
            var getList = EnumHelper.GetEnumInfoList<T>();
            getList.Insert(0, new EnumInfo(null, "不限"));
            return getList;
        }


        public static void BindComboxToEnums<T>(this ComboBox combox)
        {
            var getList = EnumHelper.GetEnumInfoList<T>();
            var srcItems = combox.Items;

            foreach(var e in srcItems)
            {
                var cbxItem = e as ComboBoxItem;
                if(cbxItem != null) getList.Insert(0, new EnumInfo(null, cbxItem.Content.ToString()));
            }
            combox.Items.Clear();
            combox.ItemsSource = getList;
        }


        public static void BindMulComboxToEnums<T>(this MultiCombobox combox)
        {
            var enumList = EnumHelper.GetEnumInfoList<T>();

            var collection = new ObservableCollection<MultipleCheckboxModel>();

            foreach (var e in enumList)
            {
                collection.Add(new MultipleCheckboxModel(e.EValue, e.Name));
            }
            combox.ItemsSource = collection;
        }



        public static T ToEnumVal<T>(this object objVal)
        {
            if (null == objVal) return default(T);
            var strEnumVal = objVal.ToString();
            return EnumHelper.ToEnumVal<T>(strEnumVal);
        }

    }
}
