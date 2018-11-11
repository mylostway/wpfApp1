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
using WpfApp1.Base;
using WpfApp1.Panels.functional;

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

            this.PostAsync("api/QueryDriverInfoList", queryParam, 
                new HttpResponseHandler(this.GetEntityListResponseCommHandler<DriverinfoEntityViewMode>));
        }


        private async void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EditDriverInfoControl();
            var result = (bool)await DialogHost.Show(dialog, "tabContentDialogHost");
            if (result)
            {
                var addEntity = dialog.EditEntity;
                if (null != addEntity)
                {
                    WaitingDialog.Show();
                    NHttpClientDAL.PostAsync("api/AddDriverInfo",
                        addEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }
        }

        private async void PackIcon_Edit(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;

            var data = control.DataContext as DriverinfoEntityViewMode;

            SAssert.MustTrue(null != data,string.Format("绑定数据异常！"));

            var dialog = new EditDriverInfoControl();
            dialog.EditEntity = data;
            var result = (bool)await DialogHost.Show(dialog, "tabContentDialogHost");
            if (result)
            {
                var addEntity = dialog.EditEntity;
                if (null != addEntity)
                {
                    WaitingDialog.Show();
                    NHttpClientDAL.PostAsync("api/UpdateDriverInfo",
                        addEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }

            //NHttpClientDAL.PostAsync("api/AddDriverInfo",
            //        editEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
        }
        

        private void PackIcon_Del(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;

            var data = control.DataContext as DriverinfoEntityViewMode;

            SAssert.MustTrue(null != data, string.Format("绑定数据异常！"));            

            var promptResult = MessageBox.Show(string.Format("确认删除记录？"),"操作确认", MessageBoxButton.OKCancel);            

            if(promptResult == MessageBoxResult.OK)
            {
                WaitingDialog.Show();

                // 删除记录
                NHttpClientDAL.GetAsync(string.Format("api/DelDriverInfo/${0}", data.Fid),
                    new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
            }
        }
    }
}
