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
using WL_OA.Data;

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

        private static DispatcherTimer s_timer = null;

        //private static DispatcherTimer s_timer = null;

        const string STR_MSG_HANDLEING = "系统处理中...";

        const string STR_MSG_HANDLE_TIMEOUT = "处理超时，请重试或联系系统管理员...";

        const string STR_MSG_HANDLE_FINISH = "处理完成";

        public static void InitUI(string msg = STR_MSG_HANDLEING)
        {
            if(!string.IsNullOrEmpty(msg)) s_instance.tbx_processingNotice.Text = msg;
            else s_instance.tbx_processingNotice.Text = STR_MSG_HANDLEING;
            s_instance.btn_close.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 显示等候对话框
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="timeOut">等候超时时间（毫秒），小于0则认为不设置超时</param>
        public static void Show(string msg = STR_MSG_HANDLEING, int timeOut = 5000)
        {
            InitUI(msg);

            // 设置超时
            if(timeOut > 0)
            {
                if(null == s_timer) s_timer = new DispatcherTimer(DispatcherPriority.DataBind);
                if (s_timer.IsEnabled) throw new UserFriendlyException("WaitingDialog中重复的计时器", ExceptionScope.System);
                s_timer.Tick += new EventHandler((sender, eArgs) => {
                    s_instance.tbx_processingNotice.Text = STR_MSG_HANDLE_TIMEOUT;
                    s_instance.btn_close.Visibility = Visibility.Visible;
                    //DialogHost.CloseDialogCommand.Execute(null, s_instance);
                });                
                s_timer.Interval = TimeSpan.FromMilliseconds(timeOut);
                s_timer.Start();
            }
            DialogHost.Show(s_instance, "MainDialogHost");
        }

        /// <summary>
        /// 改变等待框状态信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="autoHide"></param>
        public static void ChangeStateMsg(string msg = STR_MSG_HANDLE_FINISH,bool autoHide = false)
        {
            if (s_timer.IsEnabled) s_timer.Stop();
            s_instance.tbx_processingNotice.Text = msg;
            s_instance.btn_close.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// 直接隐藏等待框
        /// </summary>
        public static void Hide()
        {
            if (s_timer.IsEnabled) s_timer.Stop();
            s_instance.Close();
        }


        public void Close()
        {
            DialogHost.CloseDialogCommand.Execute(null, this);
        }
    }
}
