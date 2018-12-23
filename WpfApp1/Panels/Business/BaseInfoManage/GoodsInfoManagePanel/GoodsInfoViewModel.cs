using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using BaseLib.Data;

using WL_OA.Data.entity;

using WpfApp1.Data;


namespace WpfApp1.Panels.business
{
    /// <summary>
    /// 用于绑定DataGrid的数据源结构体
    /// </summary>
    public class GoodsInfoStruct : CheckableViewMode
    {
        private string chnName;
        private string engName;
        private string mark;
        private bool isNeedToCheckWeight;
        private bool isUsable;

        public GoodsInfoStruct() { }

        public string ChnName
        {
            get { return chnName; }
            set
            {
                if (chnName == value) return;
                chnName = value;
                OnPropertyChanged();
            }
        }

        public string EngName
        {
            get { return engName; }
            set
            {
                if (engName == value) return;
                engName = value;
                OnPropertyChanged();
            }
        }

        public string Mark
        {
            get { return mark; }
            set
            {
                if (mark == value) return;
                mark = value;
                OnPropertyChanged();
            }
        }

        public string StrIsNeedToCheckWeight
        {
            get { return DataConvetor.ConvertBoolToStrSeen(isNeedToCheckWeight); }
            set
            {
                var val = DataConvetor.ConvertStrBoolToVal(value);
                if (isNeedToCheckWeight == val) return;
                isNeedToCheckWeight = val;
                OnPropertyChanged();
            }
        }

        public string StrIsUsable
        {
            get { return DataConvetor.ConvertBoolToStrSeen(isUsable); }
            set
            {
                var val = DataConvetor.ConvertStrBoolToVal(value);
                if (isUsable == val) return;
                isUsable = val;
                OnPropertyChanged();
            }
        }
    }

    public class GoodsinfoEntityViewMode : GoodsinfoEntity, INotifyPropertyChanged, IIsCheckableView
    {
        public GoodsinfoEntityViewMode() { }

        public GoodsinfoEntityViewMode(GoodsinfoEntity rhs) : base(rhs) { }

        public int VFid
        {
            get { return Fid; }
        }

        /// <summary>
        /// viewMode子属性 - 货物名称名（中文）'
        /// </summary>
        public string VFchn_Name
        {
            get { return fchn_Name; }
            set
            {
                if (fchn_Name == value) return;
                fchn_Name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 货物名称名（英文）'
        /// </summary>
        public string VFeng_Name
        {
            get { return feng_Name; }
            set
            {
                if (feng_Name == value) return;
                feng_Name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 助记码'
        /// </summary>
        public string VFmark
        {
            get { return fmark; }
            set
            {
                if (fmark == value) return;
                fmark = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 标志-需核实重量'
        /// </summary>
        public int VFisCheckWeight
        {
            get { return fisCheckWeight; }
            set
            {
                if (fisCheckWeight == value) return;
                fisCheckWeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 标志-可用
        /// </summary>
        public int VFusable
        {
            get { return fusable; }
            set
            {
                if (fusable == value) return;
                fusable = value;
                OnPropertyChanged();
            }
        }


        bool isSelected;

        public string IsSelected
        {
            get { return DataConvetor.ConvertBoolToStrSeen(isSelected); }
            set
            {
                var val = DataConvetor.ConvertStrBoolToVal(value);
                if (isSelected == val) return;
                isSelected = val;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
