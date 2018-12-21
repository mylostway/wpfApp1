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
using WL_OA.Data.entity;

namespace WpfApp1.Panels.Business.BaseInfoManage
{
    /// <summary>
    /// EditDriverInfoControl.xaml 的交互逻辑
    /// </summary>
    public partial class EditDriverInfoControl : UserControl
    {
        public EditDriverInfoControl()
        {
            InitializeComponent();

            Init(null);
        }


        public DriverinfoEntity EditInfo { get; set; } = new DriverinfoEntity();

        public void Init(DriverinfoEntity editInfo)
        {
            if (null == editInfo) EditInfo = new DriverinfoEntity();
            else EditInfo = editInfo;

            this.DataContext = EditInfo;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            //this.grid_data.DataContext = EditEntity;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {            
            //EditInfo.Fname = tbx_name.Text;
            //EditInfo.Fphone1 = tbx_phone1.Text;
            //EditInfo.Fphone2 = tbx_phone2.Text;
            //EditInfo.FcertID = tbx_cert.Text;
            //EditInfo.FDriverNo = tbx_driverCertNo.Text;
            //EditInfo.Fstate = (short)((cbx_isInPosition.IsChecked == true) ? 1 : 0);


        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
