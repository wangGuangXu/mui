using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;

namespace FirstFloor.ModernUI.Windows.Navigation
{
    /// <summary>
    /// 提供方法和事件的数据
    /// Provides data for the <see cref="IContent.OnNavigatingFrom" /> method and the <see cref="ModernFrame.Navigating"/> event.
    /// </summary>
    public class NavigatingCancelEventArgs : NavigationBaseEventArgs
    {
        /// <summary>
        /// 获取一个值，该值指示执行导航的框架是父框架还是框架本身
        /// Gets a value indicating whether the frame performing the navigation is a parent frame or the frame itself.
        /// </summary>
        public bool IsParentFrameNavigating { get; internal set; }
        /// <summary>
        /// 获取一个值，该值指示正在发生的导航类型
        /// Gets a value that indicates the type of navigation that is occurring.
        /// </summary>
        public NavigationType NavigationType { get; internal set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是否应取消事件
        /// Gets or sets a value indicating whether the event should be canceled.
        /// </summary>
        public bool Cancel { get; set; }
    }
}