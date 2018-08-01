using System.Windows;
using System.Windows.Controls;

namespace SunTaiLibrary.Attached
{
  public static class TouchAttached
  {
    public static bool GetIsEnabled(UIElement obj)
    {
      return (bool)obj.GetValue(IsEnabledProperty);
    }

    public static void SetIsEnabled(UIElement obj, bool value)
    {
      obj.SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// 启用触摸滚动
    /// </summary>
    public static readonly DependencyProperty IsEnabledProperty =
           DependencyProperty.RegisterAttached("IsEnabled"
             , typeof(bool)
             , typeof(TouchAttached)
             , new PropertyMetadata(false, OnEnabledChanged));

    private static void OnEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is UIElement ele)
      {
        if (e.NewValue != e.OldValue)
        {
          if ((bool)e.NewValue)
          {
            ScrollViewer.SetPanningMode(ele, PanningMode.Both);
            ele.ManipulationBoundaryFeedback += Ele_ManipulationBoundaryFeedback;
          }
          else
          {
            ScrollViewer.SetPanningMode(ele, PanningMode.None);
            ele.ManipulationBoundaryFeedback -= Ele_ManipulationBoundaryFeedback;
          }
        }
      }
    }

    private static void Ele_ManipulationBoundaryFeedback(object sender, System.Windows.Input.ManipulationBoundaryFeedbackEventArgs e)
    {
      e.Handled = true;
    }
  }
}