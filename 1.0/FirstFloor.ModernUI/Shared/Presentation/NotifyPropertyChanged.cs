using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.Presentation
{
    /// <summary>
    /// 通知客户端属性更改实现
    /// </summary>
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 引发属性改变事件.（需要硬编码）
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler == null)
            {
                return;
            }
            handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 属性值变化时发生(避免硬编码,并且可避免属性名称写错)  //OnPropertyChanged(()=>this.Stocks);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            this.OnPropertyChanged(propertyName);
        }

#if !NET4
        /// <summary>
        /// 属性值变化时发生 (避免硬编码,并且可避免属性名称写错)
        /// C# 5.0 新增的3个特性中的其一CallerMemberName 用法： 可直接写 OnPropertyChanged();
        /// </summary>
        /// <param name="propertyName">可选属性名，未设置时自动设置为调用者成员名</param>
        protected virtual void PropertyChangedCallerMemberName([CallerMemberName]string propertyName="")
        {
            PropertyChangedEventHandler hander = PropertyChanged;
            if (hander==null)
            {
                return;
            }
            hander(this, new PropertyChangedEventArgs(propertyName));
        }
#endif

#if !NET4
        /// <summary>
        /// 更新指定的值，并在值更改时引发PropertyChanged事件
        /// Updates specified value, and raises the <see cref="PropertyChanged"/> event when the value has changed.
        /// </summary>
        /// <typeparam name="T">值的类型 The type of the value.</typeparam>
        /// <param name="storage">当前存储值 The current stored value</param>
        /// <param name="value">新值 The new value</param>
        /// <param name="propertyName">可选属性名，未设置时自动设置为调用者成员名 The optional property name, automatically set to caller member name when not set.</param>
        /// <returns>指示值是否已更改 Indicates whether the value has changed.</returns>
        protected bool Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (!object.Equals(storage, value)) 
            {
                storage = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
#endif
    }
}