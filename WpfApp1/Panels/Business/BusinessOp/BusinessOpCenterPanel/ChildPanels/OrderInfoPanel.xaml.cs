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

namespace WpfApp1.Panels.Business.BusinessOp.BusinessOpCenterPanel
{
    /// <summary>
    /// OrderInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class OrderInfoPanel : UserControl
    {
        public OrderInfoPanel()
        {
            InitializeComponent();

            //acb_startWharf.ItemsSource = EnumHelper.GetEnumNamesOnType<>();

            this.cbx_opTerm.BindComboxToEnums<FreBusinessTransportTermsEnums>();
            this.cbx_transportTerm.BindComboxToEnums<FreBusinessTransportTermsEnums>();
            this.cbx_payWay.BindComboxToEnums<FreBusinessPaymentTypeEnums>();
            this.cbx_detainRelease.BindComboxToEnums<FreBusinessTypeEnums>();

            this.DataContext = EditInfo;
        }

        public FreBusinessOrderInfoEntity EditInfo { get; set; } = new FreBusinessOrderInfoEntity();

        public void Init(FreBusinessOrderInfoEntity editInfo)
        {
            EditInfo = editInfo;

            this.DataContext = EditInfo;
        }
    }
}
