using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SunTaiLibrary.NewTestClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        public App()
        {
            if (SingleInstance.InitializeAsFirstInstance(this))
            {
                // ok
            }
            else
            {
                // instance exists, exit.
                Shutdown(1);
            }
        }

        public string UniqueName => "suntai.library.test.client";

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            // handle command line arguments of second instance
            SingleInstance.ActivateWindow();

            Trace.TraceInformation("Received args: {0}", String.Join(" ", args));

            return true;
        }
    }
}