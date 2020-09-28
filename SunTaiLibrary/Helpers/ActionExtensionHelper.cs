using SunTaiLibrary.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SunTaiLibrary
{
  internal static class ActionExtensionHelper
  {
    private static bool IsEventCorrect(Type eventHandlerType)
    {
      if (!typeof(Delegate).IsAssignableFrom(eventHandlerType)) return false;

      ParameterInfo[] parameters = GetParameters(eventHandlerType);
      if (parameters.Length != 2) return false;
      if (!typeof(object).IsAssignableFrom(parameters[0].ParameterType)) return false;
      if (!typeof(object).IsAssignableFrom(parameters[1].ParameterType)) return false;
      return true;
    }

    private static ParameterInfo[] GetParameters(Type eventHandlerType)
    {
      return eventHandlerType.GetMethod("Invoke").GetParameters();
    }

    public static Delegate CreateEventHandler(Type eventHandlerType, Action<object, object> eventHandler)
    {
      if (!IsEventCorrect(eventHandlerType)) return null;
      ParameterInfo[] parameters = GetParameters(eventHandlerType);
      Type handlerType = typeof(EventTriggerGenericHandler<,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType);
      object instance = Activator.CreateInstance(handlerType, new object[] { eventHandler });
      return Delegate.CreateDelegate(eventHandlerType, instance, instance.GetType().GetMethod("Handler"));
    }
  }
}