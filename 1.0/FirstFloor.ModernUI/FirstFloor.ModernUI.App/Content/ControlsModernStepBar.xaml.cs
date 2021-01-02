using FirstFloor.ModernUI.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
}