using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace SunTaiLibrary.Controls;

/// <summary>
/// 跟随鼠标画竖线，还会在竖线旁显示附加内容
/// </summary>
public class VerticalLineDecorator : Decorator
{
    private bool _isMouseInside = false;

    public Brush Background
    {
        get { return (Brush)GetValue(BackgroundProperty); }
        set { SetValue(BackgroundProperty, value); }
    }

    public static readonly DependencyProperty BackgroundProperty =
        DependencyProperty.Register("Background", typeof(Brush), typeof(VerticalLineDecorator), new PropertyMetadata(Brushes.Transparent));

    public Brush LineBrush
    {
        get => (Brush)GetValue(LineBrushProperty);
        set => SetValue(LineBrushProperty, value);
    }

    public static readonly DependencyProperty LineBrushProperty =
        DependencyProperty.Register(nameof(LineBrush), typeof(Brush), typeof(VerticalLineDecorator),
            new FrameworkPropertyMetadata(Brushes.Red, FrameworkPropertyMetadataOptions.AffectsRender));

    public double LineThickness
    {
        get => (double)GetValue(LineThicknessProperty);
        set => SetValue(LineThicknessProperty, value);
    }

    public static readonly DependencyProperty LineThicknessProperty =
        DependencyProperty.Register(nameof(LineThickness), typeof(double), typeof(VerticalLineDecorator),
            new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public object HeaderContent
    {
        get => GetValue(HeaderContentProperty);
        set => SetValue(HeaderContentProperty, value);
    }

    public static readonly DependencyProperty HeaderContentProperty =
        DependencyProperty.Register(nameof(HeaderContent), typeof(object), typeof(VerticalLineDecorator),
            new FrameworkPropertyMetadata(null));

    private ContentPresenter _headerPresenter;

    /// <summary>
    /// 获取鼠标在控件内的水平坐标
    /// </summary>
    public double MouseX
    {
        get => (double)GetValue(MouseXProperty);
        private set => SetValue(MouseXPropertyKey, value);
    }

    /// <summary>
    /// 获取鼠标在控件内的水平坐标
    /// </summary>
    private static readonly DependencyPropertyKey MouseXPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(MouseX),
            typeof(double),
            typeof(VerticalLineDecorator),
            new FrameworkPropertyMetadata(-1.0, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// 获取鼠标在控件内的水平坐标
    /// </summary>
    public static readonly DependencyProperty MouseXProperty = MouseXPropertyKey.DependencyProperty;

    public VerticalLineDecorator()
    {
        _headerPresenter = new ContentPresenter
        {
            IsHitTestVisible = false // 提示层不参与交互
        };
        AddVisualChild(_headerPresenter);
        AddLogicalChild(_headerPresenter);

        MouseMove += (s, e) =>
        {
            if (_isMouseInside)
            {
                MouseX = e.GetPosition(this).X; // 更新依赖属性
                InvalidateVisual();
            }
        };
        MouseEnter += (s, e) =>
        {
            _isMouseInside = true;
            InvalidateVisual();
        };
        MouseLeave += (s, e) =>
        {
            _isMouseInside = false;
            MouseX = -1; // 离开时重置
            InvalidateVisual();
        };
    }

    protected override int VisualChildrenCount => Child == null ? 1 : 2;

    protected override Visual GetVisualChild(int index)
    {
        if (Child == null)
        {
            if (index == 0) return _headerPresenter;
        }
        else
        {
            if (index == 0) return Child;
            if (index == 1) return _headerPresenter;
        }
        throw new ArgumentOutOfRangeException();
    }

    protected override Size MeasureOverride(Size constraint)
    {
        Size childSize = new();
        if (Child != null)
        {
            Child.Measure(constraint);
            childSize = Child.DesiredSize;
        }
        _headerPresenter.Content = HeaderContent;
        _headerPresenter.Measure(constraint);
        return childSize;
    }

    protected override Size ArrangeOverride(Size arrangeSize)
    {
        Child?.Arrange(new Rect(arrangeSize));

        if (_isMouseInside && MouseX >= 0)
        {
            _headerPresenter.Arrange(new Rect(new Point(MouseX + 5, 0), _headerPresenter.DesiredSize));
        }
        else
        {
            _headerPresenter.Arrange(new Rect(new Point(-1000, -1000), _headerPresenter.DesiredSize)); // 隐藏
        }

        return arrangeSize;
    }

    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        // 绘制背景，要不然没内容不触发鼠标 hittest
        dc.DrawRectangle(Background, null, new Rect(0, 0, RenderSize.Width, RenderSize.Height));

        // 仅在鼠标在控件内时绘制竖线
        if (_isMouseInside && MouseX >= 0 && MouseX <= RenderSize.Width)
        {
            var pen = new Pen(LineBrush, LineThickness)
            {
                DashStyle = DashStyles.Dash
            };
            dc.DrawLine(pen, new Point(MouseX, 0), new Point(MouseX, RenderSize.Height));
        }
    }

    public static PointValueDisplayConverter PointValueDisplay { get; } = new();

    public class PointValueDisplayConverter : IMultiValueConverter
    {
        /// <summary>
        /// 计算指定位置对应的标尺值。
        /// </summary>
        /// <param name="values">包含三个double值：位置、忽略表头长度、缩放比例。</param>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is [double v, double sub, double scale])
            {
                // 标尺值 =（Left - head_len）/ scale
                return Math.Max(0, (v - sub) / scale);
            }
            throw new ArgumentOutOfRangeException("必须是三个double值");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}