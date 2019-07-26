using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary
{
  public class EventArgs<T> : EventArgs
  {
    public EventArgs()
    {
    }

    public EventArgs(T args)
    {
      Args = args;
    }

    public T Args { get; }
  }
}