using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
namespace LearnSite.Common
{
    /// <summary>
    ///Htmlcheck 的摘要说明
    /// </summary>
    public class Htmlcheck
    {
        public Htmlcheck()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 检测html文件是否更新
        /// </summary>
        /// <param name="Syear"></param>
        /// <param name="Sclass"></param>
        /// <param name="Snum"></param>
        /// <param name="htmlname"></param>
        /// <returns></returns>
        public static string HtmlUpdatetime(int Syear, int Sclass, string Snum, string htmlname)
        {
            htmlname = htmlname.Trim().ToLower();
            if (htmlname.EndsWith("l"))
            {
                htmlname = htmlname.Substring(0, htmlname.Length - 2);
            }
            string updatetime = "";
            string htmlfilenme = "~/FtpSpace/" + Syear.ToString() + "/" + Sclass.ToString() + "/" + Snum + "/" + htmlname;
                string filepath = HttpContext.Current.Server.MapPath(htmlfilenme);
                if (File.Exists(filepath))
                {
                    FileInfo filemsg = new FileInfo(filepath);
                    updatetime = filemsg.LastWriteTime.ToString();
                }
                else
                {
                    filepath = filepath + "l";
                    if (File.Exists(filepath))
                    {
                        FileInfo filemsg = new FileInfo(filepath);
                        updatetime = filemsg.LastWriteTime.ToString();
                    }
                }
            return updatetime;
        }
        /// <summary>
        /// 遍历单个学生网站根目录文件，获取当前更新情况到数据库中。
        /// </summary>
        /// <param name="Syear"></param>
        /// <param name="Sclass"></param>
        /// <param name="Snum"></param>
        public static int SiteUpdateCheck(string Snum, string sitepath)
        {
            int spoint = 0;
            DateTime dt = DateTime.Now;
            string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            DateTime nowdayDt = Convert.ToDateTime(today);
            string realpath = HttpContext.Current.Server.MapPath(sitepath);
            if (Directory.Exists(realpath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(realpath);
                FileInfo[] files = dirInfo.GetFiles();
                if (files.Length > 0)
                {
                    bool isok = false;
                    foreach (FileInfo thisfile in files)
                    {
                        if (thisfile.LastWriteTime > nowdayDt)
                        {
                            isok = true;
                            break;
                        }
                    }
                    if (isok)
                    {
                        spoint = Convert.ToInt32(countDirSize(dirInfo));
                        LearnSite.BLL.Webstudy wsbll = new LearnSite.BLL.Webstudy();
                        wsbll.UpdateWebTimeSize(Snum, dt,spoint);//更新Webstudy表中网站的更新日期
                    }
                }
            }
            return spoint;
        }

        public static long countDirSize(System.IO.DirectoryInfo dir)
        {
            long size = 0;
            FileInfo[] files = dir.GetFiles();
            //通过GetFiles方法,获取目录中所有文件的大小
            foreach (System.IO.FileInfo info in files)
            {
                size += info.Length;
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            //获取目录下所有文件夹大小,并存到一个新的对象数组中
            foreach (DirectoryInfo dirinfo in dirs)
            {
                size += countDirSize(dirinfo);
            }
            return size;
        } 
    }
}