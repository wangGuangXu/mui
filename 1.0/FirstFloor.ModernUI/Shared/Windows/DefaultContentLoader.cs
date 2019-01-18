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
        /// <param name="uri">The content uri.</param>
        /// <param name="cancellationToken">用于取消加载内容任务的令牌.</param>
        /// <returns>The loaded content.</returns>
        public Task<object> LoadContentAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
               throw new InvalidOperationException(Resources.UIThreadRequired);
            }
            
            // scheduler ensures LoadContent is executed on the current UI thread
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            return Task.Factory.StartNew(() => LoadContent(uri), cancellationToken, TaskCreationOptions.None, scheduler);
        }

        /// <summary>
        /// 从指定的URI加载内容
        /// </summary>
        /// <param name="uri">The content uri</param>
        /// <returns>The loaded content.</returns>
        protected virtual object LoadContent(Uri uri)
        {
            // don't do anything in design mode
            if (ModernUIHelper.IsInDesignMode)
            {
                return null;
            }
            return Application.LoadComponent(uri);
        }
    }
}
