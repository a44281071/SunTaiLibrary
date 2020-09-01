using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SunTaiLibrary.Controls
{
  /// <summary>
  /// 均分空间，让子级内容尽量的达到最大大小。
  /// </summary>
  public class CorrectGrid : Panel
  {
    private int rows = 1;
    private int columns = 1;

    protected override Size MeasureOverride(Size availableSize)
    {
      int count = InternalChildren.Cast<UIElement>()
        .Count(dd => dd.Visibility != Visibility.Collapsed);

      // uniform measure
      double cs = Math.Sqrt(count);
      columns = (int)Math.Ceiling(cs);
      rows = (int)Math.Round(cs);

      var childConstraint = new Size(availableSize.Width / columns, availableSize.Height / rows);

      // Measure the child.
      for (int i = 0; i < count; ++i)
      {
        UIElement child = InternalChildren[i];
        child.Measure(childConstraint);
      }

      return base.MeasureOverride(availableSize);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      Rect childBounds = new Rect(0, 0, finalSize.Width / columns, finalSize.Height / rows);
      double xStep = childBounds.Width;
      double xBound = finalSize.Width - 1.0;

      for (int i = 0; i < InternalChildren.Count; i++)
      {
        var child = InternalChildren[i];

        child.Arrange(childBounds);

        if (child.Visibility != Visibility.Collapsed)
        {
          childBounds.X += xStep;
          if (childBounds.X >= xBound)
          {
            // 需要换行
            childBounds.Y += childBounds.Height;

            int remainders = InternalChildren.Count - 1 - i;
            if (remainders < columns)
            {
              childBounds.X = (finalSize.Width - childBounds.Width * remainders) / 2;
            }
            else
            {
              childBounds.X = 0;
            }
          }
        }
      }

      return finalSize;
    }
  }
}