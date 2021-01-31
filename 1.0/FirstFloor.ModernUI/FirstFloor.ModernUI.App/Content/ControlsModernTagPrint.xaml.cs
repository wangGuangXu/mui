using FirstFloor.ModernUI.App.ViewModels;
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
    /// 得力标签打印
    /// </summary>
    public partial class ControlsModernTagPrint : UserControl
    {
        public ControlsModernTagPrint()
        {
            InitializeComponent();
            this.DataContext = new ModernTagPrintViewModel();
        }
    }
}