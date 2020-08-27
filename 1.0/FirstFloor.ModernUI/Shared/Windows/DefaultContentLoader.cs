using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FirstFloor.ModernUI.Windows
{
    /// <summary>
    /// 使用Application.LoadComponent加载XAML文件
    /// </summary>
    public class DefaultContentLoader : IContentLoader
    {
        /// <summary>
        /// 异步地从指定的uri加载内容
        /// </summary>
        /// <param name="uri">内容资源地址</param>
        /// <param name="cancellationToken">用于取消加载内容任务的令牌.</param>
        /// <returns>加载的内容</returns>
        public Task<object> LoadContentAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
               throw new InvalidOperationException(Resources.UIThreadRequired);
            }

            // 调度程序确保LoadContent在当前UI线程上执行 scheduler ensures LoadContent is executed on the current UI thread
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            return Task.Factory.StartNew(() => LoadContent(uri), cancellationToken, TaskCreationOptions.None, scheduler);
        }

        /// <summary>
        /// 从指定的URI加载内容
        /// </summary>
        /// <param name="uri">内容资源地址</param>
        /// <returns>加载的内容</returns>
        protected virtual object LoadContent(Uri uri)
        {
            // 在设计模式下不要做任何事情 don't do anything in design mode
            if (ModernUIHelper.IsInDesignMode)
            {
                return null;
            }
            return Application.LoadComponent(uri);
        }
    }
}