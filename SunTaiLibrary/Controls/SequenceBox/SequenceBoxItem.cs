using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SunTaiLibrary.Controls;

/// <summary>
/// 在水平方向定位排列元素
/// </summary>
[TemplatePart(Name = PART_LeftSizeGripper, Type = typeof(Thumb))]
[TemplatePart(Name = PART_RightSizeGripper, Type = typeof(Thumb))]
public class SequenceBoxItem : ListBoxItem
{
    static SequenceBoxItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SequenceBoxItem), new FrameworkPropertyMetadata(typeof(SequenceBoxItem)));
    }

    public SequenceBoxItem()
    {
        // 禁用自动滚动到可见区域，和水平拖动相关功能冲突。
        // 理论上只禁用水平滚动跳转，但是没有相关事件，只能全部禁用。
        RequestBringIntoView += static (ss, ee) => ee.Handled = true;
    }

    private const string PART_LeftSizeGripper = "PART_LeftSizeGripper";
    private const string PART_RightSizeGripper = "PART_RightSizeGripper";

    private LeftSizeGripperWorker? lsgWorker = null;

    #region 事件

    public static readonly RoutedEvent VItemWidthDraggedEvent = EventManager.RegisterRoutedEvent(
        nameof(VItemWidthDragged),               // 事件名称
        RoutingStrategy.Bubble,                  // 路由策略：冒泡
        typeof(VItemWidthDraggedEventHandler),   // 事件处理程序类型
        typeof(SequenceBoxItem)                  // 拥有者类型
    );

    public event VItemWidthDraggedEventHandler VItemWidthDragged
    {
        add { AddHandler(VItemWidthDraggedEvent, value); }
        remove { RemoveHandler(VItemWidthDraggedEvent, value); }
    }

    protected virtual void OnVItemWidthDragged(object item, VItemWidthDraggedSide draggedSide, double oldPosition, double newPosition)
    {
        var args = new VItemWidthDraggedEventArgs(VItemWidthDraggedEvent
            , draggedSide, oldPosition, newPosition
        );
        RaiseEvent(args);
    }

    #endregion 事件

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild(PART_LeftSizeGripper)
            is Thumb leftGripper)
        {
            leftGripper.DragStarted += (ss, ee) =>
            {
                lsgWorker = new LeftSizeGripperWorker(this);
            };
            leftGripper.DragDelta += (s, e) =>
            {
                lsgWorker?.OnDragDelta(e.HorizontalChange);
            };
            leftGripper.DragCompleted += (ss, ee) =>
            {
                double newUW = VStackCanvas.GetUnitWidth(this);
                OnVItemWidthDragged(this, VItemWidthDraggedSide.Left, lsgWorker!.StartUWidth, newUW);
                lsgWorker = null;
            };
        }
        if (GetTemplateChild(PART_RightSizeGripper)
            is Thumb rightGripper)
        {
            double oldUW = 0;
            rightGripper.DragStarted += (ss, ee) =>
            {
                oldUW = VStackCanvas.GetUnitWidth(this);
            };
            rightGripper.DragDelta += (s, e) =>
            {
                VStackCanvas? panel = VisualTreeHelper.GetParent(this) as VStackCanvas;
                double w = Math.Max(ActualWidth + e.HorizontalChange, VStackCanvas.Min_Item_Width);
                double uw = w / panel?.ScaleX ?? w;
                VStackCanvas.SetUnitWidthWithMinimum(this, uw, true);
            };
            rightGripper.DragCompleted += (ss, ee) =>
            {
                double newUW = VStackCanvas.GetUnitWidth(this);
                OnVItemWidthDragged(this, VItemWidthDraggedSide.Right, oldUW, newUW);
            };
        }
    }
}

/// <summary>
/// 左侧大小调整柄
/// </summary>
internal class LeftSizeGripperWorker
{
    public LeftSizeGripperWorker(SequenceBoxItem dragging)
    {
        Dragging = dragging;
        if (VisualTreeHelper.GetParent(Dragging)
            is VStackCanvas vcp)
        {
            parentPanel = vcp;

            scaleX = vcp.ScaleX;
            startUX = VStackCanvas.GetUnitX(dragging);
            StartUWidth = VStackCanvas.GetUnitWidth(dragging);

            if (enableSnapToUnit)
            {
                minWidth = vcp.ScaleX;  // 启用对齐后，不能小于 1 个单位宽度，防止对齐计算贴近0.
            }
        }

        startWidth = Double.IsNaN(dragging.ActualWidth) ? 0 : dragging.ActualWidth;
        StartUWidth = Double.IsNaN(StartUWidth) ? startWidth / scaleX : StartUWidth;

        MaxWidth = startUX * scaleX + startWidth;
    }

    private readonly double minWidth = VStackCanvas.Min_Item_Width;
    private readonly bool enableSnapToUnit = true;    // 对齐到刻度

    private readonly VStackCanvas? parentPanel = null;
    private readonly double scaleX = 1;
    private readonly double startUX = 0;
    private readonly double startWidth;
    public double StartUWidth { get; }

    public SequenceBoxItem Dragging { get; }

    public double MaxWidth { get; }

    internal void OnDragDelta(double xChange)
    {
        if (parentPanel is not null)
        {
            // 用 parentPanel 计算鼠标位置，更准确，不会左侧拖动时跳动
            xChange = Mouse.GetPosition(parentPanel).X - startUX * scaleX;
        }
        double uxChange = xChange / scaleX;

        // ---------- 1. 先算“宽度方向”允许的最大缩量 ----------
        double maxShrink = startWidth - minWidth;   // 再缩就低于 minWidth 了
        uxChange = Math.Min(uxChange, maxShrink / scaleX);   // 不能让宽度爆表

        // ---------- 2. 再算“位置方向”允许的最大左移 ----------
        double newUX = startUX + uxChange;
        if (newUX < 0)                               // 左侧越界
        {
            uxChange = -startUX;             // 钉在 0
            newUX = 0;
        }

        // ---------- 3. 正式赋值 ----------
        if (VisualTreeHelper.GetParent(Dragging) is VStackCanvas)
            VStackCanvas.SetUnitXWithMinimum(Dragging, newUX, enableSnapToUnit);

        double uWidth = StartUWidth - uxChange; // 到这里一定 ≥ minWidth
        VStackCanvas.SetUnitWidthWithMinimum(Dragging, uWidth, enableSnapToUnit);
    }
}