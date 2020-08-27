using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 表示现代用户界面样式的对话框窗口
    /// Represents a Modern UI styled dialog window.
    /// </summary>
    public class ModernDialog : DpiAwareWindow
    {
        /// <summary>
        /// 背景内容依赖属性
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty BackgroundContentProperty = DependencyProperty.Register("BackgroundContent", typeof(object), typeof(ModernDialog));
        /// <summary>
        /// 按钮组依赖属性
        /// Identifies the Buttons dependency property.
        /// </summary>
        public static readonly DependencyProperty ButtonsProperty = DependencyProperty.Register("Buttons", typeof(IEnumerable<Button>), typeof(ModernDialog));

        private ICommand closeCommand;

        private Button okButton;
        private Button cancelButton;
        private Button yesButton;
        private Button noButton;
        private Button closeButton;

        private MessageBoxResult messageBoxResult = MessageBoxResult.None;

        /// <summary>
        /// 现代对话框
        /// </summary>
        public ModernDialog()
        {
            this.DefaultStyleKey = typeof(ModernDialog);
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            this.closeCommand = new RelayCommand(o => 
            {
                var result = o as MessageBoxResult?;
                if (result.HasValue) 
                {
                    this.messageBoxResult = result.Value;

                    // 同时设置窗口对话框结果 sets the Window.DialogResult as well
                    if (result.Value == MessageBoxResult.OK || result.Value == MessageBoxResult.Yes)
                    {
                        this.DialogResult = true;
                    }
                    else if (result.Value == MessageBoxResult.Cancel || result.Value == MessageBoxResult.No)
                    {
                        this.DialogResult = false;
                    }
                    else
                    {
                        this.DialogResult = null;
                    }
                }
                Close();
            });

            this.Buttons = new Button[] { this.CloseButton };

            // 将默认所有者设置为应用程序主窗口（如果可能） set the default owner to the app main window (if possible)
            if (Application.Current != null && Application.Current.MainWindow != this) 
            {
                this.Owner = Application.Current.MainWindow;
            }
        }

        /// <summary>
        /// 创建关闭对话框按钮组
        /// </summary>
        /// <param name="content"></param>
        /// <param name="isDefault"></param>
        /// <param name="isCancel"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Button CreateCloseDialogButton(string content, bool isDefault, bool isCancel, MessageBoxResult result)
        {
            return new Button 
            {
                Content = content,
                Command = this.CloseCommand,
                CommandParameter = result,
                IsDefault = isDefault,
                IsCancel = isCancel,
                MinHeight = 21,
                MinWidth = 65,
                Margin = new Thickness(4, 0, 0, 0)
            };
        }

        /// <summary>
        /// 关闭命令
        /// </summary>
        public ICommand CloseCommand
        {
            get { return this.closeCommand; }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        public Button OkButton
        {
            get
            {
                if (this.okButton == null) 
                {
                    this.okButton = CreateCloseDialogButton(FirstFloor.ModernUI.Resources.Ok, true, false, MessageBoxResult.OK);
                }
                return this.okButton;
            }
        }

        /// <summary>
        /// 获取取消按钮.
        /// </summary>
        public Button CancelButton
        {
            get
            {
                if (this.cancelButton == null) 
                {
                    this.cancelButton = CreateCloseDialogButton(FirstFloor.ModernUI.Resources.Cancel, false, true, MessageBoxResult.Cancel);
                }
                return this.cancelButton;
            }
        }

        /// <summary>
        /// 是按钮
        /// </summary>
        public Button YesButton
        {
            get
            {
                if (this.yesButton == null) 
                {
                    this.yesButton = CreateCloseDialogButton(FirstFloor.ModernUI.Resources.Yes, true, false, MessageBoxResult.Yes);
                }
                return this.yesButton;
            }
        }

        /// <summary>
        /// 否按钮
        /// </summary>
        public Button NoButton
        {
            get
            {
                if (this.noButton == null) {
                    this.noButton = CreateCloseDialogButton(FirstFloor.ModernUI.Resources.No, false, true, MessageBoxResult.No);
                }
                return this.noButton;
            }
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        public Button CloseButton
        {
            get
            {
                if (this.closeButton == null) {
                    this.closeButton = CreateCloseDialogButton(FirstFloor.ModernUI.Resources.Close, true, false, MessageBoxResult.OK);
                }
                return this.closeButton;
            }
        }

        /// <summary>
        /// 获取或设置此窗口实例的背景内容。
        /// </summary>
        public object BackgroundContent
        {
            get { return GetValue(BackgroundContentProperty); }
            set { SetValue(BackgroundContentProperty, value); }
        }

        /// <summary>
        /// 获取或设置对话框按钮
        /// Gets or sets the dialog buttons.
        /// </summary>
        public IEnumerable<Button> Buttons
        {
            get { return (IEnumerable<Button>)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }

        /// <summary>
        /// 获取消息框结果
        /// Gets the message box result.
        /// </summary>
        /// <value>
        /// The message box result.
        /// </value>
        public MessageBoxResult MessageBoxResult
        {
            get { return this.messageBoxResult; }
        }

        /// <summary>
        /// 显示消息框 Displays a messagebox.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="title">The title.</param>
        /// <param name="button">The button.</param>
        /// <param name="owner">The window owning the messagebox. The messagebox will be located at the center of the owner.</param>
        /// <returns></returns>
        public static MessageBoxResult ShowMessage(string text, string title, MessageBoxButton button, Window owner = null)
        {
            var dlg = new ModernDialog
            {
                Title = title,
                Content = new BBCodeBlock { BBCode = text, Margin = new Thickness(0, 0, 0, 8), FontSize=12 },
            };
            //var dlg = new ModernDialog
            //{
            //    Title = title,
            //    Content = new BBCodeBlock { BBCode = text, Margin = new Thickness(0, 0, 0, 8) },
            //    MinHeight = 0,
            //    MinWidth = 0,
            //    MaxHeight = 480,
            //    MaxWidth = 640,
            //};

            if (owner != null)
            {
                dlg.Owner = owner;
            }

            dlg.Buttons = GetButtons(dlg, button);
            dlg.ShowDialog();
            return dlg.messageBoxResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        private static IEnumerable<Button> GetButtons(ModernDialog owner, MessageBoxButton button)
        {
            if (button == MessageBoxButton.OK) 
            {
                yield return owner.OkButton;
            }
            else if (button == MessageBoxButton.OKCancel) 
            {
                yield return owner.OkButton;
                yield return owner.CancelButton;
            }
            else if (button == MessageBoxButton.YesNo) 
            {
                yield return owner.YesButton;
                yield return owner.NoButton;
            }
            else if (button == MessageBoxButton.YesNoCancel) 
            {
                yield return owner.YesButton;
                yield return owner.NoButton;
                yield return owner.CancelButton;
            }
        }
    }
}