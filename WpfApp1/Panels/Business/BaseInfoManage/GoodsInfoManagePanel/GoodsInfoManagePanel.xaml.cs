using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WL_OA.Data.dto;
using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.NET;

using WpfApp1.Data;
using WpfApp1.Data.NDAL;
using WpfApp1.Data.Test;

namespace WpfApp1.Panels.business
{
    /// <summary>
    /// GoodsInfoManagePanel.xaml 的交互逻辑
    /// </summary>
    public partial class GoodsInfoManagePanel : UserControl
    {
        public GoodsInfoManagePanel()
        {
            InitializeComponent();

            /*
            var pageViewMode = new PaggingViewMode<GoodsInfoStruct>(
                FakeDataHeler<GoodsInfoStruct>.Instance.CreateFakeDataCollection());

            DataContext = pageViewMode;
            */

            if (FirstInit) btn_search_Click(null, null);
        }

        private bool FirstInit = true;

        //private NetHandleResult GetEntityListResponse(SimpleProtocolStruct response, IPEndPoint endpoint)
        //{
        //    var handleResult = JsonHelper.DeserializeTo<NetHandleResult>(response.ResponseData);

        //    this.Dispatcher.BeginInvoke(new Action<NetHandleResult>((result) => {

        //        if (result.ResultCode == NetHandleResultCode.Succeed)
        //        {
        //            var queryResult = JsonHelper.DeserializeTo<QueryResult<IList<GoodsinfoEntityViewMode>>>(result.RetObject.ToString());

        //            if (queryResult.ResultCode != 0)
        //            {
        //                MessageBox.Show(string.Format("调用失败，原因:{0}", queryResult.RetMsg));
        //                return;
        //            }

        //            var entityList = queryResult.ResultData;

        //            if (null == entityList)
        //            {
        //                MessageBox.Show(string.Format("调用失败，原因:返回结果不是 List<DriverinfoEntity>，type = {0}", result.RetObject.GetType().Name));
        //                return;
        //            }

        //            var viewModeList = entityList;

        //            var pageViewMode = new PaggingViewMode<GoodsinfoEntityViewMode>(viewModeList);

        //            DataContext = pageViewMode;
        //        }
        //        else
        //        {
        //            MessageBox.Show(string.Format("请求：{0}调用失败，原因:{1}", response, result));
        //        }

        //    }), DispatcherPriority.DataBind, new object[] { handleResult });

        //    return null;
        //}


        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryGoodsInfoParam();
            queryParam.FChnName = tbx_searchName.Text;
            queryParam.Fmark = tbx_searchMark.Text;

            NetworkDAL.RequestAsync("GoodsInfoBLL_GetEntityList",
                queryParam, new NetHandler(this.GetEntityListResponseCommHandler<GoodsinfoEntityViewMode>));
        }
    }
}
