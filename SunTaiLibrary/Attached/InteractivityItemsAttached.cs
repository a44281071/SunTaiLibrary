using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SunTaiLibrary.Attached
{
  /// <summary>
  /// <see cref="FrameworkTemplate"/> for InteractivityElements instance
  /// <remarks>Subclassed for forward compatibility, perhaps one day <see cref="FrameworkTemplate"/> </remarks>
  /// <remarks>will not be partially internal</remarks>
  /// </summary>
  public class InteractivityTemplate : DataTemplate
  {

  }

  /// <summary>
  /// Holder for interactivity entries.
  /// 支持在Style.Setters里设置附加行为。
  /// </summary> 
  public class InteractivityItemsAttached : FrameworkElement
  {
    private List<Microsoft.Xaml.Behaviors.Behavior> _behaviors;
    private List<Microsoft.Xaml.Behaviors.TriggerBase> _triggers;

    /// <summary>
    /// Storage for triggers
    /// </summary>
    public List<Microsoft.Xaml.Behaviors.TriggerBase> Triggers
    {
      get
      {
        if (_triggers == null)
          _triggers = new List<Microsoft.Xaml.Behaviors.TriggerBase>();
        return _triggers;
      }
    }

    /// <summary>
    /// Storage for Behaviors
    /// </summary>
    public List<Microsoft.Xaml.Behaviors.Behavior> Behaviors
    {
      get
      {
        if (_behaviors == null)
          _behaviors = new List<Microsoft.Xaml.Behaviors.Behavior>();
        return _behaviors;
      }
    }

    #region Template attached property

    public static InteractivityTemplate GetTemplate(DependencyObject obj)
    {
      return (InteractivityTemplate)obj.GetValue(TemplateProperty);
    }

    public static void SetTemplate(DependencyObject obj, InteractivityTemplate value)
    {
      obj.SetValue(TemplateProperty, value);
    }

    public static readonly DependencyProperty TemplateProperty =
        DependencyProperty.RegisterAttached("Template",
        typeof(InteractivityTemplate),
        typeof(InteractivityItemsAttached),
        new PropertyMetadata(default(InteractivityTemplate), OnTemplateChanged));

    private static void OnTemplateChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
      var dt = (InteractivityTemplate)e.NewValue;
#if (!SILVERLIGHT)
      dt.Seal();
#endif
      var ih = (InteractivityItemsAttached)dt.LoadContent();
      var bc = Microsoft.Xaml.Behaviors.Interaction.GetBehaviors(d);
      Microsoft.Xaml.Behaviors.TriggerCollection tc = Microsoft.Xaml.Behaviors.Interaction.GetTriggers(d);

      foreach (var behavior in ih.Behaviors)
        bc.Add(behavior);

      foreach (var trigger in ih.Triggers)
        tc.Add(trigger);
    }

    #endregion
  }
}
