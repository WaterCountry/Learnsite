using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
namespace LearnSite.DBUtility
{
    /// <summary>
    ///DbLinkEdit 的摘要说明
    /// </summary>
    public class DbLinkEdit
    {
        public DbLinkEdit()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        
       public static  string myconnstr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;

       public static string serverNameFix()
       {
           string str = serverName();
           //str = str.ToLower().Replace("\\", "s").Replace("-", "l").Replace(".", "");
           //str = str.Replace("sqlexpress", "abcxyz");
           return Common.WordProcess.GetMD5(str);
       }
       public static string serverName()
       {
           string[] constr = ReadSqlConfig(myconnstr);
           return constr[0].ToString();
       }

        public static string[] ReadSqlConfig(string conStr)
        {
            string[] sc = new string[4];
            string[] conpl = conStr.Split(';');
            if (conpl.Length > 3)
            {
                sc[0] = spl(conpl[0]);
                sc[1] = spl(conpl[1]);
                sc[2] = spl(conpl[2]);
                sc[3] = spl(conpl[3]);
            }
            return sc;
        }
        /// <summary>
        /// 返回第一个
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        private static string spl(string ss)
        {
            string[] aa = ss.Split('=');
            return aa[1];
        }
        /// <summary>
        /// 修改web.config的连接数据库的字符串
        /// </summary>
        /// <param name="dbserver"></param>
        /// <param name="dbname"></param>
        /// <param name="dbuser"></param>
        /// <param name="dbpwd"></param> 
        public static string WriteSqlConfig(string namevalue, string dbserver, string dbname, string dbuser, string dbpwd)
        {
            //加载配置文件
            string webconfigpath = HttpContext.Current.Server.MapPath("~/web.config");
            string cs = "";
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.Load(webconfigpath);
            //修改连接字符串
            foreach (System.Xml.XmlNode Node in xmlDocument["configuration"]["connectionStrings"])
            {
                if (Node.Name == "add")
                {
                    if (Node.Attributes.GetNamedItem("name").Value == namevalue)
                    {
                        //参考Data Source=SSS-D4E6A63DAE8;Initial Catalog=LearnSiteDb;uid=sa;pwd=12345;
                        cs = String.Format("Data Source={0};Initial Catalog={1};uid={2};pwd={3};", dbserver, dbname, dbuser, dbpwd);
                        Node.Attributes.GetNamedItem("connectionString").Value = cs;
                    }
                }
            }
            xmlDocument.Save(webconfigpath);
            return cs;
        }
        /// <summary>
        /// 从脚本创建数据库
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdText"></param>
        public static void CreatSql(string connectionString, string cmdText)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (conn.State != ConnectionState.Open)//判断数据库连接状态
                        conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();//清空SqlCommand中的参数列表
                        conn.Close();
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stype"></param>
        /// <returns></returns>
        public static ArrayList readsqltxt(string stype)
        {
            ArrayList sqlarr = new ArrayList();
            string sqlpath ="";
            switch (stype)
            {
                case "SqlServer":
                   sqlpath = HttpContext.Current.Server.MapPath("~/Sql/learnsite.sql");
                    break;
                case "Ftp":
                    sqlpath = HttpContext.Current.Server.MapPath("~/Sql/ftp.sql");
                    break;
            }
            if (File.Exists(sqlpath))
            {
                sqlarr = ExecuteSqlFile(sqlpath);
            }
            return sqlarr;
        }

        /// <summary>
        /// 判断数据库连接是否存在
        /// </summary>
        /// <returns></returns>
        public static bool DatabaseExist(string connectionString)
        {
            bool isok = true;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //判断数据库连接状态
                if (conn.State != ConnectionState.Open)
                    try
                    {
                        conn.Open();
                    }
                    catch
                    {
                        isok = false;
                    }
            }
            return isok;
        }

        /// <summary>
        /// 被网上复制过来的这个读脚本害的好惨。（原代码读取时漏掉脚本中的最后一段，所以正常的脚本最后要加一句GO）
        /// </summary>
        /// <param name="varFileName"></param>
        /// <returns></returns>
        private static ArrayList ExecuteSqlFile(string varFileName)
        {
            //
            // TODO:读取.sql脚本文件
            //
            StreamReader sr = File.OpenText(varFileName);//传入的是文件路径及完整的文件名
            ArrayList alSql = new ArrayList();           //每读取一条语名存入ArrayList
            StringBuilder str = new StringBuilder();

            //string commandText = "";
            string varLine = "";
            while (sr.Peek() > -1)
            {
                varLine = sr.ReadLine();
                if (varLine == "")
                {
                    continue;
                }
                if (varLine != "GO")
                {                    
                    str.Append(varLine);
                    str.Append(" ");
                    //commandText += varLine;
                   // commandText += " ";// "\r\n";
                }
                else
                {
                    alSql.Add(str.ToString());
                    str.Length = 0;
                    //alSql.Add(commandText);
                   // commandText = "";
                }
            }
            if (str.Length > 0)
            {
                alSql.Add(str.ToString());//修订（2011-10-30温州水乡）
            }
            sr.Close();
            return alSql;
        }

    }
}