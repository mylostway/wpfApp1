using System;
using System.Collections.Generic;
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
    /// <summary>
    /// SearchTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        public SearchTextBox()
        {
            InitializeComponent();
        }

        static SearchTextBox()
        {
            SelectedTextProperty = DependencyProperty.Register("SelectedText", typeof(string), typeof(SearchTextBox), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnSelectedTextChange)));
        }

        /// <summary>
        /// 重写Size变化事件，保证search button的大小最少为MinWidth
        /// </summary>
        /// <param name="sizeInfo"></param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            var size = sizeInfo.NewSize.Width;

            if (size < STB_MIN_WIDTH) size = STB_MIN_WIDTH;

            this.tbx_result.Width = size - btn_search.MinWidth - MIN_PADDING;
        }

        /// <summary>
        /// 该控件最小总长度
        /// </summary>
        const int STB_MIN_WIDTH = 60;

        const int MIN_PADDING = 3;

        /// <summary>
        /// 附带信息
        /// </summary>
        public object Context { get; set; }

        #region 依赖属性

        /// <summary>
        /// 选中文本
        /// </summary>
        public static readonly DependencyProperty SelectedTextProperty;

        private static void OnSelectedTextChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as SearchTextBox;

            if (p != null)
            {
                var textBox = (TextBox)p.FindName("tbx_result");
                textBox.Text = (string)e.NewValue;
            }
        }

        /// <summary>
        /// 选中的Text        
        /// </summary>
        public string SelectedText
        {
            get { return (string)GetValue(SelectedTextProperty); }
            set { SetValue(SelectedTextProperty, value); }
        }

        #endregion


        public IEnumerable<object> SearchDataContext = null;

        private async void btn_search_Click(object sender, RoutedEventArgs e)
        {
            await DynamicDataGrid.Show(SearchDataContext);

            var selectPrimaryVal = DynamicDataGrid.SelectedPrimaryVal;

            this.tbx_result.Text = selectPrimaryVal.ToString();
        }
    }
}
