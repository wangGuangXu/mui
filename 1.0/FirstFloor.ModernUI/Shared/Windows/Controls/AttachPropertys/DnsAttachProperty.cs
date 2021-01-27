using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FirstFloor.ModernUI.Windows.Controls.AttachPropertys
{
    /// <summary>
    /// 
    /// </summary>
    public static class DnsAttachProperty
    {
        /// <summary>
        /// 获取有装饰器
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        /// <summary>
        /// 设置有装饰器
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        /// <summary>
        /// 有装饰器
        /// </summary>
        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(DnsAttachProperty), new UIPropertyMetadata(false, OnIsFocusedPropertyChangedCallBack));

        /// <summary>
        /// 属性改变回调函数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnIsFocusedPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (UIElement)d;
            if ((bool)e.NewValue)
            {
                control.Focus();
            }
        }
    }
}