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
using WpfApp1.Data.Test;

namespace WpfApp1.Panels.business
{
    public class AirwayEntityViewMode : AirwayEntity, INotifyPropertyChanged, IIsCheckableView
    {
        public AirwayEntityViewMode() { }

        public AirwayEntityViewMode(AirwayEntity rhs) : base(rhs) { }

        public int VFid
        {
            get { return Fid; }
        }

        /// <summary>
        /// viewMode子属性 - 航线名称名（中文）'
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
        /// viewMode子属性 - 航线名称名（英文）'
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
        /// viewMode子属性 - 备注'
        /// </summary>
        public string VFremark
        {
            get { return fremark; }
            set
            {
                if (fremark == value) return;
                fremark = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 标志-是否可用'
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

        /// <summary>
        /// viewMode子属性 - 最后修改时间
        /// </summary>
        public DateTime VFlastModifyTime
        {
            get { return flastModifyTime; }
            set
            {
                if (flastModifyTime == value) return;
                flastModifyTime = value;
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
