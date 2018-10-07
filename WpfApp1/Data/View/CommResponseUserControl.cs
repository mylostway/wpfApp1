using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;

using WpfApp1.Data;
using WpfApp1.Data.Test;
using WpfApp1.Data.NDAL;
using WpfApp1.Panels;

using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.Data.dto;
using WL_OA.BLL.query;
using WL_OA.NET;


namespace WpfApp1.Data
{
    public static class UserControlEx     
    {
        public static NetHandleResult GetEntityListResponseCommHandler<T>(this UserControl control,SimpleProtocolStruct response, IPEndPoint endpoint)
             where T : BaseEntity<int>, new()
        {
            var handleResult = JsonHelper.DeserializeTo<NetHandleResult>(response.ResponseData);

            control.Dispatcher.BeginInvoke(new Action<NetHandleResult>((result) => {

                if (result.ResultCode == NetHandleResultCode.Succeed)
                {
                    var queryResult = JsonHelper.DeserializeTo<QueryResult<IList<T>>>(result.RetObject.ToString());

                    if (queryResult.ResultCode != 0)
                    {
                        MessageBox.Show(string.Format("服务处理失败，原因:{0}", queryResult.RetMsg));
                        return;
                    }

                    var entityList = queryResult.ResultData;

                    if (null == entityList)
                    {
                        MessageBox.Show(string.Format("服务处理失败，原因:返回结果不是 List<DriverinfoEntity>，type = {0}", result.RetObject.GetType().Name));
                        return;
                    }

                    var viewModeList = entityList;

                    var pageViewMode = new PaggingViewMode<T>(viewModeList);

                    control.DataContext = pageViewMode;
                }
                else
                {
                    MessageBox.Show(string.Format("后台请求：{0}调用失败，原因:{1}", response, result));
                }

            }), DispatcherPriority.DataBind, new object[] { handleResult });

            return null;
        }


        public static NetHandleResult AddEntityResponseCommHandler<T>(this UserControl control, SimpleProtocolStruct response, IPEndPoint endpoint)
             where T : BaseEntity<int>, new()
        {
            var handleResult = JsonHelper.DeserializeTo<NetHandleResult>(response.ResponseData);

            control.Dispatcher.BeginInvoke(new Action<NetHandleResult>((result) => {

                if (result.ResultCode == NetHandleResultCode.Succeed)
                {
                    var opResult = JsonHelper.DeserializeTo<BaseOpResult>(result.RetObject.ToString());

                    if (opResult.ResultCode != QueryResultCode.Succeed)
                    {
                        // 操作失败
                        MessageBox.Show(string.Format("服务处理失败，原因:{0}", opResult.RetMsg));
                        return;
                    }
                    else
                    {
                        // 操作成功
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("后台请求：{0}调用失败，原因:{1}", response, result));
                }

            }), DispatcherPriority.DataBind, new object[] { handleResult });

            return null;
        }
    }
}
