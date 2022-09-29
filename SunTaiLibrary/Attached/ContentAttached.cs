using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SunTaiLibrary.Attached
{
    /// <summary>
    /// add more properties for content or children.
    /// </summary>
    public static class ContentAttached
    {
        #region ContentMargin

        /// <summary>
        /// ContentMargin
        /// </summary>
        public static Thickness GetContentMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ContentMarginProperty);
        }

        /// <summary>
        /// ContentMargin
        /// </summary>
        public static void SetContentMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ContentMarginProperty, value);
        }

        /// <summary>
        /// ContentMargin
        /// </summary>
        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.RegisterAttached("ContentMargin", typeof(Thickness), typeof(ContentAttached), new PropertyMetadata(new Thickness()));

        #endregion ContentMargin

        #region Orientation

        /// <summary>
        /// path and content children stacked.
        /// </summary>
        public static Orientation GetOrientation(DependencyObject obj)
        {
            return (Orientation)obj.GetValue(OrientationProperty);
        }

        /// <summary>
        /// path and content children stacked.
        /// </summary>
        public static void SetOrientation(DependencyObject obj, Orientation value)
        {
            obj.SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// path and content children stacked.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(ContentAttached), new PropertyMetadata(Orientation.Vertical));

        #endregion Orientation

        #region Path

        /// <summary>
        /// path geometry for content part.
        /// </summary>
        public static Geometry GetPath(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(PathProperty);
        }

        /// <summary>
        /// path geometry for content part.
        /// </summary>
        public static void SetPath(DependencyObject obj, Geometry value)
        {
            obj.SetValue(PathProperty, value);
        }

        /// <summary>
        /// path geometry for content part.
        /// </summary>
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.RegisterAttached("Path", typeof(Geometry), typeof(ContentAttached), new PropertyMetadata());

        #endregion Path

        #region CheckedPathProperty

        /// <summary>
        /// CheckedPath
        /// </summary>
        public static Geometry GetCheckedPath(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(CheckedPathProperty);
        }

        /// <summary>
        /// CheckedPath
        /// </summary>
        public static void SetCheckedPath(DependencyObject obj, Geometry value)
        {
            obj.SetValue(CheckedPathProperty, value);
        }

        /// <summary>
        /// CheckedPath
        /// </summary>
        public static readonly DependencyProperty CheckedPathProperty =
           DependencyProperty.RegisterAttached("CheckedPath", typeof(Geometry), typeof(ContentAttached), new PropertyMetadata());

        #endregion CheckedPathProperty

        #region PathMargin

        public static Thickness GetPathMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(PathMarginProperty);
        }

        public static void SetPathMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(PathMarginProperty, value);
        }

        public static readonly DependencyProperty PathMarginProperty =
            DependencyProperty.RegisterAttached("PathMargin", typeof(Thickness), typeof(ContentAttached), new PropertyMetadata(new Thickness()));

        #endregion PathMargin

        #region PathHeight

        /// <summary>
        /// PathHeight
        /// </summary>
        public static double GetPathHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(PathHeightProperty);
        }

        /// <summary>
        /// PathHeight
        /// </summary>
        public static void SetPathHeight(DependencyObject obj, double value)
        {
            obj.SetValue(PathHeightProperty, value);
        }

        /// <summary>
        /// PathHeight
        /// </summary>
        public static readonly DependencyProperty PathHeightProperty =
            DependencyProperty.RegisterAttached("PathHeight", typeof(double), typeof(ContentAttached), new PropertyMetadata(Double.NaN));

        #endregion PathHeight

        #region PathWidth

        /// <summary>
        /// PathWidth
        /// </summary>
        public static double GetPathWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(PathWidthProperty);
        }

        /// <summary>
        /// PathWidth
        /// </summary>
        public static void SetPathWidth(DependencyObject obj, double value)
        {
            obj.SetValue(PathWidthProperty, value);
        }

        /// <summary>
        /// PathWidth
        /// </summary>
        public static readonly DependencyProperty PathWidthProperty =
            DependencyProperty.RegisterAttached("PathWidth", typeof(double), typeof(ContentAttached), new PropertyMetadata(Double.NaN));

        #endregion PathWidth

        #region PathFill

        /// <summary>
        /// PathFill
        /// </summary>
        public static Brush GetPathFill(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PathFillProperty);
        }

        /// <summary>
        /// PathFill
        /// </summary>
        public static void SetPathFill(DependencyObject obj, Brush value)
        {
            obj.SetValue(PathFillProperty, value);
        }

        /// <summary>
        /// PathFill
        /// </summary>
        public static readonly DependencyProperty PathFillProperty =
            DependencyProperty.RegisterAttached("PathFill", typeof(Brush), typeof(ContentAttached), new PropertyMetadata());

        #endregion PathFill

        #region PathHorizontalAlignment

        public static HorizontalAlignment GetPathHorizontalAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(PathHorizontalAlignmentProperty);
        }

        public static void SetPathHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(PathHorizontalAlignmentProperty, value);
        }

        public static readonly DependencyProperty PathHorizontalAlignmentProperty =
            DependencyProperty.RegisterAttached("PathHorizontalAlignment", typeof(HorizontalAlignment), typeof(ContentAttached), new PropertyMetadata(HorizontalAlignment.Stretch));

        #endregion PathHorizontalAlignment

        #region PathVerticalAlignment

        public static VerticalAlignment GetPathVerticalAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(PathVerticalAlignmentProperty);
        }

        public static void SetPathVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(PathVerticalAlignmentProperty, value);
        }

        public static readonly DependencyProperty PathVerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("PathVerticalAlignment", typeof(VerticalAlignment), typeof(ContentAttached), new PropertyMetadata(VerticalAlignment.Stretch));

        #endregion PathVerticalAlignment

        #region PathStretch

        public static Stretch GetPathStretch(DependencyObject obj)
        {
            return (Stretch)obj.GetValue(PathStretchProperty);
        }

        public static void SetPathStretch(DependencyObject obj, Stretch value)
        {
            obj.SetValue(PathStretchProperty, value);
        }

        public static readonly DependencyProperty PathStretchProperty =
            DependencyProperty.RegisterAttached("PathStretch", typeof(Stretch), typeof(ContentAttached), new PropertyMetadata(Stretch.Uniform));

        #endregion PathStretch

        #region TopContent

        /// <summary>
        /// attach a second Content, like window title bar extra content, head content, box leader sign.
        /// </summary>
        public static object GetTopContent(DependencyObject obj)
        {
            return obj.GetValue(TopContentProperty);
        }

        /// <summary>
        /// attach a second Content, like window title bar extra content, head content, box leader sign.
        /// </summary>
        public static void SetTopContent(DependencyObject obj, object value)
        {
            obj.SetValue(TopContentProperty, value);
        }

        /// <summary>
        /// attach a second Content, like window title bar extra content, head content, box leader sign.
        /// </summary>
        public static readonly DependencyProperty TopContentProperty =
            DependencyProperty.RegisterAttached("TopContent", typeof(object), typeof(ContentAttached), new PropertyMetadata());

        /// <summary>
        /// TopContentStringFormat
        /// </summary>
        public static string GetTopContentStringFormat(DependencyObject obj)
        {
            return (string)obj.GetValue(TopContentStringFormatProperty);
        }

        /// <summary>
        /// TopContentStringFormat
        /// </summary>
        public static void SetTopContentStringFormat(DependencyObject obj, string value)
        {
            obj.SetValue(TopContentStringFormatProperty, value);
        }

        /// <summary>
        /// TopContentStringFormat
        /// </summary>
        public static readonly DependencyProperty TopContentStringFormatProperty =
            DependencyProperty.RegisterAttached("TopContentStringFormat", typeof(string), typeof(ContentAttached), new PropertyMetadata());

        /// <summary>
        /// TopContentTemplate
        /// </summary>
        public static DataTemplate GetTopContentTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(TopContentTemplateProperty);
        }

        /// <summary>
        /// TopContentTemplate
        /// </summary>
        public static void SetTopContentTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(TopContentTemplateProperty, value);
        }

        /// <summary>
        /// TopContentTemplate
        /// </summary>
        public static readonly DependencyProperty TopContentTemplateProperty =
            DependencyProperty.RegisterAttached("TopContentTemplate", typeof(DataTemplate), typeof(ContentAttached), new PropertyMetadata());

        /// <summary>
        /// TopContentTemplateSelector
        /// </summary>
        public static DataTemplateSelector GetTopContentTemplateSelector(DependencyObject obj)
        {
            return (DataTemplateSelector)obj.GetValue(TopContentTemplateSelectorProperty);
        }

        /// <summary>
        /// TopContentTemplateSelector
        /// </summary>
        public static void SetTopContentTemplateSelector(DependencyObject obj, DataTemplateSelector value)
        {
            obj.SetValue(TopContentTemplateSelectorProperty, value);
        }

        /// <summary>
        /// TopContentTemplateSelector
        /// </summary>
        public static readonly DependencyProperty TopContentTemplateSelectorProperty =
            DependencyProperty.RegisterAttached("TopContentTemplateSelector", typeof(DataTemplateSelector), typeof(ContentAttached), new PropertyMetadata());

        /// <summary>
        /// TopContentHeight
        /// </summary>
        public static double GetTopContentHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(TopContentHeightProperty);
        }

        /// <summary>
        /// TopContentHeight
        /// </summary>
        public static void SetTopContentHeight(DependencyObject obj, double value)
        {
            obj.SetValue(TopContentHeightProperty, value);
        }

        /// <summary>
        /// TopContentHeight
        /// </summary>
        public static readonly DependencyProperty TopContentHeightProperty =
            DependencyProperty.RegisterAttached("TopContentHeight", typeof(double), typeof(ContentAttached), new PropertyMetadata(Double.NaN));

        /// <summary>
        /// TopContentWidth
        /// </summary>
        public static double GetTopContentWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(TopContentWidthProperty);
        }

        /// <summary>
        /// TopContentWidth
        /// </summary>
        public static void SetTopContentWidth(DependencyObject obj, double value)
        {
            obj.SetValue(TopContentWidthProperty, value);
        }

        /// <summary>
        /// TopContentWidth
        /// </summary>
        public static readonly DependencyProperty TopContentWidthProperty =
            DependencyProperty.RegisterAttached("TopContentWidth", typeof(double), typeof(ContentAttached), new PropertyMetadata(Double.NaN));

        #endregion TopContent

        #region CheckedContent

        /// <summary>
        /// CheckedContent
        /// </summary>
        public static object GetCheckedContent(DependencyObject obj)
        {
            return obj.GetValue(CheckedContentProperty);
        }

        /// <summary>
        /// CheckedContent
        /// </summary>
        public static void SetCheckedContent(DependencyObject obj, object value)
        {
            obj.SetValue(CheckedContentProperty, value);
        }

        /// <summary>
        /// CheckedContent
        /// </summary>
        public static readonly DependencyProperty CheckedContentProperty =
            DependencyProperty.RegisterAttached("CheckedContent", typeof(object), typeof(ContentAttached), new PropertyMetadata());

        #endregion CheckedContent

        #region Image_Attach

        /// <summary>
        /// NormalImage
        /// </summary>
        public static ImageSource GetNormalImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(NormalImageProperty);
        }

        /// <summary>
        /// NormalImage
        /// </summary>
        public static void SetNormalImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(NormalImageProperty, value);
        }

        /// <summary>
        /// NormalImage
        /// </summary>
        public static readonly DependencyProperty NormalImageProperty =
            DependencyProperty.RegisterAttached("NormalImage", typeof(ImageSource), typeof(ContentAttached), new PropertyMetadata());

        /// <summary>
        /// CheckedImage
        /// </summary>
        public static ImageSource GetCheckedImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(CheckedImageProperty);
        }

        /// <summary>
        /// CheckedImage
        /// </summary>
        public static void SetCheckedImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(CheckedImageProperty, value);
        }

        /// <summary>
        /// CheckedImage
        /// </summary>
        public static readonly DependencyProperty CheckedImageProperty =
            DependencyProperty.RegisterAttached("CheckedImage", typeof(ImageSource), typeof(ContentAttached), new PropertyMetadata());

        #endregion Image_Attach
    }
}