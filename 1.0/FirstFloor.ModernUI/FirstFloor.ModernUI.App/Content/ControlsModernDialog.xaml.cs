﻿using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ControlsModernDialog.xaml
    /// </summary>
    public partial class ControlsModernDialog : UserControl
    {
        public ControlsModernDialog()
        {
            InitializeComponent();
        }

        private void CommonDialog_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ModernDialog {
                Title = "公共对话框Common dialog",
                Content = new LoremIpsum()
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton};
            dlg.ShowDialog();

            this.dialogResult.Text = dlg.DialogResult.HasValue ? dlg.DialogResult.ToString() : "<null>";
            this.dialogMessageBoxResult.Text = dlg.MessageBoxResult.ToString();
        }

        private void MessageDialog_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButton btn = MessageBoxButton.OK;
            if (true == ok.IsChecked) btn = MessageBoxButton.OK;
            else if (true == okcancel.IsChecked) btn = MessageBoxButton.OKCancel;
            else if (true == yesno.IsChecked) btn = MessageBoxButton.YesNo;
            else if (true == yesnocancel.IsChecked) btn = MessageBoxButton.YesNoCancel;


            //你好吗 
            var result = ModernDialog.ShowMessage("你喜欢吗?", "消息对话框", btn);

            this.msgboxResult.Text = result.ToString();
        }
    }
}
