using FirstFloor.ModernUI.App.Content;
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
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Series = new List<SeriesData>();
            Errors = new ObservableCollection<TestClass>();
            Warnings = new ObservableCollection<TestClass>();

            Errors.Add(new TestClass() { Category = "全球化", Number = 66 });
            Errors.Add(new TestClass() { Category = "特征", Number = 23 });
            Errors.Add(new TestClass() { Category = "内容类型", Number = 12 });
            Errors.Add(new TestClass() { Category = "正确性", Number = 94 });
            Errors.Add(new TestClass() { Category = "命名", Number = 45 });
            Errors.Add(new TestClass() { Category = "最佳实践", Number = 29 });

            //Warnings.Add(new TestClass() { Category = "全球化", Number = 34 });
            //Warnings.Add(new TestClass() { Category = "特征", Number = 23 });
            //Warnings.Add(new TestClass() { Category = "内容类型", Number = 15 });
            //Warnings.Add(new TestClass() { Category = "正确性", Number = 66 });
            //Warnings.Add(new TestClass() { Category = "命名", Number = 56 });
            //Warnings.Add(new TestClass() { Category = "最佳实践", Number = 34 });

            Series.Add(new SeriesData() { DisplayName = "错误", Items = Errors });
            //Series.Add(new SeriesData() { DisplayName = "警告", Items = Warnings });
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
                NotifyPropertyChanged("SelectedItem");
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

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}