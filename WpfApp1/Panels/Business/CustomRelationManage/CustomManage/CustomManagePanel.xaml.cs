using MaterialDesignThemes.Wpf;
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
            //if (FirstInit) btn_search_Click(null, null);
        }

        public override void EndInit()
        {
            base.EndInit();

            this.cbx_searchDateType1.ItemsSource = EnumHelper.GetEnumInfoListOnName<CustomManagerSearchDateTypeEnums>();
            this.cbx_searchStatue1.ItemsSource = EnumHelper.GetEnumInfoListOnName<QueryCustomerInfoStateEnums>();
            this.cbx_searchIDType1.ItemsSource = EnumHelper.GetEnumInfoListOnName<QueryCustomerInfoIDTypeEnums>();//this.GetEnumSelectInfoList<QueryCustomerInfoIDTypeEnums>();

            this.cbx_searchDateType1.SelectedValue = CustomManagerSearchDateTypeEnums.None;
            this.cbx_searchIDType1.SelectedValue = QueryCustomerInfoIDTypeEnums.None;
            this.cbx_searchStatue1.SelectedValue = QueryCustomerInfoStateEnums.Usable;
        }

        //private List<EnumInfo> CustomerTypeEnumBindData = EnumHelper.GetEnumInfoListOnName<QueryCustomerInfoIDTypeEnums>();        

        private bool FirstInit = true;

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryCustomerInfoParam();
            queryParam.DateType = cbx_searchDateType1.SelectedValue.ToEnumVal<DateTypeEnums>();
            queryParam.StartDate = dp_startDate.SelectedDate;
            queryParam.EndDate = dp_endDate.SelectedDate;
            queryParam.IDType1 = cbx_searchIDType1.SelectedValue.ToEnumVal<QueryCustomerInfoIDTypeEnums>();
            queryParam.ID1 = tbx_searchID.Text;

            this.PostAsync("api/GetCustomerInfoList", queryParam,
                new HttpResponseHandler(this.GetEntityListResponseCommHandler<CustomerInfoEntity>));            
        }

        

        private async void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EditCustomerInfoPanel();
            var result = (bool)await DialogHost.Show(dialog, "tabContentDialogHost");
            if(result)
            {
                var editInfo = dialog.EditInfo;
                if(null != editInfo)
                {
                    editInfo.IsValid();
                    this.PostAsync("api/AddCustomerInfo", editInfo, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }
        }



    }
}
