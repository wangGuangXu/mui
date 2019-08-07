using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.Windows.Navigation
{
    /// <summary>
    /// 为片段导航事件提供数据
    /// Provides data for fragment navigation events.
    /// </summary>
    public class FragmentNavigationEventArgs: EventArgs
    {
        /// <summary>
        /// 获取统一资源标识符（URI）片段
        /// Gets the uniform resource identifier (URI) fragment.
        /// </summary>
        public string Fragment { get; internal set; }
    }
}