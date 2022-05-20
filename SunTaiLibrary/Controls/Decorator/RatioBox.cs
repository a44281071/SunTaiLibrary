using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SunTaiLibrary.Controls
{
    /// <summary>
    /// 支持让子级以指定的比例布局大小的容器。
    /// </summary>
    public class RatioBox : Decorator
    {
        /// <summary>
        /// 为子级指定一个布局比例。
        /// </summary>
        public double Ratio
        {
            get { return (double)GetValue(RatioProperty); }
            set { SetValue(RatioProperty, value); }
        }

        /// <summary>
        /// 为子级指定一个布局比例。
        /// </summary>
        public static readonly DependencyProperty RatioProperty =
            DependencyProperty.Register("Ratio", typeof(double), typeof(RatioBox)
                , new FrameworkPropertyMetadata(Double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        protected override Size MeasureOverride(Size constraint)
        {
            double mratio = Ratio;
            if (Double.IsNaN(mratio))
            {
                return base.MeasureOverride(constraint);
            }
            else
            {
                UIElement child = Child;
                if (child != null)
                {
                    double h = constraint.Height;
                    double w = h * mratio;
                    if (w > constraint.Width)
                    {
                        w = constraint.Width;
                        h = w / mratio;
                    }
                    child.Measure(new Size(w, h));
                }
                return new Size();
            }
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            double mratio = Ratio;
            if (Double.IsNaN(mratio))
            {
                return base.ArrangeOverride(arrangeSize);
            }
            else
            {
                UIElement child = Child;

                if (child != null)
                {
                    double h = arrangeSize.Height;
                    double w = h * mratio;
                    if (w > arrangeSize.Width)
                    {
                        w = arrangeSize.Width;
                        h = w / mratio;
                    }
                    double x = (arrangeSize.Width - w) / 2;
                    double y = (arrangeSize.Height - h) / 2;
                    var cb = new Rect(x, y, w, h);
                    child.Arrange(cb);
                }

                return arrangeSize;
            }
        }
    }
}