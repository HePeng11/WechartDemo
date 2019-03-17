using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WeiXinMVC.Models
{
    public class WeixinUtil
    {
        private const string ValidateToken = "6F654275";

        /// <summary>
        /// 验证调用是否来自微信
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static bool Validate(string signature, string timestamp, string nonce)
        {
            var list = new List<string>() { ValidateToken, timestamp, nonce }.OrderBy(f => f);
            return SHA1_Encrypt(string.Join("", list)).ToLower().Equals(signature.ToLower());
        }

        /// <summary>
        /// 对字符串进行SHA1加密
        /// </summary>
        /// <param name="strIN">需要加密的字符串</param>
        /// <returns>密文</returns>
        public static string SHA1_Encrypt(string source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }
    }
}