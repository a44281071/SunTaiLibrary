using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SunTaiLibrary.Controls
{
    public class DiagramListBox : ListBox
    {
        static DiagramListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramListBox), new FrameworkPropertyMetadata(typeof(DiagramListBox)));
        }

        #region 设计器宽高

        /// <summary>
        /// 固定画布大小，用于辅助计算显示大小和它的缩放比例。
        /// 默认值按照 1080p 计算（1920 X 1080）
        /// </summary>
        public double DiagramWidth
        {
            get => (double)GetValue(DiagramWidthProperty);
            set => SetValue(DiagramWidthProperty, value);
        }

        /// <summary>
        /// 固定画布大小，用于辅助计算显示大小和它的缩放比例。
        /// 默认值按照 1080p 计算（1920 X 1080）
        /// </summary>
        public static readonly DependencyProperty DiagramWidthProperty =
            DependencyProperty.Register("DiagramWidth", typeof(double), typeof(DiagramListBox), new PropertyMetadata(1920d));

        /// <summary>
        /// 固定画布大小，用于辅助计算显示大小和它的缩放比例。
        /// 默认值按照 1080p 计算（1920 X 1080）
        /// </summary>
        public double DiagramHeight
        {
            get => (double)GetValue(DiagramHeightProperty);
            set => SetValue(DiagramHeightProperty, value);
        }

        /// <summary>
        /// 固定画布大小，用于辅助计算显示大小和它的缩放比例。
        /// 默认值按照 1080p 计算（1920 X 1080）
        /// </summary>
        public static readonly DependencyProperty DiagramHeightProperty =
            DependencyProperty.Register("DiagramHeight", typeof(double), typeof(DiagramListBox), new PropertyMetadata(1080d));

        #endregion 设计器宽高

        /// <summary>
        /// 生成列表项控件
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DiagramListBoxItem();
        }
    }
}