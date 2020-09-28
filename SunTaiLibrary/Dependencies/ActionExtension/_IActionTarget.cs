using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SunTaiLibrary.Dependencies
{
  internal interface IActionTarget
  {
    object ProvideValue();
  }
}