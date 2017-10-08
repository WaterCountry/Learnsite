using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
namespace LearnSite.DBUtility
{
    /// <summary>
    ///DbBackup 的摘要说明
    /// </summary>
    public class DbBackup
    {
        public DbBackup()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        static string savedir = "~/BackupDb";
        /// <summary>
        /// 备份列表
        /// </summary>
        /// <returns></returns>
        public static ArrayList Dblist()
        {
            ArrayList arl = new ArrayList();
            string saverealpath = HttpContext.Current.Server.MapPath(savedir);
            if (Directory.Exists(saverealpath))
            {
                DirectoryInfo di = new DirectoryInfo(saverealpath);
                FileInfo[] fis = di.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    arl.Add(fi.Name);
                }
            }
            return arl;
        }

        /// <summary>
        /// 判断今天有无备份
        /// </summary>
        /// <returns></returns>
        public static bool IsTodayBackUp()
        {
            bool isright = false;
            string saverealpath = HttpContext.Current.Server.MapPath(savedir);
            DateTime dt = DateTime.Now;

            if (Directory.Exists(saverealpath))
            {
                DirectoryInfo di = new DirectoryInfo(saverealpath);
                FileInfo[] fis = di.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    if (dt.Day == fi.LastWriteTime.Day && dt.Month == fi.LastWriteTime.Month)
                    {
                        isright = true;
                        break;
                    }
                }
            }
            return isright;
        }

        /// <summary>
        /// 判断每周有无备份：返回真则有备份
        /// </summary>
        /// <returns></returns>
        public static bool IsWeeksBackUp()
        {
            bool isright = false;
            string saverealpath = HttpContext.Current.Server.MapPath(savedir);
            DateTime dt1 = DateTime.Now.AddDays(-6);
            DateTime dtlimit = DateTime.Now.AddMonths(-6);
            if (Directory.Exists(saverealpath))
            {
                DirectoryInfo di = new DirectoryInfo(saverealpath);
                FileInfo[] fis = di.GetFiles();
                int fc = fis.Length;
                foreach (FileInfo fi in fis)
                {
                    DateTime fctime=fi.CreationTime;
                    if (DateTime.Compare(fctime, dt1) > 0)
                    {
                        isright = true;//如果备份日期大于上周日期，说明有备份
                        if (fc > 3)
                        {
                            if (DateTime.Compare(fctime, dtlimit) < 0)
                            {
                                fi.Delete();//如果备份数大于3个，且存在6个月前的数据库，则自动删除
                            }
                        }
                    }
                }
            }
            return isright;
        }
        /// <summary>
        /// 获取当前目录下的文件列表
        /// </summary>
        /// <returns></returns>
        public static DataView BackUpFileList()
        {
            return FileList(savedir);
        }
        /// <summary>
        /// 获取子目录中日期最新的这个
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static string GetLastDir(string dir)
        {
            string result = "";
            if (Directory.Exists(dir))
            {
                DirectoryInfo di = new DirectoryInfo(dir);
                DirectoryInfo[] dilist = di.GetDirectories();
                DateTime newdt = DateTime.Parse("2002-01-01");
                foreach (DirectoryInfo d in dilist)
                {
                    DateTime dirdate = d.LastWriteTime;
                    if (DateTime.Compare(dirdate, newdt) > 0)
                    {
                        result = d.Name;
                        newdt = dirdate;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取当前目录下的文件列表，返回ds数据集，包含fid,fname,fsize,furl四个字段
        /// </summary>
        /// <param name="strdir">虚拟路径</param>
        /// <returns></returns>
        public static DataView FileList(string strdir)
        {
            if (!string.IsNullOrEmpty(strdir))
            {
                string saverealpath = HttpContext.Current.Server.MapPath(strdir);
                DataView dv = new DataView();
                DataSet ds = new DataSet();
                ds.Tables.Add();
                ds.Tables[0].TableName = "filetable";
                ds.Tables[0].Columns.Add("fid", typeof(Int32));
                ds.Tables[0].Columns.Add("fname", typeof(String));
                ds.Tables[0].Columns.Add("fsize", typeof(String));
                ds.Tables[0].Columns.Add("furl", typeof(String));
                ds.Tables[0].Columns.Add("fread", typeof(String));
                ds.Tables[0].Columns.Add("fdate", typeof(DateTime));

                if (Directory.Exists(saverealpath))
                {
                    DirectoryInfo di = new DirectoryInfo(saverealpath);
                    FileInfo[] fis = di.GetFiles();
                    int i = 0;
                    if (!strdir.EndsWith("/"))
                        strdir += "/";
                    foreach (FileInfo fi in fis)
                    {
                        DataRow row;
                        row = ds.Tables[0].NewRow();
                        i++;
                        row[1] = fi.Name;
                        row[2] = (fi.Length / 1024).ToString() + "kb";
                        row[3] = strdir + fi.Name;
                        row[4] = fi.IsReadOnly.ToString().Substring(0, 1);
                        row[5] = fi.CreationTime;
                        ds.Tables[0].Rows.Add(row);
                    }
                    ds.AcceptChanges();
                    dv = ds.Tables[0].DefaultView;
                    dv.Sort = "fdate desc";
                }
                ds.Dispose();
                return dv;
            }

            else
            {
                return null;
            }
        }
                
        /// <summary>
        /// 获取备份数据库名称
        /// </summary>
        /// <returns></returns>
        public static string GetMyDbName()
        {
            string connstr = SqlHelper.connectionString;
            string[] spstr = connstr.Split(';');
            string[] dbname = spstr[1].Split('=');
            return dbname[1].Trim();
        }
        /// <summary>
        ///以当前日期为文件名，数据库备份
        /// </summary>
        /// <returns></returns>
        public static string BakupMyDb()
        {
            string connstr = SqlHelper.connectionString;
            string saverealpath = HttpContext.Current.Server.MapPath(savedir);
            if (!Directory.Exists(saverealpath))
            {
                Directory.CreateDirectory(saverealpath);
            }
            string OldDbName = GetMyDbName();
            DateTime dt = DateTime.Now;
            string BackDbName = savedir + "/" + dt.Year.ToString() + "_" + dt.Month.ToString() + "_" + dt.Day.ToString() + "_" + dt.Hour.ToString() + "_" + dt.Minute.ToString() + ".bak";
            string BackDbNamePath = HttpContext.Current.Server.MapPath(BackDbName);
            if (File.Exists(BackDbNamePath))
            {
                File.Delete(BackDbNamePath);
                System.Threading.Thread.Sleep(200);
            }
            string SqlBackStr = "backup database " + OldDbName + " to  disk='" + BackDbNamePath + "'";
            using (SqlConnection con = new SqlConnection(connstr))
            {
                con.Open();
                try
                {
                    SqlCommand com = new SqlCommand(SqlBackStr, con);
                    com.ExecuteNonQuery();
                    return "数据库备份成功！";
                }
                catch (Exception error)
                {
                    con.Close();
                    return "数据库备份失败！" + error.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        /// <summary>   
        /// 第一种还原数据库文件方法，测试成功！
        /// </summary>   
        /// <param name="dbFile">数据库备份文件（含路径）</param>   
        /// <returns></returns>   
        public static string RestoreMyDb(string dbFileUrl)
        {
            string msg = "无信息";
            string dbFile = HttpContext.Current.Server.MapPath(dbFileUrl);
            if (File.Exists(dbFile))
            {
                //sql数据库名   
                string dbName = GetMyDbName();
                //创建连接对象   
                string connstr = SqlHelper.connectionString;
                using (SqlConnection conn = new SqlConnection(connstr))
                {
                    //还原指定的数据库文件   
                    string sql = string.Format("use master ;declare @s varchar(8000);select @s=isnull(@s,'')+' kill '+rtrim(spID) from master..sysprocesses where dbid=db_id('{0}');select @s;exec(@s) ;RESTORE DATABASE {1} FROM DISK = N'{2}' with replace", dbName, dbName, dbFile);
                    SqlCommand sqlcmd = new SqlCommand(sql, conn);
                    sqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        conn.Open();
                        sqlcmd.ExecuteNonQuery();
                        msg = "数据库恢复成功！";
                    }
                    catch
                    {
                        msg = "数据库恢复失败！";
                    }
                }
            }
            else
            {
                msg = "备份文件不存在！";
            }

            return msg;
        }
        /// <summary>
        /// 第二种数据库还原方法
        /// </summary>
        /// <param name="dbFileUrl"></param>
        /// <returns></returns>
        public static string RestoreDb(string dbFileUrl)
        {
            string msg = "无信息";
            string dbFile = HttpContext.Current.Server.MapPath(dbFileUrl);
            if (File.Exists(dbFile))
            {
                string connstr = SqlHelper.connectionString;
                string DBName = GetMyDbName();
                //使远程数据库转入单用户模式，断开所有已连接数据库的用户的连接并回退它们的事务。
                string RecoveryStr = string.Format("Alter DATABASE {0} set single_user with rollback immediate use master RESTORE DATABASE {1} from disk = N'{2}' with replace", DBName, DBName, dbFile);
                using (SqlConnection conn = new SqlConnection(connstr))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand comm1 = new SqlCommand(RecoveryStr, conn);
                        comm1.ExecuteNonQuery(); //执行远程数据库恢复命令 

                        msg = "数据库还原成功!";
                        RecoveryStr = "Alter DATABASE " + DBName + " set multi_user";
                        SqlCommand comm2 = new SqlCommand(RecoveryStr, conn);
                        comm2.ExecuteNonQuery();
                        //使远程数据库转入多用户模式 
                    }
                    catch
                    {
                        RecoveryStr = "Alter DATABASE " + DBName + " set multi_user";
                        SqlCommand comm2 = new SqlCommand(RecoveryStr, conn);
                        comm2.ExecuteNonQuery();
                        msg = "数据库还原失败!数据库的平台版本不一致.";
                    }
                }
            }
            return msg;
        }


    }
}