using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 对 System.Collections.Generic.IEnumerable 的每个元素执行指定操作。
        /// 由于很可能因 Foreach 名称冲突，采用追加名。
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="source">System.Collections.Generic.IEnumerable </param>
        /// <param name="action">要对 System.Collections.Generic.IEnumerable  的每个元素执行的 System.Action 委托</param>
        public static void ForEachApply<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null && action != null)
            {
                foreach (var item in source)
                    action(item);
            }
        }

        /// <summary>
        /// 对 System.Collections.Generic.IEnumerable 的每个元素执行指定操作。
        /// 由于很可能因 Foreach 名称冲突，采用追加名。
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="source">System.Collections.Generic.IEnumerable </param>
        /// <param name="action">要对 System.Collections.Generic.IEnumerable  的每个元素执行的 System.Func 委托</param>
        public static async Task ForEachApplyAsync<T>(this IEnumerable<T> source, Func<T, Task> action)
        {
            if (source != null && action != null)
            {
                foreach (var item in source)
                    await action(item);
            }
        }

        /// <summary>
        /// 获取 System.Collections.Generic.IEnumerable 的第一个元素，并对其执行指定操作，如果无匹配项则不进行操作
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="source">System.Collections.Generic.IEnumerable </param>
        /// <param name="action">要对 System.Collections.Generic.IEnumerable  的第一个元素执行的 System.Action 委托</param>
        /// <returns>第一个item，可能会返回 null</returns>
        public static T ForFirst<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null && source.Any() && action != null)
            {
                T firstT = source.FirstOrDefault();
                if (null != firstT)
                {
                    action(firstT);
                }
                return firstT;
            }
            return default;
        }

        /// <summary>
        /// 获取 System.Collections.Generic.IEnumerable 的最后一个元素，并对其执行指定操作，如果无匹配项则不进行操作
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="source">System.Collections.Generic.IEnumerable </param>
        /// <param name="action">要对 System.Collections.Generic.IEnumerable  的最后一个元素执行的 System.Action 委托</param>
        /// <returns>最后一个item，可能会返回 null</returns>
        public static T ForLast<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null && source.Any() && action != null)
            {
                T lastT = source.LastOrDefault();
                if (null != lastT)
                {
                    action(lastT);
                }
                return lastT;
            }
            return default;
        }

        /// <summary>
        /// Invoke action for first element in source.
        /// </summary>
        public static async Task<T> ForFirstAsync<T>(this IEnumerable<T> source, Func<T, Task> action)
        {
            if (source != null && source.Any() && action != null)
            {
                T firstT = source.FirstOrDefault();
                if (firstT is not null)
                {
                    await action(firstT);
                }
                return firstT;
            }
            return default;
        }

        /// <summary>
        /// 获取 System.Collections.Generic.IEnumerable 的最后一个元素，并对其执行指定操作，如果无匹配项则不进行操作
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="source">System.Collections.Generic.IEnumerable </param>
        /// <param name="action">要对 System.Collections.Generic.IEnumerable  的最后一个元素执行的 System.Func 委托</param>
        /// <returns>最后一个item，可能会返回 null</returns>
        public static async Task<T> ForLastAsync<T>(this IEnumerable<T> source, Func<T, Task> action)
        {
            if (source != null && source.Any() && action != null)
            {
                T lastT = source.LastOrDefault();
                if (lastT is not null)
                {
                    await action(lastT);
                }
                return lastT;
            }
            return default;
        }

        /// <summary>
        /// 返回一个合并后的序列，用于临时快速的为列表补充某一些项。
        /// </summary>
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> source, params T[] items)
        {
            foreach (var js in source)
            {
                yield return js;
            }
            foreach (var ji in items)
            {
                yield return ji;
            }
        }

        /// <summary>
        /// 返回一个排除null引用的序列。
        /// </summary>
        public static IEnumerable<T> ExcludeNull<T>(this IEnumerable<T> source)
        {
            foreach (var js in source)
            {
                if (js != null)
                {
                    yield return js;
                }
            }
        }

        /// <summary>
        /// 对列表内的每个对象都执行指定的自定义清理操作，并清空列表。
        /// </summary>
        /// <param name="source">要清空的列表</param>
        /// <param name="destruction">要执行的自定义清理操作</param>
        /// <returns>清空的列表</returns>
        public static ICollection<T> ClearWith<T>(this ICollection<T> source, Action<T> destruction)
        {
            if (source is null) { throw new ArgumentNullException(nameof(source)); }
            if (destruction is null) { throw new ArgumentNullException(nameof(destruction)); }

            foreach (var ji in source)
            {
                destruction.Invoke(ji);
            }

            source.Clear();
            return source;
        }

        /// <summary>
        /// IsNullOrEmpty
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source is null || !source.Any();
        }

        /// <summary>
        /// HaveItems
        /// </summary>
        public static bool HaveItems<T>(this IEnumerable<T> source)
        {
            return source is not null && source.Any();
        }
    }
}