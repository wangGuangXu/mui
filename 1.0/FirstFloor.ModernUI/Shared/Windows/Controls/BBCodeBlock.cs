using FirstFloor.ModernUI.Windows.Controls.BBCode;
using FirstFloor.ModernUI.Windows.Media;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 用于显示少量格式丰富的BBCode内容的轻量级控件。
    /// A lighweight control for displaying small amounts of rich formatted BBCode content.
    /// </summary>
    [ContentProperty("BBCode")]
    public class BBCodeBlock : TextBlock
    {
        /// <summary>
        /// Identifies the BBCode dependency property.
        /// </summary>
        public static DependencyProperty BBCodeProperty = DependencyProperty.Register("BBCode", typeof(string), typeof(BBCodeBlock), new PropertyMetadata(new PropertyChangedCallback(OnBBCodeChanged)));
        /// <summary>
        /// Identifies the LinkNavigator dependency property.
        /// </summary>
        public static DependencyProperty LinkNavigatorProperty = DependencyProperty.Register("LinkNavigator", typeof(ILinkNavigator), typeof(BBCodeBlock), new PropertyMetadata(new DefaultLinkNavigator(), OnLinkNavigatorChanged));

        /// <summary>
        /// 肮脏的
        /// </summary>
        private bool dirty = false;

        /// <summary>
        /// Gets or sets the BB code.
        /// </summary>
        /// <value>The BB code.</value>
        public string BBCode
        {
            get { return (string)GetValue(BBCodeProperty); }
            set { SetValue(BBCodeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the link navigator.
        /// </summary>
        /// <value>The link navigator.</value>
        public ILinkNavigator LinkNavigator
        {
            get { return (ILinkNavigator)GetValue(LinkNavigatorProperty); }
            set { SetValue(LinkNavigatorProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BBCodeBlock"/> class.
        /// </summary>
        public BBCodeBlock()
        {
            // 确保使用隐式BBCodeBlock样式 ensures the implicit BBCodeBlock style is used
            this.DefaultStyleKey = typeof(BBCodeBlock);

            AddHandler(Hyperlink.LoadedEvent, new RoutedEventHandler(OnLoaded));
            AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(OnRequestNavigate));
        }

        private static void OnBBCodeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((BBCodeBlock)o).UpdateDirty();
        }

        private static void OnLinkNavigatorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) 
            {
                // 不允许空值 null values disallowed
                throw new ArgumentNullException("LinkNavigator");
            }

            ((BBCodeBlock)o).UpdateDirty();
        }

        private void OnLoaded(object o, EventArgs e)
        {
            Update();
        }

        /// <summary>
        /// 更新脏数据
        /// </summary>
        private void UpdateDirty()
        {
            this.dirty = true;
            Update();
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
            try
            {
                if (!this.IsLoaded || !this.dirty)
                {
                    return;
                }

                var bbcode = this.BBCode;

                if (this.Inlines.Count > 0)
                {
                    this.Inlines.Clear();
                }

                if (!string.IsNullOrWhiteSpace(bbcode))
                {
                    Inline inline;
                    try
                    {
                        var parser = new BBCodeParser(bbcode, this)
                        {
                            Commands = this.LinkNavigator.Commands
                        };
                        inline = parser.Parse();
                    }
                    catch (Exception)
                    {
                        // 分析失败，按原样显示BBCode值 parsing failed, display BBCode value as-is
                        inline = new Run { Text = bbcode };
                    }
                    this.Inlines.Add(inline);
                }
                this.dirty = false;
            }
            catch (Exception error)
            {
                ModernDialog.ShowMessage(error.Message, ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// 按要求导航
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try 
            {
                // 使用链接导航器执行导航 perform navigation using the link navigator
                this.LinkNavigator.Navigate(e.Uri, this, e.Target);
            }
            catch (Exception error) 
            {
                // 显示导航失败 display navigation failures
                ModernDialog.ShowMessage(error.Message, ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
            }
        }

    }
}