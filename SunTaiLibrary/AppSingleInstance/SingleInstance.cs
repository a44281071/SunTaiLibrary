using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace SunTaiLibrary
{
    /// <summary>
    /// single app instance for WPF.
    /// </summary>
    public static class SingleInstance
    {
        /// <summary>
        /// Application mutex.
        /// </summary>
        private static Mutex singleInstanceMutex;

        /// <summary>
        /// List of command line arguments for the application.
        /// </summary>
        private static IList<string> commandLineArgs;

        /// <summary>
        /// Translate command line arguments by byte array.
        /// </summary>
        private static XmlSerializer serializer = new(typeof(string[]));

        /// <summary>
        /// Gets command line args - for ClickOnce deployed applications, command line args may not be passed directly, they have to be retrieved.
        /// </summary>
        /// <returns>List of command line arg strings.</returns>
        private static IList<string> GetCommandLineArgs()
        {
            string[] args = Environment.GetCommandLineArgs() ?? Array.Empty<string>();
            return new List<string>(args);
        }

        /// <summary>
        /// As first instance, start a pipe service, receive other later app Process Arguments.
        /// </summary>
        private static void CreateRemoteService(string name, ISingleInstanceApp app)
        {
            var serThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        using var pipeServer = new NamedPipeServerStream(name, PipeDirection.In);
                        while (true)
                        {
                            // Wait for a client to connect
                            pipeServer.WaitForConnection();
                            // get length.
                            var length_buffer = new byte[4];
                            pipeServer.Read(length_buffer, 0, 4);
                            int length = BitConverter.ToInt32(length_buffer, 0);
                            // get content.
                            var buffer = new byte[length];
                            pipeServer.Read(buffer, 0, length);
                            using var contentStream = new MemoryStream(buffer);
                            var content = (string[])serializer.Deserialize(contentStream);
                            app.SignalExternalCommandLineArgs(content);
                            // close
                            pipeServer.Disconnect();
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError("SignalInstance pipe server error. ex = {0}", ex);
                    }
                }
            })
            { IsBackground = true };
            serThread.Start();
        }

        /// <summary>
        /// send data to first instance.
        /// </summary>
        private static void SignalFirstInstance(string name, ISingleInstanceApp app)
        {
            try
            {
                using var pipeClient = new NamedPipeClientStream(".", name, PipeDirection.Out);
                pipeClient.Connect(3000);
                // ready content.
                using var contentStream = new MemoryStream();
                serializer.Serialize(contentStream, commandLineArgs.ToArray());
                contentStream.Flush();
                var buffer = contentStream.ToArray();
                int length = buffer.Length;
                var length_buffer = BitConverter.GetBytes(length);
                // send length.
                pipeClient.Write(length_buffer, 0, 4);
                // send content.
                pipeClient.Write(buffer, 0, length);
                // finish.
                pipeClient.Flush();
                pipeClient.Close();
            }
            catch (Exception ex)
            {
                Trace.TraceError("SignalInstance pipe client error. ex = {0}", ex);
            }
        }

        /// <summary>
        /// Checks if the instance of the application attempting to start is the first instance.
        /// If not, activates the first instance.
        /// </summary>
        /// <returns>True if this is the first instance of the application.</returns>
        public static bool InitializeAsFirstInstance<TApplication>(TApplication app)
               where TApplication : System.Windows.Application, ISingleInstanceApp
        {
            if (app is null) { throw new ArgumentNullException(nameof(app)); }

            commandLineArgs = GetCommandLineArgs();

            // Build unique application Id and the IPC channel name.
            string applicationIdentifier = app.UniqueName + Environment.UserName;

            // Create mutex based on unique application Id to check if this is the first instance of the application.
            singleInstanceMutex = new Mutex(true, applicationIdentifier, out bool firstInstance);
            if (firstInstance)
            {
                CreateRemoteService(applicationIdentifier, app);
            }
            else
            {
                SignalFirstInstance(applicationIdentifier, app);
            }

            return firstInstance;
        }

        /// <summary>
        /// Release single app instance Mutex, useful for restart application.
        /// </summary>
        public static void ReleaseInstanceMutex()
        {
            try
            {
                singleInstanceMutex?.ReleaseMutex();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <summary>
        /// 激活指定进程的主窗体，使其成为当前窗体
        /// </summary>
        /// <param name="pProcess">进程</param>
        public static void ActivateWindow(Process pProcess)
        {
            if (null == pProcess) return;

            IntPtr mainWindowHandle = pProcess.MainWindowHandle;
            if (mainWindowHandle != IntPtr.Zero)
            {
                NativeMethods.SetForegroundWindow(mainWindowHandle);

                if (NativeMethods.IsIconic(mainWindowHandle))
                {
                    NativeMethods.OpenIcon(mainWindowHandle);
                }
            }
        }

        /// <summary>
        /// active current process main window.
        /// </summary>
        public static void ActivateWindow()
        {
            ActivateWindow(Process.GetCurrentProcess());
        }

        /// <summary>
        /// Release single app instance Mutex, then start a new app instance, then shut down old app.
        /// </summary>
        /// <param name="app">single instance app</param>
        public static void Restart<TApp>(this TApp app) where TApp : Application, ISingleInstanceApp
        {
            ReleaseInstanceMutex();
            Process.Start(Process.GetCurrentProcess().MainModule!.FileName);
            app.Shutdown();
            /* or 
             * System.Diagnostics.Process.Start(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);  
            */
        }
    }

    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        /// <summary>
        /// Delegate declaration that matches WndProc signatures.
        /// </summary>
        public delegate IntPtr MessageHandler(WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled);

        [DllImport("shell32.dll", EntryPoint = "CommandLineToArgvW", CharSet = CharSet.Unicode)]
        private static extern IntPtr _CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string cmdLine, out int numArgs);

        [DllImport("kernel32.dll", EntryPoint = "LocalFree", SetLastError = true)]
        private static extern IntPtr _LocalFree(IntPtr hMem);

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

        public static string[] CommandLineToArgvW(string cmdLine)
        {
            IntPtr argv = IntPtr.Zero;
            try
            {
                argv = _CommandLineToArgvW(cmdLine, out int numArgs);
                if (argv == IntPtr.Zero)
                {
                    throw new System.ComponentModel.Win32Exception();
                }
                var result = new string[numArgs];

                for (int i = 0; i < numArgs; i++)
                {
                    IntPtr currArg = Marshal.ReadIntPtr(argv, i * Marshal.SizeOf(typeof(IntPtr)));
                    result[i] = Marshal.PtrToStringUni(currArg);
                }

                return result;
            }
            finally
            {
                IntPtr p = _LocalFree(argv);
                // Otherwise LocalFree failed.
                // Assert.AreEqual(IntPtr.Zero, p);
            }
        }
    }

    internal enum WM
    {
        NULL = 0x0000,
        CREATE = 0x0001,
        DESTROY = 0x0002,
        MOVE = 0x0003,
        SIZE = 0x0005,
        ACTIVATE = 0x0006,
        SETFOCUS = 0x0007,
        KILLFOCUS = 0x0008,
        ENABLE = 0x000A,
        SETREDRAW = 0x000B,
        SETTEXT = 0x000C,
        GETTEXT = 0x000D,
        GETTEXTLENGTH = 0x000E,
        PAINT = 0x000F,
        CLOSE = 0x0010,
        QUERYENDSESSION = 0x0011,
        QUIT = 0x0012,
        QUERYOPEN = 0x0013,
        ERASEBKGND = 0x0014,
        SYSCOLORCHANGE = 0x0015,
        SHOWWINDOW = 0x0018,
        ACTIVATEAPP = 0x001C,
        SETCURSOR = 0x0020,
        MOUSEACTIVATE = 0x0021,
        CHILDACTIVATE = 0x0022,
        QUEUESYNC = 0x0023,
        GETMINMAXINFO = 0x0024,

        WINDOWPOSCHANGING = 0x0046,
        WINDOWPOSCHANGED = 0x0047,

        CONTEXTMENU = 0x007B,
        STYLECHANGING = 0x007C,
        STYLECHANGED = 0x007D,
        DISPLAYCHANGE = 0x007E,
        GETICON = 0x007F,
        SETICON = 0x0080,
        NCCREATE = 0x0081,
        NCDESTROY = 0x0082,
        NCCALCSIZE = 0x0083,
        NCHITTEST = 0x0084,
        NCPAINT = 0x0085,
        NCACTIVATE = 0x0086,
        GETDLGCODE = 0x0087,
        SYNCPAINT = 0x0088,
        NCMOUSEMOVE = 0x00A0,
        NCLBUTTONDOWN = 0x00A1,
        NCLBUTTONUP = 0x00A2,
        NCLBUTTONDBLCLK = 0x00A3,
        NCRBUTTONDOWN = 0x00A4,
        NCRBUTTONUP = 0x00A5,
        NCRBUTTONDBLCLK = 0x00A6,
        NCMBUTTONDOWN = 0x00A7,
        NCMBUTTONUP = 0x00A8,
        NCMBUTTONDBLCLK = 0x00A9,

        SYSKEYDOWN = 0x0104,
        SYSKEYUP = 0x0105,
        SYSCHAR = 0x0106,
        SYSDEADCHAR = 0x0107,
        COMMAND = 0x0111,
        SYSCOMMAND = 0x0112,

        MOUSEMOVE = 0x0200,
        LBUTTONDOWN = 0x0201,
        LBUTTONUP = 0x0202,
        LBUTTONDBLCLK = 0x0203,
        RBUTTONDOWN = 0x0204,
        RBUTTONUP = 0x0205,
        RBUTTONDBLCLK = 0x0206,
        MBUTTONDOWN = 0x0207,
        MBUTTONUP = 0x0208,
        MBUTTONDBLCLK = 0x0209,
        MOUSEWHEEL = 0x020A,
        XBUTTONDOWN = 0x020B,
        XBUTTONUP = 0x020C,
        XBUTTONDBLCLK = 0x020D,
        MOUSEHWHEEL = 0x020E,

        CAPTURECHANGED = 0x0215,

        ENTERSIZEMOVE = 0x0231,
        EXITSIZEMOVE = 0x0232,

        IME_SETCONTEXT = 0x0281,
        IME_NOTIFY = 0x0282,
        IME_CONTROL = 0x0283,
        IME_COMPOSITIONFULL = 0x0284,
        IME_SELECT = 0x0285,
        IME_CHAR = 0x0286,
        IME_REQUEST = 0x0288,
        IME_KEYDOWN = 0x0290,
        IME_KEYUP = 0x0291,

        NCMOUSELEAVE = 0x02A2,

        DWMCOMPOSITIONCHANGED = 0x031E,
        DWMNCRENDERINGCHANGED = 0x031F,
        DWMCOLORIZATIONCOLORCHANGED = 0x0320,
        DWMWINDOWMAXIMIZEDCHANGE = 0x0321,

        #region Windows 7

        DWMSENDICONICTHUMBNAIL = 0x0323,
        DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326,

        #endregion Windows 7

        USER = 0x0400,

        // This is the hard-coded message value used by WinForms for Shell_NotifyIcon.
        // It's relatively safe to reuse.
        TRAYMOUSEMESSAGE = 0x800, //WM_USER + 1024

        APP = 0x8000,
    }
}