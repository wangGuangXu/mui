# Modern UI for WPF (MUI)
一组控件和样式将您的WPF应用程序转换为一个美观的现代UI应用程序。此开源项目是[xaml spy]（http://xaml spy.com）、Silverlight、Windows Phone、Windows Store和WPF的可视化运行时检查器的副产品。阅读[正式公告](http://xamlspy.com/news/open-sourcing-the-xaml-spy-ui)

[![Build status](https://img.shields.io/appveyor/ci/kozw/mui.svg)](https://ci.appveyor.com/project/kozw/mui)
[![Release](https://img.shields.io/github/release/firstfloorsoftware/mui.svg)](https://github.com/firstfloorsoftware/mui/releases/latest)
[![NuGet](https://img.shields.io/nuget/dt/ModernUI.WPF.svg)](http://nuget.org/packages/ModernUI.WPF)
[![Stars](https://img.shields.io/github/stars/firstfloorsoftware/mui.svg)](https://github.com/firstfloorsoftware/mui/stargazers)

## 演示
Check out the **MUI 演示 app** 包括在 [MUI release](https://github.com/firstfloorsoftware/mui/releases). 该应用程序演示了MUI框架的大部分功能。这个[full source](https://github.com/firstfloorsoftware/mui/tree/master/1.0/FirstFloor.ModernUI/FirstFloor.ModernUI.App) 这个项目的源代码中包含了演示应用程序的。

## 文档
看到一些 [屏幕截图](https://github.com/firstfloorsoftware/mui/wiki/Screenshots) 了解WPF的现代用户界面。并参观 [wiki](https://github.com/firstfloorsoftware/mui/wiki)了解如何将WPF的现代UI合并到应用程序中。

![](http://firstfloorsoftware.com/media/github/mui/mui.intro.png)

## 特点
* 外观运行时可配置
  * 深色、浅色和定制主题
  * 重点颜色
  * 大小字体
* 新的现代控件
  * BB代码块
  * Modern按钮
  * ModernDialog
  * ModernFrame
  * Modern菜单
  * ModernProgressRing (with 8 built-in styles)
  * Modern选项卡
  * Modern按钮开关
  * ModernWindow
  * RelativeAnimatingContentControl
  * TransitioningContentControl
* 布局
  *一组预先定义的页面布局，以获得一致的外观和感觉
* 控件样式
  * 常用WPF控件的样式，如按钮、文本块等。
  * 所有样式都自动适应深色和浅色主题，并在适当的地方使用强调色。
* 可定制的导航框架
  * ILinkNavigator and IContentLoader interfaces for maximum flexibility
  * ModernFrame中的内容加载器异常模板
* 项目和项目模板
  * Visual Studio 2012、2013和2015项目和项目模板，用于尽可能快速、流畅地创建Modernui应用程序
  * 阅读更多信息并从下载包含模板的扩展[Visual Studio Gallery](http://visualstudiogallery.msdn.microsoft.com/7a4362a7-fe5d-4f9d-bc7b-0c0dc272fe31)

## 确认
* [WPF外壳集成库](http://archive.msdn.microsoft.com/WPFShell)
* Adapted the TransitioningContentControl from [WPF Toolkit](http://wpf.codeplex.com/)
* 已适应 Windows Phone [RelativeAnimatingContentControl](http://msdn.microsoft.com/en-us/library/gg442303(v=vs.92).aspx) 用于自定义不确定的ProgressBar样式
*现代进程样式 https://github.com/nigel-sampson/spinkit-xaml
* 示例应用程序中的现代按钮图标 http://modernuiicons.com/

## 改造
* 1.汉化
  * 直接改项目Shared中的默认Resources.resx文件实现，（默认是英语）也可额外专门添加中文资源文件
* 2.添加中文注释及中文语音播报
