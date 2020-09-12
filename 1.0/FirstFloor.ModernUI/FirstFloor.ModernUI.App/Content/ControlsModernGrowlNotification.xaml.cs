using FirstFloor.ModernUI.App.ViewModels;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FirstFloor.ModernUI.App.Content
{
    /// <summary>
    /// ControlsModernGrowlNotification.xaml 的交互逻辑
    /// </summary>
    public partial class ControlsModernGrowlNotification : UserControl
    {
        private const double topOffset = 40;
        private const double leftOffset = 360;
        readonly ModernGrowlNotification growlNotifications = new ModernGrowlNotification();

        /// <summary>
        /// 距离顶部的位置
        /// </summary>
        private double positionTop;
        /// <summary>
        /// 距离左边的位置
        /// </summary>
        private double positionLeft;
        /// <summary>
        /// 显示区域高度
        /// </summary>
        private double displayHeight;

        public ControlsModernGrowlNotification()
        {
            InitializeComponent();

            this.DataContext = new ModernGrowlViewModel();//"GrowlDemoPanel"
            //在桌面弹出位置计算
            positionTop = SystemParameters.WorkArea.Top + topOffset;
            positionLeft = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - leftOffset;
            displayHeight = SystemParameters.WorkArea.Height - 20;

            ////在系统区域内弹出位置计算
            //positionTop = Application.Current.MainWindow.Top + topOffset;
            //positionLeft = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width - leftOffset;
            //displayHeight = Application.Current.MainWindow.Height - 20;

            growlNotifications.Height = displayHeight;
            growlNotifications.Top = positionTop;
            growlNotifications.Left = positionLeft;
            this.Unloaded += ControlsModernGrowlNotification_Unloaded;
        }

        private void ControlsModernGrowlNotification_Unloaded(object sender, RoutedEventArgs e)
        {
            growlNotifications.Close();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            //"pack://application:,,,/Resources/microsoft-windows-8-logo.png"
            growlNotifications.AddNotify(new Notification("&#xe723;", "今天的天气不错", "#00BCD4", "您收到一笔巨款，请注意查收！您收到一笔巨款，请注意查收！您收到一笔巨款，请注意查收！"));
        }

        private void btnSuccess_Click(object sender, RoutedEventArgs e)
        {
            //pack://application:,,,/Resources/notification-icon.png
            growlNotifications.AddNotify(new Notification("&#xe603;", "文件保存成功", "#2DB84D", "您的文件刚刚保存成功。"));
        }

        private void btnWarning_Click(object sender, RoutedEventArgs e)
        {
            //"pack://application:,,,/Resources/facebook-button.png"
            growlNotifications.AddNotify(new Notification("&#xe62d;", "磁盘空间快要满了", "#e9af20", "请及时清理您的系统盘空间，目前所剩空间不多了。"));
        }

        private void btnError_Click(object sender, RoutedEventArgs e)
        {
            //"pack://application:,,,/Resources/Radiation_warning_symbol.png"
            growlNotifications.AddNotify(new Notification("&#xe61c;", "连接失败,请检查网络", "#DB3340", "当前信号不稳定，网络频繁出现异常。" ));
        }

        //严重
        private void btnFatal_Click(object sender, RoutedEventArgs e)
        {
            //"pack://application:,,,/Resources/notification-icon.png"
            growlNotifications.AddNotify(new Notification("&#xe604;", "程序已崩溃", "#212121", "您有个未处理的异常，导致程序崩溃。" ));
        }

        //询问
        private void btnAsk_Click(object sender, RoutedEventArgs e)
        {
            //"pack://application:,,,/Resources/notification-icon.png"
            growlNotifications.AddNotify(new Notification("&#xe88c;", "检测到有新版本是否更新？", "#F8491E", "收到一条最新软件更新通知，请确认您是否要更新？" ));
        }


    }
}