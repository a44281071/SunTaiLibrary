using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary.Helpers
{
  public static class ProcessHelper
  {
    /// <summary>
    /// 打开或执行一个进程。
    /// </summary>
    public static Process RunProcess(string name, string arguments = null)
    {
      var pp = new Process
      {
        StartInfo = new ProcessStartInfo(name, arguments)
      };

      pp.Start();
      return pp;
    }

    /// <summary>
    /// Open a window view with the specified folder, file or application selected.
    /// </summary>
    /// <param name="curFullFilename">folder, file or application</param>
    public static Process ExplorerSelect(string curFullFilename)
    {
      return Process.Start("Explorer.exe", "/select," + curFullFilename);
    }
  }
}