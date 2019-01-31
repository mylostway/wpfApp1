using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL_OA.Data;
using WL_OA.Data.entity;

using WpfApp1.Panels.extend_control;

namespace WpfApp1.Data.Modes
{
    [DynamicDataGridSearchCol("Fchn_Name:货物名(中文)|Fmark:助记码", "Fmark")]
    public class GoodsinfoSelectPanelViewMode : GoodsinfoEntity, IDynamicSelectedDataGirdBaseViewMode
    {
        [FakeDataNotGen]
        public string SelectText { get; set; } = "选择";
    }
}
