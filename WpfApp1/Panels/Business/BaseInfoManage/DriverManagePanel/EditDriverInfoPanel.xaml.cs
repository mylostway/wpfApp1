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
    public partial class EditDriverInfoPanel : Window
    {
        public EditDriverInfoPanel()
        {
            InitializeComponent();

            // for two way binding test
            EditEntity.Fname = "for test关生";
            EditEntity.FcertID = "2313489279x";
            EditEntity.Fphone1 = "15002094251";

            this.grid_data.DataContext = EditEntity;
        }

        public DriverinfoEntity EditEntity { get; set; } = new DriverinfoEntity();

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            //NewEntity. = tbx_driverNo.Text;
            EditEntity.Fname = tbx_name.Text;
            EditEntity.Fphone1 = tbx_phone1.Text;
            EditEntity.Fphone2 = tbx_phone2.Text;
            EditEntity.FcertID = tbx_cert.Text;
            EditEntity.FDriverNo = tbx_driverCertNo.Text;
            EditEntity.Fstate = (short)((cbx_isInPosition.IsChecked == true) ? 1 : 0);

            this.DialogResult = true;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
