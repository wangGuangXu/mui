using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// 在消息框中显示提供的命令参数的ICommand实现。
    /// </summary>
    public class SampleParameterCommand : CommandBase
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">命令参数</param>
        protected override void OnExecute(object parameter)
        {
            ModernDialog.ShowMessage(string.Format(CultureInfo.CurrentUICulture, "执行命令，命令参数 = '{0}'", parameter), "示例命令", MessageBoxButton.OK);
        }

    }
}