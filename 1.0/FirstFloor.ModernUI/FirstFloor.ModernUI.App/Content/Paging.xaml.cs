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
    /// 分页控件交互逻辑
    /// </summary>
    public partial class Paging : UserControl
    {
        #region 依赖属性

        /// <summary>
        /// 当前页
        /// </summary>
        public static readonly DependencyProperty CurrentPageProperty;
        /// <summary>
        /// 总页数
        /// </summary>
        public static readonly DependencyProperty TotalPagePreoperty;

        /// <summary>
        /// 当前页
        /// </summary>
        public string CurrentPage
        {
            get { return (string)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public string TotalPage
        {
            get { return (string)GetValue(TotalPagePreoperty); }
            set { SetValue(TotalPagePreoperty, value); }
        }

        #endregion

        #region 路由事件

        private static RoutedEvent firstPageEvent;
        private static RoutedEvent previousPageEvent;
        private static RoutedEvent nextPageEvent;
        private static RoutedEvent lastPageEvent;

        /// <summary>
        /// 首页
        /// </summary>
        public event RoutedEventHandler FirstPage
        {
            add { AddHandler(firstPageEvent, value); }
            remove { RemoveHandler(firstPageEvent, value); }
        }

        /// <summary>
        /// 上页
        /// </summary>
        public event RoutedEventHandler PreviousPage
        {
            add { AddHandler(previousPageEvent, value); }
            remove { RemoveHandler(previousPageEvent, value); }
        }

        /// <summary>
        /// 下页
        /// </summary>
        public event RoutedEventHandler NextPage
        {
            add { AddHandler(nextPageEvent, value); }
            remove { RemoveHandler(nextPageEvent, value); }
        }

        /// <summary>
        /// 末页
        /// </summary>
        public event RoutedEventHandler LastPage
        {
            add { AddHandler(lastPageEvent, value); }
            remove { RemoveHandler(lastPageEvent, value); }
        }

        #endregion

        public Paging()
        {
            InitializeComponent();
        }

        static Paging()
        {
            //注册自定义路由事件
            firstPageEvent = EventManager.RegisterRoutedEvent("FirstPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(Paging));
            previousPageEvent = EventManager.RegisterRoutedEvent("PreviousPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(Paging));
            nextPageEvent = EventManager.RegisterRoutedEvent("NextPage",RoutingStrategy.Direct,typeof(RoutedEventHandler),typeof(Paging));
            lastPageEvent = EventManager.RegisterRoutedEvent("LastPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(Paging));

            //注册依赖属性
            CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(string), typeof(Paging),new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnCurrentPageChanged)));
            TotalPagePreoperty = DependencyProperty.Register("TotalPage",typeof(string),typeof(Paging),new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnTotalPageChanged)));
        }

        #region 依赖属性回调事件
        /// <summary>
        /// 当前页改变事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d as Paging == null)
            {
                return;
            }

            Run pageIndex = (Run)(d as Paging).FindName("PageIndexLabel");
            pageIndex.Text = e.NewValue.ToString();
        }

        /// <summary>
        /// 总页数改变事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnTotalPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d as Paging == null)
            {
                return;
            }

            Run totalPage = (Run)(d as Paging).FindName("TotalPageLabel");
            totalPage.Text = e.NewValue.ToString();
        }
        #endregion

        /// <summary>
        /// 触发首页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(firstPageEvent, this));
        }

        /// <summary>
        /// 触发上页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(previousPageEvent,this));
        }

        /// <summary>
        /// 触发下页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(nextPageEvent,this));
        }

        /// <summary>
        /// 触发末页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(lastPageEvent, this));
        }
    }
}