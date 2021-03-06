﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using WL_OA.Data;
using WL_OA.Data.dto;
using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.Data.utils;
using WpfApp1.Data;
using WpfApp1.Data.NDAL;

using WpfApp1.Panels.Business.CustomRelationManage;
using WpfApp1.Panels.functional;

namespace WpfApp1.Panels.business
{
    /// <summary>
    /// BusinessOpCenterPanel.xaml 的交互逻辑
    /// </summary>
    public partial class CustomManagePanel : UserControl
    {
        public CustomManagePanel()
        {
            InitializeComponent();
        }

        public override void EndInit()
        {
            base.EndInit();

            this.cbx_searchDateType1.BindComboxToEnums<CustomManagerSearchDateTypeEnums>();
            this.cbx_searchStatue1.BindComboxToEnums<QueryCustomerInfoStateEnums>();
            this.cbx_searchIDType1.BindComboxToEnums<QueryCustomerInfoIDTypeEnums>();
        }

        //private List<EnumInfo> CustomerTypeEnumBindData = EnumHelper.GetEnumInfoListOnName<QueryCustomerInfoIDTypeEnums>();        

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryCustomerInfoParam();
            queryParam.DateType = cbx_searchDateType1.SelectedValue.ToEnumVal<DateTypeEnums>();
            queryParam.StartDate = dp_startDate.SelectedDate;
            queryParam.EndDate = dp_endDate.SelectedDate;
            queryParam.IDType1 = cbx_searchIDType1.SelectedValue.ToEnumVal<QueryCustomerInfoIDTypeEnums>();
            queryParam.ID1 = tbx_searchID.Text;

            this.PostAsync("api/GetCustomerInfoList", queryParam,
                new HttpResponseHandler(this.GetEntityListResponseCommHandler<CustomerInfoQueryResultDTO>));            
        }



        private async void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EditCustomerInfoPanel();
            var result = await dialog.SmothShow();
            if (result)
            {
                var editInfo = dialog.EditInfo;
                if (null != editInfo)
                {
                    // 在Edit层已经校验过
                    //if(editInfo.CheckValid())
                        this.PostAsync("api/AddCustomerInfo", editInfo, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }
        }

        private async void pi_edit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (null == this.grid_data.SelectedItem) return;
            var data = this.grid_data.SelectedItem as AddCustomerInfoDTO;
            SAssert.MustTrue(null != data, string.Format("绑定数据异常！"));

            var dialog = new EditCustomerInfoPanel();
            dialog.Init(data);
            var result = await dialog.SmothShow();
            if (result)
            {
                var editInfo = dialog.EditInfo;
                if (null != editInfo)
                {
                    // 在Edit层已经校验过
                    //if(editInfo.CheckValid())
                    this.PostAsync("api/AddCustomerInfo", editInfo, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }
        }

        private async void pi_del_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
