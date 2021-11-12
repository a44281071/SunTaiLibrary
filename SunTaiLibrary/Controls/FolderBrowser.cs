using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SunTaiLibrary.Controls
{
    /// <summary>
    /// 用于确定一个目录路径，一般用于配置保存位置。
    /// </summary>
    [TemplatePart(Name = PART_BrowseButton_Name, Type = typeof(Button))]
    public class FolderBrowser : Control
    {
        static FolderBrowser()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FolderBrowser), new FrameworkPropertyMetadata(typeof(FolderBrowser)));
        }

        private const string PART_BrowseButton_Name = "PART_BrowseButton";
        private static string last_selected_path = null;

        #region 组件

        private Button browseButton;

        /// <summary>
        /// 模板定义的浏览按钮
        /// </summary>
        public Button BrowseButton
        {
            get { return browseButton; }
            set
            {
                if (browseButton != null)
                {
                    browseButton.Click -= new RoutedEventHandler(BrowseButtonElement_Click);
                }
                browseButton = value;
                if (browseButton != null)
                {
                    browseButton.Click += new RoutedEventHandler(BrowseButtonElement_Click);
                }
            }
        }

        /// <summary>
        /// 点击浏览按钮后，确认一个目录位置，并保持选择的目录。
        /// </summary>
        private void BrowseButtonElement_Click(object sender, RoutedEventArgs e)
        {
            string currentFolder = !String.IsNullOrWhiteSpace(Text) && Directory.Exists(Text)
                ? Text
                : last_selected_path;

            using System.Windows.Forms.FolderBrowserDialog dialog = new()
            {
                SelectedPath = currentFolder,
                ShowNewFolderButton = true
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Text = dialog.SelectedPath;
                last_selected_path = dialog.SelectedPath;
            }
        }

        #endregion 组件

        #region 依赖属性

        /// <summary>
        /// 显示当前文件路径
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// 显示当前文件路径
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FolderBrowser)
                , new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 【false：默认值，可以手动修改文本框内容】【true：不可以修改文本框内容，只能通过选择目录操作】
        /// </summary>
        public bool IsReadonly
        {
            get { return (bool)GetValue(IsReadonlyProperty); }
            set { SetValue(IsReadonlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadonlyProperty =
            DependencyProperty.Register("IsReadonly", typeof(bool), typeof(FolderBrowser), new PropertyMetadata(false));


        #endregion 依赖属性

        public override void OnApplyTemplate()
        {
            BrowseButton = GetTemplateChild(PART_BrowseButton_Name) as Button;
            base.OnApplyTemplate();
        }
    }
}