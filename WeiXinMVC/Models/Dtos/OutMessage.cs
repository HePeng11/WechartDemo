using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace WeiXinMVC.Models.Dtos
{
    [XmlRoot("xml", Namespace = "")]
    [XmlInclude(typeof(OutMessageImage))]
    [XmlInclude(typeof(OutMessageText))]
    [XmlInclude(typeof(OutMessageNews))]
    public class OutMessage
    {
        [XmlIgnore]
        public string ToUserName { get; set; }
        [XmlIgnore]
        public string FromUserName { get; set; }
        public long CreateTime { get; set; }
        [XmlIgnore]
        public string MsgType { get; set; }


        #region


        [XmlElement("ToUserName")]
        public XmlNode[] CDataToUserName
        {
            get
            {
                return new XmlNode[] { new XmlDocument().CreateCDataSection(ToUserName) };
            }
            set
            {
                ToUserName = value[0].Value;
            }
        }
        [XmlElement("FromUserName")]
        public XmlNode[] CDataFromUserName
        {
            get
            {
                return new XmlNode[] { new XmlDocument().CreateCDataSection(FromUserName) };
            }
            set
            {
                FromUserName = value[0].Value;
            }
        }
        [XmlElement("MsgType")]
        public XmlNode[] CDataMsgType
        {
            get
            {
                return new XmlNode[] { new XmlDocument().CreateCDataSection(MsgType) };
            }
            set
            {
                MsgType = value[0].Value;
            }
        }
        #endregion


    }

    /// <summary>
    /// 文本消息
    /// </summary>
    [XmlRoot("xml", Namespace = "")]
    public class OutMessageText : OutMessage
    {
        public OutMessageText()
        {
            MsgType = "text";
        }
        /// <summary>
        /// 文本消息内容
        /// </summary>
        [XmlIgnore]
        public string Content { get; set; }
        [XmlElement("Content")]
        public XmlNode[] CDataContent
        {
            get
            {
                return new XmlNode[] { new XmlDocument().CreateCDataSection(Content) };
            }
            set
            {
                Content = value[0].Value;
            }
        }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    [XmlRoot("xml")]
    public class OutMessageImage : OutMessage
    {
        public OutMessageImage()
        {
            MsgType = "image";
        }
        /// <summary>
        /// 通过素材管理中的接口上传多媒体文件，得到的id。
        /// </summary>
        [XmlIgnore]
        public string MediaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Image")]
        public XmlNode[] CDataMediaId
        {
            get
            {
                var xml = new XmlDocument();
                var imgNode = xml.CreateNode(XmlNodeType.Element, "MediaId", "");
                imgNode.AppendChild(xml.CreateCDataSection(MediaId));
                return new XmlNode[] { imgNode };
            }
            set
            {
                MediaId = value[0].ChildNodes[0].Value;
            }
        }
    }


    /// <summary>
    /// 图文消息
    /// </summary>
    [XmlRoot("xml", Namespace = "")]
    public class OutMessageNews : OutMessage
    {
        public OutMessageNews()
        {
            MsgType = "news";
        }
        /// <summary>
        /// 图文消息个数；当用户发送文本、图片、视频、图文、地理位置这五种消息时，开发者只能回复1条图文消息；
        /// 其余场景最多可回复8条图文消息
        /// </summary>
        public int ArticleCount { get; set; }

        /// <summary>
        /// 图文消息信息，注意，如果图文数超过限制，则将只发限制内的条数
        /// </summary>
        public item[] Articles { get; set; }


        //[XmlElement("Articles")]
        //public string CDataArticles { get; set; }

        //public void SetCDataArticles()
        //{
        //    for (int i = 0; i < Articles.Length; i++)
        //    {
        //        CDataArticles += XmlSerializeUtil.Serializer(Articles[i]);
        //    }
        //}
    }

    /// <summary>
    /// 图文消息内容
    /// </summary>
    public class item
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        [XmlIgnore]
        public string Title { get; set; }
        /// <summary>
        /// 图文消息描述
        /// </summary>
        [XmlIgnore]
        public string Description { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        [XmlIgnore]
        public string PicUrl { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        [XmlIgnore]
        public string Url { get; set; }

        [XmlElement("Title")]
        public XmlNode[] CDataTitle
        {
            get
            {
                return new XmlNode[] { new XmlDocument().CreateCDataSection(Title) };
            }
            set
            {
                Title = value[0].Value;
            }
        }
        [XmlElement("Description")]
        public XmlNode[] CDataDescription
        {
            get
            {
                return new XmlNode[] { new XmlDocument().CreateCDataSection(Description) };
            }
            set
            {
                Description = value[0].Value;
            }
        }
        [XmlElement("PicUrl")]
        public XmlNode[] CDataPicUrl
        {
            get
            {
                return new XmlNode[] { new XmlDocument().CreateCDataSection(PicUrl) };
            }
            set
            {
                PicUrl = value[0].Value;
            }
        }
        [XmlElement("Url")]
        public XmlNode[] CDataUrl
        {
            get
            {
                return new XmlNode[] { new XmlDocument().CreateCDataSection(Url) };
            }
            set
            {
                Url = value[0].Value;
            }
        }
    }


}