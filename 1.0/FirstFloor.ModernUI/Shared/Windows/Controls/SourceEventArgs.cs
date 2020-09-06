using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 提供与uri源相关的事件的数据
    /// Provides data for events related to uri sources.
    /// </summary>
    public class SourceEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceEventArgs"/> class.
        /// </summary>
        /// <param name="source"></param>
        public SourceEventArgs(Uri source)
        {
            this.Source = source;
        }

        /// <summary>
        /// Gets the source uri.
        /// </summary>
        public Uri Source { get; private set; }
    }
}