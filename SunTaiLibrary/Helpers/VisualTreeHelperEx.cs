using System.Windows;

namespace SunTaiLibrary.Helpers
{
  public static class VisualTreeHelperEx
  {
    /// <summary>
    /// Return the first visual child of element by type.
    /// </summary>
    /// <typeparam name="T">The type of the Child</typeparam>
    /// <param name="obj">The parent Element</param>
    public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
    {
      for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(obj); i++)
      {
        DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(obj, i);
        if (child != null && child is T)
          return (T)child;
        else
        {
          T childOfChild = FindVisualChild<T>(child);
          if (childOfChild != null)
            return childOfChild;
        }
      }
      return null;
    }
  }
}