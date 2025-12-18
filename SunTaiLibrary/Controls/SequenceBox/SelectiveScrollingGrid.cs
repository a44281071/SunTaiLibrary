using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;

namespace SunTaiLibrary.Controls;

/// <summary>
/// Subclass of Grid that knows how to freeze certain cells in place when scrolled.
/// Used as the panel for the DataGridRow to hold the header, cells, and details.
/// 复刻修复附加属性初始化时，无法找到父级ScrollViewer的问题
/// </summary>
public class SelectiveScrollingGrid : Grid
{
    /// <summary>
    /// Attached property to specify the selective scroll behaviour of cells
    /// </summary>
    public static readonly DependencyProperty SelectiveScrollingOrientationProperty =
        DependencyProperty.RegisterAttached(
            "SelectiveScrollingOrientation",
            typeof(SelectiveScrollingOrientation),
            typeof(SelectiveScrollingGrid),
            new FrameworkPropertyMetadata(SelectiveScrollingOrientation.Both, new PropertyChangedCallback(OnSelectiveScrollingOrientationChanged)));

    /// <summary>
    /// Getter for the SelectiveScrollingOrientation attached property
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static SelectiveScrollingOrientation GetSelectiveScrollingOrientation(DependencyObject obj)
    {
        return (SelectiveScrollingOrientation)obj.GetValue(SelectiveScrollingOrientationProperty);
    }

    /// <summary>
    /// Setter for the SelectiveScrollingOrientation attached property
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public static void SetSelectiveScrollingOrientation(DependencyObject obj, SelectiveScrollingOrientation value)
    {
        obj.SetValue(SelectiveScrollingOrientationProperty, value);
    }

    /// <summary>
    /// Property changed call back for SelectiveScrollingOrientation property
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    private static void OnSelectiveScrollingOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SelectiveScrollingOrientation orientation = (SelectiveScrollingOrientation)e.NewValue;
        if (d is FrameworkElement element)
        {
            ResetOffsetScrolling(element, orientation);
        }
    }

    private static void ResetOffsetScrolling(UIElement element, in SelectiveScrollingOrientation orientation)
    {
        ScrollViewer? scrollViewer = FindVisualParent<ScrollViewer>(element);
        if (scrollViewer is null) return;

        Transform transform = element.RenderTransform;

        if (transform != null)
        {
            BindingOperations.ClearBinding(transform, TranslateTransform.XProperty);
            BindingOperations.ClearBinding(transform, TranslateTransform.YProperty);
        }

        if (orientation == SelectiveScrollingOrientation.Both)
        {
            element.RenderTransform = null;
        }
        else
        {
            TranslateTransform translateTransform = new();

            // Add binding to XProperty of transform if orientation is not horizontal
            if (orientation != SelectiveScrollingOrientation.Horizontal)
            {
                Binding horizontalBinding = new(nameof(ScrollViewer.ContentHorizontalOffset))
                {
                    Source = scrollViewer
                };
                BindingOperations.SetBinding(translateTransform, TranslateTransform.XProperty, horizontalBinding);
            }

            // Add binding to YProperty of transfrom if orientation is not vertical
            if (orientation != SelectiveScrollingOrientation.Vertical)
            {
                Binding verticalBinding = new(nameof(ScrollViewer.ContentVerticalOffset))
                {
                    Source = scrollViewer
                };
                BindingOperations.SetBinding(translateTransform, TranslateTransform.YProperty, verticalBinding);
            }

            element.RenderTransform = translateTransform;
        }
    }

    /// <summary>
    /// 修复：在附加属性初始化时，无法找到父级ScrollViewer的问题
    /// </summary>
    public static T? FindVisualParent<T>(UIElement? element) where T : UIElement
    {
        UIElement? parent = element as UIElement;
        while (parent != null)
        {
            if (parent is T correctlyTyped)
            {
                return correctlyTyped;
            }

            if (parent is FrameworkElement fe)
            {
                // 这个属性会找到树上的可能未初始化的父级
                parent = fe.Parent as UIElement;
            }
            else
            {
                // 这种默认方式可能会找不到未初始化的父级
                parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
            }
        }

        return null;
    }
}