using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.Win32
{
    /// <summary>
    /// 监控DPI(图像每英寸长度内的像素点数)类型
    /// </summary>
    internal enum MonitorDpiType
    {
        /// <summary>
        /// 有效的DPI
        /// </summary>
        EffectiveDpi = 0,
        /// <summary>
        /// 角点
        /// </summary>
        AngularDpi = 1,
        /// <summary>
        /// 原始DPI
        /// </summary>
        RawDpi = 2,
        /// <summary>
        /// 默认
        /// </summary>
        Default = EffectiveDpi,
    }
}
