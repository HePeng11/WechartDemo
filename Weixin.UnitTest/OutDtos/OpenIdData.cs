using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weixin.UnitTest.OutDtos
{
    /// <summary>
    /// 获取用户列表返回数据
    /// </summary>
    public class OpenIdData
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public OpenIdDataData Data { get; set; }
        public class OpenIdDataData
        {
            public List<string> Openid { get; set; }

            public string Next_openid { get; set; }
        }
    }


}
