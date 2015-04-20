using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SunTaiLibrary.AttachedProperty
{
    //2014年12月18日17:42:41 - 周治
    /// <summary>
    /// 用于支持PasswordBox的绑定
    /// </summary>
    public static class PasswordAtt
    {

        //用于绑定密码区域
        public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached(
            "BindPassword",
            typeof(string),
            typeof(PasswordAtt),
            new FrameworkPropertyMetadata(string.Empty, OnBindPasswordChanged)
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged
            });

        //用于激活绑定密码
        public static readonly DependencyProperty IsEnabled = DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(PasswordAtt),
            new PropertyMetadata(false, OnIsEnabledChanged));

        private static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordAtt));

        private static void OnBindPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = d as PasswordBox;

            if (d == null || !GetIsEnabled(d))
            {
                return;
            }

            box.PasswordChanged -= HandlePasswordChanged;

            string newPassword = (string)e.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = newPassword;
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnIsEnabledChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = dp as PasswordBox;

            if (box == null)
            {
                return;
            }

            bool wasBound = (bool)(e.OldValue);
            bool needToBind = (bool)(e.NewValue);

            if (wasBound)
            {
                box.PasswordChanged -= HandlePasswordChanged;
            }

            if (needToBind)
            {
                box.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox box = sender as PasswordBox;

            SetUpdatingPassword(box, true);
            SetBindPassword(box, box.Password);
            SetUpdatingPassword(box, false);
        }

        public static void SetIsEnabled(DependencyObject dp, bool value)
        {
            dp.SetValue(IsEnabled, value);
        }

        public static bool GetIsEnabled(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsEnabled);
        }

        public static string GetBindPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(BindPassword);
        }

        public static void SetBindPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BindPassword, value);
        }

        private static bool GetUpdatingPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(UpdatingPassword);
        }

        private static void SetUpdatingPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(UpdatingPassword, value);
        }
    }
}
