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

namespace WpfApp1.Panels.Business.BusinessOp.BusinessOpCenterPanel
{
    /// <summary>
    /// EditBusinessListPanel.xaml 的交互逻辑
    /// </summary>
    public partial class EditBusinessListPanel : UserControl
    {
        public EditBusinessListPanel()
        {
            InitializeComponent();
        }

        Dictionary<string, UIElement> m_dicTabContentPanels = new Dictionary<string, UIElement>();

        public BaseEntity<int> EditInfo { get; set; }

        public void Init()
        {
            m_dicTabContentPanels.Add("货柜信息", new CounterInfoPanel());
            m_dicTabContentPanels.Add("海运信息", new SeaTransportInfoPanel());
            m_dicTabContentPanels.Add("保险信息", new AssuranceInfoPanel());
            m_dicTabContentPanels.Add("事项说明", new MatterInfoPanel());
            m_dicTabContentPanels.Add("操作信息", new OperationInfoPanel());

            this.tab_bocp.Init(m_dicTabContentPanels);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
