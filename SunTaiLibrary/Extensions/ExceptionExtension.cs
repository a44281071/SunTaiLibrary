using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// 返回异常信息。包含内部错误信息。
        /// </summary>
        public static string ToMessageLines(this Exception exception)
        {
            if (exception == null) return "";

            Exception ex = exception;
            var content = new StringBuilder();
            do
            {
                content.AppendLine(ex.Message);
                ex = ex.InnerException;
            } while (ex != null);
            return content.ToString();
        }
    }
}