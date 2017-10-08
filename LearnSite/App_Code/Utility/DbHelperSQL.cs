using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
namespace LearnSite.DBUtility
{
    /// <summary>
    /// ClassDB 的摘要说明
    /// </summary>
    public class DbHelperSQL
    {
        
        public DbHelperSQL()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公用方法
        /// <summary>
        /// 查询表中记录总数
        /// </summary>
        /// <param name="Tablename"></param>
        /// <returns></returns>
        public static int TableCounts(string Tablename)
        {
            int Fcount = 0;
            if (TabExists(Tablename))
            {
                string strSql = "select count(*) as records from " + Tablename;
                string fstr = FindString(strSql);
                if (fstr!="")
                {
                    Fcount = Int32.Parse(fstr);
                }
            }
            return Fcount;
        }
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public static bool ColumnExists(string tableName, string columnName)
        {
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object res =SqlHelper.GetSingleNo(sql);
            if (res == null)
            {
                return false;
            }
            return Convert.ToInt32(res) > 0;
        }

        public static void AddColumn(string tableName, string columnName, string columnType, int defaultvalue)
        {
            StringBuilder str = new StringBuilder();
            string aa = " alter table  "+tableName+" add "+columnName+" "+ columnType;
            str.Append(aa);
            if (defaultvalue !=-1)
            {
                string bb = " default "+defaultvalue;
                str.Append(bb);
            }
            
            ExecuteSql(str.ToString());
        }

        public static void AddPrimaryAutoColumn(string tableName, string columnName)
        {
            string aa = " alter table  " + tableName + " add " + columnName +" int  IDENTITY (1, 1)  primary key not null  ";

            ExecuteSql(aa);
        }
        /// <summary>
        /// 从表中获得该字段最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = SqlHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        ///根据条件从表中获得该字段最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static long FieldMaxValue(string FieldName, string TableName,string strWhere)
        {
            string strsql = "select max(" + FieldName + ") from " + TableName+" where "+strWhere;
            object obj = SqlHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return long.Parse(obj.ToString());
            }
        }
        /// <summary>
        ///根据条件从表中获得该字段最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static long FieldMaxValueNoWhere(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ") from " + TableName ;
            object obj = SqlHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return long.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 从表中获得某字段最小值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int GetMinField(string FieldName, string TableName)
        {
            string strsql = "select min(" + FieldName + ") from " + TableName;
            object obj = SqlHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 从表中获得某字段最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int GetMaxField(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ") from " + TableName;
            object obj = SqlHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 根据查询符合条件的记录是否存在，存在则返回真
        /// sql语句形式为select count(1) from
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            object obj = SqlHelper.GetSingle(strSql);
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
        /// 表是否存在
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static bool TabExists(string TableName)
        {
            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            //string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
            object obj = SqlHelper.GetSingle(strsql);
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
        /// 得到记录数为1，存在返回true｜
        /// 得到记录数为0，不存在返回false
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = SqlHelper.GetSingle(strSql, cmdParms);
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

        
        #endregion


        /// <summary>
        /// 执行SQL语句的方法，返回一个数值表示此SqlCommand命令执行后影响的行数
        /// </summary>
        /// <param name="strSql"></param>
        public static void CreatIndex(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行SQL语句的方法，返回一个数值表示此SqlCommand命令执行后影响的行数
        /// </summary>
        /// <param name="strSql"></param>
        public static int ExecuteSql(string sql, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
            {
                conn.Open();
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql, cmdParms);
            }
        }
        /// <summary>
        /// 返回指定sql语句的dataset
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, params SqlParameter[] cmdParms)
        {
            DataSet ds=null;
            //定义对象资源保存的范围，一旦using范围结束，将释放对方所占的资源
            using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
            {
                ds=SqlHelper.ExecuteDataset(conn,CommandType.Text,sql,cmdParms);
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
            return SqlHelper.Query(SQLString, cmdParms);
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
            SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, cmdParms);
            while (dr.Read())
            {
                al.Add(dr.GetString(0));
            }
            dr.Close();
            cmd.Dispose();

            return al;
        }

        /// <summary>
        /// 返回SQL语句执行结果的第一行第一列的值，如果无返回空字符"" 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static string FindString(string strsql, params SqlParameter[] cmdParms)
        {
             object obj = SqlHelper.GetSingle(strsql,cmdParms);
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
        /// 返回SQL语句执行结果的第一行第一列的值，如果无返回0 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int  FindNum(string strsql, params SqlParameter[] cmdParms)
        {
            object obj = SqlHelper.GetSingle(strsql, cmdParms);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 返回SQL语句执行结果的第一行第一列的值，如果无返回0 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int FindNum(string strsql)
        {
            object obj = SqlHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 返回SQL语句执行结果的第一行第一列的值，如果无返回空字符"" 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static string FindString(string strsql,Parameter p)
        {
            object obj = SqlHelper.GetSingle(strsql);
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
            return SqlHelper.GetSingle(SQLString);
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            return SqlHelper.GetSingle(SQLString, cmdParms);
        }

                /// <summary>
        /// 执行查询语句（搜索指定Cid 的学案及活动），返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet CourseMissionQuery(int Cid)
        {
            return SqlHelper.CourseMissionQuery(Cid);
        }

        public static void UpdateStudentNewSid(string Snum,int newSid)
        {
            string mysql = "update TermTotal set Tsid="+newSid+"  where Tnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysql);

            string mysqla = "update Works set Wsid="+newSid+" where Wnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysqla);

            string mysqlb = "update SurveyFeedback set Fsid="+newSid+"  where Fnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysqlb);

            
            string mysqlc = "update TopicReply set Rsid="+newSid+"  where Rsnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysqlc);
                        
            string mysqld = "update Signin set Qsid="+newSid+" where Qnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysqld);

            string mysqle = "update Result set Rsid="+newSid+" where Rnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysqle);

            string mysqlf = "update Ptyper set Psid="+newSid+"  where Psnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysqlf);

            string mysqlg = "update Pfinger set Psid="+newSid+" where Psnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysqlg);

            string mysqlh = "update GaugeFeedback set Fsid="+newSid+" where Fnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysqlh);

            string mysqli = "update Webstudy set Wsid="+newSid+" where Wnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysqli);

            string mysqlj = "update WorksDiscuss set Dsid="+newSid+" where Dsnum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysqlj);
        }
        
    }
}