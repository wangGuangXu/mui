using System;
using System.Collections.Generic;
using System.Text;

namespace FirstFloor.ModernUI.Enum
{
    /// <summary>
    /// 现代提示信息类型
    /// </summary>
    public enum ModernInfoType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 信息
        /// </summary>
        Info,
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 致命的
        /// </summary>
        Fatal,
        /// <summary>
        /// 询问
        /// </summary>
        Ask
    }
}