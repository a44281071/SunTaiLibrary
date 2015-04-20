using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    /// <summary>
    /// 为 System.Collections.Generic.IEnumerable 集合扩展的方法
    /// <para> 2014-07-29 周治 </para>
    /// <para> 命名空间为 System.Linq </para>
    /// </summary>
    public static class ExtensionIEnumerable
    {
        /// <summary>
        /// 对 System.Collections.Generic.IEnumerable 的每个元素执行指定操作
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="source">System.Collections.Generic.IEnumerable </param>
        /// <param name="action">要对 System.Collections.Generic.IEnumerable  的每个元素执行的 System.Action 委托</param>
        /// <exception cref="System.ArgumentNullException">action 参数为 null</exception>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null) throw new ArgumentNullException("action 不能为空");

            foreach (var item in source)
                action(item);

            return source;
        }

        /// <summary>
        /// 获取 System.Collections.Generic.IEnumerable 的第一个元素，并对其执行指定操作，如果无匹配项则不进行操作
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="source">System.Collections.Generic.IEnumerable </param>
        /// <param name="action">要对 System.Collections.Generic.IEnumerable  的第一个元素执行的 System.Action 委托</param>
        /// <exception cref="System.ArgumentNullException">action 参数为 null</exception>
        /// <returns>第一个item，可能会返回 null</returns>
        public static T ForFirst<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException("action 不能为空");

            T firstT = source.FirstOrDefault();
            if (null != firstT)
            {
                action(firstT);
            }
            return firstT;
        }

        /// <summary>
        /// 获取 System.Collections.Generic.IEnumerable 的最后一个元素，并对其执行指定操作，如果无匹配项则不进行操作
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="source">System.Collections.Generic.IEnumerable </param>
        /// <param name="action">要对 System.Collections.Generic.IEnumerable  的最后一个元素执行的 System.Action 委托</param>
        /// <exception cref="System.ArgumentNullException">action 参数为 null</exception>
        /// <returns>最后一个item，可能会返回 null</returns>
        public static T ForLast<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException("action 不能为空");

            T lastT = source.LastOrDefault();
            if (null != lastT)
            {
                action(lastT);
            }
            return lastT;
        }

    }
}