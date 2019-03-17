using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weixin.UnitTest.OutDtos
{
    public class WxUserInfo
    {
        public string Openid { get; set; }
        public string Nickname { get; set; }
        public ManSex Sex { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Headimgurl { get; set; }
        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// </summary>
        public List<string> Privilege { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string Unionid { get; set; }
    }
    public enum ManSex
    {

        未知 = 0,
        男 = 1,
        女 = 2
    }
}
