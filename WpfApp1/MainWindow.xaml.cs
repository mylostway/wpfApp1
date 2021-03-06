﻿using System;
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

using WL_OA.Data.utils;

using WpfApp1.Data.View;
using WpfApp1.Data.NDAL;
using WpfApp1.Panels.business;
using MaterialDesignThemes.Wpf;
using WpfApp1.Panels.extend_control;
using WpfApp1.Panels.Business.CustomRelationManage;
using WpfApp1.Panels.Business.BusinessOp;
using WpfApp1.Panels;
using WpfApp1.Panels.Business.BusinessOp.BusinessOpCenterPanel;
using WpfApp1.Panels.Business;
using WpfApp1.Panels.Business.SystemManage;
using WpfApp1.Data;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitWindow();
        }        


        /// <summary>
        /// window初始化
        /// </summary>
        private void InitWindow()
        {
            leftMenu.OnMenuClickedHandler += LeftMenu_OnMenuClicked;

            // for test
            //tabContentView.AddTab("测试tab", new BusinessOpCenterPanel());
            //tabContentView.AddTab("测试tab", new DriverManagePanel());
            //tabContentView.AddTab("测试tab", new CustomManagePanel());
            //tabContentView.AddTab("测试tab", new HoldingGoodsInfoPanel());
            tabContentView.AddTab("测试tab", new BusinessOpCenterPanel());
            //tabContentView.AddTab("测试tab", new TestPanel());

            //tabContentView.AddTab("测试tab", new Panels.Business.FeeDetailPanel());
            //tabContentView.AddTab("测试tab", new OnlineAduitPanel());
        }

        private void LeftMenu_OnMenuClicked(object sender, EventArgs e)
        {
            var clickedMenuData = sender as MenuData;

            SAssert.MustTrue(null != clickedMenuData,string.Format("事件绑定有误，触发者不为MenuData"));

            var attachedData = clickedMenuData.AttachedData;

            // FIXME：本来以为MenuItem点击之后只触发自身的，但目前结果看来，子菜单被点击之后也触发父菜单事件，原因待查，目前用这个办法解决
            if (null == attachedData) return;

            var panelType = attachedData.GetType();

            var attachDataType = attachedData as Type;

            if(null == attachDataType)
            {
                // 非type类attachedData，忽略
                return;
            }

            var panel = Activator.CreateInstance(attachDataType) as UserControl;

            if(null == panel)
            {
                // 非UserControl类的忽略掉
                return;
            }

            tabContentView.AddTab(clickedMenuData.Name, panel, clickedMenuData.IsAllowMutiPanel);
        }

        private void topHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void topHeader_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
            else WindowState = WindowState.Maximized;
        }
    }
}
