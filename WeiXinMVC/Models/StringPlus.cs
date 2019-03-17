using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
    public static class StringPlus
    {
        /// <summary>
        /// toNotNullString 转换为非空字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToNNString(this string str)
        {
            return str + "";
        }
    }
}