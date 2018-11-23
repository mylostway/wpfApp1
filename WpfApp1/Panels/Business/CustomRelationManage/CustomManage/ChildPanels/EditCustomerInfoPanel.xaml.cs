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
using WpfApp1.Data.NDAL;

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

            m_dicTabContentPanels.Add("概要信息", new EditSumaryInfoPanel());
            m_dicTabContentPanels.Add("资信信息", new EditAssertInfoPanel());
            m_dicTabContentPanels.Add("配置信息", new EditCfgInfoPanel());
            m_dicTabContentPanels.Add("其他信息", new EditOtherInfoPanel());
            m_dicTabContentPanels.Add("录入信息", new EditInputInfoPanel());

            tab_editCustomerInfo.Init(m_dicTabContentPanels);
        }


        Dictionary<string, UIElement> m_dicTabContentPanels = new Dictionary<string, UIElement>();

        public CustomerInfoDTO EditInfo { get; private set; } = new CustomerInfoDTO();

        public void Init(CustomerInfoDTO editInfo)
        {
            EditInfo = editInfo;

            (m_dicTabContentPanels["概要信息"] as EditSumaryInfoPanel).Init(EditInfo.CustomerInfo);
            (m_dicTabContentPanels["资信信息"] as EditAssertInfoPanel).Init(EditInfo.CreditInfo);
            (m_dicTabContentPanels["配置信息"] as EditCfgInfoPanel).Init(EditInfo.ConfigInfo);
            (m_dicTabContentPanels["其他信息"] as EditOtherInfoPanel).Init(EditInfo.OtherInfo);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            EditInfo.CustomerInfo = (m_dicTabContentPanels["概要信息"] as EditSumaryInfoPanel).GetEditInfo();
            EditInfo.CreditInfo = (m_dicTabContentPanels["资信信息"] as EditAssertInfoPanel).GetEditInfo();
            EditInfo.ConfigInfo = (m_dicTabContentPanels["配置信息"] as EditCfgInfoPanel).GetEditInfo();
            EditInfo.OtherInfo = (m_dicTabContentPanels["其他信息"] as EditOtherInfoPanel).GetEditInfo();

            // 这个应该用不到，在服务端赋值即可 
            //var inputInfo = (m_dicTabContentPanels["概要信息"] as EditInputInfoPanel).GetEditInfo();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
