using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace TextBoxBehaviors
{
    public class InputKeyControl
    {
        /// <summary>
        /// 制限しているキーを取得する
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<VirtualKey> GetLimitKeySource(DependencyObject obj)
        {
            return (List<VirtualKey>)obj.GetValue(LimitKeySourceProperty);
        }

        /// <summary>
        /// 制限するキーをセットする
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetLimitKeySource(DependencyObject obj, List<VirtualKey> value)
        {
            _limitKeys = value;
            obj.SetValue(LimitKeySourceProperty, value);
        }

        /// <summary>
        /// 制限するキーの要素の依存プロパティ
        /// </summary>
        public static readonly DependencyProperty LimitKeySourceProperty =
            DependencyProperty.RegisterAttached("LimitKeySource",
                typeof(List<VirtualKey>),
                typeof(InputKeyControl),
                new PropertyMetadata(false, PropertyChanged));

        /// <summary>
        /// 制御するキーのリスト
        /// </summary>
        private static List<VirtualKey> _limitKeys = new List<VirtualKey>();

        /// <summary>
        /// プロパティが変更された際のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            textBox.KeyDown -= OnKeyDown;

            var newValue = (List<VirtualKey>)e.NewValue;
            if (newValue.Any())
            {
                _limitKeys = newValue;
                textBox.KeyDown += OnKeyDown;
            }

        }

        /// <summary>
        /// ビヘイビアをセットされたTextBoxのキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null)
            {
                return;
            }

            e.Handled = _limitKeys.Any(x => x == e.Key);
        }
    }
}
