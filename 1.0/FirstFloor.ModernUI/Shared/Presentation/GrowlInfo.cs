using FirstFloor.ModernUI.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstFloor.ModernUI.Presentation
{
    /// <summary>
    /// 现代提示信息实体
    /// </summary>
    public class GrowlInfo
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 显示时间
        /// </summary>
        public bool ShowDateTime { get; set; } = true;
        /// <summary>
        /// 等待时间
        /// </summary>
        public int WaitTime { get; set; } = 6;
        /// <summary>
        /// 取消
        /// </summary>
        public string CancelStr { get; set; } = "取消";
        /// <summary>
        /// 确认
        /// </summary>
        public string ConfirmStr { get; set; } ="确认";
        /// <summary>
        /// 关闭前的操作
        /// </summary>
        public Func<bool, bool> ActionBeforeClose { get; set; }
        /// <summary>
        /// 保持打开状态
        /// </summary>
        public bool StaysOpen { get; set; }
        /// <summary>
        /// 是否自定义
        /// </summary>
        public bool IsCustom { get; set; }
        /// <summary>
        /// 通知信息类型
        /// </summary>
        public ModernInfoType Type { get; set; }
        /// <summary>
        /// 图标编码
        /// </summary>
        public string IconKey { get; set; }
        /// <summary>
        /// 图标背景色
        /// </summary>
        public string IconBrushKey { get; set; }
        /// <summary>
        /// 显示关闭按钮
        /// </summary>
        public bool ShowCloseButton { get; set; } = true;
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }
    }
}