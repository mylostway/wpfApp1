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

namespace WpfApp1.Panels.Business.BusinessOp.BusinessOpCenterPanel
{
    /// <summary>
    /// AssuranceInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AssuranceInfoPanel : UserControl
    {
        public AssuranceInfoPanel()
        {
            InitializeComponent();
        }

        public FreBusinessAssuranceInfoEntity EditInfo { get; set; }

        public void Init(FreBusinessAssuranceInfoEntity editInfo)
        {
            EditInfo = editInfo;

            this.DataContext = EditInfo;
        }
    }
}
