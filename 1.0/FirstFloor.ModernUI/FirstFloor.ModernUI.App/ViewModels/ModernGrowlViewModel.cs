using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App.ViewModels
{
    /// <summary>
    /// 信息通知
    /// </summary>
    public class ModernGrowlViewModel
    {
        private readonly string _token;

        public ModernGrowlViewModel()
        {

        }

        public ModernGrowlViewModel(string token)
        {
            _token = token;
        }

        #region 系统窗口内
        
        /// <summary>
        /// 信息
        /// </summary>
        public RelayCommand InfoCmd
        {
            get
            {
                return new Lazy<RelayCommand>(() => new RelayCommand(o => ModernGrowl.Info("信息", _token))).Value;
            }
        }

        /// <summary>
        /// 成功
        /// </summary>
        public RelayCommand SuccessCmd => new Lazy<RelayCommand>(() =>new RelayCommand(o => ModernGrowl.Success("文件保存成功！", _token))).Value;
        /// <summary>
        /// 警告
        /// </summary>
        public RelayCommand WarningCmd
        {
            get
            {
                return new Lazy<RelayCommand>(() =>new RelayCommand(o => ModernGrowl.Warning(new GrowlInfo
                {
                    Message = "磁盘空间快要满了！",
                    CancelStr = "忽略",
                    ActionBeforeClose = isConfirmed =>
                    {
                        ModernGrowl.Info(isConfirmed.ToString());
                        return true;
                    },
                    Token = _token
                }))).Value;
            }
        }
        /// <summary>
        /// 错误
        /// </summary>
        public RelayCommand ErrorCmd => new Lazy<RelayCommand>(() =>new RelayCommand(o => ModernGrowl.Error("连接失败，请检查网络！", _token))).Value;
        /// <summary>
        /// 询问
        /// </summary>
        public RelayCommand AskCmd => new Lazy<RelayCommand>(() =>new RelayCommand(o => ModernGrowl.Ask("检测到有新版本！是否更新？", isConfirmed =>
        {
            ModernGrowl.Info(isConfirmed.ToString());
            return true;
        }, _token))).Value;

        /// <summary>
        /// 致命的
        /// </summary>
        public RelayCommand FatalCmd => new Lazy<RelayCommand>(() =>new RelayCommand(o => ModernGrowl.Fatal(new GrowlInfo
        {
            Message = "程序已崩溃~~~",
            ShowDateTime = false,
            Token = _token
        }))).Value;

        public RelayCommand ClearCmd => new Lazy<RelayCommand>(() => new RelayCommand(o => ModernGrowl.Clear(_token))).Value;

        #endregion

        #region 桌面弹出

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand InfoGlobalCmd => new Lazy<RelayCommand>(() =>new RelayCommand(o => ModernGrowl.InfoGlobal("今天的天气不错~~~"))).Value;

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand SuccessGlobalCmd => new Lazy<RelayCommand>(() =>new RelayCommand(o => ModernGrowl.SuccessGlobal("文件保存成功！"))).Value;

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand WarningGlobalCmd => new Lazy<RelayCommand>(() => new RelayCommand(o => ModernGrowl.WarningGlobal(new GrowlInfo
        {
            Message = "磁盘空间快要满了！",
            CancelStr = "忽略",
            ActionBeforeClose = isConfirmed =>
            {
                ModernGrowl.InfoGlobal(isConfirmed.ToString());
                return true;
            }
        }))).Value;

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand ErrorGlobalCmd => new Lazy<RelayCommand>(() =>new RelayCommand(o => ModernGrowl.ErrorGlobal("连接失败，请检查网络！"))).Value;

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand AskGlobalCmd => new Lazy<RelayCommand>(() => new RelayCommand(o => ModernGrowl.AskGlobal("检测到有新版本！是否更新？", isConfirmed =>
        {
            ModernGrowl.InfoGlobal(isConfirmed.ToString());
            return true;
        }))).Value;

        public RelayCommand FatalGlobalCmd => new Lazy<RelayCommand>(() =>new RelayCommand(o => ModernGrowl.FatalGlobal(new GrowlInfo
        {
            Message = "程序已崩溃~~~",
            ShowDateTime = false
        }))).Value;

        public RelayCommand ClearGlobalCmd => new Lazy<RelayCommand>(() =>new RelayCommand(o=>ModernGrowl.ClearGlobal())).Value;

        #endregion

        ///// <summary>
        ///// 新打开窗口
        ///// </summary>
        //public RelayCommand NewWindowCmd => new Lazy<RelayCommand>(() => new RelayCommand(() => new GrowlDemoWindow
        //{
        //    Owner = Application.Current.MainWindow
        //}.Show())).Value;
    }
}