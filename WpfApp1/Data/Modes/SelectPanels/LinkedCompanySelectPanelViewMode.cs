using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Panels.extend_control;

namespace WpfApp1.Data.Modes.SelectPanels
{
    [DynamicDataGridSearchCol("companyFullName:全称", "companyFullName")]
    public class LinkedCompanySelectPanelViewMode : IDynamicSelectedDataGirdBaseViewMode
    {
        public string companyFullName { get; set; }

        [FakeDataNotGen]
        public string SelectText { get; set; } = "选择";
    }
}
