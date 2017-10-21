using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
//namespace slsjs.Controllers
{
    public static class ContorllerEX
    {
        
        //public static JsonResult NSJson(this Controller controller, object data)
        //{
        //    return NSJson(controller, data);
        //}
        //public static JsonResult NSJson(this Controller controller, object data, string contentType)
        //{
        //    return new JsonResult();
        //}
        //public static JsonResult NSJson(this Controller controller, object data, string contentType, Encoding contentEncoding)
        //{
        //    return new JsonResult();
        //}
        //public static JsonResult NSJson(this Controller controller, object data, JsonRequestBehavior behavior)
        //{
        //    return new JsonResult();
        //}
        //public static JsonResult NSJson(this Controller controller, object data, string contentType, JsonRequestBehavior behavior)
        //{
        //    return new JsonResult();
        //}

        /// <summary>
        /// 创建 System.Web.Mvc.JsonResult 对象，该对象使用内容类型、内容编码和 JSON 请求行为将指定对象序列化为 JavaScript 对象表示法 (JSON) 格式。
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="data">要序列化的对象。</param>
        /// <param name="behavior">限制 JSON 请求行为（get、post等）</param>
        /// <param name="settings">采用Newton来序列化的配置参数</param>
        /// <param name="contentType">内容类型（MIME 类型）。</param>
        /// <param name="contentEncoding">内容编码（空则采用网络回复的默认编码）。</param>
        /// <returns></returns>
        public static JsonResult NSJson(this Controller controller, object data,
            JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet,
            JsonSerializerSettings settings = null,
            string contentType = "application/json",
            Encoding contentEncoding = null)
        {
            return new NewtonJsonResult()
            {
                ContentEncoding = contentEncoding,
                ContentType = contentType,
                Data = data,
                JsonRequestBehavior = behavior,
                JsonSerializerSettings = settings
            };
        }

    }

    /// <summary>
    /// 利用Newtonsoft来处理http请求的json处理
    /// </summary>
    public class NewtonJsonResult : JsonResult
    {
        /// <summary>
        /// 配置
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public NewtonJsonResult()
        {
            this.JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        //public NewtonJsonResult(object obj) : this()
        //{
        //    this.Data = obj;
        //}

        //public NewtonJsonResult(object obj, JsonSerializerSettings jss) : this(obj)
        //{
        //    this.JsonSerializerSettings = jss;
        //}

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("改方法当前不允许使用Get");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (this.Data != null)
            {
                string strJson = JsonConvert.SerializeObject(this.Data, this.JsonSerializerSettings);
                response.Write(strJson);
                response.End();
            }
        }
    }
}