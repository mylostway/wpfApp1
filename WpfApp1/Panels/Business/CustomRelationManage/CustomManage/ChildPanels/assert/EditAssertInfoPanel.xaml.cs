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

namespace WpfApp1.Panels.Business.CustomRelationManage
{
    /// <summary>
    /// EditAssertInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class EditAssertInfoPanel : UserControl
    {
        public EditAssertInfoPanel()
        {
            InitializeComponent();

            this.DataContext = EditCreditInfo;
        }

        public CustomerCreditInfoEntity EditCreditInfo { get; set; } = new CustomerCreditInfoEntity();

        public CustomerCreditInfoEntity GetEditInfo()
        {
            return EditCreditInfo;
        }

        public void Init(CustomerCreditInfoEntity editInfo)
        {
            if (null == editInfo) return;
            this.EditCreditInfo = editInfo;
            this.DataContext = EditCreditInfo;
        }
    }
}
