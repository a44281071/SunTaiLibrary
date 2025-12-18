using System.Windows;
using System.Windows.Controls;

namespace SunTaiLibrary.Controls;

/// <summary>
/// 用于在水平方向定位排列元素的列表控件
/// </summary>
public class SequenceBox : ListBox
{
    static SequenceBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SequenceBox), new FrameworkPropertyMetadata(typeof(SequenceBox)));
    }

    protected override DependencyObject GetContainerForItemOverride()
        => new SequenceBoxItem();

    /// <summary>
    /// 标明水平方向的值缩放比例。
    /// </summary>
    public double ScaleX
    {
        get { return (double)GetValue(ScaleXProperty); }
        set { SetValue(ScaleXProperty, value); }
    }

    public static readonly DependencyProperty ScaleXProperty =
        DependencyProperty.Register("ScaleX", typeof(double), typeof(SequenceBox)
            , new PropertyMetadata(10.0));
}