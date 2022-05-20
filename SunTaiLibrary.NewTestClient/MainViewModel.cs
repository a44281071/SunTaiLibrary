using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SunTaiLibrary.Commands;

namespace SunTaiLibrary.NewTestClient
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Items = Enumerable.Range(0, 99)
                .Select(dd => new Version(1, 1, 1, 1))
                .ToArray();
        }

        #region Bind_Data

        public Version[] Items { get; }

        #endregion Bind_Data
    }
}