using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WL_OA.Data;
using WL_OA.Data.entity;

namespace System
{
    public static class DataEx
    {
        /// <summary>
        /// 检查参数是否合法（根据约定的属性值检查）
        /// </summary>
        /// <param name="dataValidator"></param>
        /// <returns></returns>
        public static bool CheckValid(this IDataValidator dataValidator)
        {
            try
            {
                return dataValidator.IsValid();
            }
            catch (UserFriendlyException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
    }
}
