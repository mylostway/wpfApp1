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
using WL_OA.Data.utils;
using WL_OA.NET;

using WpfApp1.Data;
using WpfApp1.Data.NDAL;
using WpfApp1.Panels.Business.BaseInfoManage;

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
        }

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

            this.PostAsync("api/QueryDriverInfoList", queryParam,
                new HttpResponseHandler(this.GetEntityListResponseCommHandler<AirwayEntity>));
        }

        private void pi_edit_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void pi_del_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private async void btn_add_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private async void btn_modify_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (null == this.grid_data.SelectedItem) return;

            var data = this.grid_data.SelectedItem as AirwayEntity;

            SAssert.MustTrue(null != data, string.Format("绑定数据异常！"));

            var dialog = new EditAirLineControl();
            dialog.EditInfo = data;
            var result = await dialog.SmothShow();
            if (result)
            {
                var addEntity = dialog.EditInfo;
                if (null != addEntity)
                {
                    addEntity.IsValid();
                    this.PostAsync("api/AddDriverInfo", addEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }
        }

        private void btn_delete_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private async void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EditAirLineControl();
            var result = await dialog.SmothShow();
            if (result)
            {
                var addEntity = dialog.EditInfo;
                if (null != addEntity)
                {
                    addEntity.IsValid();
                    this.PostAsync("api/AddDriverInfo", addEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }
        }

        private void btn_modify_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
