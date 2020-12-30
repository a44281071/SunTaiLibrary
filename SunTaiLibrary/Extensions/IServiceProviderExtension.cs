using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  internal static class IServiceProviderExtension
  {
    public static T GetService<T>(this IServiceProvider serviceProvider)
    {
      return (T)serviceProvider.GetService(typeof(T));
    }
  }
}