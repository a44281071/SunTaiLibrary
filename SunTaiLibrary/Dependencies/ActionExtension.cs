using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows;
using System.Xaml;

namespace SunTaiLibrary.Dependencies
{
  public class ActionExtension : MarkupExtension
  {
    public ActionExtension() : base()
    {
    }

    public ActionExtension(string path) : this()
    {
      Path = path;
    }

    public string Path { get; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      var pvt = serviceProvider.GetService<IProvideValueTarget>();
      //var s1 = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider));
      var s2 = serviceProvider.GetService<IXamlTypeResolver>();
      var s3 = serviceProvider.GetService<IXamlSchemaContextProvider>();
       
      var sdf=  s3.SchemaContext;

      IActionTarget result = pvt.TargetProperty switch
      {
        EventInfo ee
          => new EventAction(pvt.TargetObject, Path, ee.EventHandlerType),
        DependencyProperty dp when dp.PropertyType == typeof(ICommand)
          => new CommandAction(pvt.TargetObject, Path),
        MethodInfo mm
          => new EventAction(pvt.TargetObject, Path, mm.GetParameters().ElementAt(1).ParameterType),
        _ => throw new NotSupportedException($"Not support xaml attribute: {pvt.TargetProperty}")
      };

      return result.ProvideValue(serviceProvider);
    }
  }
}