using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SunTaiLibrary.Controls
{
    /// <summary>
    /// DiagramListBoxItem
    /// </summary>
    public class DiagramListBoxItem : ListBoxItem
    {
        static DiagramListBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramListBoxItem), new FrameworkPropertyMetadata(typeof(DiagramListBoxItem)));
        }

        #region IsReadOnly

        /// <summary>
        /// IsReadOnly
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        /// <summary>
        /// IsReadOnly
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(DiagramListBoxItem), new PropertyMetadata(false));

        #endregion IsReadOnly

        #region 鼠标左键事件

        /// <summary>
        /// 为了防止列表项吃掉鼠标左键事件，造成左键拖拽失效。
        /// 当然列表项内的元素还是可以吃掉事件的，比如 Button。
        /// </summary>
        public static readonly RoutedEvent AfterMouseLeftButtonDownEvent =
            EventManager.RegisterRoutedEvent("AfterMouseLeftButtonDown", RoutingStrategy.Bubble, typeof(MouseButtonEventHandler), typeof(DiagramListBoxItem));

        /// <summary>
        /// 为了防止列表项吃掉鼠标左键事件，造成左键拖拽失效。
        /// 当然列表项内的元素还是可以吃掉事件的，比如 Button。
        /// </summary>
        public event MouseButtonEventHandler AfterMouseLeftButtonDown
        {
            add { AddHandler(AfterMouseLeftButtonDownEvent, value); }
            remove { RemoveHandler(AfterMouseLeftButtonDownEvent, value); }
        }

        /// <summary>
        /// 为了防止列表项吃掉鼠标左键事件，造成左键拖拽失效。
        /// 当然列表项内的元素还是可以吃掉事件的，比如 Button。
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            MouseButtonEventArgs args = new(e.MouseDevice, e.Timestamp, e.ChangedButton)
            {
                RoutedEvent = AfterMouseLeftButtonDownEvent,
                Source = this
            };
            base.OnMouseLeftButtonDown(e);
            RaiseEvent(args);
        }

        #endregion 鼠标左键事件
    }
}