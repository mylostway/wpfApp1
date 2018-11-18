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

            var dic = new Dictionary<string, UIElement>();
            dic.Add("联系人", new SumaryEditConcatPeoplePanel());
            dic.Add("装卸地址", new SumaryEditHoldAddrPanel());
            dic.Add("银行账号", new SumaryEditBankAccountPanel());
            dic.Add("订舱收货人", new SumaryEditBookSpaceReceiverPanel());

            tab_sumaryChildInfo.Init(dic);
        }
    }
}
