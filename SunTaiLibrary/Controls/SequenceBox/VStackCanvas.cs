using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SunTaiLibrary.Controls;

/// <summary>
/// 在垂直方向上排列子元素，在水平方向绝对定位子元素的画布。
/// 类似基于时间轴的元素编辑器。
/// </summary>
public class VStackCanvas : Panel
{
    private const double Line_Thick = 0.5;
    public const double Min_Item_Width = 5.0;

    #region 附加属性

    public static readonly DependencyProperty UnitXProperty =
        DependencyProperty.RegisterAttached("UnitX",
            typeof(double), typeof(VStackCanvas),
            new FrameworkPropertyMetadata(0.0
                , FrameworkPropertyMetadataOptions.AffectsParentArrange | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                , null
                , CoerceUnitXValue));

    public static void SetUnitX(UIElement element, double value)
    {
        element.SetValue(UnitXProperty, value);
    }

    public static double GetUnitX(UIElement element)
    {
        return (double)element.GetValue(UnitXProperty);
    }

    /// <summary>
    /// 确保 X 不小于父级设定的 MinimumX
    /// </summary>
    /// <returns>真实设置的值</returns>
    public static double SetUnitXWithMinimum(UIElement child, double valueUnits, bool enableSnapToUnit = false)
    {
        if (enableSnapToUnit)
            valueUnits = Math.Round(valueUnits);   // 仅对齐到单位刻度

        valueUnits = Math.Max(0, valueUnits);
        child.SetValue(UnitXProperty, valueUnits);
        return valueUnits;
    }

    /// <summary>
    /// 限定 不为负数
    /// </summary>
    private static object CoerceUnitXValue(DependencyObject d, object baseValue)
    {
        if (baseValue is double v)
        {
            if (Double.IsNaN(v) || Double.IsInfinity(v))
                return 0.0;
            return Math.Max(0.0, v);
        }
        else
        {
            return Double.NaN;
        }
    }

    public static readonly DependencyProperty UnitWidthProperty =
        DependencyProperty.RegisterAttached("UnitWidth", typeof(double), typeof(VStackCanvas)
            , new FrameworkPropertyMetadata(Double.NaN
                , FrameworkPropertyMetadataOptions.AffectsParentArrange
                    | FrameworkPropertyMetadataOptions.AffectsParentMeasure
                    | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                , null
                , CoerceUnitWidthValue));

    public static double GetUnitWidth(DependencyObject obj)
    {
        return (double)obj.GetValue(UnitWidthProperty);
    }

    public static void SetUnitWidth(DependencyObject obj, double value)
    {
        obj.SetValue(UnitWidthProperty, value);
    }

    public static void SetUnitWidthWithMinimum(UIElement child, double valueUnits, bool enableSnapToUnit = false)
    {
        if (enableSnapToUnit)
            valueUnits = Math.Round(valueUnits);   // 仅对齐到单位刻度

        valueUnits = Math.Max(1, valueUnits);
        child.SetValue(UnitWidthProperty, valueUnits);
    }

    private static object CoerceUnitWidthValue(DependencyObject d, object baseValue)
    {
        if (baseValue is double v)
        {
            if (Double.IsInfinity(v))
                return 0.0;
            else if (v < 0.0)
                return 0.0;
            return v;
        }
        else
        {
            return Double.NaN;
        }
    }

    #endregion 附加属性

    #region 依赖属性

    public double ScaleX
    {
        get { return (double)GetValue(ScaleXProperty); }
        set { SetValue(ScaleXProperty, value); }
    }

    public static readonly DependencyProperty ScaleXProperty =
        DependencyProperty.Register("ScaleX", typeof(double), typeof(VStackCanvas)
            , new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

    public Brush LineBrush
    {
        get => (Brush)GetValue(LineBrushProperty);
        set => SetValue(LineBrushProperty, value);
    }

    public static readonly DependencyProperty LineBrushProperty =
        DependencyProperty.Register(nameof(LineBrush), typeof(Brush), typeof(VStackCanvas),
            new FrameworkPropertyMetadata(Brushes.Gray,
                FrameworkPropertyMetadataOptions.AffectsRender));

    #endregion 依赖属性

    #region 事件

    public static readonly RoutedEvent VItemDraggedEvent = EventManager.RegisterRoutedEvent(
        nameof(VItemDragged),               // 事件名称
        RoutingStrategy.Bubble,             // 路由策略：冒泡
        typeof(VItemDraggedEventHandler),   // 事件处理程序类型
        typeof(VStackCanvas)                // 拥有者类型
    );

    public event VItemDraggedEventHandler VItemDragged
    {
        add { AddHandler(VItemDraggedEvent, value); }
        remove { RemoveHandler(VItemDraggedEvent, value); }
    }

    protected virtual void OnVItemDragged(object item, double oldPosition, double newPosition)
    {
        var args = new VItemDraggedEventArgs(VItemDraggedEvent
            , oldPosition, newPosition
        );
        RaiseEvent(args);
    }

    #endregion 事件

    #region 测绘逻辑

    protected override Size MeasureOverride(Size availableSize)
    {
        double totalHeight = 0;
        double maxWidth = 0;
        double scale = ScaleX;

        foreach (UIElement child in InternalChildren)
        {
            if (child == null) continue;

            double x = GetUnitX(child) * scale;
            double w = GetUnitWidth(child) * scale;
            double dw = Double.IsNaN(w) ? Double.PositiveInfinity : w;

            child.Measure(new Size(dw, Double.PositiveInfinity));
            Size desired = child.DesiredSize;

            totalHeight += desired.Height;
            maxWidth = Math.Max(maxWidth, desired.Width + x);
        }

        return new Size(maxWidth, totalHeight);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        double scale = ScaleX;
        double y = 0;

        foreach (UIElement child in InternalChildren)
        {
            if (child == null) continue;

            double x = GetUnitX(child) * scale;
            double w = GetUnitWidth(child) * scale;
            Size desired = child.DesiredSize;
            double fixW = Double.IsNaN(w) ? desired.Width : w;

            //child.Arrange(new Rect(new Point(x, y), desired));
            child.Arrange(new Rect(x, y, fixW, desired.Height));

            y += desired.Height;
        }

        return finalSize;
    }

    #endregion 测绘逻辑

    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        if (InternalChildren.Count == 0) return;

        var pen = new Pen(LineBrush, Line_Thick) { DashStyle = DashStyles.Solid };

        double y = 0;
        // 记录每一行底边
        var bottoms = new double[InternalChildren.Count];
        for (int i = 0; i < InternalChildren.Count; i++)
        {
            y += InternalChildren[i].DesiredSize.Height;
            bottoms[i] = y;
        }

        // 画横线
        foreach (double b in bottoms)
            dc.DrawLine(pen, new Point(0, b), new Point(RenderSize.Width, b));
    }

