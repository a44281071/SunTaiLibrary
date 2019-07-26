using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SunTaiLibrary.Commands
{
  /// <summary>
  /// Contains commands for text box types.
  /// </summary>
  public static class TextCommands
  {
    private static ICommand _ClearTextBoxCommand;

    /// <summary>
    /// Gets the command that clears a <see cref="TextBox"/>.
    /// </summary>
    /// <value>
    /// The clear text command.
    /// </value>
    public static ICommand ClearTextBoxCommand
    {
      get
      {
        if (_ClearTextBoxCommand == null)
        {
          _ClearTextBoxCommand = new RelayCommand<TextBox>(box =>
          {
            if (box != null)
            {
              box.Clear();
              box.Focus();
            }
          }, box =>
          {
            return box != null && !box.IsReadOnly && !String.IsNullOrEmpty(box.Text);
          });
        }
        return _ClearTextBoxCommand;
      }
    }

    private static ICommand _ClearPasswordBoxCommand;

    /// <summary>
    /// Gets the command that clears a <see cref="PasswordBox"/>.
    /// </summary>
    /// <value>
    /// The clear text command.
    /// </value>
    public static ICommand ClearPasswordBoxCommand
    {
      get
      {
        if (_ClearPasswordBoxCommand == null)
        {
          _ClearPasswordBoxCommand = new RelayCommand<PasswordBox>(box =>
          {
            if (box != null)
            {
              box.Clear();
              box.Focus();
            }
          }, box =>
          {
            return box != null && box.SecurePassword.Length > 0;
          });
        }
        return _ClearPasswordBoxCommand;
      }
    }
  }
}