using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace SunTaiLibrary.TestClient
{
  public partial class App : Application, ISingleInstanceApp
  {
    public App()
    {
      if (SingleInstance<App>.InitializeAsFirstInstance(app_guid))
      {
        // ok
      }
      else
      {
        // instance exists, exit.
        Shutdown(1);
      }
    }

    private const string app_guid = "suntai.library.test.client";

    public bool SignalExternalCommandLineArgs(IList<string> args)
    {
      // handle command line arguments of second instance
      SingleInstance<App>.ActivateWindow(Process.GetCurrentProcess());
      Current.MainWindow.WindowState = WindowState.Maximized;
      Current.MainWindow.Topmost = true;
      return true;
    }
  }
}
