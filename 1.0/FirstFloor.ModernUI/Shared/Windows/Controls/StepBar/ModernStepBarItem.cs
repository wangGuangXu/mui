using FirstFloor.ModernUI.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FirstFloor.ModernUI.Windows.Controls.StepBar
{
    /// <summary>
    /// 步骤条项
    /// </summary>
    public class ModernStepBarItem: ContentControl
    {
        /// <summary>
        /// 步骤编号
        /// </summary>
        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            internal set { SetValue(IndexProperty, value); }
        }

        /// <summary>
        /// 步骤编号
        /// </summary>
        public static readonly DependencyProperty IndexProperty = DependencyProperty.Register("Index", typeof(int), typeof(ModernStepBarItem), new PropertyMetadata(-1));

        /// <summary>
        /// 步骤状态
        /// </summary>
        public ModernStepStatus Status
        {
            get { return (ModernStepStatus)GetValue(StatusProperty); }
            internal set { SetValue(StatusProperty, value); }
        }

        /// <summary>
        /// 步骤状态
        /// </summary>
        public static readonly DependencyProperty StatusProperty =DependencyProperty.Register("Status", typeof(ModernStepStatus), typeof(ModernStepBarItem), new PropertyMetadata(0));

    }
}