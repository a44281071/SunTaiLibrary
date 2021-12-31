using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SunTaiLibrary.Controls
{
    public class ResizeThumb : Thumb
    {
        private FrameworkElement parent;

        public ResizeThumb()
        {
            DragDelta += ResizeThumb_DragDelta;

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

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (TargetElement is FrameworkElement ele)
            {
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