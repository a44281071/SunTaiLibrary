using System.Windows;

namespace SunTaiLibrary.Controls
{
    public static class DisplayRatioAttached
    {
        public static double GetDisplayRatio(DependencyObject obj)
        {
            return (double)obj.GetValue(DisplayRatioProperty);
        }

        public static void SetDisplayRatio(DependencyObject obj, double value)
        {
            obj.SetValue(DisplayRatioProperty, value);
        }

        /// <summary>
        /// 【-1：不保持比例】【other：宽高保持比例】
        /// </summary>
        public static readonly DependencyProperty DisplayRatioProperty =
            DependencyProperty.RegisterAttached("DisplayRatio"
                , typeof(double)
                , typeof(DisplayRatioAttached)
                , new PropertyMetadata(-1d));
    }
}
