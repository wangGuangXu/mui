using FirstFloor.ModernUI.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Windows.Media;
using System.Net;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Interactivity;
using System.Windows;
using System.Windows.Controls;

namespace FirstFloor.ModernUI.Presentation
{
    /// <summary>
    /// 通知模型
    /// </summary>
    public class Notification: NotifyPropertyChanged
    {
        private string icon;
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged(()=>this.Icon);
            }
        }

        private Brush iconBackgroundBrush;
        /// <summary>
        /// 字体图标背景色画刷
        /// </summary>
        public Brush IconBackgroundBrush
        {
            get { return iconBackgroundBrush; }
            set
            {
                iconBackgroundBrush = value;
                OnPropertyChanged(() => this.IconBackgroundBrush);
            }
        }

        private string title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged(() => this.Title);
            }
        }

        private string message;
        /// <summary>
        /// 提示内容
        /// </summary>
        public string Message
        {
            get { return message; }
            set 
            { 
                message = value;
                OnPropertyChanged(() => this.Message);
            }
        }

        private bool isShowDateTime;
        /// <summary>
        /// 是否显示时间
        /// </summary>
        public bool IsShowDateTime
        {
            get { return isShowDateTime; }
            set
            {
                isShowDateTime = value;
                OnPropertyChanged(() => this.IsShowDateTime);
            }
        }

        private int waitTime=6;
        /// <summary>
        /// 等待时间
        /// </summary>
        public int WaitTime
        {
            get { return waitTime; }
            set
            {
                waitTime = value;
                OnPropertyChanged(() => this.WaitTime);
            }
        }

        private string token;
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token
        {
            get { return token; }
            set
            {
                token = value;
                OnPropertyChanged(() => this.Token);
            }
        }

        ///// <summary>
        ///// 取消字符串
        ///// </summary>
        //public string CancelStr { get; set; }
        ///// <summary>
        ///// 确认字符串
        ///// </summary>
        //public string ConfirmStr { get; set; }
        ///// <summary>
        ///// 关闭前的操作
        ///// </summary>
        //public Func<bool, bool> ActionBeforeClose { get; set; }
        ///// <summary>
        ///// 是否保持打开状态
        ///// </summary>
        //public bool StaysOpen { get; set; }
        ///// <summary>
        ///// 是否自定义
        ///// </summary>
        //public bool IsCustom { get; set; }
        ///// <summary>
        ///// 提示类型
        ///// </summary>
        //public ModernInfoType Type { get; set; }

        ///// <summary>
        ///// 图标画刷
        ///// </summary>
        //public string IconBrushKey { get; set; }
        ///// <summary>
        ///// 显示关闭按钮
        ///// </summary>
        //public bool ShowCloseButton { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public ICommand SizeChangedCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Notification(string icon, string title, string iconBackgroundBrush, string message)
        {
            Token = Guid.NewGuid().ToString();

            Icon = WebUtility.HtmlDecode(icon) ;
            IconBackgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(iconBackgroundBrush));
            Title = title;
            Message = message;

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

            //RemoveNotify(notifications.First(n => n.Token == element.Tag.ToString()));
        }

        private bool CanModernGrowlNotificationSizeChanged(ExCommandParameter parameter)
        {
            if (parameter == null || parameter.EventArgs == null)
            {
                return false;
            }
            return true;
        }

    }
}