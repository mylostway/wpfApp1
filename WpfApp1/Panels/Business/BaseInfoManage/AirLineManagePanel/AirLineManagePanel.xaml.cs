using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WL_OA.Data.dto;
using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.NET;

using WpfApp1.Data;
using WpfApp1.Data.NDAL;
using WpfApp1.Data.Test;

namespace WpfApp1.Panels.business
{
    /// <summary>
    /// GoodsInfoManagePanel.xaml 的交互逻辑
    /// </summary>
    public partial class AirLineManagePanel : UserControl
    {
        public AirLineManagePanel()
        {
            InitializeComponent();

            if (FirstInit) btn_search_Click(null, null);
        }

        private bool FirstInit = true;

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryAirLineInfoParam();
            queryParam.AirName = tbx_searchCnName.Text;
            var searchIDTxt = tbx_searchID.Text;
            if (!string.IsNullOrEmpty(searchIDTxt))
            {
                int nID = 0;
                if(!int.TryParse(searchIDTxt, out nID))
                {
                    MessageBox.Show(string.Format("非法的查询航线编号参数，请填入数字"));
                    return;
                }
                queryParam.AirLineNo = nID;
            }

            NetworkDAL.RequestAsync("AirLineInfoBLL_GetEntityList",
                queryParam, new NetHandler(this.GetEntityListResponseCommHandler<GoodsinfoEntityViewMode>));
        }
    }
}
