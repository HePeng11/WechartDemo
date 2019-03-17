using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Weixin.UnitTest.OutDtos;

namespace Weixin.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client = new RestClient("https://api.weixin.qq.com");
        string token = "";
      

        /// <summary>
        /// 获取普通token
        /// </summary>
        public void Gettokenurl()
        {
            var gettokenurl = "/cgi-bin/token?grant_type=client_credential&appid={APPID}&secret={APPSECRET}";
            gettokenurl = gettokenurl.Replace("{APPID}", GlabolParams.APPID).Replace("{APPSECRET}", GlabolParams.APPSECRET);
            var request = new RestRequest(gettokenurl, Method.GET);
            var response = client.Execute<TokenReturn>(request);
            token = response.Data.Access_token;
        }

        /// <summary>
        /// 素材
        /// </summary>
        [TestMethod]
        public void GetMList()
        {
            var getmlisturl = $"/cgi-bin/material/batchget_material?access_token={token}";
            var ps = new
            {
                type = "image",
                offset = "0",
                count = "20"
            };
            var request = new RestRequest(getmlisturl, Method.POST);
            request.AddJsonBody(ps);
            var response = client.Execute(request);
            var content = response.Content;
        }

        /// <summary>
        /// 设置菜单
        /// </summary>
        [TestMethod]
        public void SetMenu()
        {
            Gettokenurl();
            var getmlisturl = $"/cgi-bin/menu/create?access_token={token}";
            var ps = new
            {
                button = new object[] {
                 new {
                      type="click",
                      name="今日歌曲",
                      key="music"
                    },
                 new {
                    name="扫码",
                    sub_button=new object[]{
                          new {
                               type="scancode_waitmsg",
                                name="扫码带提示",
                                key= "rselfmenu_0_0",
                            },
                           new {
                               type="scancode_push",
                                name="扫码推事件",
                                key= "rselfmenu_0_1",
                            }
                     }
                    },
                 new {
                     name="菜单",
                     sub_button=new object[]{
                          new {
                               type="view",
                               name="百度一下",
                               url="http://www.baidu.com/"
                            },
                            new{
                                 type="miniprogram",
                                 name="查看日历",
                                 url="http://mp.weixin.qq.com",
                                 appid="wx286b93c14bbf93aa",
                                 pagepath="pages/lunar/index"
                             },
                            new{
                               type="click",
                               name="为公众号点个赞",
                               key="goodgzh"
                                },
                            new{
                               type="view",
                               name="进入网站demo",
                               url=$"https://open.weixin.qq.com/connect/oauth2/authorize?appid={GlabolParams.APPID}&redirect_uri=http://gy6qcn.natappfree.cc/weixin/index&response_type=code&scope=snsapi_userinfo&state=selfparams#wechat_redirect"
                            }
                     }
                }
                }
            };
            var request = new RestRequest(getmlisturl, Method.POST);
            request.AddJsonBody(ps);
            var response = client.Execute(request);
            var content = response.Content;
        }


        /// <summary>
        /// 删除菜单
        /// </summary>
        [TestMethod]
        public void DeleteMenu()
        {
            Gettokenurl();
            var deletemenuurl = $"/cgi-bin/menu/delete?access_token={token}";
            var request = new RestRequest(deletemenuurl, Method.GET);
            var response = client.Execute(request);


        }

        /// <summary>
        /// 发生模板消息
        /// </summary>
        [TestMethod]
        public void SendTemplateMsg()
        {
            Gettokenurl();
            var sendtemplatemsgurl = $"/cgi-bin/message/template/send?access_token={token}";
            var ids = GetUerIds();
            foreach (var id in ids.Data.Openid)
            {
                var request = new RestRequest(sendtemplatemsgurl, Method.POST);
                var ps = new
                {
                    touser = id,
                    template_id = "Ntuyw-g9UZWanRs7vShS_6nSEInUF1qSYdw-8JCxf6s",
                    url = "http://wzsy5a.natappfree.cc/weixin/index.html",
                    data = new
                    {
                        First = new
                        {
                            value = "起床了吗~",
                            color = "#173177"
                        },
                        Name = new
                        {
                            value = "卢大红是笨蛋",
                            color = "#173177"
                        },
                        Amount = new
                        {
                            value = "229",
                            color = "#173177"
                        },
                        OrderTime = new
                        {
                            value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            color = "#173177"
                        },
                        Remark = new
                        {
                            value = "欢迎下次光临！"
                        },
                    }
                };
                request.AddJsonBody(ps);
                var response = client.Execute(request);
                var content = response.Content;
            }


        }

        /// <summary>
        /// 获取前10000名用户的openid
        /// </summary>
        public OpenIdData GetUerIds()
        {
            //Gettokenurl();
            var usergeturl = $"/cgi-bin/user/get";
            var request = new RestRequest(usergeturl, Method.GET);
            request.AddQueryParameter("access_token", token);
            var response = client.Execute<OpenIdData>(request);
            var content = response.Content;
            return response.Data;
        }
    }
}
