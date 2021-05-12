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
      if (action == null) throw new ArgumentNullException(nameof(action), "action 不能为空");

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
  }
}