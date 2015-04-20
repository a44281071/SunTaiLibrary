using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Linq.Expressions
{
    /// <summary>
    /// 扩展方法 for <see cref="Expression"/>.
    /// </summary>
    public static class ExtensionLinqExpresstion
    {
        /// <summary>
        /// 将lambda表达式树转换为 <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="expression">要转换的 lambda表达式树</param>
        /// <returns>有关成员特性的信息并提供对成员元数据的访问</returns>
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member;
        }
    }


}
