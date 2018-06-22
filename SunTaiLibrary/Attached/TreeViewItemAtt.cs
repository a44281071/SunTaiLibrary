using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SunTaiLibrary.Attached
{
  public static class TreeViewItemAtt
  {
    #region IsBroughtIntoViewWhenSelected附加属性

    public static bool GetIsBroughtIntoViewWhenSelected(TreeViewItem treeViewItem)
    {
      return (bool)treeViewItem.GetValue(IsBroughtIntoViewWhenSelectedProperty);
    }

    public static void SetIsBroughtIntoViewWhenSelected(
      TreeViewItem treeViewItem, bool value)
    {
      treeViewItem.SetValue(IsBroughtIntoViewWhenSelectedProperty, value);
    }

    /// <summary>
    /// 启用当选择项更新时，确保该项在界面中显示。
    /// </summary>
    public static readonly DependencyProperty IsBroughtIntoViewWhenSelectedProperty =
            DependencyProperty.RegisterAttached(
            "IsBroughtIntoViewWhenSelected",
            typeof(bool),
            typeof(TreeViewItemAtt),
            new UIPropertyMetadata(false, OnIsBroughtIntoViewWhenSelectedChanged));

    static void OnIsBroughtIntoViewWhenSelectedChanged(
      DependencyObject depObj, DependencyPropertyChangedEventArgs e)
    {
      TreeViewItem item = depObj as TreeViewItem;
      if (item == null)
        return;

      if (e.NewValue is bool == false)
        return;

      if ((bool)e.NewValue)
        item.Selected += OnTreeViewItemSelected;
      else
        item.Selected -= OnTreeViewItemSelected;
    }

    static void OnTreeViewItemSelected(object sender, RoutedEventArgs e)
    {
      // 激活选中
      TreeViewItem item = e.OriginalSource as TreeViewItem;

      item.Focus();
      if (!item.IsFocused)
      {
        item.Focus();
      }

      // Only react to the Selected event raised by the TreeViewItem
      // whose IsSelected property was modified. Ignore all ancestors
      // who are merely reporting that a descendant's Selected fired.

      if (!Object.ReferenceEquals(sender, e.OriginalSource))
        return;

      if (item != null)
      {
        item.BringIntoView();
      }
    }

    #endregion IsBroughtIntoViewWhenSelected附加属性
  }
}