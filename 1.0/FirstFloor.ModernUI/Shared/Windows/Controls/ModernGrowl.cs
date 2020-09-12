using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Interactivity;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using FirstFloor.ModernUI.Enum;
using System.Net;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// 现代淡入淡出信息提示信息控件
    /// </summary>
    [TemplatePart(Name = ElementPanelMore, Type = typeof(Panel))]
    [TemplatePart(Name = ElementGridMain, Type = typeof(Grid))]
    [TemplatePart(Name = ElementButtonClose, Type = typeof(Button))]
    public class ModernGrowl:Control
    {
        //添加部件，wpf约定名称以PART_开头，后跟元素名称，元素名称首字母大写
        private const string ElementGridMain = "PART_GridMain";
        private const string ElementPanelMore = "PART_PanelMore";
        private const string ElementButtonClose = "PART_ButtonClose";
        private static ModernGrowlWindow GrowlWindow;
        /// <summary>
        /// 消息容器
        /// </summary>
        public static Panel GrowlPanel { get; set; }

        private Panel _panelMore;

        private Grid _gridMain;

        private Button _buttonClose;

        private bool _showCloseButton;
        /// <summary>
        /// 保持打开状态
        /// </summary>
        private bool _staysOpen;
        /// <summary>
        /// 等待时间
        /// </summary>
        private int _waitTime = 6;
        /// <summary>
        /// 计数
        /// </summary>
        private int _tickCount;
        /// <summary>
        /// 关闭计时器
        /// </summary>
        private DispatcherTimer _timerClose;

        private static readonly Dictionary<string, Panel> PanelDic = new Dictionary<string, Panel>();
        /// <summary>
        /// 
        /// </summary>
        private Func<bool, bool> ActionBeforeClose { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal static readonly DependencyProperty CancelStrProperty = DependencyProperty.Register("CancelStr", typeof(string), typeof(ModernGrowl), new PropertyMetadata(default(string)));
        /// <summary>
        /// 
        /// </summary>
        internal static readonly DependencyProperty ConfirmStrProperty = DependencyProperty.Register("ConfirmStr", typeof(string), typeof(ModernGrowl), new PropertyMetadata(default(string)));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ShowDateTimeProperty = DependencyProperty.Register("ShowDateTime", typeof(bool), typeof(ModernGrowl), new PropertyMetadata(true));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(ModernGrowl), new PropertyMetadata(default(string)));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(DateTime), typeof(ModernGrowl), new PropertyMetadata(default(DateTime)));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(ModernGrowl), new PropertyMetadata("\ue604"));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconBrushProperty = DependencyProperty.Register("IconBrush", typeof(Brush), typeof(ModernGrowl), new PropertyMetadata(default(Brush)));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(ModernInfoType), typeof(ModernGrowl), new PropertyMetadata(default(ModernInfoType)));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty GrowlParentProperty = DependencyProperty.RegisterAttached("GrowlParent", typeof(bool), typeof(ModernGrowl), new PropertyMetadata(false, OnGrowlParentChanged));

        private static void OnGrowlParentChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && d is Panel panel)
            {
                SetGrowlPanel(panel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TokenProperty = DependencyProperty.RegisterAttached("Token", typeof(string), typeof(ModernGrowl), new PropertyMetadata(default(string), OnTokenChanged));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnTokenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Panel panel)
            {
                if (e.NewValue == null)
                {
                    Unregister(panel);
                }
                else
                {
                    Register(e.NewValue.ToString(), panel);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetToken(DependencyObject element, string value)
        {
            element.SetValue(TokenProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetToken(DependencyObject element)
        {
            return (string)element.GetValue(TokenProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetGrowlParent(DependencyObject element, bool value)
        {
            element.SetValue(GrowlParentProperty,value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetGrowlParent(DependencyObject element)
        {
            return (bool)element.GetValue(GrowlParentProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        public ModernInfoType Type
        {
            get => (ModernInfoType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        internal string CancelStr
        {
            get => (string)GetValue(CancelStrProperty);
            set => SetValue(CancelStrProperty, value);
        }

        internal string ConfirmStr
        {
            get => (string)GetValue(ConfirmStrProperty);
            set => SetValue(ConfirmStrProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowDateTime
        {
            get
            {
                return (bool)GetValue(ShowDateTimeProperty);
            }

            set
            {
                SetValue(ShowDateTimeProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time
        {
            get => (DateTime)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public Brush IconBrush
        {
            get => (Brush)GetValue(IconBrushProperty);
            set => SetValue(IconBrushProperty, value);
        }

        /// <summary>
        /// 信息通知
        /// </summary>
        public ModernGrowl()
        {
            CommandBindings.Add(new CommandBinding(ControlAttachProperty.CloseGrowlCommand, OnButtonClose));
            CommandBindings.Add(new CommandBinding(ControlAttachProperty.CancelGrowlCommand, OnButtonCancel));
            CommandBindings.Add(new CommandBinding(ControlAttachProperty.ConfirmGrowlCommand, OnButtonOk));
        }

        private void OnButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnButtonCancel(object sender, RoutedEventArgs e)
        {
            Close(true,false);
        }

        private void OnButtonOk(object sender, RoutedEventArgs e)
        {
            Close(true);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        private void Close(bool invokeActionBeforeClose = false, bool invokeParam = true)
        {
            if (invokeActionBeforeClose)
            {
                if (ActionBeforeClose?.Invoke(invokeParam) == false)
                {
                    return;
                }
            }

            _timerClose?.Stop();
            var transform = new TranslateTransform();
            _gridMain.RenderTransform = transform;
            var animation = new DoubleAnimation(ActualWidth, new Duration(TimeSpan.FromMilliseconds(200)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            };
            animation.Completed += (s, e) =>
            {
                if (Parent is Panel panel)
                {
                    panel.Children.Remove(this);

                    if (GrowlWindow?.GrowlPanel != null && GrowlWindow.GrowlPanel.Children.Count == 0)
                    {
                        GrowlWindow.Close();
                        GrowlWindow = null;
                    }
                }
            };
            transform.BeginAnimation(TranslateTransform.XProperty, animation);
        }

        /// <summary>
        /// 在模板中查找元素并关联事件处理程序或添加数据绑定表达式
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _panelMore = GetTemplateChild(ElementPanelMore) as Panel;
            _gridMain = GetTemplateChild(ElementGridMain) as Grid;
            _buttonClose = GetTemplateChild(ElementButtonClose) as Button;

            CheckNull();
            Update();
        }

        private void CheckNull()
        {
            if (_panelMore == null || _gridMain == null || _buttonClose == null)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            if (Type == ModernInfoType.Ask)
            {
                _panelMore.IsEnabled = true;
                _panelMore.Visibility = Visibility.Visible;
            }

            var transform = new TranslateTransform { X = MaxWidth };
            _gridMain.RenderTransform = transform;

            var doubleAnimation=new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(200)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            };

            transform.BeginAnimation(TranslateTransform.XProperty, doubleAnimation);
            if (_staysOpen == false)
            {
                StartTimer();
            }
        }

        /// <summary>
        /// 开始计时器
        /// </summary>
        private void StartTimer()
        {
            _timerClose = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timerClose.Tick += delegate
            {
                if (IsMouseOver)
                {
                    _tickCount = 0;
                    return;
                }

                _tickCount++;
                if (_tickCount >= _waitTime)
                {
                    Close(true);
                }
            };
            _timerClose.Start();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="token"></param>
        /// <param name="panel"></param>
        public static void Register(string token, Panel panel)
        {
            if (string.IsNullOrEmpty(token) || panel == null)
            {
                return;
            }

            PanelDic[token] = panel;
            InitGrowlPanel(panel);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="panel"></param>
        public static void Unregister(Panel panel)
        {
            if (panel == null)
            {
                return;
            }

            //var first = PanelDic.FirstOrDefault(item => ReferenceEquals(panel, item.Value));
            //if (!string.IsNullOrEmpty(first.Key))
            //{
            //    PanelDic.Remove(first.Key);
            //    panel.ContextMenu = null;
            //    panel.SetCurrentValue(PanelElement.FluidMoveBehaviorProperty, DependencyProperty.UnsetValue);
            //}
        }

        /// <summary>
        ///     消息容器
        /// </summary>
        /// <param name="panel"></param>
        private static void SetGrowlPanel(Panel panel)
        {
            GrowlPanel = panel;
            InitGrowlPanel(panel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        private static void InitGrowlPanel(Panel panel)
        {
            if (panel == null)
            {
                return;
            }

            //var menuItem = new MenuItem();
            //LangProvider.SetLang(menuItem, HeaderedItemsControl.HeaderProperty, LangKeys.Clear);

            //menuItem.Click += (s, e) =>
            //{
            //    foreach (var item in panel.Children.OfType<Growl>())
            //    {
            //        item.Close();
            //    }
            //};
            //panel.ContextMenu = new ContextMenu
            //{
            //    Items =
            //    {
            //        menuItem
            //    }
            //};

            //PanelElement.SetFluidMoveBehavior(panel, ResourceHelper.GetResource<FluidMoveBehavior>(ResourceToken.BehaviorXY400));
        }

        /// <summary>
        /// 鼠标在时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            _buttonClose.Visibility = _showCloseButton ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// 鼠标离开时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            _buttonClose.Visibility=  Visibility.Collapsed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="growlInfo"></param>
        /// <param name="infoType"></param>
        private static void InitGrowlInfo(ref GrowlInfo growlInfo, ModernInfoType infoType)
        {
            if (growlInfo == null)
            {
                throw new ArgumentNullException(nameof(growlInfo));
            }

            growlInfo.Type = infoType;

            switch (infoType)
            {
                case ModernInfoType.Info:
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = "&#xe723;";
                        growlInfo.IconBrushKey = "#00BCD4";
                    }
                    else
                    {
                        growlInfo.IconKey = "&#xe723;";
                        growlInfo.IconBrushKey = "#00BCD4";
                    }
                    break;
                case ModernInfoType.Success:
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = "&#xe603;";
                        growlInfo.IconBrushKey = "#2DB84D";
                    }
                    else
                    {
                        growlInfo.IconKey = "&#xe603;";
                        growlInfo.IconBrushKey = "#2DB84D";
                    }
                    break;
                case ModernInfoType.Warning:
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = "&#xe62d;";
                        growlInfo.IconBrushKey = "#e9af20";
                    }
                    else
                    {
                        growlInfo.IconKey = "&#xe62d;";
                        growlInfo.IconBrushKey = "#e9af20";
                    }
                    break;
                case ModernInfoType.Error:
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = "&#xe61c;";
                        growlInfo.IconBrushKey = "#DB3340";
                        growlInfo.StaysOpen = true;
                    }
                    else
                    {
                        growlInfo.IconKey = "&#xe61c;";
                        growlInfo.IconBrushKey = "#DB3340";
                    }
                    break;
                case ModernInfoType.Fatal:
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = "&#xe604;";
                        growlInfo.IconBrushKey = "#212121";
                        growlInfo.StaysOpen = true;
                        growlInfo.ShowCloseButton = false;
                        if (GrowlPanel.ContextMenu != null)
                        {
                            GrowlPanel.ContextMenu.Opacity = 0;
                        }
                    }
                    else
                    {
                        growlInfo.IconKey = "&#xe604;";
                        growlInfo.IconBrushKey = "#212121";
                    }
                    break;
                case ModernInfoType.Ask:
                    growlInfo.StaysOpen = true;
                    growlInfo.ShowCloseButton = false;
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = "&#xe88c;";
                        growlInfo.IconBrushKey = "#F8491E";
                    }
                    else
                    {
                        growlInfo.IconKey = "&#xe88c;";
                        growlInfo.IconBrushKey = "#F8491E";
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(infoType), infoType, null);
            }
        }

        /// <summary>
        /// 系统内部显示
        /// </summary>
        /// <param name="growlInfo"></param>
        private static void Show(GrowlInfo growlInfo)
        {
            Application.Current.Dispatcher?.Invoke(
#if NET4
                new Action(
#endif                    
                    () =>
                    {
                        var ctl = new ModernGrowl
                        {
                            Message = growlInfo.Message,
                            Time = DateTime.Now,
                            Icon = WebUtility.HtmlDecode(growlInfo.IconKey) ,
                            IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(growlInfo.IconBrushKey)),
                            _showCloseButton = growlInfo.ShowCloseButton,
                            ActionBeforeClose = growlInfo.ActionBeforeClose,
                            _staysOpen = growlInfo.StaysOpen,
                            ShowDateTime = growlInfo.ShowDateTime,
                            ConfirmStr = growlInfo.ConfirmStr,
                            CancelStr = growlInfo.CancelStr,
                            Type = growlInfo.Type,
                            _waitTime = Math.Max(growlInfo.WaitTime, 2)
                        };
                        if (!string.IsNullOrEmpty(growlInfo.Token))
                        {
                            if (PanelDic.TryGetValue(growlInfo.Token, out var panel))
                            {
                                panel?.Children.Insert(0, ctl);
                            }
                        }
                        else
                        {
                            GrowlPanel?.Children.Insert(0, ctl);
                        }
                    }
#if NET4
                    )
#endif
                );
        }

        /// <summary>
        /// 全局显示(显示分为桌面显示和系统内部显示)
        /// </summary>
        /// <param name="growlInfo"></param>
        private static void ShowGlobal(GrowlInfo growlInfo)
        {
            if (GrowlWindow == null)
            {
                GrowlWindow = new ModernGrowlWindow();
                GrowlWindow.Show();
                InitGrowlPanel(GrowlWindow.GrowlPanel);
                GrowlWindow.Init();
            }

            GrowlWindow.Visibility=Visibility.Visible;

            Application.Current.Dispatcher?.Invoke(
#if NET4
                new Action(
#endif
                    () =>
                    {
                        var ctl = new ModernGrowl
                        {
                            Message = growlInfo.Message,
                            Time = DateTime.Now,
                            Icon = WebUtility.HtmlDecode(growlInfo.IconKey),
                            IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(growlInfo.IconBrushKey)),
                            _showCloseButton = growlInfo.ShowCloseButton,
                            ActionBeforeClose = growlInfo.ActionBeforeClose,
                            _staysOpen = growlInfo.StaysOpen,
                            ShowDateTime = growlInfo.ShowDateTime,
                            ConfirmStr = growlInfo.ConfirmStr,
                            CancelStr = growlInfo.CancelStr,
                            Type = growlInfo.Type,
                            _waitTime = Math.Max(growlInfo.WaitTime, 2)
                        };
                        GrowlWindow.GrowlPanel.Children.Insert(0, ctl);
                    }
#if NET4
                    )
#endif
                );
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public static void Success(string message, string token = "")
        {
            Success(new GrowlInfo
            {
                Message = message,
                Token = token
            });
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Success(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Success);
            Show(growlInfo);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        public static void SuccessGlobal(string message)
        {
            SuccessGlobal(new GrowlInfo
            {
                Message = message
            });
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void SuccessGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Success);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public static void Info(string message, string token = "")
        {
            Info(new GrowlInfo
            {
                Message = message,
                Token = token
            });
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Info(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Info);
            Show(growlInfo);
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="message"></param>
        public static void InfoGlobal(string message)
        {
            InfoGlobal(new GrowlInfo
            {
                Message = message
            });
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void InfoGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Info);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public static void Warning(string message, string token = "")
        {
            Warning(new GrowlInfo
            {
                Message = message,
                Token = token
            });
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Warning(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Warning);
            Show(growlInfo);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        public static void WarningGlobal(string message)
        {
            WarningGlobal(new GrowlInfo
            {
                Message = message
            });
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void WarningGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Warning);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public static void Error(string message, string token = "")
        {
            Error(new GrowlInfo
            {
                Message = message,
                Token = token
            });
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Error(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Error);
            Show(growlInfo);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        public static void ErrorGlobal(string message) => ErrorGlobal(new GrowlInfo
        {
            Message = message
        });

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void ErrorGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Error);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 严重
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public static void Fatal(string message, string token = "")
        {
            Fatal(new GrowlInfo
            {
                Message = message,
                Token = token
            });
        }

        /// <summary>
        /// 严重
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Fatal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Fatal);
            Show(growlInfo);
        }

        /// <summary>
        /// 严重
        /// </summary>
        /// <param name="message"></param>
        public static void FatalGlobal(string message)
        {
            FatalGlobal(new GrowlInfo
            {
                Message = message
            });
        }

        /// <summary>
        /// 严重
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void FatalGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Fatal);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="message"></param>
        /// <param name="actionBeforeClose"></param>
        /// <param name="token"></param>
        public static void Ask(string message, Func<bool, bool> actionBeforeClose, string token = "")
        {
            Ask(new GrowlInfo
            {
                Message = message,
                ActionBeforeClose = actionBeforeClose,
                Token = token
            });
        }

        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Ask(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Ask);
            Show(growlInfo);
        }

        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="message"></param>
        /// <param name="actionBeforeClose"></param>
        public static void AskGlobal(string message, Func<bool, bool> actionBeforeClose)
        {
            AskGlobal(new GrowlInfo
            {
                Message = message,
                ActionBeforeClose = actionBeforeClose
            });
        }

        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void AskGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, ModernInfoType.Ask);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="token"></param>
        public static void Clear(string token = "")
        {
            if (!string.IsNullOrEmpty(token))
            {
                if (PanelDic.TryGetValue(token, out var panel))
                {
                    Clear(panel);
                }
            }
            else
            {
                Clear(GrowlPanel);
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="panel"></param>
        private static void Clear(Panel panel)
        {
            if (panel == null)
            {
                return;
            }

            panel.Children.Clear();
            if (panel.ContextMenu != null)
            {
                panel.ContextMenu.IsOpen = false;
                panel.ContextMenu.Opacity = 1;
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        public static void ClearGlobal()
        {
            if (GrowlWindow == null)
            {
                return;
            }

            Clear(GrowlWindow.GrowlPanel);
            GrowlWindow.Close();
            GrowlWindow = null;
        }

    }
}