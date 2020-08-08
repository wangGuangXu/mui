using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FirstFloor.ModernUI.Windows.Navigation
{
    /// <summary>
    /// 为导航提供帮助程序功能 Provides helper function for navigation.
    /// </summary>
    public static class NavigationHelper
    {
        /// <summary>
        /// 标识当前框架 Identifies the current frame.
        /// </summary>
        public const string FrameSelf = "_self";
        /// <summary>
        /// 标识头部框架 Identifies the top frame.
        /// </summary>
        public const string FrameTop = "_top";
        /// <summary>
        /// 标识当前框架的父框架 Identifies the parent of the current frame.
        /// </summary>
        public const string FrameParent = "_parent";

        /// <summary>
        /// 查找在指定上下文中使用给定名称标识的框架
        /// Finds the frame identified with given name in the specified context.
        /// </summary>
        /// <param name="name">框架名字 The frame name.</param>
        /// <param name="context">提供查找框架上下文的框架元素 The framework element providing the context for finding a frame.</param>
        /// <returns>如果找不到该框架，则为空 The frame or null if the frame could not be found.</returns>
        public static ModernFrame FindFrame(string name, FrameworkElement context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            // 收集所有祖先框架 collect all ancestor frames
            var frames = context.AncestorsAndSelf().OfType<ModernFrame>().ToArray();
            if (name == null || name == FrameSelf)
            {
                // 查找第一个祖先框架 find first ancestor frame
                return frames.FirstOrDefault();
            }

            if (name == FrameParent)
            {
                //查找父框架 find parent frame
                return frames.Skip(1).FirstOrDefault();
            }

            if (name == FrameTop)
            {
                // 查找最上面的框架 find top-most frame
                return frames.LastOrDefault();
            }

            // 查找名称与目标匹配的祖先框架 find ancestor frame having a name matching the target
            var frame = frames.FirstOrDefault(f => f.Name == name);
            if (frame == null)
            {
                // 在上下文范围内查找框架 find frame in context scope
                frame = context.FindName(name) as ModernFrame;
                if (frame == null)
                {
                    // 在祖先框架内容范围内查找框架 find frame in scope of ancestor frame content
                    var parent = frames.FirstOrDefault();
                    if (parent != null && parent.Content != null)
                    {
                        var content = parent.Content as FrameworkElement;
                        if (content != null)
                        {
                            frame = content.FindName(name) as ModernFrame;
                        }
                    }
                }
            }

            return frame;
        }

        /// <summary>
        /// 从指定的uri中移除片段并传回
        /// Removes the fragment from specified uri and return it.
        /// </summary>
        /// <param name="uri">The uri</param>
        /// <returns>The uri without the fragment, or the uri itself if no fragment is found</returns>
        public static Uri RemoveFragment(Uri uri)
        {
            string fragment;
            return RemoveFragment(uri, out fragment);
        }

        /// <summary>
        /// 从指定的uri中移除片段，并返回不包含片段和片段本身的uri
        /// Removes the fragment from specified uri and returns the uri without the fragment and the fragment itself.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <param name="fragment">The fragment, null if no fragment found</param>
        /// <returns>The uri without the fragment, or the uri itself if no fragment is found</returns>
        public static Uri RemoveFragment(Uri uri, out string fragment)
        {
            fragment = null;

            if (uri != null)
            {
                var value = uri.OriginalString;

                var i = value.IndexOf('#');
                if (i != -1)
                {
                    fragment = value.Substring(i + 1);
                    uri = new Uri(value.Substring(0, i), uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
                }
            }

            return uri;
        }

        /// <summary>
        /// 尝试将指定的值转换为uri。可以接受uri或字符串输入
        /// Tries to cast specified value to a uri. Either a uri or string input is accepted.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Uri ToUri(object value)
        {
            var uri = value as Uri;
            if (uri == null)
            {
                var uriString = value as string;
                if (uriString == null || !Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out uri))
                {
                    return null; // no valid uri found
                }
            }
            return uri;
        }

        /// <summary>
        /// 尝试使用给定值中的参数分析uri
        /// Tries to parse a uri with parameters from given value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="targetName">Name of the target.</param>
        /// <returns></returns>
        public static bool TryParseUriWithParameters(object value, out Uri uri, out string parameter, out string targetName)
        {
            uri = value as Uri;
            parameter = null;
            targetName = null;

            if (uri == null) 
            {
                var valueString = value as string;
                return TryParseUriWithParameters(valueString, out uri, out parameter, out targetName);
            }
            
            return true;
        }

        /// <summary>
        /// 尝试使用给定字符串值中的参数分析uri
        /// Tries to parse a uri with parameters from given string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="targetName">目标的名称 Name of the target.</param>
        /// <returns></returns>
        public static bool TryParseUriWithParameters(string value, out Uri uri, out string parameter, out string targetName)
        {
            uri = null;
            parameter = null;
            targetName = null;

            if (value == null) {
                return false;
            }

            // 分析可选参数和/或目标的uri值 parse uri value for optional parameter and/or target, eg 'cmd://foo|parameter|target'
            string uriString = value;
            var parts = uriString.Split(new char[] { '|' }, 3);
            if (parts.Length == 3) 
            {
                uriString = parts[0];
                parameter = Uri.UnescapeDataString(parts[1]);
                targetName = Uri.UnescapeDataString(parts[2]);
            }
            else if (parts.Length == 2) 
            {
                uriString = parts[0];
                parameter = Uri.UnescapeDataString(parts[1]);
            }

            return Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out uri);
        }
    }
}