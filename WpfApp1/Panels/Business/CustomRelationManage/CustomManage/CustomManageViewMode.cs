﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using BaseLib.Data;

using WL_OA.Data.dto;
using WL_OA.Data.entity;

using WpfApp1.Data;
using WpfApp1.Data.Test;

namespace WpfApp1.Panels.business
{
    /// <summary>
    /// 用于绑定DataGrid的数据源结构体
    /// </summary>
    public class CustomManageViewMode
    {
        static CustomManageViewMode()
        {
            var customerContactEntityList = new List<CustomerContactEntity>();

            customerContactEntityList.Add(new CustomerContactEntity()
            {
                
            });

            s_testEditInfo.CustomerInfo = new CustomerSummaryInfoDTO()
            {

            };

            s_testEditInfo.CreditInfo = new CustomerCreditInfoEntity()
            {

            };
        }

        private static CustomerInfoDTO s_testEditInfo = new CustomerInfoDTO();

        public static CustomerInfoDTO TestEditData
        {
            get { return s_testEditInfo; }
        }





    }
}
