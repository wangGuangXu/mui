using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 表示一个现代UI样式的窗口
    /// </summary>
    public class ModernWindow  : DpiAwareWindow
    {
        #region 依赖属性
        /// <summary>
        /// 标识背景内容依赖项属性.
        /// </summary>
        public static readonly DependencyProperty BackgroundContentProperty = DependencyProperty.Register("BackgroundContent", typeof(object), typeof(ModernWindow));
        /// <summary>
        /// 标识菜单链接组依赖属性 Identifies the MenuLinkGroups dependency property.
        /// </summary>
        public static readonly DependencyProperty MenuLinkGroupsProperty = DependencyProperty.Register("MenuLinkGroups", typeof(LinkGroupCollection), typeof(ModernWindow));
        /// <summary>
        /// 标识标题链接组依赖属性 Identifies the TitleLinks dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleLinksProperty = DependencyProperty.Register("TitleLinks", typeof(LinkCollection), typeof(ModernWindow));
        /// <summary>
        /// 标识标题是否可见依赖属性 Identifies the IsTitleVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty IsTitleVisibleProperty = DependencyProperty.Register("IsTitleVisible", typeof(bool), typeof(ModernWindow), new PropertyMetadata(false));
        /// <summary>
        /// 标识Logo数据依赖属性 Identifies the LogoData dependency property.
        /// </summary>
        public static readonly DependencyProperty LogoDataProperty = DependencyProperty.Register("LogoData", typeof(Geometry), typeof(ModernWindow));
        /// <summary>
        /// 定义ContentSource依赖项属性 Defines the ContentSource dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentSourceProperty = DependencyProperty.Register("ContentSource", typeof(Uri), typeof(ModernWindow));
        /// <summary>
        /// 标识内容来源依赖项属性 Identifies the ContentLoader dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentLoaderProperty = DependencyProperty.Register("ContentLoader", typeof(IContentLoader), typeof(ModernWindow), new PropertyMetadata(new DefaultContentLoader()));
        /// <summary>
        /// 标识链接导航器依赖项属性 Identifies the LinkNavigator dependency property.
        /// </summary>
        public static DependencyProperty LinkNavigatorProperty = DependencyProperty.Register("LinkNavigator", typeof(ILinkNavigator), typeof(ModernWindow), new PropertyMetadata(new DefaultLinkNavigator()));
        /// <summary>
        /// 关闭窗口附加属性
        /// Window窗体的DialogResult属性不是依赖属性，所以没法绑定，所以此处添加附加属性解决此问题。
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.Register("DialogResult", typeof(bool), typeof(ModernWindow), new PropertyMetadata(DialogResultChanged));

        /// <summary>
        /// 状态栏用户
        /// </summary>
        public static readonly DependencyProperty StatusUserProperty = DependencyProperty.Register("StatusUser", typeof(string), typeof(ModernWindow), new PropertyMetadata(""));
        /// <summary>
        /// 状态栏网络
        /// </summary>
        public static readonly DependencyProperty StatusNetWorkProperty = DependencyProperty.Register("StatusNetWork", typeof(string), typeof(ModernWindow));


        /// <summary>
        /// 
        /// </summary>
        public TabItemCollection TabItems
        {
            get { return (TabItemCollection)GetValue(TabItemsProperty); }
            set { SetValue(TabItemsProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TabItemsProperty = DependencyProperty.Register("TabItems", typeof(TabItemCollection), typeof(ModernWindow));

        /// <summary>
        /// 背景动画
        /// </summary>
        private Storyboard backgroundAnimation; 
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置此窗口实例的背景内容
        /// </summary>
        public object BackgroundContent
        {
            get { return GetValue(BackgroundContentProperty); }
            set { SetValue(BackgroundContentProperty, value); }
        }

        /// <summary>
        /// 获取或设置窗口菜单中显示的链接组的集合
        /// </summary>
        public LinkGroupCollection MenuLinkGroups
        {
            get { return (LinkGroupCollection)GetValue(MenuLinkGroupsProperty); }
            set { SetValue(MenuLinkGroupsProperty, value); }
        }

        /// <summary>
        /// 获取或设置出现在窗口标题区域的菜单中的链接集合.
        /// </summary>
        public LinkCollection TitleLinks
        {
            get { return (LinkCollection)GetValue(TitleLinksProperty); }
            set { SetValue(TitleLinksProperty, value); }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示窗口标题在UI中是否可见
        /// </summary>
        public bool IsTitleVisible
        {
            get { return (bool)GetValue(IsTitleVisibleProperty); }
            set { SetValue(IsTitleVisibleProperty, value); }
        }

        /// <summary>
        /// 获取或设置窗口标题区域中显示的标志的路径数据.
        /// </summary>
        public Geometry LogoData
        {
            get { return (Geometry)GetValue(LogoDataProperty); }
            set { SetValue(LogoDataProperty, value); }
        }

        /// <summary>
        /// 获取或设置当前内容的源uri。
        /// Gets or sets the source uri of the current content.
        /// </summary>
        public Uri ContentSource
        {
            get { return (Uri)GetValue(ContentSourceProperty); }
            set { SetValue(ContentSourceProperty, value); }
        }

        /// <summary>
        /// 获取或设置内容加载器
        /// Gets or sets the content loader.
        /// </summary>
        public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        /// <summary>
        /// 获取或设置链接导航器
        /// Gets or sets the link navigator.
        /// </summary>
        /// <value>The link navigator.</value>
        public ILinkNavigator LinkNavigator
        {
            get { return (ILinkNavigator)GetValue(LinkNavigatorProperty); }
            set { SetValue(LinkNavigatorProperty, value); }
        }

        /// <summary>
        /// 状态栏用户
        /// </summary>
        public string StatusUser
        {
            get { return (string)GetValue(StatusUserProperty); }
            set { SetValue(StatusUserProperty, value); }
        }

        /// <summary>
        /// 状态栏网络
        /// </summary>
        public string StatusNetWork
        {
            get { return (string)GetValue(StatusNetWorkProperty); }
            set { SetValue(StatusNetWorkProperty, value); }
        } 
        #endregion

        /// <summary>
        /// 初始化示例
        /// </summary>
        public ModernWindow()
        {
            this.DefaultStyleKey = typeof(ModernWindow);

            // 创建空集合
            SetCurrentValue(MenuLinkGroupsProperty, new LinkGroupCollection());
            SetCurrentValue(TitleLinksProperty, new LinkCollection());
            SetCurrentValue(TabItemsProperty, new TabItemCollection());

            // 将窗口命令与此实例关联
#if NET4
            this.CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.CloseWindowCommand, OnCloseWindow));
            this.CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));
#else
            this.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));
