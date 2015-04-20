using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caliburn.Micro
{
    public static class ExtensionCaliburnMicro
    {
        /// <summary>
        /// 从 System.Collections.Generic.IEnumerable&lt;T&gt; 创建一个 Caliburn.Micro.BindableCollection&lt;T&gt;
        /// </summary>
        /// <typeparam name="T">source 中的元素的类型</typeparam>
        /// <param name="source">要从其创建 Caliburn.Micro.BindableCollection 的 System.Collections.Generic.IEnumerable</param>
        /// <returns>一个包含输入序列中元素的 Caliburn.Micro.BindableCollection</returns>
        public static BindableCollection<T> ToBindableCollection<T>(this IEnumerable<T> source)
        {
            return new BindableCollection<T>(source);
        }

    }
}
