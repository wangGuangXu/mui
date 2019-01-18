using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI
{
    /// <summary>
    /// 标识每英寸点(dpi)感知值
    /// </summary>
    public enum ProcessDpiAwareness
    {
        /// <summary>
        /// 进程不知道DPI
        /// </summary>
        DpiUnaware = 0,
        /// <summary>
        /// 进程是系统DPI感知的（=WPF默认值）
        /// </summary>
        SystemDpiAware = 1,
        /// <summary>
        /// 进程按监视器DPI感知（仅限Win81+）
        /// </summary>
        PerMonitorDpiAware = 2
    }
}