using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading;
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
    public partial class EditBusinessListPanel : UserControl, IDialogPanel
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

            if (AppRunConfigs.Instance.IsSingleTestMode || AppRunConfigs.Instance.IsCreateDataForTest)
            {
                var testFakeData = ClientFakeDataHelper.Instance.GenData<FreBussinessOpCenterDTO>();
                //testFakeData.CustomerInfo.FpayWay = FakeDataHelper.Instance.GenRandomInt((int)PaywayEnums.Advance);
                //testFakeData.CustomerInfo.FdefaultType = FakeDataHelper.Instance.GenRandomInt((int)QueryCustomerInfoTypeEnums.WharfProxy);
                Init(testFakeData);
                return;
            }

            // 默认是添加记录
            if (null == EditInfo) Init(new FreBussinessOpCenterDTO());
        }

        /// <summary>
        /// 子panel
        /// </summary>
        Dictionary<string, UIElement> m_dicTabContentPanels = new Dictionary<string, UIElement>();

        /// <summary>
        /// 该Panel绑定的数据
        /// </summary>
        public FreBussinessOpCenterDTO EditInfo { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dto"></param>
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

            if(EditInfo.OrderInfo.Fid != 0)
            {
                // Fid != 0 代表是更新记录
                StartCalcCostTime();
            }            
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (EditInfo.CheckValid())
            {
                ClearPanel();
                DialogHost.CloseDialogCommand.Execute(true, this);
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            ClearPanel();
            DialogHost.CloseDialogCommand.Execute(false, this);
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

        public void SetPanelVisible(bool yes = true)
        {
            if (yes) this.rootLayout.Visibility = Visibility.Visible;
            else this.rootLayout.Visibility = Visibility.Hidden;
        }

        Timer m_timer = null;

        public void StartCalcCostTime()
        {
            m_timer = new Timer(new TimerCallback((x) =>
            {
                var startTime = EditInfo.OrderInfo.Fbusiness_date;

                var costStr = (DateTime.Now - startTime).ToCostStr();

                this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(()=> {
                    this.tbk_costTime.Text = costStr;
                }));
            }), null, 1000, 1000);
        }



        public void ClearPanel()
        {
            if(null != m_timer)
            {
                m_timer.Dispose();
            }
        }

        
    }
}
