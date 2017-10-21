using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace slsjs.Controllers.Helper
{
    /// <summary>
    /// 定义一个可以由submit类型input的name属性来影响操作方法的选择
    /// </summary>
    public class SubmitBtnAttribute : ActionNameSelectorAttribute
    {
        /// <summary>
        /// 指定name的对应属性（大小写敏感）
        /// </summary>
        public string Name { get; set; }

        public override bool IsValidName(
            ControllerContext controllerContext,
            string actionName,
            System.Reflection.MethodInfo methodInfo)
        {
            /*
            if (string.IsNullOrEmpty(this.Name))
            {
                return false;
            }
            return controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.Name);
            */

            return !string.IsNullOrEmpty(this.Name)
                && controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.Name);
        }

        public SubmitBtnAttribute() { }

        public SubmitBtnAttribute(string name)
            : this()
        {
            this.Name = name;
        }
    }
}