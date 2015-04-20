using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SunTaiLibrary.AttachedProperty
{

    /// <summary>
    /// 为 TreeViewItem 控件，公开自定义附加属性
    /// </summary>
    public static class TreeViewItemAtt
    {

        #region IsBroughtIntoViewWhenSelected附加属性

        // 当本属性节点控件被选中时，提供跳转显示本节点
        // 为True时，表示支持此行为

        public static bool GetIsBroughtIntoViewWhenSelected(TreeViewItem treeViewItem)
        {
            return (bool)treeViewItem.GetValue(IsBroughtIntoViewWhenSelectedProperty);
        }

        public static void SetIsBroughtIntoViewWhenSelected(
          TreeViewItem treeViewItem, bool value)
        {
            treeViewItem.SetValue(IsBroughtIntoViewWhenSelectedProperty, value);
        }

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

        #endregion  // IsBroughtIntoViewWhenSelected

    }
}
