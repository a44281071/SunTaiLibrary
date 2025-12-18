using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SunTaiLibrary.Controls;

public delegate void VItemDraggedEventHandler(object sender, VItemDraggedEventArgs e);

public delegate void VItemWidthDraggedEventHandler(object sender, VItemWidthDraggedEventArgs e);

/// <summary>
/// 项水平拖动事件参数
/// </summary>
public class VItemDraggedEventArgs : RoutedEventArgs
{
    public VItemDraggedEventArgs(RoutedEvent routedEvent
        , double oldPosition, double newPosition)
        : base(routedEvent)
    {
        OldPosition = oldPosition;
        NewPosition = newPosition;
        Delta = newPosition - oldPosition;
    }

    public double OldPosition { get; set; }
    public double NewPosition { get; set; }

    /// <summary>
    /// 改变大小
    /// </summary>
    public double Delta { get; set; }
}

/// <summary>
/// 项宽度拖动事件参数
/// </summary>
public class VItemWidthDraggedEventArgs : RoutedEventArgs
{
    public VItemWidthDraggedEventArgs(RoutedEvent routedEvent
        , VItemWidthDraggedSide draggedSide
        , double oldWidth, double newWidth)
        : base(routedEvent)
    {
        DraggedSide = draggedSide;
        OldWidth = oldWidth;
        NewWidth = newWidth;
        Delta = newWidth - oldWidth;
    }

    public VItemWidthDraggedSide DraggedSide { get; }
    public double OldWidth { get; set; }
    public double NewWidth { get; set; }

    /// <summary>
    /// 改变大小
    /// </summary>
    public double Delta { get; set; }
}

public enum VItemWidthDraggedSide
{
    /// <summary>
    /// 通过左侧调整大小，逻辑相对更复杂
    /// </summary>
    Left,

    /// <summary>
    /// 通过右侧调整大小
    /// </summary>
    Right
}