    #region 子级拖动

    private CavasHDragWorker? hDragWorker = null;

    private void Canvas_MouseLeftButtonDown(MouseButtonEventArgs e)
    {
        // thumb可能是额外的自定义拖动能力，排除。
        //if (e.Source is Thumb) return;
        if (VisualTreeHelperEx.FindAncestor<Thumb>(e.OriginalSource as DependencyObject) is { }) return;

        // 找到点击的子元素，准备水平拖动
        else if (GetDirectChild(e.OriginalSource as DependencyObject)
                is UIElement child1)
        {
            hDragWorker = new CavasHDragWorker(child1, ScaleX, e.GetPosition(this), GetUnitX(child1));
        }
    }

    private void Canvas_MouseMove(MouseEventArgs e)
    {
        if (hDragWorker != null && e.LeftButton == MouseButtonState.Pressed)
        {
            Point currentMouse = e.GetPosition(this);

            double ux = hDragWorker.UpdateDragMove(currentMouse);
            //SetXOffset(hDragWorker.DraggingChild, offset);
            SetUnitXWithMinimum(hDragWorker.DraggingChild, ux, true);
        }
    }

    private void Canvas_MouseLeftButtonUp(MouseButtonEventArgs e)
    {
        if (hDragWorker is not null)
        {
            if (hDragWorker.IsDragging)
            {
                // 完成拖动，触发事件
                OnVItemDragged(hDragWorker.DraggingChild
                    , hDragWorker.StartUX, GetUnitX(hDragWorker.DraggingChild));
            }
            hDragWorker.Dispose();
            hDragWorker = null;
        }
    }

    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseLeftButtonDown(e);
        Canvas_MouseLeftButtonDown(e);
    }

    protected override void OnPreviewMouseMove(MouseEventArgs e)
    {
        base.OnPreviewMouseMove(e);
        Canvas_MouseMove(e);
    }

    protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseLeftButtonUp(e);
        Canvas_MouseLeftButtonUp(e);
    }

    #endregion 子级拖动

    /// <summary>
    /// 获取能直接作为 Canvas 子元素的元素
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private UIElement? GetDirectChild(DependencyObject? element)
    {
        DependencyObject? current = element;
        while (current != null)
        {
            DependencyObject? parent = VisualTreeHelper.GetParent(current);
            if (parent == this)
            {
                return current as UIElement;
            }
            current = parent;
        }
        return null;
    }
}

internal class CavasHDragWorker : IDisposable
{
    public CavasHDragWorker(UIElement child, double scaleX
        , Point startMouse, double startUX)
    {
        DraggingChild = child;
        this.scaleX = scaleX;
        this.startMouse = startMouse;
        this.StartUX = Double.IsNaN(startUX) ? 0 : startUX;
    }

    private readonly double scaleX;
    public Point startMouse;

    /// <summary>
    /// 拖动开始时的单位X位置
    /// </summary>
    public double StartUX { get; }

    public UIElement DraggingChild { get; }

    /// <summary>
    /// 拖动偏移太小，视为单击操作，忽略位移。
    /// 一旦超出1像素，算开始拖动。
    /// </summary>
    public bool IsDragging { get; private set; }

    /// <summary>
    /// 一旦定位拖动操作，则捕获鼠标。
    /// </summary>
    /// <param name="currentMouse">鼠标位置</param>
    /// <returns>基于拖动开始点的位移。</returns>
    internal double UpdateDragMove(Point currentMouse)
    {
        double dx = currentMouse.X - startMouse.X;
        if (false == IsDragging
            && Math.Abs(dx) > 1)
        {
            // 拖动偏移太小，视为单击操作，忽略位移。
            // 一旦超出1像素，算开始拖动，捕获鼠标。
            IsDragging = true;
            DraggingChild.CaptureMouse();
        }

        return StartUX + dx / scaleX;    // unit x
    }

    internal void FinishDrag()
    {
        if (IsDragging)
        {
            DraggingChild.ReleaseMouseCapture();
        }
        IsDragging = false;
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // 释放托管状态(托管对象)
                FinishDrag();
            }

            // 释放未托管资源
            disposedValue = true;
        }
    }

    /// <summary>
    /// 释放可能捕获的鼠标
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion IDisposable
}