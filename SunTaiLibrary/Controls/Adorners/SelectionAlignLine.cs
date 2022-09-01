using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Documents;

namespace SunTaiLibrary.Controls
{
    public class SelectionAlignLine:Adorner
    {
        public SelectionAlignLine(UIElement adornedElement, Point start, Point end) : base(adornedElement)
        {
            startPoint = start;
            endPoint = end;
        }

        Point startPoint = default(Point);
        Point endPoint = default(Point);
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Rect adornerRect = new Rect(AdornedElement.DesiredSize);
            Pen render = new Pen(new SolidColorBrush(Colors.RoyalBlue), 3);
            render.DashCap = PenLineCap.Round;
            render.DashStyle = new DashStyle(new List<double>() { 4, 2 }, 2);
            drawingContext.DrawLine(render, startPoint, endPoint);
        }
    }
}
