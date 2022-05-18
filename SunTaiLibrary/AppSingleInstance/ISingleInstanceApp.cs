//-----------------------------------------------------------------------
// <copyright file="SingleInstance.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
//     This class checks to make sure that only one instance of
//     this application is running at a time.
// </summary>
// <see>https://www.codeproject.com/Articles/84270/WPF-Single-Instance-Application</see>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace SunTaiLibrary
{
    /// <summary>
    /// single instance app.
    /// </summary>
    public interface ISingleInstanceApp
    {
        /// <summary>
        /// Program unique identification.
        /// </summary>
        string UniqueName { get; }

        /// <summary>
        /// receive a signal from follow-up app instance.
        /// </summary>
        bool SignalExternalCommandLineArgs(IList<string> args);
    }
}