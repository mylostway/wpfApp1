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
using WpfApp1.Data;

namespace WpfApp1.Panels.Business.CustomRelationManage
{
    /// <summary>
    /// EditCfgInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class EditCfgInfoPanel : UserControl
    {
        public EditCfgInfoPanel()
        {
            InitializeComponent();
            
            this.stb_belongsShipCompany.SearchDataContext = ShipCompanySelectPanelData.Instance.DataList;

            this.DataContext = EditConfigInfo;
        }

        public CustomerConfigInfoEntity EditConfigInfo { get; set; } = new CustomerConfigInfoEntity();

        public CustomerConfigInfoEntity GetEditInfo()
        {
            return EditConfigInfo;
        }

        public void Init(CustomerConfigInfoEntity editInfo)
        {
            if (null == editInfo) this.EditConfigInfo = new CustomerConfigInfoEntity();
            else this.EditConfigInfo = editInfo;
            this.DataContext = EditConfigInfo;
        }
    }
}
