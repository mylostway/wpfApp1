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
    public class WharfinfoEntityViewMode : WharfinfoEntity, INotifyPropertyChanged, IIsCheckableView
    {
        public WharfinfoEntityViewMode() { }

        public WharfinfoEntityViewMode(WharfinfoEntity rhs) : base(rhs) { }

        /// <summary>
        /// viewMode子属性 - 位置，这是一个不定层级列表，目前使用;间隔'
        /// </summary>
        public string VFPosition
        {
            get { return fPosition; }
            set
            {
                if (fPosition == value) return;
                fPosition = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 别名'
        /// </summary>
        public string VFalias
        {
            get { return falias; }
            set
            {
                if (falias == value) return;
                falias = value;
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
        /// viewMode子属性 - 操作，目前没值'
        /// </summary>
        public int VFop
        {
            get { return fop; }
            set
            {
                if (fop == value) return;
                fop = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 录入人'
        /// </summary>
        public string VFinputUser
        {
            get { return finputUser; }
            set
            {
                if (finputUser == value) return;
                finputUser = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 录入时间'
        /// </summary>
        public DateTime VFinputTime
        {
            get { return finputTime; }
            set
            {
                if (finputTime == value) return;
                finputTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 最后一次修改人'
        /// </summary>
        public string VFlastModifyUser
        {
            get { return flastModifyUser; }
            set
            {
                if (flastModifyUser == value) return;
                flastModifyUser = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 最后一次修改时间
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
