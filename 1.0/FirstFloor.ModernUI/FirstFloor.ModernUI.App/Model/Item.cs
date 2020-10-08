using FirstFloor.ModernUI.Windows.TreeGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Item : TreeGridElement
    {
        /// <summary>
        /// 
        /// </summary>
        public int Value { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; private set; }

        public Item(string name, int value, string remark, bool hasChildren)
        {
            // Initialize the item
            Name = name;
            Value = value;
            Remark = remark;
            IsExpanded = true;
            HasChildren = hasChildren;
        }

    }
}