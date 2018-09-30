using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using BaseLib.Data;
using WpfApp1.Data;

namespace WpfApp1.Panels
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
    /// ViewMode基类
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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


    /// <summary>
    /// 用于分页的ViewMode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaggingViewMode<T> : ViewModel
        where T : new()
    {
        public PaggingViewMode(IEnumerable<T> sourceData)
        {
            _currentPage = 1;
            // FIXME： pagesize绑定未实现。
            _pageSize = 10;

            _firstPageCommand = new DelegateCommand(FirstPageAction);
            _previousPageCommand = new DelegateCommand(PreviousPageAction);
            _nextPageCommand = new DelegateCommand(NextPageAction);
            _lastPageCommand = new DelegateCommand(LastPageAction);

            _source = sourceData;
            _totalPage = DataHelper.Ceil((_source.Count() / (double)_pageSize));        
            _fakeSoruce.Clear();
            _fakeSoruce.AddRange(_source.Take(_pageSize).ToList());
        }

        #region ICommand
        private ICommand _firstPageCommand;

        public ICommand FirstPageCommand
        {
            get
            {
                return _firstPageCommand;
            }

            set
            {
                _firstPageCommand = value;
            }
        }

        private ICommand _previousPageCommand;

        public ICommand PreviousPageCommand
        {
            get
            {
                return _previousPageCommand;
            }

            set
            {
                _previousPageCommand = value;
            }
        }

        private ICommand _nextPageCommand;

        public ICommand NextPageCommand
        {
            get
            {
                return _nextPageCommand;
            }

            set
            {
                _nextPageCommand = value;
            }
        }

        private ICommand _lastPageCommand;

        public ICommand LastPageCommand
        {
            get
            {
                return _lastPageCommand;
            }

            set
            {
                _lastPageCommand = value;
            }
        }
        #endregion

        private int _pageSize;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    OnPropertyChanged("PageSize");
                }
            }
        }

        private int _currentPage;

        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }

            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged("CurrentPage");
                }
            }
        }

        private int _totalPage;

        public int TotalPage
        {
            get
            {
                return _totalPage;
            }

            set
            {
                if (_totalPage != value)
                {
                    _totalPage = value;
                    OnPropertyChanged("TotalPage");
                }
            }
        }

        /// <summary>
        /// UI显示的数据源
        /// </summary>
        private ObservableCollection<T> _fakeSoruce = new ObservableCollection<T>();

        public ObservableCollection<T> FakeSource
        {
            get
            {
                return _fakeSoruce;
            }
            set
            {
                if (_fakeSoruce != value)
                {
                    _fakeSoruce = value;
                    OnPropertyChanged("FakeSource");
                    //if (null != PageDataSourceChangeHandler) PageDataSourceChangeHandler(_fakeSoruce, null);
                }
            }
        }

        //public event EventHandler PageDataSourceChangeHandler = null;

        /// <summary>
        /// 总的数据源
        /// </summary>
        private IEnumerable<T> _source;

        private void SetViewModeSource(IEnumerable<T> source)
        {
            _fakeSoruce.Clear();
            _fakeSoruce.AddRange(source);
        }

        private void FirstPageAction()
        {
            CurrentPage = 1;
            SetViewModeSource(_source.Take(_pageSize));
        }

        private void PreviousPageAction()
        {
            if (CurrentPage == 1) return;

            if (CurrentPage == 2)
            {
                FirstPageAction();
            }
            else
            {
                SetViewModeSource(_source.Skip((CurrentPage - 2) * _pageSize).Take(_pageSize).ToList());
                CurrentPage--;
            }
        }

        private void NextPageAction()
        {
            if (CurrentPage == _totalPage) return;           

            SetViewModeSource(_source.Skip(CurrentPage * _pageSize).Take(_pageSize));

            CurrentPage++;
        }

        private void LastPageAction()
        {
            CurrentPage = TotalPage;
            int skipCount = (_totalPage - 1) * _pageSize;
            int takeCount = _source.Count() - skipCount;
            SetViewModeSource(_source.Skip(skipCount).Take(takeCount).ToList());
        }
    }
}
