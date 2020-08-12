using FirstFloor.ModernUI.Windows.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 
    /// </summary>

    public  class FButton : Button
    {
        /// <summary>
        /// 按钮类型
        /// </summary>
        public ButtonType ButtonType
        {
            get { return (ButtonType)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }
        /// <summary>
        /// 按钮类型
        /// </summary>
        public static readonly DependencyProperty ButtonTypeProperty =
            DependencyProperty.Register("ButtonType", typeof(ButtonType), typeof(FButton), new PropertyMetadata(ButtonType.Normal));

        /// <summary>
        /// 鼠标按下背景样式
        /// </summary>
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(FButton), new PropertyMetadata(Brushes.DarkBlue));
        /// <summary>
        /// 鼠标按下背景样式
        /// </summary>
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        /// <summary>
        /// 鼠标按下前景样式（图标、文字）
        /// </summary>
        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(FButton), new PropertyMetadata(Brushes.White));
        /// <summary>
        /// 鼠标按下前景样式（图标、文字）
        /// </summary>
        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        /// <summary>
        /// 鼠标进入背景样式
        /// </summary>
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(FButton), new PropertyMetadata(Brushes.RoyalBlue));

        /// <summary>
        /// 鼠标进入背景样式
        /// </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        /// <summary>
        /// 鼠标进入前景样式
        /// </summary>
        public static readonly DependencyProperty MouseOverForegroundProperty =
            DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(FButton), new PropertyMetadata(Brushes.White));

        /// <summary>
        /// 鼠标进入前景样式
        /// </summary>
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }


        /// <summary>
        /// 按钮字体图标编码
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(FButton), new PropertyMetadata("\ue604"));

        /// <summary>
        /// 按钮字体图标编码
        /// </summary>
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// 按钮字体图标大小
        /// </summary>
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(int), typeof(FButton), new PropertyMetadata(20));

        /// <summary>
        /// 按钮字体图标大小
        /// </summary>
        public int IconSize
        {
            get { return (int)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        /// <summary>
        /// 字体图标间距
        /// </summary>
        public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register(
            "IconMargin", typeof(Thickness), typeof(FButton), new PropertyMetadata(new Thickness(0, 1, 3, 1)));

        /// <summary>
        /// 字体图标间距
        /// </summary>
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        /// <summary>
        /// 是否启用Ficon动画
        /// </summary>
        public static readonly DependencyProperty AllowsAnimationProperty = DependencyProperty.Register(
            "AllowsAnimation", typeof(bool), typeof(FButton), new PropertyMetadata(true));

        /// <summary>
        /// 是否启用Ficon动画
        /// </summary>
        public bool AllowsAnimation
        {
            get { return (bool)GetValue(AllowsAnimationProperty); }
            set { SetValue(AllowsAnimationProperty, value); }
        }

        /// <summary>
        /// 按钮圆角大小,左上，右上，右下，左下
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FButton), new PropertyMetadata(new CornerRadius(2)));
        /// <summary>
        /// 按钮圆角大小,左上，右上，右下，左下
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// 内容装饰
        /// </summary>
        public static readonly DependencyProperty ContentDecorationsProperty = DependencyProperty.Register(
            "ContentDecorations", typeof(TextDecorationCollection), typeof(FButton), new PropertyMetadata(null));

        /// <summary>
        /// 内容装饰
        /// </summary>
        public TextDecorationCollection ContentDecorations
        {
            get { return (TextDecorationCollection)GetValue(ContentDecorationsProperty); }
            set { SetValue(ContentDecorationsProperty, value); }
        }

        /// <summary>
        /// FButton
        /// </summary>
        public FButton()
        {
            this.DefaultStyleKey = typeof(FButton);
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        protected override void OnClick()
        {
            base.OnClick();

            CloseTabItem();
        }

        /// <summary>
        /// 关闭选项卡
        /// </summary>
        private void CloseTabItem()
        {
            if (string.IsNullOrEmpty(Name) || Name != "PART_Close_TabItem")
            {
                return;
            }

            TabItem itemclose = VisualTreeHelperEx.FindVisualParent<TabItem>(this);
            if (itemclose == null)
            {
                return;
            }

            (itemclose.Parent as TabControl).Items.Remove(itemclose);
            var args = new RoutedEventArgs(TabItemClose.CloseItemEvent, itemclose);
            itemclose.RaiseEvent(args);
        }
    }
}