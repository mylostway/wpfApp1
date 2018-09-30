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

namespace WpfApp1.Panels.extend_control
{
    /// <summary>
    /// TabViewPage.xaml 的交互逻辑
    /// </summary>
    public partial class TabViewPage : UserControl
    {
        public TabViewPage()
        {
            InitializeComponent();
        }

        public void AddTab(string title,UserControl addControl)
        {
            var item = new TabItemEx();
            item.Header = title;
            item.ToolTip = title;
            item.Height = 28;
            item.Margin = new Thickness(0, 0, 5, 1);
            item.Background = new SolidColorBrush(Colors.LightGray);
            //item.BorderThickness = new Thickness(1, 1, 1, 1);
            item.Content = addControl;
            tab_Main.Items.Add(item);
            
            // 设置最新点击的Item
            tab_Main.SelectedItem = item;
        }
    }
}
