using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

    }
}
