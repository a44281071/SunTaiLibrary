using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SunTaiLibrary.Attached
{
  /// <summary>
  /// add more properties for Border control.
  /// </summary>
  public static class BorderAttached
  {
    #region CornerRadiusProperty

    private static CornerRadius Default_CornerRadius = new CornerRadius(0);

    public static CornerRadius GetCornerRadius(DependencyObject obj)
    {
      return (CornerRadius)obj.GetValue(CornerRadiusProperty);
    }

    public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
    {
      obj.SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// 为不支持圆角属性的控件，附加圆角描述
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(BorderAttached), new PropertyMetadata(Default_CornerRadius));

    #endregion CornerRadiusProperty
  }
}