﻿using SunTaiLibrary.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SunTaiLibrary.TestClient
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            ChangePower = new RelayCommand<PowerState>(OnChangePower);
            BindCommandAction = new RelayCommand(OnBindCommandAction);

            // test not defined.
            //EnumItems.Add((PowerState)233);
        }

        #region Bind_Data

        private PowerState _Power = PowerState.On;

        /// <summary>
        /// 绑定通知，power state enum
        /// </summary>
        public PowerState Power { get => _Power; set => Set(ref _Power, value); }

        public ObservableCollection<PowerState> EnumItems { get; } = new ObservableCollection<PowerState>(ExtensionEnum.ToArray<PowerState>());

        public string SaveFolder { get; set; }

        #endregion Bind_Data

        #region Bind_Command

        public RelayCommand<PowerState> ChangePower { get; }

        private async void OnChangePower(PowerState powerState)
        {
            await Task.Delay(1000);
            Power = powerState;
        }

        public RelayCommand BindCommandAction { get; }

        private void OnBindCommandAction()
        {
            MessageToast.Show("OnBindCommandAction");
        }

        #endregion Bind_Command

        #region Bind_Method

        public void BindEventAction()
        {
            MessageToast.Show("BindEventAction");
        }

        public async Task CommandBindingExecuted()
        {
            MessageToast.Show("Ctrl+S CommandBindingExecuting...");
            await Task.Delay(1000);
            MessageToast.Show("Ctrl+S CommandBindingExecuted");
        }

        public void CommandBindingExecuted2()
        {
            MessageToast.Show("Delete CommandBindingExecuted");
        }

        #endregion Bind_Method

        #region ElementBinding

        public void InvokeF4Key()
        {
            MessageToast.Show("InvokeF4Key");
        }

        #endregion ElementBinding
    }
}