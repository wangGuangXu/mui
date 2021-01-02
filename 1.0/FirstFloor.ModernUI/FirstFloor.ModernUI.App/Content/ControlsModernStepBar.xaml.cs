using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls.StepBar;
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
    /// 步骤条
    /// </summary>
    public partial class ControlsModernStepBar : UserControl
    {
        public ControlsModernStepBar()
        {
            InitializeComponent();

            this.DataContext = new ModernStepBarViewModel();
        }
    }

    public class ModernStepBarViewModel:NotifyPropertyChanged
    {
        private int _stepIndex;

        public int StepIndex
        {
            get => _stepIndex;
#if NET40
            set => Set(nameof(StepIndex), ref _stepIndex, value);
#else
            set => Set(ref _stepIndex, value);
#endif
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        private IList<StepBarModel> _dataList;

        /// <summary>
        /// 数据列表
        /// </summary>
        public IList<StepBarModel> DataList
        {
            get
            {
                return _dataList;
            }
#if NET40
            set => Set(nameof(DataList), ref _dataList, value);
#else
            set => Set(ref _dataList, value);
#endif       
        }

        /// <summary>
        /// 下一步
        /// </summary>
        public RelayCommand<Panel> NextCmd => new Lazy<RelayCommand<Panel>>(() => new RelayCommand<Panel>(Next)).Value;

        /// <summary>
        /// 上一步
        /// </summary>
        public RelayCommand<Panel> PrevCmd => new Lazy<RelayCommand<Panel>>(() => new RelayCommand<Panel>(Prev)).Value;

        /// <summary>
        /// 
        /// </summary>
        public ModernStepBarViewModel()
        {
            DataList = GetStepBarDemoDataList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<StepBarModel> GetStepBarDemoDataList()
        {
            return new List<StepBarModel>
            {
                new StepBarModel 
                {
                    Header = "步骤",
                    Content = "注册"
                },
                new StepBarModel
                {
                    Header = "步骤",
                    Content = "基础信息"
                },
                new StepBarModel
                {
                    Header = "步骤",
                    Content = "上传文件"
                },
                new StepBarModel
                {
                    Header = "步骤",
                    Content = "完成"
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        private void Next(Panel panel)
        {
            foreach (var stepBar in panel.Children.OfType<ModernStepBar>())
            {
                stepBar.Next();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        private void Prev(Panel panel)
        {
            foreach (var stepBar in panel.Children.OfType<ModernStepBar>())
            {
                stepBar.Prev();
            }
        }
    }

    public class StepBarModel
    {
        public string Header { get; set; }

        public string Content { get; set; }
    }
}