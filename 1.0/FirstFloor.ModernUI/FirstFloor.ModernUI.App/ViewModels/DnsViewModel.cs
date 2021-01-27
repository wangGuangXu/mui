using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App.ViewModels
{
    /// <summary>
    /// Dns输入框视图模型
    /// </summary>
    public class DnsViewModel:NotifyPropertyChanged
    {
        public event EventHandler DnsChanged;

        private bool _isPartOneFocused;

        public bool IsPartOneFocused
        {
            get { return _isPartOneFocused; }
            set 
            {
                _isPartOneFocused = value; 
                OnPropertyChanged(()=>this.IsPartOneFocused); 
            }
        }

        private bool _isPartTwoFocused;

        public bool IsPartTwoFocused
        {
            get { return _isPartTwoFocused; }
            set
            {
                _isPartTwoFocused = value;
                OnPropertyChanged(() => this.IsPartTwoFocused);
            }
        }

        private bool _isPartThreeFocused;

        public bool IsPartThreeFocused
        {
            get { return _isPartThreeFocused; }
            set
            {
                _isPartThreeFocused = value;
                OnPropertyChanged(() => this.IsPartThreeFocused);
            }
        }

        private bool _isPartFourFocused;

        public bool IsPartFourFocused
        {
            get { return _isPartFourFocused; }
            set
            {
                _isPartFourFocused = value;
                OnPropertyChanged(() => this.IsPartFourFocused);
            }
        }

        private string _part1;
        /// <summary>
        /// 第一部分
        /// </summary>
        public string Part1
        {
            get { return _part1; }
            set 
            {
                _part1 = value;
                SetFocus(true, false, false, false);
                var moveNext = CanMoveNext(ref _part1);
                OnPropertyChanged(()=>this.Part1);
                OnPropertyChanged(nameof(DnsText));
                DnsChanged?.Invoke(this, EventArgs.Empty);
                if (moveNext)
                {
                    SetFocus(false, true, false, false);
                }
            }
        }

        private string _part2;
        /// <summary>
        /// 第二部分
        /// </summary>
        public string Part2
        {
            get { return _part2; }
            set
            {
                _part2 = value;
                SetFocus(false, true, false, false);
                var moveNext = CanMoveNext(ref _part2);
                OnPropertyChanged(() => this.Part2);
                OnPropertyChanged(nameof(DnsText));
                DnsChanged?.Invoke(this, EventArgs.Empty);
                if (moveNext)
                {
                    SetFocus(false, false, true, false);
                }
            }
        }

        private string _part3;
        /// <summary>
        /// 第二部分
        /// </summary>
        public string Part3
        {
            get { return _part3; }
            set
            {
                _part3 = value;
                SetFocus(false, false, true, false);
                var moveNext = CanMoveNext(ref _part3);
                OnPropertyChanged(() => this.Part3);
                OnPropertyChanged(nameof(DnsText));
                DnsChanged?.Invoke(this, EventArgs.Empty);
                if (moveNext)
                {
                    SetFocus(false, false, false, true);
                }
            }
        }

        private string _part4;
        /// <summary>
        /// 第二部分
        /// </summary>
        public string Part4
        {
            get { return _part4; }
            set
            {
                _part4 = value;
                SetFocus(false, true, false, false);
                var moveNext = CanMoveNext(ref _part4);
                OnPropertyChanged(() => this.Part4);
                OnPropertyChanged(nameof(DnsText));
                DnsChanged?.Invoke(this, EventArgs.Empty);
                
            }
        }

        public string DnsText
        {
            get { return $"{Part1??"0"}.{Part2??"0"}.{Part3??"0"}.{Part4??"0"}"; }
        }

        /// <summary>
        /// 设置DNS
        /// </summary>
        /// <param name="dns"></param>
        public void SetDns(string dns)
        {
            if (string.IsNullOrWhiteSpace(dns))
            {
                return;
            }
            var parts = dns.Split('.');
            if (int.TryParse(parts[0], out var num0))
            {
                Part1 = num0.ToString();
            }

            if (int.TryParse(parts[1], out var num1))
            {
                Part2 = num1.ToString();
            }

            if (int.TryParse(parts[2], out var num2))
            {
                Part3 = num2.ToString();
            }

            if (int.TryParse(parts[3], out var num3))
            {
                Part4 = num3.ToString();
            }
        }

        /// <summary>
        /// 是否可以向后移动
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        private bool CanMoveNext(ref string part)
        {
            bool moveNext = false;
            if (string.IsNullOrWhiteSpace(part)==false)
            {
                if (part.Length >= 3)
                {
                    moveNext = true;
                }

                if (part.EndsWith("."))
                {
                    moveNext = true;
                    part = part.Replace(".", "");
                }
            }
            return moveNext;
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <param name="part3"></param>
        /// <param name="part4"></param>
        private void SetFocus(bool part1,bool part2,bool part3,bool part4)
        {
            IsPartOneFocused = part1;
            IsPartTwoFocused = part2;
            IsPartThreeFocused = part3;
            IsPartFourFocused = part4;
        }

    }
}