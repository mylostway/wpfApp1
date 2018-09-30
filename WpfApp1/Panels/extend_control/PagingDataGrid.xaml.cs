using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.Panels.extend_control
{
    public class PaggingDataGridViewMode<T> : ViewModel
        where T:new()
    {
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

        private ObservableCollection<T> _dataSoruce;

        public ObservableCollection<T> DataSource
        {
            get
            {
                return _dataSoruce;
            }
            set
            {
                if (_dataSoruce != value)
                {
                    _dataSoruce = value;
                    OnPropertyChanged("FakeSource");
                }
            }
        }

        public PaggingDataGridViewMode(ObservableCollection<T> datas)
        {
            _dataSoruce = datas;
        }
    }

    /// <summary>
    /// PagingDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class PagingDataGrid : UserControl
    {
        public PagingDataGrid()
        {
            InitializeComponent();
        }


        public void SetBindData<T>(ObservableCollection<T> bindData)
            where T : new()
        {
            DataContext = new PaggingDataGridViewMode<T>(bindData);
        }
    }
}
