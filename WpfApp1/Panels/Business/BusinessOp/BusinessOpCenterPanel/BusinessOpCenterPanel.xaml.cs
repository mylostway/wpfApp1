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

using WpfApp1.Data;
using WpfApp1.Data.Test;

namespace WpfApp1.Panels.business
{
    /// <summary>
    /// BusinessOpCenterPanel.xaml 的交互逻辑
    /// </summary>
    public partial class BusinessOpCenterPanel : UserControl
    {
        public BusinessOpCenterPanel()
        {
            InitializeComponent();

            ResetSearch();

            DataContext = new PaggingViewMode<BusinessOpCenterStruct>(
                FakeDataHeler<BusinessOpCenterStruct>.Instance.CreateFakeDataCollection()); 
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<BusinessOpCenterStruct> searchData = null;

            // TODO：调用search API

            if(null != searchData)
            {
                DataContext = new PaggingViewMode<BusinessOpCenterStruct>(searchData);
            }
        }

        private void ResetSearch()
        {
            tbx_searchID.Text = "";
            dp_startDate.Text = DateTimeHelper.GetDayStringOnAddingDays();
            dp_endDate.Text = DateTimeHelper.GetDayStringOnAddingDays(1);
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            ResetSearch();
        }
    }
}
