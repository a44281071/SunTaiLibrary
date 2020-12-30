using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace System.Windows
{
    public static class ExtensionWindow
    {
        public static void MinimizeWindow(this Window window) => SystemCommands.MinimizeWindow(window);

        public static void RestoreWindow(this Window window) => SystemCommands.RestoreWindow(window);

        public static void MaximizeWindow(this Window window) => SystemCommands.MaximizeWindow(window);

        public static void ShowSystemMenu(this Window window, Point screenLocation) => SystemCommands.ShowSystemMenu(window, screenLocation);
    }
}