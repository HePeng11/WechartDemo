using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WeiXinMVC.Models.Dtos
{
    [XmlRoot("xml")]
    public class InMessage
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public long CreateTime { get; set; }
        public string MsgType { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
    }

    /// <summary>
    /// 文本消息
    /// </summary>
    [XmlRoot("xml")]
    public class InMessageText: InMessage
    {

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }

    }

    /// <summary>
    /// 图片消息
    /// </summary>
    [XmlRoot("xml")]
    public class InMessageImage : InMessage
    {
        /// <summary>
        /// 图片链接（由系统生成）
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

    }


    /// <summary>
    /// 事件消息
    /// </summary>
    [XmlRoot("xml")]
    public class InMessageEvent : InMessage
    {
        /// <summary>
        /// 事件类型，subscribe
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// 扫描信息
        /// </summary>
        public string ScanCodeInfo { get; set; }
        /// <summary>
        /// 扫描类型，一般是qrcode
        /// </summary>
        public string ScanType { get; set; }
        /// <summary>
        /// 扫描结果，即二维码对应的字符串信息
        /// </summary>
        public string ScanResult { get; set; }
    }

}