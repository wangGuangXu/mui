﻿using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FirstFloor.ModernUI.App.Content
{
    /// <summary>
    /// 用于配置主题、字体、颜色的简单视图模型
    /// </summary>
    public class SettingsAppearanceViewModel : NotifyPropertyChanged
    {
        private const string FontSmall = "small";
        private const string FontLarge = "large";

        private const string PaletteMetro = "metro";
        private const string PaletteWP = "windows phone";

        // 来自metro 设计原则的9种强调色
        private Color[] metroAccentColors = new Color[]
        {
            Color.FromRgb(0x33, 0x99, 0xff),   // blue
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x33, 0x99, 0x33),   // green
            Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
            Color.FromRgb(0xf0, 0x96, 0x09),   // orange
            Color.FromRgb(0xff, 0x45, 0x00),   // orange red
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xff, 0x00, 0x97),   // magenta
            Color.FromRgb(0xa2, 0x00, 0xff),   // purple            
        };

        // 来自Windwows Phone 8的20种强调色
        private Color[] wpAccentColors = new Color[]
        {
            Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            Color.FromRgb(0x60, 0xa9, 0x17),   // green
            Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            Color.FromRgb(0x00, 0x50, 0xef),   // cobalt
            Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            Color.FromRgb(0x64, 0x76, 0x87),   // steel
            Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };

        private string selectedPalette = PaletteWP;

        private Color selectedAccentColor;
        private LinkCollection themes = new LinkCollection();
        private Link selectedTheme;
        private string selectedFontSize;

        public SettingsAppearanceViewModel()
        {
            // 添加默认主题
            this.themes.Add(new Link { DisplayName = "dark", Source = AppearanceManager.DarkThemeSource });
            this.themes.Add(new Link { DisplayName = "light", Source = AppearanceManager.LightThemeSource });

            // 添加额外的主题
            this.themes.Add(new Link { DisplayName = "QiangDaGeLanYunTian", Source = new Uri("/ModernUIDemo;component/Assets/ModernUI.QiangDaGeLanYunTian.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "bing image", Source = new Uri("/ModernUIDemo;component/Assets/ModernUI.BingImage.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "hello kitty", Source = new Uri("/ModernUIDemo;component/Assets/ModernUI.HelloKitty.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "love", Source = new Uri("/ModernUIDemo;component/Assets/ModernUI.Love.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "snowflakes", Source = new Uri("/ModernUIDemo;component/Assets/ModernUI.Snowflakes.xaml", UriKind.Relative) });

            this.SelectedFontSize = AppearanceManager.Current.FontSize == FontSize.Large ? FontLarge : FontSmall;
            SyncThemeAndColor();

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        /// <summary>
        /// 同步主题和颜色
        /// </summary>
        private void SyncThemeAndColor()
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            this.SelectedTheme = this.themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

            // and make sure accent color is up-to-date
            this.SelectedAccentColor = AppearanceManager.Current.AccentColor;
        }

        /// <summary>
        /// 外观属性改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                SyncThemeAndColor();
            }
        }

        /// <summary>
        /// 主题列表
        /// </summary>
        public LinkCollection Themes
        {
            get { return this.themes; }
        }

        /// <summary>
        /// 字体大小集合
        /// </summary>
        public string[] FontSizes
        {
            get { return new string[] { FontSmall, FontLarge }; }
        }

        /// <summary>
        /// 调色板列表
        /// </summary>
        public string[] Palettes
        {
            get { return new string[] { PaletteMetro, PaletteWP }; }
        }

        /// <summary>
        /// 强调色
        /// </summary>
        public Color[] AccentColors
        {
            get { return this.selectedPalette == PaletteMetro ? this.metroAccentColors : this.wpAccentColors; }
        }

        /// <summary>
        /// 选择调色板
        /// </summary>
        public string SelectedPalette
        {
            get { return this.selectedPalette; }
            set
            {
                if (this.selectedPalette != value) {
                    this.selectedPalette = value;
                    OnPropertyChanged(() => this.AccentColors);

                    this.SelectedAccentColor = this.AccentColors.FirstOrDefault();
                }
            }
        }

        /// <summary>
        /// 选择主题
        /// </summary>
        public Link SelectedTheme
        {
            get { return this.selectedTheme; }
            set
            {
                if (this.selectedTheme != value)
                {
                    this.selectedTheme = value;
                    OnPropertyChanged(() => this.SelectedTheme);

                    // and update the actual theme
                    AppearanceManager.Current.ThemeSource = value.Source;
                }
            }
        }

        /// <summary>
        /// 选定字体大小
        /// </summary>
        public string SelectedFontSize
        {
            get { return this.selectedFontSize; }
            set
            {
                if (this.selectedFontSize != value)
                {
                    this.selectedFontSize = value;
                    OnPropertyChanged(() => this.SelectedFontSize);

                    AppearanceManager.Current.FontSize = value == FontLarge ? FontSize.Large : FontSize.Small;
                }
            }
        }

        /// <summary>
        /// 选定强调色
        /// </summary>
        public Color SelectedAccentColor
        {
            get { return this.selectedAccentColor; }
            set
            {
                if (this.selectedAccentColor != value)
                {
                    this.selectedAccentColor = value;
                    OnPropertyChanged(() => this.SelectedAccentColor);

                    AppearanceManager.Current.AccentColor = value;
                }
            }
        }


    }
}