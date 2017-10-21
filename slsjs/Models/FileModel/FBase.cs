using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace slsjs.Models.FileModel
{
    /// <summary>
    /// 文件系统的基本类型
    /// </summary>
    public abstract class FBase
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        [JsonProperty(PropertyName = "ftype", NullValueHandling = NullValueHandling.Ignore)]
        public abstract string FType { get; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// 文件属性
        /// </summary>
        [JsonProperty(PropertyName = "attr", NullValueHandling = NullValueHandling.Ignore)]
        public FileAttributes Attr { get; set; }

        /// <summary>
        /// 文件创建时间
        /// </summary>
        [JsonProperty(PropertyName = "ctime", NullValueHandling = NullValueHandling.Ignore)]
        //[JsonConverter(typeof(MyDatetimeConverter))]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        [JsonProperty(PropertyName = "latime", NullValueHandling = NullValueHandling.Ignore)]
        //[JsonConverter(typeof(MyDatetimeConverter))]
        public DateTime LastAccessTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonProperty(PropertyName = "lwtime", NullValueHandling = NullValueHandling.Ignore)]
        //[JsonConverter(typeof(MyDatetimeConverter))]
        public DateTime LastWriteTime { get; set; }


        /// <summary>
        /// 一个用于json序列化的时间转化方法
        /// 非通用性处理，不处理其它异常
        /// </summary>
        //public class MyDatetimeConverter : DateTimeConverterBase
        //{
        //    private string _datetime_format = "yyyy-MM-dd HH:mm:ss";

        //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //    {
        //        try
        //        {
        //            string dtstr = existingValue.ToString();
        //            DateTime dt = DateTime.ParseExact(dtstr, this._datetime_format, CultureInfo.CurrentCulture);
        //            if (objectType.IsGenericType)
        //            {
        //                return new DateTime?(dt);
        //            }
        //            else
        //            {
        //                return dt;
        //            }
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }

        //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //    {
        //        if (value is DateTime dt)
        //        {
        //            writer.WriteValue(dt.ToString(this._datetime_format));
        //        }
        //        else if (value is DateTime?)
        //        {
        //            DateTime? dt1 = value as DateTime?;
        //            if (dt1.HasValue)
        //            {
        //                writer.WriteValue(dt1.Value.ToString(this._datetime_format));
        //            }
        //            else
        //            {
        //                writer.WriteNull();
        //            }
        //        }
        //        else
        //        {
        //            writer.WriteNull();
        //        }
        //    }
        //}
    }
}