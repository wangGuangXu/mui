using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// 无法执行的命令实现
    /// </summary>
    public class SampleDisabledCommand : CommandBase
    {
        /// <summary>
        /// 检查命令是否可用的方法
        /// </summary>
        /// <param name="parameter">命令使用的数据。如果命令不需要传递数据，则可以将该对象设置为null</param>
        /// <returns>
        /// 如果可以执行此命令，则为true；否则为false。
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            return false; // 不能执行
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">命令参数</param>
        protected override void OnExecute(object parameter)
        {
            throw new NotSupportedException();
        }
    }
}