using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类Ptyper。
	/// </summary>
	public class Ptyper
	{
		public Ptyper()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Pid", "Ptyper"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Pid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Ptyper");
			strSql.Append(" where Pid=@Pid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4)};
			parameters[0].Value = Pid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsPsnum(int Psid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ptyper");
            strSql.Append(" where Psid=@Psid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Psid", SqlDbType.Int,4)};
            parameters[0].Value = Psid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Ptyper model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Ptyper(");
            strSql.Append("Ptid,Psnum,Pscore,Pdate,Pip,Ptype,Pdegree,Pgrade,Pterm,Psid)");
            strSql.Append(" values (");
            strSql.Append("@Ptid,@Psnum,@Pscore,@Pdate,@Pip,@Ptype,@Pdegree,@Pgrade,@Pterm,@Psid)");
            SqlParameter[] parameters = {
					new SqlParameter("@Ptid", SqlDbType.Int,4),
					new SqlParameter("@Psnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Pscore", SqlDbType.Int,4),
					new SqlParameter("@Pdate", SqlDbType.DateTime),
					new SqlParameter("@Pip", SqlDbType.NVarChar,50),
					new SqlParameter("@Ptype", SqlDbType.Int,4),
					new SqlParameter("@Pdegree", SqlDbType.Int,4),
                    new SqlParameter("@Pgrade", SqlDbType.Int,4),
                    new SqlParameter("@Pterm", SqlDbType.Int,4),
                    new SqlParameter("@Psid", SqlDbType.Int,4)};
            parameters[0].Value = model.Ptid;
            parameters[1].Value = model.Psnum;
            parameters[2].Value = model.Pscore;
            parameters[3].Value = model.Pdate;
            parameters[4].Value = model.Pip;
            parameters[5].Value = model.Ptype;
            parameters[6].Value = model.Pdegree;
            parameters[7].Value = model.Pgrade;
            parameters[8].Value = model.Pterm;
            parameters[9].Value = model.Psid;

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
		/// 更新一条数据，根据学号更新
		/// </summary>
        public int Update(LearnSite.Model.Ptyper model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ptyper set ");
            strSql.Append("Ptid=@Ptid,");
            strSql.Append("Pscore=@Pscore,");
            strSql.Append("Pdate=@Pdate,");
            strSql.Append("Pip=@Pip,");
            strSql.Append("Ptype=Ptype+1,");
            strSql.Append("Pdegree=@Pdegree,");
            strSql.Append("Pgrade=@Pgrade,");
            strSql.Append("Pterm=@Pterm");
            strSql.Append(" where Psid=@Psid and Pscore<@Pscore ");
            SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@Ptid", SqlDbType.Int,4),
					new SqlParameter("@Psid", SqlDbType.Int,4),
					new SqlParameter("@Pscore", SqlDbType.Int,4),
					new SqlParameter("@Pdate", SqlDbType.DateTime),
					new SqlParameter("@Pip", SqlDbType.NVarChar,50),
					new SqlParameter("@Pdegree", SqlDbType.Int,4),
                    new SqlParameter("@Pgrade", SqlDbType.Int,4),
                    new SqlParameter("@Pterm", SqlDbType.Int,4)};
            parameters[0].Value = model.Pid;
            parameters[1].Value = model.Ptid;
            parameters[2].Value = model.Psid;
            parameters[3].Value = model.Pscore;
            parameters[4].Value = model.Pdate;
            parameters[5].Value = model.Pip;
            parameters[6].Value = model.Pdegree;
            parameters[7].Value = model.Pgrade;
            parameters[8].Value = model.Pterm;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void UpdateStscore(int Psid,int Pscore)
        {
            string mysql = "update Students set Stscore="+Pscore+" where Sid="+Psid;
            DbHelperSQL.ExecuteSql(mysql);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Pid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Ptyper ");
			strSql.Append(" where Pid=@Pid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4)};
			parameters[0].Value = Pid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 清空所有打字记录
        /// </summary>
        public int DeleteAll()
        {
            string mysql = "delete from Ptyper";
            return  DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 清空所教班级学生的打字记录
        /// </summary>
        public void DeleteMyAll(int Rhid)
        {
            string mysql = "delete from Ptyper where Psnum in(select Snum from Students,Room where Sgrade=Rgrade and Sclass=Rclass and Rhid="+Rhid+")";
            DbHelperSQL.ExecuteSql(mysql);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Ptyper GetModel(int Pid)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Pid,Ptid,Psnum,Pscore,Pdate,Pip,Ptype,Pdegree,Pgrade,Pterm from Ptyper ");
            strSql.Append(" where Pid=@Pid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4)};
            parameters[0].Value = Pid;

            LearnSite.Model.Ptyper model = new LearnSite.Model.Ptyper();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Pid"].ToString() != "")
                {
                    model.Pid = int.Parse(ds.Tables[0].Rows[0]["Pid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ptid"].ToString() != "")
                {
                    model.Ptid = int.Parse(ds.Tables[0].Rows[0]["Ptid"].ToString());
                }
                model.Psnum = ds.Tables[0].Rows[0]["Psnum"].ToString();
                if (ds.Tables[0].Rows[0]["Pscore"].ToString() != "")
                {
                    model.Pscore = int.Parse(ds.Tables[0].Rows[0]["Pscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Pdate"].ToString() != "")
                {
                    model.Pdate = DateTime.Parse(ds.Tables[0].Rows[0]["Pdate"].ToString());
                }
                model.Pip = ds.Tables[0].Rows[0]["Pip"].ToString();
                if (ds.Tables[0].Rows[0]["Ptype"].ToString() != "")
                {
                    model.Ptype = int.Parse(ds.Tables[0].Rows[0]["Ptype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Pdegree"].ToString() != "")
                {
                    model.Pdegree = int.Parse(ds.Tables[0].Rows[0]["Pdegree"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Pgrade"].ToString() != "")
                {
                    model.Pdegree = int.Parse(ds.Tables[0].Rows[0]["Pgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Pterm"].ToString() != "")
                {
                    model.Pdegree = int.Parse(ds.Tables[0].Rows[0]["Pterm"].ToString());
                }
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
            strSql.Append("select Pid,Ptid,Psnum,Pscore,Pdate,Pip,Ptype,Pdegree,Pgrade,Pterm ");
            strSql.Append(" FROM Ptyper ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Pid,Ptid,Psnum,Pscore,Pdate,Pip,Ptype,Pdegree,Pgrade,Pterm ");
            strSql.Append(" FROM Ptyper ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 显示前60本篇打字英雄榜记录
        /// </summary>
        /// <param name="Tid"></param>
        /// <returns></returns>
        public DataSet ShowTypeScore(int Tid)
        {
            string mysql = "SELECT TOP 60 Psnum,Pid,Ptid,Sname,Pscore,Ptype  FROM Ptyper,Students WHERE Psnum=Snum and  Ptid=" + Tid + " ORDER BY Pscore DESC";
            return DbHelperSQL.Query(mysql);
        }

        /// <summary>
        ///  显示本班本篇文章打字英雄榜记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public DataSet ShowClassTidScore(int Sgrade, int Sclass,int Ptid)
        {
            string mysql = "SELECT Psnum,Pid,Ptid,Sname,Sgrade,Sclass,Pscore,Pdate,Pip,Ptype  FROM Ptyper,Students WHERE Ptid=" + Ptid + " and  Sgrade=" + Sgrade + " and Sclass=" + Sclass + "and Psnum=Snum  ORDER BY Pscore DESC,Pdate DESC";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        ///  显示本班打字英雄榜记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public DataSet ShowClassTypeScore(int Sgrade,int Sclass)
        {
            string mysql = "SELECT Psnum,Pid,Ptid,Sname,Sgrade,Sclass,Pscore,Pdate,Pip,Ptype  FROM Ptyper,Students WHERE Psnum=Snum and  Sgrade=" + Sgrade + " and Sclass=" + Sclass + "  ORDER BY Pscore DESC,Pdate DESC";
            return DbHelperSQL.Query(mysql);
        }

        /// <summary>
        /// 显示年级段打字英雄榜记录
        /// </summary>
        /// <param name="GVtyper"></param>
        public DataSet ShowAllTypeScore(int Sgrade)
        {
            string mysql = "SELECT Psnum,Ptid,Sname ,Sgrade,Sclass,Pscore,Pdate,Pip, Ptype from Ptyper,Students WHERE Psnum=Snum and Sgrade=" + Sgrade + " ORDER BY Pscore DESC,Pdate DESC";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 显示全校打字英雄榜记录
        /// </summary>
        /// <param name="GVtyper"></param>
        public DataSet ShowSchoolTypeScore()
        {
            string mysql = "SELECT  Psnum,Ptid,Sname ,Sgrade,Sclass,Pscore,Pdate,Pip, Ptype from Ptyper,Students WHERE Psnum=Snum  ORDER BY Pscore  DESC,Pdate DESC";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 检验今天本班本机除本人外是否有别人账号打过字
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Pip"></param>
        /// <returns></returns>
        public bool ExistPtyper(int Sid, int Sgrade, int Sclass, string Pip)
        {
            DateTime dt = DateTime.Now;
            string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            string mysql = "select count(1) from Ptyper,Students where Psid=Sid and Sid<>" + Sid + " and Sgrade=" + Sgrade + " and Sclass=" + Sclass + " and Pip='" + Pip + "' and Pdate>'" + today + "'";
            return DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 清除超过指定速度的打字成绩
        /// </summary>
        /// <param name="Pscore"></param>
        public void DeleteOverScore(int Pscore, int Rhid)
        {
            string mysql = "delete from Ptyper where Pscore>" + Pscore + " and Psnum in(select Snum from Students,Room where Sgrade=Rgrade and Sclass=Rclass and Rhid=" + Rhid + ")";
            DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 显示年级段打字英雄榜记录
        /// </summary>
        /// <param name="GVtyper"></param>
        public DataSet ShowTopTypeScore(int Sgrade,int ntop)
        {
            string mysql = "SELECT top " + ntop.ToString() + "  Psnum,Sname,Sgrade,Sclass ,Pscore from Ptyper,Students WHERE Pscore>0 and  Psnum=Snum and Sgrade=" + Sgrade + " ORDER BY Pscore DESC";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 显示全校打字英雄榜记录
        /// </summary>
        /// <param name="GVtyper"></param>
        public DataSet ShowSchoolTopTypeScore(int nTop)
        {
            string mysql = "SELECT top  " + nTop.ToString() + "  Psnum,Pdate,Sname,Sgrade,Sclass ,Pscore from Ptyper,Students WHERE Pscore>0 and  Psnum=Snum  ORDER BY Pscore DESC,Pdate DESC";
            return DbHelperSQL.Query(mysql);
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
			parameters[0].Value = "Ptyper";
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

