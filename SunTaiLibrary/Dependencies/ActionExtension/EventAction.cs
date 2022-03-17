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

        private WeakReference<Action<object, object>> memberHandler = null;

        private void LoadMember(IServiceProvider serviceProvider)
        {
            if (targetObject.Target is not FrameworkElement ele)
            {
                // not a FrameworkElement xaml element, get root parent.
                var rootObjectProvider = serviceProvider.GetService<IRootObjectProvider>();
                ele = rootObjectProvider?.RootObject as FrameworkElement;
            }
            // ensure dataContext.           
            if (ele?.DataContext is object data)
            {
                var dct = data.GetType();
                var member = dct.GetMember(path).FirstOrDefault();
                if (member is PropertyInfo pi)
                {
                    if (pi.GetValue(data) is ICommand cmd)
                    {
                        memberHandler = new((ss, ee) => cmd.Execute(ele));
                    }
                }
                else if (member is MethodInfo mi)
                {
                    memberHandler = new((ss, ee) => mi.Invoke(data, new object[] { }));
                }
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (UiContextHelper.InDesignMode) return ActionExtensionHelper.CreateEventHandler(eventHandlerType, (ss, ee) => Task.Yield());

            return ActionExtensionHelper.CreateEventHandler(eventHandlerType, (ss, ee) =>
            {
                if (memberHandler == null) LoadMember(serviceProvider);
                if (memberHandler.TryGetTarget(out var mem))
                {
                    mem?.Invoke(ss, ee);
                }
            });
        }
    }
}