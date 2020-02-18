using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 使用默认现代UI元素样式的DataGrid文本列
    /// A DataGrid text column using default Modern UI element styles.
    /// </summary>
    public class DataGridTextColumn : System.Windows.Controls.DataGridTextColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataGridTextColumn"/> class.
        /// </summary>
        public DataGridTextColumn()
        {
            this.ElementStyle = Application.Current.Resources["DataGridTextStyle"] as Style;
            this.EditingElementStyle = Application.Current.Resources["DataGridEditingTextStyle"] as Style;
        }
    }
}