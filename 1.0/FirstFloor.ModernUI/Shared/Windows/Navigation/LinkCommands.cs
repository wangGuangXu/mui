using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FirstFloor.ModernUI.Windows.Navigation
{
    /// <summary>
    /// 路由链接命令
    /// The routed link commands.
    /// </summary>
    public static class LinkCommands
    {
        private static RoutedUICommand navigateLink = new RoutedUICommand(Resources.NavigateLink, "NavigateLink", typeof(LinkCommands));

        /// <summary>
        /// 获取导航链接路由命令
        /// Gets the navigate link routed command.
        /// </summary>
        public static RoutedUICommand NavigateLink
        {
            get { return navigateLink; }
        }
    }
}