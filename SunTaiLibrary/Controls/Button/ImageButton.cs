using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SunTaiLibrary.Controls
{
  /// <summary>
  /// button with image.
  /// </summary>
  public class ImageButton : Button
  {
    static ImageButton()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
    }

    /// <summary>
    /// Gets or sets a System.Windows.Media.Stretch enumeration value that describes
    /// how the shape fills its allocated space.
    /// </summary>
    [Category("Common")]
    public Stretch Stretch
    {
      get { return (Stretch)GetValue(StretchProperty); }
      set { SetValue(StretchProperty, value); }
    }

    /// <summary>
    /// Identifies the System.Windows.Shapes.Shape.Stretch dependency property.
    /// </summary>
    public static readonly DependencyProperty StretchProperty =
        DependencyProperty.Register("Stretch", typeof(Stretch), typeof(ImageButton), new PropertyMetadata(Stretch.Uniform));

    #region Alignment Properties

    /// <summary>
    /// Gets or sets the horizontal alignment of the control's image.
    /// </summary>
    [Category("Common")]
    public HorizontalAlignment HorizontalImageAlignment
    {
      get { return (HorizontalAlignment)GetValue(HorizontalImageAlignmentProperty); }
      set { SetValue(HorizontalImageAlignmentProperty, value); }
    }

    /// <summary>
    /// Identifies the System.Windows.FrameworkElement.HorizontalAlignment dependency property.
    /// </summary>
    public static readonly DependencyProperty HorizontalImageAlignmentProperty =
        DependencyProperty.Register("HorizontalImageAlignment", typeof(HorizontalAlignment), typeof(ImageButton), new PropertyMetadata(HorizontalAlignment.Stretch));

    /// <summary>
    /// Gets or sets the vertical alignment of the control's image.
    /// </summary>
    [Category("Common")]
    public VerticalAlignment VerticalImageAlignment
    {
      get { return (VerticalAlignment)GetValue(VerticalImageAlignmentProperty); }
      set { SetValue(VerticalImageAlignmentProperty, value); }
    }

    /// <summary>
    /// Identifies the System.Windows.Controls.Control.VerticalContentAlignment dependency property.
    /// </summary>
    public static readonly DependencyProperty VerticalImageAlignmentProperty =
        DependencyProperty.Register("VerticalImageAlignment", typeof(VerticalAlignment), typeof(ImageButton), new PropertyMetadata(VerticalAlignment.Stretch));


    /// <summary>
    /// Image host control Margin.
    /// </summary>
    public Thickness ImageMargin
    {
      get { return (Thickness)GetValue(ImageMarginProperty); }
      set { SetValue(ImageMarginProperty, value); }
    }

    /// <summary>
    /// Identifies the System.Windows.Thickness dependency property.
    /// </summary>
    public static readonly DependencyProperty ImageMarginProperty =
        DependencyProperty.Register("ImageMargin", typeof(Thickness), typeof(ImageButton), new PropertyMetadata(new Thickness()));


    #endregion Alignment Properties

    #region Image Properties

    /// <summary>
    /// 默认图片
    /// </summary>
    [Category("Common")]
    public ImageSource NormalImage
    {
      get { return (ImageSource)GetValue(NormalImageProperty); }
      set { SetValue(NormalImageProperty, value); }
    }

    /// <summary>
    /// 默认图片
    /// </summary>
    public static readonly DependencyProperty NormalImageProperty =
        DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata());

    /// <summary>
    /// 鼠标经过图片
    /// </summary>
    [Category("Common")]
    public ImageSource HoverImage
    {
      get { return (ImageSource)GetValue(HoverImageProperty); }
      set { SetValue(HoverImageProperty, value); }
    }

    /// <summary>
    /// 鼠标经过图片
    /// </summary>
    public static readonly DependencyProperty HoverImageProperty =
    DependencyProperty.Register("HoverImage", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata());

    /// <summary>
    /// 鼠标按下图片
    /// </summary>
    [Category("Common")]
    public ImageSource PressedImage
    {
      get { return (ImageSource)GetValue(PressedImageProperty); }
      set { SetValue(PressedImageProperty, value); }
    }

    /// <summary>
    /// 鼠标按下图片
    /// </summary>
    public static readonly DependencyProperty PressedImageProperty =
    DependencyProperty.Register("PressedImage", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata());

    /// <summary>
    /// 禁用控件
    /// </summary>
    [Category("Common")]
    public ImageSource DisableImage
    {
      get { return (ImageSource)GetValue(DisableImageProperty); }
      set { SetValue(DisableImageProperty, value); }
    }

    /// <summary>
    /// 禁用控件
    /// </summary>
    public static readonly DependencyProperty DisableImageProperty =
    DependencyProperty.Register("DisableImage", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata());

    #endregion Image Properties
  }
}