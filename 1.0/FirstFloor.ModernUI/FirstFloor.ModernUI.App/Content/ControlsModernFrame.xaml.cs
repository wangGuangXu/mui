using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace FirstFloor.ModernUI.App.Content
{
    /// <summary>
    /// Interaction logic for ControlsModernFrame.xaml
    /// </summary>
    public partial class ControlsModernFrame : UserControl
    {
        private string eventLogMessage;

        public ControlsModernFrame()
        {
            InitializeComponent();

            this.TextEvents.Text = eventLogMessage;
        }

        /// <summary>
        /// 日志信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="o"></param>
        private void LogMessage(string message, params object[] o)
        {
            message = string.Format(CultureInfo.CurrentUICulture, message, o);

            if (this.TextEvents == null)
            {
                this.eventLogMessage += message;
            }
            else
            {
                this.TextEvents.AppendText(message);
            }
        }

        private void Frame_FragmentNavigation(object sender, FragmentNavigationEventArgs e)
        {
            LogMessage("片段进行导航 FragmentNavigation: {0}\r\n", e.Fragment);
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            LogMessage("已导航 Navigated: [{0}] {1}\r\n", e.NavigationType, e.Source);
        }

        private void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            LogMessage("正在导航中 Navigating: [{0}] {1}\r\n", e.NavigationType, e.Source);
        }

        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            LogMessage("导航失败 NavigationFailed: {0}\r\n", e.Error.Message);
        }
    }
}
