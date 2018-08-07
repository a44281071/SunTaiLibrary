using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SunTaiLibrary.Controls
{
  /// <summary>
  /// 提供一个画布，实现了里面的内容从右向左飘过的跑马灯效果。
  /// </summary>
  public class MarqueeCanvas : Canvas
  {
    public MarqueeCanvas()
    {
      _Timer = new DispatcherTimer(TimeSpan.FromMilliseconds(DefaultSpeed), DispatcherPriority.Render, Timer_Tick, Dispatcher);
      WeakEventManager<MarqueeCanvas, RoutedEventArgs>.AddHandler(this, "Loaded", MarqueeCanvas_Loaded);
      WeakEventManager<MarqueeCanvas, RoutedEventArgs>.AddHandler(this, "Unloaded", MarqueeCanvas_Unloaded);
    }

    private const double DefaultSpeed = 33.33d;
    private readonly DispatcherTimer _Timer;


    /// <summary>
    /// 是否强制启用跑马灯效果。
    /// 默认不强制，表示子内容如果未被画布裁切，属于完整显示，则不启用跑马灯效果。
    /// 设置为强制，则不考虑大小，强制执行跑马灯效果。
    /// </summary>
    public bool IsForcing
    {
      get { return (bool)GetValue(IsForcingProperty); }
      set { SetValue(IsForcingProperty, value); }
    }

    public static readonly DependencyProperty IsForcingProperty =
        DependencyProperty.Register("IsForcing", typeof(bool), typeof(MarqueeCanvas), new PropertyMetadata(false));



    /// <summary>
    /// 一个大于 0 的有限值，指定此时间线的时间相对于其父级速度的前进速率。
    /// 其中 1 表示正常速度，2 表示双倍速度，0.5 为半速，依此类推。默认值为 1。
    /// </summary>
    public double SpeedRatio
    {
      get { return (double)GetValue(SpeedRatioProperty); }
      set { SetValue(SpeedRatioProperty, value); }
    }

    public static readonly DependencyProperty SpeedRatioProperty =
        DependencyProperty.Register("SpeedRatio", typeof(double), typeof(MarqueeCanvas)
          , new PropertyMetadata(1d, SpeedRatioChanged));

    private static void SpeedRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var ele = (MarqueeCanvas)d;
      var ratio = (double)e.NewValue;
      ele._Timer.Interval = TimeSpan.FromMilliseconds(DefaultSpeed / ratio);
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      foreach (UIElement jchild in Children)
      {
        if (jchild.IsMouseOver) return;
        if (jchild is FrameworkElement jfe)
        {
          var canvas_width = ActualWidth;
          var child_width = jfe.ActualWidth;
          if (IsForcing
            || child_width > canvas_width)
          {
            var left = GetLeft(jchild);
            left = Double.IsNaN(left) ? 0d : left;
            var minWidth = Math.Min(canvas_width, child_width);
            if ((0 - left) > minWidth)
            {
              // 飞出左侧，复位至最右侧
              SetLeft(jchild, canvas_width);
            }
            else if (left > canvas_width)
            {
              // 飞出右侧，复位至最右侧（最有可能的原因是画布大小改变导致宽度变窄）
              SetLeft(jchild, canvas_width);
            }
            else
            {
              // 在画布中，左移一像素。
              SetLeft(jchild, left - 1);
            }
          }

        }
      }
    }

    private void MarqueeCanvas_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
      _Timer.Start();
    }

    private void MarqueeCanvas_Unloaded(object sender, System.Windows.RoutedEventArgs e)
    {
      _Timer.Stop();
    }
  }
}