using FirstFloor.ModernUI.Windows.Adorners;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace FirstFloor.ModernUI.Windows.Controls.AttachPropertys
{
    /// <summary>
    /// 装饰附加属性
    /// https://www.cnblogs.com/HelloMyWorld/p/3965177.html
    /// </summary>
    public class AdornerAttachProperty
    {
        /// <summary>
        /// 获取有装饰器
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetHasAdorner(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasAdornerProperty);
        }

        /// <summary>
        /// 设置有装饰器
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetHasAdorner(DependencyObject obj, bool value)
        {
            obj.SetValue(HasAdornerProperty, value);
        }

        /// <summary>
        /// 有装饰器
        /// </summary>
        public static readonly DependencyProperty HasAdornerProperty =
            DependencyProperty.RegisterAttached("HasAdorner", typeof(bool), typeof(AdornerAttachProperty), new PropertyMetadata(false, PropertyChangedCallBack));

        /// <summary>
        /// 属性改变回调函数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void PropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                var element = d as Visual;
                if (element == null)
                {
                    return;
                }

                var adornerLayer = AdornerLayer.GetAdornerLayer(element);
                if (adornerLayer == null)
                {
                    return;
                }
                adornerLayer.Add(new ListBoxItemAdorner(element as UIElement));
            }
        }


        /// <summary>
        /// 获取是否显示装饰器
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsShowAdorner(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsShowAdornerProperty);
        }

        /// <summary>
        /// 设置是否显示装饰器
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsShowAdorner(DependencyObject obj, bool value)
        {
            obj.SetValue(IsShowAdornerProperty, value);
        }

        /// <summary>
        /// 是否显示装饰器
        /// </summary>
        public static readonly DependencyProperty IsShowAdornerProperty =
            DependencyProperty.RegisterAttached("IsShowAdorner", typeof(bool), typeof(AdornerAttachProperty), new PropertyMetadata(false, IsShowChangedCallBack));

        /// <summary>
        /// 是否显示改变回调函数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void IsShowChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;
            if (element == null)
            {
                return;
            }

            var adornerLayer = AdornerLayer.GetAdornerLayer(element);
            if (adornerLayer == null)
            {
                return;
            }

            var adorners = adornerLayer.GetAdorners(element);
            if (adorners == null || adorners.Length == 0)
            {
                return;
            }

            var adorner = adorners[0] as ListBoxItemAdorner;
            if (adorner == null)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                adorner.ShowAdorner();
            }
            else
            {
                adorner.HideAdorner();
            }
        }

    }
}