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

using WpfApp1.Base;
using WpfApp1.Data;
using WpfApp1.Data.View;
using WpfApp1.Panels.business;

using MaterialDesignThemes.Wpf;

namespace WpfApp1.Panels.main
{
    /// <summary>
    /// LeftMenu.xaml 的交互逻辑
    /// </summary>
    public partial class LeftMenu : UserControl
    {
        public LeftMenu()
        {
            InitializeComponent();

            Init();
        }

        /// <summary>
        /// 根菜单数据
        /// </summary>
        private MenuData m_rootMenu = new MenuData();

        const string PACK_ICON_URL_PREFIX = "PackIconKind:";

        private string PackPackIconUrl(PackIconKind kind)
        {
            return string.Format("{0}:{1}", PACK_ICON_URL_PREFIX, kind.ToString());
        }

        private string UnpackPackIconUrl(string packIconUrl)
        {
            if (string.IsNullOrEmpty(packIconUrl)) return "";
            var idx = packIconUrl.IndexOf(PACK_ICON_URL_PREFIX);
            if (idx < 0) return string.Empty;
            return packIconUrl.Substring(PACK_ICON_URL_PREFIX.Length + 1);
        }

        /// <summary>
        /// 初始化菜单数据
        /// </summary>
        public void Init()
        {
            var menuOp = new MenuData("业务操作", "", PackPackIconUrl(PackIconKind.RelativeScale));
            menuOp.AddChildMenuData(new MenuData("货代业务操作中心", typeof(BusinessOpCenterPanel), "", true));
            menuOp.AddChildMenuData(new MenuData("预定舱管理", Consts.MENU_NAME_BOOKED_SHIP_MANAGE, ""));
            menuOp.AddChildMenuData(new MenuData("跟踪服务", Consts.MENU_NAME_TRACE_SERVICES, ""));
            
            var menuInfoManage = new MenuData("基本信息维护","", PackPackIconUrl(PackIconKind.YoutubeCreatorStudio));
            menuInfoManage.AddChildMenuData(new MenuData("商品信息", typeof(GoodsInfoManagePanel), ""));
            menuInfoManage.AddChildMenuData(new MenuData("司机信息", typeof(DriverManagePanel), ""));
            menuInfoManage.AddChildMenuData(new MenuData("码头信息", typeof(WharfsManagePanel), ""));
            menuInfoManage.AddChildMenuData(new MenuData("航线信息", typeof(AirLineManagePanel), ""));
            menuInfoManage.AddChildMenuData(new MenuData("车牌号", typeof(CarNoManagePanel), ""));

            var menuCustomRelationManage = new MenuData("客户关系管理", "", PackPackIconUrl(PackIconKind.HumanMale));
            menuCustomRelationManage.AddChildMenuData(new MenuData("客户管理", typeof(CustomManagePanel), ""));
            menuCustomRelationManage.AddChildMenuData(new MenuData("公共客户管理", typeof(PublicCustomManagePanel), ""));

            var menuSelfSettingManage = new MenuData("个性化管理", "", PackPackIconUrl(PackIconKind.Wrench));
            menuSelfSettingManage.AddChildMenuData(new MenuData("货代工作单信息", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            var menuBusinessManage = new MenuData("商务管理", "", PackPackIconUrl(PackIconKind.AppleKeyboardCommand));
            menuBusinessManage.AddChildMenuData(new MenuData("客户管理", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            var menuUserCenter = new MenuData("用户中心", "", PackPackIconUrl(PackIconKind.TicketPercent));
            menuUserCenter.AddChildMenuData(new MenuData("客户管理", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            var menuStudy = new MenuData("神鲸研究院", "", PackPackIconUrl(PackIconKind.GestureDoubleTap));
            menuStudy.AddChildMenuData(new MenuData("神鲸研究院", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            m_rootMenu.AddChildMenuData(menuOp);
            m_rootMenu.AddChildMenuData(menuInfoManage);
            m_rootMenu.AddChildMenuData(menuCustomRelationManage);
            m_rootMenu.AddChildMenuData(menuSelfSettingManage);
            m_rootMenu.AddChildMenuData(menuBusinessManage);
            m_rootMenu.AddChildMenuData(menuUserCenter);
            m_rootMenu.AddChildMenuData(menuStudy);

            TreeViewItem initItem = null;
            for (var i = 0;i < m_rootMenu.ChildMenus.Count;i++)
            {
                var newItem = InitMenuOnData(m_rootMenu.ChildMenus[i]);
                rootMenuTree.Items.Add(newItem);
                if (i == 0)
                {
                    initItem = newItem;
                }
            }

            if(null != initItem)
            {
                initItem.IsExpanded = true;
            }
        }


        /// <summary>
        /// 按照menu数据创建TreeViewItem
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TreeViewItem InitMenuOnData(MenuData data)
        {
            var item = new TreeViewItem();            
            item.Name = data.MenuID;
            item.MouseLeftButtonUp += MenuClicked;
            if (!string.IsNullOrEmpty(data.IconUrl))
            {
                // 设置图标
                var headerLayout = new StackPanel();
                headerLayout.Orientation = Orientation.Horizontal;

                var packIconName = UnpackPackIconUrl(data.IconUrl);
                if(!string.IsNullOrEmpty(packIconName))
                {
                    var icon = new PackIcon();
                    icon.Width = 16;
                    icon.Height = 16;
                    icon.Kind = (PackIconKind)Enum.Parse(typeof(PackIconKind), packIconName);
                    icon.Foreground = new SolidColorBrush(Colors.White);
                    headerLayout.Children.Add(icon);
                }
                
                var textBlock = new TextBlock();
                textBlock.Text = data.Name;
                textBlock.Margin = new Thickness(5, 0, 5, 0);
                textBlock.Height = 20;
                //textBlock.Foreground = new SolidColorBrush(Color.FromRgb(113, 121, 130));
                textBlock.Foreground = new SolidColorBrush(Colors.White);
                headerLayout.Children.Add(textBlock);

                item.Header = headerLayout;
            }
            else
            {
                item.Header = data.Name;
                item.Foreground = new SolidColorBrush(Colors.White);
            }

            if(data.ChildMenus.Count > 0)
            {
                //item.Background = new SolidColorBrush(Color.FromRgb(38, 50, 64));
                foreach (var eData in data.ChildMenus)
                {
                    // 递归初始化各层级子菜单
                    item.Items.Add(InitMenuOnData(eData));
                }
            }
            else
            {
                // 没有子菜单的选项
                item.Background = new SolidColorBrush(Color.FromRgb(30, 38, 49));
            }

            return item;
        }



        private void MenuClicked(object sender, MouseButtonEventArgs e)
        {
            if (null == OnMenuClickedHandler) return;

            var clickedItem = sender as TreeViewItem;

            var menuID = clickedItem.Name;

            var menuData = m_rootMenu.FindMenuDataOnId(menuID);

            if (menuData.ChildMenus.Count > 0)
            {
                clickedItem.IsExpanded = !clickedItem.IsExpanded;
                //var result = clickedItem.IsExpanded ? true : clickedItem.ExpandSubtree();
            }
            else
            {
                OnMenuClickedHandler(menuData, e);

                e.Handled = true;
            }
        }

        /// <summary>
        /// 菜单触发点击事件的回调
        /// </summary>
        public event EventHandler OnMenuClickedHandler = null;
    }
}
