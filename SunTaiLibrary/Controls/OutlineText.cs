using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace SunTaiLibrary.Controls
{
  /// <summary>
  /// 本文本块支持更多的文字特效，并可以防止中西文混合显示时水平渐变色按语言字体文件，导致色彩重置的问题。
  /// </summary>
  public class OutlineText : FrameworkElement, IAddChild
  {
    #region Private Fields

    /// <summary>
    /// 文字几何形状
    /// </summary>
    private Geometry m_TextGeometry;

    #endregion Private Fields

    #region Private Methods

    /// <summary>
    /// 当依赖项属性改变文字无效时，创建新的空心文字对象来显示。
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    private static void OnOutlineTextInvalidated(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (Convert.ToString(e.NewValue) != Convert.ToString(e.OldValue))
      {
        ((OutlineText)d).CreateText();
      }
    }

    #endregion Private Methods

    #region FrameworkElement Overrides

    /// <summary>
    /// 重写绘制文字的方法。
    /// </summary>
    /// <param name="drawingContext">空心文字控件的绘制上下文。</param>
    protected override void OnRender(DrawingContext drawingContext)
    {
      drawingContext.DrawGeometry(Fill, new Pen(Stroke, StrokeThickness), m_TextGeometry);
    }

    /// <summary>
    /// 基于格式化文字创建文字的几何轮廓。
    /// </summary>
    public void CreateText()
    {
      FontStyle fontStyle = FontStyles.Normal;
      FontWeight fontWeight = FontWeights.Medium;
      if (Bold) fontWeight = FontWeights.Bold;
      if (Italic) fontStyle = FontStyles.Italic;

      var formattedText = new FormattedText(
          Text ?? "", CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight,
          new Typeface(Font, fontStyle, fontWeight, FontStretches.Normal),
          FontSize, Brushes.Black)
      {
        MaxTextWidth = MaxTextWidth,
        MaxTextHeight = MaxTextHeight
      };
      // 创建表示文字的几何对象。
      m_TextGeometry = formattedText.BuildGeometry(new Point(0, 0));
      // 基于格式化文字的大小设置空心文字的大小。
      MinWidth = formattedText.Width;
      MinHeight = formattedText.Height;
    }

    #endregion FrameworkElement Overrides

    #region DependencyProperties

    /// <summary>
    /// 指定将文本约束为特定宽度
    /// </summary>
    [Category("Text")]
    public double MaxTextWidth
    {
      get { return (double)GetValue(MaxTextWidthProperty); }
      set { SetValue(MaxTextWidthProperty, value); }
    }

    /// <summary>
    /// 指定将文本约束为特定宽度依赖属性
    /// </summary>
    public static readonly DependencyProperty MaxTextWidthProperty =
        DependencyProperty.Register("MaxTextWidth", typeof(double), typeof(OutlineText),
            new FrameworkPropertyMetadata(1000.0, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnOutlineTextInvalidated), null));

    /// <summary>
    /// 指定将文本约束为特定高度
    /// </summary>
    [Category("Text")]
    public double MaxTextHeight
    {
      get { return (double)GetValue(MaxTextHeightProperty); }
      set { SetValue(MaxTextHeightProperty, value); }
    }

    /// <summary>
    /// 指定将文本约束为特定高度依赖属性
    /// </summary>
    public static readonly DependencyProperty MaxTextHeightProperty =
        DependencyProperty.Register("MaxTextHeight", typeof(double), typeof(OutlineText),
             new FrameworkPropertyMetadata(1000.0, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnOutlineTextInvalidated), null));

    /// <summary>
    /// 指定字体是否加粗。
    /// </summary>
    [Category("Text")]
    public bool Bold
    {
      get { return (bool)GetValue(BoldProperty); }
      set { SetValue(BoldProperty, value); }
    }

    /// <summary>
    /// 指定字体是否加粗依赖属性。
    /// </summary>
    public static readonly DependencyProperty BoldProperty = DependencyProperty.Register(
        "Bold", typeof(bool), typeof(OutlineText),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnOutlineTextInvalidated), null));

    /// <summary>
    /// 指定填充字体的画刷颜色。
    /// </summary>
    [Category("Brush")]
    public Brush Fill
    {
      get { return (Brush)GetValue(FillProperty); }
      set { SetValue(FillProperty, value); }
    }

    /// <summary>
    /// 指定填充字体的画刷颜色依赖属性。
    /// </summary>
    public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
        "Fill", typeof(Brush), typeof(OutlineText),
        new FrameworkPropertyMetadata(new SolidColorBrush(Colors.LightSteelBlue), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnOutlineTextInvalidated), null));

    /// <summary>
    /// 指定文字显示的字体。
    /// </summary>
    [Category("Text")]
    public FontFamily Font
    {
      get { return (FontFamily)GetValue(FontProperty); }
      set { SetValue(FontProperty, value); }
    }

    /// <summary>
    /// 指定文字显示的字体依赖属性。
    /// </summary>
    public static readonly DependencyProperty FontProperty = DependencyProperty.Register(
        "Font", typeof(FontFamily), typeof(OutlineText),
        new FrameworkPropertyMetadata(SystemFonts.MessageFontFamily, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnOutlineTextInvalidated), null));

    /// <summary>
    /// 指定字体大小。
    /// </summary>
    [Category("Text")]
    public double FontSize
    {
      get { return (double)GetValue(FontSizeProperty); }
      set { SetValue(FontSizeProperty, value); }
    }

    /// <summary>
    /// 指定字体大小依赖属性。
    /// </summary>
    public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
        "FontSize", typeof(double), typeof(OutlineText),
        new FrameworkPropertyMetadata(SystemFonts.MessageFontSize, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnOutlineTextInvalidated), null));

    /// <summary>
    /// 指定字体是否显示斜体字体样式。
    /// </summary>
    [Category("Text")]
    public bool Italic
    {
      get { return (bool)GetValue(ItalicProperty); }
      set { SetValue(ItalicProperty, value); }
    }

    /// <summary>
    /// 指定字体是否显示斜体字体样式依赖属性。
    /// </summary>
    public static readonly DependencyProperty ItalicProperty = DependencyProperty.Register(
        "Italic", typeof(bool), typeof(OutlineText),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnOutlineTextInvalidated), null));

    /// <summary>
    /// 指定绘制空心字体边框画刷的颜色。
    /// </summary>
    [Category("Text")]
    public Brush Stroke
    {
      get { return (Brush)GetValue(StrokeProperty); }
      set { SetValue(StrokeProperty, value); }
    }

    /// <summary>
    /// 指定绘制空心字体边框画刷的颜色依赖属性。
    /// </summary>
    public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
        "Stroke", typeof(Brush), typeof(OutlineText),
        new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Teal), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnOutlineTextInvalidated), null));

    /// <summary>
    /// 指定空心字体边框大小。
    /// </summary>
    [Category("Text")]
    public ushort StrokeThickness
    {
      get { return (ushort)GetValue(StrokeThicknessProperty); }
      set { SetValue(StrokeThicknessProperty, value); }
    }

    /// <summary>
    /// 指定空心字体边框大小依赖属性。
    /// </summary>
    public static readonly DependencyProperty StrokeThicknessProperty =
        DependencyProperty.Register("StrokeThickness",
        typeof(ushort), typeof(OutlineText),
        new FrameworkPropertyMetadata((ushort)0, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnOutlineTextInvalidated), null));

    /// <summary>
    /// 指定要显示的文字字符串。
    /// </summary>
    [Category("Common")]
    public string Text
    {
      get { return (string)GetValue(TextProperty); }
      set { SetValue(TextProperty, value); }
    }

    /// <summary>
    /// 指定要显示的文字字符串依赖属性。
    ///  </summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        "Text", typeof(string), typeof(OutlineText),
        new FrameworkPropertyMetadata("",
            FrameworkPropertyMetadataOptions.AffectsRender,
            new PropertyChangedCallback(OnOutlineTextInvalidated),
            null));

    #endregion DependencyProperties

    #region Public Methods

    /// <summary>
    /// 添加子对象。
    /// </summary>
    /// <param name="value">要添加的子对象。</param>
    public void AddChild(Object value)
    { }

    /// <summary>
    /// 将节点的文字内容添加到对象。
    /// </summary>
    /// <param name="value">要添加到对象的文字。</param>
    public void AddText(string value)
    {
      Text = value;
    }

    #endregion Public Methods
  }
}