using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Threading.Tasks;

namespace SunTaiLibrary.TestClient
{
  public class AppBootstrapper : Caliburn.Micro.BootstrapperBase
  {
    public AppBootstrapper()
    {
      Initialize();
    }

    private SimpleContainer container;

    protected override void Configure()
    {
      container = new SimpleContainer();

      container.Singleton<IWindowManager, WindowManager>();
      container.Singleton<IEventAggregator, EventAggregator>();
      container.PerRequest<MainViewModel>();
    }

    protected override object GetInstance(Type service, string key)
    {
      return container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
      return container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
      container.BuildUp(instance);
    }

    protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
    {
      DisplayRootViewFor<MainViewModel>();
    }
  }
}