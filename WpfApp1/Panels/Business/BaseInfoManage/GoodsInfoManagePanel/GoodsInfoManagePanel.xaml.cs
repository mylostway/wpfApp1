﻿using System;
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
using WpfApp1.Data.Test;

namespace WpfApp1.Panels.business
{
    /// <summary>
    /// GoodsInfoManagePanel.xaml 的交互逻辑
    /// </summary>
    public partial class GoodsInfoManagePanel : UserControl
    {
        public GoodsInfoManagePanel()
        {
            InitializeComponent();

            var pageViewMode = new PaggingViewMode<GoodsInfoStruct>(
                FakeDataHeler<GoodsInfoStruct>.Instance.CreateFakeDataCollection());

            DataContext = pageViewMode;
        }


    }
}
