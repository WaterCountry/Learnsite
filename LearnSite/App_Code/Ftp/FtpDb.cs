using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace LearnSite.Ftp
{
    /// <summary>
    ///FtpDb 的摘要说明
    /// </summary>
    public class FtpDb
    {
         public FtpDb()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

         /// <summary>
         /// 执行SQL语句的方法，返回一个数值表示此SqlCommand命令执行后影响的行数
         /// </summary>
         /// <param name="strSql"></param>
         public static int ExecuteSql(string sql, params SqlParameter[] cmdParms)
         {
             SqlCommand cmd = new SqlCommand();
             using (SqlConnection conn = new SqlConnection( FtpHelper.connectionString))
             {
                 
                 conn.Open();
                 return FtpHelper.ExecuteNonQuery(conn, CommandType.Text, sql, cmdParms);
             }
         }
         /// <summary>
         /// 返回指定sql语句的dataset
         /// </summary>
         /// <param name="strSql"></param>
         /// <returns></returns>
         public static DataSet GetDataSet(string sql, params SqlParameter[] cmdParms)
         {
             DataSet ds = null;
             //定义对象资源保存的范围，一旦using范围结束，将释放对方所占的资源
             using (SqlConnection conn = new SqlConnection(FtpHelper.connectionString))
             {
                 //打开连接
                 conn.Open();
                 ds = FtpHelper.ExecuteDataset(conn, CommandType.Text, sql, cmdParms);
             }

             return ds;
         }

         /// <summary>
         /// 执行查询语句，返回DataSet
         /// </summary>
         /// <param name="SQLString">查询语句</param>
         /// <returns>DataSet</returns>
         public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
         {
             return FtpHelper.Query(SQLString, cmdParms);
         }

         /// <summary>
         /// 执行一条sql语句，获取每一个查询字段记录集，返回arraylist
         /// </summary>
         /// <param name="sql"></param>
         /// <returns></returns>
         public static ArrayList ExecuteSqlArrayList(string sql, params SqlParameter[] cmdParms)
         {
             ArrayList al = new ArrayList();
             SqlCommand cmd = new SqlCommand();
             SqlDataReader dr = FtpHelper.ExecuteReader(CommandType.Text, sql, cmdParms);
             while (dr.Read())
             {
                 al.Add(dr.GetString(0));
             }
             dr.Close();
             cmd.Dispose();

             return al;
         }
         /// <summary>
         /// 执行一条sql语句，返回bool值，判断该记录是否存在
         /// </summary>
         /// <param name="sql"></param>
         /// <returns></returns>
         public static bool ExecuteSqlExist(string sql, params SqlParameter[] cmdParms)
         {
             SqlCommand cmd = new SqlCommand();
             SqlDataReader dr = FtpHelper.ExecuteReader(CommandType.Text, sql, cmdParms);
             if (dr.Read())
             {
                 dr.Close();
                 cmd.Dispose();
                 return true;
             }
             else
             {
                 dr.Close();
                 cmd.Dispose();
                 return false;
             }
         }
         /// <summary>
         /// 返回SQL语句执行结果的第一行第一列的值，如果无返回空字符"" 
         /// </summary>
         /// <param name="strSql"></param>
         /// <returns></returns>
         public static string FindString(string strsql)
         {
             object obj = FtpHelper.GetSingle(strsql);
             if (obj == null)
             {
                 return "";
             }
             else
             {
                 return obj.ToString();
             }

         }
         /// <summary>
         /// 执行一条计算查询结果语句，返回查询结果（object）。
         /// </summary>
         /// <param name="SQLString">计算查询结果语句</param>
         /// <returns>查询结果（object）</returns>
         public static object GetSingle(string SQLString)
         {
             return FtpHelper.GetSingle(SQLString);
         }
         /// <summary>
         /// 执行一条计算查询结果语句，返回查询结果（object）。
         /// </summary>
         /// <param name="SQLString">计算查询结果语句</param>
         /// <returns>查询结果（object）</returns>
         public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
         {
             return FtpHelper.GetSingle(SQLString, cmdParms);
         }

         /// <summary>
         /// 表是否存在
         /// </summary>
         /// <param name="TableName"></param>
         /// <returns></returns>
         public static bool TabExists(string TableName)
         {
             string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
             //string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
             object obj = FtpHelper.GetSingle(strsql);
             int cmdresult;
             if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
             {
                 cmdresult = 0;
             }
             else
             {
                 cmdresult = int.Parse(obj.ToString());
             }
             if (cmdresult == 0)
             {
                 return false;
             }
             else
             {
                 return true;
             }
         }

        /// <summary>
        /// 对GridView进行数据绑定，无排序
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dg"></param>
        public static void BindGridView(string sql, GridView dg)
        {
            dg.DataSource = Query(sql);
            dg.DataBind();
        }

        //=================================================
        //功能描述：对datalist进行数据绑定，无排序
        //输入参数：sql，查询的SQL语句；dl，需要绑定的datalist控件
        //返回值：无
        //时间：2007.11.10
        //=================================================
        public static void Binddatalist(string sql, DataList dl)
        {
            dl.DataSource = Query(sql);
            dl.DataBind();

        }
        /// <summary>
        /// 对repeater进行数据绑定，无排序
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="rp"></param>
        public static void Bindrepeater(string sql, Repeater rp)
        {
            rp.DataSource = Query(sql);
            rp.DataBind();
        }
    }
}