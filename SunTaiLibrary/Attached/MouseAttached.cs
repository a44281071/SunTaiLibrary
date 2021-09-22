using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SunTaiLibrary.Attached
{
    public static class MouseAttached
    {
        #region IsHorizontalScroll

        /// <summary>
        /// 鼠标在列表中滚动时，更改为水平滚动。
        /// </summary>
        public static bool GetIsHorizontalScroll(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsHorizontalScrollProperty);
        }

        /// <summary>
        /// 鼠标在列表中滚动时，更改为水平滚动。
        /// </summary>
        public static void SetIsHorizontalScroll(DependencyObject obj, bool value)
        {
            obj.SetValue(IsHorizontalScrollProperty, value);
        }

        /// <summary>
        /// 鼠标在列表中滚动时，更改为水平滚动。
        /// </summary>
        public static readonly DependencyProperty IsHorizontalScrollProperty =
            DependencyProperty.RegisterAttached("IsHorizontalScroll", typeof(bool), typeof(MouseAttached), new PropertyMetadata(false, IsHorizontalScrollChanged));

        /// <summary>
        /// 鼠标在列表中滚动时，更改为水平滚动。
        /// </summary>
        private static void IsHorizontalScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vv = (bool)e.NewValue;
            if (d is FrameworkElement dfe)
            {
                if (vv)
                {
                    dfe.Loaded += Element_Loaded;
                }
                else
                {
                    dfe.Loaded -= Element_Loaded;
                    var bar = SunTaiLibrary.VisualTreeHelperEx.FindVisualChild<ScrollViewer>(dfe);
                    if (bar != null)
                    {
                        bar.PreviewMouseWheel -= ScrollBar_PreviewMouseWheel;
                    }
                }
            }
        }

        private static void Element_Loaded(object sender, RoutedEventArgs e)
        {
            var bar = SunTaiLibrary.VisualTreeHelperEx.FindVisualChild<ScrollViewer>(sender as DependencyObject);
            if (bar != null)
            {
                bar.PreviewMouseWheel += ScrollBar_PreviewMouseWheel;
            }
        }

        private static void ScrollBar_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var sv = (ScrollViewer)sender;
            var newOffset = sv.HorizontalOffset - e.Delta;

            sv.ScrollToHorizontalOffset(newOffset);
        }

        #endregion IsHorizontalScroll
    }
}