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
        /// <summary>
        /// get enum description.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is Enum vee)
                ? vee.GetEnumDescription(true)
                : "";
        }

        /// <summary>
        /// not implemented
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
