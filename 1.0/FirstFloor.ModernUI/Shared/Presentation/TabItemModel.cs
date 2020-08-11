﻿using System;
using System.Collections.Generic;
using System.Text;

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
        public TabItemModel(string header, string url)
        {
            this.Header = header;
            this.Source = new Uri(url, UriKind.Relative);
        }

    }
}