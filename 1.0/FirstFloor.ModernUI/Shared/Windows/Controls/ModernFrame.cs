using FirstFloor.ModernUI.Windows.Media;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 一个简单的带有导航支持的内容框架实现 A simple content frame implementation with navigation support.
    /// </summary>
    public class ModernFrame : ContentControl
    {
        /// <summary>
        /// 标识保活附件依赖属性
        /// Identifies the KeepAlive attached dependency property.
        /// </summary>
        public static readonly DependencyProperty KeepAliveProperty = DependencyProperty.RegisterAttached("KeepAlive", typeof(bool?), typeof(ModernFrame), new PropertyMetadata(null));
        /// <summary>
        /// 标识内容保活依赖属性 注意默认是保活，会导致页面内容切换链接后不能及时刷新。
        /// Identifies the KeepContentAlive dependency property.
        /// </summary>
        public static readonly DependencyProperty KeepContentAliveProperty = DependencyProperty.Register("KeepContentAlive", typeof(bool), typeof(ModernFrame), new PropertyMetadata(true, OnKeepContentAliveChanged));
        /// <summary>
        /// 标识内容加载器依赖属性
        /// Identifies the ContentLoader dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentLoaderProperty = DependencyProperty.Register("ContentLoader", typeof(IContentLoader), typeof(ModernFrame), new PropertyMetadata(new DefaultContentLoader(), OnContentLoaderChanged));
        /// <summary>
        /// 正在加载内容
        /// </summary>
        private static readonly DependencyPropertyKey IsLoadingContentPropertyKey = DependencyProperty.RegisterReadOnly("IsLoadingContent", typeof(bool), typeof(ModernFrame), new PropertyMetadata(false));
        /// <summary>
        /// 标识正在加载内容依赖项属性
        /// Identifies the IsLoadingContent dependency property.
        /// </summary>
        public static readonly DependencyProperty IsLoadingContentProperty = IsLoadingContentPropertyKey.DependencyProperty;
        /// <summary>
        /// 标识源依赖项属性 Identifies the Source dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(Uri), typeof(ModernFrame), new PropertyMetadata(OnSourceChanged));

        /// <summary>
        /// 当导航到内容片段开始时
        /// Occurs when navigation to a content fragment begins.
        /// </summary>
        public event EventHandler<FragmentNavigationEventArgs> FragmentNavigation;
        /// <summary>
        /// 当请求新的导航时发生
        /// Occurs when a new navigation is requested.
        /// </summary>
        /// <remarks>
        /// 导航事件在父框架导航时也会引发。这允许取消父导航
        /// The navigating event is also raised when a parent frame is navigating. This allows for cancelling parent navigation.
        /// </remarks>
        public event EventHandler<NavigatingCancelEventArgs> Navigating;
        /// <summary>
        /// 导航到新内容完成时发生
        /// Occurs when navigation to new content has completed.
        /// </summary>
        public event EventHandler<NavigationEventArgs> Navigated;
        /// <summary>
        /// 导航失败时发生 Occurs when navigation has failed.
        /// </summary>
        public event EventHandler<NavigationFailedEventArgs> NavigationFailed;

        /// <summary>
        /// 历史访问记录
        /// </summary>
        public Stack<Uri> history = new Stack<Uri>();
        /// <summary>
        /// 内容缓存
        /// </summary>
        public Dictionary<Uri, object> contentCache = new Dictionary<Uri, object>();
#if NET4
        private List<WeakReference> childFrames = new List<WeakReference>();        // list of registered frames in sub tree
#else
        /// <summary>
        /// 子框架集合
        /// </summary>
        public List<WeakReference<ModernFrame>> childFrames = new List<WeakReference<ModernFrame>>();        // list of registered frames in sub tree
#endif
        private CancellationTokenSource tokenSource;
        private bool isNavigatingHistory;
        private bool isResetSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModernFrame"/> class.
        /// </summary>
        public ModernFrame()
        {
            //定义控件的外观及属性的默认值
            this.DefaultStyleKey = typeof(ModernFrame);

            // 将应用程序和导航命令与此实例关联 associate application and navigation commands with this instance
            this.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseBack, OnBrowseBack, OnCanBrowseBack));
            this.CommandBindings.Add(new CommandBinding(NavigationCommands.GoToPage, OnGoToPage, OnCanGoToPage));
            this.CommandBindings.Add(new CommandBinding(NavigationCommands.Refresh, OnRefresh, OnCanRefresh));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, OnCopy, OnCanCopy));

            this.Loaded += OnLoaded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var parent = NavigationHelper.FindFrame(NavigationHelper.FrameParent, this);
            if (parent != null)
            {
                parent.RegisterChildFrame(this);
            }
        }

        private static void OnKeepContentAliveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ModernFrame)o).OnKeepContentAliveChanged((bool)e.NewValue);
        }

        /// <summary>
        /// 关于保持内容活动已更改
        /// </summary>
        /// <param name="keepAlive"></param>
        private void OnKeepContentAliveChanged(bool keepAlive)
        {
            // 清除内容缓存 clear content cache
            this.contentCache.Clear();
        }

        private static void OnContentLoaderChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                // 不允许内容加载器的空值 null values for content loader not allowed
                throw new ArgumentNullException("ContentLoader");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private static void OnSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ModernFrame)o).OnSourceChanged((Uri)e.OldValue, (Uri)e.NewValue);
        }

        /// <summary>
        /// 源更改事件
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private void OnSourceChanged(Uri oldValue, Uri newValue)
        {
            // 如果重置源或旧源等于新建，则不要执行任何操作 if resetting source or old source equals new, don't do anything
            if (this.isResetSource || newValue != null && newValue.Equals(oldValue))
            {
                return;
            }

            // 处理片段导航 handle fragment navigation
            string newFragment = null;
            var oldValueNoFragment = NavigationHelper.RemoveFragment(oldValue);
            var newValueNoFragment = NavigationHelper.RemoveFragment(newValue, out newFragment);

            if (newValueNoFragment != null && newValueNoFragment.Equals(oldValueNoFragment))
            {
                // 片段进行导航 fragment navigation
                var args = new FragmentNavigationEventArgs
                {
                    Fragment = newFragment
                };

                OnFragmentNavigation(this.Content as IContent, args);
            }
            else
            {
                var navType = this.isNavigatingHistory ? NavigationType.Back : NavigationType.New;

                // 只有调用才能导航新的导航 only invoke CanNavigate for new navigation
                if (!this.isNavigatingHistory && !CanNavigate(oldValue, newValue, navType))
                {
                    return;
                }

                Navigate(oldValue, newValue, navType);
            }
        }

        /// <summary>
        /// 可以导航
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="navigationType"></param>
        /// <returns></returns>
        private bool CanNavigate(Uri oldValue, Uri newValue, NavigationType navigationType)
        {
            var cancelArgs = new NavigatingCancelEventArgs
            {
                Frame = this,
                Source = newValue,
                IsParentFrameNavigating = true,
                NavigationType = navigationType,
                Cancel = false,
            };

            OnNavigating(this.Content as IContent, cancelArgs);

            // 检查是否取消导航 check if navigation cancelled
            if (cancelArgs.Cancel)
            {
                Debug.WriteLine("取消导航 '{0}' 到 '{1}'", oldValue, newValue);

                if (this.Source != oldValue)
                {
                    // 加入操作队列，将源重置为旧值 enqueue the operation to reset the source back to the old value
                    Dispatcher.BeginInvoke((Action)(() => 
                    {
                        this.isResetSource = true;
                        SetCurrentValue(SourceProperty, oldValue);
                        this.isResetSource = false;
                    }));
                }
                return false;
            }

            return true;
        }

        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="navigationType"></param>
        private void Navigate(Uri oldValue, Uri newValue, NavigationType navigationType)
        {
            Debug.WriteLine("从导航 '{0}' 到 '{1}'", oldValue, newValue);

            // 设置正在加载内容状态 set IsLoadingContent state
            SetValue(IsLoadingContentPropertyKey, true);

            // 取消以前的加载内容任务(如果有的话) cancel previous load content task (if any)
            // 注意:不需要线程同步，这段代码总是在UI线程上执行 note: no need for thread synchronization, this code always executes on the UI thread
            if (this.tokenSource != null)
            {
                this.tokenSource.Cancel();
                this.tokenSource = null;
            }

            // 将以前的源代码推入历史堆栈(仅用于新的导航类型) push previous source onto the history stack (only for new navigation types)
            if (oldValue != null && navigationType == NavigationType.New)
            {
                this.history.Push(oldValue);
            }

            object newContent = null;

            if (newValue != null)
            {
                // 内容缓存在没有片段的URI上 content is cached on uri without fragment
                var newValueNoFragment = NavigationHelper.RemoveFragment(newValue);

                if (navigationType == NavigationType.Refresh || !this.contentCache.TryGetValue(newValueNoFragment, out newContent))
                {
                    var localTokenSource = new CancellationTokenSource();
                    this.tokenSource = localTokenSource;

                    // 加载内容（异步！） load the content (asynchronous!)
                    var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                    var task = this.ContentLoader.LoadContentAsync(newValue, this.tokenSource.Token);

                    task.ContinueWith(t => 
                    {
                        try 
                        {
                            if (t.IsCanceled || localTokenSource.IsCancellationRequested)
                            {
                                Debug.WriteLine("取消导航 Cancelled navigation to '{0}'", newValue);
                            }
                            else if (t.IsFaulted)
                            {
                                // 引发失败事件 raise failed event
                                var failedArgs = new NavigationFailedEventArgs
                                {
                                    Frame = this,
                                    Source = newValue,
                                    Error = t.Exception.InnerException,
                                    Handled = false
                                };

                                OnNavigationFailed(failedArgs);

                                // 如果未处理，则将错误显示为内容 if not handled, show error as content
                                newContent = failedArgs.Handled ? null : failedArgs.Error;

                                SetContent(newValue, navigationType, newContent, true);
                            }
                            else
                            {
                                newContent = t.Result;
                                if (ShouldKeepContentAlive(newContent))
                                {
                                    // 将新内容保存在内存中 keep the new content in memory
                                    this.contentCache[newValueNoFragment] = newContent;
                                }
                                SetContent(newValue, navigationType, newContent, false);
                            }
                        }
                        finally
                        {
                            // 清除全局标记源以避免取消释放的对象 clear global tokenSource to avoid a Cancel on a disposed object
                            if (this.tokenSource == localTokenSource)
                            {
                                this.tokenSource = null;
                            }

                            // 并处理本地令牌源 and dispose of the local tokensource
                            localTokenSource.Dispose();
                        }
                    }, scheduler);
                    return;
                }
            }

            // 新值为null或缓存中找到了新内容 newValue is null or newContent was found in the cache
            SetContent(newValue, navigationType, newContent, false);
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="newSource">新源地址</param>
        /// <param name="navigationType">导航类型</param>
        /// <param name="newContent">新内容</param>
        /// <param name="contentIsError">内容是否错误</param>
        private void SetContent(Uri newSource, NavigationType navigationType, object newContent, bool contentIsError)
        {
            var oldContent = this.Content as IContent;

            // 分配内容 assign content
            this.Content = newContent;

            // 错误时不引发导航事件 do not raise navigated event when error
            if (!contentIsError)
            {
                var args = new NavigationEventArgs { Frame = this, Source = newSource, Content = newContent, NavigationType = navigationType };

                OnNavigated(oldContent, newContent as IContent, args);
            }

            // 正在将内容加载为false set IsLoadingContent to false
            SetValue(IsLoadingContentPropertyKey, false);

            if (!contentIsError)
            {
                // 并引发可选的片段导航事件 and raise optional fragment navigation events
                NavigationHelper.RemoveFragment(newSource, out string fragment);
                if (fragment == null)
                {
                    return;
                }

                // 片段导航 fragment navigation
                var fragmentArgs = new FragmentNavigationEventArgs { Fragment = fragment };

                OnFragmentNavigation(newContent as IContent, fragmentArgs);
            }
        }

        /// <summary>
        /// 获取子框架集合
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ModernFrame> GetChildFrames()
        {
            var refs = this.childFrames.ToArray();
            foreach (var r in refs)
            {
                var valid = false;
                ModernFrame frame;

#if NET4
                if (r.IsAlive)
                {
                    frame = (ModernFrame)r.Target;
#else
                if (r.TryGetTarget(out frame)) 
                    {
#endif
                    //检查frame是否仍然是一个实际的子级（不是移除子级时的情况，而是尚未进行垃圾回收）
                    // check if frame is still an actual child (not the case when child is removed, but not yet garbage collected)
                    if (NavigationHelper.FindFrame(null, frame) == this)
                    {
                        valid = true;
                        yield return frame;
                    }
                }

                if (!valid)
                {
                    this.childFrames.Remove(r);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="e"></param>
        private void OnFragmentNavigation(IContent content, FragmentNavigationEventArgs e)
        {
            // 调用可选IContent.on碎片导航 invoke optional IContent.OnFragmentNavigation
            if (content != null)
            {
                content.OnFragmentNavigation(e);
            }

            // 引发碎片导航事件 raise the FragmentNavigation event
            if (FragmentNavigation != null)
            {
                FragmentNavigation(this, e);
            }
        }

        /// <summary>
        /// 在导航
        /// </summary>
        /// <param name="content"></param>
        /// <param name="e"></param>
        private void OnNavigating(IContent content, NavigatingCancelEventArgs e)
        {
            // 首先调用子框架导航事件 first invoke child frame navigation events
            foreach (var f in GetChildFrames())
            {
                f.OnNavigating(f.Content as IContent, e);
            }

            e.IsParentFrameNavigating = e.Frame != this;

            // 调用IContent.OnNavigation(仅当内容实现IContent时) invoke IContent.OnNavigating (only if content implements IContent)
            if (content != null)
            {
                content.OnNavigatingFrom(e);
            }

            // 引发导航事件 raise the Navigating event
            if (Navigating != null)
            {
                Navigating(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldContent"></param>
        /// <param name="newContent"></param>
        /// <param name="e"></param>
        private void OnNavigated(IContent oldContent, IContent newContent, NavigationEventArgs e)
        {
            // 在被导航到和被导航到时调用IContent invoke IContent.OnNavigatedFrom and OnNavigatedTo
            if (oldContent != null)
            {
                oldContent.OnNavigatedFrom(e);
            }

            if (newContent != null)
            {
                newContent.OnNavigatedTo(e);
            }

            // 引发导航事件 raise the Navigated event
            if (Navigated != null)
            {
                Navigated(this, e);
            }
        }

        /// <summary>
        /// 导航失败时
        /// </summary>
        /// <param name="e"></param>
        private void OnNavigationFailed(NavigationFailedEventArgs e)
        {
            if (NavigationFailed == null)
            {
                return;
            }
            NavigationFailed(this, e);
        }

        /// <summary>
        /// 确定是否应处理路由事件参数 Determines whether the routed event args should be handled.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <remarks>This method prevents parent frames from handling routed commands.</remarks>
        private bool HandleRoutedEvent(CanExecuteRoutedEventArgs args)
        {
            var originalSource = args.OriginalSource as DependencyObject;
            if (originalSource == null)
            {
                return false;
            }

            return originalSource.AncestorsAndSelf().OfType<ModernFrame>().FirstOrDefault() == this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCanBrowseBack(object sender, CanExecuteRoutedEventArgs e)
        {
            // 只启用浏览后退框，不要冒泡 only enable browse back for source frame, do not bubble
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = this.history.Count > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCanCopy(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = this.Content != null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCanGoToPage(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = e.Parameter is String || e.Parameter is Uri;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCanRefresh(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = this.Source != null;
            }
        }

        /// <summary>
        /// 浏览后退
        /// </summary>
        /// <param name="target"></param>
        /// <param name="e"></param>
        private void OnBrowseBack(object target, ExecutedRoutedEventArgs e)
        {
            if (this.history.Count <1)
            {
                return;
            }

            var oldValue = this.Source;
            var newValue = this.history.Peek();     // 还没有删除，导航可能会被取消 do not remove just yet, navigation may be cancelled

            if (CanNavigate(oldValue, newValue, NavigationType.Back))
            {
                this.isNavigatingHistory = true;
                SetCurrentValue(SourceProperty, this.history.Pop());
                this.isNavigatingHistory = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="e"></param>
        private void OnGoToPage(object target, ExecutedRoutedEventArgs e)
        {
            var newValue = NavigationHelper.ToUri(e.Parameter);
            SetCurrentValue(SourceProperty, newValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="e"></param>
        private void OnRefresh(object target, ExecutedRoutedEventArgs e)
        {
            if (CanNavigate(this.Source, this.Source, NavigationType.Refresh))
            {
                Navigate(this.Source, this.Source, NavigationType.Refresh);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="e"></param>
        private void OnCopy(object target, ExecutedRoutedEventArgs e)
        {
            // 将当前内容的字符串表示形式复制到剪贴板 copies the string representation of the current content to the clipboard
            Clipboard.SetText(this.Content.ToString());
        }

        /// <summary>
        /// 注册子框架
        /// </summary>
        /// <param name="frame"></param>
        private void RegisterChildFrame(ModernFrame frame)
        {
            // 不注册现有框架 do not register existing frame
            if (!GetChildFrames().Contains(frame))
            {
#if NET4
                //若引用
                var r = new WeakReference(frame);
#else
                var r = new WeakReference<ModernFrame>(frame);
#endif
                this.childFrames.Add(r);
            }
        }

        /// <summary>
        /// 确定是否应保持指定内容为活动内容
        /// Determines whether the specified content should be kept alive.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool ShouldKeepContentAlive(object content)
        {
            var o = content as DependencyObject;
            if (o != null)
            {
                var result = GetKeepAlive(o);

                // 如果给定内容存在值，请使用它 if a value exists for given content, use it
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
            // 否则就让现代框架来决定 otherwise let the ModernFrame decide
            return this.KeepContentAlive;
        }

        /// <summary>
        /// 获取一个值，该值指示是否在现代化框架实例中保持指定对象的活动状态
        /// Gets a value indicating whether to keep specified object alive in a ModernFrame instance.
        /// </summary>
        /// <param name="o">The target dependency object.</param>
        /// <returns>Whether to keep the object alive. Null to leave the decision to the ModernFrame.</returns>
        public static bool? GetKeepAlive(DependencyObject o)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }
            return (bool?)o.GetValue(KeepAliveProperty);
        }

        /// <summary>
        /// 设置一个值，该值指示是否在现代化框架实例中保持指定对象的活动状态
        /// Sets a value indicating whether to keep specified object alive in a ModernFrame instance.
        /// </summary>
        /// <param name="o">The target dependency object.</param>
        /// <param name="value">Whether to keep the object alive. Null to leave the decision to the ModernFrame.</param>
        public static void SetKeepAlive(DependencyObject o, bool? value)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }
            o.SetValue(KeepAliveProperty, value);
        }

        /// <summary>
        /// 获取或设置是否应将内容保存在内存中
        /// Gets or sets a value whether content should be kept in memory.
        /// </summary>
        public bool KeepContentAlive
        {
            get { return (bool)GetValue(KeepContentAliveProperty); }
            set { SetValue(KeepContentAliveProperty, value); }
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
        /// Gets a value indicating whether this instance is currently loading content.
        /// </summary>
        public bool IsLoadingContent
        {
            get { return (bool)GetValue(IsLoadingContentProperty); }
        }

        /// <summary>
        /// 获取或设置当前内容的源 
        /// Gets or sets the source of the current content.
        /// </summary>
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
    }
}