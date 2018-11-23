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
    public delegate string SearchTextBoxClickHandler(SearchTextBox box, object context);

    /// <summary>
    /// SearchTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        public SearchTextBox()
        {
            InitializeComponent();
        }


        public object Context { get; set; }

        public SearchTextBoxClickHandler OnSearchClickedHandler { get; set; }

        public string SelectedText
        {
            get
            {
                return this.tbx_result.Text;
            }
            private set { this.tbx_result.Text = value; }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            SelectedText = OnSearchClickedHandler?.Invoke(this, Context);
        }
    }
}
