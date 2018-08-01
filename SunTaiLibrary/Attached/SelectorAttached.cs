using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SunTaiLibrary.Attached
{
  public class SelectorAttached
  {
    public static bool GetEnableBringIntoViewOnSelected(Selector obj)
    {
      return (bool)obj.GetValue(EnableBringIntoViewOnSelectedProperty);
    }

    public static void SetEnableBringIntoViewOnSelected(Selector obj, bool value)
    {
      obj.SetValue(EnableBringIntoViewOnSelectedProperty, value);
    }

    /// <summary>
    /// 启用当选择项更新时，确保该项在界面中显示。
    /// </summary>
    public static readonly DependencyProperty EnableBringIntoViewOnSelectedProperty =
        DependencyProperty.RegisterAttached("EnableBringIntoViewOnSelected", typeof(bool), typeof(SelectorAttached)
          , new UIPropertyMetadata(false, OnEnableBringIntoViewOnSelectedChanged));

    private static void OnEnableBringIntoViewOnSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var ele = d as Selector;

      if ((bool)e.NewValue)
        ele.SelectionChanged += OnSelectionChanged;
      else
        ele.SelectionChanged -= OnSelectionChanged;
    }

    private static void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var ele = sender as Selector;
      if (ele.SelectedIndex != -1)
      {
        var item = ele.ItemContainerGenerator.ContainerFromIndex(ele.SelectedIndex) as FrameworkElement;
        item?.BringIntoView();
      }
    }
  }
}