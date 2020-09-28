using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary
{
  public class ViewModelBase : INotifyPropertyChanged
  {
    protected virtual void NotifyOfPropertyChange(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual bool Set<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
    {
      if (!EqualityComparer<T>.Default.Equals(oldValue, newValue))
      {
        oldValue = newValue;
        NotifyOfPropertyChange(propertyName: propertyName);
        return true;
      }
      else
      {
        return false;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}