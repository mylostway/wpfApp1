using BaseLib.Data;
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
using WL_OA.Data;
using WL_OA.Data.utils;
using WpfApp1.Data;

namespace WpfApp1.Panels.extend_control
{
    /// <summary>
    /// 分页数回调
    /// </summary>
    /// <param name="pageIdx"></param>
    /// <param name="pageSize"></param>
    public delegate void PaggingChangeCallBack(int pageIdx, int pageSize);

    public class PagerMode<T>
        where T : new()
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<T> SourceData { get; set; }

        public PaggingChangeCallBack PaggingChangeHandler { get; set; }

        public FrameworkElement PaggingElement { get; set; }
    }

    /// <summary>
    /// Pager.xaml 的交互逻辑
    /// </summary>
    public partial class Pager : UserControl
    {
        /// <summary>
        /// 默认分页PageSize
        /// </summary>
        const int DEFAULT_PAGE_SIZE = 10;

        private int m_currentPage = 0;

        public int CurrentPage
        {
            get { return m_currentPage; }
            set
            {
                m_currentPage = value;
                rCurrent.Text = m_currentPage.ToString();
            }
        }

        private int m_totalPage = 0;

        public int TotalPage
        {
            get { return m_totalPage; }
            set { m_totalPage = value;this.rTotal.Text = m_totalPage.ToString(); }
        }

        public int PageSize { get; set; }

        private int m_totalCount = 0;

        public int TotalCount
        {
            get { return m_totalCount; }
            set
            {
                m_totalCount = value;
                this.tbk_totalCount.Text = m_totalCount.ToString();
            }
        }

        public PaggingChangeCallBack PaggingChangeHandler { get; set; }

        public Pager()
        {
            InitializeComponent();
            cb_pageSize.BindComboxToEnums<DefaultPageSizeEnums>();
            Reset();
        }

        public static readonly DependencyProperty CurrentPageProperty;
        public static readonly DependencyProperty PageSizeProperty;

        static Pager()
        {
            //CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(int), typeof(Pager), new PropertyMetadata(0, new PropertyChangedCallback(OnCurrentPageChanged)));            
            //PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(Pager), new PropertyMetadata(0, new PropertyChangedCallback(OnPageSizeChanged)));
        }
        
        public void Init(int totalCount, PaggingChangeCallBack handler)
        {
            if (totalCount < 0) throw new UserFriendlyException($"分页数据异常，总数据量不能小于0");
            TotalCount = totalCount;
            PaggingChangeHandler = handler;
            TotalPage = DataHelper.Ceil((totalCount / (double)PageSize));
        }


        public void Reset()
        {
            TotalCount = 0;
            PaggingChangeHandler = null;
            TotalPage = 0;
            CurrentPage = 1;
            PageSize = DEFAULT_PAGE_SIZE;
        }


        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Pager p = d as Pager;
            if (p != null)
            {
                Run rCurrrent = (Run)p.FindName("rCurrent");
                rCurrrent.Text = e.NewValue.ToString();

                p.PaggingChangeHandler?.Invoke(p.CurrentPage, p.PageSize);
            }
        }

        public static void OnPageSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Pager p = d as Pager;
            if (p != null)
            {
                var targetElem = (ComboBox)p.FindName("cb_pageSize");
                targetElem.SelectedValue = (int)e.NewValue;

                p.PaggingChangeHandler?.Invoke(p.CurrentPage, p.PageSize);
            }
        }

        private void FirstPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = 1;
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage == 1) return;
            CurrentPage--;
            PaggingChangeHandler?.Invoke(CurrentPage, PageSize);
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage == TotalPage) return;
            CurrentPage++;
            PaggingChangeHandler?.Invoke(CurrentPage, PageSize);
        }

        private void LastPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = TotalPage;
            PaggingChangeHandler?.Invoke(CurrentPage, PageSize);
        }

        private void btn_go_Click(object sender, RoutedEventArgs e)
        {
            var goPageVal = tbx_goPageCount.Text;
            int nGoPage = 0;
            int nMaxPage = TotalPage;
            if (!int.TryParse(goPageVal, out nGoPage)) nGoPage = 1;

            if(nGoPage <= 1)
            {
                CurrentPage = 1;
            }
            else if(nGoPage >= nMaxPage)
            {
                CurrentPage = TotalPage;
            }
            else
            {
                CurrentPage = (nGoPage - 1);
            }
            PaggingChangeHandler?.Invoke(CurrentPage, PageSize);
        }

        private void cb_pageSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selItem = cb_pageSize.SelectedValue as EnumInfo;
            if (null == selItem) return;
            PageSize = selItem.Name.ToInt32();
            SAssert.MustTrue(PageSize > 0,$"分页数据异常，PageSize：{PageSize} 不为大于0的数值");
        }
    }
}
