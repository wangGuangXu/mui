using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ModernGrowlWindow:Window
    {
        /// <summary>
        /// 
        /// </summary>
        internal Panel GrowlPanel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal ModernGrowlWindow()
        {
            //ShowActivated = false;
            //AllowsTransparency = true;
            //WindowStyle = WindowStyle.None;
            //ShowInTaskbar = false;
            //Background = Brushes.Transparent;
            //Topmost = true;
            //UseLayoutRounding = true;

            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;

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