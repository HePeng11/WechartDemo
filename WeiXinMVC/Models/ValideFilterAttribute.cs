using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiXinMVC.Models
{
    /// <summary>
    /// 验证请求是否来自微信
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValideFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ///signature, timestamp, nonce
            var signature = filterContext.HttpContext.Request.Params["signature"].ToNNString();
            var timestamp = filterContext.HttpContext.Request.Params["timestamp"].ToNNString();
            var nonce = filterContext.HttpContext.Request.Params["nonce"].ToNNString();
            if (!WeixinUtil.Validate(signature, timestamp, nonce))
            {
                filterContext.HttpContext.Response.Write($"signature:{signature},timestamp:{timestamp},nonce:{nonce}\r\n");
                filterContext.HttpContext.Response.Write("验证参数无效，无法执行操作，请输入正确的微信加密签名、时间戳、随机数");
                filterContext.HttpContext.Response.End();
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //var controllerName = filterContext.RouteData.Values["controller"].ToString();
            //var actionName = filterContext.RouteData.Values["action"].ToString();
        }
    }
}