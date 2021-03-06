﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace FirstFloor.ModernUI.Windows.TreeGrid
{
    /// <summary>
    /// 
    /// </summary>
    public class TreeGridFlatModel : ObservableCollection<TreeGridElement>
    {
        private const string ModificationError = "用户无法修改集合.";//The collection cannot be modified by the user

        private bool modification;
        private HashSet<TreeGridElement> keys;

        /// <summary>
        /// 
        /// </summary>
        public TreeGridFlatModel()
        {
            // Initialize the model
            keys = new HashSet<TreeGridElement>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal bool ContainsKey(TreeGridElement item)
        {
            // Return a value indicating if the item is within the model
            return keys.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        internal void PrivateInsert(int index, TreeGridElement item)
        {
            // Set the modification flag
            modification = true;

            // Add the item to the model
            Insert(index, item);

            // Add the item to the keys
            keys.Add(item);

            // Clear the modification flag
            modification = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="items"></param>
        internal void PrivateInsertRange(int index, IList<TreeGridElement> items)
        {
            // Set the modification flag
            modification = true;

            // Iterate through all of the children within the items
            foreach (TreeGridElement child in items)
            {
                // Add the child to the model
                Insert(index++, child);

                // Add the child to the keys
                keys.Add(child);
            }

            // Clear the modification flag
            modification = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        internal void PrivateRemoveRange(int index, int count)
        {
            //设置修改标志 Set the modification flag
            modification = true;

            // 迭代所有要从模型中删除的项 Iterate through all of the items to remove from the model
            for (int itemIndex = 0; itemIndex < count; itemIndex++)
            {
                // Remove the item from the keys
                keys.Remove(Items[index]);

                // Remove the item from the model
                RemoveAt(index);
            }

            // Clear the modification flag
            modification = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            // Is the modification flag set?
            if (!modification)
            {
                // The collection is for internal use only
                throw new InvalidOperationException(ModificationError);
            }

            // Call base method
            base.OnCollectionChanged(args);
        }


    }
}