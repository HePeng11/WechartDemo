using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudContractTest
{
    /// <summary>
    /// RestSharp帮助类
    /// </summary>
    public class RestSharpHelper
    {

        /// <summary>
        /// 使用json格式数据去Post请求http服务
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="client">已初始化baseUrl的http客户端</param>
        /// <param name="url">请求地址</param>
        /// <param name="jsonObj">json对象</param>
        /// <param name="headers">头部信息键值对</param>
        /// <returns></returns>
        public static IRestResponse<T> RequestByPostJson<T>(RestClient client, string url, object jsonObj, Dictionary<string, string> headers = null) where T : new()
        {
            var request = new RestRequest(url, Method.POST);
            request.RequestFormat = DataFormat.Json;
            if (headers != null && headers.Count > 0)
            {
                foreach (var head in headers)
                {
                    request.AddHeader(head.Key, head.Value);
                }
            }
            request.AddJsonBody(jsonObj);
            return client.Execute<T>(request);

        }

        

    }
}
