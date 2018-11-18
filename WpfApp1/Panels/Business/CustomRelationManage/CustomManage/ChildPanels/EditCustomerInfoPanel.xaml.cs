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

namespace WpfApp1.Panels.Business.CustomRelationManage
{
    /// <summary>
    /// EditCustomerInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class EditCustomerInfoPanel : UserControl
    {
        public EditCustomerInfoPanel()
        {
            InitializeComponent();

            var dic = new Dictionary<string, UIElement>();
            dic.Add("概要信息", new EditSumaryInfoPanel());
            dic.Add("资信信息", new EditAssertInfoPanel());
            dic.Add("配置信息", new EditCfgInfoPanel());
            dic.Add("其他信息", new EditOtherInfoPanel());
            dic.Add("录入信息", new EditInputInfoPanel());

            tab_editCustomerInfo.Init(dic);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
