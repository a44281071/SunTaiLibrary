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
  /// Simple relay command.
  /// </summary>
  public class RelayCommand : ICommand
  {
    /// <summary>
    /// Initializes a new instance of <see cref="RelayCommand"/>.
    /// </summary>
    /// <param name="execute">Delegate to execute when Execute is called on the command. </param>
    /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
    public RelayCommand(Action execute)
        : this(execute, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="RelayCommand"/>.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    public RelayCommand(Action execute, Func<bool> canExecute)
    {
      _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
      _CanExecute = canExecute;
    }

    private readonly Action _Execute;
    private readonly Func<bool> _CanExecute;

    #region ICommand Members

    ///<summary>
    ///Defines the method that determines whether the command can execute in its current state.
    ///</summary>
    ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    ///<returns>
    ///true if this command can be executed; otherwise, false.
    ///</returns>
    [DebuggerStepThrough]
    public bool CanExecute(object parameter)
    {
      return _CanExecute == null || _CanExecute();
    }

    ///<summary>
    ///Occurs when changes occur that affect whether or not the command should execute.
    ///</summary>
    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    ///<summary>
    ///Defines the method to be called when the command is invoked.
    ///</summary>
    ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
    public void Execute(object parameter)
    {
      _Execute();
    }

    #endregion ICommand Members
  }

  /// <summary>
  /// Simple relay command.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  internal class RelayCommand<T> : ICommand where T : class
  {
    /// <summary>
    /// Initializes a new instance of <see cref="RelayCommand{T}"/>.
    /// </summary>
    /// <param name="execute">Delegate to execute when Execute is called on the command. </param>
    /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>

    public RelayCommand(Action<T> execute)
        : this(execute, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="RelayCommand{T}"/>.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    public RelayCommand(Action<T> execute, Predicate<T> canExecute)
    {
      _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
      _CanExecute = canExecute;
    }

    private readonly Action<T> _Execute;
    private readonly Predicate<T> _CanExecute;

    #region ICommand Members

    ///<summary>
    ///Defines the method that determines whether the command can execute in its current state.
    ///</summary>
    ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    ///<returns>
    ///true if this command can be executed; otherwise, false.
    ///</returns>
    [DebuggerStepThrough]
    public bool CanExecute(object parameter)
    {
      return _CanExecute == null || _CanExecute(parameter as T);
    }

    ///<summary>
    ///Occurs when changes occur that affect whether or not the command should execute.
    ///</summary>
    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    ///<summary>
    ///Defines the method to be called when the command is invoked.
    ///</summary>
    ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>

    public void Execute(object parameter)
    {
      _Execute(parameter as T);
    }

    #endregion ICommand Members
  }
}