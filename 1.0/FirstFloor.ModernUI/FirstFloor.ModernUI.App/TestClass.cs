using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// 资料数据
    /// </summary>
    public class SeriesData
    {
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public ObservableCollection<TestClass> Items { get; set; }
    }

    public class TestClass : NotifyPropertyChanged
    {
        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }

        private float _number = 0;
        public float Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged(() => this.Number);
            }
        }
    }
}