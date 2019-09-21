using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SunTaiLibrary.Controls
{
  public class ToastControl : ButtonBase
  {
    static ToastControl()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ToastControl), new FrameworkPropertyMetadata(typeof(ToastControl)));
      FlyIn_Anime = new DoubleAnimation { From = 100, Duration = Fly_Duration };
      FlyOut_Anime = new DoubleAnimation { To = 100, Duration = Fly_Duration };
    }

    public ToastControl()
    {
      FlyIn_Anime.Completed += FlyIn_Anime_Completed;
    }

    /// <summary>
    /// 飞出飞入动画持续时间（100毫秒）
    /// </summary>
    private static readonly Duration Fly_Duration = new Duration(TimeSpan.FromMilliseconds(100));

    /// <summary>
    /// 默认控件飞出延时（10秒后飞出）
    /// </summary>
    private static readonly KeyTime Default_FlyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(10));

    private static readonly DoubleAnimation FlyIn_Anime;
    private static readonly DoubleAnimation FlyOut_Anime;

    private readonly DispatcherTimer _Timer = new DispatcherTimer();
    private TranslateTransform translateTransform;

    #region 依赖属性

    /// <summary>
    /// 控件飞出延时（毫秒）
    /// </summary>
    public KeyTime FlyTime
    {
      get { return (KeyTime)GetValue(FlyTimeProperty); }
      set { SetValue(FlyTimeProperty, value); }
    }

    /// <summary>
    /// 默认10秒后控件飞出，之后消失
    /// </summary>
    public static readonly DependencyProperty FlyTimeProperty =
        DependencyProperty.Register("FlyTime", typeof(KeyTime), typeof(ToastControl), new PropertyMetadata(Default_FlyTime));

    #endregion 依赖属性

    #region 路由事件

    public event RoutedEventHandler FlyOut
    {
      add { AddHandler(FlyOutEvent, value); }
      remove { RemoveHandler(FlyOutEvent, value); }
    }

    public static readonly RoutedEvent FlyOutEvent = EventManager.RegisterRoutedEvent(
      "FlyOutEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToastControl));

    #endregion 路由事件

    private void FlyIn_Anime_Completed(object sender, EventArgs e)
    {
      FlyIn_Anime.Completed -= FlyIn_Anime_Completed;
      _Timer.Interval = FlyTime.TimeSpan;
      _Timer.Tick += Timer_Tick;
      _Timer.Start();
    }

    /// <summary>
    /// 飞出动画结束后，销毁自身。
    /// </summary>
    private void FlyOut_Anime_Completed(object sender, EventArgs e)
    {
      FlyOut_Anime.Completed -= FlyOut_Anime_Completed;
      (Parent as Panel)?.Children?.Remove(this);
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      _Timer.Stop();
      _Timer.Tick -= Timer_Tick;
      OnFlyOut();
    }

    protected override void OnClick()
    {
      OnFlyOut();
      base.OnClick();
    }

    /// <summary>
    /// 当从父级移除时，停止计时器，不再触发飞出动画。
    /// </summary>
    protected override void OnVisualParentChanged(DependencyObject oldParent)
    {
      if (oldParent != null)
      {
        if (_Timer.IsEnabled)
        {
          // 手动移除，意思就是说还没触发飞出动画
          _Timer.Stop();
        }
      }
      base.OnVisualParentChanged(oldParent);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      translateTransform = new TranslateTransform();
      RenderTransform = translateTransform;

      // 触发飞入动画
      if (translateTransform != null)
      {
        translateTransform.BeginAnimation(TranslateTransform.YProperty, FlyIn_Anime, HandoffBehavior.SnapshotAndReplace);
      }
    }

    /// <summary>
    /// 触发飞出动画与事件
    /// </summary>
    protected virtual void OnFlyOut()
    {
      if (translateTransform != null)
      {
        FlyOut_Anime.Completed += FlyOut_Anime_Completed;
        translateTransform.BeginAnimation(TranslateTransform.YProperty, FlyOut_Anime, HandoffBehavior.SnapshotAndReplace);
      }
      RaiseEvent(new RoutedEventArgs(FlyOutEvent, this));
    }
  }
}