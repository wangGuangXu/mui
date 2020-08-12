using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FirstFloor.ModernUI.Presentation
{
    /// <summary>
    /// 实现命令的基类
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// 在命令可执行状态发生改变时触发
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 引发执行更改事件
        /// </summary>
        public void OnCanExecuteChanged()
        {
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// 检查命令是否可用的方法
        /// </summary>
        /// <param name="parameter">命令使用的数据。如果命令不需要传递数据，则可以将该对象设置为null。</param>
        /// <returns>
        /// 如果可以执行此命令，则为true；否则为false。
        /// </returns>
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// 命令执行的方法 
        /// </summary>
        /// <param name="parameter">命令使用的数据。如果命令不需要传递数据，则可以将此对象设置为空。</param>
        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            OnExecute(parameter);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">命令参数</param>
        protected abstract void OnExecute(object parameter);
    }
}