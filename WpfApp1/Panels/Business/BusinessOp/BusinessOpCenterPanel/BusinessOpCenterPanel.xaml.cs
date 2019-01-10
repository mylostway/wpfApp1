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
using System.Net;
using System.Windows.Threading;

using WpfApp1.Data;

using WpfApp1.Data.NDAL;

using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.Data.dto;
using WL_OA.BLL.query;
using WL_OA.NET;
using WpfApp1.Panels.Business.BusinessOp.BusinessOpCenterPanel;
using MaterialDesignThemes.Wpf;
using WL_OA.Data;

namespace WpfApp1.Panels.business
{
    /// <summary>
    /// BusinessOpCenterPanel.xaml 的交互逻辑
    /// </summary>
    public partial class BusinessOpCenterPanel : UserControl
    {
        public BusinessOpCenterPanel()
        {
            InitializeComponent();

            this.cbx_searchDateType1.BindComboxToEnums<FreBusinessSearchTimeTypeEnums>();
            this.cbx_searchIDType1.BindComboxToEnums<FreBusinessSearchDtoObjectTypeEnums>();
            this.cbx_searchStatue1.BindComboxToEnums<FreBusinessSearchDtoStatusTypeEnums>();
        }


        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryFreBusinessCenterParam();
            queryParam.StartDate = dp_startDate.GetDateTimeVal();
            queryParam.EndDate = dp_endDate.GetDateTimeVal();
            queryParam.ListID = tbx_searchID.Text;

            this.PostAsync("api/GetFreBusinessList", queryParam,
                new HttpResponseHandler(this.GetEntityListResponseCommHandler<FreBusinessCenterEntity>));
        }

        private void ResetSearch()
        {
            tbx_searchID.Text = "";
            dp_startDate.Text = DateTimeHelper.GetDayStringOnAddingDays();
            dp_endDate.Text = DateTimeHelper.GetDayStringOnAddingDays(1);
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            ResetSearch();
        }

        private async void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EditBusinessListPanel();
            var result = (bool)await dialog.SmothShow();
            if (result)
            {
                var editInfo = dialog.EditInfo;
                if (null != editInfo)
                {
                    editInfo.IsValid();
                    this.PostAsync("api/AddFreBusiness", editInfo, 
                        new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }
        }
    }
}
