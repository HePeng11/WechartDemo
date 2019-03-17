using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WeiXinMVC.Models
{
    /// <summary>  
    /// <remarks>Xml序列化与反序列化</remarks>  
    /// <creator>zhangdapeng</creator>  
    /// </summary>  
    public class XmlSerializeUtil
    {
        #region 反序列化  
        /// <summary>  
        /// 反序列化  
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="xml">XML字符串</param>  
        /// <returns></returns>  
        public static T Deserialize<T>(string xml) where T : class
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(typeof(T));
                return xmldes.Deserialize(sr) as T;
            }
        }
        /// <summary>  
        /// 反序列化  
        /// </summary>  
        /// <param name="type"></param>  
        /// <param name="xml"></param>  
        /// <returns></returns>  
        public static T Deserialize<T>(Stream stream) where T : class
        {
            XmlSerializer xmldes = new XmlSerializer(typeof(T));
            return xmldes.Deserialize(stream) as T;
        }
        #endregion

        #region 序列化  
        /// <summary>  
        /// 序列化  
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="obj">对象</param>  
        /// <returns></returns>  
        public static string Serializer<T>(T obj)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = " ",
                NewLineChars = "\r\n",
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true//省略xml声明
            };

            MemoryStream stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(typeof(T));

            using (XmlWriter xmlWriter = XmlWriter.Create(stream, settings))
            {
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);
                xml.Serialize(xmlWriter, obj, xns);
                xmlWriter.Close();
            }


            stream.Position = 0;
            var sr = new StreamReader(stream);
            var str = sr.ReadToEnd();

            sr.Dispose();
            stream.Dispose();


            return str;
        }

        #endregion
    }
}
