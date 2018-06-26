using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SunTaiLibrary.Converters
{
  /// <summary>
  /// 判断传入的所有数据是否都一样。
  /// </summary>
  public class EqualsConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      var result = true;
      var v0 = values[0];
      for (int i = 1; i < values.Length; i++)
      {
        var item= values[i];
        result = v0.Equals(item);
        if (!result)
        {
          return false;
        }
      }
      return result;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
