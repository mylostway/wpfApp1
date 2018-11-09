using System;
using System.Collections.Generic;
using System.Linq;
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
using MaterialDesignThemes.Wpf;

namespace WpfApp1.Panels.functional
{
    /// <summary>
    /// WaitingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class WaitingDialog : UserControl
    {
        public WaitingDialog()
        {
            InitializeComponent();
        }

        private static WaitingDialog s_instance = new WaitingDialog();

        //private static DispatcherTimer s_timer = null;

        const string STR_MSG_HANDLEING = "系统处理中...";

        const string STR_MSG_HANDLE_TIMEOUT = "处理超时，请重试或联系系统管理员...";

        public static void InitUI(string msg = "")
        {
            if(!string.IsNullOrEmpty(msg)) s_instance.tbx_processingNotice.Text = msg;
            else s_instance.tbx_processingNotice.Text = STR_MSG_HANDLEING;
            s_instance.btn_close.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 显示等候对话框
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="timeOut"></param>
        public static void Show(string msg = "", int timeOut = 3)
        {
            InitUI(msg);

            var s_timer = new DispatcherTimer();
            s_timer.Tick += new EventHandler((sender, eArgs) => {
                s_instance.tbx_processingNotice.Text = STR_MSG_HANDLE_TIMEOUT;
                s_instance.btn_close.Visibility = Visibility.Visible;
                //DialogHost.CloseDialogCommand.Execute(null, s_instance);
            });
            s_timer.Interval = TimeSpan.FromSeconds(timeOut);
            s_timer.Start();
            DialogHost.Show(s_instance, "tabContentDialogHost");
        }

        public static void HideWithMsg(string msg)
        {
            s_instance.tbx_processingNotice.Text = msg;
            s_instance.btn_close.Visibility = Visibility.Visible;
        }

        public void Close()
        {
            DialogHost.CloseDialogCommand.Execute(null, this);
        }
    }
}
