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
using System.Windows.Shapes;
using WL_OA.Data.entity;

namespace WpfApp1.Panels.Business.BaseInfoManage
{
    /// <summary>
    /// AddTest.xaml 的交互逻辑
    /// </summary>
    public partial class AddTest : Window
    {
        public AddTest()
        {
            InitializeComponent();
        }

        public DriverinfoEntity NewEntity { get; private set; } = new DriverinfoEntity();

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            //NewEntity. = tbx_driverNo.Text;
            NewEntity.Fname = tbx_name.Text;
            NewEntity.Fphone1 = tbx_phone1.Text;
            NewEntity.Fphone2 = tbx_phone2.Text;
            NewEntity.FcertID = tbx_cert.Text;
            NewEntity.FDriverNo = tbx_driverCertNo.Text;
            NewEntity.Fstate = (short)((cbx_isInPosition.IsChecked == true) ? 1 : 0);

            this.DialogResult = true;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
