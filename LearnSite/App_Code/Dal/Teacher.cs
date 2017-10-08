using System;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类Teacher。
	/// </summary>
	public class Teacher
	{
		public Teacher()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Hid", "Teacher"); 
		}
        /// <summary>
        /// 记录教师上课的电脑室名称
        /// </summary>
        /// <param name="Hid"></param>
        /// <param name="Hroom"></param>
        public void updateHroom(int Hid, string Hroom)
        {
            string mysql = "update Teacher set Hroom='" + Hroom + "' where Hid=" + Hid.ToString();
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 初始化昵称
        /// </summary>
        public void initHnick()
        {
            string mysql = "update Teacher set Hnick=Hname where Hnick is null";
            DbHelperSQL.ExecuteSql(mysql);
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Hid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Teacher");
			strSql.Append(" where Hid=@Hid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Hid", SqlDbType.Int,4)};
			parameters[0].Value = Hid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsHname(string Hname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Teacher");
            strSql.Append(" where Hname=@Hname ");
            SqlParameter[] parameters = {
					new SqlParameter("@Hname", SqlDbType.NVarChar,50)};
            parameters[0].Value = Hname;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Teacher model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Teacher(");
            strSql.Append("Hname,Hpwd,Hpermiss,Hnote,Hnick)");
            strSql.Append(" values (");
            strSql.Append("@Hname,@Hpwd,@Hpermiss,@Hnote,@Hnick)");
            strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Hname", SqlDbType.NVarChar,50),
					new SqlParameter("@Hpwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Hpermiss", SqlDbType.Bit,1),
					new SqlParameter("@Hnote", SqlDbType.NText),
					new SqlParameter("@Hnick", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Hname;
			parameters[1].Value = model.Hpwd;
			parameters[2].Value = model.Hpermiss;
            parameters[3].Value = model.Hnote;
            parameters[4].Value = model.Hnick;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			if (obj == null)
			{
				return 1;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Teacher model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Teacher set ");
            strSql.Append("Hname=@Hname,");
            strSql.Append("Hpwd=@Hpwd,");
            strSql.Append("Hpermiss=@Hpermiss,");
            strSql.Append("Hnote=@Hnote, ");
            strSql.Append("Hnick=@Hnick ");
            strSql.Append(" where Hid=@Hid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Hid", SqlDbType.Int,4),
					new SqlParameter("@Hname", SqlDbType.NVarChar,50),
					new SqlParameter("@Hpwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Hpermiss", SqlDbType.Bit,1),
					new SqlParameter("@Hnote", SqlDbType.NText),
					new SqlParameter("@Hnick", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Hid;
            parameters[1].Value = model.Hname;
            parameters[2].Value = model.Hpwd;
            parameters[3].Value = model.Hpermiss;
            parameters[4].Value = model.Hnote;
            parameters[5].Value = model.Hnick;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
        /// <summary>
        ///根据Hid 更新密码
        /// </summary>
        /// <param name="Hid"></param>
        /// <param name="Hpwd"></param>
        public void UpdatePwd(int Hid, string Hpwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Teacher set ");
            strSql.Append(" Hpwd=@Hpwd ");
            strSql.Append(" where Hid=@Hid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Hid", SqlDbType.Int,4),
					new SqlParameter("@Hpwd", SqlDbType.NVarChar,50)};
            parameters[0].Value = Hid;
            parameters[1].Value = Hpwd;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 给所有老师统计学案数
        /// </summary>
        public void UpdateHcountAll()
        {
            string sql = "select Hid from Teacher where Hdelete=0";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    string hid = dt.Rows[i]["Hid"].ToString();
                    UpdateHcount(hid);
                }
            }
        }
        /// <summary>
        /// 给一位老师统计学案数
        /// </summary>
        /// <param name="hid"></param>
        public void UpdateHcount(string hid)
        {
            int hcount = Ccount(hid);
            string mysql = "update Teacher set Hcount=" + hcount + " where Hid=" + hid;
            DbHelperSQL.ExecuteSql(mysql);
        }
        public int Ccount(string hid)
        {
            string strsql = "select count(*) from Courses where Cdelete=0 and Chid=" + hid;
            object obj = SqlHelper.GetSingle(strsql);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return 0;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Hid)
		{			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Teacher ");
			strSql.Append(" where Hid=@Hid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Hid", SqlDbType.Int,4)};
			parameters[0].Value = Hid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 删除标志一条数据
        /// </summary>
        public int DownTeacher(int Hid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Teacher set Hdelete=1 ");
            strSql.Append(" where Hid=@Hid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Hid", SqlDbType.Int,4)};
            parameters[0].Value = Hid;

           return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.Teacher GetModel(int Hid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Hid,Hname,Hpwd,Hpermiss,Hnote,Hnick,Hroom from Teacher ");
            strSql.Append(" where Hid=@Hid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Hid", SqlDbType.Int,4)};
            parameters[0].Value = Hid;

            LearnSite.Model.Teacher model = new LearnSite.Model.Teacher();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Hid"].ToString() != "")
                {
                    model.Hid = int.Parse(ds.Tables[0].Rows[0]["Hid"].ToString());
                }
                model.Hname = ds.Tables[0].Rows[0]["Hname"].ToString();
                model.Hpwd = ds.Tables[0].Rows[0]["Hpwd"].ToString();
                if (ds.Tables[0].Rows[0]["Hpermiss"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Hpermiss"].ToString() == "1") || (ds.Tables[0].Rows[0]["Hpermiss"].ToString().ToLower() == "true"))
                    {
                        model.Hpermiss = true;
                    }
                    else
                    {
                        model.Hpermiss = false;
                    }
                }
                model.Hnote = ds.Tables[0].Rows[0]["Hnote"].ToString();
                model.Hnick = ds.Tables[0].Rows[0]["Hnick"].ToString();
                model.Hroom = ds.Tables[0].Rows[0]["Hroom"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 根据姓名和密码得到一个教师对象实体，如果不存在返回null
        /// </summary>
        public LearnSite.Model.Teacher GetTeacherModel(string Hname, string Hpwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Hid,Hname,Hpwd,Hpermiss,Hnote,Hnick,Hroom from Teacher ");
            strSql.Append(" where Hname=@Hname and Hpwd=@Hpwd and Hdelete=0");
            SqlParameter[] parameters = {
					new SqlParameter("@Hname", SqlDbType.NVarChar,50),
					new SqlParameter("@Hpwd", SqlDbType.NVarChar,50)};
            parameters[0].Value = Hname;
            parameters[1].Value = Hpwd;

            LearnSite.Model.Teacher model = new LearnSite.Model.Teacher();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Hid"].ToString() != "")
                {
                    model.Hid = int.Parse(ds.Tables[0].Rows[0]["Hid"].ToString());
                }
                model.Hname = ds.Tables[0].Rows[0]["Hname"].ToString();
                model.Hpwd = ds.Tables[0].Rows[0]["Hpwd"].ToString();
                if (ds.Tables[0].Rows[0]["Hpermiss"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Hpermiss"].ToString() == "1") || (ds.Tables[0].Rows[0]["Hpermiss"].ToString().ToLower() == "true"))
                    {
                        model.Hpermiss = true;
                    }
                    else
                    {
                        model.Hpermiss = false;
                    }
                }
                model.Hnote = ds.Tables[0].Rows[0]["Hnote"].ToString();
                model.Hnick = ds.Tables[0].Rows[0]["Hnick"].ToString();
                model.Hroom = ds.Tables[0].Rows[0]["Hroom"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Hid,Hname,Hpwd,Hpermiss,Hnote,Hcount,Hnick,Hroom ");
            strSql.Append(" FROM Teacher ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得教师ID和姓名列表，非管理员
        /// </summary>
        public DataSet GetListHidHname()
        {
            string strSql = "select Hid,Hnick from Teacher where Hpermiss=0 and Hdelete=0 order by Hid asc ";          
            return DbHelperSQL.Query(strSql);
        }
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Hid,Hname,Hpwd,Hpermiss,Hnote ");
			strSql.Append(" FROM Teacher ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 设置该教师的学生空间当前教学子目录
        /// </summary>
        /// <param name="Hpath"></param>
        /// <param name="Hid"></param>
        public void SetHpath(string Hpath, int Hid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Teacher set Hpath=@Hpath where Hid=@Hid");
            SqlParameter[] parameters = {
					new SqlParameter("@Hpath", SqlDbType.NVarChar,50),
					new SqlParameter("@Hid", SqlDbType.Int,4)};
            parameters[0].Value = Hpath;
            parameters[1].Value = Hid;

            DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 返回该教师的学生空间当前教学子目录，无则返回空字符""
        /// </summary>
        /// <param name="Hid"></param>
        /// <returns></returns>
        public string GetHpath(string Hid)
        {
            string strSql = "select top 1 Hpath from Teacher where Hid=" + Hid;
            string res = DbHelperSQL.FindString(strSql);
            if (res.IndexOf('~') > -1)
            {
                SetHpath("", Int32.Parse(Hid));//重置为""，解决原未用字段残留问题
                return "";
            }
            else
                return res;
        }
        
        /// <summary>
        /// 返回该教师的学生空间当前教学子目录，无则返回空字符""
        /// </summary>
        /// <param name="Hid"></param>
        /// <returns></returns>
        public string GetHpathfroStu(int Sgrade,int Sclass)
        {
            string strSql = "select top 1 Hpath from Teacher,Room where Hid=Rhid and Rgrade=" + Sgrade + " and Rclass=" + Sclass;
            string res = DbHelperSQL.FindString(strSql);
            if (res.IndexOf('~') > -1)
            {
                return "";
            }
            else
                return res;
        }
		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Teacher";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法
	}
}

