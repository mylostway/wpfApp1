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

        /// <summary>
        /// 最大tab建立数
        /// </summary>
        const int MAX_CHILD_TAB_COUNT = 10;

        

        private double CalcTabHeaderWidth(int headTitleLen)
        {
            return TabItemEx.TAB_HEADER_CHAR_WIDTH * headTitleLen + TabItemEx.TAB_HEADER_CLOSE_BTN_WIDTH;
        }


        private double GetFixedTabWidth(double toAddTabsWidth)
        {
            var totalWidth = this.Width;

            var allTabs = tab_Main.Items;

            double calcWidth = 0;

            for(var i = 0;i < allTabs.Count;i++)
            {
                var ctl = allTabs[i] as TabItemEx;

                calcWidth += ctl.Width;
            }

            calcWidth += toAddTabsWidth;

            if (calcWidth > totalWidth)
            {
                double forShortTimes = calcWidth / totalWidth;

                for (var i = 0; i < allTabs.Count; i++)
                {
                    var ctl = allTabs[i] as TabItemEx;

                    ctl.ResizeOnTimes(forShortTimes);
                }
            }
            else
            {

            }

            return toAddTabsWidth;
        }

        public void AddTab(string title,UserControl addControl)
        {
            if(tab_Main.Items.Count > MAX_CHILD_TAB_COUNT)
            {
                MessageBox.Show("已经达到最大标签数，请关闭一些无用标签再点击");
                return;
            }

            var item = new TabItemEx();
            item.Header = title;
            item.ToolTip = title;
            item.Height = 28;
            item.Margin = new Thickness(3, 0, 0, 0);
            item.Content = addControl;

            var fixWidth = GetFixedTabWidth(addControl.Width);

            tab_Main.Items.Add(item);
            
            // 设置最新点击的Item
            tab_Main.SelectedItem = item;
        }
    }
}
