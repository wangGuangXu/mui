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

## 项目目录结构说明
* 1.Themes 文件夹放控件的样式，也就是定义样式的生活不带key,只有TargetType属性
* 2.Assets 文件夹放控件的模板及特殊定制化样式
## TextBox 控件说明
* 1.CaretBrush：Caret笔刷可以用来修改光标颜色。 示例：<Setter Property="CaretBrush" Value="{DynamicResource TextForeground}" />
* 2.SelectionBrush：Selection笔刷用来修改选中区背景颜色。示例：<Setter Property="SelectionBrush" Value="{DynamicResource Accent}" />
## 改造
* 1.汉化
  * 直接改项目Shared中的默认Resources.resx文件实现，（默认是英语）也可额外专门添加中文资源文件
* 2.添加中文注释及中文语音播报
* 3.DataGrid 新增全选示例
* 4.新增分页用户控件，并添加分页示例。
    * 需要引用Binaries\net45\System.Windows.Interactivity.dll (根据框架选择具体版本的程序集)

##WPF资源引用
*     1.启动程序引用附属项目的资源
　　[pack://application:,,,/MyProject;component/Window1.xaml]或[/MyProject;component/Window1.xaml]
　　[pack://application:,,,/MyProject;component/Image/advancedsettings.png]或[/MyProject;component/Image/advancedsettings.png]

*     2.附属项目引用启动项目的资源
　　[pack://application:,,,/Image/system_upgrade.png]或[/Image/system_upgrade.png]

*     3.引用本项目的资源
　　[/MyProject;component/Image/user.ico]或文件的相对路径

##    Binding用法：
*     1. {Binding和Binding RelativeSource={RelativeSource Mode=Self},Path=DataContext} 等价
*      2.ItemsSource="{Binding .}" .路径可用于绑定到当前源。
## 1.样式引用地址：https://www.cnblogs.com/beimeng/p/7843427.html
* 1.如果将 x:Key 值显式设置为 {x:Type TextBlock} 之外的任何值，如设置为 x:key="cc"，Style 就不会自动应用于所有 TextBlock 元素。此时，必须通过使用 x:Key 值，将样式显式应用于 TextBlock 元素
* 2.样式位于资源部分，并且未设置样式的 TargetType 属性，则必须提供 x:Key。
* 3.将 TargetType 属性设置为 TextBlock 而不为样式分配 x:Key，样式就会应用于所有 TextBlock 元素。这种情况下，x:Key 隐式设置为 {x:Type TextBlock}。　
<Style x:Key="{x:Type TextBlock}" TargetType="TextBlock">
   <Setter Property="FontSize" Value="28"/>
</Style>
## WPF之UseLayoutRounding和SnapsToDevicePixels的区别 ：
参考资料：https://blog.csdn.net/catshitone/article/details/77454465
* UserLayoutRounding为False，导致控件布局相对屏幕若不是整数则不会四舍五入，导致边缘模糊
* SnapsToDevicePixels默认为false， 为true可以让元素像素级对齐
* UIElement.UseLayoutRounding 属性
获取或设置一个值，该值确定对象及其可视化子树的呈现是否应使用将呈现与整像素对齐的舍入行为。（一般在容器元素上设置， 发生在Measure&Arrange期间）
* UIElement.SnapsToDevicePixels 属性
获取或设置一个值，该值决定呈现元素期间是否应使用设备特定的像素设置。这是一个依赖项属性。（一般在根元素设置， 发生在Render， 不是容器元素）
* 共同点：
1. 默认值都是false，如果设置到root元素上，则child元素也自动使用同样设置
2. 都是为了解决wpf元素边缘模糊的问题
* 不同点：
UseLayoutRounding是在during layout的时候生效的，而SnapsToDevicePixels是在during rendering的时候生效的

