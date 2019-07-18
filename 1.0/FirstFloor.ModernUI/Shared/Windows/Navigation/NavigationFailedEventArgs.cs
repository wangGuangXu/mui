using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Windows.Controls;

namespace FirstFloor.ModernUI.Windows.Navigation
{
    /// <summary>
    /// 为导航失败事件提供数据 Provides data for the <see cref="ModernFrame.NavigationFailed"/> event.
    /// </summary>
    public class NavigationFailedEventArgs : NavigationBaseEventArgs
    {
        /// <summary>
        /// 从失败的导航中获取错误 Gets the error from the failed navigation.
        /// </summary>
        public Exception Error { get; internal set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是否处理了故障事件 Gets or sets a value that indicates whether the failure event has been handled.
        /// </summary>
        /// <remarks>
        /// 如果不处理，错误将显示在引发NavigationFailed事件的ModernFrame中 When not handled, the error is displayed in the ModernFrame raising the NavigationFailed event.
        /// </remarks>
        public bool Handled { get; set; }
    }
}