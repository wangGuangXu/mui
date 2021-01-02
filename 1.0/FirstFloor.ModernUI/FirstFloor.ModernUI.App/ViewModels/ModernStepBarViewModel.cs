using FirstFloor.ModernUI.App.Model;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls.StepBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FirstFloor.ModernUI.App.ViewModels
{
    /// <summary>
    /// 步骤条视图模型
    /// </summary>
    public class ModernStepBarViewModel : NotifyPropertyChanged
    {
        #region 属性
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
        #endregion

        #region 命令

        /// <summary>
        /// 下一步
        /// </summary>
        public RelayCommand<Panel> NextCmd => new Lazy<RelayCommand<Panel>>(() => new RelayCommand<Panel>(Next)).Value;

        /// <summary>
        /// 上一步
        /// </summary>
        public RelayCommand<Panel> PrevCmd => new Lazy<RelayCommand<Panel>>(() => new RelayCommand<Panel>(Prev)).Value; 
        #endregion

        #region 构造函数
        /// <summary>
        /// 步骤条视图模型
        /// </summary>
        public ModernStepBarViewModel()
        {
            DataList = GetStepBars();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取步骤列表
        /// </summary>
        /// <returns></returns>
        private List<StepBarModel> GetStepBars()
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
        /// 下一步
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
        /// 上一步
        /// </summary>
        /// <param name="panel"></param>
        private void Prev(Panel panel)
        {
            foreach (var stepBar in panel.Children.OfType<ModernStepBar>())
            {
                stepBar.Prev();
            }
        } 
        #endregion
    }
}