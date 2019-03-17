using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WeiXinMVC.Models
{
    public class Util
    {
        /// <summary>
        /// HttpContext.Current.Request.InputStream=>string
        /// </summary>
        /// <returns></returns>
        public static string GetInputStream() {
            Stream requestStream = HttpContext.Current.Request.InputStream;
            byte[] requestByte = new byte[requestStream.Length];
            requestStream.Read(requestByte, 0, (int)requestStream.Length);
            string requestStr = Encoding.UTF8.GetString(requestByte);
            return requestStr;
        }
    }
}