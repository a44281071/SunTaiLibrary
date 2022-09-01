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
    /// <summary>
    /// list box for diagram items in special canvas. 
    /// items can move and zoom in canvas panel.
    /// </summary>
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


        #region Item拖动自动对齐并显示标线
        /// <summary>
        /// Item拖动自动对齐并显示标线
        /// </summary>
        public bool ItemAlignToDrag
        {
            get { return (bool)GetValue(ItemAlignToDragProperty); }
            set { SetValue(ItemAlignToDragProperty, value); }
        }

        public static readonly DependencyProperty ItemAlignToDragProperty =
            DependencyProperty.Register("ItemAlignToDrag", typeof(bool), typeof(DiagramListBox), new PropertyMetadata(false));


        /// <summary>
        /// 自动对齐像素范围
        /// </summary>
        public double ItemAlignToDragScope
        {
            get { return (double)GetValue(ItemAlignToDragScopeProperty); }
            set { SetValue(ItemAlignToDragScopeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemAlignToDragScope.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemAlignToDragScopeProperty =
            DependencyProperty.Register("ItemAlignToDragScope", typeof(double), typeof(DiagramListBox), new PropertyMetadata(10d));


        #endregion

        /// <summary>
        /// 生成列表项控件
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DiagramListBoxItem();
        }
    }
}