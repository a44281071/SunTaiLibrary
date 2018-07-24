using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SunTaiLibrary.Converters
{
  public class RowIndexConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var ele = (FrameworkElement)value;
      var parent = parameter as ItemsControl ?? ele.GetVisualTreeParent<ItemsControl>();
      int index = parent.ItemContainerGenerator.IndexFromContainer(parent.ItemContainerGenerator.ContainerFromItem(ele.DataContext));
      return index + 1;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

}
