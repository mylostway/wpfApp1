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
    public partial class EditAirLineControl : UserControl, IDialogPanel
    {
        public EditAirLineControl()
        {
            InitializeComponent();

            Init(AppRunConfigs.Instance.IsSingleTestMode ? 
                FakeDataHelper.Instance.GenData<AirwayEntity>() : null);
        }


        public AirwayEntity EditInfo { get; set; } = new AirwayEntity();

        public void Init(AirwayEntity editInfo)
        {
            SetPanelVisible(false);

            if (null == editInfo) EditInfo = new AirwayEntity();
            else EditInfo = editInfo;

            this.DataContext = EditInfo;

            this.cbx_isUsable.IsChecked = EditInfo.Fusable > 0; 
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            //this.grid_data.DataContext = EditEntity;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            EditInfo.Fusable = (this.cbx_isUsable.IsChecked == true) ? 1 : 0;
            
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
