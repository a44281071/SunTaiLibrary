using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary.TestClient
{
  public class MainViewModel : ViewModelBase
  {
    public MainViewModel()
    {
      ChangePower = new RelayCommand<PowerState>(OnChangePower);

      // test not defined.
      EnumItems.Add((PowerState)233);      
    }

    #region Bind_Data

    private PowerState _Power = PowerState.On;

    /// <summary>
    /// 绑定通知，power state enum
    /// </summary>
    public PowerState Power { get => _Power; set => Set(ref _Power, value); } 
    public ObservableCollection<PowerState> EnumItems { get; } = new ObservableCollection<PowerState>(ExtensionEnum.ToArray<PowerState>());

    #endregion Bind_Data

    #region Bind_Command

    public RelayCommand<PowerState> ChangePower { get; }

    private async void OnChangePower(PowerState powerState)
    {
      await Task.Delay(1000);
      Power = powerState;
    }

    #endregion Bind_Command
  }
}