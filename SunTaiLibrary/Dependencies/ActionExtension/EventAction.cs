using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SunTaiLibrary.Dependencies
{
  internal class EventAction : IActionTarget
  {
    public EventAction(object targetObject, string path, Type eventHandlerType)
    {
      this.targetObject = targetObject;
      this.path = path;
      this.eventHandlerType = eventHandlerType;
    }

    private readonly object targetObject;
    private readonly string path;
    private readonly Type eventHandlerType;

    public object ProvideValue()
    {
      if (UiContextHelper.InDesignMode) return ActionExtensionHelper.CreateEventHandler(eventHandlerType, (ss, ee) => Task.Yield());

      var ele = targetObject as FrameworkElement;
      var dct = ele.DataContext.GetType();
      var dcm = dct.GetMember(path).FirstOrDefault();
      return dcm.MemberType switch
      {
        MemberTypes.Property => ActionExtensionHelper.CreateEventHandler(eventHandlerType, (ss, ee) =>
        {
          var cmd = (dcm as PropertyInfo).GetValue(ele.DataContext) as ICommand;
          cmd.Execute(ele);
        }),
        MemberTypes.Method => ActionExtensionHelper.CreateEventHandler(eventHandlerType, (ss, ee) =>
        {
          (dcm as MethodInfo).Invoke(ele.DataContext, null);
        }),
        _ => throw new NotSupportedException($"Can not find object member to bind. Path = {path}, DataContext = {dct}")
      };
    }
  }
}