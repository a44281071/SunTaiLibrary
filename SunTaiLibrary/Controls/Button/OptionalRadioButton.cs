using System.Windows;
using System.Windows.Controls;

namespace SunTaiLibrary.Controls
{
  /// <summary>
  /// 支持取消选择的单选按钮（当按钮选中时再次点击将取消选中）
  /// </summary>
  public class OptionalRadioButton : RadioButton
  {
    #region bool IsOptional dependency property

    public static DependencyProperty IsOptionalProperty =
        DependencyProperty.Register(
            "IsOptional",
            typeof(bool),
            typeof(OptionalRadioButton),
            new PropertyMetadata(true));

    /// <summary>
    /// 是否启用当按钮选中时再次点击将取消选中（默认启用）
    /// </summary>
    public bool IsOptional
    {
      get { return (bool)GetValue(IsOptionalProperty); }
      set { SetValue(IsOptionalProperty, value); }
    }

    #endregion bool IsOptional dependency property

    /// <summary>
    /// if checked, click will set to uncheck.
    /// </summary>
    protected override void OnToggle()
    {
      bool? wasChecked = IsChecked;
      if (IsOptional && wasChecked == true)
      {
        IsChecked = false;
      }
      else
      {
        base.OnToggle();
      }
    }
  }
}