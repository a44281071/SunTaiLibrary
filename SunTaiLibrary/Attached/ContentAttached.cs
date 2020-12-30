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

    public static Thickness GetContentMargin(DependencyObject obj)
    {
      return (Thickness)obj.GetValue(ContentMarginProperty);
    }

    public static void SetContentMargin(DependencyObject obj, Thickness value)
    {
      obj.SetValue(ContentMarginProperty, value);
    }

    public static readonly DependencyProperty ContentMarginProperty =
        DependencyProperty.RegisterAttached("ContentMargin", typeof(Thickness), typeof(ContentAttached), new PropertyMetadata(new Thickness()));

    #endregion ContentMargin

    #region Orientation

    public static Orientation GetOrientation(DependencyObject obj)
    {
      return (Orientation)obj.GetValue(OrientationProperty);
    }

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

    public static Geometry GetPath(DependencyObject obj)
    {
      return (Geometry)obj.GetValue(PathProperty);
    }

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

    public static Geometry GetCheckedPath(DependencyObject obj)
    {
      return (Geometry)obj.GetValue(CheckedPathProperty);
    }

    public static void SetCheckedPath(DependencyObject obj, Geometry value)
    {
      obj.SetValue(CheckedPathProperty, value);
    }

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

    public static double GetPathHeight(DependencyObject obj)
    {
      return (double)obj.GetValue(PathHeightProperty);
    }

    public static void SetPathHeight(DependencyObject obj, double value)
    {
      obj.SetValue(PathHeightProperty, value);
    }

    public static readonly DependencyProperty PathHeightProperty =
        DependencyProperty.RegisterAttached("PathHeight", typeof(double), typeof(ContentAttached), new PropertyMetadata(Double.NaN));

    #endregion PathHeight

    #region PathWidth

    public static double GetPathWidth(DependencyObject obj)
    {
      return (double)obj.GetValue(PathWidthProperty);
    }

    public static void SetPathWidth(DependencyObject obj, double value)
    {
      obj.SetValue(PathWidthProperty, value);
    }

    public static readonly DependencyProperty PathWidthProperty =
        DependencyProperty.RegisterAttached("PathWidth", typeof(double), typeof(ContentAttached), new PropertyMetadata(Double.NaN));

    #endregion PathWidth

    #region PathFill

    public static Brush GetPathFill(DependencyObject obj)
    {
      return (Brush)obj.GetValue(PathFillProperty);
    }

    public static void SetPathFill(DependencyObject obj, Brush value)
    {
      obj.SetValue(PathFillProperty, value);
    }

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

    public static object GetTopContent(DependencyObject obj)
    {
      return obj.GetValue(TopContentProperty);
    }

    public static void SetTopContent(DependencyObject obj, object value)
    {
      obj.SetValue(TopContentProperty, value);
    }

    public static readonly DependencyProperty TopContentProperty =
        DependencyProperty.RegisterAttached("TopContent", typeof(object), typeof(ContentAttached), new PropertyMetadata());

    public static string GetTopContentStringFormat(DependencyObject obj)
    {
      return (string)obj.GetValue(TopContentStringFormatProperty);
    }

    public static void SetTopContentStringFormat(DependencyObject obj, string value)
    {
      obj.SetValue(TopContentStringFormatProperty, value);
    }

    public static readonly DependencyProperty TopContentStringFormatProperty =
        DependencyProperty.RegisterAttached("TopContentStringFormat", typeof(string), typeof(ContentAttached), new PropertyMetadata());

    public static DataTemplate GetTopContentTemplate(DependencyObject obj)
    {
      return (DataTemplate)obj.GetValue(TopContentTemplateProperty);
    }

    public static void SetTopContentTemplate(DependencyObject obj, DataTemplate value)
    {
      obj.SetValue(TopContentTemplateProperty, value);
    }

    public static readonly DependencyProperty TopContentTemplateProperty =
        DependencyProperty.RegisterAttached("TopContentTemplate", typeof(DataTemplate), typeof(ContentAttached), new PropertyMetadata());

    public static DataTemplateSelector GetTopContentTemplateSelector(DependencyObject obj)
    {
      return (DataTemplateSelector)obj.GetValue(TopContentTemplateSelectorProperty);
    }

    public static void SetTopContentTemplateSelector(DependencyObject obj, DataTemplateSelector value)
    {
      obj.SetValue(TopContentTemplateSelectorProperty, value);
    }

    public static readonly DependencyProperty TopContentTemplateSelectorProperty =
        DependencyProperty.RegisterAttached("TopContentTemplateSelector", typeof(DataTemplateSelector), typeof(ContentAttached), new PropertyMetadata());

    #endregion TopContent

    #region CheckedContent

    public static object GetCheckedContent(DependencyObject obj)
    {
      return obj.GetValue(CheckedContentProperty);
    }

    public static void SetCheckedContent(DependencyObject obj, object value)
    {
      obj.SetValue(CheckedContentProperty, value);
    }

    public static readonly DependencyProperty CheckedContentProperty =
        DependencyProperty.RegisterAttached("CheckedContent", typeof(object), typeof(ContentAttached), new PropertyMetadata());

    #endregion CheckedContent

    #region NormalImage

    public static ImageSource GetNormalImage(DependencyObject obj)
    {
      return (ImageSource)obj.GetValue(NormalImageProperty);
    }

    public static void SetNormalImage(DependencyObject obj, ImageSource value)
    {
      obj.SetValue(NormalImageProperty, value);
    }

    public static readonly DependencyProperty NormalImageProperty =
        DependencyProperty.RegisterAttached("NormalImage", typeof(ImageSource), typeof(ContentAttached), new PropertyMetadata());

    #endregion NormalImage

    #region CheckedImage

    public static ImageSource GetCheckedImage(DependencyObject obj)
    {
      return (ImageSource)obj.GetValue(CheckedImageProperty);
    }

    public static void SetCheckedImage(DependencyObject obj, ImageSource value)
    {
      obj.SetValue(CheckedImageProperty, value);
    }

    public static readonly DependencyProperty CheckedImageProperty =
        DependencyProperty.RegisterAttached("CheckedImage", typeof(ImageSource), typeof(ContentAttached), new PropertyMetadata());

    #endregion CheckedImage
  }
}