using MaterialDesignThemes.Wpf;
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
using WpfApp1.Panels.Business.BaseInfoManage;

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

            btn_search_Click(null, null);
        }
        
        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryGoodsInfoParam();
            queryParam.FChnName = tbx_searchName.Text;
            queryParam.Fmark = tbx_searchMark.Text;            

            //NetworkDAL.RequestAsync("GoodsInfoBLL_GetEntityList",
            //    queryParam, new NetHandler(this.GetEntityListResponseCommHandler<GoodsinfoEntityViewMode>));

            this.PostAsync("api/QueryGoodsInfoList",
                queryParam, new HttpResponseHandler(this.GetEntityListResponseCommHandler<GoodsinfoEntity>));
        }

        private async void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EditGoodsInfoControl();
            var result = await dialog.SmothShow();
            if (result)
            {
                var addEntity = dialog.EditInfo;
                if (null != addEntity)
                {
                    addEntity.IsValid();
                    this.PostAsync("api/AddGoodsInfo", addEntity, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
                }
            }
        }

        private void btn_modify_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pi_edit_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void pi_del_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
