using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SunTaiLibrary.Attached
{
    public class SelectorAttached
    {
        #region EnableBringIntoViewOnSelected

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

        #endregion EnableBringIntoViewOnSelected

        #region SelectNoneWhenClickBlankArea

        public static bool GetSelectNoneWhenClickBlankArea(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectNoneWhenClickBlankAreaProperty);
        }

        public static void SetSelectNoneWhenClickBlankArea(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectNoneWhenClickBlankAreaProperty, value);
        }

        public static readonly DependencyProperty SelectNoneWhenClickBlankAreaProperty =
            DependencyProperty.RegisterAttached("SelectNoneWhenClickBlankArea", typeof(bool), typeof(SelectorAttached)
                , new UIPropertyMetadata(false, OnSelectNoneWhenClickBlankAreaChanged));

        private static void OnSelectNoneWhenClickBlankAreaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Selector ele)
            {
                if ((bool)e.NewValue)
                {
                    ele.MouseLeftButtonDown += Selector_MouseLeftButtonDown;
                }
                else
                {
                    ele.MouseLeftButtonDown -= Selector_MouseLeftButtonDown;
                }
            }
            else
            {
                throw new NotSupportedException("Muset be used in System.Windows.Controls.Primitives.Selector: SelectorAttached.SelectNoneWhenClickBlankArea");
            }
        }

        private static void Selector_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var ele = (Selector)sender;
            ele.SelectedIndex = -1;
            ele.Focus();
        }

        #endregion SelectNoneWhenClickBlankArea

        #region SelectNoneWhenPressEsc

        /// <summary>
        /// Select None When Press Esc
        /// </summary>
        public static bool GetSelectNoneWhenPressEsc(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectNoneWhenPressEscProperty);
        }

        /// <summary>
        /// Select None When Press Esc
        /// </summary>
        public static void SetSelectNoneWhenPressEsc(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectNoneWhenPressEscProperty, value);
        }

        /// <summary>
        /// Select None When Press Esc
        /// </summary>
        public static readonly DependencyProperty SelectNoneWhenPressEscProperty =
            DependencyProperty.RegisterAttached("SelectNoneWhenPressEsc", typeof(bool), typeof(SelectorAttached), new PropertyMetadata(false, OnSelectNoneWhenPressEscChanged));

        private static void OnSelectNoneWhenPressEscChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Selector ele)
            {
                if ((bool)e.NewValue)
                {
                    ele.KeyDown += Selector_KeyDown;
                }
                else
                {
                    ele.KeyDown -= Selector_KeyDown;
                }
            }
            else
            {
                throw new NotSupportedException("Muset be used in System.Windows.Controls.Primitives.Selector: SelectorAttached.SelectNoneWhenPressEsc");
            }
        }

        private static void Selector_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var ele = (Selector)sender;
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                ele.SelectedIndex = -1;
                ele.Focus();
            }
        }

        #endregion SelectNoneWhenPressEsc
    }
}