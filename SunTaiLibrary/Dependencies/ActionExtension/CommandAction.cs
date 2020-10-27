using SunTaiLibrary.Commands;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SunTaiLibrary.Dependencies
{
  internal class CommandAction : IActionTarget
  {
    public CommandAction(object targetObject, string path)
    {
      this.targetObject = new WeakReference(targetObject);
      this.path = path;
    }

    private readonly WeakReference targetObject;
    private readonly string path;

    public object ProvideValue(IServiceProvider serviceProvider)
    {
      if (UiContextHelper.InDesignMode) return new RelayCommand(() => Task.Yield());

      FrameworkElement ele = targetObject.Target as FrameworkElement;
      if (ele == null) throw new NullReferenceException("target object did not type 'FrameworkElement'");
      if (ele.DataContext == null) return DependencyProperty.UnsetValue;

      var dct = ele.DataContext.GetType();
      var dcm = dct.GetMember(path).FirstOrDefault();
      if (dcm == null) throw new NullReferenceException($"target object is did have member '{path}'");

      return dcm.MemberType switch
      {
        MemberTypes.Property => (dcm as PropertyInfo).GetValue(ele.DataContext) as ICommand,
        MemberTypes.Method => new RelayCommand(() =>
        {
          (dcm as MethodInfo).Invoke(ele.DataContext, null);
        }),
        _ => throw new NotSupportedException($"Can not find object member to bind. Path = {path}, DataContext = {dct}")
      };
    }
  }
}