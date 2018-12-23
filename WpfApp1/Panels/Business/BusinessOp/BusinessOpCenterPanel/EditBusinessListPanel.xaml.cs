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
using WL_OA.Data.dto;
using WL_OA.Data.entity;
using WpfApp1.Data;


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

            m_dicTabContentPanels.Add("货柜信息", new CounterInfoPanel());
            m_dicTabContentPanels.Add("海运信息", new SeaTransportInfoPanel());
            m_dicTabContentPanels.Add("保险信息", new AssuranceInfoPanel());
            m_dicTabContentPanels.Add("事项说明", new MatterInfoPanel());
            m_dicTabContentPanels.Add("操作信息", new OperationInfoPanel());

            this.tab_bocp.Init(m_dicTabContentPanels);

            HideContent();

            if (AppRunConfigs.Instance.IsSingleTestMode)
            {
                var testFakeData = FakeDataHelper.Instance.GenData<FreBussinessOpCenterDTO>();
                //testFakeData.CustomerInfo.FpayWay = FakeDataHelper.Instance.GenRandomInt((int)PaywayEnums.Advance);
                //testFakeData.CustomerInfo.FdefaultType = FakeDataHelper.Instance.GenRandomInt((int)QueryCustomerInfoTypeEnums.WharfProxy);
                Init(testFakeData);
            }

            if (null == EditInfo) EditInfo = new FreBussinessOpCenterDTO();
        }

        Dictionary<string, UIElement> m_dicTabContentPanels = new Dictionary<string, UIElement>();

        public FreBussinessOpCenterDTO EditInfo { get; set; }

        public void Init(FreBussinessOpCenterDTO dto)
        {
            if (null == dto) return;

            this.EditInfo = dto;

            (m_dicTabContentPanels["货柜信息"] as CounterInfoPanel).Init(EditInfo.ContainsInfoList);
            (m_dicTabContentPanels["海运信息"] as SeaTransportInfoPanel).Init(EditInfo.SeaTransportInfo);
            (m_dicTabContentPanels["保险信息"] as AssuranceInfoPanel).Init(EditInfo.AssuranceInfo);
            (m_dicTabContentPanels["事项说明"] as MatterInfoPanel).Init(EditInfo.MatterInfo);
            (m_dicTabContentPanels["操作信息"] as OperationInfoPanel).Init(EditInfo.OpInfo);

            this.orderInfoPanel.Init(this.EditInfo.OrderInfo);
            this.holdingGoodsInfoPanel.Init(this.EditInfo.HoldGoodsInfo);
            this.layingGoodsInfoPanel.Init(this.EditInfo.LayGoodsInfo);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 显示tab元素
        /// </summary>
        public void ShowContent()
        {
            this.rootLayout.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 隐藏tab内容，有助于加快tab显示（特别是动画）
        /// </summary>
        public void HideContent()
        {
            this.rootLayout.Visibility = Visibility.Hidden;
        }
    }
}
