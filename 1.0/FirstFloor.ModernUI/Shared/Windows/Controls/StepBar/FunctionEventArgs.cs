using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FirstFloor.ModernUI.Windows.Controls.StepBar
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionEventArgs<T>:RoutedEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public T Info { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public FunctionEventArgs(T info)
        {
            Info = info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routedEvent"></param>
        /// <param name="source"></param>
        public FunctionEventArgs(RoutedEvent routedEvent,object source):base(routedEvent,source)
        {

        }
    }
}