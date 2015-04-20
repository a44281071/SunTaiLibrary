using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace SunTaiLibrary.AttachedProperty
{

    //2014年12月18日17:43:04 - 周治
    /// <summary>
    /// 用于 TextBox 获取焦点时，自动全选内容
    /// </summary>
    public static class TextBoxAtt
    {
        public static readonly DependencyProperty AutoSelectAllProperty =
            DependencyProperty.RegisterAttached(
            "AutoSelectAll",
            typeof(bool),
            typeof(TextBoxAtt),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnAutoSelectAllChanged)));

        public static bool GetAutoSelectAll(TextBox d)
        {
            return (bool)d.GetValue(AutoSelectAllProperty);
        }

        public static void SetAutoSelectAll(TextBox d, bool value)
        {
            d.SetValue(AutoSelectAllProperty, value);
        }

        private static void OnAutoSelectAllChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;
            if (textBox != null)
            {
                var flag = (bool)e.NewValue;
                if (flag)
                {
                    textBox.GotFocus += TextBoxOnGotFocus;
                    textBox.PreviewMouseLeftButtonDown += textBox_PreviewMouseLeftButtonDown;
                }
                else
                {
                    textBox.GotFocus -= TextBoxOnGotFocus;
                    textBox.PreviewMouseLeftButtonDown -= textBox_PreviewMouseLeftButtonDown;
                }
            }
        }

        /// <summary>
        /// 鼠标点击时，如果是重新获取焦点，则忽略光标定位功能，防止全选功能失效
        /// </summary>
        static void textBox_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
    }


}
