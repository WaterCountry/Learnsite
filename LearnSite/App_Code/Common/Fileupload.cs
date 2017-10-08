using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;

namespace LearnSite.Common
{
    /// <summary>
    ///Fileupload 的摘要说明
    /// </summary>
    public class Fileupload
    {
        public Fileupload()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 保存资源上传文件
        /// </summary>
        /// <param name="FUsoft"></param>
        /// <returns></returns>
        public static string Fupload(FileUpload FUsoft)
        {            
            string myfile = "";
            if (FUsoft.HasFile)
            {
                string uploadfile = FUsoft.PostedFile.FileName;
                string filename = uploadfile.Substring(uploadfile.LastIndexOf("\\") + 1);
                string DownloadPath = "~/Download/";
                string realpath = HttpContext.Current.Server.MapPath(DownloadPath);
                DateTime dt = DateTime.Now;
                string NowTime = dt.Year.ToString()+"-"+dt.Month.ToString()+"-"+dt.Day.ToString();
                string Creatfile = NowTime + filename;
                if (!Directory.Exists(realpath))
                {
                    Directory.CreateDirectory(realpath);
                }
                FUsoft.SaveAs(Checkbdir(realpath) + Creatfile);
                
                myfile = Checkpdir(DownloadPath) + Creatfile;
            }
            return myfile;
        }

        /// <summary>
        /// 检验路径最后一个字符是否缺少"\"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Checkbdir(string str)
        {
            if (!str.EndsWith("\\"))
            {
                str = str + "\\";
            }
            return str;
        }

        private static string Checkpdir(string str)
        {
            if (!str.EndsWith("/"))
            {
                str = str + "/";
            }
            return str;
        }
    }
}
