using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SunTaiLibrary.Controls
{
    /// <Remarks>
    ///     As a side effect ClippingBorder will surpress any databinding or animation of
    ///         its childs UIElement.Clip property until the child is removed from ClippingBorder.
    /// Thanks [Javier Suárez] for RoundRectangleGeometry.
    /// </Remarks>
    public class ClippingBorder : Border
    {
        /// <summary>
        /// create a radius reactangle geometry for clip.
        /// </summary>
        /// <see href="https://github.com/xamarin/Xamarin.Forms/pull/11851"/>
        /// <param name="rect">contennt bound</param>
        /// <returns>a geometry for clip.</returns>
        private Geometry CreateRoundRectangleGeometry(Rect rect)
        {
            var roundedRectGeometry = new GeometryGroup
            {
                FillRule = FillRule.Nonzero
            };

            if (CornerRadius.TopLeft > 0)
                roundedRectGeometry.Children.Add(
                    new EllipseGeometry(new Point(rect.Location.X + CornerRadius.TopLeft, rect.Location.Y + CornerRadius.TopLeft), rect.Location.Y + CornerRadius.TopLeft, rect.Location.Y + CornerRadius.TopLeft));

            if (CornerRadius.TopRight > 0)
                roundedRectGeometry.Children.Add(
                    new EllipseGeometry(new Point(rect.Location.X + rect.Width - CornerRadius.TopRight, rect.Location.Y + CornerRadius.TopRight), CornerRadius.TopRight, CornerRadius.TopRight));

            if (CornerRadius.BottomRight > 0)
                roundedRectGeometry.Children.Add(
                    new EllipseGeometry(new Point(rect.Location.X + rect.Width - CornerRadius.BottomRight, rect.Location.Y + rect.Height - CornerRadius.BottomRight), CornerRadius.BottomRight, CornerRadius.BottomRight));

            if (CornerRadius.BottomLeft > 0)
                roundedRectGeometry.Children.Add(
                    new EllipseGeometry(new Point(rect.Location.X + CornerRadius.BottomLeft, rect.Location.Y + rect.Height - CornerRadius.BottomLeft), CornerRadius.BottomLeft, CornerRadius.BottomLeft));

            var pathFigure = new PathFigure
            {
                IsClosed = true,
                StartPoint = new Point(rect.Location.X + CornerRadius.TopLeft, rect.Location.Y),
                Segments = new PathSegmentCollection
                {
                    new LineSegment { Point = new Point(rect.Location.X + rect.Width - CornerRadius.TopRight, rect.Location.Y) },
                    new LineSegment { Point = new Point(rect.Location.X + rect.Width, rect.Location.Y + CornerRadius.TopRight) },
                    new LineSegment { Point = new Point(rect.Location.X + rect.Width, rect.Location.Y + rect.Height - CornerRadius.BottomRight) },
                    new LineSegment { Point = new Point(rect.Location.X + rect.Width - CornerRadius.BottomRight, rect.Location.Y + rect.Height) },
                    new LineSegment { Point = new Point(rect.Location.X + CornerRadius.BottomLeft, rect.Location.Y + rect.Height) },
                    new LineSegment { Point = new Point(rect.Location.X, rect.Location.Y + rect.Height - CornerRadius.BottomLeft) },
                    new LineSegment { Point = new Point(rect.Location.X, rect.Location.Y + CornerRadius.TopLeft) }
                }
            };

            var pathFigureCollection = new PathFigureCollection
            {
                pathFigure
            };

            roundedRectGeometry.Children.Add(new PathGeometry(pathFigureCollection, FillRule.Nonzero, null));

            return roundedRectGeometry;
        }

        /// <summary>
        /// Apply Clip
        /// </summary>
        protected virtual void OnApplyClip()
        {
            UIElement child = this.Child;
            if (child != null)
            {
                var rect = new Rect(RenderSize);
                Clip = CreateRoundRectangleGeometry(rect);
            }
        }

        /// <summary>
        /// OnRenderSizeChanged
        /// </summary>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            OnApplyClip();
            base.OnRenderSizeChanged(sizeInfo);
        }
    }
}