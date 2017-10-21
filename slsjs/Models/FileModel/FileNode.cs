using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace slsjs.Models.FileModel
{
    /// <summary>
    /// 表示一个文件节点
    /// </summary>
    public class FileNode : FBase
    {
        public override string FType => "file";

        /// <summary>
        /// 文件大小
        /// </summary>
        [JsonProperty(PropertyName = "length")]
        public long Length { get; set; }
    }
}