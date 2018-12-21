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
using WL_OA.Data;
using WL_OA.Data.entity;
using WL_OA.Data.dto;
using WpfApp1.Data.Test;
using WpfApp1.Data.Helpers;
using WpfApp1.Data.Modes;
using WpfApp1.Data;

namespace WpfApp1.Panels.Business.CustomRelationManage
{
    /// <summary>
    /// SumaryEditInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class EditSumaryInfoPanel : UserControl
    {
        public EditSumaryInfoPanel()
        {
            InitializeComponent();

            m_dicTabContentPanels.Add("联系人", new SumaryEditConcatPeoplePanel());
            m_dicTabContentPanels.Add("装卸地址", new SumaryEditHoldAddrPanel());
            m_dicTabContentPanels.Add("银行账号", new SumaryEditBankAccountPanel());
            m_dicTabContentPanels.Add("订舱收货人", new SumaryEditBookSpaceReceiverPanel());

            tab_sumaryChildInfo.Init(m_dicTabContentPanels);

            this.cbx_defaultType.BindComboxToEnums<QueryCustomerInfoTypeEnums>();
            this.cbx_payWay.BindComboxToEnums<PaywayEnums>();
            this.mcbx_companyType.BindMulComboxToEnums<FreBusinessCompanyType>();

            this.cbx_payWay.SelectedValue = PaywayEnums.None;

            if (AppRunConfigs.Instance.IsSingleTestMode)
            {
                this.scb_mainGoods.SearchDataContext = FakeDataHelper.Instance.CreateFakeDataCollection<GoodsinfoSelectPanelViewMode>().Distinct(new FastPropertyComparer<GoodsinfoSelectPanelViewMode>("Fmark"));
                this.scb_businessMan.SearchDataContext = FakeDataHelper.Instance.CreateFakeDataCollection<SystemUserSelectPanelViewMode>().Distinct(new FastPropertyComparer<SystemUserSelectPanelViewMode>("Fname"));
            }

            if (null == EditInfo) EditInfo = new CustomerSummaryInfoDTO();
            this.DataContext = EditInfo;
        }

        private CustomerSummaryInfoDTO EditInfo { get; set; } = null;//new CustomerSummaryInfoDTO();

        Dictionary<string, UIElement> m_dicTabContentPanels = new Dictionary<string, UIElement>();

        public void Init(CustomerSummaryInfoDTO editInfo)
        {
            if (null == editInfo) return;
            EditInfo = editInfo;
            this.DataContext = EditInfo;

            SetConcatPeopleInfo(EditInfo.ContactInfoList);
            SetHoldAddrInfo(EditInfo.HoldAddrInfoList);
            SetBankAccountInfo(EditInfo.BankAccountInfoList);
            SetBookSpaceReceiverInfo(EditInfo.BookSpaceReceiverInfoList);
        }


        public CustomerSummaryInfoDTO GetEditInfo()
        {
            EditInfo.ContactInfoList = (m_dicTabContentPanels["联系人"] as SumaryEditConcatPeoplePanel).EditingConcatPeopleList;
            EditInfo.HoldAddrInfoList = (m_dicTabContentPanels["装卸地址"] as SumaryEditHoldAddrPanel).EditingHoldAddrList;
            EditInfo.BankAccountInfoList = (m_dicTabContentPanels["银行账号"] as SumaryEditBankAccountPanel).EditBankAccountList;
            EditInfo.BookSpaceReceiverInfoList = (m_dicTabContentPanels["订舱收货人"] as SumaryEditBookSpaceReceiverPanel).EditBookSpaceReceiverList;

            return EditInfo;
        }

        public void SetConcatPeopleInfo(IList<CustomerContactEntity> editingConcatPeopleList)
        {
            (m_dicTabContentPanels["联系人"] as SumaryEditConcatPeoplePanel).Init(editingConcatPeopleList);
        }

        public void SetHoldAddrInfo(IList<CustomerHoldAddrEntity> editingConcatPeopleList)
        {
            (m_dicTabContentPanels["装卸地址"] as SumaryEditHoldAddrPanel).Init(editingConcatPeopleList);
        }

        public void SetBankAccountInfo(IList<CustomerBankAccountEntity> editingConcatPeopleList)
        {
            (m_dicTabContentPanels["银行账号"] as SumaryEditBankAccountPanel).Init(editingConcatPeopleList);
        }

        public void SetBookSpaceReceiverInfo(IList<CustomerBookSpaceReceiverEntity> editingConcatPeopleList)
        {
            (m_dicTabContentPanels["订舱收货人"] as SumaryEditBookSpaceReceiverPanel).Init(editingConcatPeopleList);
        }



    }
}
