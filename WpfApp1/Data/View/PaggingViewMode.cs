﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using BaseLib.Data;
using WpfApp1.Panels;

namespace WpfApp1.Data
{
    /// <summary>
    /// 分页数回调
    /// </summary>
    /// <param name="pageIdx"></param>
    /// <param name="pageSize"></param>
    public delegate void PaggingChangeCallBack(int pageIdx, int pageSize);

    /// <summary>
    /// 用于分页的ViewMode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaggingViewMode<T> : ViewModel
        where T : new()
    {
        public PaggingViewMode(IEnumerable<T> sourceData, int totalCount = -1, PaggingChangeCallBack handler = null)
        {
            _currentPage = 1;
            // FIXME： pagesize绑定未实现。
            _pageSize = DEFAULT_PAGE_SIZE;

            FirstPageCommand = new DelegateCommand(FirstPageAction);
            PreviousPageCommand = new DelegateCommand(PreviousPageAction);
            NextPageCommand = new DelegateCommand(NextPageAction);
            LastPageCommand = new DelegateCommand(LastPageAction);
            PageSizeChangeCommand = new DelegateCommand(PageSizeChangeAction);

            PaggingChangeHandler = handler;

            if (-1 == totalCount) totalCount = sourceData.Count();

            _source = sourceData;
            _totalPage = DataHelper.Ceil((totalCount / (double)_pageSize));
            _fakeSoruce.Clear();
            _fakeSoruce.AddRange(_source.Take(_pageSize).ToList());
        }

        #region ICommand
        public ICommand FirstPageCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand LastPageCommand { get; set; }
        public ICommand PageSizeChangeCommand { get; set; }
        #endregion


        /// <summary>
        /// 页码变化事件，如果设置了事件，表明数据变化需要回调，而不是固定数据源
        /// </summary>
        private PaggingChangeCallBack PaggingChangeHandler { get; set; }

        /// <summary>
        /// 默认分页数
        /// </summary>
        const int DEFAULT_PAGE_SIZE = 10;

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
            if(PaggingChangeHandler != null)
            {
                PaggingChangeHandler(1, _pageSize);
                return;
            }
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
                if (PaggingChangeHandler != null)
                {
                    PaggingChangeHandler(CurrentPage - 1, _pageSize);
                    return;
                }
                SetViewModeSource(_source.Skip((CurrentPage - 2) * _pageSize).Take(_pageSize).ToList());
                CurrentPage--;
            }
        }


        private void NextPageAction()
        {            
            if (CurrentPage == _totalPage) return;
            if (PaggingChangeHandler != null)
            {
                PaggingChangeHandler(CurrentPage + 1, _pageSize);
                return;
            }
            var takeIdx = CurrentPage * _pageSize;
            SetViewModeSource(_source.Skip(takeIdx).Take(_pageSize));
            CurrentPage++;
        }

        private void LastPageAction()
        {
            CurrentPage = TotalPage;
            if (PaggingChangeHandler != null)
            {
                PaggingChangeHandler(TotalPage, _pageSize);
                return;
            }
            int skipCount = (_totalPage - 1) * _pageSize;
            int takeCount = _source.Count() - skipCount;
            SetViewModeSource(_source.Skip(skipCount).Take(takeCount).ToList());
        }


        private void PageSizeChangeAction()
        {
            if (PaggingChangeHandler != null)
            {
                PaggingChangeHandler(CurrentPage, _pageSize);
                return;
            }
            var takeIdx = CurrentPage * _pageSize;
            SetViewModeSource(_source.Skip(takeIdx).Take(_pageSize));
        }
    }
}
