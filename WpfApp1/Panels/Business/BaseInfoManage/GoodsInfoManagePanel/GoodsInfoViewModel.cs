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


    /*
    internal class FakeDataHeler
    {
        public static ObservableCollection<GoodsInfoStruct> CreateFakeData()
        {
            return new ObservableCollection<GoodsInfoStruct>
            {
                new GoodsInfoStruct
                {
                    Code = 'M',
                    Name = "Material Design",
                    Description = "Material Design in XAML Toolkit",
                    IsSelected = true
                },
                new GoodsInfoStruct
                {
                    Code = 'D',
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control",
                    Food = "Fries"
                },
                new GoodsInfoStruct
                {
                    Code = 'P',
                    Name = "Predator",
                    Description = "If it bleeds, we can kill it"
                },

                new GoodsInfoStruct
                {
                    Code = 'M',
                    Name = "Material Design",
                    Description = "Material Design in XAML Toolkit",
                    IsSelected = true
                },
                new GoodsInfoStruct
                {
                    Code = 'D',
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control",
                    Food = "Fries"
                },
                new GoodsInfoStruct
                {
                    Code = 'P',
                    Name = "Predator",
                    Description = "If it bleeds, we can kill it"
                },

                new GoodsInfoStruct
                {
                    Code = 'M',
                    Name = "Material Design",
                    Description = "Material Design in XAML Toolkit",
                    IsSelected = true
                },
                new GoodsInfoStruct
                {
                    Code = 'T',
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control",
                    Food = "Fries"
                },
                new GoodsInfoStruct
                {
                    Code = 'Y',
                    Name = "Predator",
                    Description = "If it bleeds, we can kill it"
                },

                new GoodsInfoStruct
                {
                    Code = 'U',
                    Name = "Material Design",
                    Description = "Material Design in XAML Toolkit",
                    IsSelected = true
                },
                new GoodsInfoStruct
                {
                    Code = 'T',
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control",
                    Food = "Fries"
                },
                new GoodsInfoStruct
                {
                    Code = 'Y',
                    Name = "Predator",
                    Description = "If it bleeds, we can kill it"
                },
                new GoodsInfoStruct
                {
                    Code = 'T',
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control",
                    Food = "Fries"
                },
                new GoodsInfoStruct
                {
                    Code = 'Y',
                    Name = "Predator",
                    Description = "If it bleeds, we can kill it"
                },
                new GoodsInfoStruct
                {
                    Code = 'T',
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control",
                    Food = "Fries"
                },
                new GoodsInfoStruct
                {
                    Code = 'Y',
                    Name = "Predator",
                    Description = "If it bleeds, we can kill it"
                },
                new GoodsInfoStruct
                {
                    Code = 'T',
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control",
                    Food = "Fries"
                },
                new GoodsInfoStruct
                {
                    Code = 'Y',
                    Name = "Predator",
                    Description = "If it bleeds, we can kill it"
                },
            };
        }
    }
    */
}
