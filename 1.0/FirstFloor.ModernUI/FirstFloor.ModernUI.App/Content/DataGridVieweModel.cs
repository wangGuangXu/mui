using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FirstFloor.ModernUI.App.Content
{
    public class DataGridVieweModel : NotifyPropertyChanged
    {
        #region 命令

        /// <summary>
        /// 首页命令
        /// </summary>
        public ICommand FirstPageCommand { get; set; }
        /// <summary>
        /// 上页命令
        /// </summary>
        public ICommand PreviousPageCommand { get; set; }
        /// <summary>
        /// 下页命令
        /// </summary>
        public ICommand NextPageCommand { get; set; }
        /// <summary>
        /// 末页命令
        /// </summary>
        public ICommand LastPageCommand { get; set; }

        #endregion

        #region 依赖属性
        private int _pageSize;
        private int _currentPage;
        private int _totalPage;
        private List<Customer> _source;
        private ObservableCollection<Customer> customers;

        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    OnPropertyChanged(() => this.PageSize);
                }
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(() => this.CurrentPage);
                }
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged(() => this.TotalPage);
            }
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                if (value == null) return;

                customers = value;
                OnPropertyChanged(() => this.Customers);
            }
        }

        #endregion

        public DataGridVieweModel()
        {
            CurrentPage = 1;
            PageSize = 5;

            _source = GetDatas();
            
            TotalPage = (_source.Count % PageSize) > 0 ? (_source.Count / PageSize) + 1 : _source.Count / PageSize;

            List<Customer> result = _source.Take(20).ToList();

            Customers = new ObservableCollection<Customer>(result);

            FirstPageCommand = new RelayCommand((o)=> { FirstPageAction(); });
            PreviousPageCommand = new RelayCommand((o)=> { PreviousPageAction(); });
            NextPageCommand = new RelayCommand((o) => { NextPageAction(); });
            LastPageCommand = new RelayCommand((o) => { LastPageAction(); });
        }

        #region 方法

        /// <summary>
        /// 首页
        /// </summary>
        private void FirstPageAction()
        {
            CurrentPage = 1;

            var result = _source.Take(PageSize).ToList();

            Customers = new ObservableCollection<Customer>(result);
        }

        /// <summary>
        /// 上页
        /// </summary>
        private void PreviousPageAction()
        {
            if (CurrentPage == 1)
            {
                return;
            }

            var result = new List<Customer>();

            if (CurrentPage == 2)
            {
                result = _source.Take(_pageSize).ToList();
            }
            else
            {
                result = _source.Skip((CurrentPage - 2) * _pageSize).Take(_pageSize).ToList();
            }

            Customers = new ObservableCollection<Customer>(result);
            CurrentPage--;
        }

        /// <summary>
        /// 下页
        /// </summary>
        private void NextPageAction()
        {
            if (CurrentPage == TotalPage)
            {
                return;
            }

            List<Customer> result = _source.Skip(CurrentPage * PageSize).Take(PageSize).ToList();

            Customers = new ObservableCollection<Customer>(result);
            CurrentPage++;
        }

        /// <summary>
        /// 末页
        /// </summary>
        private void LastPageAction()
        {
            CurrentPage = TotalPage;

            int skipCount = (TotalPage - 1) * PageSize;
            int takeCount = _source.Count - skipCount;

            var result = _source.Skip(skipCount).Take(takeCount).ToList();

            Customers = new ObservableCollection<Customer>(result);
        }

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns></returns>
        private List<Customer> GetDatas()
        {
            var customers = new List<Customer>
            {
                new Customer { Code = "1001", FirstName = "王健康的接口附近的会计数据都快来发生纠纷离开了九分裤劳动节放假劳动竞赛风口浪尖疯狂<br>夺金六块腹肌砥砺奋进大家都看见多了几分考虑到数据法律框架来看待缴费圣诞节的解放路会计分录收到了两地分居来得及法律的解放路", LastName = "光旭", Email = "orlando0@adventure-works.com", IsMember = true, Status = OrderStatus.New },
                new Customer { Code = "1002", FirstName = "Keith", LastName = "Harris", Email = "keith0@adventure-works.com", IsMember = true, Status = OrderStatus.Received },
                new Customer { Code = "1003", FirstName = "Donna", LastName = "Carreras", Email = "donna0@adventure-works.com", IsMember = false, Status = OrderStatus.None },
                new Customer { Code = "1004", FirstName = "Janet", LastName = "Gates", Email = "janet0@adventure-works.com", IsMember = true, Status = OrderStatus.Shipped },
                new Customer { Code = "1005", FirstName = "Lucy", LastName = "Harrington", Email = "lucy0@adventure-works.com", IsMember = false, Status = OrderStatus.New },
                new Customer { Code = "1006", FirstName = "Rosmarie", LastName = "Carroll", Email = "rosmarie0@adventure-works.com", IsMember = true, Status = OrderStatus.Processing },
                new Customer { Code = "1007", FirstName = "Dominic", LastName = "Gash", Email = "dominic0@adventure-works.com", IsMember = true, Status = OrderStatus.Received },
                new Customer { Code = "1008", FirstName = "Kathleen", LastName = "Garza", Email = "kathleen0@adventure-works.com", IsMember = false, Status = OrderStatus.None },
                new Customer { Code = "1009", FirstName = "Katherine", LastName = "Harding", Email = "katherine0@adventure-works.com", IsMember = true, Status = OrderStatus.Shipped },
                new Customer { Code = "1010", FirstName = "Johnny", LastName = "Caprio", Email = "johnny0@adventure-works.com", IsMember = false, Status = OrderStatus.Processing },
                new Customer { Code = "1011", FirstName = "Johnny", LastName = "Caprio", Email = "johnny0@adventure-works.com", IsMember = false, Status = OrderStatus.Processing },

                new Customer { Code = "1022", FirstName = "方法", LastName = "Harris", Email = "keith0@adventure-works.com", IsMember = true, Status = OrderStatus.Received },
                new Customer { Code = "1023", FirstName = "共和国", LastName = "Carreras", Email = "donna0@adventure-works.com", IsMember = false, Status = OrderStatus.None },
                new Customer { Code = "1024", FirstName = "等等", LastName = "Gates", Email = "janet0@adventure-works.com", IsMember = true, Status = OrderStatus.Shipped },
                new Customer { Code = "1025", FirstName = "Lu等等cy", LastName = "Harrington", Email = "lucy0@adventure-works.com", IsMember = false, Status = OrderStatus.New },
                new Customer { Code = "1026", FirstName = "开机即可", LastName = "Carroll", Email = "rosmarie0@adventure-works.com", IsMember = true, Status = OrderStatus.Processing },
                new Customer { Code = "1027", FirstName = "十点多", LastName = "Gash", Email = "dominic0@adventure-works.com", IsMember = true, Status = OrderStatus.Received },
                new Customer { Code = "1028", FirstName = "地方", LastName = "Garza", Email = "kathleen0@adventure-works.com", IsMember = false, Status = OrderStatus.None },
                new Customer { Code = "1029", FirstName = "天天", LastName = "Harding", Email = "katherine0@adventure-works.com", IsMember = true, Status = OrderStatus.Shipped },
                new Customer { Code = "1020", FirstName = "研究院", LastName = "Caprio", Email = "johnny0@adventure-works.com", IsMember = false, Status = OrderStatus.Processing },
                new Customer { Code = "1021", FirstName = " 苟富贵", LastName = "Caprio", Email = "johnny0@adventure-works.com", IsMember = false, Status = OrderStatus.Processing },

                new Customer { Code = "1033", FirstName = "会更好", LastName = "Carreras", Email = "donna0@adventure-works.com", IsMember = false, Status = OrderStatus.None },
                new Customer { Code = "1034", FirstName = "交换机", LastName = "Gates", Email = "janet0@adventure-works.com", IsMember = true, Status = OrderStatus.Shipped },
                new Customer { Code = "1035", FirstName = "电放费", LastName = "Harrington", Email = "lucy0@adventure-works.com", IsMember = false, Status = OrderStatus.New },
                new Customer { Code = "1036", FirstName = "水电费", LastName = "Carroll", Email = "rosmarie0@adventure-works.com", IsMember = true, Status = OrderStatus.Processing },
                new Customer { Code = "1037", FirstName = "花椒", LastName = "Gash", Email = "dominic0@adventure-works.com", IsMember = true, Status = OrderStatus.Received },
                new Customer { Code = "1038", FirstName = "IIS", LastName = "Garza", Email = "kathleen0@adventure-works.com", IsMember = false, Status = OrderStatus.None },
                new Customer { Code = "1039", FirstName = "投入人体", LastName = "Harding", Email = "katherine0@adventure-works.com", IsMember = true, Status = OrderStatus.Shipped },
                new Customer { Code = "1030", FirstName = "问问", LastName = "Caprio", Email = "johnny0@adventure-works.com", IsMember = false, Status = OrderStatus.Processing },
                new Customer { Code = "1031", FirstName = " 微微", LastName = "Caprio", Email = "johnny0@adventure-works.com", IsMember = false, Status = OrderStatus.Processing }
            };

            return customers;
        }

        private ObservableCollection<Customer> GetData()
        {
            var customers = new ObservableCollection<Customer>();
            customers.Add(new Customer { FirstName = "Orlando", LastName = "Gee", Email = "orlando0@adventure-works.com", IsMember = true, Status = OrderStatus.New });
            customers.Add(new Customer { FirstName = "Keith", LastName = "Harris", Email = "keith0@adventure-works.com", IsMember = true, Status = OrderStatus.Received });
            customers.Add(new Customer { FirstName = "Donna", LastName = "Carreras", Email = "donna0@adventure-works.com", IsMember = false, Status = OrderStatus.None });
            customers.Add(new Customer { FirstName = "Janet", LastName = "Gates", Email = "janet0@adventure-works.com", IsMember = true, Status = OrderStatus.Shipped });
            customers.Add(new Customer { FirstName = "Lucy", LastName = "Harrington", Email = "lucy0@adventure-works.com", IsMember = false, Status = OrderStatus.New });
            customers.Add(new Customer { FirstName = "Rosmarie", LastName = "Carroll", Email = "rosmarie0@adventure-works.com", IsMember = true, Status = OrderStatus.Processing });
            customers.Add(new Customer { FirstName = "Dominic", LastName = "Gash", Email = "dominic0@adventure-works.com", IsMember = true, Status = OrderStatus.Received });
            customers.Add(new Customer { FirstName = "Kathleen", LastName = "Garza", Email = "kathleen0@adventure-works.com", IsMember = false, Status = OrderStatus.None });
            customers.Add(new Customer { FirstName = "Katherine", LastName = "Harding", Email = "katherine0@adventure-works.com", IsMember = true, Status = OrderStatus.Shipped });
            customers.Add(new Customer { FirstName = "Johnny", LastName = "Caprio", Email = "johnny0@adventure-works.com", IsMember = false, Status = OrderStatus.Processing });

            return customers;
        }

        #endregion

    }
}