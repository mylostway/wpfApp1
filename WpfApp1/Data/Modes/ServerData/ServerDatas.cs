using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL_OA.Data.entity;
using WpfApp1.Data.NDAL;

namespace WpfApp1.Data
{
    /// <summary>
    /// 进程一启动就到Server获取的数据
    /// </summary>
    public class ServerDatas
    {
        static ServerDatas()
        {
            Init();
        }

        static void Init()
        {
            var taskList = new List<Task>();

            taskList.Add(NHttpClientDAL.PostAsync<DriverinfoEntity>("api/QueryDriverInfoList", null, new HttpResponseHandler((res, context) =>
            {
                res.ServerDataGetEntityListCommHandler(out ServerDriverinfoList);
            })));

            taskList.Add(NHttpClientDAL.PostAsync<GoodsinfoEntity>("api/QueryGoodsInfoList", null, new HttpResponseHandler((res, context) =>
            {
                res.ServerDataGetEntityListCommHandler(out ServerGoodsinfoList);
            })));

            Task.WaitAll(taskList.ToArray());
        }

        /// <summary>
        /// 服务器已录入的司机列表
        /// </summary>
        public static IList<DriverinfoEntity> ServerDriverinfoList = null;

        /// <summary>
        /// 服务器已录入的商品信息列表
        /// </summary>
        public static IList<GoodsinfoEntity> ServerGoodsinfoList = null;


        /// <summary>
        /// 服务器人员信息列表
        /// </summary>
        public static IList<SystemUserEntity> ServerUserList = null;

    }
}
