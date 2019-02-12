using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WL_OA.Data;
using WL_OA.Data.entity;
using WL_OA.Data.param;
using WpfApp1.Data.Modes;
using WpfApp1.Data.NDAL;

namespace WpfApp1.Data
{
    /// <summary>
    /// 进程一启动就到Server获取的数据
    /// </summary>
    public class ServerDatas
    {
        private ServerDatas()
        {
            Init();
        }

        public static void Init()
        {
            // 单机模式不采用向服务器请求设定数据
            if (AppRunConfigs.Instance.IsSingleTestMode) return;

            if (REFERESH_SERVER_DATA_INTERVAL <= 0)
            {
                // 设置为 <= 0则认为后续不需再向Server请求
                Parallel.Invoke(RefereshDriverInfo, RefereshGoodsInfo);
                return;
            }

            RefereshDataTimer = new Timer(new TimerCallback((obj) =>
            {
                Parallel.Invoke(RefereshDriverInfo, RefereshGoodsInfo);
            }), null, 0, REFERESH_SERVER_DATA_INTERVAL);
        }


        public static void Exit()
        {
            if(null != RefereshDataTimer)
            {
                RefereshDataTimer.Dispose();
            }
        }


        //private static ServerDatas Instance = new ServerDatas();

        /// <summary>
        /// 向Server请求数据更新的时间间隔，如果设置为 <= 0则认为后续不需再向Server请求
        /// </summary>
        private static readonly int REFERESH_SERVER_DATA_INTERVAL = AppRunConfigs.Instance.RefereshServerDataIntervalInMs;

        /// <summary>
        /// 定期到Server刷新数据
        /// </summary>
        public static Timer RefereshDataTimer = null;

        /// <summary>
        /// 服务器已录入的司机列表
        /// </summary>
        public static IList<DriverInfoSelectPanelViewMode> ServerDriverinfoList = new List<DriverInfoSelectPanelViewMode>();

        /// <summary>
        /// 服务器已录入的商品信息列表
        /// </summary>
        public static IList<GoodsinfoSelectPanelViewMode> ServerGoodsinfoList = new List<GoodsinfoSelectPanelViewMode>();


        /// <summary>
        /// 服务器人员信息列表
        /// </summary>
        public static IList<SystemUserSelectPanelViewMode> ServerUserList = new List<SystemUserSelectPanelViewMode>();



        /// <summary>
        /// 刷新司机数据
        /// </summary>
        public static async void RefereshDriverInfo()
        {
            var queryParam = new BaseQueryParam() { IsAllowPagging = false };
            await NHttpClientDAL.PostAsync("api/QueryDriverInfoList", queryParam, new HttpResponseHandler((res, context) =>
            {
                res.ServerDataGetEntityListCommHandler(out ServerDriverinfoList);
            }));
        }

        /// <summary>
        /// 刷新商品数据
        /// </summary>
        public static async void RefereshGoodsInfo()
        {
            var queryParam = new BaseQueryParam() { IsAllowPagging = false };
            await NHttpClientDAL.PostAsync("api/QueryGoodsInfoList", queryParam, new HttpResponseHandler((res, context) =>
            {
                res.ServerDataGetEntityListCommHandler(out ServerGoodsinfoList);
            }));
        }




    }
}
