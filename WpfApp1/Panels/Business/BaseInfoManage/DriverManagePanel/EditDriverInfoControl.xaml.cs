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

            this.grid_data.DataContext = m_editEntity;
        }

        private DriverinfoEntity m_editEntity = new DriverinfoEntity();

        public DriverinfoEntity EditEntity
        {
            get { return m_editEntity; }
            set
            {
                m_editEntity = value;
                this.grid_data.DataContext = m_editEntity;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            //this.grid_data.DataContext = EditEntity;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            //NewEntity. = tbx_driverNo.Text;
            EditEntity.Fname = tbx_name.Text;
            EditEntity.Fphone1 = tbx_phone1.Text;
            EditEntity.Fphone2 = tbx_phone2.Text;
            EditEntity.FcertID = tbx_cert.Text;
            EditEntity.FDriverNo = tbx_driverCertNo.Text;
            EditEntity.Fstate = (short)((cbx_isInPosition.IsChecked == true) ? 1 : 0);
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
