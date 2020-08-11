using FirstFloor.ModernUI.Windows.Controls;
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
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Net;
using FirstFloor.ModernUI.Presentation;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// 主窗体视图逻辑
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.StatusUser = "当前用户：超级管理员";
            this.StatusNetWork = "网络："+Dns.GetHostName();

            this.TabItems = new TabItemCollection
            {
                new TabItemModel ("通用字典",@"/Pages/Main.xaml"),
                new TabItemModel ("数据库管理",@"/Pages/LayoutBasic.xaml"),
                new TabItemModel ("用户管理",@"/Pages/LayoutList.xaml"),
                new TabItemModel ("权限管理",@"/Pages/ControlsModern.xaml"),
            };
        }
    }
}