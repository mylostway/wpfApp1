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
    /// LayingGoodsInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class LayingGoodsInfoPanel : UserControl
    {
        public LayingGoodsInfoPanel()
        {
            InitializeComponent();

            this.cbx_DeliveryLevel.BindComboxToEnums<FreBusinessDeliveryLevelEnums>();
            this.cbx_DetainRelease.BindComboxToEnums<FreBusinessDetainReleaseEnums>();

            this.DataContext = EditInfo;
        }

        public FreBusinessLayGoodsInfoEntity EditInfo { get; set; } = new FreBusinessLayGoodsInfoEntity();


        public void Init(FreBusinessLayGoodsInfoEntity editInfo)
        {
            EditInfo = editInfo;

            this.DataContext = EditInfo;
        }
    }
}
