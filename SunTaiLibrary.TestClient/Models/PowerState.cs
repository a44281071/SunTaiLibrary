using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary.TestClient
{
  public enum PowerState
  {
    [Description("关闭")]
    Off,
    [Description("开启")]
    On
  }
}
