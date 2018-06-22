using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SunTaiLibrary.Controls
{
  [TemplatePart(Name = "brush1", Type = typeof(ImageBrush))]
  [TemplatePart(Name = "brush2", Type = typeof(ImageBrush))]
  [TemplatePart(Name = "brush3", Type = typeof(ImageBrush))]
  [TemplatePart(Name = "brush4", Type = typeof(ImageBrush))]
  [TemplatePart(Name = "brush5", Type = typeof(ImageBrush))]
  [TemplatePart(Name = "brush6", Type = typeof(ImageBrush))]
  [TemplatePart(Name = "brush7", Type = typeof(ImageBrush))]
  [TemplatePart(Name = "brush8", Type = typeof(ImageBrush))]
  [TemplatePart(Name = "brush9", Type = typeof(ImageBrush))]
  [TemplatePart(Name = PART_topleft, Type = typeof(Rectangle))]
  [TemplatePart(Name = PART_midleft, Type = typeof(Rectangle))]
  [TemplatePart(Name = PART_topmid, Type = typeof(Rectangle))]
  [TemplatePart(Name = PART_bottomright, Type = typeof(Rectangle))]
  public class NineGridImage : Control
  {
    private const string PART_topleft = "PART_topleft";
    private const string PART_midleft = "PART_midleft";
    private const string PART_topmid = "PART_topmid";
    private const string PART_bottomright = "PART_bottomright";

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(ImageSource), typeof(NineGridImage), new UIPropertyMetadata(null, PropertyChanged));

    public static readonly DependencyProperty OffsetsProperty =
        DependencyProperty.Register("Offsets", typeof(Thickness), typeof(NineGridImage), new PropertyMetadata(new Thickness(), PropertyChanged));

    private readonly ImageBrush[] image = new ImageBrush[9];
    private Rectangle topleft, midleft, topmid, bottomright;

    static NineGridImage()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(NineGridImage), new FrameworkPropertyMetadata(typeof(NineGridImage)));
    }

    public NineGridImage()
    {
      this.SizeChanged += NineGridImage_SizeChanged;
    }

    private void NineGridImage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      this.UpdateNines();
      this.UpdateSizes();
    }

    [Category("Common")]
    public ImageSource Source
    {
      get { return (ImageSource)GetValue(SourceProperty); }
      set { SetValue(SourceProperty, value); }
    }

    [Category("Common")]
    public Thickness Offsets
    {
      get { return (Thickness)GetValue(OffsetsProperty); }
      [TypeConverter(typeof(ThicknessConverter))]
      set { SetValue(OffsetsProperty, value); }
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      for (int i = 0; i < 9; i++)
      {
        image[i] = GetTemplateChild("brush" + (i + 1)) as ImageBrush;
      }
      this.topleft = GetTemplateChild(PART_topleft) as Rectangle;
      this.topmid = GetTemplateChild(PART_topmid) as Rectangle;
      this.midleft = GetTemplateChild(PART_midleft) as Rectangle;
      this.bottomright = GetTemplateChild(PART_bottomright) as Rectangle;

      this.UpdateSizes();
      this.UpdateNines();
    }

    public static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs a)
    {
      var ng = sender as NineGridImage;
      ng.MinWidth = ng.Offsets.Left + ng.Offsets.Right;
      ng.MinHeight = ng.Offsets.Bottom + ng.Offsets.Top;
      ng.UpdateNines();
    }

    public void UpdateNines()
    {
      if (this.image.Any(i => i == null) || this.Source == null)
      {
        return;
      }

      double left = this.Offsets.Left / this.Source.Width;
      if (left > 1) left = 1;
      double right = this.Offsets.Right / this.Source.Width;
      if (right > 1) right = 1;
      double top = this.Offsets.Top / this.Source.Height;
      if (top > 1) top = 1;
      double bottom = this.Offsets.Bottom / this.Source.Height;
      if (bottom > 1) bottom = 1;

      double hmid = 1 - left - right;
      if (hmid < 0) hmid = 0;
      double vmid = 1 - bottom - top;
      if (vmid < 0) vmid = 0;

      this.image[0].Viewbox = new Rect(0, 0, left, top);
      this.image[1].Viewbox = new Rect(left, 0, hmid, top);
      this.image[2].Viewbox = new Rect(1 - right, 0, right, top);

      this.image[3].Viewbox = new Rect(0, top, left, vmid);
      this.image[4].Viewbox = new Rect(left, top, hmid, vmid);
      this.image[5].Viewbox = new Rect(1 - right, top, right, vmid);

      this.image[6].Viewbox = new Rect(0, 1 - bottom, left, bottom);
      this.image[7].Viewbox = new Rect(left, 1 - bottom, hmid, bottom);
      this.image[8].Viewbox = new Rect(1 - right, 1 - bottom, right, bottom);
    }

    private void UpdateSizes()
    {
      if (this.topleft == null || this.bottomright == null || this.topmid == null || this.midleft == null)
        return;

      this.topleft.Width = this.Offsets.Left;
      this.topleft.Height = this.Offsets.Top;

      this.bottomright.Width = this.Offsets.Right;
      this.bottomright.Height = this.Offsets.Bottom;

      this.topmid.Width = Math.Max(this.ActualWidth - this.Offsets.Left - this.Offsets.Right, 0);

      this.midleft.Height = Math.Max(this.ActualHeight - this.Offsets.Bottom - this.Offsets.Top, 0);

      this.MinWidth = this.topleft.Width + this.bottomright.Width;
      this.MinHeight = this.topleft.Height + this.bottomright.Height;
    }
  }
}