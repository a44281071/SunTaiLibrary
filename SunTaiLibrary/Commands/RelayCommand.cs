using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SunTaiLibrary.Commands
{
  /// <summary>
  /// Simple relay command for internal use only.
  /// </summary>
  public class RelayCommand : ICommand
  {
    #region Fields

    private readonly Action _Execute;
    private readonly Func<bool> _CanExecute;

    #endregion Fields

    #region Constructors

    public RelayCommand(Action execute)
        : this(execute, null)
    {
    }

    public RelayCommand(Action execute, Func<bool> canExecute)
    {
      _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
      _CanExecute = canExecute;
    }

    #endregion Constructors

    #region ICommand Members

    [DebuggerStepThrough]
    public bool CanExecute(object parameter)
    {
      return _CanExecute == null ? true : _CanExecute();
    }

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameter)
    {
      _Execute();
    }

    #endregion ICommand Members
  }

  /// <summary>
  /// Simple relay command for internal use only.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  internal class RelayCommand<T> : ICommand where T : class
  {
    #region Fields

    private readonly Action<T> _Execute;
    private readonly Predicate<T> _CanExecute;

    #endregion Fields

    #region Constructors

    public RelayCommand(Action<T> execute)
        : this(execute, null)
    {
    }

    public RelayCommand(Action<T> execute, Predicate<T> canExecute)
    {
      _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
      _CanExecute = canExecute;
    }

    #endregion Constructors

    #region ICommand Members

    [DebuggerStepThrough]
    public bool CanExecute(object parameter)
    {
      return _CanExecute == null ? true : _CanExecute(parameter as T);
    }

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameter)
    {
      _Execute(parameter as T);
    }

    #endregion ICommand Members
  }
}