using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary
{
  [SuppressUnmanagedCodeSecurity]
  public static class Win32Helper
  {
    /// <summary>
    /// Tests whether the current user is a member of the Administrator's group.
    /// </summary>
    [DllImport("shell32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsUserAnAdmin();

    /// <summary>
    ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
    ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。
    /// </summary>
    /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
    /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
    [DllImport("User32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    /// <summary>
    /// 确定给定窗口是否是最小化（图标化）的窗口
    /// </summary>
    [DllImport("user32")]
    public static extern bool IsIconic(IntPtr hWnd);

    /// <summary>
    /// 恢复一个最小化的程序，并将其激活
    /// </summary>
    [DllImport("user32")]
    public static extern bool OpenIcon(IntPtr hWnd);
  }
}