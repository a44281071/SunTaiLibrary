using System.Windows.Media;

namespace System.Windows
{
    public static class DependencyObjectExtension
    {
        public static T GetVisualTreeParent<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject result = null;
            do
            {
                result = VisualTreeHelper.GetParent(child);
            } while (result != null && !(result is T));
            return result as T;
        }
    }
}