using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var reader = new SpeechSynthesizer();
            reader.SpeakAsync("现代WPFUI框架启动 ");

            DispatcherUnhandledException += App_DispatcherUnhandledException;               //UI线程异常
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; //非UI线程异常
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException; //Task线程内异常

            base.OnStartup(e);

        }

        #region 异常处理
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ModernDialog.ShowMessage(string.Format("发生App_DispatcherUnhandledException异常，错误信息：{0}", e.Exception.ToString()), "提示", MessageBoxButton.OK);
        }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ModernDialog.ShowMessage(string.Format("发生Current_DispatcherUnhandledException异常，错误信息：{0}", e.Exception.ToString()), "提示", MessageBoxButton.OK);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ModernDialog.ShowMessage(string.Format("发生CurrentDomain_UnhandledException异常，错误信息：{0}", e.ExceptionObject.ToString()), "提示", MessageBoxButton.OK);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            ModernDialog.ShowMessage(string.Format("发生TaskScheduler_UnobservedTaskException异常，错误信息：{0}", e.Exception.ToString()), "提示", MessageBoxButton.OK);
        }
        #endregion

        //Todo FirstFloor.ModernUI\Themes\ModernDialog.xaml 中 TextBlock 元素的大小写转换不需要，去除转换大小写，标题不显示

    }
}