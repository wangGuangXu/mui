using FirstFloor.ModernUI.App.Model;
using FirstFloor.ModernUI.Windows.TreeGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstFloor.ModernUI.App.Content
{
    /// <summary>
    /// TreeGrid
    /// </summary>
    public partial class ControlsStylesTreeGrid : UserControl
    {
        /// <summary>
        /// 级别
        /// </summary>
        private const int Levels = 3;
        /// <summary>
        /// 
        /// </summary>
        private const int Roots = 10;
        /// <summary>
        /// 项目级别
        /// </summary>
        private const int ItemsPerLevel = 5;
        /// <summary>
        /// 
        /// </summary>
        private int value;
        private TreeGridModel model;

        public ControlsStylesTreeGrid()
        {
            InitializeComponent();

            // Initialize the model
            InitModel();

            // Set the model for the grid
            grid.ItemsSource = model.FlatModel;
        }

        /// <summary>
        /// 初始化模型
        /// </summary>
        private void InitModel()
        {
            // Create the model
            model = new TreeGridModel();

            // Add a bunch of items at the root
            for (int count = 0; count < Roots; count++)
            {
                // Create the root item
                int num = value++;
                Item root = new Item($"Root {count}", num, $"这是备注字段{num}", true);

                // Add children to the root
                AddChildren(root);

                // Add the root to the model
                model.Add(root);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="level"></param>
        private void AddChildren(Item item, int level = 0)
        {
            // Determine if the item will have children
            bool hasChildren = (level < Levels);

            // Create children for the item
            for (int count = 0; count < ItemsPerLevel; count++)
            {
                // Create the child
                int num = value++;
                Item child = new Item($"Child {count}, Level {level}", num, $"Child {count}, Level {level}这是备注字段{num}", hasChildren);

                // Does the child have children?
                if (hasChildren)
                {
                    // Recursively add children to the child
                    AddChildren(child, (level + 1));
                }

                // Add the child to the item
                item.Children.Add(child);
            }
        }

        //全部展开
        private void btnExpanderAll_Click(object sender, RoutedEventArgs e)
        {
            Expander(model.FlatModel, true);
        }

        //全部收缩
        private void btnCollapsedAll_Click(object sender, RoutedEventArgs e)
        {
            Expander(model.FlatModel, false);
        }

        /// <summary>
        /// 展开或者收缩
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="expand">展开</param>
        private void Expander(ObservableCollection<TreeGridElement> nodes,bool expand)
        {
            if (nodes == null || nodes.Count < 1)
            {
                return;
            }
            try
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    var item = nodes[i];
                    if (item==null)
                    {
                        break;
                    }
                    item.IsExpanded = expand;

                    Expander(item.Children, expand);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}