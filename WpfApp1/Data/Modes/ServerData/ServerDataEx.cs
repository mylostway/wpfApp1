using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WL_OA.Data;
using WL_OA.Data.dto;
using WL_OA.Data.entity;
using WpfApp1.Data.NDAL;

namespace WpfApp1.Data
{
    public static class ServerDataEx
    {
        public static void ServerDataGetEntityListCommHandler<T>(this HttpResponse res,
            out IList<T> retList)
            where T : BaseEntity<int>, new()
        {
            if (null != res)
            {
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var queryResult = JsonHelper.DeserializeTo<QueryResult<IList<T>>>(res.ResponseContent);

                    if (queryResult.ResultCode == 0)
                    {
                        //WaitingDialog.ChangeStateMsg(string.Format("服务处理失败，原因:{0}", queryResult.RetMsg));
                        retList = queryResult.ResultData;
                        return;
                    }
                }               
            }

            retList = new List<T>();
            return;        
        }
    }
}
