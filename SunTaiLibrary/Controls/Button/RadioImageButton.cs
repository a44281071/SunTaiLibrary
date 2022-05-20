using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SunTaiLibrary.Controls
{
  /// <summary>
  /// radio image button.
  /// </summary>
  public class RadioImageButton : RadioButton
  {
    static RadioImageButton()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioImageButton), new FrameworkPropertyMetadata(typeof(RadioImageButton)));
    }

    #region propdp_CheckedImage

    /// <summary>
    /// display image for checked state 'True'.
    /// </summary>
    [Bindable(true)]
    [Category("Common")]
    public ImageSource CheckedImage
    {
      get { return (ImageSource)GetValue(CheckedImageProperty); }
      set { SetValue(CheckedImageProperty, value); }
    }

    /// <summary>
    /// propdp display image for checked state 'True'.
    /// </summary>
    public static readonly DependencyProperty CheckedImageProperty =
        DependencyProperty.Register("CheckedImage", typeof(ImageSource), typeof(RadioImageButton), new PropertyMetadata());

    #endregion propdp_CheckedImage

    #region propdp_IndeterminateImage

    /// <summary>
    /// display image for checked state 'Null".
    /// </summary>
    [Bindable(true)]
    [Category("Common")]
    public ImageSource IndeterminateImage
    {
      get { return (ImageSource)GetValue(IndeterminateImageProperty); }
      set { SetValue(IndeterminateImageProperty, value); }
    }

    /// <summary>
    /// propdp display image for checked state 'Null'.
    /// </summary>
    public static readonly DependencyProperty IndeterminateImageProperty =
        DependencyProperty.Register("IndeterminateImage", typeof(ImageSource), typeof(RadioImageButton), new PropertyMetadata());

    #endregion propdp_IndeterminateImage

    #region propdp_NormalImage

    /// <summary>
    /// display image for checked state 'False'.
    /// </summary>
    [Bindable(true)]
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
        DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(RadioImageButton), new PropertyMetadata());

    #endregion propdp_NormalImage

    #region propdp_HoverImage

    /// <summary>
    /// 鼠标经过图片
    /// </summary>
    [Bindable(true)]
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
    DependencyProperty.Register("HoverImage", typeof(ImageSource), typeof(RadioImageButton), new PropertyMetadata());

    #endregion propdp_HoverImage

    #region propdp_PressedImage

    /// <summary>
    /// 鼠标按下图片
    /// </summary>
    [Bindable(true)]
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
    DependencyProperty.Register("PressedImage", typeof(ImageSource), typeof(RadioImageButton), new PropertyMetadata());

    #endregion propdp_PressedImage

    #region propdp_DisableImage

    /// <summary>
    /// 禁用控件
    /// </summary>
    [Bindable(true)]
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
    DependencyProperty.Register("DisableImage", typeof(ImageSource), typeof(RadioImageButton), new PropertyMetadata());

    #endregion propdp_DisableImage
  }
}
