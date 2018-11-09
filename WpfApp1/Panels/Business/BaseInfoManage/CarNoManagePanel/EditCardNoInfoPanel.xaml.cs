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
    public partial class EditCardNoInfoPanel : Window
    {
        private static EditCardNoInfoPanel Instance = new EditCardNoInfoPanel();

        public EditCardNoInfoPanel()
        {
            InitializeComponent();

            // 采用hide替代close，省下多次创建窗口的开销
            this.Closing += EditPanel_Closing;

            this.grid_data.DataContext = EditEntity;
        }

        private void EditPanel_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private DriverinfoEntity m_editEntity = null;

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

            this.grid_data.DataContext = EditEntity;
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

            this.DialogResult = true;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            //this.Close();
            this.Hide();
        }        

        /// <summary>
        /// 使用默认编辑窗口编辑数据
        /// </summary>
        /// <param name="editEntity"></param>
        /// <returns></returns>
        public static DriverinfoEntity Show(DriverinfoEntity editEntity = null)
        {
            Instance.EditEntity = editEntity;

            var bRet = Instance.ShowDialog();

            if (bRet != null && bRet.Value)
            {
                return Instance.EditEntity;
            }

            return null;
        }

        
    }
}
