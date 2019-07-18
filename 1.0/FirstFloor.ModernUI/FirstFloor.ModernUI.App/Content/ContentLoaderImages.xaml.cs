using FirstFloor.ModernUI.Windows.Controls;
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
    /// Interaction logic for ContentLoaderImages.xaml
    /// </summary>
    public partial class ContentLoaderImages : UserControl
    {
        public ContentLoaderImages()
        {
            InitializeComponent();

            LoadImageLinks();
        }

        /// <summary>
        /// 添加图片链接列表
        /// </summary>
        private async void LoadImageLinks()
        {
            var loader = (FlickrImageLoader)Tab.ContentLoader;

            try {
                // 加载图像链接并分配到选项卡列表
                this.Tab.Links = await loader.GetInterestingnessListAsync();

                // 选择第一个链接
                this.Tab.SelectedSource = this.Tab.Links.Select(l => l.Source).FirstOrDefault();
            }
            catch (Exception e) {
                ModernDialog.ShowMessage(e.Message, "Failure", MessageBoxButton.OK);
            }
        }

    }
}