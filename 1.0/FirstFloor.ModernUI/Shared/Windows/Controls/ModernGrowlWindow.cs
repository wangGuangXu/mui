using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 现代信息提示窗口
    /// </summary>
    public sealed class ModernGrowlWindow:Window
    {
        /// <summary>
        /// 提示信息容器
        /// </summary>
        internal Panel GrowlPanel { get; set; }

        /// <summary>
        /// 现代信息提示窗口
        /// </summary>
        internal ModernGrowlWindow()
        {
            GrowlPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 10, 10, 10)
            };

            Content = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                Content = GrowlPanel
            };
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Init()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            Height = desktopWorkingArea.Height;
            Left = desktopWorkingArea.Right - Width;
            Top = 0;
        }
    }
}