using SunTaiLibrary.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace System.Windows
{

    /// <summary>
    /// 通过UI线程，在当前活动窗体下方弹出通知消息的浮动框
    /// </summary>
    public class MessagePanel
    {

        /// <summary>
        /// 通过UI线程，在当前窗体显示 “弹出消息”，默认 10 秒后自动消失
        /// <para>如果 自动消失时间 小于等于 0，则不会自动消失，只能通过鼠标点击隐藏</para>
        /// </summary>
        /// <param name="pMessage">要显示的内容，会转化为字符信息</param>
        /// <param name="pHideMessageTime">自动消失时间，默认 10 秒</param>
        public static void Show(object pMessage, double pHideMessageTime = 10d)
        {
            if (null == pMessage) return;

            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    MyMessagePanelControl.Show(pMessage, pHideMessageTime);
                }), null);
        }

    }

}


namespace SunTaiLibrary.Controls
{
    //2014年10月25日16:50:47 【三台】
    /// <summary>
    /// 用于在当前活动窗体下方弹出通知消息的展示内容，
    /// 请使用 Show(消息内容) 调用显示
    /// </summary>
    public partial class MyMessagePanelControl : UserControl
    {
        internal MyMessagePanelControl()
        {
            InitializeComponent();
            InitializeTimer();
        }

        DispatcherTimer timer = new DispatcherTimer();


        private static double _HideTime = 10d;
        /// <summary>
        /// 获取弹出消息的自动消失时间
        /// </summary>
        public static double HideTime
        {
            get { return _HideTime; }
            private set { _HideTime = value; }
        }


        private void InitializeTimer()
        {
            if (HideTime < 0) return;

            timer.Interval = TimeSpan.FromSeconds(HideTime);
            timer.Tick += (ss, ee) =>
            {
                HidePanel();
            };
        }

        /// <summary>
        /// 鼠标点击时，飞出并隐藏元素
        /// </summary>
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HidePanel();
        }


        /// <summary>
        /// 一个消息提示层完全消失后，将自动销毁这个对象
        /// </summary>
        private void HideRemoveStoryboard_Completed(object sender, EventArgs e)
        {
            var panel = Parent as System.Windows.Controls.Panel;

            if (null != panel)
            {
                panel.Children.Remove(this);
            }
        }

        /// <summary>
        /// 动画式飞出并隐藏该控件
        /// </summary>
        public void HidePanel()
        {
            Storyboard sb_Click = this.TryFindResource("sr_HideRemoveAnime") as Storyboard;

            // 飞出消失
            if (null != sb_Click)
            {
                sb_Click.Begin();
            }
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
        }

        /// <summary>
        /// 动画式飞入该控件
        /// </summary>
        public void ShowPanel()
        {
            Storyboard sb_Show = this.TryFindResource("sr_NewVisiableAnime") as Storyboard;
            if (null != sb_Show)
            {
                sb_Show.Begin();
            }

            //倒计时隐藏，如果 自动消失时间小于 0，则不会自动消失
            if (HideTime > 0)
            {
                timer.Start();
            }
        }


        /// <summary>
        /// 在当前窗体显示 “弹出消息”，默认 10 秒后自动消失
        /// <para>如果 自动消失时间 小于等于 0，则不会自动消失，只能通过鼠标点击隐藏</para>
        /// </summary>
        /// <param name="pMessage">要显示的内容，会转化为字符信息</param>
        /// <param name="pHideMessageTime">自动消失时间，默认 10 秒</param>
        internal static void Show(object pMessage, double pHideMessageTime = 10d)
        {

            if (HideTime != pHideMessageTime)
            {
                HideTime = pHideMessageTime;
            }

            foreach (Window iWin in Application.Current.Windows)
            {
                if (iWin.IsActive)
                {
                    //支持多级Child的容器
                    var panel = iWin.Content as System.Windows.Controls.Panel;

                    //其他类型的容器（不支持多级Child）
                    if (null == panel)
                    {
                        //改为嵌套一个支持多级Child的容器
                        var oldContent = iWin.Content as UIElement;
                        iWin.Content = null;
                        Grid grid = new Grid();
                        grid.Children.Add(oldContent);
                        iWin.Content = grid;
                        panel = grid;
                    }

                    /* //测试时方法
                    string asdf = "";
                    foreach (UIElement iEle in panel.Children)
                    {
                        asdf += iEle.GetType().Name + "   ";
                    }
                    MessageBox.Show(asdf);
                    */

                    // 手动 隐藏旧的提示
                    if (panel.Children.Count > 0)
                    {
                        var oldMsg = panel.Children[panel.Children.Count - 1] as MyMessagePanelControl;
                        if (null != oldMsg)
                        {
                            //oldMsg.HidePanel();
                            oldMsg.Visibility = Visibility.Collapsed;
                        }
                    }

                    // 重新显示提示信息
                    MyMessagePanelControl messagePanel = new MyMessagePanelControl();
                    messagePanel.txt_message.Text = pMessage.ToString();
                    panel.Children.Add(messagePanel);
                    Grid.SetColumnSpan(messagePanel, 1000);
                    Grid.SetRowSpan(messagePanel, 1000);
                    messagePanel.ShowPanel();
                }
            }
        }

    }

}
