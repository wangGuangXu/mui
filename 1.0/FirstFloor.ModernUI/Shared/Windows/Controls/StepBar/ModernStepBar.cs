using FirstFloor.ModernUI.Enum;
using FirstFloor.ModernUI.Windows.Animations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace FirstFloor.ModernUI.Windows.Controls.StepBar
{
    /// <summary>
    /// 现代步骤条
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(ModernStepBarItem))]
    [DefaultEvent("StepChanged")]
    [TemplatePart(Name = ElementProgressBarBack, Type = typeof(ProgressBar))]
    public class ModernStepBar : ItemsControl
    {
        /// <summary>
        /// 后退进度条
        /// </summary>
        private ProgressBar _progressBarBack;
        /// <summary>
        /// 原始步骤索引
        /// </summary>
        private int _originStepIndex = -1;

        #region 常数

        private const string ElementProgressBarBack = "PART_ProgressBarBack";

        #endregion

        #region 路由和事件

        /// <summary>
        /// 上一步
        /// </summary>
        public static RoutedCommand PrevCommand { get; } = new RoutedCommand(nameof(PrevCommand), typeof(ModernStepBar));
        /// <summary>
        /// 下一步
        /// </summary>
        public static RoutedCommand NextCommand { get; } = new RoutedCommand(nameof(NextCommand), typeof(ModernStepBar));

        /// <summary>
        /// 步骤改变事件
        /// </summary>
        public static readonly RoutedEvent StepChangedEvent = EventManager.RegisterRoutedEvent("StepChanged", RoutingStrategy.Bubble, typeof(EventHandler<FunctionEventArgs<int>>), typeof(ModernStepBar));
        /// <summary>
        /// 步骤改变事件
        /// </summary>
        [Category("Behavior")]
        public event EventHandler<FunctionEventArgs<int>> StepChanged
        {
            add => AddHandler(StepChangedEvent, value);
            remove => RemoveHandler(StepChangedEvent, value);
        } 
        #endregion

        #region 依赖属性
        /// <summary>
        /// 当前步骤序号
        /// </summary>
        public static readonly DependencyProperty StepIndexProperty = DependencyProperty.Register("StepIndex", typeof(int), typeof(ModernStepBar), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnStepIndexChangedCallBack, CoerceStepIndexCallBack));

        /// <summary>
        /// 步骤条停靠方式
        /// </summary>
        public Dock Dock
        {
            get { return (Dock)GetValue(DockProperty); }
            set { SetValue(DockProperty, value); }
        }

        /// <summary>
        /// 步骤条停靠方式
        /// </summary>
        public static readonly DependencyProperty DockProperty = DependencyProperty.Register("Dock", typeof(Dock), typeof(ModernStepBar), new PropertyMetadata(Dock.Top));
        #endregion

        #region 构造函数
        /// <summary>
        /// 现代步骤条
        /// </summary>
        public ModernStepBar()
        {
            //绑定命令
            CommandBindings.Add(new CommandBinding(NextCommand, (s, e) => Next()));
            CommandBindings.Add(new CommandBinding(PrevCommand, (s, e) => Prev()));

            ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        } 
        #endregion

        /// <summary>
        /// 项容器生成状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            if (ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated || Items.Count < 1)
            {
                return;
            }

            for (var i = 0; i < Items.Count; i++)
            {
                if (ItemContainerGenerator.ContainerFromIndex(i) is ModernStepBarItem stepBarItem)
                {
                    stepBarItem.Index = i + 1;
                }
            }

            if (_originStepIndex > 0)
            {
                StepIndex = _originStepIndex;
                _originStepIndex = -1;
            }
            else
            {
                OnStepIndexChanged(StepIndex);
            }
        }

        /// <summary>
        /// 步骤索引
        /// </summary>
        public int StepIndex
        {
            get { return (int)GetValue(StepIndexProperty); }
            set { SetValue(StepIndexProperty, value); }
        }

        /// <summary>
        /// 步骤索引改变回调函数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnStepIndexChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ModernStepBar)d;
            control.OnStepIndexChanged((int)e.NewValue);
        }

        /// <summary>
        /// 步骤索引改变
        /// </summary>
        /// <param name="stepIndex"></param>
        private void OnStepIndexChanged(int stepIndex)
        {
            for (int i = 0; i < stepIndex; i++)
            {
                if (ItemContainerGenerator.ContainerFromIndex(i) is ModernStepBarItem stepItemFinished)
                {
                    stepItemFinished.Status = ModernStepStatus.Complete;
                }
            }

            for (var i = stepIndex + 1; i < Items.Count; i++)
            {
                if (ItemContainerGenerator.ContainerFromIndex(i) is ModernStepBarItem stepItemFinished)
                {
                    stepItemFinished.Status = ModernStepStatus.Waiting;
                }
            }

            if (ItemContainerGenerator.ContainerFromIndex(stepIndex) is ModernStepBarItem stepItemSelected)
            {
                stepItemSelected.Status = ModernStepStatus.UnderWay;
            }

            _progressBarBack?.BeginAnimation(RangeBase.ValueProperty, AnimationHelper.CreateAnimation(stepIndex));
        }

        /// <summary>
        /// 强制执行索引回调函数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        private static object CoerceStepIndexCallBack(DependencyObject d, object baseValue)
        {
            var ctl = (ModernStepBar)d;
            var stepIndex = (int)baseValue;
            if (ctl.Items.Count == 0 && stepIndex > 0)
            {
                ctl._originStepIndex = stepIndex;
                return 0;
            }

            return stepIndex < 0 
                ? 0 
                : stepIndex >= ctl.Items.Count 
                ? ctl.Items.Count == 0 ? 0 : ctl.Items.Count - 1 
                : baseValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ModernStepBarItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ModernStepBarItem();
        }

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _progressBarBack = GetTemplateChild(ElementProgressBarBack) as ProgressBar;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var colCount = Items.Count;
            if (_progressBarBack == null || colCount < 1)
            {
                return;
            }

            _progressBarBack.Maximum = colCount - 1;
            _progressBarBack.Value = StepIndex;

            if (Dock == Dock.Top || Dock==Dock.Bottom)
            {
                _progressBarBack.Width = (colCount - 1) * (ActualWidth / colCount);
            }
            else
            {
                _progressBarBack.Height = (colCount - 1) * (ActualHeight / colCount);
            }
        }

        /// <summary>
        /// 下一步
        /// </summary>
        public void Next()
        {
            StepIndex++;
        }

        /// <summary>
        /// 上一步
        /// </summary>
        public void Prev()
        {
            StepIndex--;
        }
    }
}