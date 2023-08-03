using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace SunTaiLibrary.Converters;

/// <summary>
/// 旋转竖排布局文本内容。
/// </summary>
public class VerticalTextConverter : IValueConverter
{
    /// <summary>
    /// 按照指定角度旋转每一个文字。（推荐0度90度或270度）
    /// </summary>
    public double RotateAngle { get; set; } = 0;

    /// <summary>
    /// 文字排序方向（270度left_dock推荐倒排）
    /// </summary>
    public FlowDirection FlowDirection { get; set; } = FlowDirection.LeftToRight;

    private IEnumerable<string> ReadWords(string text)
    {
        TextElementEnumerator charEnum = StringInfo.GetTextElementEnumerator(text);
        while (charEnum.MoveNext())
        {
            yield return charEnum.GetTextElement();
        }
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) return null;

        string txt = value.ToString();

        if (txt.Length <= 0) return null;

        var rotate = new RotateTransform() { Angle = RotateAngle };
        rotate.Freeze();

        var ele = new StackPanel();

        var txtItems = FlowDirection is FlowDirection.LeftToRight
            ? ReadWords(txt)
            : ReadWords(txt).Reverse();

        foreach (string jw in txtItems)
        {
            var tb = new TextBlock()
            {
                Text = jw,
                LayoutTransform = rotate
            };
            ele.Children.Add(tb);
        }
        return ele;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}