using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.Windows.Navigation
{
    /// <summary>
    /// 为框架导航事件提供数据 Provides data for frame navigation events.
    /// </summary>
    public class NavigationEventArgs: NavigationBaseEventArgs
    {
        /// <summary>
        /// 获取一个值，该值指示正在发生的导航类型
        /// Gets a value that indicates the type of navigation that is occurring.
        /// </summary>
        public NavigationType NavigationType { get; internal set; }
        /// <summary>
        /// 获取要导航到的目标的内容
        /// Gets the content of the target being navigated to.
        /// </summary>
        public object Content { get; internal set; }
    }
}