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
using WL_OA.Data;

namespace WpfApp1.Data
{
    public static partial class UserControlEx     
    {
        public static void GetEntityListResponseCommHandler<T>(this UserControl control, HttpResponse res,object context)
            where T : BaseEntity<int>, new()
        {
            control.Dispatcher.BeginInvoke(new Action<HttpResponse>((result) => {
                try
                {
                    if(null == result)
                    {
                        MessageBox.Show(string.Format("服务处理失败，应答数据为空！"));
                        return;
                    }

                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var queryResult = JsonHelper.DeserializeTo<QueryResult<IList<T>>>(result.ResponseContent);

                        if (queryResult.ResultCode != 0)
                        {
                            MessageBox.Show(string.Format("服务处理失败，原因:{0}", queryResult.RetMsg));
                            return;
                        }

                        var entityList = queryResult.ResultData;

                        if (null == entityList)
                        {
                            MessageBox.Show(string.Format("服务处理失败，原因:返回结果不是 List<T>"));
                            return;
                        }
                        //return;
                        var viewModeList = entityList;

                        var pageViewMode = new PaggingViewMode<T>(viewModeList);

                        control.DataContext = pageViewMode;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("后台请求：调用失败，原因:{0}{1}", result.StatusCode, result.ResponseMsg));
                        return;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(string.Format("处理UI出错，msg:{0}",ex.Message));
                }
            }), DispatcherPriority.DataBind, new object[] { res });
        }

        public static void CommOpResponseCommHandler<T>(this UserControl control, HttpResponse response, object context)
             where T : BaseOpResult
        {
            control.Dispatcher.BeginInvoke(new Action<HttpResponse>((result) => {

                if (null == result)
                {
                    MessageBox.Show(string.Format("服务处理失败，应答数据为空！"));
                    return;
                }

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var opResult = JsonHelper.DeserializeTo<BaseOpResult>(result.ResponseContent);

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
                    MessageBox.Show(string.Format("后台请求：调用失败，原因:{0}{1}", result.StatusCode,result.ResponseMsg));
                    return;
                }

            }), DispatcherPriority.DataBind, new object[] { response });
        }

    }



}
