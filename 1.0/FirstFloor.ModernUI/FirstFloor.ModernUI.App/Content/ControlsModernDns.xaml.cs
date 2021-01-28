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
    /// 
    /// </summary>
    public partial class ControlsModernDns : UserControl
    {
        public ControlsModernDns()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(part1, TextBox_Pasting);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            var vm = this.DataContext as DnsViewModel;
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String pastingText = (String)e.DataObject.GetData(typeof(String));
                vm.SetDns(pastingText);
                part1.Focus();
                e.CancelCommand();
            }
        }

        private void part1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right && part1.CaretIndex == part1.Text.Length)
            {
                part2.Focus();
                e.Handled = true;
            }
            if (e.KeyboardDevice.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.C)
            {
                if (part1.SelectionLength == 0)
                {
                    var vm = this.DataContext as DnsViewModel;
                    Clipboard.SetText(vm.DnsText);
                }
            }
        }

        private void part2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back && part2.Text == "")
            {
                part1.CaretIndex = part1.Text.Length;
                part1.Focus();
            }
            if (e.Key == Key.Right && part2.CaretIndex == part2.Text.Length)
            {
                part3.Focus();
                e.Handled = true;
            }
            if (e.Key == Key.Left && part2.CaretIndex == 0)
            {
                part1.Focus();
                e.Handled = true;
            }

            if (e.KeyboardDevice.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.C)
            {
                if (part2.SelectionLength == 0)
                {
                    var vm = this.DataContext as DnsViewModel;
                    Clipboard.SetText(vm.DnsText);
                }
            }
        }

        private void part3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back && part3.Text == "")
            {
                part2.CaretIndex = part2.Text.Length;
                part2.Focus();
            }
            if (e.Key == Key.Right && part3.CaretIndex == part3.Text.Length)
            {
                part4.Focus();
                e.Handled = true;
            }
            if (e.Key == Key.Left && part3.CaretIndex == 0)
            {
                part2.Focus();
                e.Handled = true;
            }

            if (e.KeyboardDevice.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.C)
            {
                if (part3.SelectionLength == 0)
                {
                    var vm = this.DataContext as DnsViewModel;
                    Clipboard.SetText(vm.DnsText);
                }
            }
        }

        private void part4_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back && part4.Text == "")
            {
                part3.CaretIndex = part3.Text.Length;
                part3.Focus();
            }
            if (e.Key == Key.Left && part4.CaretIndex == 0)
            {
                part3.Focus();
                e.Handled = true;
            }

            if (e.KeyboardDevice.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.C)
            {
                if (part4.SelectionLength == 0)
                {
                    var vm = this.DataContext as DnsViewModel;
                    Clipboard.SetText(vm.DnsText);
                }
            }
        }
    }
}
