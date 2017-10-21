using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace slsjs.Models.Formula
{
    /// <summary>
    /// 公式的本地保存信息
    /// </summary>
    public class FormulaLocal
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastTime { get; set; }
        /// <summary>
        /// 公式数据
        /// </summary>
        public string FormulaData { get; set; }
    }
}