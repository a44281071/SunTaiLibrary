using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SunTaiLibrary.Commands
{
  public static class WindowCommands
  {
    private static ICommand Close_Window_Command;

    /// <summary>
    /// Gets the command that close a <see cref="Window"/>.
    /// </summary>
    /// <value>
    /// The clear text command.
    /// </value>
    public static ICommand CloseWindowCommand
    {
      get
      {
        if (Close_Window_Command == null)
        {
          Close_Window_Command = new RelayCommand<DependencyObject>(sender =>
          {
            if (sender != null)
            {
              Window.GetWindow(sender).Close();
            }
          }, sender =>
          {
            return sender != null && Window.GetWindow(sender) != null;
          });
        }
        return Close_Window_Command;
      }
    }

    private static ICommand MiniMize_Window_Command;

    /// <summary>
    /// Gets the command that minimize a <see cref="Window"/>.
    /// </summary>
    /// <value>
    /// The clear text command.
    /// </value>
    public static ICommand MiniMizeWindowCommand
    {
      get
      {
        if (MiniMize_Window_Command == null)
        {
          MiniMize_Window_Command = new RelayCommand<DependencyObject>(sender =>
          {
            if (sender != null)
            {
              Window.GetWindow(sender).WindowState = WindowState.Minimized;
            }
          }, sender =>
          {
            return sender != null && Window.GetWindow(sender) != null;
          });
        }
        return Close_Window_Command;
      }
    }

    private static ICommand Resize_Maximization_Window_Command;

    /// <summary>
    /// Gets the command that minimize a <see cref="Window"/>.
    /// </summary>
    /// <value>
    /// The clear text command.
    /// </value>
    public static ICommand ResizeMaximizationWindowCommand
    {
      get
      {
        if (Resize_Maximization_Window_Command == null)
        {
          Resize_Maximization_Window_Command = new RelayCommand<DependencyObject>(sender =>
          {
            if (sender != null)
            {
              Window win = Window.GetWindow(sender);
              if (win.WindowState == WindowState.Normal)
              {
                win.WindowState = WindowState.Maximized;
              }
              else
              {
                win.WindowState = WindowState.Normal;
              }
            }
          }, sender =>
          {
            return sender != null && Window.GetWindow(sender) != null;
          });
        }
        return Close_Window_Command;
      }
    }
  }
}