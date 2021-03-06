﻿using System;
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

namespace WpfApp1.Panels.Business.BusinessOp.BusinessOpCenterPanel
{
    /// <summary>
    /// CounterInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class CounterInfoPanel : UserControl
    {
        public CounterInfoPanel()
        {
            InitializeComponent();

            //this.grid_dataList.ItemsSource = EditInfo;
        }

        public IList<FreBusinessContainsInfoEntity> EditInfo { get; set; } = new List<FreBusinessContainsInfoEntity>();

        public void Init(IList<FreBusinessContainsInfoEntity> editInfo)
        {
            if (null == editInfo) return;

            EditInfo = editInfo;

            this.grid_dataList.ItemsSource = EditInfo;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
