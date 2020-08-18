using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FirstFloor.ModernUI.Presentation
{
    /// <summary>
    /// 树节点对象
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所属的层级
        /// </summary>
        public int Step { get; set; }
        /// <summary>
        /// 父级的ID
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TreeNode> ChildNodes { get; set; }
        /// <summary>
        /// 节点是否展开
        /// </summary>
        public bool IsExpanded { get; set; }
        /// <summary>
        /// 节点是否选中
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 树节点对象
        /// </summary>
        public TreeNode()
        {
            ChildNodes = new List<TreeNode>();
        }
    }
}