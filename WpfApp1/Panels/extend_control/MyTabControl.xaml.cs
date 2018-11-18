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
    /// MyTabControl.xaml 的交互逻辑
    /// </summary>
    public partial class MyTabControl : UserControl
    {
        public MyTabControl()
        {
            InitializeComponent();
        }

        public void Init(Dictionary<string, UIElement> dic)
        {
            tab_header.Children.Clear();

            HeaderPanelDic = dic;

            var idx = 0;
            foreach(var e in HeaderPanelDic)
            {
                if (idx == 0) m_curBorder = AddTabItem(e.Key, e.Value, true);                   
                else AddTabItem(e.Key, e.Value, false);
                idx++;
            }
        }

        private Border AddTabItem(string title,UIElement element,bool isSelected = false)
        {
            var border = new Border();
            if (isSelected) border.BorderThickness = BORDER_THICKNESS_SELECTED;
            else border.BorderThickness = BORDER_THICKNESS_UNSELECTED;
            border.MouseDown += Border_MouseDown;

            var textElem = new TextBlock();
            textElem.Text = title;

            border.Child = textElem;

            tab_header.Children.Add(border);

            if (isSelected) element.Visibility = Visibility.Visible;
            else element.Visibility = Visibility.Hidden;
            tab_content.Children.Add(element);

            return border;
        }


        /// <summary>
        /// 设置Tab被选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="isSelected"></param>
        private string SetTabSelected(Border sender,bool isSelected)
        {
            if (isSelected) sender.BorderThickness = BORDER_THICKNESS_SELECTED;
            else sender.BorderThickness = BORDER_THICKNESS_UNSELECTED;

            var selectedBlock = sender.Child as TextBlock;
            var selectedTitle = selectedBlock.Text;
            var panel = HeaderPanelDic[selectedTitle];

            if (isSelected) panel.Visibility = Visibility.Visible;
            else panel.Visibility = Visibility.Hidden;

            return selectedTitle;
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var lastBorder = m_curBorder;

            SetTabSelected(m_curBorder, false);

            m_curBorder = sender as Border;

            SetTabSelected(m_curBorder, true);

            TabChangeHandler?.Invoke(lastBorder, m_curBorder);
        }

        /// <summary>
        /// 边框宽度
        /// </summary>
        const double THICKNESS_WIDTH = 1;

        /// <summary>
        /// 选中的Border边框
        /// </summary>
        static readonly Thickness BORDER_THICKNESS_SELECTED = new Thickness(THICKNESS_WIDTH, THICKNESS_WIDTH, THICKNESS_WIDTH, 0);

        /// <summary>
        /// 未选中的Border的边框
        /// </summary>
        static readonly Thickness BORDER_THICKNESS_UNSELECTED = new Thickness(0, 0, 0, THICKNESS_WIDTH);

        /// <summary>
        /// 当前选中的Tab
        /// </summary>
        Border m_curBorder = null;

        /// <summary>
        /// 切换tab事件
        /// </summary>
        public EventHandler<Border> TabChangeHandler { get; set; } = null;

        /// <summary>
        /// 标题和TabPanel的映射
        /// </summary>
        public Dictionary<string,UIElement> HeaderPanelDic { get; set; }
    }
}
