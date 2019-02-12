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
    [DynamicDataGridSearchCol("Fname:司机姓名|FdriverNo:司机编号|Fphone1:司机电话", "FdriverNo")]
    public class DriverInfoSelectPanelViewMode : DriverinfoEntity, IDynamicSelectedDataGirdBaseViewMode
    {
        public DriverInfoSelectPanelViewMode() { }

        [FakeDataNotGen]
        public string SelectText { get; set; } = "选择";
    }
}
