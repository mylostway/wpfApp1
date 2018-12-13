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
    /// SeaTransportInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class SeaTransportInfoPanel : UserControl
    {
        public SeaTransportInfoPanel()
        {
            InitializeComponent();

            this.cbx_BargeInformation.BindComboxToEnums<FreBusinessBargeInformationEnums>();

            this.DataContext = EditInfo;
        }

        public FreBusinessSeaTransportInfoEntity EditInfo { get; set; } = new FreBusinessSeaTransportInfoEntity();

        public void Init(FreBusinessSeaTransportInfoEntity editInfo)
        {
            if (null == editInfo) return;

            EditInfo = editInfo;

            this.DataContext = EditInfo;
        }
    }
}
