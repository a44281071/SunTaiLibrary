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
    public interface IHasBuzyScope
    {
        /// <summary>
        /// 指示自己当前是否繁忙。
        /// 【true：繁忙状态】【false：空闲状态】
        /// </summary>
        public BuzyScope Buzy { get; }
    }

    /// <summary>
    /// 忙状态标记。
    /// </summary>
    public class BuzyScope : INotifyPropertyChanged
    {
        bool _IsBuzy = false;

        /// <summary>
        /// 指示自己当前是否繁忙。
        /// 【true：繁忙状态】【false：空闲状态】
        /// </summary>
        public bool IsBuzy
        {
            get => _IsBuzy;
            private set
            {
                if (value != _IsBuzy)
                {
                    _IsBuzy = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBuzy)));
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
        public IDisposable BeginScope() => new BuzyLifeScope(this);

        private class BuzyLifeScope : IDisposable
        {
            public BuzyLifeScope(BuzyScope buzyScope)
            {
                buzyScope.IsBuzy = true;
                this.buzyScope = new(buzyScope);
            }

            private readonly WeakReference<BuzyScope> buzyScope;
            private bool disposedValue;

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        if (buzyScope.TryGetTarget(out var scope))
                        {
                            scope.IsBuzy = false;
                        }
                    }

                    disposedValue = true;
                }
            }

            //~BuzyLifeScope()
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