using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net;
using System.Windows.Threading;

using WpfApp1.Data;
using WpfApp1.Data.NDAL;
using WpfApp1.Panels.functional;
using WpfApp1.Panels.Business.BusinessOp.BusinessOpCenterPanel;

using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.Data.dto;
using WL_OA.BLL.query;
using WL_OA.NET;
using WL_OA.Data;
using WL_OA.Data.utils;

using MaterialDesignThemes.Wpf;


namespace WpfApp1.Panels.business
{
    /// <summary>
    /// BusinessOpCenterPanel.xaml 的交互逻辑
    /// </summary>
    public partial class BusinessOpCenterPanel : UserControl
    {
        public BusinessOpCenterPanel()
        {
            InitializeComponent();

            this.cbx_searchDateType1.BindComboxToEnums<FreBusinessSearchTimeTypeEnums>();
            this.cbx_searchIDType1.BindComboxToEnums<FreBusinessSearchDtoObjectTypeEnums>();
            this.cbx_searchStatue1.BindComboxToEnums<FreBusinessSearchDtoStatusTypeEnums>();
        }


        public string StrBeginDate1 = "起始日期";

        public string StrEndDate1 = "结束日期";


        private void FreBusinessSearchResultResponseHandler<T>(HttpResponse res, object context)
            where T : FreBussinessOpCenterDTO
        {
            Dispatcher.BeginInvoke(new Action<HttpResponse>((result) =>
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
                        var queryResult = JsonHelper.DeserializeTo<QueryResult<IList<FreBussinessOpCenterDTO>>>(result.ResponseContent);

                        if (null == queryResult)
                        {
                            strHandleMsg = $"服务器应答结果为空，系统异常，请联系管理员";
                            WaitingDialog.ChangeStateMsg(strHandleMsg);
                            SLogger.Err(res.ToString());
                            return;
                        }

                        if (queryResult.ResultCode != 0)
                        {
                            strHandleMsg = $"服务处理失败，原因:{queryResult.RetMsg}";
                            WaitingDialog.ChangeStateMsg(strHandleMsg);
                            SLogger.Err(res.ToString());
                            return;
                        }

                        var entityList = queryResult.ResultData;
                        if (null == entityList)
                        {
                            //strHandleMsg = $"返回结果不是数据列表，原因:{queryResult.RetMsg}";
                            //WaitingDialog.ChangeStateMsg(strHandleMsg);
                            //SLogger.Err(res.ToString());
                            //return;

                            // 返回结果为空
                            entityList = new List<FreBussinessOpCenterDTO>();
                        }

                        var showSearchResultList = new List<FreBusinessSearchPanelMode>();
                        foreach (var e in entityList)
                        {
                            showSearchResultList.Add(new FreBusinessSearchPanelMode(e));
                        }
                        var pageViewMode = new PaggingViewMode<FreBusinessSearchPanelMode>(
                            showSearchResultList, queryResult.ResultCount, StartSearch);
                        this.DataContext = pageViewMode;

                        this.Pagger.Init(queryResult.ResultCount, StartSearch);

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


        private void StartSearch(int pageIdx = 0, int pageSize = -1)
        {
            var queryParam = new QueryFreBusinessCenterParam();
            queryParam.StartDate = dp_startDate.GetDateTimeVal();
            queryParam.EndDate = dp_endDate.GetDateTimeVal();
            queryParam.ListID = tbx_searchID.Text;

            if (pageSize > 0)
            {
                queryParam.Take = pageSize;
            }

            if (pageIdx > 0)
            {
                queryParam.IsAllowPagging = true;
                queryParam.Skip = queryParam.Take * (pageIdx - 1);
            }

            this.PostAsync("api/GetFreBusinessList", queryParam,
                new HttpResponseHandler(FreBusinessSearchResultResponseHandler<FreBussinessOpCenterDTO>));
        }


        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            StartSearch();
        }

        private void ResetSearch()
        {
            tbx_searchID.Text = "";
            dp_startDate.Text = "";//DateTimeHelper.GetDayStringOnAddingDays();
            dp_endDate.Text = "";//DateTimeHelper.GetDayStringOnAddingDays(1);
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            ResetSearch();
        }

        private async void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EditBusinessListPanel();
            var result = (bool)await dialog.SmothShow();
            if (result)
            {
                var editInfo = dialog.EditInfo;
                if (null != editInfo)
                {
                    this.PostAsync("api/AddFreBusiness", editInfo,
                        new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }
        }


        private async void grid_data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null == this.grid_data.SelectedItem) return;
            var selValue = this.grid_data.SelectedItem as FreBusinessSearchPanelMode;
            SAssert.MustTrue(null != selValue, string.Format("绑定数据异常！"));

            var dialog = new EditBusinessListPanel();
            var toUpdateData = new FreBussinessOpCenterDTO(selValue.SrcData);
            dialog.Init(toUpdateData);
            var result = await dialog.SmothShow();
            if (result)
            {
                var editInfo = dialog.EditInfo;
                if (null != editInfo)
                {
                    toUpdateData.Flist_id = toUpdateData.OrderInfo.Flist_id;
                    selValue.SrcData.FixUpdateResult(toUpdateData);
                    if (!toUpdateData.IsNullOrEmpty())
                    {
                        this.PostAsync("api/UpdateFreBusiness", toUpdateData,
                            new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                    }
                }
            }
        }
    }
}
