using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary.Dependencies
{
  internal class EventTriggerGenericHandler<TSender, TArgs>
  {
    private readonly Action<object, object> action;

    public EventTriggerGenericHandler(Action<object, object> action)
    {
      this.action = action;
    }

    public void Handler(TSender sender, TArgs args)
    {
      action(sender, args);
    }
  }
}
