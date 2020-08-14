using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class HomeViewModel : NotifyPropertyChanged
    {
        public HomeViewModel()
        {
            Series = new List<SeriesData>();

            Errors = new ObservableCollection<TestClass>();
            Warnings = new ObservableCollection<TestClass>();

            Errors.Add(new TestClass() { Category = "Globalization", Number = 66 });
            Errors.Add(new TestClass() { Category = "Features", Number = 23 });
            Errors.Add(new TestClass() { Category = "Content Types", Number = 12 });
            Errors.Add(new TestClass() { Category = "Correctness", Number = 94 });
            Errors.Add(new TestClass() { Category = "Naming", Number = 45 });
            Errors.Add(new TestClass() { Category = "Best Practices", Number = 29 });

            Warnings.Add(new TestClass() { Category = "Globalization", Number = 34 });
            Warnings.Add(new TestClass() { Category = "Features", Number = 23 });
            Warnings.Add(new TestClass() { Category = "Content Types", Number = 15 });
            Warnings.Add(new TestClass() { Category = "Correctness", Number = 66 });
            Warnings.Add(new TestClass() { Category = "Naming", Number = 56 });
            Warnings.Add(new TestClass() { Category = "Best Practices", Number = 34 });

            Series.Add(new SeriesData() { DisplayName = "错误", Items = Errors });
            Series.Add(new SeriesData() { DisplayName = "警告", Items = Warnings });
        }

        private object selectedItem = null;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged(()=>this.SelectedItem);
            }
        }

        public List<SeriesData> Series
        {
            get;
            set;
        }

        public ObservableCollection<TestClass> Errors
        {
            get;
            set;
        }

        public ObservableCollection<TestClass> Warnings
        {
            get;
            set;
        }
    }
}