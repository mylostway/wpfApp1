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

using WpfApp1.Data.NDAL;
using WpfApp1.Panels;

using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.Data.dto;
using WL_OA.BLL.query;
using WL_OA.NET;
using WL_OA.Data;
using WpfApp1.Panels.functional;

namespace WpfApp1.Data
{
    public static partial class UserControlEx     
    {
        /// <summary>
        /// 最简单的Http请求（查询类）应答处理函数，需要自己扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="res"></param>
        /// <param name="context"></param>
        public static void GetEntityListResponseCommHandler<T>(this Control control, HttpResponse res,object context)
            where T : BaseEntity<int>, new()
        {
            control.Dispatcher.BeginInvoke(new Action<HttpResponse>((result) => {
                try
                {
                    var requestUrl = res.RequestMessage?.RequestUri;

                    if (null == result)
                    {
                        WaitingDialog.ChangeStateMsg(string.Format("服务处理{0}失败，应答数据为空！", requestUrl?.AbsolutePath));
                        return;
                    }

                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var queryResult = JsonHelper.DeserializeTo<QueryResult<IList<T>>>(result.ResponseContent);

                        if (queryResult.ResultCode != 0)
                        {                            
                            WaitingDialog.ChangeStateMsg(string.Format("服务处理失败，原因:{0}", queryResult.RetMsg));
                            return;
                        }

                        var entityList = queryResult.ResultData;

                        if (null == entityList)
                        {
                            WaitingDialog.ChangeStateMsg(string.Format("服务处理失败，返回结果不是数据列表，原因:{0}", queryResult.RetMsg));
                            return;
                        }
                        
                        var viewModeList = entityList;

                        var pageViewMode = new PaggingViewMode<T>(viewModeList);

                        control.DataContext = pageViewMode;

                        WaitingDialog.Hide();
                    }
                    else
                    {                        
                        WaitingDialog.ChangeStateMsg(string.Format("后台请求：调用失败，原因:{0}{1}", result.StatusCode, result.ResponseMsg));
                        return;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(string.Format("处理UI出错，msg:{0}",ex.Message));
                }
            }), DispatcherPriority.DataBind, new object[] { res });
        }

        

        /// <summary>
        /// 最简单的Http请求（操作类）应答处理函数，需要自己扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="response"></param>
        /// <param name="context"></param>
        public static void CommOpResponseCommHandler<T>(this Control control, HttpResponse response, 
            object context)
             where T : BaseOpResult
        {
            control.Dispatcher.BeginInvoke(new Action<HttpResponse>((result) => {

                var requestUrl = response.RequestMessage?.RequestUri;                

                if (null == result)
                {
                    WaitingDialog.ChangeStateMsg(string.Format("服务处理{0}失败，应答数据为空！", requestUrl?.AbsolutePath));
                    //MessageBox.Show(string.Format("服务处理{0}失败，应答数据为空！", requestUrl?.AbsolutePath));
                    return;
                }

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var opResult = JsonHelper.DeserializeTo<T>(result.ResponseContent);

                    if (opResult.ResultCode != QueryResultCode.Succeed)
                    {
                        // 操作失败
                        //MessageBox.Show(string.Format("服务{0}处理失败，原因:{1}", requestUrl?.AbsolutePath, opResult.RetMsg));
                        WaitingDialog.ChangeStateMsg(string.Format("服务{0}处理失败，原因:{1}", requestUrl?.AbsolutePath, opResult.RetMsg));
                        return;
                    }
                    else
                    {
                        // 操作成功
                        var succeedMsg = opResult.RetMsg;
                        WaitingDialog.ChangeStateMsg(succeedMsg);
                    }
                }
                else
                {
                    //MessageBox.Show(string.Format("后台请求{0}调用失败，原因:{1}{2}", requestUrl?.AbsolutePath,result.StatusCode,result.ResponseMsg));
                    WaitingDialog.ChangeStateMsg(string.Format("后台请求{0}调用失败，原因:{1}{2}", requestUrl?.AbsolutePath, result.StatusCode, result.ResponseMsg));
                    return;
                }

            }), DispatcherPriority.DataBind, new object[] { response });
        }

    }



}
