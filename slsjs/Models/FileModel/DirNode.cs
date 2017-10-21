using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace slsjs.Models.FileModel
{
    /// <summary>
    /// 表示一个目录
    /// </summary>
    public class DirNode : FBase
    {
        public override string FType => "dir";

        /// <summary>
        /// 目录下的文件
        /// </summary>
        [JsonProperty(
            PropertyName = "children",
            NullValueHandling = NullValueHandling.Ignore,
            Order = 3
            )]
        public IEnumerable<FBase> Children { get; set; }

        /// <summary>
        /// 获取或设置完整的目录
        /// </summary>
        [JsonProperty(
            PropertyName = "fdir",
            NullValueHandling = NullValueHandling.Ignore,
            Order = 2
            )]
        public string FullDir { get; set; }
        
        /// <summary>
        /// 获取或设置是否正在加载中（暂时仅用于json格式中）
        /// </summary>
        [JsonProperty(
            PropertyName = "isloading",
            Order = 1
            )]
        public bool IsLoading { get; set; } = false;
    }
}