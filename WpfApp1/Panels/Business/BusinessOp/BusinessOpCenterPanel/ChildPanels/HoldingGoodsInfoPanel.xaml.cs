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
using WpfApp1.Data;

namespace WpfApp1.Panels.Business.BusinessOp.BusinessOpCenterPanel
{
    /// <summary>
    /// HoldingGoodsInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class HoldingGoodsInfoPanel : UserControl
    {
        public HoldingGoodsInfoPanel()
        {
            InitializeComponent();

            this.cbx_LoadingLevel.BindComboxToEnums<FreBusinessLoadingLevelEnums>();
            this.cbx_ReserveCar.BindComboxToEnums<FreBusinessReserveCarEnums>();

            this.stb_goodsName.Init(ServerDatas.ServerGoodsinfoList,"货名列表");

            this.DataContext = EditInfo;
        }

        public FreBusinessHoldGoodsInfoEntity EditInfo { get; set; } = new FreBusinessHoldGoodsInfoEntity();

        public void Init(FreBusinessHoldGoodsInfoEntity editInfo)
        {
            EditInfo = editInfo;

            this.DataContext = EditInfo;
        }
    }
}
