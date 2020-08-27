using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.Windows
{
    /// <summary>
    /// 加载内容接口
    /// </summary>
    public interface IContentLoader
    {
        /// <summary>
        /// 从指定的uri异步加载内容 Asynchronously loads content from specified uri
        /// </summary>
        /// <param name="uri">内容uri</param>
        /// <param name="cancellationToken">用于取消加载内容任务的令牌</param>
        /// <returns>加载的内容</returns>
        Task<object> LoadContentAsync(Uri uri, CancellationToken cancellationToken);
    }
}