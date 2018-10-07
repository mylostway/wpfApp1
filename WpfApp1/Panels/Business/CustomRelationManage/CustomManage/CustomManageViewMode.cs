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
    public class CustomManageViewMode: CheckableViewMode
    {
        string cardOrder;
        string cardNo;
        string cardCompany;
        string trailerNo;
        string driver;
        bool isUsable;

        public string CardOrder
        {
            get { return cardOrder; }
            set
            {
                if (cardOrder == value) return;
                cardOrder = value;
                OnPropertyChanged();
            }
        }

        public string CardNo
        {
            get { return cardNo; }
            set
            {
                if (cardNo == value) return;
                cardNo = value;
                OnPropertyChanged();
            }
        }

        public string CardCompany
        {
            get { return cardCompany; }
            set
            {
                if (cardCompany == value) return;
                cardCompany = value;
                OnPropertyChanged();
            }
        }

        public string TrailerNo
        {
            get { return trailerNo; }
            set
            {
                if (trailerNo == value) return;
                trailerNo = value;
                OnPropertyChanged();
            }
        }

        public string Driver
        {
            get { return driver; }
            set
            {
                if (driver == value) return;
                driver = value;
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
}
