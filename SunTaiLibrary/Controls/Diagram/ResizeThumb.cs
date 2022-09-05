using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace SunTaiLibrary.Controls
{
    public class ResizeThumb : Thumb
    {
        private FrameworkElement parent;

        public double AutoAlignScope
        {
            get { return (double)GetValue(AutoAlignScopeProperty); }
            set { SetValue(AutoAlignScopeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoAlignScope.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoAlignScopeProperty =
            DependencyProperty.Register("AutoAlignScope", typeof(double), typeof(ResizeThumb), new PropertyMetadata(10d));



        public ResizeThumb()
        {
            DragDelta += ResizeThumb_DragDelta;

            PreviewMouseLeftButtonUp += ResizeThumb_MouseLeftButtonUp;
            Loaded += (s, _) =>
            {
                if (TargetElement is FrameworkElement ele)
                {
                    parent = VisualTreeHelper.GetParent(ele) as FrameworkElement;
                    if (DisplayRatio != -1)
                    {
                        ele.Height = (double)ele.ActualWidth / DisplayRatio;
                    }
                }
            };
        }

        private void ResizeThumb_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var canvas = parent as Canvas;
            if (canvas is null) return;
            canvas.ClearAlignLine();
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (TargetElement is FrameworkElement ele)
            {
                //获取可对齐的点
                var canvas = parent as Canvas;
                if (canvas is null) return;
                canvas.ClearAlignLine();
                List<Point> points = new List<Point>();
                foreach (FrameworkElement ctrl in canvas.Children)
                {
                    if (ctrl != TargetElement)
                    {
                        //左上角的点
                        Point itemtl = new Point(Canvas.GetLeft(ctrl), Canvas.GetTop(ctrl));
                        //左下角的点
                        Point itembl = new Point(Canvas.GetLeft(ctrl), Canvas.GetTop(ctrl) + ctrl.ActualHeight);
                        //右上角的点
                        Point itemtr = new Point(Canvas.GetLeft(ctrl) + ctrl.ActualWidth, Canvas.GetTop(ctrl));
                        //右下角的点
                        Point itembr = new Point(Canvas.GetLeft(ctrl) + ctrl.ActualWidth, Canvas.GetTop(ctrl) + ctrl.ActualHeight);

                        points.AddRange(new Point[] { itemtl, itemtr, itembl, itembr });
                    }
                }

                double deltaVertical = 0;
                double deltaHorizontal = 0;
                double height, width;
                Vector offset = VisualTreeHelper.GetOffset(ele);

                if (DisplayRatio > Double.Epsilon)
                {
                    switch (DragDirection)
                    {
                        case DragDirection.TopLeft:
                            if (Math.Abs(e.HorizontalChange) <= Math.Abs(e.VerticalChange))
                            {
                                deltaHorizontal = Math.Min(e.HorizontalChange, ele.ActualWidth - ele.MinWidth);
                                width = ele.ActualWidth - deltaHorizontal;
                                height = (double)width / DisplayRatio;

                                if (offset.X + deltaHorizontal <= 0
                                    || offset.Y + ele.ActualHeight - height <= 0)
                                    break;
                                Canvas.SetLeft(ele, offset.X + deltaHorizontal);
                                Canvas.SetTop(ele, offset.Y + ele.ActualHeight - height);
                                ele.Width = width;
                                ele.Height = height;
                            }
                            else
                            {
                                deltaVertical = Math.Min(e.VerticalChange, ele.ActualHeight - ele.MinHeight);

                                height = ele.ActualHeight - deltaVertical;
                                width = height * DisplayRatio;

                                if (offset.Y + deltaVertical <= 0
                                    || ele.ActualWidth - width + offset.X <= 0)
                                    break;
                                Canvas.SetTop(ele, offset.Y + deltaVertical);
                                Canvas.SetLeft(ele, offset.X + ele.ActualWidth - width);
                                ele.Height = height;
                                ele.Width = width;
                            }
                            break;

                        case DragDirection.TopCenter:
                            deltaVertical = Math.Min(e.VerticalChange, ele.ActualHeight - ele.MinHeight);

                            height = ele.ActualHeight - deltaVertical;
                            width = height * DisplayRatio;

                            if (offset.Y + deltaVertical <= 0
                                || ele.ActualWidth - width + offset.X <= 0)
                                break;
                            Canvas.SetTop(ele, offset.Y + deltaVertical);
                            Canvas.SetLeft(ele, offset.X + ele.ActualWidth - width);
                            ele.Height = height;
                            ele.Width = width;
                            break;

                        case DragDirection.TopRight:
                            if (Math.Abs(e.HorizontalChange) <= Math.Abs(e.VerticalChange))
                            {
                                deltaHorizontal = Math.Min(-e.HorizontalChange, ele.ActualWidth - ele.MinWidth);
                                width = ele.ActualWidth - deltaHorizontal;
                                height = (double)width / DisplayRatio;

                                if (offset.X + deltaHorizontal <= 0
                                    || width >= parent.ActualWidth - offset.X)
                                    break;
                                Canvas.SetTop(ele, offset.Y + ele.ActualHeight - height);
                                ele.Width = width;
                                ele.Height = height;
                            }
                            else
                            {
                                deltaVertical = Math.Min(e.VerticalChange, ele.ActualHeight - ele.MinHeight);

                                height = ele.ActualHeight - deltaVertical;
                                width = height * DisplayRatio;

                                if (offset.Y + deltaVertical <= 0
                                    || width >= parent.ActualWidth - offset.X)
                                    break;
                                Canvas.SetTop(ele, offset.Y + deltaVertical);
                                ele.Height = height;
                                ele.Width = width;
                            }
                            break;

                        case DragDirection.MiddleLeft:
                            deltaHorizontal = Math.Min(e.HorizontalChange, ele.ActualWidth - ele.MinWidth);
                            width = ele.ActualWidth - deltaHorizontal;
                            height = (double)width / DisplayRatio;

                            if (offset.X + deltaHorizontal <= 0
                                || height >= parent.ActualHeight - offset.Y)
                                break;
                            Canvas.SetLeft(ele, offset.X + deltaHorizontal);
                            ele.Width = width;
                            ele.Height = height;
                            break;

                        case DragDirection.MiddleCenter:
                            break;

                        case DragDirection.MiddleRight:
                            deltaHorizontal = Math.Min(-e.HorizontalChange, ele.ActualWidth - ele.MinWidth);
                            width = ele.ActualWidth - deltaHorizontal;
                            height = (double)width / DisplayRatio;

                            if (width >= parent.ActualWidth - offset.X
                                || height >= parent.ActualHeight - offset.Y)
                                break;
                            ele.Width = width;
                            ele.Height = height;
                            break;

                        case DragDirection.BottomLeft:
                            if (Math.Abs(e.HorizontalChange) <= Math.Abs(e.VerticalChange))
                            {
                                deltaHorizontal = Math.Min(e.HorizontalChange, ele.ActualWidth - ele.MinWidth);
                                width = ele.ActualWidth - deltaHorizontal;
                                height = (double)width / DisplayRatio;

                                if (offset.X + deltaHorizontal <= 0
                                    || height >= parent.ActualHeight - offset.Y)
                                    break;
                                Canvas.SetLeft(ele, offset.X + deltaHorizontal);
                                ele.Width = width;
                                ele.Height = height;
                            }
                            else
                            {
                                deltaVertical = Math.Min(-e.VerticalChange, ele.ActualHeight - ele.MinHeight);

                                height = ele.ActualHeight - deltaVertical;
                                width = height * DisplayRatio;

                                if (offset.Y + deltaVertical <= 0
                                    || height >= parent.ActualHeight - offset.Y)
                                    break;
                                Canvas.SetLeft(ele, offset.X + ele.ActualWidth - width);
                                ele.Height = height;
                                ele.Width = width;
                            }
                            break;

                        case DragDirection.BottomCenter:
                            deltaVertical = Math.Min(-e.VerticalChange, ele.ActualHeight - ele.MinHeight);

                            height = ele.ActualHeight - deltaVertical;
                            width = height * DisplayRatio;

                            if (width >= parent.ActualWidth - offset.X
                                || height >= parent.ActualHeight - offset.Y)
                                break;
                            ele.Height = height;
                            ele.Width = width;
                            break;

                        case DragDirection.BottomRight:
                            if (Math.Abs(e.HorizontalChange) <= Math.Abs(e.VerticalChange))
                            {
                                deltaHorizontal = Math.Min(-e.HorizontalChange, ele.ActualWidth - ele.MinWidth);
                                width = ele.ActualWidth - deltaHorizontal;
                                height = (double)width / DisplayRatio;

                                if (height >= parent.ActualHeight - offset.Y
                                    || width >= parent.ActualWidth - offset.X)
                                    break;
                                ele.Width = width;
                                ele.Height = height;
                            }
                            else
                            {
                                deltaVertical = Math.Min(-e.VerticalChange, ele.ActualHeight - ele.MinHeight);

                                height = ele.ActualHeight - deltaVertical;
                                width = height * DisplayRatio;

                                if (height >= parent.ActualHeight - offset.Y
                                    || width >= parent.ActualWidth - offset.X)
                                    break;
                                ele.Height = height;
                                ele.Width = width;
                            }
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    switch (VerticalAlignment)
                    {
                        case VerticalAlignment.Bottom:
                            deltaVertical = Math.Min(-e.VerticalChange,
                                ele.ActualHeight - ele.MinHeight);
                            if (ele.ActualHeight - deltaVertical <= parent.ActualHeight - offset.Y)
                            {
                                ele.Height = ele.ActualHeight - deltaVertical;
                            }
                            break;

                        case VerticalAlignment.Top:
                            deltaVertical = Math.Min(e.VerticalChange,
                                ele.ActualHeight - ele.MinHeight);
                            if (offset.Y + deltaVertical > 0)
                            {
                                Canvas.SetTop(ele, Canvas.GetTop(ele) + deltaVertical);
                                ele.Height = ele.ActualHeight - deltaVertical;
                            }
                            break;

                        default:
                            break;
                    }

                    switch (HorizontalAlignment)
                    {
                        case HorizontalAlignment.Left:
                            deltaHorizontal = Math.Min(e.HorizontalChange,
                                ele.ActualWidth - ele.MinWidth);
                            if (offset.X + deltaHorizontal > 0)
                            {
                                Canvas.SetLeft(ele, Canvas.GetLeft(ele) + deltaHorizontal);
                                ele.Width = ele.ActualWidth - deltaHorizontal;
                            }

                            break;

                        case HorizontalAlignment.Right:
                            deltaHorizontal = Math.Min(-e.HorizontalChange,
                                ele.ActualWidth - ele.MinWidth);
                            if (ele.ActualWidth - deltaHorizontal < parent.ActualWidth - offset.X)
                            {
                                ele.Width = ele.ActualWidth - deltaHorizontal;
                            }
                            break;

                        default:
                            break;
                    }
                }

                #region 拖拽大小自动对齐并显示对齐标签
                //计算是否显示对齐线
                bool needSetLAlign = false;
                bool needSetTAlign = false;
                //左
                var lAlign = points.FirstOrDefault(x => Math.Abs(x.X - Canvas.GetLeft(TargetElement)) <= AutoAlignScope);
                if (lAlign != default)
                {
                    double px = lAlign.X - Canvas.GetLeft(TargetElement);
                    if (px != 0)
                    {
                        var layer = AdornerLayer.GetAdornerLayer(canvas);
                        layer.Add(new SelectionAlignLine(canvas, lAlign, new Point(lAlign.X, Canvas.GetTop(TargetElement) + TargetElement.Height)));
                        if (px > 0)
                        {
                            TargetElement.Width -= px;
                        }
                        else if (px < 0)
                        {
                            TargetElement.Width += Math.Abs(px);
                        }
                        needSetLAlign = true;
                        Canvas.SetLeft(TargetElement, lAlign.X);
                    }
                }

                //顶
                var tAlign = points.FirstOrDefault(x => Math.Abs(x.Y - Canvas.GetTop(TargetElement)) <= AutoAlignScope);
                if (tAlign != default)
                {
                    double px = tAlign.Y - Canvas.GetTop(TargetElement);
                    if (px != 0) 
                    {
                        if (px > 0)
                        {
                            TargetElement.Height -= px;
                        }
                        else if (px < 0)
                        {
                            TargetElement.Height += Math.Abs(px);
                        }
                        var layer = AdornerLayer.GetAdornerLayer(canvas);
                        layer.Add(new SelectionAlignLine(canvas, tAlign, new Point(Canvas.GetLeft(TargetElement) + TargetElement.Width, tAlign.Y)));
                        Canvas.SetTop(TargetElement, tAlign.Y);
                        needSetTAlign = true;
                    }
                }

                //右
                var rAlign = points.FirstOrDefault(x => (x.X -(Canvas.GetLeft(TargetElement) + TargetElement.ActualWidth) <= AutoAlignScope && x.X - (Canvas.GetLeft(TargetElement) + TargetElement.ActualWidth) > 0)
                || ((Canvas.GetLeft(TargetElement) + TargetElement.ActualWidth)-x.X<= AutoAlignScope && (Canvas.GetLeft(TargetElement) + TargetElement.ActualWidth) - x.X>0));
                if (rAlign != default)
                {
                    //不是向右拉不执行对齐
                    if((DragDirection ^ DragDirection.BottomRight) != 0|| (DragDirection ^ DragDirection.MiddleRight) != 0|| (DragDirection ^ DragDirection.TopRight) != 0)
                    {
                        if (!needSetLAlign)
                        {
                            TargetElement.Width = rAlign.X - Canvas.GetLeft(TargetElement);
                            var layer = AdornerLayer.GetAdornerLayer(canvas);
                            layer.Add(new SelectionAlignLine(canvas, rAlign, new Point(rAlign.X, Canvas.GetTop(TargetElement))));
                        }
                    }
                }

                //底
                var bAlign = points.FirstOrDefault(x => x.Y - (Canvas.GetTop(TargetElement) + TargetElement.ActualHeight) <= AutoAlignScope && x.Y - (Canvas.GetTop(TargetElement) + TargetElement.ActualHeight) > 0
                ||((Canvas.GetTop(TargetElement) + TargetElement.ActualHeight)-x.Y<= AutoAlignScope && (Canvas.GetTop(TargetElement) + TargetElement.ActualHeight) - x.Y > 0));
                if (bAlign != default)
                {
                    //不是向底拉不执行对齐
                    if ((DragDirection ^ DragDirection.BottomLeft) != 0 || (DragDirection ^ DragDirection.BottomRight) != 0 || (DragDirection ^ DragDirection.BottomCenter) != 0) 
                    {
                        if (!needSetTAlign)
                        {
                            TargetElement.Height = bAlign.Y - Canvas.GetTop(TargetElement);
                            var layer = AdornerLayer.GetAdornerLayer(canvas);
                            layer.Add(new SelectionAlignLine(canvas, bAlign, new Point(Canvas.GetLeft(TargetElement), bAlign.Y)));
                        }
                    }
                }

                #endregion

                points.Clear();
            }
        }

        #region Property TargetElement

        public FrameworkElement TargetElement
        {
            get { return (FrameworkElement)GetValue(TargetElementProperty); }
            set { SetValue(TargetElementProperty, value); }
        }

        public static readonly DependencyProperty TargetElementProperty =
            DependencyProperty.Register("TargetElement"
              , typeof(FrameworkElement)
              , typeof(ResizeThumb)
              , new PropertyMetadata(null));

        #endregion Property TargetElement

        #region Property DisplayRatio

        public double DisplayRatio
        {
            get { return (double)GetValue(DisplayRatioProperty); }
            set { SetValue(DisplayRatioProperty, value); }
        }

        public static readonly DependencyProperty DisplayRatioProperty =
            DependencyProperty.Register("DisplayRatio"
              , typeof(double)
              , typeof(ResizeThumb)
              , new PropertyMetadata(1d, new PropertyChangedCallback(OnDisplayRatioPropertyChanged)));

        private static void OnDisplayRatioPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ResizeThumb rt)
            {
                if (rt.TargetElement is FrameworkElement ele && rt.DisplayRatio > Double.Epsilon)
                {
                    if (rt.parent != null && ele.ActualHeight >= rt.parent.ActualHeight)
                        return;
                    ele.Height = (double)ele.ActualWidth / rt.DisplayRatio;
                }
            }
        }

        public DragDirection DragDirection { get; set; }

        #endregion Property DisplayRatio
    }
}