using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FirstFloor.ModernUI.Windows.Adorners
{
    /// <summary>
    /// 列表项装饰器
    /// </summary>
    public class ListBoxItemAdorner : Adorner
    {
        private VisualCollection _visuals;
        private Canvas _grid;
        private Image _image;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adornedElement"></param>
        public ListBoxItemAdorner(UIElement adornedElement):base(adornedElement)
        {
            _visuals = new VisualCollection(this);
            _image = new Image() { Source = new BitmapImage(new Uri("demo.png", UriKind.RelativeOrAbsolute)), Width = 45, Height = 45 };
            _grid = new Canvas();
            _grid.Children.Add(_image);

            _visuals.Add(_grid);
        }

        /// <summary>
        /// 显示装饰
        /// </summary>
        public void ShowAdorner()
        {
            _image.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 隐藏装饰
        /// </summary>
        public void HideAdorner()
        {
            _image.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 可视化子元素数量
        /// </summary>
        protected override int VisualChildrenCount => _visuals.Count;

        /// <summary>
        /// 获取可视化子元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }

        /// <summary>
        /// 测量大小
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        /// <summary>
        /// 定位子元素并确定大小
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            _grid.Arrange(new Rect(finalSize));

            _image.Margin = new Thickness(finalSize.Width - 12.5, -12.5, 0, 0);

            return base.ArrangeOverride(finalSize); 
        }

    }
}