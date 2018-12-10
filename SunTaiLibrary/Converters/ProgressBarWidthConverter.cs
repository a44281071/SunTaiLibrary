using System;
using System.Globalization;
using System.Windows.Data;

namespace SunTaiLibrary.Converters
{
  public class ProgressBarWidthConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      double contentWidth = (double)values[0];
      double parentMinWidth = (double)values[1];

      return Math.Max(contentWidth, parentMinWidth);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}