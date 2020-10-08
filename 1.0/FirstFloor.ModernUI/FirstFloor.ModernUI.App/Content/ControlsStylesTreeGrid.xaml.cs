using FirstFloor.ModernUI.App.Model;
using FirstFloor.ModernUI.Windows.TreeGrid;
using System;
using System.Collections.Generic;
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
        private const int Levels = 3;
        private const int Roots = 100;
        private const int ItemsPerLevel = 5;

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
            Demo(model,true);
        }

        //全部收缩
        private void btnCollapsedAll_Click(object sender, RoutedEventArgs e)
        {
            Demo(model,false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expand"></param>
        private void Demo(TreeGridElement model,bool expand)
        {
            try
            {
                if (model.Children==null || model.Children.Count<1)
                {
                    return;
                }

                for (int i = 0; i < model.FlatModel.Count; i++)
                {
                    var item = model.FlatModel[i];
                    if (item==null)
                    {
                        break;
                    }
                    item.IsExpanded = expand;

                    Demo(item, expand);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
            //foreach (var item in model.FlatModel)
            //{
            //    item.IsExpanded = expand;
            //    //if (item.Children!=null && item.Children.Count>0)
            //    //{
            //    //    item.IsExpanded = expand;
            //    //}
            //    ////Demo(item.Model,expand);
            //}
        }

    }
}