#endif
            // 将导航链接命令与此实例关联
            this.CommandBindings.Add(new CommandBinding(LinkCommands.NavigateLink, OnNavigateLink, OnCanNavigateLink));

            // 监听主题改变事件
            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        #region 方法
        /// <summary>
        /// 引发 System.Windows.Window.Closed 事件.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // 分离事件处理程序 detach event handler
            AppearanceManager.Current.PropertyChanged -= OnAppearanceManagerPropertyChanged;
        }

        /// <summary>
        /// 在派生类中重写时，在应用程序代码或内部进程调用System.Windows.FrameworkElement.ApplyTemplate()时调用。
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // 检索背景动画故事板 retrieve BackgroundAnimation storyboard
            var border = GetTemplateChild("WindowBorder") as Border;
            if (border != null)
            {
                this.backgroundAnimation = border.Resources["BackgroundAnimation"] as Storyboard;

                if (this.backgroundAnimation != null)
                {
                    this.backgroundAnimation.Begin();
                }
            }
        }

        /// <summary>
        /// 外观管理属性更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // 如果主题已更改，则启动背景动画
            if (e.PropertyName == "ThemeSource" && this.backgroundAnimation != null)
            {
                this.backgroundAnimation.Begin();
            }
        }

        /// <summary>
        /// 打开链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCanNavigateLink(object sender, CanExecuteRoutedEventArgs e)
        {
            // 默认为True
            e.CanExecute = true;

            if (this.LinkNavigator != null && this.LinkNavigator.Commands != null)
            {
                // 如果是命令uri，请检查icommand.canexecute是否为true
                Uri uri;
                string parameter;
                string targetName;

                // Todo: cannavigate被大量调用，这意味着需要大量解析。需要改进??
                if (NavigationHelper.TryParseUriWithParameters(e.Parameter, out uri, out parameter, out targetName))
                {
                    ICommand command;
                    if (this.LinkNavigator.Commands.TryGetValue(uri, out command))
                    {
                        e.CanExecute = command.CanExecute(parameter);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigateLink(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.LinkNavigator == null)
            {
                return;
            }

            if (NavigationHelper.TryParseUriWithParameters(e.Parameter, out Uri uri, out string parameter, out string targetName))
            {
                this.LinkNavigator.Navigate(uri, e.Source as FrameworkElement, parameter);
            }
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode != ResizeMode.NoResize;
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
#if NET4
            Microsoft.Windows.Shell.SystemCommands.CloseWindow(this);
#else
            SystemCommands.CloseWindow(this);
#endif
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
#if NET4
            Microsoft.Windows.Shell.SystemCommands.MaximizeWindow(this);
#else
            SystemCommands.MaximizeWindow(this);
#endif
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
#if NET4
            Microsoft.Windows.Shell.SystemCommands.MinimizeWindow(this);
#else
            SystemCommands.MinimizeWindow(this);
#endif
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
#if NET4
            Microsoft.Windows.Shell.SystemCommands.RestoreWindow(this);
#else
            SystemCommands.RestoreWindow(this);
#endif
        }

        /// <summary>
        /// 获取窗口关闭值
        /// </summary>
        /// <param name="o">目标依赖对象.</param>
        /// <returns></returns>
        public static bool? GetDialogResult(DependencyObject o)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }
            return (bool?)o.GetValue(DialogResultProperty);
        }

        /// <summary>
        /// 设置窗口关闭值
        /// </summary>
        /// <param name="o">目标依赖对象.</param>
        /// <param name="value"></param>
        public static void SetDialogResult(DependencyObject o, bool? value)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }
            o.SetValue(DialogResultProperty, value);
        }

        /// <summary>
        /// 值改变事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.DialogResult = true;
            }
        } 
        #endregion

    }
}