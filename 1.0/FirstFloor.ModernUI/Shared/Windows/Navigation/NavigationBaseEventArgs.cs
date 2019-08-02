using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.Windows.Navigation
{
    /// <summary>
    /// 定义基本导航事件参数
    /// Defines the base navigation event arguments.
    /// </summary>
    public abstract class NavigationBaseEventArgs : EventArgs
    {
        /// <summary>
        /// 获取引发此事件的框架
        /// Gets the frame that raised this event.
        /// </summary>
        public ModernFrame Frame { get; internal set; }
        /// <summary>
        /// 获取要导航到的目标的源uri
        /// Gets the source uri for the target being navigated to.
        /// </summary>
        public Uri Source { get; internal set; }
    }
}
