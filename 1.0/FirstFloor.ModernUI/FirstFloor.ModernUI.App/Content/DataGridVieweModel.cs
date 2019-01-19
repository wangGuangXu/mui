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
        private ICommand firstPageCommand;
        private ICommand previousPageCommand;
        private ICommand nextPageCommand;
        private ICommand lastPageCommand;

        public ICommand FirstPageCommand
        {
            get { return firstPageCommand; }
            set { firstPageCommand = value; }
        }

        public ICommand PreviousPageCommand
        {
            get
            {
                return previousPageCommand;
            }
            set
            {
                previousPageCommand = value;
            }
        }

        public ICommand NextPageCommand
        {
            get
            {
                return nextPageCommand;
            }
            set
            {
                nextPageCommand = value;
            }
        }

        public ICommand LastPageCommand
        {
            get
            {
                return lastPageCommand;
            }
            set
            {
                lastPageCommand = value;
            }
        }

        #endregion

        private int _pageSize;
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

        private int _currentPage;
        private int _totalPage;

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

        private ObservableCollection<Customer> customers;

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

        private List<Customer> _source;

        public DataGridVieweModel()
        {
            CurrentPage = 1;
            _source = GetDatas();
            
            PageSize = 2;
            TotalPage = (_source.Count % PageSize) > 0 ? (_source.Count / PageSize) + 1 : _source.Count / PageSize;

            List<Customer> result = _source.Take(20).ToList();

            Customers = new ObservableCollection<Customer>(result);

            FirstPageCommand = new RelayCommand((o)=> { FirstPageAction(); });
            PreviousPageCommand = new RelayCommand((o)=> { PreviousPageAction(); });
            NextPageCommand = new RelayCommand((o) => { NextPageAction(); });
            LastPageCommand = new RelayCommand((o) => { LastPageAction(); });
        }

        private void FirstPageAction()
        {
            CurrentPage = 1;

            var result = _source.Take(PageSize).ToList();

            Customers.Clear();
            Customers = new ObservableCollection<Customer>(result);
        }

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

        private void LastPageAction()
        {
            CurrentPage = TotalPage;

            int skipCount = (TotalPage - 1) * PageSize;
            int takeCount = _source.Count - skipCount;

            var result = _source.Skip(skipCount).Take(takeCount).ToList();

            Customers = new ObservableCollection<Customer>(result);
        }

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

    }
}