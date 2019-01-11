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

namespace WpfApp1.Panels.Business.SystemManage
{
    /// <summary>
    /// OnlineAduitPanel.xaml 的交互逻辑
    /// </summary>
    public partial class OnlineAduitPanel : UserControl
    {
        public OnlineAduitPanel()
        {
            InitializeComponent();

            this.rootLayout.Children.Remove(dg_toAduit);
            this.rootLayout.Children.Remove(dg_pass);
            this.rootLayout.Children.Remove(dg_reject);
            this.rootLayout.Children.Remove(dg_useless);

            m_dicTabContentPanels.Add("待审批", this.dg_toAduit);
            m_dicTabContentPanels.Add("已通过", this.dg_pass);
            m_dicTabContentPanels.Add("已拒绝", this.dg_reject);
            m_dicTabContentPanels.Add("已失效", this.dg_useless);
            this.tab_onlineAduit.Init(m_dicTabContentPanels);
        }


        Dictionary<string, UIElement> m_dicTabContentPanels = new Dictionary<string, UIElement>();



    }
}
