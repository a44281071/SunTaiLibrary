using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SunTaiLibrary.Controls
{
    /// <summary>
    /// Zoom Content by mouse.
    /// </summary>
    [TemplatePart(Name = PART_Content_Name, Type = typeof(ContentControl))]
    public class ZoomContentControl : ContentControl
    {
        static ZoomContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomContentControl), new FrameworkPropertyMetadata(typeof(ZoomContentControl)));
        }

        /// <summary>
        /// PART_Content_Name
        /// </summary>
        public const string PART_Content_Name = "PART_Content";

        private ContentPresenter part_content;

        /// <summary>
        /// Zoom is enable
        /// </summary>
        public bool IsZoom
        {
            get { return (bool)GetValue(IsZoomProperty); }
            set { SetValue(IsZoomProperty, value); }
        }

        /// <summary>
        /// Zoom is enable
        /// </summary>
        public static readonly DependencyProperty IsZoomProperty =
            DependencyProperty.Register("IsZoom", typeof(bool), typeof(ZoomContentControl)
                , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsZoomPropertyChanged)));

        private static void IsZoomPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ele = (ZoomContentControl)d;
            ContentPresenter ele_part_content = ele.part_content;
            if (ele_part_content == null) return;

            bool newValue = (bool)e.NewValue;
            if (newValue)
            {
                ele.CreateTransformGroup();
                ele.StartMouseZoom();
            }
            else
            {
                ele.ClearTransformGroup();
                ele.StopMouseZoom();
            }
        }

        private void CreateTransformGroup()
        {
            var transformGroup = new TransformGroup();
            var scaleTransform = new ScaleTransform();
            var translateTransform = new TranslateTransform();
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);
            part_content.RenderTransform = transformGroup;
        }

        private void ClearTransformGroup()
        {
            var group = part_content?.RenderTransform as TransformGroup;
            if (group != null)
            {
                var transform = group.Children[1] as TranslateTransform;
                var scaleTransform = group.Children[0] as ScaleTransform;
                scaleTransform.ScaleX = 1;
                scaleTransform.ScaleY = 1;
                transform.X = 0;
                transform.Y = 0;
            }
            part_content.RenderTransform = null;
        }

        private void StartMouseZoom()
        {
            MouseLeftButtonDown += ContentControl_MouseLeftButtonDown;
            MouseLeftButtonUp += ContentControl_MouseLeftButtonUp;
            MouseMove += ContentControl_MouseMove;
            MouseWheel += ContentControl_MouseWheel;
        }

        private void StopMouseZoom()
        {
            MouseLeftButtonDown -= ContentControl_MouseLeftButtonDown;
            MouseLeftButtonUp -= ContentControl_MouseLeftButtonUp;
            MouseMove -= ContentControl_MouseMove;
            MouseWheel -= ContentControl_MouseWheel;
        }

        #region MouseZoom

        private bool canUseMouse = true;
        private bool mouseDown = false;
        private Point mouseXY;

        //鼠标按下时的事件，启用捕获鼠标位置并把坐标赋值给mouseXY.
        private void ContentControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();
            mouseDown = true;
            mouseXY = e.GetPosition(this);
        }

        //鼠标松开时的事件，停止捕获鼠标位置。
        private void ContentControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
            mouseDown = false;
        }

        //鼠标移动时的事件，当鼠标按下并移动时发生Domousemove(img, e);函数
        private void ContentControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!canUseMouse) return;
            if (mouseDown)
            {
                Domousemove(e);
            }
        }

        //group.Children中的第二个是移动的函数
        //它根据X.Y的值来移动。并把当前鼠标位置赋值给mouseXY.
        private void Domousemove(MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            var group = part_content.RenderTransform as TransformGroup;
            var transform = group.Children[1] as TranslateTransform;
            var scaleTransform = group.Children[0] as ScaleTransform;
            var position = e.GetPosition(this);
            transform.X -= (mouseXY.X - position.X);
            transform.Y -= (mouseXY.Y - position.Y);
            var transformArea = new Point(-part_content.ActualWidth * (scaleTransform.ScaleX - 1), -part_content.ActualHeight * (scaleTransform.ScaleY - 1));
            //往右与往下移动禁止超出
            if (mouseXY.X - position.X < 0 && transform.X > 0)
                transform.X = 0;
            if (mouseXY.Y - position.Y < 0 && transform.Y > 0)
                transform.Y = 0;
            // 往左与往上移动禁止超出
            if (mouseXY.X - position.X > 0 && transform.X < transformArea.X)
                transform.X = transformArea.X;
            if (mouseXY.Y - position.Y > 0 && transform.Y < transformArea.Y)
                transform.Y = transformArea.Y;

            mouseXY = position;
        }

        //鼠标滑轮事件，得到坐标，放缩函数和滑轮指数，由于滑轮值变化较大所以*0.001.
        private void ContentControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null) { return; }
            if (!canUseMouse) return;
            var point = e.GetPosition(this);
            var group = part_content.RenderTransform as TransformGroup;
            var delta = e.Delta * 0.001;
            DowheelZoom(group, point, delta);
        }

        //Group.Children中的第一个是放缩函数。
        //如果ScaleX+滑轮指数小于0.1时就返回。
        //var pointToContent = group.Inverse.Transform(point);
        //获取此变换的逆变换的值
        //使图片放缩后，放缩原点也随之变化。
        private void DowheelZoom(TransformGroup group, Point point, double delta)
        {
            var pointToContent = group.Inverse.Transform(point);
            var scaleTransform = group.Children[0] as ScaleTransform;
            if (scaleTransform.ScaleX + delta < 0.1) return;
            scaleTransform.ScaleX += delta;
            scaleTransform.ScaleY += delta;

            if (scaleTransform.ScaleX < 1)
                scaleTransform.ScaleX = 1;
            if (scaleTransform.ScaleY < 1)
                scaleTransform.ScaleY = 1;

            var transform1 = group.Children[1] as TranslateTransform;
            transform1.X = -1 * ((pointToContent.X * scaleTransform.ScaleX) - point.X);
            transform1.Y = -1 * ((pointToContent.Y * scaleTransform.ScaleY) - point.Y);
            if (transform1.X > 0)
                transform1.X = 0;
            if (transform1.Y > 0)
                transform1.Y = 0;
        }

        #endregion MouseZoom

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            part_content = GetTemplateChild(PART_Content_Name) as ContentPresenter;
        }
    }
}