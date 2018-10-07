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

        /// <summary>
        /// 重新resize所有tabHeader
        /// </summary>
        /// <param name="headerTitleStrLen"></param>
        /// <returns></returns>
        private bool FixedTabHeaderWidth()
        {
            // 留下空间作备用，不要占满            
            var totalWidth = this.ActualWidth * 0.8;

            if(0 == totalWidth)
            {
                // 还未显示UI的时候，直接返回
                return true;
            }

            var allTabs = tab_Main.Items;

            double calcWidth = 0;

            for(var i = 0;i < allTabs.Count;i++)
            {
                var ctl = allTabs[i] as TabItemEx;

                if (double.IsNaN(ctl.ActualWidth)) calcWidth += TabItemEx.MIN_TAB_WIDTH;
                else calcWidth += ctl.ActualWidth;
            }

            if (calcWidth > totalWidth)
            {
                // FIXME:动态调整tabItem的Header大小未实现好，待修复，目前不能添加过长的tab
                return false;

                double forShortTimes = calcWidth / totalWidth;

                for (var i = 0; i < allTabs.Count; i++)
                {
                    var ctl = allTabs[i] as TabItemEx;

                    ctl.ResizeOnTimes(forShortTimes);
                }
            }
            else
            {
                for (var i = 0; i < allTabs.Count; i++)
                {
                    var ctl = allTabs[i] as TabItemEx;

                    ctl.ResizeToNormal();
                }
            }

            return true;
        }


        private Dictionary<string, TabItemEx> m_tabNamesItemDic = new Dictionary<string, TabItemEx>();

        public TabItemEx FindTabItemOnName(string tabName)
        {
            if (string.IsNullOrEmpty(tabName)) return null;
            if (m_tabNamesItemDic.ContainsKey(tabName)) return m_tabNamesItemDic[tabName];
            var allTabs = tab_Main.Items;
            for (var i = 0; i < allTabs.Count; i++)
            {
                var ctl = allTabs[i] as TabItemEx;
                if (ctl.Header.ToString() == tabName) return ctl;
            }
            return null;
        }


        public void AddTab(string title, UserControl addControl,bool isAllowMutiPanel = false)
        {
            if(!isAllowMutiPanel)
            {
                var findItem = FindTabItemOnName(title);
                if (null != findItem)
                {
                    tab_Main.SelectedItem = findItem;
                    return;
                }
            }

            if (tab_Main.Items.Count > MAX_CHILD_TAB_COUNT)
            {
                MessageBox.Show("已经达到最大标签数，请关闭一些无用标签再点击");
                return;
            }

            var item = new TabItemEx();
            item.Header = title;
            //item.ToolTip = title;
            item.Height = 28;
            item.Margin = new Thickness(3, 0, 0, 0);
            item.Content = addControl;

            if (tab_Main.Items.Count > 1)
            {
                if(!FixedTabHeaderWidth())
                {
                    // FIXME:动态调整tabItem的Header大小未实现好，待修复，目前不能添加过长的tab
                    return;
                }
            }

            tab_Main.Items.Add(item);
            // 设置最新点击的Item
            tab_Main.SelectedItem = item;
        }
    }
}
