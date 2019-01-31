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
using WL_OA.Data.utils;

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
        public static void GetEntityListResponseCommHandler<T>(this Control control, HttpResponse res, object context)
            where T : class, new()
        {
            control.Dispatcher.BeginInvoke(new Action<HttpResponse>((result) =>
            {
                var strHandleMsg = "";
                try
                {
                    var requestUrl = res.RequestMessage?.RequestUri;

                    if (null == result)
                    {
                        strHandleMsg = string.Format("服务处理{0}失败，应答数据为空！", requestUrl?.AbsolutePath);
                        WaitingDialog.ChangeStateMsg(strHandleMsg);
                        SLogger.Err(res.ToString());
                        return;
                    }

                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var queryResult = JsonHelper.DeserializeTo<QueryResult<IList<T>>>(result.ResponseContent);

                        if (null == queryResult)
                        {
                            strHandleMsg = $"服务器应答结果为空，系统异常，请联系管理员";
                            WaitingDialog.ChangeStateMsg(strHandleMsg);
                            SLogger.Err(res.ToString());
                            return;
                        }

                        if (queryResult.ResultCode != 0)
                        {
                            strHandleMsg = string.Format("服务处理失败，原因:{0}", queryResult.RetMsg);
                            WaitingDialog.ChangeStateMsg(strHandleMsg);
                            SLogger.Err(res.ToString());
                            return;
                        }

                        var entityList = queryResult.ResultData;

                        if (null == entityList)
                        {
                            strHandleMsg = string.Format("服务处理失败，返回结果不是数据列表，原因:{0}", queryResult.RetMsg);
                            WaitingDialog.ChangeStateMsg();
                            SLogger.Err(res.ToString());
                            return;
                        }

                        var viewModeList = entityList;

                        var pageViewMode = new PaggingViewMode<T>(viewModeList);

                        control.DataContext = pageViewMode;

                        WaitingDialog.Hide();
                    }
                    else
                    {
                        strHandleMsg = string.Format("后台请求：调用失败，原因:{0}", result.ResponseContent);
                        WaitingDialog.ChangeStateMsg(strHandleMsg);
                        SLogger.Err(res.ToString());
                        return;
                    }
                }
                catch (Exception ex)
                {
                    strHandleMsg = string.Format("软件处理出错，msg:{0}", ex.Message);
                    MessageBox.Show(strHandleMsg);
                    SLogger.Err(res.ToString(), ex);
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
            control.Dispatcher.BeginInvoke(new Action<HttpResponse>((result) =>
            {
                var strHandleMsg = "";
                try
                {
                    var requestUrl = response.RequestMessage?.RequestUri;
                    if (null == result)
                    {
                        strHandleMsg = string.Format("服务处理{0}失败，应答数据为空！", requestUrl?.AbsolutePath);
                        WaitingDialog.ChangeStateMsg(strHandleMsg);
                        SLogger.Err(result.ToString());
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
                            strHandleMsg = string.Format("服务{0}处理失败，原因:{1}", requestUrl?.AbsolutePath, opResult.RetMsg);
                            WaitingDialog.ChangeStateMsg(strHandleMsg);
                            SLogger.Err(result.ToString());
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
                        strHandleMsg = string.Format("后台请求失败，原因:{0}", result.ResponseContent);
                        WaitingDialog.ChangeStateMsg(strHandleMsg);
                        SLogger.Err(result.ToString());
                        return;
                    }
                }
                catch (Exception ex)
                {
                    strHandleMsg = string.Format("软件处理出错，msg:{0}", ex.Message);
                    WaitingDialog.ChangeStateMsg(strHandleMsg);
                    SLogger.Err(result.ToString(), ex);
                    return;
                }
            }), DispatcherPriority.DataBind, new object[] { response });
        }

    }
}
