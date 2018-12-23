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
using WpfApp1.Data;


using WL_OA.Data.entity;

namespace WpfApp1.Panels.business
{
    public class DriverinfoEntityViewMode : DriverinfoEntity, INotifyPropertyChanged, IIsCheckableView
    {
        public DriverinfoEntityViewMode() { }

        public DriverinfoEntityViewMode(DriverinfoEntity rhs) : base(rhs) { }

        /// <summary>
        /// viewMode子属性 - 司机编号'
        /// </summary>
        public string VFdriverNo
        {
            get { return fdriverNo; }
            set
            {
                if (fdriverNo == value) return;
                fdriverNo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 司机姓名'
        /// </summary>
        public string VFname
        {
            get { return fname; }
            set
            {
                if (fname == value) return;
                fname = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 司机手机号码'
        /// </summary>
        public string VFphone1
        {
            get { return fphone1; }
            set
            {
                if (fphone1 == value) return;
                fphone1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 司机备用手机号码'
        /// </summary>
        public string VFphone2
        {
            get { return fphone2; }
            set
            {
                if (fphone2 == value) return;
                fphone2 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 司机证件号码'
        /// </summary>
        public string VFcertID
        {
            get { return fcertID; }
            set
            {
                if (fcertID == value) return;
                fcertID = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 驾驶证编号'
        /// </summary>
        public string VFdriverCardNo
        {
            get { return fdriverCardNo; }
            set
            {
                if (fdriverCardNo == value) return;
                fdriverCardNo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 车牌号'
        /// </summary>
        public string VFcarNo
        {
            get { return fcarNo; }
            set
            {
                if (fcarNo == value) return;
                fcarNo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 挂车号'
        /// </summary>
        public string VFtrailerNo
        {
            get { return ftrailerNo; }
            set
            {
                if (ftrailerNo == value) return;
                ftrailerNo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 生日'
        /// </summary>
        public DateTime VFbirthday
        {
            get { return fbirthday; }
            set
            {
                if (fbirthday == value) return;
                fbirthday = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 籍贯'
        /// </summary>
        public string VFbirthPlace
        {
            get { return fbirthPlace; }
            set
            {
                if (fbirthPlace == value) return;
                fbirthPlace = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 家庭住址'
        /// </summary>
        public string VFlivePlace
        {
            get { return flivePlace; }
            set
            {
                if (flivePlace == value) return;
                flivePlace = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 保底工资(分)'
        /// </summary>
        public int VFlowestSalary
        {
            get { return flowestSalary; }
            set
            {
                if (flowestSalary == value) return;
                flowestSalary = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 备注'
        /// </summary>
        public string VFmemo
        {
            get { return fmemo; }
            set
            {
                if (fmemo == value) return;
                fmemo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 是否在职 0 - 否 1 - 是，其他待添加，默认0
        /// </summary>
        public int VFworkState
        {
            get { return fworkState; }
            set
            {
                if (fworkState == value) return;
                fworkState = value;
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
