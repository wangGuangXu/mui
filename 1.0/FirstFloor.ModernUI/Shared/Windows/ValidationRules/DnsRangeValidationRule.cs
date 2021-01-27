using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace FirstFloor.ModernUI.Windows.ValidationRules
{
    /// <summary>
    /// Dns范围验证规则
    /// </summary>
    public class DnsRangeValidationRule : ValidationRule
    {
        private int _min;
        /// <summary>
        /// 最小值
        /// </summary>
        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        private int _max;
        /// <summary>
        /// 最大值
        /// </summary>
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int val = 0;
            string strVal = (string)value;

            try
            {
                if (strVal.Length>0)
                {
                    if (strVal.EndsWith("."))
                    {
                        return CheckRanges(strVal.Replace(".", ""));
                    }

                    //允许点字符移动到下一个框
                    return CheckRanges(strVal);
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "非法字符或" + ex.Message);
            }

            if (val<Min || val>Max)
            {
                return new ValidationResult(false, "请输入范围内的值: " + Min + " - " + Max + ".");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }

        /// <summary>
        /// 检查范围
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private ValidationResult CheckRanges(string strValue)
        {
            if (int.TryParse(strValue, out int result) == false)
            {
                return new ValidationResult(false, "输入非法字符");
            }

            if (result<Min || result>Max)
            {
                return new ValidationResult(false, "请输入范围内的值: " + Min + " - " + Max + ".");
            }
            return ValidationResult.ValidResult;
        }

    }
}