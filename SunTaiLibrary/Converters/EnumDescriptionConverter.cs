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
  /// 获取枚举的描述文字。
  /// </summary>
  [ValueConversion(typeof(Enum), typeof(string))]
  public class EnumDescriptionConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      Enum vv = (Enum)value;
      return vv.GetEnumDescription();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
