using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 为 System.String 集合扩展的方法
    /// <para> 2014-07-29 周治 </para>
    /// <para> 命名空间为 System </para>
    /// </summary>
    public static class ExtensionString
    {
        /// <summary>
        /// 移除字符串中所有的空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveAllSpace(this String str)
        {
            return str.Replace(" ", "");
        }

        /// <summary>
        /// 返回一个值，该值指示指定的 System.String 对象是否出现在此字符串中
        /// <para>忽略字符串的大小写</para>
        /// </summary>
        /// <param name="str">字符串，可以是任意值</param>
        /// <param name="pString">要检索的内容</param>
        public static bool ContainsIgnoreCase(this String str, String value)
        {
            if (String.IsNullOrWhiteSpace(value)) return false;

            return str.ToLower().Contains(value.ToLower());
        }

    }
}
