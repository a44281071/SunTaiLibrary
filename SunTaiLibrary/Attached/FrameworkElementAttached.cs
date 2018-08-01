using System;
using System.Windows;

namespace SunTaiLibrary.Attached
{
  public static class FrameworkElementAttached
  {
    public static bool GetLoadedAutoFocus(FrameworkElement obj)
    {
      return (bool)obj.GetValue(LoadedAutoFocusProperty);
    }

    public static void SetLoadedAutoFocus(FrameworkElement obj, bool value)
    {
      obj.SetValue(LoadedAutoFocusProperty, value);
    }

    public static readonly DependencyProperty LoadedAutoFocusProperty =
        DependencyProperty.RegisterAttached("LoadedAutoFocus", typeof(bool), typeof(FrameworkElementAttached)
          , new PropertyMetadata(false, OnLoadedAutoFocusChanged));

    private static void OnLoadedAutoFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is FrameworkElement ele)
      {
        if (((bool)e.NewValue) == true)
        {
          ele.Loaded += Element_Loaded;
        }
        else
        {
          ele.Loaded -= Element_Loaded;
        }
      }
    }

    private static void Element_Loaded(object sender, RoutedEventArgs e)
    {
      var ele = (UIElement)sender;
      ele.Focus();
    }
  }
}