using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
namespace LearnSite.Common
{
    /// <summary>
    ///Log 的摘要说明
    /// </summary>
    public class Log
    {
        private static string spstr = "_";
        public Log()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        ///根据log文件的相对路径，读log文件内容，返回字符串
        /// </summary>
        /// <param name="logurl">log文件的相对路径</param>
        /// <returns></returns>
        public static string readlog(string logurl)
        {
            string str = "内容为空";
            if (!string.IsNullOrEmpty(logurl))
            {
                string logpath = HttpContext.Current.Server.MapPath(logurl);
                if (File.Exists(logpath))
                {
                    str = File.ReadAllText(logpath, Encoding.GetEncoding("Utf-8"));
                }
            }
            return str;
        }

        public static void Addlog(string msgtype, string msg)
        {
            DateTime today = DateTime.Now;
            string myurl = HttpContext.Current.Request.Url.ToString();//获取当前网页url
            string logger = today.ToString() + " 页面:" + myurl + "\r\n\r\n" + "信息类型：" + msgtype + "\r\n\r\n" + msg;
            string logpath = GetLogFilename(today);
            FileStream fs = new FileStream(logpath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                //开始写入
                sw.Write(logger);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
            catch
            {
                HttpContext.Current.Response.Write("无法写log记录，请确认网站目录为everyone可读写!");
                sw.Close();
                fs.Close();
            }
        }
        private static string GetLogFilename(DateTime dt)
        {
            string logfilename = dt.Year + spstr + dt.Month + spstr + dt.Day + spstr + dt.Hour + spstr + dt.Minute + spstr + dt.Second + spstr + dt.Millisecond;
            string loggerpath = LogDir() + logfilename + ".log";
            return loggerpath;
        }
        /// <summary>
        /// 返回log记录的目录物理路径，带\
        /// </summary>
        /// <returns></returns>
        private static string LogDir()
        {
            string savelog = "~/Log/";
            string pathlog = HttpContext.Current.Server.MapPath(savelog);
            if (!Directory.Exists(pathlog))
                Directory.CreateDirectory(pathlog);
            return pathlog;
        }

        /// <summary>
        /// 获取当前目录下的文件列表，返回ds数据集，包含fid,furl,fdate四个字段
        /// </summary>
        /// <returns></returns>
        public static DataView FileList()
        {
            string strdir = "~/Log/";
            string saverealpath = HttpContext.Current.Server.MapPath(strdir);
            DataView dv = new DataView();
            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].TableName = "filetable";
            ds.Tables[0].Columns.Add("fid", typeof(Int32));
            ds.Tables[0].Columns.Add("furl", typeof(String));
            ds.Tables[0].Columns.Add("fnote", typeof(String));
            ds.Tables[0].Columns.Add("fdate", typeof(DateTime));

            if (Directory.Exists(saverealpath))
            {
                DirectoryInfo di = new DirectoryInfo(saverealpath);
                FileInfo[] fis = di.GetFiles();
                int i = 0;
                if (!strdir.EndsWith("/"))
                    strdir += "/";
                int snd = DateTime.Now.Millisecond;
                foreach (FileInfo fi in fis)
                {
                    string fname = fi.Name;
                    if (IsRight(fname))
                    {
                        DataRow row;
                        row = ds.Tables[0].NewRow();
                        i++;
                        row[1] = strdir + fname;
                        row[2] = File.ReadAllText(fi.FullName);
                        row[3] = fi.CreationTime;
                        ds.Tables[0].Rows.Add(row);
                    }
                }
                ds.AcceptChanges();
            }
            dv = ds.Tables[0].DefaultView;
            dv.Sort = "fdate desc";
            ds.Dispose();
            return dv;
        }

        private static bool IsRight(string fname)
        {
            bool isok = false;
            string ext = GetExtions(fname).ToLower();
            switch (ext)
            {
                case ".log":
                case ".xml":
                    isok = true;
                    break;
            }
            return isok;
        }

        /// <summary>
        /// 取扩展名（如.jpg）含点
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string GetExtions(string filename)
        {
            int dian = filename.LastIndexOf('.');
            if (dian > 0)
                return filename.Substring(dian);
            else
                return "";
        }
    }
}