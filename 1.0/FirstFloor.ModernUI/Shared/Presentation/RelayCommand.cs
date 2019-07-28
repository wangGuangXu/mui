using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FirstFloor.ModernUI.Presentation
{
    /// <summary>
    /// 通过调用委托来传递其功能的命令。
    /// </summary>
    public class RelayCommand: CommandBase
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// 初始化RelayCommand类的新实例
        /// </summary>
        /// <param name="execute">执行无参委托</param>
        /// <param name="canExecute">可以执行</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("执行");
            }

            if (canExecute == null)
            {
                // 不可以执行提供的，则始终可执行 no can execute provided, then always executable
                canExecute = (o) => true;
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// 检查命令是否可用的方法
        /// </summary>
        /// <param name="parameter">命令使用的数据。如果命令不需要传递数据，则可以将该对象设置为null</param>
        /// <returns>
        /// 如果可以执行此命令，则为true；否则为false。
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">命令参数</param>
        protected override void OnExecute(object parameter)
        {
            execute(parameter);
        }

    }
}