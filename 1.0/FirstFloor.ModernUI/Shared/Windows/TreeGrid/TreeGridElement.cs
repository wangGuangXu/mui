using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Text;
using System.Windows;

namespace FirstFloor.ModernUI.Windows.TreeGrid
{
    /// <summary>
    /// 树表格元素
    /// </summary>
    public class TreeGridElement:ContentElement
    {
        private const string NullItemError = "添加到集合中的项不能为空";

        #region 路由事件
        /// <summary>
        /// 展开中
        /// </summary>
        public static readonly RoutedEvent ExpandingEvent;
        /// <summary>
        /// 展开完
        /// </summary>
        public static readonly RoutedEvent ExpandedEvent;
        /// <summary>
        /// 收起中
        /// </summary>
        public static readonly RoutedEvent CollapsingEvent;
        /// <summary>
        /// 收起完成
        /// </summary>
        public static readonly RoutedEvent CollapsedEvent;

        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler Expanding
        {
            add { AddHandler(ExpandingEvent, value); }
            remove { RemoveHandler(ExpandingEvent, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler Expanded
        {
            add { AddHandler(ExpandedEvent, value); }
            remove { RemoveHandler(ExpandedEvent, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler Collapsing
        {
            add { AddHandler(CollapsingEvent, value); }
            remove { RemoveHandler(CollapsingEvent, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler Collapsed
        {
            add { AddHandler(CollapsedEvent, value); }
            remove { RemoveHandler(CollapsedEvent, value); }
        }
        #endregion

        #region 依赖属性
        /// <summary>
        /// 是否包含子节点
        /// </summary>
        public static readonly DependencyProperty HasChildrenProperty;
        /// <summary>
        /// 是否展开
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty;
        /// <summary>
        /// 层级
        /// </summary>
        public static readonly DependencyProperty LevelProperty;

        /// <summary>
        /// 
        /// </summary>
        public bool HasChildren
        {
            get { return (bool)GetValue(HasChildrenProperty); }
            set { SetValue(HasChildrenProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Level
        {
            get { return (int)GetValue(LevelProperty); }
            private set { SetValue(LevelProperty, value); }
        } 
        #endregion

        /// <summary>
        /// 父对象
        /// </summary>
        public TreeGridElement Parent {  get; private set; }
        /// <summary>
        /// 树表格实体模型
        /// </summary>
        public TreeGridModel Model { get; private set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public ObservableCollection<TreeGridElement> Children { get; private set; }

        static TreeGridElement()
        {
            //注册依赖属性
            HasChildrenProperty= DependencyProperty.Register("HasChildren", typeof(bool), typeof(TreeGridElement), new FrameworkPropertyMetadata() { DefaultValue = false });
            IsExpandedProperty= DependencyProperty.Register("IsExpanded", typeof(bool), typeof(TreeGridElement), new FrameworkPropertyMetadata(){ DefaultValue = false,PropertyChangedCallback = OnIsExpandedChanged });
            LevelProperty = DependencyProperty.Register("Level", typeof(int), typeof(TreeGridElement), new FrameworkPropertyMetadata() { DefaultValue = 0 });

            //注册路由事件
            ExpandingEvent= EventManager.RegisterRoutedEvent(nameof(Expanding), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TreeGridElement));
            ExpandedEvent = EventManager.RegisterRoutedEvent(nameof(ExpandedEvent), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TreeGridElement));
            CollapsingEvent = EventManager.RegisterRoutedEvent(nameof(CollapsingEvent), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TreeGridElement));
            CollapsedEvent = EventManager.RegisterRoutedEvent(nameof(CollapsedEvent), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TreeGridElement));
        }

        /// <summary>
        /// 
        /// </summary>
        public TreeGridElement()
        {
            //初始化元素
            Children = new ObservableCollection<TreeGridElement>();

            //附加事件
            Children.CollectionChanged += OnChildrenChanged;
        }

        /// <summary>
        /// 已展开改变事件
        /// </summary>
        /// <param name="element"></param>
        /// <param name="args"></param>
        private static void OnIsExpandedChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            // 获取树项 Get the tree item
            TreeGridElement item = (TreeGridElement)element;

            // Is the item being expanded?
            if ((bool)args.NewValue)
            {
                // Raise expanding event
                item.RaiseEvent(new RoutedEventArgs(ExpandingEvent, item));

                // Execute derived expanding handler
                item.OnExpanding();

                // Expand the item in the model
                item.Model?.Expand(item);

                // Raise expanded event
                item.RaiseEvent(new RoutedEventArgs(ExpandedEvent, item));

                // Execute derived expanded handler
                item.OnExpanded();
            }
            else
            {
                // Raise collapsing event
                item.RaiseEvent(new RoutedEventArgs(CollapsingEvent, item));

                // Execute derived collapsing handler
                item.OnCollapsing();

                // Collapse the item in the model
                item.Model?.Collapse(item);

                // Raise collapsed event
                item.RaiseEvent(new RoutedEventArgs(CollapsedEvent, item));

                // Execute derived collapsed handler
                item.OnCollapsed();
            }
        }

        private void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            // Process the event
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    // Process added child
                    OnChildAdded(args.NewItems[0]);
                    break;

                case NotifyCollectionChangedAction.Replace:

                    // Process replaced child
                    OnChildReplaced((TreeGridElement)args.OldItems[0], args.NewItems[0], args.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:

                    // Process removed child
                    OnChildRemoved((TreeGridElement)args.OldItems[0]);
                    break;

                case NotifyCollectionChangedAction.Reset:

                    // Process cleared children
                    OnChildrenCleared(args.OldItems);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void OnChildAdded(object item)
        {
            // Verify the new child
            TreeGridElement child = VerifyItem(item);

            // Set the model for the child
            child.SetModel(Model, this);

            // Notify the model that a child was added to the item
            Model?.OnChildAdded(child);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldChild"></param>
        /// <param name="item"></param>
        /// <param name="index"></param>
        private void OnChildReplaced(TreeGridElement oldChild, object item, int index)
        {
            // Verify the new child
            TreeGridElement child = VerifyItem(item);

            // Clear the model for the old child
            oldChild.SetModel(null);

            // Notify the model that a child was replaced
            Model?.OnChildReplaced(oldChild, child, index);
        }

        private void OnChildRemoved(TreeGridElement child)
        {
            // Clear the model for the child
            child.SetModel(null);

            // Notify the model that a child was removed from the item
            Model?.OnChildRemoved(child);
        }

        private void OnChildrenCleared(IList children)
        {
            // 遍历所有子项 Iterate through all of the children
            foreach (TreeGridElement child in children)
            {
                // 为孩子清除模型  Clear the model for the child
                child.SetModel(null);
            }

            // 通知模型已从项中删除所有子项   Notify the model that all of the children were removed from the item
            Model?.OnChildrenRemoved(this, children);
        }

        /// <summary>
        /// 验证项目
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static TreeGridElement VerifyItem(object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), NullItemError);
            }

            // 返回元素
            return (TreeGridElement)item;
        }

        /// <summary>
        /// 设置模型
        /// </summary>
        /// <param name="model"></param>
        /// <param name="parent"></param>
        internal void SetModel(TreeGridModel model, TreeGridElement parent = null)
        {
            // 设置元素信息 Set the element information
            Model = model;
            Parent = parent;
            Level = (parent != null) ? parent.Level + 1 : 0;

            // 遍历所有子元素
            foreach (TreeGridElement child in Children)
            {
                // 为孩子设定模型 Set the model for the child
                child.SetModel(model, this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnExpanding()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnExpanded()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnCollapsing()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnCollapsed()
        {
        }

    }
}