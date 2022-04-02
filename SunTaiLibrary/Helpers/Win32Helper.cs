using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary
{
    /// <summary>
    /// 用于访问win api的帮助类。
    /// </summary>
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

        /// <summary>
        /// 使应用程序能够通知系统它正在使用中，从而防止系统在应用程序运行时进入睡眠状态或关闭显示器。
        /// </summary>
        /// <seealso href="http://www.pinvoke.net/default.aspx/kernel32/SetThreadExecutionState.html">MSDN</seealso>
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate">MSDN</seealso>
        /// <param name="esFlags">线程的执行要求。该参数可以是以下一个或多个值。</param>
        /// <returns>如果函数成功，则返回值为之前的线程执行状态。如果函数失败，则返回值为NULL。</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        /// <summary>
        /// 使应用程序能够通知系统它正在使用中，从而防止系统在应用程序运行时进入睡眠状态或关闭显示器。
        /// </summary>
        /// <param name="keepAwake">【true：保持活跃状态，防止睡眠】【false：恢复默认状态】</param>
        public static void SetThreadExecutionState(bool keepAwake)
        {
            if (keepAwake)
            {
                // Television recording is beginning. Enable away mode and prevent the sleep idle time-out.
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
            }
            else
            {
                // Clear EXECUTION_STATE flags to disable away mode and allow the system to idle to sleep normally.
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }
        }
    }

    /// <summary>
    /// 线程的执行要求。
    /// </summary>
    [Flags]
    public enum EXECUTION_STATE : uint
    {
        /// <summary>
        /// 启用离开模式。
        /// </summary>
        ES_AWAYMODE_REQUIRED = 0x00000040,

        /// <summary>
        /// 通知系统正在设置的状态应该保持有效，直到使用ES_CONTINUOUS的下一个调用和其他状态标志之一被清除。
        /// </summary>
        ES_CONTINUOUS = 0x80000000,

        /// <summary>
        /// 通过重置显示空闲计时器强制显示打开。
        /// </summary>
        ES_DISPLAY_REQUIRED = 0x00000002,

        /// <summary>
        /// 通过重置系统空闲计时器强制系统处于工作状态。
        /// </summary>
        ES_SYSTEM_REQUIRED = 0x00000001,
    }
}