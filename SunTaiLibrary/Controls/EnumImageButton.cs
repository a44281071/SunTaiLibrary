using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SunTaiLibrary.Controls
{
  /// <summary>
  /// 支持两个枚举值匹配并切换显示图片内容的图片按钮。
  /// 本控件一般用于支持异步操作，在不等待操作结果的情况下被动切换按钮显示图片。
  /// </summary>
  public class EnumImageButton : ButtonBase
  {
    static EnumImageButton()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumImageButton), new FrameworkPropertyMetadata(typeof(EnumImageButton)));
    }

    private void UpdateMatch()
    {
      if (MatchSource is Enum v1 && MatchTarget is Enum v2)
      {
        bool oldvalue = IsMatched;
        bool newvalue = v1.Equals(v2);
        if (oldvalue != newvalue)
        {
          SetValue(IsMatchedPropertyKey, newvalue);
        }
      }
    }

    #region 依赖属性（逻辑）

    /// <summary>
    /// 只读依赖属性，标识源枚举值和目标枚举值是否已匹配
    /// </summary>
    public bool IsMatched
    {
      get { return (bool)GetValue(IsMatchedProperty); }
    }

    /// <summary>
    /// 获取一个值，标识源枚举值和目标枚举值是否已匹配
    /// </summary>
    internal static readonly DependencyPropertyKey IsMatchedPropertyKey =
        DependencyProperty.RegisterReadOnly("IsMatched", typeof(bool), typeof(EnumImageButton)
          , new PropertyMetadata(false));

    /// <summary>
    /// 获取一个值，标识源枚举值和目标枚举值是否已匹配
    /// </summary>
    public static readonly DependencyProperty IsMatchedProperty = IsMatchedPropertyKey.DependencyProperty;

    /// <summary>
    /// 要匹配的源枚举值
    /// </summary>
    [Category("Common")]
    public Enum MatchSource
    {
      get { return (Enum)GetValue(MatchSourceProperty); }
      set { SetValue(MatchSourceProperty, value); }
    }

    /// <summary>
    /// 要匹配的源枚举值
    /// </summary>
    public static readonly DependencyProperty MatchSourceProperty =
        DependencyProperty.Register("MatchSource", typeof(Enum), typeof(EnumImageButton)
          , new PropertyMetadata(OnMatchSourceChanged));

    private static void OnMatchSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var element = (EnumImageButton)d;
      element.UpdateMatch();
    }

    /// <summary>
    /// 要匹配的目标枚举值
    /// </summary>
    [Category("Common")]
    public Enum MatchTarget
    {
      get { return (Enum)GetValue(MatchTargetProperty); }
      set { SetValue(MatchTargetProperty, value); }
    }

    /// <summary>
    /// 要匹配的目标枚举值
    /// </summary>
    public static readonly DependencyProperty MatchTargetProperty =
        DependencyProperty.Register("MatchTarget", typeof(Enum), typeof(EnumImageButton)
          , new PropertyMetadata(OnMatchTargetChanged));

    private static void OnMatchTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var element = (EnumImageButton)d;
      element.UpdateMatch();
    }

    #endregion 依赖属性（逻辑）

    #region 依赖属性（显示）

    /// <summary>
    /// 枚举源和目标未匹配时的图片
    /// </summary>
    [Category("Common")]
    public ImageSource NormalImage
    {
      get { return (ImageSource)GetValue(NormalImageProperty); }
      set { SetValue(NormalImageProperty, value); }
    }

    /// <summary>
    /// 枚举源和目标未匹配时的图片
    /// </summary>
    public static readonly DependencyProperty NormalImageProperty =
        DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(EnumImageButton), new PropertyMetadata(null));

    /// <summary>
    /// 枚举源和目标匹配时的图片
    /// </summary>
    [Category("Common")]
    public ImageSource MatchedImage
    {
      get { return (ImageSource)GetValue(MatchedImageProperty); }
      set { SetValue(MatchedImageProperty, value); }
    }

    /// <summary>
    /// 枚举源和目标匹配时的图片
    /// </summary>
    public static readonly DependencyProperty MatchedImageProperty =
        DependencyProperty.Register("MatchedImage", typeof(ImageSource), typeof(EnumImageButton), new PropertyMetadata(null));

    #endregion 依赖属性（显示）

    /// <summary>
    /// 如果状态已匹配，则忽略点击操作。
    /// </summary>
    protected override void OnClick()
    {
      if (IsMatched)
      {
        return;
      }
      base.OnClick();
    }
  }
}