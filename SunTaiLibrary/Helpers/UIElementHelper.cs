using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary.Helpers
{
  /// <summary>
  /// 提供元素布局相关的辅助功能，比如布局计算，大小计算。
  /// </summary>
  public static class UIElementHelper
  {
    /// <summary>
    /// 根据一个容器大小，计算出指定比例可展示子内容的最大大小。
    /// </summary>
    /// <param name="boundSize">容器大小</param>
    /// <param name="scale">子内容大小比例。【如：16 / 9d】</param>
    /// <returns>计算后的子内容大小</returns>
    public static Size ChildScaleSize(Size boundSize, double scale)
    {
      if (boundSize.IsEmpty || boundSize.Width == 0 || boundSize.Height == 0) return new Size(0, 0);

      double bs = boundSize.Width / boundSize.Height;
      if (bs == scale)
      {
        return boundSize;
      }
      else if (bs >= scale)
      {
        return new Size(boundSize.Height * scale, boundSize.Height);
      }
      else
      {
        return new Size(boundSize.Width, boundSize.Width / scale);
      }
    }
  }
}