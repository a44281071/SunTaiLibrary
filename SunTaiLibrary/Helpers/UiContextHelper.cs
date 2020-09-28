using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SunTaiLibrary
{
  public static class UiContextHelper
  {
    private static Lazy<bool> _InDesignMode = new Lazy<bool>(GenerateInDesignMode);

    /// <summary>
    /// Gets a value indicating whether design mode is currently active.
    /// </summary>
    public static bool InDesignMode => _InDesignMode.Value;

    private static bool GenerateInDesignMode()
    {
      var descriptor = DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement));
      return (bool)descriptor.Metadata.DefaultValue;
    }
  }
}