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
using WL_OA.Data.entity;

namespace WpfApp1.Panels.Business.CustomRelationManage
{
    /// <summary>
    /// SumaryEditConcatPeoplePanel.xaml 的交互逻辑
    /// </summary>
    public partial class SumaryEditHoldAddrPanel : UserControl
    {
        public SumaryEditHoldAddrPanel()
        {
            InitializeComponent();

            grid_summaryConcatPeoples.ItemsSource = EditingHoldAddrList;
        }

        public List<CustomerHoldAddrEntity> EditingHoldAddrList { get; set; } = new List<CustomerHoldAddrEntity>();

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            EditingHoldAddrList.Add(new CustomerHoldAddrEntity());
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
