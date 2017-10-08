using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
namespace LearnSite.Store
{
    /// <summary>
    ///Package 的摘要说明
    /// </summary>
    public class Package
    {
        public Package()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private static string PackagePath = "~/Package/";

        /// <summary>
        /// 如果学案包存放目录不存在，则自动创建
        /// </summary>
        private static void PackagePathCreate()
        {
            string mappackagePath = HttpContext.Current.Server.MapPath(PackagePath);
            if (!Directory.Exists(mappackagePath))
                Directory.CreateDirectory(mappackagePath);
        }
        /// <summary>
        /// 将指定学案Cid的文件夹压缩打包为rar格式，
        /// 存放到Package目录，文件名为Cid.rar        
        /// </summary>
        /// <param name="Cid"></param>
        public static void ZipToPackageFile(int Cid)
        {
            PackagePathCreate();
            string CidDir = LearnSite.Store.XmlCourse.CourseCidPath(Cid);
            string ZipFilename = PackagePath + Cid.ToString() + ".cs";//虚拟路径
            string ZipFilenameMapPath = HttpContext.Current.Server.MapPath(ZipFilename);//物理路径

            File.Delete(ZipFilenameMapPath);//不在不引发异常
            System.Threading.Thread.Sleep(200);

            if (Directory.Exists(CidDir))
                LearnSite.Store.SharpZip.PackFiles(ZipFilenameMapPath, CidDir);
        }
        /// <summary>
        /// 将学案名为CidCtitle的学案包解压到临时文件夹tempbag中
        /// </summary>
        /// <param name="Cid"></param>
        public static void UnZipToCourseDir(string Cid)
        {
            string ZipFilename = PackageFile(Cid);
            string UnZipDir = UnZipTempDir();
            if (File.Exists(ZipFilename))
            {
                LearnSite.Store.SharpZip.UnpackFiles(ZipFilename, UnZipDir);
            }
        }
        /// <summary>
        /// 返回学案包物理路径
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        private static string PackageFile(string Cid)
        {
            string filename = PackagePath + Cid + ".cs";
            return HttpContext.Current.Server.MapPath(filename);
        }
        /// <summary>
        /// 判断学案包是否存在
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static bool PackageExists(string Cid)
        {

            return File.Exists(PackageFile(Cid));
        }
        /// <summary>
        /// 获得学案包创建时间
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static DateTime PackageTime(string Cid)
        {
            FileInfo packageinfo = new FileInfo(PackageFile(Cid));
            return packageinfo.LastWriteTime;
        }
        /// <summary>
        /// 获得学案包的大小，单位KB（千字节）
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static string PackageLengh(string Cid)
        {
            FileInfo packageinfo = new FileInfo(PackageFile(Cid));
            long len = packageinfo.Length / 1024;
            return len.ToString(); ;
        }
        /// <summary>
        /// 返回学案包的常见属性
        /// </summary>
        /// <param name="Cid">学案自动编号</param>
        /// <returns></returns>
        public static string PachageAttrible(string Cid)
        {
            FileInfo packageinfo = new FileInfo(PackageFile(Cid));
            string strmsg = "学案包压缩大小：" + (packageinfo.Length / 1024).ToString() + "Kb <br/><br/>学案包只读属性：" + packageinfo.IsReadOnly.ToString() + "<br/><br/>学案包创建时间：" + packageinfo.LastWriteTime.ToShortDateString();

            return strmsg;
        }

        /// <summary>
        /// 获得解压缩临时文件夹（物理路径）
        /// </summary>
        /// <returns></returns>
        private static string UnZipTempDir()
        {
            string strDir = HttpContext.Current.Server.MapPath("~/tempbag/");
            if (Directory.Exists(strDir))
            {
                Directory.Delete(strDir, true);//如果存在，则删除文件夹
            }
            Directory.CreateDirectory(strDir);//无条件创建文件夹

            return strDir;
        }

        /// <summary>
        /// 将指定网站打包        
        /// </summary>
        /// <param name="SitePath">网站的虚拟路径，结尾不带斜杠</param>
        public static void SiteToRar(string SitePath, string Cid)
        {
            string ZipFilename = SitePath + Cid + ".rar";//虚拟路径
            string ZipFilenameMapPath = HttpContext.Current.Server.MapPath(ZipFilename);//物理路径

            File.Delete(ZipFilenameMapPath);//不在不引发异常
            System.Threading.Thread.Sleep(200);
            string SiteDir = HttpContext.Current.Server.MapPath(SitePath);
            if (Directory.Exists(SiteDir))
                LearnSite.Store.SharpZip.PackFiles(ZipFilenameMapPath, SiteDir);
        }
        /// <summary>
        /// 获取网站打包文件日期
        /// </summary>
        /// <param name="sitepath">网站的虚拟路径，结尾不带斜杠</param>
        /// <returns></returns>
        public static string SiteDate(string sitepath, string cid)
        {
            string sitedatetime = "";
            string sFilename = sitepath + cid + ".rar";//虚拟路径
            string sFilenameMapPath = HttpContext.Current.Server.MapPath(sFilename);//物理路径
            if (File.Exists(sFilenameMapPath))
            {
                FileInfo fi = new FileInfo(sFilenameMapPath);
                sitedatetime = fi.LastWriteTime.ToShortDateString();
            }
            return sitedatetime;
        }
    }
}