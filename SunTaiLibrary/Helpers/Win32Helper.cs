using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary
{
  public static class Win32Helper
  {
    /// <summary>
    /// Tests whether the current user is a member of the Administrator's group.
    /// </summary>
    [DllImport("shell32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsUserAnAdmin();
  }
}