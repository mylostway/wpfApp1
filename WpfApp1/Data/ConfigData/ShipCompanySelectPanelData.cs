using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL_OA.Data;
using WL_OA.Data.utils.cfg;
using WpfApp1.Data.Modes.SelectPanels;

namespace WpfApp1.Data
{
    [Config("Configs/ConstDataJson/ShipCompanyInfo.json")]
    public class ShipCompanySelectPanelData
    {
        static ShipCompanySelectPanelData()
        {
            Instance = ConfigHelper.Get<ShipCompanySelectPanelData>();
        }

        public static ShipCompanySelectPanelData Instance { get; private set; }

        public List<ShipCompanySelectPanelViewMode> DataList { get; set; }
    }
}
