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

            //TestDialog();

            if (FirstInit) btn_search_Click(null,null);            
        }

        private bool FirstInit = true;
        
        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryDriverInfoParams();
            queryParam.FName = tbx_searchName.Text;
            queryParam.Fphone = tbx_searchPhone1.Text;
            queryParam.Take = 5;

            NHttpClientDAL.PostAsync("api/QueryDriverInfoList",
                    queryParam, new HttpResponseHandler(this.GetEntityListResponseCommHandler<DriverinfoEntityViewMode>));            
        }


        private async void btn_add_Click(object sender, RoutedEventArgs e)
        {
            /*
            var addEntity = EditDriverInfoPanel.Show(new DriverinfoEntity());

            if(null != addEntity)
            {
                NHttpClientDAL.PostAsync("api/AddDriverInfo",
                    addEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
            }
            */

            var dialog = new EditDriverInfoControl();
            var result = (bool)await DialogHost.Show(dialog, "MainDialogHost");
            if (result)
            {
                var addEntity = dialog.EditEntity;
                if (null != addEntity)
                {
                    NHttpClientDAL.PostAsync("api/AddDriverInfo",
                        addEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }
        }

        private void PackIcon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;

            var data = control.DataContext as DriverinfoEntityViewMode;

            SAssert.MustTrue(null != data,string.Format("绑定数据异常！"));

            var editEntity = EditDriverInfoPanel.Show(data);

            if(null != editEntity)
            {
                DialogHost.Show(new WaitingDialog());

                DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(3), DispatcherPriority.DataBind, new EventHandler((timeSender, evetArgs) =>
                {
                    NHttpClientDAL.PostAsync("api/AddDriverInfo",
                        editEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));

                    var tTimer = (DispatcherTimer)timeSender;
                    tTimer.Stop();
                    
                }), Dispatcher.CurrentDispatcher);
                timer.Start();

                //NHttpClientDAL.PostAsync("api/AddDriverInfo",
                //        editEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
            }
        }

        private void PackIcon_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;

            var data = control.DataContext as DriverinfoEntityViewMode;

            SAssert.MustTrue(null != data, string.Format("绑定数据异常！"));            

            var promptResult = MessageBox.Show(string.Format("确认删除记录？"),"操作确认", MessageBoxButton.OKCancel);            

            if(promptResult == MessageBoxResult.OK)
            {
                DialogHost.Show(new WaitingDialog());

                // 删除记录
                NHttpClientDAL.GetAsync(string.Format("api/DelDriverInfo/${0}", data.Fid),
                    new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
            }
        }


        private async void TestDialog()
        {
            var dialog = new WaitingDialog();
            var result = (bool)await DialogHost.Show(dialog, "MainDialogHost");            
        }

        private void btn_modify_Click(object sender, RoutedEventArgs e)
        {
            WaitingDialog.Show();

            return;
        }
    }
}
