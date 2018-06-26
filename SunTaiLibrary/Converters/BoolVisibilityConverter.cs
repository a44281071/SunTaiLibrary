using System;
using System.Windows;
using System.Windows.Data;

namespace SunTaiLibrary.Converters
{
  /// <summary>
  /// 【true：可见】【false：隐藏】【null：折叠】
  /// </summary>
  [ValueConversion(typeof(bool?), typeof(Visibility))]
  public class BoolVisibilityConverter : IValueConverter
  {
    static BoolVisibilityConverter()
    {
      Instance = new BoolVisibilityConverter();
    }

    public static BoolVisibilityConverter Instance { get; }

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      bool? vv = value as bool?;
      if (vv.HasValue)
      {
        return ((bool)value) ? Visibility.Visible : Visibility.Hidden;
      }
      return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      Visibility vv = (Visibility)value;
      switch (vv)
      {
        case Visibility.Visible:
          return true;

        case Visibility.Hidden:
          return false;

        default:
          return null;
      }
    }
  }
}