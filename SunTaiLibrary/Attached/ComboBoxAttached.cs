using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SunTaiLibrary.Attached
{
  public static class ComboBoxAttached
  {
    public static bool GetIsFilterSearch(ComboBox obj)
    {
      return (bool)obj.GetValue(IsFilterSearchProperty);
    }

    public static void SetIsFilterSearch(ComboBox obj, bool value)
    {
      obj.SetValue(IsFilterSearchProperty, value);
    }

    /// <summary>
    /// 为 ComboBox 提供搜索筛选功能的支持
    /// <para>缺陷：只支持绑定到 “DisplayMemberPath” 属性，且只能解析单级，无法支持wpf的路径属性写法</para>
    /// </summary>
    public static readonly DependencyProperty IsFilterSearchProperty =
        DependencyProperty.RegisterAttached("IsFilterSearch", typeof(bool), typeof(PasswordAttached), new PropertyMetadata(false, IsFilterSearchChangedHandler));

    private static void IsFilterSearchChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var isFilterSearch = (bool)e.NewValue;

      if (d is ComboBox eleComboBox && isFilterSearch)
      {
        if (!String.IsNullOrWhiteSpace(eleComboBox.DisplayMemberPath))
        {
          eleComboBox.IsEditable = true;
          eleComboBox.IsTextSearchEnabled = false;
          eleComboBox.StaysOpenOnEdit = true;
        }

        if (isFilterSearch)
        {
          eleComboBox.KeyDown += ComboBox_KeyDown;
          eleComboBox.DropDownClosed += ComboBox_DropDownClosed;
        }
        else
        {
          eleComboBox.KeyDown -= ComboBox_KeyDown;
          eleComboBox.DropDownClosed -= ComboBox_DropDownClosed;
        }
      }
    }

    /// <summary>
    /// 输入字符，按下Enter，筛选符合条件的并展示结果
    /// </summary>
    static void ComboBox_KeyDown(object sender, KeyEventArgs e)
    {
      //var textBox = eleComboBox.Template.FindName("PART_EditableTextBox", eleComboBox) as TextBox;
      //if (null == eleComboBox || null == textBox) return;
      if (!(sender is ComboBox eleComboBox)) return;

      if (e.Key == Key.Enter)
      {
        var text = eleComboBox.Text;

        if (!String.IsNullOrWhiteSpace(text))
        {
          ICollectionView view = CollectionViewSource.GetDefaultView(eleComboBox.ItemsSource);

          if (null != view)
          {
            // 绑定数据筛选的条件

            #region ***view.Filter***

            view.Filter = item =>
            {
              bool resultItem = false;

              if (null != item)
              {
                string v = GetPropertyValue(item, eleComboBox.DisplayMemberPath);
                if (!String.IsNullOrWhiteSpace(v))
                {
                  resultItem = v.ToLower().Contains(eleComboBox.Text.ToLower());
                }
              }

              return resultItem;
            };

            #endregion ***view.Filter***

            eleComboBox.IsDropDownOpen = true;
          }
        }
      }
    }

    /// <summary>
    /// 当展示结果关闭时，初始化列表，防止被筛选的数据回不来了
    /// </summary>
    static void ComboBox_DropDownClosed(object sender, EventArgs e)
    {
      var eleComboBox = sender as ComboBox;

      ICollectionView view = CollectionViewSource.GetDefaultView(eleComboBox.ItemsSource);

      if (null != view)
      {
        view.Filter = item => true;
      }
    }

    #region 内部方法

    private static string GetPropertyValue(object pObj, string propertyName)
    {
      string result = String.Empty;

      if (!String.IsNullOrWhiteSpace(propertyName))
      {
        var propInfo = pObj.GetType().GetProperty(propertyName);
        if (null != propInfo)
        {
          result = (propInfo.GetValue(pObj, null) ?? String.Empty).ToString();
        }
      }

      return result;
    }

    #endregion 内部方法
  }
}