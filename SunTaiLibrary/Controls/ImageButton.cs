using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SunTaiLibrary.Controls
{
  public class ImageButton : Button
  {
    static ImageButton()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
    }

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
  }
}