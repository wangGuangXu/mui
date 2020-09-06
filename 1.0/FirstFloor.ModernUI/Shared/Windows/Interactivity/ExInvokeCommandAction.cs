using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FirstFloor.ModernUI.Windows.Interactivity
{
    /// <summary>
    /// 扩展的调用命令操作
    /// </summary>
    public class ExInvokeCommandAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// 命令
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ExInvokeCommandAction), null);
        /// <summary>
        /// 命令参数
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =DependencyProperty.Register("CommandParameter", typeof(object), typeof(ExInvokeCommandAction), new PropertyMetadata(0));

        /// <summary>
        /// 获取或设置此操作应调用的命令。这是依赖属性
        /// </summary>
        /// <value>要执行的命令。</value>
        /// <remarks>如果设置了此属性和 CommandName 属性，则此属性将优先于后者。</remarks>
        public ICommand Command
        {
            get { return (ICommand)base.GetValue(CommandProperty); }
            set { base.SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// 获得或设置命令参数。这是依赖属性
        /// </summary>
        /// <value>命令参数。</value>
        /// <remarks>这是传递给 ICommand.CanExecute 和 ICommand.Execute 的值。</remarks>
        public object CommandParameter
        {
            get { return base.GetValue(CommandParameterProperty); }
            set { base.SetValue(CommandParameterProperty, value); }
        }

        private string commandName;
        /// <summary>
        /// 获得或设置此操作应调用的命令的名称
        /// </summary>
        /// <value>此操作应调用的命令的名称。</value>
        /// <remarks>如果设置了此属性和 Command 属性，则此属性将被后者所取代。</remarks>
        public string CommandName
        {
            get
            {
                base.ReadPreamble();
                return this.commandName;
            }
            set
            {
                if (this.CommandName != value)
                {
                    base.WritePreamble();
                    this.commandName = value;
                    base.WritePostscript();
                }
            }
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="parameter">操作的参数。如果操作不需要参数，则可以将参数设置为空引用。</param>
        protected override void Invoke(object parameter)
        {
            if (base.AssociatedObject != null)
            {
                ICommand command = this.ResolveCommand();

                //注意这里添加了事件触发源和事件参数
                ExCommandParameter exParameter = new ExCommandParameter
                {
                    Sender = base.AssociatedObject,
                    Parameter = GetValue(CommandParameterProperty),
                    EventArgs = parameter as EventArgs
                };

                if (command != null && command.CanExecute(exParameter))
                {
                    //注意将扩展的参数传递到Execute方法中
                    command.Execute(exParameter);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ICommand ResolveCommand()
        {
            if (this.Command != null)
            {
                return this.Command;
            }

            ICommand resultCommand = null;
            if (base.AssociatedObject == null)
            {
                return resultCommand;
            }

            Type type = base.AssociatedObject.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] array = propertyInfos;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];

                if (typeof(ICommand).IsAssignableFrom(propertyInfo.PropertyType) && string.Equals(propertyInfo.Name, this.CommandName, StringComparison.Ordinal))
                {
                    resultCommand = (ICommand)propertyInfo.GetValue(base.AssociatedObject, null);
                }
            }
            return resultCommand;
        }
    }
}