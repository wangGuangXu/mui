using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// 
    /// </summary>
    public class MainViewModel : NotifyPropertyChanged
    {
        private string statusUser;
        /// <summary>
        /// 状态栏登录用户信息
        /// </summary>
        public string StatusUser
        {
            get { return statusUser; }
            set
            {
                if (this.statusUser != value)
                {
                    this.statusUser = value;
                    OnPropertyChanged("StatusUser");
                }
            }
        }

        private string currentTime;
        /// <summary>
        /// 当前时间
        /// </summary>
        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
                if (this.currentTime != value)
                {
                    this.currentTime = value;
                    OnPropertyChanged("CurrentTime");
                }
            }
        }

        private string statusNetWork;
        /// <summary>
        /// 状态栏网络
        /// </summary>
        public string StatusNetWork
        {
            get { return statusNetWork; }
            set
            {
                if (this.statusNetWork != value)
                {
                    this.statusNetWork = value;
                    OnPropertyChanged("StatusNetWork");
                }
            }
        }

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

        private TabItemModel selectedTabItem;
        /// <summary>
        /// 选中的选项卡
        /// </summary>
        public TabItemModel SelectedTabItem
        {
            get { return selectedTabItem; }
            set
            {
                if (this.selectedTabItem != value)
                {
                    this.selectedTabItem = value;
                    OnPropertyChanged("SelectedTabItem");
                }
            }
        }

        private List<TreeNode> treenodes = new List<TreeNode>();
        /// <summary>
        /// 
        /// </summary>
        public List<TreeNode> TreeNodes
        {
            get { return treenodes; }
            set
            {
                if (this.treenodes != value)
                {
                    this.treenodes = value;
                    OnPropertyChanged("TreeNodes");
                }
            }
        }

        /// <summary>
        /// 选项卡关闭命令
        /// </summary>
        public ICommand CloseTabCommand { get; set; }
        /// <summary>
        /// 树选择命令
        /// </summary>
        public ICommand SelectedTreeItemChangedCommand { get; set; }

        private DispatcherTimer _timer;

        public MainViewModel()
        {
            StatusUser = "当前用户：超级管理员";
            StatusNetWork = "网络：" + Dns.GetHostName();

            InputData();

            // Nodes是我已经获得的一组节点
            TreeNodes = getChildNodes(0, Nodes);

            CloseTabCommand = new RelayCommand(o => CloseTab(o), o => CanCloseTab(o));

            SelectedTreeItemChangedCommand= new RelayCommand(o => SelectedTreeItemChanged(o), o => CanSelectedTreeItemChanged(o));

            TabItems = new TabItemCollection
            {
                new TabItemModel ("通用字典","/Pages/Main.xaml",CloseTabCommand),
                new TabItemModel ("数据库管理","/Pages/LayoutBasic.xaml",CloseTabCommand),
                new TabItemModel ("用户管理","/Pages/LayoutList.xaml",CloseTabCommand),
                new TabItemModel ("权限管理","/Pages/ControlsModern.xaml",CloseTabCommand),
            };

            CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _timer = new DispatcherTimer();
            _timer.Tick += OnTimerTick;
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        /// <summary>
        /// 关闭选项卡
        /// </summary>
        /// <param name="para"></param>
        private void CloseTab(object para)
        {
            if (para==null)
            {
                return;
            }

            var header = para.ToString();
            var tabItem = tabItems.FirstOrDefault(a => a.Header == header);
            if (tabItem == null)
            {
                return;
            }
            tabItems.Remove(tabItem);
        }

        /// <summary>
        /// 是否允许关闭选项卡
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        private bool CanCloseTab(object para)
        {
            return true;
        }

        /// <summary>
        /// 树选择
        /// </summary>
        /// <param name="para"></param>
        private void SelectedTreeItemChanged(object para)
        {
            var treeNode = para as TreeNode;
            var tabItem = TabItems.FirstOrDefault(a => a.Header == treeNode.Name);
            if (tabItem == null)
            {
                tabItem = new TabItemModel(treeNode.Name, treeNode.Source, CloseTabCommand);
                TabItems.Add(tabItem);
                SelectedTabItem = tabItem;
            }
        }

        /// <summary>
        /// 是否允许添加选项卡
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        private bool CanSelectedTreeItemChanged(object para)
        {
            if (para == null)
            {
                return false;
            }

            var treeNode = para as TreeNode;
            if (treeNode == null || treeNode.ChildNodes.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="nodes"></param>Name
        /// <returns></returns>
        private List<TreeNode> getChildNodes(int parentId, List<TreeNode> nodes)
        {
            List<TreeNode> mainNodes = nodes.Where(x => x.ParentId == parentId).ToList();
            List<TreeNode> otherNodes = nodes.Where(x => x.ParentId != parentId).ToList();

            foreach (TreeNode node in mainNodes)
            {
                node.ChildNodes = getChildNodes(node.Id, otherNodes);
            }
            return mainNodes;
        }

        private List<TreeNode> Nodes;
        private void InputData()
        {
            Nodes = new List<TreeNode>()
            {
                new TreeNode(){ParentId=0, Id=1, Name = "百度" },
                new TreeNode(){ParentId=0, Id=2, Name="数据库管理"},
                new TreeNode(){ParentId=0,Id=3, Name="Chapter3"},
                new TreeNode(){ParentId=1, Id=4, Name="百度搜索", Source="https://www.baidu.com/"},
                new TreeNode(){ParentId=1, Id=5, Name="百度百科", Source="https://baike.baidu.com/"},
                new TreeNode(){ParentId=2, Id=6, Name="Oracle"},
                new TreeNode(){ParentId=3, Id=7, Name="Section3.1",Source="/Pages/Main.xaml"},
                new TreeNode(){ParentId=6, Id=8, Name="oracle 9.2 ",Source="/Pages/LayoutBasic.xaml"},
                new TreeNode(){ParentId=6, Id=9, Name="SubSection2.1.2",Source="/Pages/ControlsModern.xaml"},
                new TreeNode(){ParentId=2, Id=10,Name="SQL Server",Source="/Pages/Settings.xaml"},
                new TreeNode(){ParentId=3, Id=11, Name="Section3.2",Source="/Pages/LayoutList.xaml"}
            };
        }

        /// <summary>
        /// 显示当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }


    }
}