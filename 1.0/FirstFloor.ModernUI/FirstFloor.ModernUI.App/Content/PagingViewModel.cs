using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App.Pages
{
    /// <summary>
    /// 分页视图
    /// </summary>
    public class PagingViewModel
    {
        private int pageIndex;
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        private int totalPage;
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get { return totalPage; }
            set { totalPage = value; }
        }

    }
}