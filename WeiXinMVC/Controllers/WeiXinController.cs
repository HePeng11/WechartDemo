using Newtonsoft.Json;
using RestSharp;
using System;
using System.Web.Mvc;
using System.Xml;
using Weixin.UnitTest;
using Weixin.UnitTest.OutDtos;
using WeiXinMVC.Models;
using WeiXinMVC.Models.Dtos;

namespace WeiXinMVC.Controllers
{
    public class WeiXinController : Controller
    {
        /// <summary>
        /// 微信接入连接验证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Weichart")]
        public string Validate(string signature, string timestamp, string nonce, string echostr)
        {
            return WeixinUtil.Validate(signature.ToNNString(), timestamp.ToNNString(), nonce.ToNNString()) ? echostr : "验证失败";
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [ValideFilter]
        [HttpPost]
        [Route("Weichart")]
        public string ReceiveMessage()
        {
            string requestStr = Util.GetInputStream();
            var inMessage = XmlSerializeUtil.Deserialize<InMessage>(requestStr);
            OutMessage outMessage = null;
            switch (inMessage.MsgType)
            {
                case "text":
                    outMessage = HandleTextMsg(requestStr);
                    break;
                case "image":
                    outMessage = HandleImageMsg(requestStr);
                    break;
                case "event":
                    outMessage = HandleEventMsg(requestStr);
                    break;
                case "voice":

                    break;
                case "video":

                    break;
                case "shortvideo":

                    break;
                case "location":

                    break;
                case "link":

                    break;
                default:
                    break;
            }
            if (outMessage == null)
            {
                return null;
            }
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(XmlSerializeUtil.Serializer(outMessage));
            var rootxml = xml.SelectSingleNode("xml");
            rootxml.Attributes.RemoveAll();
            var result = xml.OuterXml;
            return result;
        }

        /// <summary>
        /// 处理文本消息
        /// </summary>
        /// <param name="requestStr"></param>
        /// <returns></returns>
        private OutMessage HandleTextMsg(string requestStr)
        {
            var inMessage = XmlSerializeUtil.Deserialize<InMessageText>(requestStr);
            var outMessage = new OutMessageText
            {
                Content = "您输入的内容是" + inMessage.Content,
                MsgType = inMessage.MsgType,
                CreateTime = inMessage.CreateTime,
                FromUserName = inMessage.ToUserName,
                ToUserName = inMessage.FromUserName
            };
            if (inMessage.Content.Contains("hello") || inMessage.Content.Contains("你好") || inMessage.Content.Contains("你是谁") || inMessage.Content.Contains("从哪里来"))
            {
                outMessage.Content = "hello,我是何大鹏，欢迎来到召唤师峡谷，您的OPenid是" + inMessage.FromUserName;
            }

            return outMessage;
        }


        /// <summary>
        /// 处理文本消息
        /// </summary>
        /// <param name="requestStr"></param>
        /// <returns></returns>
        private OutMessage HandleImageMsg(string requestStr)
        {
            var inMessage = XmlSerializeUtil.Deserialize<InMessageImage>(requestStr);
            var outMessage = new OutMessageImage
            {
                MsgType = inMessage.MsgType,
                CreateTime = inMessage.CreateTime,
                FromUserName = inMessage.ToUserName,
                ToUserName = inMessage.FromUserName,
                MediaId = inMessage.MediaId
            };

            return outMessage;
        }

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="requestStr"></param>
        /// <returns></returns>
        private OutMessage HandleEventMsg(string requestStr)
        {
            var inMessage = XmlSerializeUtil.Deserialize<InMessageEvent>(requestStr);
            var outMessage = new OutMessageText
            {
                CreateTime = inMessage.CreateTime,
                FromUserName = inMessage.ToUserName,
                ToUserName = inMessage.FromUserName,
                Content = "未知事件类型"
            };
            switch (inMessage.Event.ToLower())
            {
                case "subscribe":
                    return HandleSubscribe(inMessage);
                case "click":
                    return HandleClick(inMessage);
                case "view":
                    return HandleView(inMessage);
                case "scancode_push":
                    return HandleScancodePush(inMessage);
                case "scancode_waitmsg":
                    return HandleScancodeWaitmsg(inMessage);
                default:
                    break;
            }


            return outMessage;
        }

        /// <summary>
        /// 处理关注事件
        /// </summary>
        /// <param name="inMessage"></param>
        /// <returns></returns>
        private OutMessage HandleSubscribe(InMessageEvent inMessage)
        {
            var outMessageNews = new OutMessageNews
            {
                CreateTime = inMessage.CreateTime,
                FromUserName = inMessage.ToUserName,
                ToUserName = inMessage.FromUserName,
                ArticleCount = 2,
                Articles = new item[] {
                            new  item()
                            {
                                Description ="",
                                PicUrl="http://e.hiphotos.baidu.com/image/pic/item/1c950a7b02087bf49661186dffd3572c10dfcfa1.jpg",
                                Title="代码改变世界",
                                Url="http://www.cnblogs.com/hepeng"
                            },
                             new  item()
                            {
                                Description ="代码的质量取决于项目管理者对于技术和代码的把握能力，如果摊上不懂技术的项目管理者以及对于代码质量没有要求的研发人员，可能最终输出的代码，将成为一团乱麻，只能在一个个项目中无穷次的积累，直到遇到一群优秀的开发人员费劲心力把体系重构为止",
                                PicUrl="http://www.wolfcode.cn//data/upload/20181122/5bf6676158412.jpg",
                                Title="感谢关注",
                                Url="http://www.cnblogs.com/hepeng"
                            }
                        }
            };

            return outMessageNews;

        }

        /// <summary>
        /// 处理菜单点击事件
        /// </summary>
        /// <param name="inMessage"></param>
        /// <returns></returns>
        private OutMessage HandleClick(InMessageEvent inMessage)
        {
            var outMessage = new OutMessageText
            {
                CreateTime = inMessage.CreateTime,
                FromUserName = inMessage.ToUserName,
                ToUserName = inMessage.FromUserName,
                Content = $"您点击的按钮是{inMessage.EventKey}，暂未设置返回信息"
            };
            if (inMessage.EventKey.ToLower() == "music")
            {
                outMessage.Content = "http://music.taihe.com/";
            }


            return outMessage;
        }

        /// <summary>
        /// 处理菜单跳转事件 返回消息不会起作用！
        /// </summary>
        /// <param name="inMessage"></param>
        /// <returns></returns>
        private OutMessage HandleView(InMessageEvent inMessage)
        {
            var outMessage = new OutMessageText
            {
                CreateTime = inMessage.CreateTime,
                FromUserName = inMessage.ToUserName,
                ToUserName = inMessage.FromUserName,
                Content = $"{inMessage.EventKey}您跳转的url是{inMessage.EventKey}"
            };

            return outMessage;//可以返回空
        }

        /// <summary>
        /// 处理二维码扫描非url事件
        /// </summary>
        /// <param name="inMessage"></param>
        /// <returns></returns>
        private OutMessage HandleScancodeWaitmsg(InMessageEvent inMessage)
        {
            var outMessage = new OutMessageText
            {
                CreateTime = inMessage.CreateTime,
                FromUserName = inMessage.ToUserName,
                ToUserName = inMessage.FromUserName,
                Content = $"HandleScancodeWaitmsg{inMessage.EventKey}扫描结果：ScanCodeInfo:{inMessage.ScanCodeInfo},ScanType:{inMessage.ScanType},ScanResult:{inMessage.ScanResult}"
            };

            return outMessage;
        }
        /// <summary>
        /// 处理二维码扫描到url跳转事件 返回消息不会起作用！
        /// </summary>
        /// <param name="inMessage"></param>
        /// <returns></returns>
        private OutMessage HandleScancodePush(InMessageEvent inMessage)
        {
            var outMessage = new OutMessageText
            {
                CreateTime = inMessage.CreateTime,
                FromUserName = inMessage.ToUserName,
                ToUserName = inMessage.FromUserName,
                Content = $"HandleScancodePush{inMessage.EventKey}扫描结果：ScanCodeInfo:{inMessage.ScanCodeInfo},ScanType:{inMessage.ScanType},ScanResult:{inMessage.ScanResult}"
            };

            return outMessage;//可以返回空
        }

        /// <summary>
        /// 微网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string code, string state)
        {
            WxUserInfo userinfo = new WxUserInfo();
            var a = GetToken(code);
            if (a != null)
            {
                userinfo = GetUserInfo(a.access_token, a.Openid);
            }
            return View(userinfo);
        }

        /// <summary>
        /// 微网站首页
        /// </summary>
        /// <returns></returns>
        public PartialViewResult IndexPage(string code, string state)
        {
            WxUserInfo userinfo = new WxUserInfo();
            var a = GetToken(code);
            if (a != null)
            {
                userinfo = GetUserInfo(a.access_token, a.Openid);
            }
            return PartialView(userinfo);
        }

        /// <summary>
        /// 微网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Home()
        {
          
            return View();
        }

        /// <summary>
        /// GetToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private UserTokenAccess GetToken(string code)
        {
            RestClient client = new RestClient("https://api.weixin.qq.com");
            var getWYTokenUrl = $"/sns/oauth2/access_token?appid={GlabolParams.APPID}&secret={GlabolParams.APPSECRET}&code={code}&grant_type=authorization_code";
            var request = new RestRequest(getWYTokenUrl, Method.GET);
            var response = client.Execute<UserTokenAccess>(request);
            var a = JsonConvert.DeserializeObject<UserTokenAccess>(response.Content);

            return a;
        }

        /// <summary>
        /// GetToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private WxUserInfo GetUserInfo(string access_token, string openid)
        {
            RestClient client = new RestClient("https://api.weixin.qq.com");
            var getuserinfourl = $"/sns/userinfo?access_token={access_token}&openid={openid}&lang=zh_CN";
            var request = new RestRequest(getuserinfourl, Method.GET);
            var response = client.Execute<WxUserInfo>(request);
            var a = JsonConvert.DeserializeObject<WxUserInfo>(response.Content);
            return a;
        }


    }
}