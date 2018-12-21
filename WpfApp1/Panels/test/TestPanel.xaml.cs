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
using WpfApp1.Data.Helpers;
using WpfApp1.Data.Modes;
using WpfApp1.Data.Test;
using WpfApp1.Panels.extend_control;

namespace WpfApp1.Panels
{
    public class AutoCompleteBoxTestName
    {
        public string Words { get; set; }
    }

    /// <summary>
    /// TestPanel.xaml 的交互逻辑
    /// </summary>
    public partial class TestPanel : UserControl
    {
        public TestPanel()
        {
            InitializeComponent();

            //var collection = FakeDataHelper.Instance.CreateFakeDataCollection<MultipleCheckboxModel>(5);

            var collection = new ObservableCollection<MultipleCheckboxModel>();

            var fakeList = EnumHelper.GetEnumInfoList<FreBusinessCompanyType>(); 

            foreach(var e in fakeList)
            {
                collection.Add(new MultipleCheckboxModel(e.EValue, e.Name));
            }

            this.mul_cbx.ItemsSource = collection;

            this.bfs_test.ItemSource = EnumHelper.GetEnumNamesOnType<CustomerInfoStateEnums>();

            this.bfs_test.BitValue = FakeDataHelper.Instance.GenRandomInt((int)Math.Pow(2,((int)CustomerInfoStateEnums.收短信 + 1)));

            this.stb_test.SearchDataContext = FakeDataHelper.Instance.CreateFakeDataCollection<GoodsinfoSelectPanelViewMode>().Distinct(new FastPropertyComparer<GoodsinfoSelectPanelViewMode>("Fmark"));

            this.acb_test.ItemsSource = FakeDataHelper.Instance.CreateFakeDataCollection<AutoCompleteBoxTestName>().Select(new Func<AutoCompleteBoxTestName, string>((x) =>
            {
                return x.Words;
            }));
        }
    }
}
