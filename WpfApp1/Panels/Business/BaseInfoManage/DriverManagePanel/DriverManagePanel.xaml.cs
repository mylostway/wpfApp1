using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

using WpfApp1.Data;
using WpfApp1.Data.Test;
using WpfApp1.Data.NDAL;
using WpfApp1.Data.View;
using WpfApp1.Panels.extend_control;
using WpfApp1.Panels.Business.BaseInfoManage;

using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.Data.dto;
using WL_OA.BLL.query;
using WL_OA.NET;

using MaterialDesignThemes.Wpf;
using WL_OA.Data.utils;

namespace WpfApp1.Panels.business
{
    /// <summary>
    /// GoodsInfoManagePanel.xaml 的交互逻辑
    /// </summary>
    public partial class DriverManagePanel : UserControl
    {       
        public DriverManagePanel()
        {
            InitializeComponent();

            if (FirstInit) btn_search_Click(null,null);            
        }

        private bool FirstInit = true;
        
        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryDriverInfoParams();
            queryParam.FName = tbx_searchName.Text;
            queryParam.Fphone = tbx_searchPhone1.Text;
            queryParam.Take = 5;

            //NetworkDAL.RequestAsync("DriverInfoBLL_GetEntityList",
            //    queryParam, new NetHandler(this.GetEntityListResponseCommHandler<DriverinfoEntityViewMode>));

            NHttpClientDAL.PostAsync("api/QueryDriverInfoList", 
                queryParam, new HttpResponseHandler(this.GetEntityListResponseCommHandler<DriverinfoEntityViewMode>));
        }


        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var addPanel = new AddTest();

            var ret = addPanel.ShowDialog();

            if(ret.Value)
            {
                var addEntity = addPanel.NewEntity;

                NHttpClientDAL.PostAsync("api/AddDriverInfo",
                    addEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
            }
            else
            {
                MessageBox.Show("取消操作");
            }
        }
    }
}
