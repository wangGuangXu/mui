using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.Windows.Navigation
{
    /// <summary>
    /// 框架导航类型
    /// </summary>
    public enum NavigationType
    {
        /// <summary>
        /// 导航到新内容
        /// </summary>
        New,
        /// <summary>
        /// 在向后导航历史记录中向后导航
        /// </summary>
        Back,
        /// <summary>
        /// 重新加载当前内容
        /// </summary>
        Refresh
    }
}