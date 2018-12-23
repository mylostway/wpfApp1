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


namespace WpfApp1.Panels.business
{
    /// <summary>
    /// GoodsInfoManagePanel.xaml 的交互逻辑
    /// </summary>
    public partial class WharfsManagePanel : UserControl
    {
        public WharfsManagePanel()
        {
            InitializeComponent();

            if (FirstInit) btn_search_Click(null, null);
        }

        private bool FirstInit = true;

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryWharfInfoParam();
            queryParam.Alias = tbx_searchAlias.Text;
            queryParam.Mark = tbx_searchMark.Text;
            queryParam.WharfName = tbx_searchMt.Text;
            queryParam.Area = tbx_searchArea.Text;

            //NetworkDAL.RequestAsync("WharfInfoBLL_GetEntityList",
            //    queryParam, new NetHandler(this.GetEntityListResponseCommHandler<WharfinfoEntityViewMode>));

            this.PostAsync("api/Datas/QueryWharfInfoList",
                queryParam, new HttpResponseHandler(this.GetEntityListResponseCommHandler<WharfinfoEntityViewMode>));
        }
    }
}
