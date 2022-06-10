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
    /// <summary>
    /// Mask child clip.
    /// </summary>
    public class MaskBox : Decorator
    {
        /// <summary>
        /// enable mask, default is false.
        /// </summary>
        public bool IsMaskEnabled
        {
            get { return (bool)GetValue(IsMaskEnabledProperty); }
            set { SetValue(IsMaskEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsMaskEnabledProperty =
            DependencyProperty.Register("IsMaskEnabled", typeof(bool), typeof(MaskBox), new PropertyMetadata(false, PropertyChangedUpdateMask));

        /// <summary>
        /// left mask ratio. Range is 0 - 1.
        /// </summary>
        public double LeftMask
        {
            get { return (double)GetValue(LeftMaskProperty); }
            set { SetValue(LeftMaskProperty, value); }
        }

        /// <summary>
        /// left mask ratio. Range is 0 - 1.
        /// </summary>
        public static readonly DependencyProperty LeftMaskProperty =
            DependencyProperty.Register("LeftMask", typeof(double), typeof(MaskBox), new PropertyMetadata(0d, PropertyChangedUpdateMask));

        /// <summary>
        /// top mask ratio. Range is 0 - 1.
        /// </summary>
        public double TopMask
        {
            get { return (double)GetValue(TopMaskProperty); }
            set { SetValue(TopMaskProperty, value); }
        }

        /// <summary>
        /// top mask ratio. Range is 0 - 1.
        /// </summary>
        public static readonly DependencyProperty TopMaskProperty =
            DependencyProperty.Register("TopMask", typeof(double), typeof(MaskBox), new PropertyMetadata(0d, PropertyChangedUpdateMask));

        /// <summary>
        /// right mask ratio. Range is 0 - 1.
        /// </summary>
        public double RightMask
        {
            get { return (double)GetValue(RightMaskProperty); }
            set { SetValue(RightMaskProperty, value); }
        }

        /// <summary>
        /// right mask ratio. Range is 0 - 1.
        /// </summary>
        public static readonly DependencyProperty RightMaskProperty =
            DependencyProperty.Register("RightMask", typeof(double), typeof(MaskBox), new PropertyMetadata(0d, PropertyChangedUpdateMask));

        /// <summary>
        /// bottom mask ratio. Range is 0 - 1.
        /// </summary>
        public double BottomMask
        {
            get { return (double)GetValue(BottomMaskProperty); }
            set { SetValue(BottomMaskProperty, value); }
        }

        /// <summary>
        /// bottom mask ratio. Range is 0 - 1.
        /// </summary>
        public static readonly DependencyProperty BottomMaskProperty =
            DependencyProperty.Register("BottomMask", typeof(double), typeof(MaskBox), new PropertyMetadata(0d, PropertyChangedUpdateMask));

        private static void PropertyChangedUpdateMask(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MaskBox ele) ele.UpdateClipChild();
        }

        private RectangleGeometry maskGeometry;

        /// <summary>
        /// 更新对子级控件裁切功能
        /// </summary>
        private void UpdateClipChild()
        {
            if (Child is null) return;

            if (IsMaskEnabled)
            {
                if (Child.Clip == null || Child.Clip != maskGeometry)
                {
                    maskGeometry = new RectangleGeometry();
                    Child.Clip = maskGeometry;
                }
                var maxSize = RenderSize;
                double rectWidth = Math.Max(0, (1 - LeftMask - RightMask) * maxSize.Width);
                double rectHeight = Math.Max(0, (1 - TopMask - BottomMask) * maxSize.Height);
                var rect = new Rect(maxSize.Width * LeftMask, maxSize.Height * TopMask, rectWidth, rectHeight);
                maskGeometry.Rect = rect;
            }
            else if (Child.Clip == maskGeometry)
            {
                Child.Clip = null;
                maskGeometry = null;
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            UpdateClipChild();
        }
    }
}