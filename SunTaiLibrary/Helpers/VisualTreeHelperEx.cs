using System.Collections.Generic;
using System.Windows;

namespace SunTaiLibrary
{
  public static class VisualTreeHelper
  {
    /// <summary>
    /// Return the first visual child of element by type.
    /// </summary>
    /// <typeparam name="T">The type of the Child</typeparam>
    /// <param name="obj">The parent Element</param>
    public static T FindVisualChild<T>(this DependencyObject obj) where T : DependencyObject
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

    /// <summary>
    /// Return the ancestor of element by type.
    /// </summary>
    /// <typeparam name="T">The type of the Ancestor</typeparam>
    /// <param name="obj">The child Element</param>
    public static T FindAncestor<T>(this DependencyObject obj) where T : DependencyObject
    {
      if (obj == null) return null;

      DependencyObject parent = System.Windows.Media.VisualTreeHelper.GetParent(obj);
      while (parent != null && !(parent is T))
      {
        parent = System.Windows.Media.VisualTreeHelper.GetParent(parent);
      }
      return parent as T;
    }

    /// <summary>
    /// This method will use the VisualTreeHelper.GetParent method to do a depth first walk up
    /// the visual tree and return all ancestors of the specified object, including the object itself.
    /// </summary>
    /// <param name="dependencyObject">The object in the visual tree to find ancestors of.</param>
    /// <returns>Returns itself an all ancestors in the visual tree.</returns>
    public static IEnumerable<DependencyObject> GetSelfAndAncestors(this DependencyObject dependencyObject)
    {
      while (dependencyObject != null)
      {
        yield return dependencyObject;
        dependencyObject = System.Windows.Media.VisualTreeHelper.GetParent(dependencyObject);
      }
    }
  }
}