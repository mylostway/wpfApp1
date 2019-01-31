using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL_OA.Data;
using WpfApp1.Panels.extend_control;

namespace WpfApp1.Data.Modes.SelectPanels
{
    [DynamicDataGridSearchCol("companyShortName:简称|companyFullName:全称", "companyFullName")]
    public class ShipCompanySelectPanelViewMode:IDynamicSelectedDataGirdBaseViewMode
    {
        public string companyShortName { get; set; }

        public string companyFullName { get; set; }

        [FakeDataNotGen]
        public string SelectText { get; set; } = "选择";
    }
}
