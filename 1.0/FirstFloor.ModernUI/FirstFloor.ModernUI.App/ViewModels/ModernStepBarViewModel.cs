using FirstFloor.ModernUI.App.Model;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls.StepBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private Visibility spOne;
        /// <summary>
        /// 当前步骤值
        /// </summary>
        public Visibility SpOne
        {
            get { return spOne; }
            set
            {
                spOne = value;
                PropertyChangedCallerMemberName();
            }
        }

        private Visibility spTwo;
        /// <summary>
        /// 当前步骤值
        /// </summary>
        public Visibility SpTwo
        {
            get { return spTwo; }
            set
            {
                spTwo = value;
                PropertyChangedCallerMemberName();
            }
        }

        private Visibility spThree;
        /// <summary>
        /// 当前步骤值
        /// </summary>
        public Visibility SpThree
        {
            get { return spThree; }
            set
            {
                spThree = value;
                PropertyChangedCallerMemberName();
            }
        }

        private Visibility spFour;
        /// <summary>
        /// 当前步骤值
        /// </summary>
        public Visibility SpFour
        {
            get { return spFour; }
            set
            {
                spFour = value;
                PropertyChangedCallerMemberName();
            }
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        private IList<ModernStepBarModel> _dataList;

        /// <summary>
        /// 数据列表
        /// </summary>
        public IList<ModernStepBarModel> DataList
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
            SpOne = Visibility.Visible;
            SpTwo = Visibility.Hidden;
            SpThree = Visibility.Hidden;
            SpFour = Visibility.Hidden;

            DataList = GetStepBars();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取步骤列表
        /// </summary>
        /// <returns></returns>
        private List<ModernStepBarModel> GetStepBars()
        {
            return new List<ModernStepBarModel>
            {
                new ModernStepBarModel
                {
                    Header = "步骤",
                    Content = "注册"
                },
                new ModernStepBarModel
                {
                    Header = "步骤",
                    Content = "基础信息"
                },
                new ModernStepBarModel
                {
                    Header = "步骤",
                    Content = "上传文件"
                },
                new ModernStepBarModel
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
                SetChildViewVisibility(stepBar);
            }
        }

        private void SetChildViewVisibility(ModernStepBar stepBar)
        {
            switch (stepBar.StepIndex)
            {
                case 0:
                    SpOne = Visibility.Visible;
                    SpTwo = Visibility.Hidden;
                    SpThree = Visibility.Hidden;
                    SpFour = Visibility.Hidden;
                    break;
                case 1:
                    SpOne = Visibility.Hidden;
                    SpTwo = Visibility.Visible;
                    SpThree = Visibility.Hidden;
                    SpFour = Visibility.Hidden;
                    break;
                case 2:
                    SpOne = Visibility.Hidden;
                    SpTwo = Visibility.Hidden;
                    SpThree = Visibility.Visible;
                    SpFour = Visibility.Hidden;
                    break;
                case 3:
                    SpOne = Visibility.Hidden;
                    SpTwo = Visibility.Hidden;
                    SpThree = Visibility.Hidden;
                    SpFour = Visibility.Visible;
                    break;
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
                SetChildViewVisibility(stepBar);
            }
        } 
        #endregion
    }
}