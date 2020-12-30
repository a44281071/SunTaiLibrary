using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace SunTaiLibrary.Attached
{
  public static class TextBoxAttached
  {
    #region PlaceholderProperty

    public static string GetPlaceholder(DependencyObject obj)
    {
      return (string)obj.GetValue(PlaceholderProperty);
    }

    public static void SetPlaceholder(DependencyObject obj, object value)
    {
      obj.SetValue(PlaceholderProperty, value);
    }

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.RegisterAttached("Placeholder"
            , typeof(string)
            , typeof(TextBoxAttached)
            , new PropertyMetadata());

    #endregion PlaceholderProperty

    #region AutoSelectAllProperty

    public static readonly DependencyProperty AutoSelectAllProperty =
        DependencyProperty.RegisterAttached(
        "AutoSelectAll",
        typeof(bool),
        typeof(TextBoxAttached),
        new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnAutoSelectAllChanged)));

    public static bool GetAutoSelectAll(TextBox d)
    {
      return (bool)d.GetValue(AutoSelectAllProperty);
    }

    public static void SetAutoSelectAll(TextBox d, bool value)
    {
      d.SetValue(AutoSelectAllProperty, value);
    }

    /// <summary>
    /// 用于 TextBox 获取焦点时，自动全选内容
    /// </summary>
    private static void OnAutoSelectAllChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is TextBox textBox)
      {
        var flag = (bool)e.NewValue;
        if (flag)
        {
          textBox.GotFocus += TextBoxOnGotFocus;
          textBox.PreviewMouseLeftButtonDown += TextBox_PreviewMouseLeftButtonDown;
        }
        else
        {
          textBox.GotFocus -= TextBoxOnGotFocus;
          textBox.PreviewMouseLeftButtonDown -= TextBox_PreviewMouseLeftButtonDown;
        }
      }
    }

    /// <summary>
    /// 鼠标点击时，如果是重新获取焦点，则忽略光标定位功能，防止全选功能失效
    /// </summary>
    private static void TextBox_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      var textBox = sender as TextBox;

      if (!textBox.IsFocused)
      {
        textBox.Focus();
        e.Handled = true;
      }
    }

    private static void TextBoxOnGotFocus(object sender, RoutedEventArgs e)
    {
      var textBox = sender as TextBox;
      if (textBox != null)
      {
        textBox.SelectAll();
      }
    }

    #endregion AutoSelectAllProperty
  }
}