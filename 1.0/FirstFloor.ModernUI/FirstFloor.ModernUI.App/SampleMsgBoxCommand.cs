using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// 显示消息框的命令实现
    /// </summary>
    public class SampleMsgBoxCommand: CommandBase
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">命令参数</param>
        protected override void OnExecute(object parameter)
        {
            ModernDialog.ShowMessage("通过选择超链接触发的消息框", "Messagebox", MessageBoxButton.OK);
        }

    }
}