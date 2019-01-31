using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL_OA.Data;
using WL_OA.Data.dto;

namespace WpfApp1.Data
{
    public class FreBusinessSearchPanelMode
    {
        protected string finterflow_state = "";
        /// <summary>
        /// 物流动态 - 未知填什么值'
        /// </summary>
        [Required]
        [MaxLength(50)]
        public virtual string Finterflow_state { get { return finterflow_state; } set { finterflow_state = value; } }

        protected string fconsign_man = "";
        /// <summary>
        /// 托运人'
        /// </summary>
        [MaxLength(30)]
        public virtual string Fconsign_man { get { return fconsign_man; } set { fconsign_man = value; } }

        protected string fstart_wharf = "";
        /// <summary>
        /// 起运码头'
        /// </summary>
        [Required]
        [MaxLength(30)]
        public virtual string Fstart_wharf { get { return fstart_wharf; } set { fstart_wharf = value; } }

        protected string fstart_place = "";
        /// <summary>
        /// 起运地'
        /// </summary>
        [MaxLength(30)]
        public virtual string Fstart_place { get { return fstart_place; } set { fstart_place = value; } }

        protected string fto_place = "";
        /// <summary>
        /// 目的地'
        /// </summary>
        [MaxLength(50)]
        public virtual string Fto_place { get { return fto_place; } set { fto_place = value; } }

        protected string fto_wharf = "";
        /// <summary>
        /// 目的码头'
        /// </summary>
        [Required]
        [MaxLength(30)]
        public virtual string Fto_wharf { get { return fto_wharf; } set { fto_wharf = value; } }

        protected DateTime fbusiness_date = DateTime.Now;
        /// <summary>
        /// 业务日期'
        /// </summary>
        public virtual DateTime Fbusiness_date { get { return fbusiness_date; } set { fbusiness_date = value; } }

        protected string fbusinesser = "";
        /// <summary>
        /// 业务员'
        /// </summary>
        [MaxLength(30)]
        public virtual string Fbusinesser { get { return fbusinesser; } set { fbusinesser = value; } }

        protected string fgoods_name = "";
        /// <summary>
        /// 货名'
        /// </summary>
        [Required]
        [MaxLength(60)]
        public virtual string Fgoods_name { get { return fgoods_name; } set { fgoods_name = value; } }

        protected string fhold_goods_place = "";
        /// <summary>
        /// 装货低点'
        /// </summary>
        [Required]
        [MaxLength(60)]
        public virtual string Fhold_goods_place { get { return fhold_goods_place; } set { fhold_goods_place = value; } }

        protected string fhold_goods_people_phone = "";
        /// <summary>
        /// 装货联系人'
        /// </summary>
        [Required]
        [MaxLength(20)]
        public virtual string Fhold_goods_people_phone { get { return fhold_goods_people_phone; } set { fhold_goods_people_phone = value; } }

        protected string fput_goods_place = "";
        /// <summary>
        /// 卸货低点'
        /// </summary>
        [Required]
        [MaxLength(60)]
        public virtual string Fput_goods_place { get { return fput_goods_place; } set { fput_goods_place = value; } }

        protected string fput_goods_people_phone = "";
        /// <summary>
        /// 卸货联系人'
        /// </summary>
        [Required]
        [MaxLength(20)]
        public virtual string Fput_goods_people_phone { get { return fput_goods_people_phone; } set { fput_goods_people_phone = value; } }

        protected string fop_term = "";
        /// <summary>
        /// 操作条款'
        /// </summary>
        [Required]
        [MaxLength(20)]
        public virtual string Fop_term { get { return fop_term; } set { fop_term = value; } }

        protected string ftransit_term = "";
        /// <summary>
        /// 运输条款'
        /// </summary>
        [Required]
        [MaxLength(20)]
        public virtual string Ftransit_term { get { return ftransit_term; } set { ftransit_term = value; } }

        protected string fpay_way = "";
        /// <summary>
        /// 付款方式'
        /// </summary>
        [Required]
        [MaxLength(20)]
        public virtual string Fpay_way { get { return fpay_way; } set { fpay_way = value; } }

        protected string fwork_order_no = "";
        /// <summary>
        /// 工单号'
        /// </summary>
        [Required]
        [MaxLength(40)]
        public virtual string Fwork_order_no { get { return fwork_order_no; } set { fwork_order_no = value; } }

        protected int frecord_state = 0;
        /// <summary>
        /// 
        /// </summary>
        public virtual int Frecord_state { get { return frecord_state; } set { frecord_state = value; } }

        protected string fship_company = "";
        /// <summary>
        /// 船公司'
        /// </summary>
        [Required]
        [MaxLength(40)]
        public virtual string Fship_company { get { return fship_company; } set { fship_company = value; } }

        protected string fmain_line_ship_name = "";
        /// <summary>
        /// 干线船名'
        /// </summary>
        [Required]
        [MaxLength(40)]
        public virtual string Fmain_line_ship_name { get { return fmain_line_ship_name; } set { fmain_line_ship_name = value; } }

