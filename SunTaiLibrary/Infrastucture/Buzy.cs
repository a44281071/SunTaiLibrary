using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary
{
    /// <summary>
    /// 表示这个对象带着一个忙状态标记
    /// </summary>
    public interface IHasBusyScope
    {
        /// <summary>
        /// 指示自己当前是否繁忙。
        /// 【true：繁忙状态】【false：空闲状态】
        /// </summary>
        public BusyScope Busy { get; }
    }

    /// <summary>
    /// 忙状态标记。
    /// </summary>
    public class BusyScope : INotifyPropertyChanged
    {
        bool _IsBusy = false;

        /// <summary>
        /// 指示自己当前是否繁忙。
        /// 【true：繁忙状态】【false：空闲状态】
        /// </summary>
        public bool IsBusy
        {
            get => _IsBusy;
            private set
            {
                if (value != _IsBusy)
                {
                    _IsBusy = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
                }
            }
        }

        /// <summary>
        /// INotifyPropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 获取一个忙域，这个域释放前，会一直处于忙状态。
        /// </summary>
        public IDisposable BeginScope() => new BusyLifeScope(this);

        private class BusyLifeScope : IDisposable
        {
            public BusyLifeScope(BusyScope busyScope)
            {
                busyScope.IsBusy = true;
                this.busyScope = new(busyScope);
            }

            private readonly WeakReference<BusyScope> busyScope;
            private bool disposedValue;

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        if (busyScope.TryGetTarget(out var scope))
                        {
                            scope.IsBusy = false;
                        }
                    }

                    disposedValue = true;
                }
            }

            //~BusyLifeScope()
            //{
            //    // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            //    Dispose(disposing: false);
            //}

            public void Dispose()
            {
                // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}