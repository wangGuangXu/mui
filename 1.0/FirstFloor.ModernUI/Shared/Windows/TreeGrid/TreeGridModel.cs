using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace FirstFloor.ModernUI.Windows.TreeGrid
{
    /// <summary>
    /// 树表格模型
    /// </summary>
    public class TreeGridModel:ObservableCollection<TreeGridElement>
    {
        /// <summary>
        /// 
        /// </summary>
        public TreeGridFlatModel FlatModel { get; private set; }

        private List<TreeGridElement> itemCache;

        /// <summary>
        /// 
        /// </summary>
        public TreeGridModel()
        {
            // 初始化模型
            itemCache = new List<TreeGridElement>();
            FlatModel = new TreeGridFlatModel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            // 处理事件 Process the event
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    // 处理添加项 Process added item
                    OnRootAdded(args.NewItems[0]);
                    break;
            }
        }

        /// <summary>
        /// 展开
        /// </summary>
        /// <param name="item"></param>
        internal void Expand(TreeGridElement item)
        {
            // 我们需要扩大这个项目吗 Do we need to expand the item?
            if (!FlatModel.ContainsKey(item) || !item.IsExpanded)
            {
                // 我们不需要扩大这个项目 We do not need to expand the item
                return;
            }

            // 清除项目缓存 Clear the item cache
            itemCache.Clear();

            //缓存项的平面子级  Cache the flat children for the item
            CacheFlatChildren(item);

            // 获取项的插入索引 Get the insertion index for the item
            int index = FlatModel.IndexOf(item) + 1;

            // 将平面子元素添加到平面模型中 Add the flat children to the flat model
            FlatModel.PrivateInsertRange(index, itemCache);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        internal void Collapse(TreeGridElement item)
        {
            // 我们需要折叠这个项目吗 Do we need to collapse the item?
            if (!FlatModel.ContainsKey(item))
            {
                // 我们不需要折叠这个项目 We do not need to collapse the item
                return;
            }

            // 获取折叠信息 Get the collapse information
            int index = FlatModel.IndexOf(item) + 1;
            int count = CountFlatChildren(item);

            // 从平面模型中删除项目以折叠它们 Remove the items from the flat model to collapse them
            FlatModel.PrivateRemoveRange(index, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        internal void OnChildAdded(TreeGridElement child)
        {
            // 获取子元素的父元素 Get the parent of the child
            TreeGridElement parent = child.Parent;

            // 检查父节点是否展开 Check if the parent is expanded
            if (!FlatModel.ContainsKey(parent) || !parent.IsExpanded)
            {
                // 我们不需要更新平面模型 We don't need to update the flat model
                return;
            }

            // 在平面模型中查找子级的插入索引s's Find the insertion index for the child into the flat model
            int index = FindFlatInsertionIndex(child);

            // 将子节点插入平面模型 Insert the child into the flat model
            FlatModel.PrivateInsert(index, child);

            // 展开子对象 Expand the child
            Expand(child);
        }

        internal void OnChildReplaced(TreeGridElement oldChild, TreeGridElement child, int index)
        {
        }

        internal void OnChildRemoved(TreeGridElement child)
        {
        }

        internal void OnChildrenRemoved(TreeGridElement parent, IList children)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void OnRootAdded(object item)
        {
            // 验证根项目 Verify the root item
            TreeGridElement root = TreeGridElement.VerifyItem(item);

            //为根设置模型 Set the model for the root
            root.SetModel(this);

            //查找要插入到平面模型中的索引 Find the index for insertion into the flat model
            int index = FindFlatInsertionIndex(root);

            // 将根插入到平面模型 Insert the root into the flat model
            FlatModel.PrivateInsert(index, root);

            // 展开根目录 Expand the root
            Expand(root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void CacheFlatChildren(TreeGridElement item)
        {
            //遍历项目中的所有子项 Iterate through all of the children within the item
            foreach (TreeGridElement child in item.Children)
            {
                //将子元素添加到项缓存中 Add the child to the item cache
                itemCache.Add(child);

                // 是否展开子节点 Is the child expanded?
                if (child.IsExpanded)
                {
                    // 递归地在子节点中缓存子节点 Recursively cache the children within the child
                    CacheFlatChildren(child);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private int CountFlatChildren(TreeGridElement item)
        {
            // 初始化子节点数量 Initialize child count
            int children = item.Children.Count;

            // 遍历每个子节点 Iterate through each child
            foreach (TreeGridElement child in item.Children)
            {
                // 是否展开子节点 Is the child expanded?
                if (child.IsExpanded)
                {
                    // 递归计算子节点数量 Recursively count the children
                    children += CountFlatChildren(child);
                }
            }

            //返回平面子节点数量 Return the number of flat children
            return children;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private int FindFlatInsertionIndex(TreeGridElement item)
        {
            // 获取搜索信息 Get the search information
            TreeGridElement parent = item.Parent;
            IList<TreeGridElement> items = (parent != null) ? parent.Children : this;
            int index = items.IndexOf(item);
            int lastIndex = items.Count - 1;

            //确定该项是否为项中的最后一项 Determine if the item is the last item in the items
            if (index < lastIndex)
            {
                // 返回使用项插入索引 Return the insertion index using the items
                return FlatModel.IndexOf(items[(index + 1)]);
            }
            //验证父元素是否有效 Is the parent valid?
            else if (parent != null)
            {
                // 确定父节点的平面子节点的数量 Determine the number of flat children the parent has
                int children = CountFlatChildren(parent);

                // 使用平面子元素的数量返回插入索引 Return the insertion index using the number of flat children
                return (FlatModel.IndexOf(parent) + children);
            }
            else
            {
                // 返回平面模型数量 Return the flat model count
                return FlatModel.Count;
            }
        }
    }
}