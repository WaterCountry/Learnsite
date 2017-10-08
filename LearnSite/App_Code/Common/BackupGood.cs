using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data;

namespace LearnSite.Common
{
    /// <summary>
    ///BackupGood 的摘要说明
    /// </summary>
    public class BackupGood
    {
        public BackupGood()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// Wcid,Wmid,Wurl,Wname,Wgrade,Wclass,Wyear,Ctitle
        /// </summary>
        /// <returns></returns>
        public static string Backup()
        {
            string msg = "";
            int goodcount = 0;
            LearnSite.BLL.Works wbll = new BLL.Works();
            DataTable dt = wbll.GetListGoodWorks();
            goodcount = dt.Rows.Count;
            int existcount = 0;
            int creatcount = 0;
            int wrongcount = 0;
            int failcount = 0;

            if (goodcount > 0)
            {
                for (int i = 0; i < goodcount; i++)
                {
                    string Wyear = dt.Rows[i]["Wyear"].ToString();
                    string Wgrade = dt.Rows[i]["Wgrade"].ToString();
                    string Wclass = dt.Rows[i]["Wclass"].ToString();
                    string Wcid = dt.Rows[i]["Wcid"].ToString();
                    string Wmid = dt.Rows[i]["Wmid"].ToString();
                    string Wurl = dt.Rows[i]["Wurl"].ToString();
                    string Wname = dt.Rows[i]["Wname"].ToString();
                    string Ctitle = dt.Rows[i]["Ctitle"].ToString();
                    string savefname = Getsavefname(Wname, Wgrade, Wclass, Wmid, Wurl);
                    string savefpath = GoodStore(Wyear, Ctitle);
                    if (savefname != "" && savefpath != "")
                    {
                        string goodfname = savefpath + savefname;
                        if (!File.Exists(goodfname))
                        {
                            string oldpath = HttpContext.Current.Server.MapPath(Wurl);
                            if (File.Exists(oldpath))
                            {
                                try
                                {
                                    File.Copy(oldpath, goodfname);
                                    creatcount++;
                                }
                                catch
                                {
                                    failcount++;
                                    HttpContext.Current.Response.Write(goodfname);
                                }
                            }
                            else
                            {
                                wrongcount++;
                            }
                        }
                        else
                        {
                            existcount++;
                        }
                    }
                }
                msg = "推荐作品总数为" + goodcount.ToString() + " 备份成功" + creatcount.ToString() + "<br/>已经存在备份" + existcount.ToString() + " 原作品丢失" + wrongcount.ToString() + " 备份失败" + failcount.ToString();
            }
            else
            {
                msg = "推荐作品数为零!";
            }
            return msg;
        }
        private static string Getsavefname(string Wname, string Wgrade, string Wclass, string Wmid, string Wurl)
        {
            string ext = LearnSite.Common.WordProcess.getextions(Wurl);
            if (ext != "" && Wname != "")
            {
                string fname = Wname + "_" + Wgrade + "_" + Wclass + "_" + Wmid + ext;
                return fname;
            }
            else
                return "";
        }
        private static string GoodStore(string Wyear,string Ctitle)
        {
            string result = "";
            if (Wyear != "" && Ctitle != "")
            {
                string goodurl = "~/GoodStore/";
                string goodpath = HttpContext.Current.Server.MapPath(goodurl);
                if (!Directory.Exists(goodpath))
                    Directory.CreateDirectory(goodpath);//不存在则创建

                string yearurl = goodurl + Wyear + "/";
                string yearpath = HttpContext.Current.Server.MapPath(yearurl);
                if (!Directory.Exists(yearpath))
                    Directory.CreateDirectory(yearpath);//不存在则创建

                string ctitleurl = yearurl + LearnSite.Common.WordProcess.FilterSpecial(Ctitle) + "/";
                string ctitlepath = HttpContext.Current.Server.MapPath(ctitleurl);
                if (!Directory.Exists(ctitlepath))
                    Directory.CreateDirectory(ctitlepath);//不存在则创建
                result = ctitlepath;
            }
            return result;
        }

    }

}