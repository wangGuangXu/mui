using BarTender;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Application = BarTender.Application;

namespace FirstFloor.ModernUI.App.ViewModels
{
    /// <summary>
    /// 标签打印
    /// 得力DL-888D(New)
    /// </summary>
    public class ModernTagPrintViewModel : NotifyPropertyChanged
    {
        private static Application btApp = new Application();
        private static Format btFormat = new Format();

        private string tagTemplatePath;
        /// <summary>
        /// 标签模板路径
        /// </summary>
        public string TagTemplatePath
        {
            get { return tagTemplatePath; }
            set
            {
                if (this.tagTemplatePath != value)
                {
                    this.tagTemplatePath = value;
                    OnPropertyChanged(() => this.TagTemplatePath);
                }
            }
        }

        /// <summary>
        /// 打印命令
        /// </summary>
        public ICommand PrintCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ModernTagPrintViewModel()
        {
            PrintCommand = new RelayCommand(o => Print(), o => true);
            //TagTemplatePath = AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\标签模板.btw";
            TagTemplatePath = @"D:\111\\文档1.btw";
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="args"></param>
        private void Print()
        {
            try
            {
                //第一个参数设置为模板的路径，在此设置Debug目录下
                btFormat = btApp.Formats.Open(TagTemplatePath, false, "");

                if (string.IsNullOrEmpty(btFormat.Printer))
                {
                    ModernDialog.ShowMessage("请检查打印机驱动设置", "提示", MessageBoxButton.OK);
                    return;
                }

                //设置同序列打印的份数
                btFormat.PrintSetup.IdenticalCopiesOfLabel = 1;

                //设置需要打印的序列数
                btFormat.PrintSetup.NumberSerializedLabels = 1;

                //向BarTender模板传递变量
                btFormat.SetNamedSubStringValue("DishName", "宫保鸡丁");
                btFormat.SetNamedSubStringValue("Price", "388.00");
                btFormat.SetNamedSubStringValue("Count", "1");
                btFormat.SetNamedSubStringValue("TotalPrice", "388.00");
                btFormat.SetNamedSubStringValue("PrintTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //菜品编号
                btFormat.SetNamedSubStringValue("DishNumber", string.Format("GLYT{0}",DateTime.Now.ToString("yyyyMMddHHmmss")));

                //第二个False设置打印时是否跳出打印属性
                int count = btFormat.PrintOut(true, false);

                //退出时是否保存标签
                btFormat.Close(BtSaveOptions.btDoNotSaveChanges);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                btApp.Quit(BtSaveOptions.btDoNotSaveChanges);
            }
        }
    }
}