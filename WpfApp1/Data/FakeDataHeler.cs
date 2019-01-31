using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WL_OA.Data;
using WL_OA.Data.dto;
using WL_OA.NET;
using WpfApp1.Data.NDAL;

using WL_OA.Data.entity;
using System.ComponentModel.DataAnnotations;

namespace WpfApp1.Data
{
    public sealed class ClientFakeDataHelper : FakeDataHelper
    {
        private ClientFakeDataHelper() { }

        public static new ClientFakeDataHelper Instance = new ClientFakeDataHelper();

        /// <summary>
        /// 单机测试数据源（随机制作测试数据）
        /// </summary>
        /// <param name="genNum"></param>
        /// <returns></returns>
        public HttpResponse CreateFakeDataNetResponse(Type type, int genNum = FakeDataHelerSettings.DEFAULT_GEN_DATA_NUM)
        {
            var genData = CreateFakeDataCollection(type, genNum).ToList();
            var queryResult = new QueryResult<IList<object>>(genData);
            var rsp = new HttpResponse(JsonHelper.SerializeTo(queryResult));
            return rsp;
        }


    }

}
