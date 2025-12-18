using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace SunTaiLibrary.Controls;

/// <summary>
/// 能显示刻度标签的刻度条
/// </summary>
public class LabeledTickBar : TickBar
{
    public double ScaleX
    {
        get { return (double)GetValue(ScaleXProperty); }
        set { SetValue(ScaleXProperty, value); }
    }

    public static readonly DependencyProperty ScaleXProperty =
        DependencyProperty.Register(nameof(ScaleX), typeof(double), typeof(LabeledTickBar)
            , new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

    /// <summary>
    /// 每隔多少刻度显示一次单位值，0 表示不显示
    /// </summary>
    public double LabelInterval
    {
        get => (double)GetValue(LabelIntervalProperty);
        set => SetValue(LabelIntervalProperty, value);
    }

    public static readonly DependencyProperty LabelIntervalProperty =
        DependencyProperty.Register(nameof(LabelInterval), typeof(double), typeof(LabeledTickBar)
            , new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsRender));

    protected override Size MeasureOverride(Size availableSize)
    {
        double dw = double.IsNaN(Width)
            ? ScaleX * (Maximum - Minimum)    // 使用刻度做为宽度参考
            : Width;                          // 固定宽度

        return new Size(dw, 5);   // 5个高保证能够显示刻度线
    }

    protected override void OnRender(DrawingContext dc)
    {
        //base.OnRender(dc);

        if (LabelInterval <= 0 || Maximum <= Minimum) return;

        var typeface = new Typeface("Segoe UI");
        var brush = Fill ?? Brushes.Black;

        double range = Maximum - Minimum;
        double step = LabelInterval;
        double stepLength = step / range * ActualWidth;

        double lineH1 = ActualHeight * 0.2;  // 1主线起始点。

        for (double tick = Minimum; tick <= Maximum; tick += step)
        {
            // 计算位置（假设水平）
            double x = (tick - Minimum) / range * ActualWidth;

            // 绘制刻度线 - 长线
            dc.DrawLine(new Pen(brush, 0.5), new Point(x, lineH1), new Point(x, ActualHeight));

            // 绘制辅助刻度线 - 短线
            double hsX = x + stepLength / 2;
            dc.DrawLine(new Pen(brush, 0.5)
                , new Point(hsX, ActualHeight - 4)
                , new Point(hsX, ActualHeight));

            // 绘制文字
            string text = tick.ToString(CultureInfo.CurrentCulture);
            var ft = new FormattedText(
                text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                10,
                brush
#if NET462_OR_GREATER
            // .NET Framework 4.6.2 及以上
               , VisualTreeHelper.GetDpi(this).PixelsPerDip);
#else           
            // .NET Framework 4.7.x 及以下（含 4.6.1）
               );
#endif

            dc.DrawText(ft, new Point(x + 2, 2));
        }
    }
}