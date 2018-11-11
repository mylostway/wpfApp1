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
using WpfApp1.Data;
using WpfApp1.Data.NDAL;
using WpfApp1.Data.Test;
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
            //if (FirstInit) btn_search_Click(null, null);
        }

        public override void EndInit()
        {
            base.EndInit();

            this.cbx_searchIDType1.ItemsSource = CustomerTypeEnumBindData;
        }

        private List<EnumInfo> CustomerTypeEnumBindData = EnumHelper.GetEnumInfoListOnName<QueryCustomerInfoIDTypeEnums>();        

        private bool FirstInit = true;

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryCustomerInfoParam();
            switch (cbx_searchDateType1.SelectedIndex)
            {
                case 1: queryParam.DateType = DateTypeEnums.InputTime; break;
                case 2: queryParam.DateType = DateTypeEnums.AduitTime; break;
                default: queryParam.DateType = null; break;
            }
            queryParam.StartDate = dp_startDate.SelectedDate;
            queryParam.EndDate = dp_endDate.SelectedDate;
            var selectedIDType1 = cbx_searchIDType1.SelectedValue;
            //queryParam.IDType1 = (QueryCustomerInfoIDTypeEnums)(selectedIDType1?.ToEnumVal<QueryCustomerInfoIDTypeEnums>());
            queryParam.ID1 = tbx_searchID.Text;

            this.PostAsync("api/GetCustomerInfoList", queryParam,
                new HttpResponseHandler(this.GetEntityListResponseCommHandler<CustomerInfoEntity>));            
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
