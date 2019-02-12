using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WL_OA.Data.utils;
using WpfApp1.Data;
using WpfApp1.Data.NDAL;

namespace WpfApp1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //UI线程未捕获异常处理事件
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            InitAppDatas();

            Activated += App_Activated;

            Exit += App_Exit;

            // FIXME：这里配置文件路径需要确认
            var cfgPath = $"{Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)}/Configs/log4net.config";
            XmlConfigurator.Configure(new FileInfo(cfgPath));
        }

        private void App_Activated(object sender, EventArgs e)
        {
            
        }

        
        private void App_Exit(object sender, ExitEventArgs e)
        {
            ServerDatas.Exit();
            //throw new NotImplementedException();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(string.Format("系统出错了，非UI线程未捕获异常处理事件，原因：{0}", e.ExceptionObject.ToString()));
            SLogger.Err(e.ExceptionObject.ToString());
            Application.Current.Shutdown(-1);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            MessageBox.Show(string.Format("系统出错了，Task线程内未捕获异常处理事件，原因：{0}", e.Exception.Message));
            SLogger.Err(e.Exception.ToString(), e.Exception);
            Application.Current.Shutdown(-1);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(string.Format("系统出错了，UI线程未捕获异常处理事件，原因：{0}", e.Exception.Message));
            //e.Handled = true;
            SLogger.Err(e.Exception.ToString(), e.Exception);
            Application.Current.Shutdown(-1);
        }


        private void InitAppDatas()
        {
            ServerDatas.Init();

            // 测试代码，必须，以后替换成登录初始化
            NetClientSession.BuildNetSession("管理员", "abcdefg123");
        }
    }
}
