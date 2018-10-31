using System.Windows;
using System.Windows.Controls;

namespace SunTaiLibrary.Controls
{
  /// <summary>
  /// 自定义窗体右上角的功能按钮组（最小化，最大化，还原，关闭）
  /// <see cref="https://github.com/Grabacr07/MetroRadiance"/>
  /// </summary>
  public class SystemButtons : Control
    { 
        static SystemButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SystemButtons), new FrameworkPropertyMetadata(typeof(SystemButtons)));
        }
    }
}
