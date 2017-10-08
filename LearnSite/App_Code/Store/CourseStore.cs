using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
namespace LearnSite.Store
{
    /// <summary>
    ///Store 的摘要说明
    /// </summary>
    public class CourseStore
    {
        public CourseStore()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 创建学案目录，返回目录虚拟路径
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static string CreateStore(int Cid)
        {
            string storepath = FCKWebConfigPath();
            MakeNewDir(storepath);
            string Cidpath = storepath + Cid.ToString() + "/";
            MakeNewDir(Cidpath);
            return Cidpath;
        }

        /// <summary>
        /// 初始化FCKeditor:UserFilesPath路径
        /// </summary>
        /// <returns></returns>
        private static string FCKWebConfigPath()
        {
            string SavePath = "~/Store/";
            return SavePath;
        }

        public static string CoursePath(string Cid)
        {
            return FCKWebConfigPath() + Cid.ToString() + "/";
        }

        /// <summary>
        /// 新建目录
        /// </summary>
        /// <param name="dirname">虚拟路径</param>
        public static void MakeNewDir(string dirname)
        {
            string newdir = HttpContext.Current.Server.MapPath(dirname);
            if (!Directory.Exists(newdir))
            {
                Directory.CreateDirectory(newdir);
            }
        }
        /// <summary>
        /// 获取学案上传资料存放目录，虚拟路径（不存在则自动创建），结尾带/
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static string SetCourseStore(string Cid)
        {
            if (LearnSite.Common.WordProcess.IsNum(Cid))
            {
                string storepath = "~/Store/";
                MakeNewDir(storepath);
                string Cidpath = storepath + Cid.ToString() + "/";
                MakeNewDir(Cidpath);
                return Cidpath;
            }
            else
            {
                return "~/sql/";
            }
        }
        /// <summary>
        /// 设置资源下载的保存路径
        /// </summary>
        /// <returns></returns>
        public static string SetSoftDownload()
        {
            string DownloadPath = "~/Download/";
            MakeNewDir(DownloadPath);
            return DownloadPath;
        }
        /// <summary>
        /// 设置资源下载的保存路径
        /// </summary>
        /// <returns></returns>
        public static string SaveSoftDownload()
        {
            string DownloadPath = "~/Download/";
            MakeNewDir(DownloadPath);
            DateTime today = DateTime.Now;
            string newsavedir = today.Year.ToString() + "_" + today.Month.ToString();
            string monPath = DownloadPath + newsavedir + "/";
            MakeNewDir(monPath);
            return monPath;
        }
        public static string SetQuizStorage()
        {
            string QuizStorage = "~/Quiz/Storage/";
            MakeNewDir(QuizStorage);
            return QuizStorage;
        }
        /// <summary>
        /// 文件空间浏览用
        /// </summary>
        /// <param name="ty"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static string GetStoreUrl(string ty, string cid)
        {
            string url = "~/";
            switch (ty)
            {
                case "Course":
                    url = SetCourseStore(cid);
                    break;
                case "Soft":
                    url = SetSoftDownload();
                    break;
                case "Quiz":
                    url = SetQuizStorage();
                    break;
            }
            return url;
        }
        /// <summary>
        /// 上传用
        /// </summary>
        /// <param name="ty"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static string GetSaveUrl(string ty, string cid)
        {
            string url = "~/";
            switch (ty)
            {
                case "Course":
                    url = SetCourseStore(cid);
                    break;
                case "Soft":
                    url = SaveSoftDownload();
                    break;
                case "Quiz":
                    url = SetQuizStorage();
                    break;
            }
            return url;
        }
    }
}