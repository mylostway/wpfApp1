﻿using System;
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
using WpfApp1.Data.Test;

using WL_OA.Data.entity;

namespace WpfApp1.Panels.business
{
    /// <summary>
    /// 用于绑定DataGrid的数据源结构体
    /// </summary>
    public class DriverManageStruct : CheckableViewMode
    {
        string driverNo;
        string driverName;
        string phone1;
        string certNo;
        string driverCard;
        bool isInPosition;

        public string DriverNo
        {
            get { return driverNo; }
            set
            {
                if (driverNo == value) return;
                driverNo = value;
                OnPropertyChanged();
            }
        }

        public string DriverName
        {
            get { return driverName; }
            set
            {
                if (driverName == value) return;
                driverName = value;
                OnPropertyChanged();
            }
        }

        public string Phone1
        {
            get { return phone1; }
            set
            {
                if (phone1 == value) return;
                phone1 = value;
                OnPropertyChanged();
            }
        }

        public string CertNo
        {
            get { return certNo; }
            set
            {
                if (certNo == value) return;
                certNo = value;
                OnPropertyChanged();
            }
        }

        public string DriverCard
        {
            get { return driverCard; }
            set
            {
                if (driverCard == value) return;
                driverCard = value;
                OnPropertyChanged();
            }
        }

        public string StrIsInPosition
        {
            get { return DataConvetor.ConvertBoolToStrSeen(isInPosition); }
            set
            {
                var val = DataConvetor.ConvertStrBoolToVal(value);
                if (isInPosition == val) return;
                isInPosition = val;
                OnPropertyChanged();
            }
        }

    }

    public class DriverinfoEntityViewMode : DriverinfoEntity, INotifyPropertyChanged, IIsCheckableView
    {
        public DriverinfoEntityViewMode() { }

        public DriverinfoEntityViewMode(DriverinfoEntity rhs)
            :base(rhs)
        { }

        public int VFid
        {
            get { return Fid; }
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
        /// viewMode子属性 - 司机备用手机号码'
        /// </summary>
        public string VFphone3
        {
            get { return fphone3; }
            set
            {
                if (fphone3 == value) return;
                fphone3 = value;
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
        public string VFDriverNo
        {
            get { return fDriverNo; }
            set
            {
                if (fDriverNo == value) return;
                fDriverNo = value;
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
