using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;

namespace FirstFloor.ModernUI.App.Content
{
    /// <summary>
    /// ControlsModernGrowlNotification.xaml 的交互逻辑
    /// </summary>
    public partial class ControlsModernGrowlNotification : UserControl
    {
        private const double topOffset = 20;
        private const double leftOffset = 380;
        //readonly FirstFloor.ModernUI.Windows.Controls.ModernGrowlNotification growlNotifications = new FirstFloor.ModernUI.Windows.Controls.ModernGrowlNotification();
        readonly ModernGrowlNotification growlNotifications = new ModernGrowlNotification();

        public ControlsModernGrowlNotification()
        {
            InitializeComponent();
            //growlNotifications.NotificationControlSource = growlNotifications.notifications;
            growlNotifications.Top = SystemParameters.WorkArea.Top + topOffset;
            growlNotifications.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - leftOffset;
            this.Unloaded += ControlsModernGrowlNotification_Unloaded;
        }

        private void ControlsModernGrowlNotification_Unloaded(object sender, RoutedEventArgs e)
        {
            growlNotifications.Close();
        }

        private void btnSuccess_Click(object sender, RoutedEventArgs e)
        {
            growlNotifications.AddNotify(new Notification { Title = "今天的天气不错", Icon = "pack://application:,,,/Resources/notification-icon.png", Message = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." });
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            growlNotifications.AddNotify(new Notification { Title = "文件保存成功", Icon = "pack://application:,,,/Resources/microsoft-windows-8-logo.png", Message = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." });
        }

        private void btnWarning_Click(object sender, RoutedEventArgs e)
        {
            growlNotifications.AddNotify(new Notification { Title = "磁盘空间快要满了", Icon = "pack://application:,,,/Resources/facebook-button.png", Message = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." });
        }

        private void btnError_Click(object sender, RoutedEventArgs e)
        {
            growlNotifications.AddNotify(new Notification { Title = "连接失败,请检查网络", Icon = "pack://application:,,,/Resources/Radiation_warning_symbol.png", Message = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." });
        }

        private void btnFatal_Click(object sender, RoutedEventArgs e)
        {
            growlNotifications.AddNotify(new Notification { Title = "程序已崩溃", Icon = "pack://application:,,,/Resources/notification-icon.png", Message = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." });
        }

        private void btnAsk_Click(object sender, RoutedEventArgs e)
        {
            growlNotifications.AddNotify(new Notification { Title = "检测到有新版本是否更新？", Icon = "pack://application:,,,/Resources/notification-icon.png", Message = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." });
        }


    }
}
