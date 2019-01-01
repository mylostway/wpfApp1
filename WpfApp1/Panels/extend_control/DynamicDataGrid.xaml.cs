using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using WpfApp1.Data;

namespace WpfApp1.Panels.extend_control
{
   
    /// <summary>
    /// 动态选择配置的DataGrid必须继承的基类
    /// </summary>
    public interface IDynamicSelectedDataGirdBaseViewMode
    {
        [FakeDataNotGenAttribute]
        string SelectText { get; set; }
    }


    /// <summary>
    /// DynamicDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class DynamicDataGrid : UserControl
    {
        public DynamicDataGrid()
        {
            InitializeComponent();
        }

        const string DEFAULT_DYNAMIC_DG_TITLE = "请选择";

        private static DynamicDataGrid s_instance = new DynamicDataGrid();

        private Dictionary<string, object> KeyObjDic = new Dictionary<string, object>();

        private List<string> AllKeyList = new List<string>();

        /// <summary>
        /// 选中的数据
        /// </summary>
        public static object SelectedObj = null;

        /// <summary>
        /// 选中数据的关键值
        /// </summary>
        public static object SelectedPrimaryVal = "";

        /// <summary>
        /// 关键属性
        /// </summary>
        private static PropertyInfo s_primaryProperty = null;


        /// <summary>
        /// 当前枚举数据
        /// </summary>
        private static IEnumerable<object> s_allDatas = null;

        /// <summary>
        /// 过滤过的数据
        /// </summary>
        private static IEnumerable<object> s_filterDatas = null;

        /// <summary>
        /// 过滤词
        /// </summary>
        private string s_filterWord = null;

        /// <summary>
        /// 选择后回调
        /// </summary>
        private EventHandler m_callBackHandler = null;

        public void ClearGird()
        {
            dg_dynamic.ItemsSource = null;
            dg_dynamic.Columns.Clear();

            AllKeyList.Clear();
            KeyObjDic.Clear();
            SelectedObj = null;
            SelectedPrimaryVal = null;
            s_primaryProperty = null;

            s_filterWord = null;
            s_allDatas = null;
            s_filterDatas = null;
        }


        public void Init(IEnumerable<object> bindingData,EventHandler callBack = null,string title = "")
        {
            if (null == bindingData) throw new Exception("动态DataGrid初始化的数据不能为空！");

            // 目前不处理空数据
            if (0 == bindingData.Count()) return;

            ClearGird();

            s_allDatas = bindingData;

            m_callBackHandler = callBack;

            var type = bindingData.ElementAt(0).GetType();

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var dynamicSearchColAttr = type.GetCustomAttribute<DynamicDataGridSearchColAttribute>();

            var keyColName = dynamicSearchColAttr?.KeyColumn;

            var searchColDic = dynamicSearchColAttr.ExplainSearchColumnSettings();

            string colHeader = "";

            // 根据配置，找出显示的查询属性
            var selProperties = from pro in properties
                                 where searchColDic.Keys.Contains(pro.Name)
                                 select pro;

            if (null == selProperties || 0 == selProperties.Count()) return;

            PropertyInfo keyColumnProperty = null;
            if (!string.IsNullOrEmpty(keyColName))
                keyColumnProperty = (from pro in selProperties where pro.Name == keyColName select pro).First();
            else
            {
                // 如果没有配置关键列，则取第一列
                keyColumnProperty = selProperties.First();
                keyColName = keyColumnProperty.Name;
            }
             
            s_primaryProperty = keyColumnProperty;

            var choiceCol = new DataGridHyperlinkColumn();
            choiceCol.Header = "操作";
            choiceCol.Binding = new Binding("SelectText");
            choiceCol.Width = DataGridLength.Auto;
            choiceCol.MinWidth = 60;
            dg_dynamic.Columns.Add(choiceCol);

            foreach (var ePro in selProperties)
            {
                var proName = ePro.Name;
                colHeader = searchColDic[proName];

                var newCol = new DataGridTextColumn();
                newCol.Header = colHeader;
                newCol.Binding = new Binding(proName);                
                newCol.MinWidth = 100;
                dg_dynamic.Columns.Add(newCol);
            }

            dg_dynamic.ItemsSource = bindingData;

            foreach (var eData in bindingData)
            {
                var key = keyColumnProperty.GetValue(eData).ToString();
                AllKeyList.Add(key);
                KeyObjDic.Add(key, eData);
            }

            acb_search.ItemsSource = AllKeyList;
        }


        public void Init<T>(IEnumerable<T> bindingData, EventHandler callBack = null, string title = "")
            where T : class, IDynamicSelectedDataGirdBaseViewMode
        {
            Init(bindingData, callBack, title);
        }

        /// <summary>
        /// 超链接点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_dynamic_Click(object sender, RoutedEventArgs e)
        {
            if (null == dg_dynamic.SelectedItem) return;
            SelectedObj = dg_dynamic.SelectedItem;
            SelectedPrimaryVal = s_primaryProperty.GetValue(SelectedObj);
            m_callBackHandler?.Invoke(SelectedPrimaryVal, null);
            this.Close();
        }

        /// <summary>
        /// 显示动态选择数据列表
        /// </summary>
        /// <param name="bindingData"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public static async Task Show(IEnumerable<object> bindingData, EventHandler callBack = null)
        {
            s_instance.Init(bindingData, callBack);
            await DialogHost.Show(s_instance, "MainDialogHost", new DialogOpenedEventHandler((x, args) =>
            {
                s_instance.rootLayout.Visibility = Visibility.Visible;
            }), new DialogClosingEventHandler((x, args) =>
            {
                s_instance.rootLayout.Visibility = Visibility.Hidden;
            }));
        }

        /// <summary>
        /// 显示动态选择数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bindingData"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public static async Task Show<T>(IEnumerable<T> bindingData, EventHandler callBack = null)
            where T : class, IDynamicSelectedDataGirdBaseViewMode
        {
            s_instance.Init(bindingData, callBack);
            await DialogHost.Show(s_instance, "MainDialogHost", new DialogOpenedEventHandler((x, args) =>
            {
                s_instance.rootLayout.Visibility = Visibility.Visible;
            }), new DialogClosingEventHandler((x, args) =>
            {
                s_instance.rootLayout.Visibility = Visibility.Hidden;
            }));
        }

        /// <summary>
        /// 直接隐藏等待框
        /// </summary>
        public static void Hide()
        {
            s_instance.Close();
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        public void Close()
        {
            DialogHost.CloseDialogCommand.Execute(null, this);
        }

        /// <summary>
        /// 筛选条件，根据输入的单词筛选结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            var filterWord = this.acb_search.Text;

            if(!string.IsNullOrEmpty(filterWord))
            {
                s_filterDatas = from item in s_allDatas
                                where s_primaryProperty.GetValue(item).ToString().Contains(filterWord)
                                select item;
            }
            else
            {
                s_filterDatas = s_allDatas;
            }

            dg_dynamic.ItemsSource = s_filterDatas;
        }


        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            ClearGird();
            Hide();
        }
    }
}
