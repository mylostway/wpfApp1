using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfApp1.Data
{
    /// <summary>
    /// ViewMode的扩展方法
    /// </summary>
    public static class Extension
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            items.ToList().ForEach(collection.Add);
        }
    }

    /// <summary>
    /// 是否有checkbox框
    /// </summary>
    public interface IIsCheckableView
    {
        string IsSelected { get; set; }
    }

    /// <summary>
    /// ViewMode基类
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    /// <summary>
    /// 带选择框的viewMode
    /// </summary>
    public class CheckableViewMode: ViewModel
    {
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
    }


    
}
