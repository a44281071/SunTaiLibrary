using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xaml;

namespace SunTaiLibrary.Dependencies
{
  internal class EventAction : IActionTarget
  {
    public EventAction(object targetObject, string path, Type eventHandlerType)
    {
      this.targetObject = new WeakReference(targetObject);
      this.path = path;
      this.eventHandlerType = eventHandlerType;
    }

    private readonly WeakReference targetObject;
    private readonly string path;
    private readonly Type eventHandlerType;

    public object ProvideValue(IServiceProvider serviceProvider)
    {
      if (UiContextHelper.InDesignMode) return ActionExtensionHelper.CreateEventHandler(eventHandlerType, (ss, ee) => Task.Yield());

      FrameworkElement ele = targetObject.Target as FrameworkElement;
      if (ele == null)
      {
        // not a FrameworkElement xaml element, get root parent.
        var rootObjectProvider = serviceProvider.GetService<IRootObjectProvider>();
        ele = rootObjectProvider?.RootObject as FrameworkElement;
      }
      // ensure dataContext.
      object data = ele?.DataContext;
      if (data == null) return DependencyProperty.UnsetValue;

      var dct = data.GetType();
      var dcm = dct.GetMember(path).FirstOrDefault();

      //if (dcm == null) throw new NullReferenceException($"Can not find member '{path}' in DataContext {}");

      return dcm?.MemberType switch
      {
        MemberTypes.Property => ActionExtensionHelper.CreateEventHandler(eventHandlerType, (ss, ee) =>
        {
          var cmd = (dcm as PropertyInfo).GetValue(data) as ICommand;
          cmd.Execute(ele);
        }),
        MemberTypes.Method => ActionExtensionHelper.CreateEventHandler(eventHandlerType, (ss, ee) =>
        {
          (dcm as MethodInfo).Invoke(data, null);
        }),
        _ => throw new NotSupportedException($"Can not find object member to bind. Path = {path}, DataContext = {dct}")
      };
    }
  }
}