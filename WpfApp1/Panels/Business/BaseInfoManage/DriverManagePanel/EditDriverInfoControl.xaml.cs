using MaterialDesignThemes.Wpf;
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
using WpfApp1.Data;

namespace WpfApp1.Panels.Business.BaseInfoManage
{
    /// <summary>
    /// EditDriverInfoControl.xaml 的交互逻辑
    /// </summary>
    public partial class EditDriverInfoControl : UserControl, IDialogPanel
    {
        public EditDriverInfoControl()
        {
            InitializeComponent();

            if (AppRunConfigs.Instance.IsSingleTestMode)
                Init(ClientFakeDataHelper.Instance.GenData<DriverinfoEntity>());
        }


        public DriverinfoEntity EditInfo { get; set; } = new DriverinfoEntity();

        public void Init(DriverinfoEntity editInfo)
        {
            SetPanelVisible(false);

            if (null == editInfo) EditInfo = new DriverinfoEntity();
            else EditInfo = editInfo;

            this.DataContext = EditInfo;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            EditInfo.FworkState = (this.cbx_isInPosition.IsChecked == true) ? 1 : 0;

            if (EditInfo.CheckValid())
            {
                DialogHost.CloseDialogCommand.Execute(true, this);
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void SetPanelVisible(bool yes = true)
        {
            if (yes) this.rootLayout.Visibility = Visibility.Visible;
            else this.rootLayout.Visibility = Visibility.Hidden;
        }
    }
}
