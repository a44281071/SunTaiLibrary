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

        /// <summary>
        /// 需要使用上一项值进行转换，使用类似于 Rx 的 Pairwise 操作符。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector">【TSource?：prev，TSource current】</param>
        public static IEnumerable<TResult> Pairwise<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource?, TSource, TResult> selector)
        {
            using var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
                yield break;

            var previous = enumerator.Current;
            var isFirst = true;

            // 处理第一项（特殊情况）
            if (isFirst)
            {
                yield return selector(default, previous);
                isFirst = false;
            }

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                yield return selector(previous, current);
                previous = current;
            }
        }

        /// <summary>
        /// 根据相邻两项是否属于同一组的判断条件，将列表分段。
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源列表</param>
        /// <param name="isSameGroup">判断当前项和前一项是否属于同一组的委托。参数：(previous, current)</param>
        /// <returns>分段后的列表，每个分段是一个 List&lt;T&gt;</returns>
        public static IEnumerable<List<T>> GroupByAdjacent<T>(
            this IEnumerable<T> source,
            Func<T, T, bool> isSameGroup)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (isSameGroup == null)
                throw new ArgumentNullException(nameof(isSameGroup));

            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                yield break; // 空序列

            var currentGroup = new List<T> { enumerator.Current };

            while (enumerator.MoveNext())
            {
                var previous = currentGroup[^1]; // 当前组最后一个元素即为前一项
                var current = enumerator.Current;

                if (isSameGroup(previous, current))
                {
                    currentGroup.Add(current);
                }
                else
                {
                    yield return currentGroup;
                    currentGroup = [current];
                }
            }

            if (currentGroup.Count > 0)
                yield return currentGroup;
        }

        /// <summary>
        /// 获取指定项在列表中左右相邻的元素（如果存在）。
        /// 返回一个包含至多两个元素的列表：[左侧, 右侧]（如果存在）。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">源列表</param>
        /// <param name="item">要查找的项</param>
        /// <param name="comparer">可选的相等比较器，默认使用 EqualityComparer&lt;T&gt;.Default</param>
        /// <returns>包含相邻项的列表（0~2 个元素）</returns>
        public static List<T> GetAdjacentItems<T>(
            this IList<T> list,
            T item,
            IEqualityComparer<T>? comparer = null)
        {
            if (list == null) return [];

            comparer ??= EqualityComparer<T>.Default;

            // 查找第一个匹配项的索引
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (comparer.Equals(list[i], item))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                return []; // 未找到项

            var result = new List<T>(2);

            // 添加左侧（如果存在）
            if (index > 0)
                result.Add(list[index - 1]);

            // 添加右侧（如果存在）
            if (index < list.Count - 1)
                result.Add(list[index + 1]);

            return result;
        }

        /// <summary>
        /// 根据指定索引，获取列表中左右相邻的元素（如果存在）。
        /// 返回一个包含至多两个元素的列表：[左侧, 右侧]（如果存在）。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">源列表</param>
        /// <param name="index">目标索引</param>
        /// <returns>包含相邻项的列表（0~2 个元素）</returns>
        public static List<T> GetAdjacentItemsByIndex<T>(this IList<T> list, int index)
        {
            if (list == null) return [];
            if (index < 0 || index >= list.Count) return [];

            var result = new List<T>(2);

            // 左侧存在？
            if (index > 0)
                result.Add(list[index - 1]);

            // 右侧存在？
            if (index < list.Count - 1)
                result.Add(list[index + 1]);

            return result;
        }
    }
}