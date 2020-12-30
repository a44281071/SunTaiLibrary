using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Tasks
{
  public static class TaskExtension
  {
    /// <summary>
    /// Task with time out.
    /// </summary>
    /// <exception cref="TimeoutException"/>
    public static async Task TimeoutAfter(this Task task, int millisecondsTimeout)
    {
      if (task == await Task.WhenAny(task, Task.Delay(millisecondsTimeout)))
        await task;
      else
        throw new TimeoutException();
    }

    /// <summary>
    /// Task with time out.
    /// </summary>
    /// <exception cref="TimeoutException"/>
    public static async Task<T> TimeoutAfter<T>(this Task<T> task, int millisecondsTimeout)
    {
      if (task == await Task.WhenAny(task, Task.Delay(millisecondsTimeout)))
        return await task;
      else
        throw new TimeoutException();
    }
  }
}