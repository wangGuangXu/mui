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
    public class ModernScrollViewer: ScrollViewer
    {
        /// <summary>
        /// 是否支持惯性
        /// </summary>
        public bool IsInertiaEnabled
        {
            get => (bool)GetValue(IsInertiaEnabledProperty);
            set => SetValue(IsInertiaEnabledProperty, value);
        }

        /// <summary>
        /// 是否支持惯性
        /// </summary>
        public static readonly DependencyProperty IsInertiaEnabledProperty = DependencyProperty.RegisterAttached("IsInertiaEnabled", typeof(bool), typeof(ScrollViewer), new PropertyMetadata(false));

        /// <summary>
        /// 是否支持惯性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetIsInertiaEnabled(DependencyObject element, bool value)
        {
            element.SetValue(IsInertiaEnabledProperty, value);
        }

        /// <summary>
        /// 是否支持惯性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetIsInertiaEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(IsInertiaEnabledProperty);
        }

        /// <summary>
        /// 控件是否可以穿透点击
        /// </summary>
        public bool IsPenetrating
        {
            get => (bool)GetValue(IsPenetratingProperty);
            set => SetValue(IsPenetratingProperty, value);
        }

        /// <summary>
        /// 控件是否可以穿透点击
        /// </summary>
        public static readonly DependencyProperty IsPenetratingProperty = DependencyProperty.RegisterAttached("IsPenetrating", typeof(bool), typeof(ScrollViewer), new PropertyMetadata(false));

        /// <summary>
        /// 控件是否可以穿透点击
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetIsPenetrating(DependencyObject element, bool value)
        {
            element.SetValue(IsPenetratingProperty, value);
        }

        /// <summary>
        /// 控件是否可以穿透点击
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetIsPenetrating(DependencyObject element)
        {
            return (bool)element.GetValue(IsPenetratingProperty);
        }

        /// <summary>
        /// 命中测试
        /// </summary>
        /// <param name="hitTestParameters"></param>
        /// <returns></returns>
        protected override GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters)
        {
            return IsPenetrating ? null : base.HitTestCore(hitTestParameters);
        }
    }
}