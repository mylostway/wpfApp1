using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WL_OA.Data;
using WpfApp1.Data.NDAL;
using WpfApp1.Panels.extend_control;
using WpfApp1.Panels.functional;

namespace WpfApp1.Panels
{
    static class ExHelper
    {
        /// <summary>
        /// 发起Http Get请求
        /// </summary>
        /// <param name="control"></param>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task GetAsync(this Control control, string url, HttpResponseHandler callback = null, object context = null)
        {
            WaitingDialog.Show();
            await NHttpClientDAL.GetAsync(url, callback);
        }

        /// <summary>
        /// 发起Http Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="callback"></param>
        /// <param name="context"></param>
        public static async void PostAsync<T>(this Control control, string url, T param = null, HttpResponseHandler callback = null, object context = null)
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


        /// <summary>
        /// 绑定指定类型枚举到Combobox
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combox"></param>
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

            bool isSelectDefault = false;

            foreach(var e in getList)
            {
                if(e.IsSelected)
                {
                    combox.SelectedValue = e.EValue;
                    isSelectDefault = true;
                    break;
                }
            }

            if (!isSelectDefault) combox.SelectedIndex = 0;
        }


        /// <summary>
        /// 绑定指定类型枚举到多选Combobox
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combox"></param>
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


        /// <summary>
        /// 枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objVal"></param>
        /// <returns></returns>
        public static T ToEnumVal<T>(this object objVal)
        {
            if (null == objVal) return default(T);
            var strEnumVal = objVal.ToString();
            return EnumHelper.ToEnumVal<T>(strEnumVal);
        }



        /// <summary>
        /// 获取DatePicker的日期值（自动检查和校验日期格式）
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static DateTime? GetDateTimeVal(this DatePicker dp)
        {
            var dpText = dp.Text;
            if (string.IsNullOrEmpty(dpText)) return null;
            DateTime parseVal;
            if(!DateTime.TryParse(dpText,out parseVal))
            {
                MessageBox.Show("错误的日期格式");
                dp.Focus();
                return null;
            }
            return parseVal;
        }
    }
}
