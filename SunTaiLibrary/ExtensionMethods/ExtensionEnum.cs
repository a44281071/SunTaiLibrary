using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// 为 System.Enum 枚举扩展的方法
    /// <para> 2014-10-30 周治 </para>
    /// <para> 命名空间为 System </para>
    /// </summary>
    public static class ExtensionEnum
    {
        /// <summary>
        /// 获取枚举类子项描述信息（DescriptionAttribute），一个参数表示返回失败结果的类型
        /// </summary> 
        /// <param name="enumSubitem">一个枚举项常量</param>
        /// <param name="isGetFallbackValue">true：如果获取不到则返回枚举项的定义名称。false：返回 String.Empty</param>
        public static string GetEnumDescription(this Enum enumSubitem, bool isGetFallbackValue = true)
        {
            string strValue = enumSubitem.ToString();
            string result = isGetFallbackValue ? strValue : String.Empty;

            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
            Object[] da = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (null != da && da.Length > 0)
            {
                var da1 = (DescriptionAttribute)da[0];
                result = da1.Description;
            }

            return result;
        }
    }
}
