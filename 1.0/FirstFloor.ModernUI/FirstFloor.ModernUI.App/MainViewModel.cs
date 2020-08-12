using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// 
    /// </summary>
    public class MainViewModel : NotifyPropertyChanged
    {
        private TabItemCollection tabItems;
        /// <summary>
        /// 内容源
        /// </summary>
        public TabItemCollection TabItems
        {
            get { return tabItems; }
            set
            {
                if (this.tabItems != value)
                {
                    this.tabItems = value;
                    OnPropertyChanged("TabItems");
                }
            }
        }

        public MainViewModel()
        {

        }


    }
}