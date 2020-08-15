using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// 主框架
    /// </summary>
    public class MainViewModel : NotifyPropertyChanged
    {
        #region 属性
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
                    OnPropertyChanged(()=>this.StatusUser);
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
                    OnPropertyChanged(() => this.CurrentTime);
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
                    OnPropertyChanged(() => this.StatusNetWork);
                }
            }
        }

        private string version;
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get { return version; }
            set
            {
                if (this.version != value)
                {
                    this.version = value;
                    OnPropertyChanged(() => this.Version);
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
                    OnPropertyChanged(() => this.TabItems);
                }
            }
        }

        private List<TreeNode> Nodes;

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
                    OnPropertyChanged(() => this.TreeNodes);
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
                    OnPropertyChanged(() => this.SelectedTabItem);
                }
            }
        }

        private List<MenuItem> rightMenu;
        /// <summary>
        /// 右键菜单
        /// </summary>
        public List<MenuItem> RightMenu
        {
            get { return rightMenu; }
            set
            {
                if (this.rightMenu != null)
                {
                    rightMenu = value;
                    OnPropertyChanged(() => this.RightMenu);
                }
            }
        }

        private DispatcherTimer _timer;
        #endregion

        #region 命令
        /// <summary>
        /// 选项卡关闭命令
        /// </summary>
        public ICommand CloseTabCommand { get; set; }
        /// <summary>
        /// 树选择命令
        /// </summary>
        public ICommand SelectedTreeItemChangedCommand { get; set; } 
        /// <summary>
        /// 右键菜单
        /// </summary>
        public ICommand RightContextMenuCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            TabItems = new TabItemCollection();
            StatusUser = "当前用户：超级管理员";
            StatusNetWork = "网络：" + GetIp();
            CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Version = "主程序版本：" + Assembly.GetExecutingAssembly().GetName().Version; //Application.ResourceAssembly.GetName().Version;

            InputData();
            TreeNodes = GetChildNodes(0, Nodes);// Nodes是我已经获得的一组节点

            CloseTabCommand = new RelayCommand(o => CloseTab(o), o => CanCloseTab(o));
            SelectedTreeItemChangedCommand = new RelayCommand(o => SelectedTreeItemChanged(o), o => CanSelectedTreeItemChanged(o));

            var tabItem = new TabItemModel("主页", "/Pages/Home.xaml", CloseTabCommand, Visibility.Hidden);
            TabItems.Add(tabItem);
            SelectedTabItem = tabItem;

            //_timer = new DispatcherTimer();
            //_timer.Tick += OnTimerTick;
            //_timer.Interval = new TimeSpan(0, 0, 1);
            //_timer.Start();
        }

        #region 方法
        /// <summary>
        /// 关闭选项卡
        /// </summary>
        /// <param name="para"></param>
        private void CloseTab(object para)
        {
            var tabItem = para as TabItemModel;
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
            return (para == null) ? false : true;
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
                tabItem = new TabItemModel(treeNode.Name, treeNode.Source, CloseTabCommand,Visibility.Visible);
                TabItems.Add(tabItem);
                SelectedTabItem = tabItem;
            }
        }

        /// <summary>
        /// 根据选择的树节点判断是否可添加选项卡
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
        private List<TreeNode> GetChildNodes(int parentId, List<TreeNode> nodes)
        {
            List<TreeNode> mainNodes = nodes.Where(x => x.ParentId == parentId).ToList();
            List<TreeNode> otherNodes = nodes.Where(x => x.ParentId != parentId).ToList();

            foreach (TreeNode node in mainNodes)
            {
                node.ChildNodes = GetChildNodes(node.Id, otherNodes);
            }
            return mainNodes;
        }

        /// <summary>
        /// 初始化树控件数据
        /// </summary>
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
                new TreeNode(){ParentId=3, Id=7, Name="Section3.1",Source="/Pages/Home.xaml"},
                new TreeNode(){ParentId=6, Id=8, Name="oracle 9.2 ",Source="/Content/ControlsStylesDataGrid.xaml"},
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

        /// <summary>
        /// 获取本地的IP地址
        /// </summary>
        /// <returns></returns>
        string GetIp()
        {
            ///获取本地的IP地址
            string ip = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    ip = _IPAddress.ToString();
                    break;
                }
            }
            return ip;
        }
        #endregion

    }
}