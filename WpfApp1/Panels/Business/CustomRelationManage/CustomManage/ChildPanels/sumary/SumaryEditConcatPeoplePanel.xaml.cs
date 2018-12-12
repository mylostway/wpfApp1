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
    public partial class SumaryEditConcatPeoplePanel : UserControl
    {
        public SumaryEditConcatPeoplePanel()
        {
            /*
            var testEntity = new CustomerContactEntity();
            testEntity.Fcert = "39172839789";
            testEntity.Fname = "test";
            testEntity.Fmobile = "15002094251";
            EditingConcatPeopleList.Add(testEntity);
            */

            InitializeComponent();

            if(null == EditingConcatPeopleList) EditingConcatPeopleList = new List<CustomerContactEntity>();
            grid_summaryConcatPeoples.ItemsSource = EditingConcatPeopleList;
        }

        public IList<CustomerContactEntity> EditingConcatPeopleList { get; private set; } = null;

        public void Init(IList<CustomerContactEntity> editingConcatPeopleList)
        {
            EditingConcatPeopleList = editingConcatPeopleList;
            grid_summaryConcatPeoples.ItemsSource = EditingConcatPeopleList;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
