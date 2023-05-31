using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SunTaiLibrary.Controls;

/// <summary>
/// shift content for boolean switch.
/// </summary>
[ContentProperty("FalseContent")]
[DefaultProperty("FalseContent")]
public class SwitchGearControl : Control
{
    static SwitchGearControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchGearControl), new FrameworkPropertyMetadata(typeof(SwitchGearControl)));
    }

    /// <summary>
    /// boolean switch decision value.
    /// </summary>
    [Bindable(true)]
    public bool DecisionValue
    {
        get { return (bool)GetValue(DecisionValueProperty); }
        set { SetValue(DecisionValueProperty, value); }
    }

    /// <summary>
    /// boolean switch decision value.
    /// </summary>
    public static readonly DependencyProperty DecisionValueProperty =
        DependencyProperty.Register("DecisionValue", typeof(bool), typeof(SwitchGearControl)
            , new PropertyMetadata(false, NeedComputeContent));

    /// <summary>
    /// display content for true switch.
    /// </summary>
    [Bindable(true)]
    public object TrueContent
    {
        get { return GetValue(TrueContentProperty); }
        set { SetValue(TrueContentProperty, value); }
    }

    /// <summary>
    /// display content for true switch.
    /// </summary>
    public static readonly DependencyProperty TrueContentProperty =
               DependencyProperty.Register("TrueContent", typeof(object), typeof(SwitchGearControl)
                   , new PropertyMetadata(NeedComputeContent));

    /// <summary>
    /// display content for false switch.
    /// </summary>
    [Bindable(true)]
    public object FalseContent
    {
        get { return GetValue(FalseContentProperty); }
        set { SetValue(FalseContentProperty, value); }
    }

    /// <summary>
    /// display content for fal.se switch.
    /// </summary>
    public static readonly DependencyProperty FalseContentProperty =
        DependencyProperty.Register("FalseContent", typeof(object), typeof(SwitchGearControl)
            , new PropertyMetadata(NeedComputeContent));

    internal static readonly DependencyPropertyKey ComputedContentPropertyKey =
        DependencyProperty.RegisterReadOnly("ComputedContent", typeof(object), typeof(SwitchGearControl)
            , new PropertyMetadata());

    /// <summary>
    /// display content for fal.se switch.
    /// </summary>
    public static readonly DependencyProperty ComputedContentProperty = ComputedContentPropertyKey.DependencyProperty;

    /// <summary>
    /// computed display content.
    /// </summary>
    public object ComputedContent => GetValue(ComputedContentProperty);

    private static void NeedComputeContent(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ele = (SwitchGearControl)d;
        ele.NeedComputeContentCore();
    }

    private void NeedComputeContentCore()
    {
        var content = DecisionValue ? TrueContent : FalseContent;
        SetValue(ComputedContentPropertyKey, content);
    }
}