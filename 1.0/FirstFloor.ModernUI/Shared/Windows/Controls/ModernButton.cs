using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 将图标内容添加到标准按钮 Adds icon content to a standard button.
    /// </summary>
    public class ModernButton : Button
    {
        /// <summary>
        /// 标识椭圆直径属性 Identifies the EllipseDiameter property.
        /// </summary>
        public static readonly DependencyProperty EllipseDiameterProperty = DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(ModernButton), new PropertyMetadata(22D));
        /// <summary>
        /// 标识椭圆笔划厚度属性 Identifies the EllipseStrokeThickness property.
        /// </summary>
        public static readonly DependencyProperty EllipseStrokeThicknessProperty = DependencyProperty.Register("EllipseStrokeThickness", typeof(double), typeof(ModernButton), new PropertyMetadata(1D));
        /// <summary>
        /// 标识图标数据属性 Identifies the IconData property.
        /// </summary>
        public static readonly DependencyProperty IconDataProperty = DependencyProperty.Register("IconData", typeof(Geometry), typeof(ModernButton));
        /// <summary>
        /// 标识图标高度属性 Identifies the IconHeight property.
        /// </summary>
        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(double), typeof(ModernButton), new PropertyMetadata(12D));
        /// <summary>
        /// 标识图标宽度属性 Identifies the IconWidth property.
        /// </summary>
        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(double), typeof(ModernButton), new PropertyMetadata(12D));

        #region 扩展依属性
        /// <summary>
        /// 按钮字体图标编码
        /// </summary>
        public static readonly DependencyProperty IconCodeProperty = DependencyProperty.Register("IconCode", typeof(string), typeof(ModernButton), new PropertyMetadata("\ue604"));
        /// <summary>
        /// 按钮字体图标大小
        /// </summary>
        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof(int), typeof(ModernButton), new PropertyMetadata(20));
        /// <summary>
        /// 字体图标间距
        /// </summary>
        public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(ModernButton), new PropertyMetadata(new Thickness(0, 1, 3, 1)));
        /// <summary>
        /// 是否启用Icon图标动画
        /// </summary>
        public static readonly DependencyProperty IconAllowsAnimationProperty = DependencyProperty.Register("IconAllowsAnimation", typeof(bool), typeof(ModernButton), new PropertyMetadata(true));
        /// <summary>
        /// 按钮圆角大小,左上，右上，右下，左下
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ModernButton), new PropertyMetadata(new CornerRadius(2)));
        /// <summary>
        /// 鼠标进入背景样式
        /// </summary>
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(ModernButton), new PropertyMetadata(Brushes.RoyalBlue));
        /// <summary>
        /// 鼠标进入前景样式
        /// </summary>
        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(ModernButton), new PropertyMetadata(Brushes.White));
        /// <summary>
        /// 鼠标按下背景样式
        /// </summary>
        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(ModernButton), new PropertyMetadata(Brushes.DarkBlue));
        /// <summary>
        /// 鼠标按下前景样式（图标、文字）
        /// </summary>
        public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(ModernButton), new PropertyMetadata(Brushes.White));
        /// <summary>
        /// 内容装饰
        /// </summary>
        public static readonly DependencyProperty ContentDecorationsProperty = DependencyProperty.Register("ContentDecorations", typeof(TextDecorationCollection), typeof(ModernButton), new PropertyMetadata(new TextDecorationCollection()));

        /// <summary>
        /// 按钮字体图标编码
        /// </summary>
        public string IconCode
        {
            get { return (string)GetValue(IconCodeProperty); }
            set { SetValue(IconCodeProperty, value); }
        }

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
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        /// <summary>
        /// 是否启用Icon图标动画
        /// </summary>
        public bool IconAllowsAnimation
        {
            get { return (bool)GetValue(IconAllowsAnimationProperty); }
            set { SetValue(IconAllowsAnimationProperty, value); }
        }

        /// <summary>
        /// 按钮圆角大小,左上，右上，右下，左下
        /// </summary>

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius )GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

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
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }

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
        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        /// <summary>
        /// 内容装饰
        /// </summary>
        public TextDecorationCollection ContentDecorations
        {
            get { return (TextDecorationCollection )GetValue(ContentDecorationsProperty); }
            set { SetValue(ContentDecorationsProperty, value); }
        }


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ModernButton"/> class.
        /// </summary>
        //public ModernButton()
        //{
        //    this.DefaultStyleKey = typeof(ModernButton);
        //}

        static ModernButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModernButton), new FrameworkPropertyMetadata(typeof(ModernButton)));
        }

        /// <summary>
        /// Gets or sets the ellipse diameter.
        /// </summary>
        public double EllipseDiameter
        {
            get { return (double)GetValue(EllipseDiameterProperty); }
            set { SetValue(EllipseDiameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ellipse stroke thickness.
        /// </summary>
        public double EllipseStrokeThickness
        {
            get { return (double)GetValue(EllipseStrokeThicknessProperty); }
            set { SetValue(EllipseStrokeThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon path data.
        /// </summary>
        /// <value>
        /// The icon path data.
        /// </value>
        public Geometry IconData
        {
            get { return (Geometry)GetValue(IconDataProperty); }
            set { SetValue(IconDataProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon height.
        /// </summary>
        /// <value>
        /// The icon height.
        /// </value>
        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon width.
        /// </summary>
        /// <value>
        /// The icon width.
        /// </value>
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

    }
}