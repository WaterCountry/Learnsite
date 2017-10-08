using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.IO;
namespace LearnSite.Ftp
{
    /// <summary>
    ///Disk 的摘要说明
    /// </summary>
    public class Disk
    {
        public Disk()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        /// <summary>
        /// 新建目录，无返回判断
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
        /// 新建目录，返回是否存在,如果已经存在，返回真
        /// </summary>
        /// <param name="dirname">虚拟路径</param>
        public static bool MakeDir(string dirname)
        {
            string newdir = HttpContext.Current.Server.MapPath(dirname);
            if (!Directory.Exists(newdir))
            {
                Directory.CreateDirectory(newdir);
                return false;
            }
            else
            {
                return true;//如果存在，返回真
            }
        }
        /// <summary>
        /// 根据年份、班级、学号创建一个Ftp目录
        /// </summary>
        /// <param name="Syear"></param>
        /// <param name="Sclass"></param>
        /// <param name="Snum"></param>
        public static void CreateOneDir(string Syear, string Sclass, string Snum)
        {
            string savepath = "~/FtpSpace/";
            MakeNewDir(savepath);

            string Syearpath = savepath + Syear;
            MakeNewDir(Syearpath);

            string Sclasspath = Checkfatherdir(Syearpath) + Sclass;
            MakeNewDir(Sclasspath);

            string Snumpath = Checkfatherdir(Sclasspath) + Snum;
            MakeNewDir(Snumpath);
        }
        /// <summary>
        /// 从学生表读取全部用户数据Snum  Syear,Sclass
        /// 生成Ftp目录(目录顺序 学年->班级->学号)
        /// 如果某个学生转班，那会就重新生成该学生目录
        /// </summary>
        /// <returns></returns>
        public static string CreateFtpDir()
        {
            int existdir = 0;
            int createdir = 0;
            string msg = "";
            string mysql = "SELECT Snum,Syear,Sclass FROM Students";//读取用户数据
            LearnSite.BLL.Students st = new LearnSite.BLL.Students();
            DataSet ds = st.GetSqlList(mysql);
            int countis = ds.Tables[0].Rows.Count;
            string savepath = "~/FtpSpace/";
            MakeNewDir(savepath);
            if (countis > 0)
            {
                for (int i = 0; i < countis; i++)
                {
                    string Snum = ds.Tables[0].Rows[i]["Snum"].ToString();
                    string Syear= ds.Tables[0].Rows[i]["Syear"].ToString();
                    string Sclass = ds.Tables[0].Rows[i]["Sclass"].ToString();
                    if (Syear != "")
                    {
                        string Syearpath = savepath + Syear;
                        MakeNewDir(Syearpath);
                        if (Sclass != "")
                        {
                            string Sclasspath = Checkfatherdir(Syearpath) + Sclass;
                            MakeNewDir(Sclasspath);
                            if (Snum != "")
                            {
                                string Snumpath = Checkfatherdir(Sclasspath) + Snum;
                                if(MakeDir(Snumpath))
                                {
                                existdir+=1;
                                }
                                else
                                {
                                createdir += 1;
                                }
                            }
                        }
                    }
                }
                msg = "用户数为" + countis.ToString() + "位  新建目录" + createdir.ToString() + "个 已经存在"+existdir+"个";

            }
            else
            {
                msg = "没有导入用户数据！";
            }
            return msg;
        }

        private static string Checkfatherdir(string str)
        {
            if (!str.EndsWith("/"))
            {
                str+= "/";
            }
            return str;
        }


        /// <summary>
        /// 清理所有不允许上传的目录和文件
        /// </summary>
        public static void DelAllOtherDir()
        {
            string mysql = "SELECT HomeDir FROM User_accounts ";
            DataSet ds = LearnSite.Ftp.FtpDb.Query(mysql);
            int countis = ds.Tables[0].Rows.Count;
            if (countis > 0)
            {
                for (int i = 0; i < countis; i++)
                {
                    string HomeDir = ds.Tables[0].Rows[i]["HomeDir"].ToString();
                    DelOtherDir(HomeDir);
                }
            }

        }

        /// <summary>
        /// 递归调用自己，直接删除目录下的非法文件及文件夹
        /// </summary>
        /// <param name="strDir">目录地址</param>
        private static void DelOtherDir(string strDir)
        {
            if (Directory.Exists(strDir))
            {
                string[] strDirs = Directory.GetDirectories(strDir);
                string[] strFiles = Directory.GetFiles(strDir);
                foreach (string strFile in strFiles)
                {
                    bool isdel = true;
                    string[] strType = GetFileType();
                    foreach (string myType in strType)
                    {
                        if (myType == GetSingleFileType(strFile))
                        {
                            isdel = false;
                        }
                    }
                    if (isdel)
                    {
                        File.Delete(strFile);
                    }
                }

                foreach (string strdir in strDirs)
                {
                    bool isdel = true;
                    string[] DirType = GetDirType();
                    foreach (string myType in DirType)
                    {
                        if (myType == GetDirName(strdir))
                        {
                            DelOtherDir(strdir);
                            isdel = false;
                        }
                    }
                    if (isdel)
                    {

                        Directory.Delete(strdir, true);
                    }
                }

            }

        }
        /// <summary>
        /// 获取目录路径中的目录名
        /// </summary>
        /// <param name="myDir"></param>
        /// <returns></returns>
        private static string GetDirName(string myDir)
        {
            return myDir.Substring(myDir.LastIndexOf("\\") + 1).ToLower();
        }
        /// <summary>
        /// 获取允许目录名
        /// </summary>
        /// <returns></returns>
        private static string[] GetDirType()
        {
            string AllDirType = LearnSite.Common.XmlHelp.AllowDir();
            AllDirType = AllDirType.Replace(" ", "");

            string[] GetDirTypes = AllDirType.Split(new char[] { '|' });

            return GetDirTypes;
        }
        /// <summary>
        /// 获取允许文件的后缀名
        /// </summary>
        /// <returns></returns>
        private static string[] GetFileType()
        {
            string AllFileType = LearnSite.Common.XmlHelp.AllowFileType();
            AllFileType = AllFileType.Replace(" ", "");
            string[] GetFileTypes = AllFileType.Split(new char[] { '|' });
            return GetFileTypes;
        }

        /// <summary>
        /// 获取文件名后缀
        /// </summary>
        /// <param name="myfile"></param>
        /// <returns></returns>
        private static string GetSingleFileType(string myfile)
        {
            return myfile.Substring(myfile.LastIndexOf(".") + 1).ToLower();
        }
    }
}