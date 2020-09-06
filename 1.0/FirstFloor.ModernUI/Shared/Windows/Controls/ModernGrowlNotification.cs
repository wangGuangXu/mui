using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Interactivity;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 信息通知
    /// </summary>
    public class ModernGrowlNotification:Window
    {
        /// <summary>
        /// 屏幕最大显示4个通知
        /// </summary>
        private const int maxNotifications = 4;
        /// <summary>
        /// 通知集合
        /// </summary>
        public Notifications notifications = new Notifications();
        /// <summary>
        /// 缓冲区待显示通知
        /// </summary>
        private readonly Notifications bufferNotifications = new Notifications();

        /// <summary>
        /// 
        /// </summary>
        public ICommand SizeChangedCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Notifications NotificationControlSource
        {
            get { return (Notifications)GetValue(NotificationControlSourceProperty); }
            set { SetValue(NotificationControlSourceProperty, value); }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty NotificationControlSourceProperty =DependencyProperty.Register("NotificationControlSource", typeof(Notifications), typeof(ModernGrowlNotification), new PropertyMetadata(null));


        /// <summary>
        /// 信息通知
        /// </summary>
        public ModernGrowlNotification()
        {
            ShowActivated = false;
            AllowsTransparency = true;
            WindowStyle = WindowStyle.None;
            ShowInTaskbar = false;
            Background = Brushes.Transparent;
            Topmost = true;
            UseLayoutRounding = true;
            SizeChangedCommand = new RelayCommand<ExCommandParameter>(o => ModernGrowlNotificationSizeChanged(o), o => CanModernGrowlNotificationSizeChanged(o));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        private void ModernGrowlNotificationSizeChanged(ExCommandParameter parameter)
        {
            SizeChangedEventArgs changedEventArgs = parameter.EventArgs as SizeChangedEventArgs;
            if (changedEventArgs.NewSize.Height != 0.0)
            {
                return;
            }

            var element = parameter.Sender as Grid;
            if (element == null || element.Tag == null)
            {
                return;
            }

            RemoveNotify(notifications.First(n => n.Token == element.Tag.ToString()));
        }

        private bool CanModernGrowlNotificationSizeChanged(ExCommandParameter parameter)
        {
            if (parameter==null || parameter.EventArgs==null)
            {
                return false;
            }
            return true;
        }

        private void ModernGrowlNotification_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height != 0.0)
            {
                return;
            }

            var element = sender as Grid;
            if (element==null || element.Tag==null)
            {
                return;
            }

            RemoveNotify(notifications.First(n => n.Token == element.Tag.ToString()));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="notification"></param>
        public void AddNotify(Notification notification)
        {
            if (notifications.Count + 1 > maxNotifications)
            {
                bufferNotifications.Add(notification);
            }
            else
            {
                notifications.Add(notification);
            }

            //如果有通知显示窗口
            if (notifications.Count > 0 && IsActive == false)
            {
                Show();
            }
        }

        /// <summary>
        /// 移除通知
        /// </summary>
        /// <param name="notification"></param>
        public void RemoveNotify(Notification notification)
        {
            if (notifications.Contains(notification))
            {
                notifications.Remove(notification);
            }

            if (bufferNotifications.Count>0)
            {
                notifications.Add(bufferNotifications[0]);
                bufferNotifications.RemoveAt(0);
            }

            //如果当前没什么通知需要显示，就把通知窗口关上
            if (notifications.Count<1)
            {
                Hide();
            }
        }

    }
}