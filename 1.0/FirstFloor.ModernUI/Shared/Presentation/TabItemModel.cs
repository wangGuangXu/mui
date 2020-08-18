using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FirstFloor.ModernUI.Presentation
{
    /// <summary>
    /// 
    /// </summary>
    public class TabItemModel : Displayable
    {
        private string header;
        /// <summary>
        /// 选项卡头部
        /// </summary>
        public string Header
        {
            get { return header; }
            set
            {
                if (this.header != value)
                {
                    this.header = value;
                    OnPropertyChanged("Header");
                }
            }
        }

        private Uri source;
        /// <summary>
        /// 内容源
        /// </summary>
        public Uri Source
        {
            get { return source; }
            set
            {
                if (this.source != value)
                {
                    this.source = value;
                    OnPropertyChanged("Source");
                }
            }
        }

        private Visibility btnStatus;
        /// <summary>
        /// 内容源
        /// </summary>
        public Visibility BtnStatus
        {
            get { return btnStatus; }
            set
            {
                if (this.btnStatus != value)
                {
                    this.btnStatus = value;
                    OnPropertyChanged(()=> BtnStatus);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand CloseTabCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TabItemModel()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="url"></param>
        /// <param name="command"></param>
        /// <param name="btnStatus"></param>
        public TabItemModel(string header, string url,ICommand command,Visibility btnStatus)
        {
            this.Header = header;
            this.CloseTabCommand = command;
            this.BtnStatus = btnStatus;
            this.Source = new Uri(url, UriKind.Relative);
        }

    }
}