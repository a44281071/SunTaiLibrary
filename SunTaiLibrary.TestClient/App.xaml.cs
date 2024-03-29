﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace SunTaiLibrary.TestClient
{
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
            SingleInstance.ActivateWindow(Process.GetCurrentProcess());

            Trace.TraceInformation("Received args: {0}", String.Join(" ", args));

            return true;
        }
    }
}