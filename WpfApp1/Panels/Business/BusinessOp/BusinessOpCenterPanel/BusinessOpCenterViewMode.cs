using System;
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
using WL_OA.Data.entity;
using WpfApp1.Data;
using WpfApp1.Data.Test;

namespace WpfApp1.Panels.business
{
    public class FreBusinessCenterEntityViewMode : FreBusinessCenterEntity, INotifyPropertyChanged, IIsCheckableView
    {
        public FreBusinessCenterEntityViewMode() { }

        public FreBusinessCenterEntityViewMode(FreBusinessCenterEntity rhs) : base(rhs) { }

        /// <summary>
        /// viewMode子属性 - 物流动态 - 未知填什么值'
        /// </summary>
        public string VFinterflow_state
        {
            get { return finterflow_state; }
            set
            {
                if (finterflow_state == value) return;
                finterflow_state = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 托运人'
        /// </summary>
        public string VFconsign_man
        {
            get { return fconsign_man; }
            set
            {
                if (fconsign_man == value) return;
                fconsign_man = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 起运码头'
        /// </summary>
        public string VFstart_wharf
        {
            get { return fstart_wharf; }
            set
            {
                if (fstart_wharf == value) return;
                fstart_wharf = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 起运地'
        /// </summary>
        public string VFstart_place
        {
            get { return fstart_place; }
            set
            {
                if (fstart_place == value) return;
                fstart_place = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 目的地'
        /// </summary>
        public string VFto_place
        {
            get { return fto_place; }
            set
            {
                if (fto_place == value) return;
                fto_place = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 目的码头'
        /// </summary>
        public string VFto_wharf
        {
            get { return fto_wharf; }
            set
            {
                if (fto_wharf == value) return;
                fto_wharf = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 业务日期'
        /// </summary>
        public DateTime VFbusiness_date
        {
            get { return fbusiness_date; }
            set
            {
                if (fbusiness_date == value) return;
                fbusiness_date = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 业务员'
        /// </summary>
        public string VFbusinesser
        {
            get { return fbusinesser; }
            set
            {
                if (fbusinesser == value) return;
                fbusinesser = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 货名'
        /// </summary>
        public string VFgoods_name
        {
            get { return fgoods_name; }
            set
            {
                if (fgoods_name == value) return;
                fgoods_name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 装货低点'
        /// </summary>
        public string VFhold_goods_place
        {
            get { return fhold_goods_place; }
            set
            {
                if (fhold_goods_place == value) return;
                fhold_goods_place = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 装货联系人'
        /// </summary>
        public string VFhold_goods_people_phone
        {
            get { return fhold_goods_people_phone; }
            set
            {
                if (fhold_goods_people_phone == value) return;
                fhold_goods_people_phone = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 卸货低点'
        /// </summary>
        public string VFput_goods_place
        {
            get { return fput_goods_place; }
            set
            {
                if (fput_goods_place == value) return;
                fput_goods_place = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 卸货联系人'
        /// </summary>
        public string VFput_goods_people_phone
        {
            get { return fput_goods_people_phone; }
            set
            {
                if (fput_goods_people_phone == value) return;
                fput_goods_people_phone = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 操作条款'
        /// </summary>
        public string VFop_term
        {
            get { return fop_term; }
            set
            {
                if (fop_term == value) return;
                fop_term = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 运输条款'
        /// </summary>
        public string VFtransit_term
        {
            get { return ftransit_term; }
            set
            {
                if (ftransit_term == value) return;
                ftransit_term = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 付款方式'
        /// </summary>
        public string VFpay_way
        {
            get { return fpay_way; }
            set
            {
                if (fpay_way == value) return;
                fpay_way = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 工单号'
        /// </summary>
        public string VFwork_order_no
        {
            get { return fwork_order_no; }
            set
            {
                if (fwork_order_no == value) return;
                fwork_order_no = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 
        /// </summary>
        public int VFrecord_state
        {
            get { return frecord_state; }
            set
            {
                if (frecord_state == value) return;
                frecord_state = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 船公司'
        /// </summary>
        public string VFship_company
        {
            get { return fship_company; }
            set
            {
                if (fship_company == value) return;
                fship_company = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 干线船名'
        /// </summary>
        public string VFship_main_ship_name
        {
            get { return fship_main_ship_name; }
            set
            {
                if (fship_main_ship_name == value) return;
                fship_main_ship_name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 干线航次'
        /// </summary>
        public string VFship_main_line_no
        {
            get { return fship_main_line_no; }
            set
            {
                if (fship_main_line_no == value) return;
                fship_main_line_no = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 运单号'
        /// </summary>
        public string VFship_trans_no
        {
            get { return fship_trans_no; }
            set
            {
                if (fship_trans_no == value) return;
                fship_trans_no = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 起始拖车'
        /// </summary>
        public string VFstart_trail_car
        {
            get { return fstart_trail_car; }
            set
            {
                if (fstart_trail_car == value) return;
                fstart_trail_car = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 装货日期'
        /// </summary>
        public DateTime VFhold_goods_date
        {
            get { return fhold_goods_date; }
            set
            {
                if (fhold_goods_date == value) return;
                fhold_goods_date = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 目的拖车'
        /// </summary>
        public string VFto_trail_car
        {
            get { return fto_trail_car; }
            set
            {
                if (fto_trail_car == value) return;
                fto_trail_car = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 柜号/封号'
        /// </summary>
        public string VFcabinet_no
        {
            get { return fcabinet_no; }
            set
            {
                if (fcabinet_no == value) return;
                fcabinet_no = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 20'
        /// </summary>
        public int VF20th
        {
            get { return f20th; }
            set
            {
                if (f20th == value) return;
                f20th = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 40'
        /// </summary>
        public int VF40th
        {
            get { return f40th; }
            set
            {
                if (f40th == value) return;
                f40th = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 40hq'
        /// </summary>
        public int VF40th_hq
        {
            get { return f40th_hq; }
            set
            {
                if (f40th_hq == value) return;
                f40th_hq = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// viewMode子属性 - 装货 - 状态，从低到高分别为： 装货完成(0/1)
        /// </summary>
        public int VFhold_state
        {
            get { return fhold_state; }
            set
            {
                if (fhold_state == value) return;
                fhold_state = value;
                OnPropertyChanged();
            }
        }

        bool isSelected;
        public string IsSelected
        {
            get { return DataConvetor.ConvertBoolToStrSeen(isSelected); }
            set
            {
                var val = DataConvetor.ConvertStrBoolToVal(value);
                if (isSelected == val) return;
                isSelected = val;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
