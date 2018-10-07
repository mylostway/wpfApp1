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
using WpfApp1.Data.Test;
using WpfApp1.Data.NDAL;

using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.Data.dto;
using WL_OA.BLL.query;
using WL_OA.NET;


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

            if(!FirstInit)
            {
                ResetSearch();

                btn_search_Click(null, null);

                FirstInit = true;
            }            
        }

        private bool FirstInit = true;

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryFreBusinessCenterParam();
            queryParam.StartDate = DateTime.Parse(dp_startDate.Text);
            queryParam.EndDate = DateTime.Parse(dp_endDate.Text);
            queryParam.ListID = tbx_searchID.Text;

            NetworkDAL.RequestAsync("FreBusinessCenterBLL_GetEntityList",
                queryParam, new NetHandler(this.GetEntityListResponseCommHandler<FreBusinessCenterEntityViewMode>));
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
    }
}
