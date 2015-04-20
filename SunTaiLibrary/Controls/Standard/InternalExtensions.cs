using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Windows.Shell;


namespace SunTaiLibrary.Controls
{
    internal static class InternalExtensions
    {
        /// <summary>
        /// ウィンドウ操作を実行します。
        /// </summary>
        /// <param name="action">実行するウィンドウ操作。</param>
        /// <param name="source">操作を実行しようとしている UI 要素。この要素をホストするウィンドウに対し、<paramref name="action"/> 操作が実行されます。</param>
        public static void Invoke(this WindowAction action, FrameworkElement source)
        {
            var window = Window.GetWindow(source);
            if (window == null) return;

            switch (action)
            {
                case WindowAction.Active:
                    window.Activate();
                    break;
                case WindowAction.Close:
                    SystemCommands.CloseWindow(window);
                    break;
                case WindowAction.Maximize:
                    SystemCommands.MaximizeWindow(window);
                    break;
                case WindowAction.Minimize:
                    SystemCommands.MinimizeWindow(window);
                    break;
                case WindowAction.Normalize:
                    SystemCommands.RestoreWindow(window);
                    break;
                case WindowAction.OpenSystemMenu:
                    var point = source.PointToScreen(new Point(0, source.ActualHeight));
                    SystemCommands.ShowSystemMenu(window, point);
                    break;
            }
        }
                
        /// <summary>
        /// 現在の文字列と、指定した文字列を比較します。大文字と小文字は区別されません。
        /// </summary>
        public static bool Compare(this string strA, string strB)
        {
            return string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence) action(item);
        }
    }
}