        protected string fship_main_line_no = "";
        /// <summary>
        /// 干线航次'
        /// </summary>
        [Required]
        [MaxLength(40)]
        public virtual string Fship_main_line_no { get { return fship_main_line_no; } set { fship_main_line_no = value; } }

        protected string fship_trans_no = "";
        /// <summary>
        /// 运单号'
        /// </summary>
        [MaxLength(50)]
        public virtual string Fship_trans_no { get { return fship_trans_no; } set { fship_trans_no = value; } }

        protected string fstart_trail_car = "";
        /// <summary>
        /// 起始拖车'
        /// </summary>
        [MaxLength(50)]
        public virtual string Fstart_trail_car { get { return fstart_trail_car; } set { fstart_trail_car = value; } }

        protected DateTime fhold_goods_date = DateTime.Now;
        /// <summary>
        /// 装货日期'
        /// </summary>
        public virtual DateTime Fhold_goods_date { get { return fhold_goods_date; } set { fhold_goods_date = value; } }

        protected string fto_trail_car = "";
        /// <summary>
        /// 目的拖车'
        /// </summary>
        [MaxLength(50)]
        public virtual string Fto_trail_car { get { return fto_trail_car; } set { fto_trail_car = value; } }

        protected string fcabinet_no = "";
        /// <summary>
        /// 柜号/封号'
        /// </summary>
        [MaxLength(50)]
        public virtual string Fcabinet_no { get { return fcabinet_no; } set { fcabinet_no = value; } }

        protected int f20th = 0;
        /// <summary>
        /// 20'
        /// </summary>
        public virtual int F20th { get { return f20th; } set { f20th = value; } }

        protected int f40th = 0;
        /// <summary>
        /// 40'
        /// </summary>
        public virtual int F40th { get { return f40th; } set { f40th = value; } }

        protected int f40th_hq = 0;
        /// <summary>
        /// 40hq'
        /// </summary>
        public virtual int F40th_hq { get { return f40th_hq; } set { f40th_hq = value; } }

        protected int fhold_state = 0;
        /// <summary>
        /// 装货 - 状态，从低到高分别为： 装货完成(0/1)
        /// </summary>
        public virtual int Fhold_state { get { return fhold_state; } set { fhold_state = value; } }

        /// <summary>
        /// 从服务器端获取的源数据
        /// </summary>
        public FreBussinessOpCenterDTO SrcData { get; set; } = null;

        public FreBusinessSearchPanelMode() { }
        
        public FreBusinessSearchPanelMode(FreBussinessOpCenterDTO dto)
        {
            SrcData = dto;

            var orderInfo = dto.OrderInfo;
            if(null != orderInfo)
            {
                Fconsign_man = orderInfo.Fconsign_man;
                Fstart_wharf = orderInfo.Fstart_wharf;
                Fstart_place = orderInfo.Fstart_place;
                Fto_place = orderInfo.Fto_place;
                Fto_wharf = orderInfo.Fto_wharf;
                Fbusiness_date = orderInfo.Fbusiness_date;
                Fbusinesser = orderInfo.Fbusinesser;
                Fop_term = EnumHelper.ValToName<FreBusinessTransportTermsEnums>(orderInfo.Fop_term);
                Ftransit_term = EnumHelper.ValToName<FreBusinessTransportTermsEnums>(orderInfo.Ftransit_term);
                Fpay_way = EnumHelper.ValToName<FreBusinessPaymentTypeEnums>(orderInfo.Fpay_way);
                Fwork_order_no = orderInfo.Flist_id;
            }            

            // FIXME：待确认该值
            Frecord_state = 0;

            var hgi = dto.HoldGoodsInfo;
            if(null != hgi)
            {
                Fgoods_name = hgi.Fgoods_name;
                Fhold_goods_place = hgi.Fhold_goods_place;
                Fhold_goods_people_phone = hgi.Fhold_goods_people_phone;
                Fstart_trail_car = hgi.Fstart_trailer;
                Fhold_goods_date = hgi.Fhold_date;
            }
                       

            var lgi = dto.LayGoodsInfo;
            if(null != lgi)
            {
                Fput_goods_place = lgi.Flay_goods_place;
                Fput_goods_people_phone = lgi.Flay_goods_people_phone;
                Fto_trail_car = lgi.Ftarget_trailer;
            }            

            var seaInfo = dto.SeaTransportInfo;
            if(null != seaInfo)
            {
                Fship_company = seaInfo.Fship_company;
                Fmain_line_ship_name = seaInfo.Fmain_line_ship_name;
                Fship_main_line_no = seaInfo.Fmain_line_no;
                Fship_trans_no = seaInfo.Fship_no;
            }            

            // FIXME：这里待确认，是每个cabinet一行记录（即一个FreBussinessOpCenterDTO可以生成多条FreBusinessSearchPanelMode），还是只取其中一条？
            Fcabinet_no = "";
            F20th = 0;
            F40th = 0;
            F40th_hq = 0;

            // FIXME：状态这里待确认
            Fhold_state = 0;
        }
        
    }
}
