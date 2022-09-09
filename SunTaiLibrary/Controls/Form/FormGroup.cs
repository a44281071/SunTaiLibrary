using System.Windows;
using System.Windows.Controls;

namespace SunTaiLibrary.Controls
{
    /// <summary>
    /// form panel group
    /// </summary>
    public class FormGroup : HeaderedItemsControl
    {
        static FormGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormGroup), new FrameworkPropertyMetadata(typeof(FormGroup)));
        }
    }
}