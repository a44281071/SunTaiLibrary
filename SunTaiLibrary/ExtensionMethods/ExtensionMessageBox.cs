using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows
{
    /// <summary>
    /// 为 System.Windows.MessageBox 相关的对象提供便捷方法
    /// </summary>
    public static class ExtensionMessageBox
    {
        /// <summary>
        /// 将 模态框 的返回结果，转换为 Boolean 值，方便进行判断
        /// <para>如果用户选择了 “确定”、“是”，会返回 True 值，其他情况返回 False</para>
        /// </summary>
        /// <param name="pResult">用户选择的值</param>
        /// <returns>用户选择的值（Boolean形式）</returns>
        public static bool ToBoolean(this MessageBoxResult pResult)
        {
            bool result = false;

            if (pResult == MessageBoxResult.OK || pResult == MessageBoxResult.Yes)
            {
                result = true;
            }

            return result;
        }
    }
}
