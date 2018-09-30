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
using WpfApp1.Data.Test;

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

    /*
    internal class FakeDataForDriver : FakeDataHeler<DriverManageStruct>
    {
        public FakeDataForDriver() { FakeDataCount = 99; }

        protected override DriverManageStruct GenData()
        {
            return new DriverManageStruct()
            {

            };
        }
    }
    */
}
