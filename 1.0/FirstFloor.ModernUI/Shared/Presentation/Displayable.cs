using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.Presentation
{
    /// <summary>
    /// 为用户界面中显示的对象提供基本实现
    /// Provides a base implementation for objects that are displayed in the UI.
    /// </summary>
    public abstract class Displayable : NotifyPropertyChanged
    {
        private string displayName;

        /// <summary>
        /// 获取或设置显示名称
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get { return this.displayName; }
            set
            {
                if (this.displayName != value) {
                    this.displayName = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }
    }
}