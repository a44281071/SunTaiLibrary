using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SunTaiLibrary
{
  public static class LocalHelper
  {
    /// <summary>
    /// 弹出保存文件窗体并返回文件保存路径。
    /// </summary>
    /// <returns>文件保存路径</returns>
    public static string DialogSaveFile()
    {
      string result = null;
      var dialog = new Microsoft.Win32.SaveFileDialog();
      if (dialog.ShowDialog() == true)
      {
        result = dialog.FileName;
      }
      return result;
    }

    /// <summary>
    /// 弹出选择文件窗体并返回单一的文件路径。
    /// </summary>
    /// <param name="filter">筛选器</param>
    /// <returns>选中的文件路径。</returns>
    public static string DialogOpenOneFile(string filter = null)
    {
      string result = null;
      var dialog = new Microsoft.Win32.OpenFileDialog()
      {
        Multiselect = false
      };
      if (!String.IsNullOrWhiteSpace(filter))
      {
        dialog.Filter = filter;
      }
      if (dialog.ShowDialog() == true)
      {
        result = dialog.FileName;
      }
      return result;
    }

    /// <summary>
    /// 弹出选择文件窗体并返回多个文件路径。
    /// </summary>
    /// <param name="filter">筛选器</param>
    /// <returns>选中的文件路径。</returns>
    public static IList<string> DialogOpenMultiFile(string filter = null)
    {
      var result = new List<string>();
      var dialog = new Microsoft.Win32.OpenFileDialog()
      {
        Multiselect = true
      };
      if (!String.IsNullOrWhiteSpace(filter))
      {
        dialog.Filter = filter;
      }
      if (dialog.ShowDialog() == true)
      {
        result.AddRange(dialog.FileNames);
      }
      return result;
    }

    /// <summary>
    /// 生成指定的临时文件，并返回该文件的操作流。
    /// </summary>
    public static Stream GetTempFileStream()
    {
      string result = Path.GetTempFileName();
      return new FileStream(result, FileMode.Open);
    }

    /// <summary>
    /// 将指定的文件，复制一份放在临时目录。
    /// </summary>
    /// <param name="sourceFile">要备份复制的文件。</param>
    /// <returns>临时目录中的备份复制文件。</returns>
    public static string CopyFileToTemp(string sourceFile)
    {
      string result = Path.GetTempFileName();
      using (var rs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
      using (var ws = new FileStream(result, FileMode.Open))
      {
        rs.CopyTo(ws);
      }
      return result;
    }

    /// <summary>
    /// 将指定的文件流，复制一份放在临时目录。
    /// </summary>
    /// <param name="sourceFile">要备份复制的文件。</param>
    /// <returns>临时目录中的备份复制文件。</returns>
    public static string CopyFileToTemp(Stream sourceStream)
    {
      string result = Path.GetTempFileName();
      using (var ws = new FileStream(result, FileMode.Open))
      {
        sourceStream.CopyTo(ws);
      }
      return result;
    }

    /// <summary>
    /// copy a file async.
    /// </summary>
    public static async Task CopyToAsync(string source, string destination)
    {
      using FileStream sourceStream = File.Open(source, FileMode.Open);
      using FileStream destinationStream = File.Create(destination);
      await sourceStream.CopyToAsync(destinationStream);
    }
  }
}