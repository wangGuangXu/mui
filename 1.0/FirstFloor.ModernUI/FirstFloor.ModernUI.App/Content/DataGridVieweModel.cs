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
            PageSize = 2;

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

            Customers.Clear();
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

            Customers.Clear();
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
                new Customer { Code = "1001", FirstName = "王", LastName = "光旭", Email = "orlando0@adventure-works.com", IsMember = true, Status = OrderStatus.New },
                new Customer { Code = "1002", FirstName = "Keith", LastName = "Harris", Email = "keith0@adventure-works.com", IsMember = true, Status = OrderStatus.Received },
                new Customer { Code = "1003", FirstName = "Donna", LastName = "Carreras", Email = "donna0@adventure-works.com", IsMember = false, Status = OrderStatus.None },
                new Customer { Code = "1004", FirstName = "Janet", LastName = "Gates", Email = "janet0@adventure-works.com", IsMember = true, Status = OrderStatus.Shipped },
                new Customer { Code = "1005", FirstName = "Lucy", LastName = "Harrington", Email = "lucy0@adventure-works.com", IsMember = false, Status = OrderStatus.New },
                new Customer { Code = "1006", FirstName = "Rosmarie", LastName = "Carroll", Email = "rosmarie0@adventure-works.com", IsMember = true, Status = OrderStatus.Processing },
                new Customer { Code = "1007", FirstName = "Dominic", LastName = "Gash", Email = "dominic0@adventure-works.com", IsMember = true, Status = OrderStatus.Received },
                new Customer { Code = "1008", FirstName = "Kathleen", LastName = "Garza", Email = "kathleen0@adventure-works.com", IsMember = false, Status = OrderStatus.None },
                new Customer { Code = "1009", FirstName = "Katherine", LastName = "Harding", Email = "katherine0@adventure-works.com", IsMember = true, Status = OrderStatus.Shipped },
                new Customer { Code = "1010", FirstName = "Johnny", LastName = "Caprio", Email = "johnny0@adventure-works.com", IsMember = false, Status = OrderStatus.Processing },
                new Customer { Code = "1011", FirstName = "Johnny", LastName = "Caprio", Email = "johnny0@adventure-works.com", IsMember = false, Status = OrderStatus.Processing }
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