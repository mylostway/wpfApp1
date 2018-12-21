using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL_OA.Data.entity;
using WpfApp1.Panels.extend_control;

namespace WpfApp1.Data.Modes
{
    [DynamicDataGridSearchCol("Fname:业务员姓名|Fuser_account:登录名", "Fname")]
    public class SystemUserSelectPanelViewMode : SystemUserEntity, IDynamicSelectedDataGirdBaseViewMode
    {
        [FakeDataNotGen]
        public string SelectText { get; set; } = "选择";
    }
}
