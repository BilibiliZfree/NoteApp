using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NoteApp.Modules.ModuleName.ControlsExtension
{
    public class PasswordBoxExtension
    {
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxExtension),new PropertyMetadata(string.Empty,OnBoundPasswordChanged));

        public static readonly DependencyProperty BindPassword =
            DependencyProperty.RegisterAttached("BindPassword", typeof(bool), typeof(PasswordBoxExtension), new PropertyMetadata(false, OnBindPasswordChanged));

        public static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword ", typeof(bool),typeof(PasswordBoxExtension),new PropertyMetadata(false));

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = d as PasswordBox;

            //仅当属性附加到 PasswordBox 时才处理此事件
            //并且当 BindPassword 附加属性已设置为 true 时
            if (d == null || !GetBindPassword(d))
            {
                return;
            }

            //通过忽略框的已更改事件来避免递归更新
            box.PasswordChanged -= HandlePasswordChanged;

            string newPassword = (string)e.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = newPassword;
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnBindPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //当在密码框上设置 BindPassword 附加属性时，
            //开始侦听其密码更改事件
            PasswordBox box = d as PasswordBox;
            if (box == null) return;

            bool wasBound = (bool)e.OldValue;
            bool needToBound = (bool)e.NewValue;

            if (wasBound) box.PasswordChanged -= HandlePasswordChanged;
            if (needToBound) box.PasswordChanged += HandlePasswordChanged;

        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox box = sender as PasswordBox;

            //设置一个标志以指示我们正在更新密码
            SetUpdatingPassword(box, true);
            //将新密码推送到 BoundPassword 属性中
            SetBoundPassword(box, box.Password);
            SetUpdatingPassword(box, false);

        }

        public static void SetBindPassword(DependencyObject dp, bool value) => 
            dp.SetValue(BindPassword, value);

        public static bool GetBindPassword(DependencyObject dp) => 
            (bool)dp.GetValue(BindPassword);

        public static void SetBoundPassword(DependencyObject dp, string value) =>
            dp.SetValue(BoundPassword, value);

        public static string GetBoundPassword(DependencyObject dp) =>
            (string) dp.GetValue(BoundPassword);

        public static void SetUpdatingPassword(DependencyObject dp, bool value) =>
            dp.SetValue(UpdatingPassword, value);

        public static bool GetUpdatingPassword(DependencyObject dp) =>
            (bool)dp.GetValue(UpdatingPassword);

    }
}
