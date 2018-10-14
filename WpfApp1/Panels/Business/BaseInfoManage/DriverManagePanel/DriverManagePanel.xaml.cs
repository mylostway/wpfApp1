using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

using WpfApp1.Data;
using WpfApp1.Data.Test;
using WpfApp1.Data.NDAL;
using WpfApp1.Data.View;
using WpfApp1.Panels.extend_control;
using WpfApp1.Panels.Business.BaseInfoManage;

using WL_OA.Data.entity;
using WL_OA.Data.param;
using WL_OA.Data.dto;
using WL_OA.BLL.query;
using WL_OA.NET;

using MaterialDesignThemes.Wpf;

namespace WpfApp1.Panels.business
{
    /// <summary>
    /// GoodsInfoManagePanel.xaml 的交互逻辑
    /// </summary>
    public partial class DriverManagePanel : UserControl
    {       
        public DriverManagePanel()
        {
            InitializeComponent();

            if (FirstInit) btn_search_Click(null,null);            
        }

        private bool FirstInit = true;
        
        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var queryParam = new QueryDriverInfoParams();
            queryParam.FName = tbx_searchName.Text;
            queryParam.Fphone = tbx_searchPhone1.Text;
            queryParam.Take = 5;

            //NetworkDAL.RequestAsync("DriverInfoBLL_GetEntityList",
            //    queryParam, new NetHandler(this.GetEntityListResponseCommHandler<DriverinfoEntityViewMode>));

            NHttpClientDAL.PostAsync("api/Datas/AddDriverInfo", 
                queryParam, new HttpResponseHandler(this.CommOpResponseCommHandler<BaseOpResult>));
        }


        public ICommand AddEntityDialogCommand => new AnotherCommandImplementation(ExecuteAddEntityDialog);


        private async void ExecuteAddEntityDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddDriverInfoPanel
            {
                //DataContext = new SampleDialogViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }


        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");
        }

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var ret = (new AddTest()).ShowDialog();

            if(ret.Value)
            {
                MessageBox.Show("添加Entity中。。");
            }
            else
            {
                MessageBox.Show("取消操作");
            }
        }
    }
}
