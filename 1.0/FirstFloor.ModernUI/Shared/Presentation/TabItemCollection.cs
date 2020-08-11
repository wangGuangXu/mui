using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FirstFloor.ModernUI.Presentation
{
    /// <summary>
    /// 
    /// </summary>
    public class TabItemCollection:ObservableCollection<TabItemModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public TabItemCollection()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabItems"></param>
        public TabItemCollection(IEnumerable<TabItemModel> tabItems)
        {
            if (tabItems==null)
            {
                throw new ArgumentNullException("tabItems");
            }

            foreach (var item in tabItems)
            {
                Add(item);
            }
        }
    }
}