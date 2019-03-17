using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weixin.UnitTest.OutDtos
{
    /// <summary>
    /// 获取用户信息的凭证
    /// </summary>
    public class UserTokenAccess
    {
        public string access_token { get; set; }
        /// <summary>
        /// Access_token一般为两个小时有效期
        /// </summary>
        public int Expires_in { get; set; }
        /// <summary>
        /// 可用此参数进行刷新有效期
        /// </summary>
        public string Refresh_token { get; set; }
        public string Openid { get; set; }
        public string Scope { get; set; }
    }
}
