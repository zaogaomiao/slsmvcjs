using slsjs.Models.FileModel;
using slsjs.Models.Formula;
using System.IO;
using System.Linq;
using System.Web.Mvc;

using IOFile = System.IO.File;

namespace slsjs.Controllers
{
    public class VueTestController : Controller
    {
        // GET: VueTest
        public ActionResult Index()
        {
            return View();
        }

        #region 文件读取测试

        /// <summary>
        /// VUE目录测试的开头
        /// </summary>
        /// <returns></returns>
        [ActionName("DirTest")]
        public ActionResult DirTest_Start()
        {
            return View(this.GetDirInfo("D:\\", 0));
            //return Json(this.GetDirInfo("D:\\"), JsonRequestBehavior.AllowGet);
        }

        [ActionName("DirView")]
        public ActionResult DirView_Start()
        {
            return View(this.GetDirInfo("D:\\", 1));
        }

        [HttpPost]
        [ActionName("DirTestGet")]
        public ActionResult DirTest_GetDir(string dir)
        {
            return this.NSJson(
                data: this.GetDirInfo(dir, 1),
                behavior: JsonRequestBehavior.DenyGet
                );
            //return Json(this.GetDirInfo(dir, 1), JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 获取目录下的信息
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public FBase GetDirInfo(string dir, int deep = 0)
        {
            return this.GetDirNode(new DirectoryInfo(dir), deep);
        }

        private FBase GetDirNode(DirectoryInfo di, int deep = 0)
        {
            return new DirNode()
            {
                Name = di.Name,
                FullDir = di.FullName,
                Attr = di.Attributes,
                CreationTime = di.CreationTime,
                LastAccessTime = di.LastAccessTime,
                LastWriteTime = di.LastWriteTime,
                Children = deep > 0
                    ? (
                        from d in di.GetDirectories()
                        select this.GetDirNode(d, deep - 1)
                    ).Union(
                        from fi in di.GetFiles()
                        select new FileNode()
                        {
                            Name = fi.Name,
                            Length = fi.Length,
                            Attr = fi.Attributes,
                            CreationTime = fi.CreationTime,
                            LastAccessTime = fi.LastAccessTime,
                            LastWriteTime = fi.LastWriteTime
                        }
                    )
                    : null
            };
        }

        #endregion

        #region 数学公式测试

        [ActionName("FmHome")]
        public ActionResult Formula_Index()
        {
            string datafile = this.GetFormulaDataFileName();
            FormulaLocal fl = new FormulaLocal()
            {
                FileName = "test01.txt"
            };
            if (IOFile.Exists(datafile))
            {
                using (FileStream fs = new FileStream(datafile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
                    {
                        fl.FormulaData = sr.ReadToEnd();
                    }
                }
            }
            return View(fl);
        }

        [HttpPost]
        [ActionName("FmSave")]
        public ActionResult Formula_Save(string fvalue)
        {
            string datafile = this.GetFormulaDataFileName();

            using (FileStream fs = new FileStream(datafile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.SetLength(0);
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                {
                    sw.Write(fvalue);
                    sw.Flush();
                }
            }

            return Content("保存成功");
        }

        //[HttpPost]
        [ActionName("FmRead")]
        public ActionResult Formula_Read()
        {
            string fd = this.GetFormulaData();
            return Content(fd);
        }

        /// <summary>
        /// 获取算式数据文件的全文件名
        /// </summary>
        /// <returns></returns>
        public string GetFormulaDataFileName()
        {
            string fpath = Server.MapPath("~\\Data\\Formula");
            // C:\slscode\jstool\slsjs\slsjs\Data\Formula
            string datafile = fpath + "\\test01.txt";
            return datafile;
        }

        /// <summary>
        /// 获取算式数据文件内容
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public string GetFormulaData()
        {
            string datafile = this.GetFormulaDataFileName();
            string ret = string.Empty;
            if (IOFile.Exists(datafile))
            {
                using (FileStream fs = new FileStream(datafile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
                    {
                        ret = sr.ReadToEnd();
                    }
                }
            }
            return ret;
        }

        #endregion
    }
}