using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace FirstFloor.ModernUI.Presentation
{
    /// <summary>
    /// 树节点对象
    /// </summary>
    public class TreeNode : INotifyPropertyChanged
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
        /// 地址
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<TreeNode> ChildNodes { get; set; }
        private bool isExpanded;
        /// <summary>
        /// 节点是否展开
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                isExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }
        private bool isSelected;
        /// <summary>
        /// 节点是否选中
        /// </summary>
        public bool IsSelected 
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }
        /// <summary>
        /// 是否分组
        /// </summary>
        public bool IsGrouping { get { return ChildNodes != null && ChildNodes.Count > 0; } }
        /// <summary>
        /// 
        /// </summary>
        public string SurName { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 树节点对象
        /// </summary>
        public TreeNode()
        {
            ChildNodes = new List<TreeNode>();
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}