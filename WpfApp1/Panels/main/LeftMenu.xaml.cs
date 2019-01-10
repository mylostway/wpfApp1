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
            var sysManager = new MenuData("系统管理", "", PackPackIconUrl(PackIconKind.RelativeScale));
            sysManager.AddChildMenuData(new MenuData("在线审批", typeof(BusinessOpCenterPanel), ""));
            sysManager.AddChildMenuData(new MenuData("费用核销控制", typeof(BusinessOpCenterPanel), ""));
            sysManager.AddChildMenuData(new MenuData("位置服务配置", typeof(BusinessOpCenterPanel), ""));
            sysManager.AddChildMenuData(new MenuData("登录日志", typeof(BusinessOpCenterPanel), ""));

            var organizationManager = new MenuData("组织机构管理", "", PackPackIconUrl(PackIconKind.RelativeScale));
            organizationManager.AddChildMenuData(new MenuData("公司信息管理", typeof(BusinessOpCenterPanel), ""));
            organizationManager.AddChildMenuData(new MenuData("部门管理", typeof(BusinessOpCenterPanel), ""));
            organizationManager.AddChildMenuData(new MenuData("用户管理", typeof(BusinessOpCenterPanel), ""));
            organizationManager.AddChildMenuData(new MenuData("发票抬头管理", typeof(BusinessOpCenterPanel), ""));

            var menuOp = new MenuData("业务操作", "", PackPackIconUrl(PackIconKind.RelativeScale));
            menuOp.AddChildMenuData(new MenuData("货代业务操作中心", typeof(BusinessOpCenterPanel), "", true));
            menuOp.AddChildMenuData(new MenuData("预定舱管理", Consts.MENU_NAME_BOOKED_SHIP_MANAGE, ""));
            menuOp.AddChildMenuData(new MenuData("在线订舱", Consts.MENU_NAME_BOOKED_SHIP_MANAGE, ""));
            menuOp.AddChildMenuData(new MenuData("新新搜车", Consts.MENU_NAME_BOOKED_SHIP_MANAGE, ""));
            menuOp.AddChildMenuData(new MenuData("跟踪服务", Consts.MENU_NAME_TRACE_SERVICES, ""));
            menuOp.AddChildMenuData(new MenuData("扫码回单管理", Consts.MENU_NAME_BOOKED_SHIP_MANAGE, ""));

            var menuInfoManage = new MenuData("基本信息维护","", PackPackIconUrl(PackIconKind.YoutubeCreatorStudio));
            // 基本类
            menuInfoManage.AddChildMenuData(new MenuData("定制装卸地点", typeof(WharfsManagePanel), ""));
            menuInfoManage.AddChildMenuData(new MenuData("港口码头", typeof(WharfsManagePanel), ""));

            // 业务类
            menuInfoManage.AddChildMenuData(new MenuData("车牌号", typeof(CarNoManagePanel), ""));
            menuInfoManage.AddChildMenuData(new MenuData("司机", typeof(DriverManagePanel), ""));
            menuInfoManage.AddChildMenuData(new MenuData("货名维护", typeof(GoodsInfoManagePanel), ""));
            menuInfoManage.AddChildMenuData(new MenuData("业务子类型", typeof(GoodsInfoManagePanel), ""));

            // 财务类
            menuInfoManage.AddChildMenuData(new MenuData("结算方式", typeof(DriverManagePanel), ""));
            menuInfoManage.AddChildMenuData(new MenuData("公司账号", typeof(GoodsInfoManagePanel), ""));
            menuInfoManage.AddChildMenuData(new MenuData("费用项目", typeof(GoodsInfoManagePanel), ""));

            // 船务类
            menuInfoManage.AddChildMenuData(new MenuData("航线", typeof(AirLineManagePanel), ""));
            

            var menuCustomRelationManage = new MenuData("客户关系管理", "", PackPackIconUrl(PackIconKind.HumanMale));
            menuCustomRelationManage.AddChildMenuData(new MenuData("客户管理", typeof(CustomManagePanel), ""));
            menuCustomRelationManage.AddChildMenuData(new MenuData("公共客户管理", typeof(PublicCustomManagePanel), ""));

            var menuSelfSettingManage = new MenuData("个性化管理", "", PackPackIconUrl(PackIconKind.Wrench));
            menuSelfSettingManage.AddChildMenuData(new MenuData("货代工作单信息", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuSelfSettingManage.AddChildMenuData(new MenuData("货代工作单列表", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuSelfSettingManage.AddChildMenuData(new MenuData("货代工作单货柜", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuSelfSettingManage.AddChildMenuData(new MenuData("货代工作单费用", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuSelfSettingManage.AddChildMenuData(new MenuData("商务审核列表", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuSelfSettingManage.AddChildMenuData(new MenuData("商务审核费用", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuSelfSettingManage.AddChildMenuData(new MenuData("费用审核列表", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuSelfSettingManage.AddChildMenuData(new MenuData("商务对账列表", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuSelfSettingManage.AddChildMenuData(new MenuData("费用核销列表", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuSelfSettingManage.AddChildMenuData(new MenuData("收付款核销列表", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuSelfSettingManage.AddChildMenuData(new MenuData("凭证列表", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            var menuBusinessManage = new MenuData("商务管理", "", PackPackIconUrl(PackIconKind.AppleKeyboardCommand));
            menuBusinessManage.AddChildMenuData(new MenuData("商务审核", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuBusinessManage.AddChildMenuData(new MenuData("费用审核", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuBusinessManage.AddChildMenuData(new MenuData("商务对账", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            var priceManage = new MenuData("费用管理", "", PackPackIconUrl(PackIconKind.AppleKeyboardCommand));
            priceManage.AddChildMenuData(new MenuData("默认费用管理", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            var financeManage = new MenuData("财务管理", "", PackPackIconUrl(PackIconKind.AppleKeyboardCommand));
            financeManage.AddChildMenuData(new MenuData("凭证管理", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            financeManage.AddChildMenuData(new MenuData("费用核销", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            financeManage.AddChildMenuData(new MenuData("收付款核销", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            financeManage.AddChildMenuData(new MenuData("收付款反核销", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            financeManage.AddChildMenuData(new MenuData("财务费用登账", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            financeManage.AddChildMenuData(new MenuData("财务应收应付查询", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            var summaryAnalyse = new MenuData("统计分析", "", PackPackIconUrl(PackIconKind.AppleKeyboardCommand));
            summaryAnalyse.AddChildMenuData(new MenuData("经营报表", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            summaryAnalyse.AddChildMenuData(new MenuData("业务员单票毛利统计", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            var shortMsgManage = new MenuData("短信管理", "", PackPackIconUrl(PackIconKind.AppleKeyboardCommand));
            shortMsgManage.AddChildMenuData(new MenuData("自定义短信模板", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            shortMsgManage.AddChildMenuData(new MenuData("短信发送记录", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            shortMsgManage.AddChildMenuData(new MenuData("短信账号维护", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            var menuUserCenter = new MenuData("用户中心", "", PackPackIconUrl(PackIconKind.TicketPercent));
            menuUserCenter.AddChildMenuData(new MenuData("公司信息", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuUserCenter.AddChildMenuData(new MenuData("部门信息", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuUserCenter.AddChildMenuData(new MenuData("个人信息", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuUserCenter.AddChildMenuData(new MenuData("修改密码", Consts.MENU_NAME_CUSTOM_MANAGE, ""));
            menuUserCenter.AddChildMenuData(new MenuData("位置服务管理", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            var menuStudy = new MenuData("神鲸研究院", "", PackPackIconUrl(PackIconKind.GestureDoubleTap));
            menuStudy.AddChildMenuData(new MenuData("神鲸研究院", Consts.MENU_NAME_CUSTOM_MANAGE, ""));

            m_rootMenu.AddChildMenuData(sysManager);
            m_rootMenu.AddChildMenuData(organizationManager);
            m_rootMenu.AddChildMenuData(menuOp);
            m_rootMenu.AddChildMenuData(menuInfoManage);
            m_rootMenu.AddChildMenuData(menuCustomRelationManage);
            m_rootMenu.AddChildMenuData(menuSelfSettingManage);
            m_rootMenu.AddChildMenuData(menuBusinessManage);
            m_rootMenu.AddChildMenuData(priceManage);
            m_rootMenu.AddChildMenuData(financeManage);
            m_rootMenu.AddChildMenuData(summaryAnalyse);
            m_rootMenu.AddChildMenuData(shortMsgManage);
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
