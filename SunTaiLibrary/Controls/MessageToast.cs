using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using SunTaiLibrary.Controls;

namespace System.Windows
{
  /// <summary>
  /// 通过UI线程，在当前活动窗体下方弹出通知消息的浮动框
  /// </summary>
  public static class MessageToast
  {
    /// <summary>
    /// 通过UI线程，在当前窗体显示 “弹出消息”，默认 10 秒后自动消失
    /// <para>如果 自动消失时间 小于等于 0，则不会自动消失，只能通过鼠标点击隐藏</para>
    /// </summary>
    /// <param name="pMessage">要显示的内容</param>
    /// <param name="pHideMessageSeconds">自动消失时间，默认 10 秒</param>
    public static void Show(object pMessage, double pHideMessageSeconds = 10)
    {
      Application.Current.Dispatcher.BeginInvoke(new Action(() =>
      {
        Application.Current.Windows
         .Cast<Window>()
         .Where(dd => dd.IsActive)
         .ForFirst(iWin =>
         {
           Grid panel;
           //其他类型的容器（不支持多级Child）
           if ((iWin.Content as Grid)?.Name != "ShellGridForSingleCell")
           {
             //改为嵌套一个支持多级Child的容器
             var oldContent = iWin.Content as UIElement;
             iWin.Content = null;
             var grid = new Grid() { Name = "ShellGridForSingleCell" };
             grid.Children.Add(oldContent);
             iWin.Content = grid;
             panel = grid;
           }
           else
           {
             panel = (Grid)iWin.Content;
           }

           // 手动 隐藏旧的提示
           panel.Children.OfType<ToastControl>()
             .ToList()
             .ForEach(dd => panel.Children.Remove(dd));

           // 重新显示提示信息
           var toast = new ToastControl
           {
             FlyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(pHideMessageSeconds)),
             Content = pMessage
           };
           panel.Children.Add(toast);
         });
      }), null);
    }
  }
}