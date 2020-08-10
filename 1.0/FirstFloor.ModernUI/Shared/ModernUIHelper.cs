using FirstFloor.ModernUI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FirstFloor.ModernUI
{
    /// <summary>
    /// 包含辅助方法用于获取和设置当前进程的DPI感知。提供各种通用帮助器方法
    /// </summary>
    public static class ModernUIHelper
    {
        private static bool? isInDesignMode;

        /// <summary>
        ///确定当前代码是在Visual Studio或Blend等设计时环境中执行
        /// </summary>
        public static bool IsInDesignMode
        {
            get
            {
                if (!isInDesignMode.HasValue) 
                {
                    isInDesignMode = DesignerProperties.GetIsInDesignMode(new DependencyObject());
                }
                return isInDesignMode.Value;
            }
        }

        /// <summary>
        /// 获取当前进程的DPI感知
        /// </summary>
        /// <returns>
        /// DPI对当前进程的感知
        /// </returns>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        public static ProcessDpiAwareness GetDpiAwareness()
        {
            if (OSVersionHelper.IsWindows8Point1OrGreater)
            {
                ProcessDpiAwareness value;
                var result = NativeMethods.GetProcessDpiAwareness(IntPtr.Zero, out value);
                if (result != NativeMethods.S_OK)
                {
                    throw new Win32Exception(result);
                }

                return value;
            }

            if (OSVersionHelper.IsWindowsVistaOrGreater)
            {
                // 使用旧的win32 api查询系统dpi感知
                return NativeMethods.IsProcessDPIAware() ? ProcessDpiAwareness.SystemDpiAware : ProcessDpiAwareness.DpiUnaware;
            }

            // 假设WPF默认
            return ProcessDpiAwareness.SystemDpiAware;
        }

        /// <summary>
        /// 尝试将此过程的dpi意识设置为每个监视器DPI感知 
        /// Attempts to set the DPI awareness of this process to PerMonitorDpiAware
        /// </summary>
        /// <returns>A value indicating whether the DPI awareness has been set to PerMonitorDpiAware.</returns>
        /// <remarks>
        /// <para>
        /// For this operation to succeed the host OS must be Windows 8.1 or greater, and the initial
        /// DPI awareness must be set to DpiUnaware (apply [assembly:DisableDpiAwareness] to application assembly).
        /// </para>
        /// <para>
        /// When the host OS is Windows 8 or lower, an attempt is made to set the DPI awareness to SystemDpiAware (= WPF default). This
        /// effectively revokes the [assembly:DisableDpiAwareness] attribute if set.
        /// </para>
        /// </remarks>
        public static bool TrySetPerMonitorDpiAware()
        {
            var awareness = GetDpiAwareness();

            // initial awareness must be DpiUnaware
            if (awareness == ProcessDpiAwareness.DpiUnaware)
            {
                if (OSVersionHelper.IsWindows8Point1OrGreater)
                {
                    return NativeMethods.SetProcessDpiAwareness(ProcessDpiAwareness.PerMonitorDpiAware) == NativeMethods.S_OK;
                }

                // 使用旧的Win32 API将感知设置为SystemDPiaware
                return NativeMethods.SetProcessDPIAware() == NativeMethods.S_OK;
            }

            // 如果已启用监视器，则返回true
            return awareness == ProcessDpiAwareness.PerMonitorDpiAware;
        }
    }
}