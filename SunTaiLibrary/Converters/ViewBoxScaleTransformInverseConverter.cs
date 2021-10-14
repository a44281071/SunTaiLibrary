using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace SunTaiLibrary.Converters
{
    /// <summary>
    /// 反转 ViewBox 控件的缩放值，还原控件本身大小
    /// </summary>
    public class ViewBoxScaleTransformInverseConverter : IMultiValueConverter
    {
        /// <summary>
        /// 当 ViewBox 大小改变时，重新计算缩放，然后得到反向缩放值，用来还原本身的大小
        /// </summary>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var vbox = values[0] as Viewbox;
            // values[1] is Viewbox size
            // values[2] also is Viewbox size

            if (VisualTreeHelper.GetChild(vbox, 0) is ContainerVisual child
                && child.Transform is ScaleTransform scale)
            {
                return scale.Inverse as Transform;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}