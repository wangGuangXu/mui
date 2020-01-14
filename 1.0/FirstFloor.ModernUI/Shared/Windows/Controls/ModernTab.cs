using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 表示包含在屏幕上共享同一空间的多个页面的控件
    /// Represents a control that contains multiple pages that share the same space on screen.
    /// </summary>
    public class ModernTab: Control
    {
        /// <summary>
        /// 标识内容加载器依赖项属性 Identifies the ContentLoader dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentLoaderProperty = DependencyProperty.Register("ContentLoader", typeof(IContentLoader), typeof(ModernTab), new PropertyMetadata(new DefaultContentLoader()));
        /// <summary>
        /// 标识布局依赖项属性 Identifies the Layout dependency property.
        /// </summary>
        public static readonly DependencyProperty LayoutProperty = DependencyProperty.Register("Layout", typeof(TabLayout), typeof(ModernTab), new PropertyMetadata(TabLayout.Tab));
        /// <summary>
        /// 标识列表宽度依赖项属性 Identifies the ListWidth dependency property.
        /// </summary>
        public static readonly DependencyProperty ListWidthProperty = DependencyProperty.Register("ListWidth", typeof(GridLength), typeof(ModernTab), new PropertyMetadata(new GridLength(170)));
        /// <summary>
        /// 标识链接依赖项属性 Identifies the Links dependency property.
        /// </summary>
        public static readonly DependencyProperty LinksProperty = DependencyProperty.Register("Links", typeof(LinkCollection), typeof(ModernTab), new PropertyMetadata(OnLinksChanged));
        /// <summary>
        /// 标识选定的源依赖项属性. Identifies the SelectedSource dependency property
        /// </summary>
        public static readonly DependencyProperty SelectedSourceProperty = DependencyProperty.Register("SelectedSource", typeof(Uri), typeof(ModernTab), new PropertyMetadata(OnSelectedSourceChanged));

        /// <summary>
        /// 当所选源发生更改时发生 Occurs when the selected source has changed.
        /// </summary>
        public event EventHandler<SourceEventArgs> SelectedSourceChanged;

        private ListBox linkList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModernTab"/> control.
        /// </summary>
        public ModernTab()
        {
            this.DefaultStyleKey = typeof(ModernTab);

            // create a default links collection
            SetCurrentValue(LinksProperty, new LinkCollection());
        }

        private static void OnLinksChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ModernTab)o).UpdateSelection();
        }

        private static void OnSelectedSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ModernTab)o).OnSelectedSourceChanged((Uri)e.OldValue, (Uri)e.NewValue);
        }

        private void OnSelectedSourceChanged(Uri oldValue, Uri newValue)
        {
            UpdateSelection();

            // 引发选择源改变事件 raise SelectedSourceChanged event
            var handler = this.SelectedSourceChanged;
            if (handler != null) {
                handler(this, new SourceEventArgs(newValue));
            }
        }

        /// <summary>
        /// 更新选择
        /// </summary>
        private void UpdateSelection()
        {
            if (this.linkList == null || this.Links == null)
            {
                return;
            }

            // 将列表选择与当前源同步 sync list selection with current source
            this.linkList.SelectedItem = this.Links.FirstOrDefault(l => l.Source == this.SelectedSource);
        }

        /// <summary>
        /// 当在派生类中重写时，每当应用程序代码或内部进程调用系统窗口时都会调用框架元素应用模板
        /// When overridden in a derived class, is invoked whenever application code or internal processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.linkList != null)
            {
                this.linkList.SelectionChanged -= OnLinkListSelectionChanged;
            }

            this.linkList = GetTemplateChild("LinkList") as ListBox;
            if (this.linkList != null)
            {
                this.linkList.SelectionChanged += OnLinkListSelectionChanged;
            }

            UpdateSelection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLinkListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var link = this.linkList.SelectedItem as Link;
            if (link != null && link.Source != this.SelectedSource)
            {
                SetCurrentValue(SelectedSourceProperty, link.Source);
                e.Handled = true;
            }
        }

        /// <summary>
        /// 获取或设置内容加载器 Gets or sets the content loader
        /// </summary>
        public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示应如何呈现选项卡 Gets or sets a value indicating how the tab should be rendered
        /// </summary>
        public TabLayout Layout
        {
            get { return (TabLayout)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }

        /// <summary>
        /// 获取或设置定义此选项卡中可用内容的链接集合 Gets or sets the collection of links that define the available content in this tab.
        /// </summary>
        public LinkCollection Links
        {
            get { return (LinkCollection)GetValue(LinksProperty); }
            set { SetValue(LinksProperty, value); }
        }

        /// <summary>
        /// 获取或设置布局设置为列表时的宽度 Gets or sets the width of the list when Layout is set to List
        /// </summary>
        /// <value>
        /// The width of the list.
        /// </value>
        public GridLength ListWidth
        {
            get { return (GridLength)GetValue(ListWidthProperty); }
            set { SetValue(ListWidthProperty, value); }
        }

        /// <summary>
        /// 获取或设置所选链接的源URI。Gets or sets the source URI of the selected link
        /// </summary>
        /// <value>The source URI of the selected link.</value>
        public Uri SelectedSource
        {
            get { return (Uri)GetValue(SelectedSourceProperty); }
            set { SetValue(SelectedSourceProperty, value); }
        }
    }
}