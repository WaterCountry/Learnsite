using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.UI.WebControls;
namespace LearnSite.Common
{
    /// <summary>
    ///WorkUpload 的摘要说明
    /// </summary>
    public class WorkUpload
    {
        public WorkUpload()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private static  string AutonomicSavePath = "~/NomicWork/";
        private static void CreateNomicDir(string Sid )
        {
            string savepath = HttpContext.Current.Server.MapPath(AutonomicSavePath);
            MakeDir(savepath);
            string sidpath = Checkbdir(savepath) + Sid;
            MakeDir(sidpath);     
        }
        /// <summary>
        /// 创建自学作品目录
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public static string GetAurl(int Sid)
        {
            string Aurl = AutonomicSavePath + Sid.ToString();
            CreateNomicDir(Sid.ToString());
            return Aurl;
        }
        /// <summary>
        /// 创建作品目录
        /// </summary>
        /// <param name="Syear"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        private static void CreateWorkDir(string Syear, string Sgrade, string Sclass, string Wcid, string Wmid)
        {
            string savepath =HttpContext.Current.Server.MapPath( WorkSavePath());
            MakeDir(savepath);
            string yearpath = Checkbdir(savepath) + Syear;
            MakeDir(yearpath);
            string gradepath = Checkbdir(yearpath) + Sgrade;
            MakeDir(gradepath);
            string classpath = Checkbdir(gradepath) + Sclass;
            MakeDir(classpath);
            string wcidpath = Checkbdir(classpath) + Wcid;
            MakeDir(wcidpath);
            string wmidpath = Checkbdir(wcidpath) + Wmid;
            MakeDir(wmidpath);        
        }
        /// <summary>
        /// 如果不存在，创建目录
        /// </summary>
        /// <param name="savepath">物理路径</param>
        private static void MakeDir(string savepath)
        {
            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
            }
        }
        private static string WorkSavePath()
        {
            string HomeWorkPath = System.Web.Configuration.WebConfigurationManager.AppSettings["HomeWork"].ToString();
            if (HomeWorkPath == "")
                HomeWorkPath = "~/HomeWork/";

            return HomeWorkPath;
        }
        /// <summary>
        ///  获得作品存放虚拟路径，如果目录不存在则创建
        /// </summary>
        /// <param name="Syear"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public static string GetWurl(string Syear,string Sgrade,string Sclass,string Wcid,string Wmid)
        {
            string Wurl = WorkSavePath() + Syear + "/" + Sgrade + "/" + Sclass + "/" + Wcid + "/" + Wmid;
            CreateWorkDir(Syear, Sgrade, Sclass, Wcid, Wmid);// 创建作品目录
            return Wurl;
        }

        /// <summary>
        /// 保存提交作品，路径参数为虚拟路径
        /// </summary>
        /// <param name="FuWork">上传控件</param>
        /// <param name="SavePath">上传保存虚拟路径</param>
        /// <returns></returns>
        public static void WorkUploadSave(FileUpload FuWork, string SaveFile)
        {
            FuWork.SaveAs(HttpContext.Current.Server.MapPath(SaveFile));
        }
        /// <summary>
        /// 检验提交作品类型是否正确
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="FileType"></param>
        /// <returns></returns>
        public static bool CheckType(string UpFileName, string Mfiletype)
        {
            if (UpFileName != null && UpFileName != "")
            {
                string GetFileType = UpFileName.Substring(UpFileName.LastIndexOf(".") + 1);
                if (Mfiletype.ToLower() == GetFileType.ToLower())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检验物理路径c:\最后一个字符是否缺少"\"，缺少则添加
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
    }

}