using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TextBoxBehaviors
{
    public class KeepFocus
    {
        /// <summary>
        /// 制限しているキーを取得する
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsKeepFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsKeepFocusProperty);
        }

        /// <summary>
        /// 制限するキーをセットする
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsKeepFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(IsKeepFocusProperty, value);
        }

        /// <summary>
        /// 制限するキーの要素の依存プロパティ
        /// </summary>
        public static readonly DependencyProperty IsKeepFocusProperty =
            DependencyProperty.RegisterAttached("IsKeepFocus",
                typeof(bool),
                typeof(KeepFocus),
                new PropertyMetadata(false, PropertyChanged));

        public static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox == null)
            {
                return;
            }

            textBox.LostFocus -= LostFocus;

            var newValue = (bool)e.NewValue;
            if (newValue)
            {
                textBox.LostFocus += LostFocus;
            }
        }

        static void LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            textBox.Focus(FocusState.Programmatic);
        }
    }
}
