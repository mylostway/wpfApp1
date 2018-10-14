using System;
using System.Collections.Generic;
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

using WpfApp1.Data;
using WpfApp1.Data.Test;
using WpfApp1.Data.NDAL;
using WpfApp1.Panels.business;

using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.Data.dto;
using WL_OA.BLL.query;
using WL_OA.NET;


namespace WpfApp1.Panels.Business.BaseInfoManage
{
    /// <summary>
    /// AddDriverInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AddDriverInfoPanel : UserControl
    {
        public AddDriverInfoPanel()
        {
            InitializeComponent();

            //this.DataContext = new DriverinfoEntityViewMode();
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            var ne = new DriverinfoEntity();
            ne.Fname = tbx_name.Text;
            ne.Fphone1 = tbx_phone1.Text;
            ne.Fphone2 = tbx_phone2.Text;
            ne.FDriverNo = tbx_driverCertNo.Text;
            ne.FcertID = tbx_cert.Text;
            ne.FworkState = (cbx_isInPosition.IsChecked == true) ? 1 : 0;

            //NetworkDAL.RequestAsync("DriverInfoBLL_AddEntity",
            //    ne, new NetHandler(this.AddEntityResponseCommHandler<DriverinfoEntity>));

            NHttpClientDAL.PostAsync("api/Datas/QueryDriverInfoList",
               ne, new HttpResponseHandler(this.GetEntityListResponseCommHandler<DriverinfoEntityViewMode>));
        }
    }
}
