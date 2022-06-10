using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SunTaiLibrary.Controls
{
    /// <summary>
    /// 采用类似12均分表格的方式，按行的方式布局子项
    /// </summary>
    public class SqueezePanel : Panel
    {
        const double SqueezeNumber = 12d;

        #region 两个指定宽度的属性

        /// <summary>
        /// 默认子项的栅格大小，值范围为【0-12】
        /// </summary>
        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        /// <summary>
        /// 默认子项的栅格大小，值范围为【0-12】
        /// </summary>
        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register("ItemWidth", typeof(double), typeof(SqueezePanel)
                , new FrameworkPropertyMetadata(6d
                    , FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                    , OnItemWidthPropertyChanged
                    , OnCoerceItemWidthProperty)
                , ValidateItemWidthProperty);

        /// <summary>
        /// 默认子项的栅格大小，值范围为【0-12】，默认值为NaN.
        /// </summary>
        public static double GetWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(WidthProperty);
        }

        /// <summary>
        /// 默认子项的栅格大小，值范围为【0-12】，默认值为NaN.
        /// </summary>
        public static void SetWidth(DependencyObject obj, double value)
        {
            obj.SetValue(WidthProperty, value);
        }

        /// <summary>
        /// 默认子项的栅格大小，值范围为【0-12】，默认值为NaN.
        /// </summary>
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.RegisterAttached("Width", typeof(double), typeof(SqueezePanel)
                , new FrameworkPropertyMetadata(Double.NaN
                    , FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                    , OnItemWidthPropertyChanged
                    , OnCoerceItemWidthProperty)
                , ValidateItemWidthProperty);

        /// <summary>
        /// 值范围为【0-12】
        /// </summary>
        private static bool ValidateItemWidthProperty(object value)
        {
            double vv = (double)value;
            return vv >= 0 && vv <= SqueezeNumber;
        }

        private static void OnItemWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// 值范围为【0-12】
        /// </summary>
        private static object OnCoerceItemWidthProperty(DependencyObject d, object baseValue)
        {
            double vv = (double)baseValue;
            if (vv > SqueezeNumber) { return SqueezeNumber; }
            else if (vv < 0) { return 0; }
            else { return vv; }
        }

        #endregion 两个指定宽度的属性

        /// <summary>
        /// 计算大小
        /// </summary>
        protected override Size MeasureOverride(Size availableSize)
        {
            UIElementCollection children = InternalChildren;

            double parentWidth = 0;   // Our current required width due to children thus far.
            double accumulatedWidth = 0;   // Total width consumed by children.

            double lineLengthScale = SqueezeNumber;  // 本行剩下的空间

            for (int i = 0, count = children.Count; i < count; ++i)
            {
                UIElement child = children[i];            
                if (child == null) { continue; }

                double childItemWidthScale = SqueezePanel.GetWidth(child);
                if (Double.IsNaN(childItemWidthScale))
                {
                    childItemWidthScale = ItemWidth;
                }
                double childWidth = availableSize.Width * (childItemWidthScale / SqueezeNumber);

                child.Measure(new Size(childWidth,availableSize) );

                Size childDesiredSize = child.DesiredSize;

                if (maxChildDesiredWidth < childDesiredSize.Width) { maxChildDesiredWidth = childDesiredSize.Width; }
                if (maxChildDesiredHeight < childDesiredSize.Height) { maxChildDesiredHeight = childDesiredSize.Height; }
            }

            return new Size(w, h);
        }

        /// <summary>
        /// 放置
        /// </summary>
        protected override Size ArrangeOverride(Size finalSize)
        {
        }
    }
}