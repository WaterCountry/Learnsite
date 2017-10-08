using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类Result。
	/// </summary>
	public class Result
	{
		public Result()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Rid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Result");
			strSql.Append(" where Rid=@Rid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Rid", SqlDbType.Int,4)};
			parameters[0].Value = Rid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsBynumdate(int Rsid,DateTime Rdate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Result");
            strSql.Append(" where Rsid=@Rsid and Rdate=@Rdate ");
            SqlParameter[] parameters = {
					new SqlParameter("@Rsid", SqlDbType.Int,4),
					new SqlParameter("@Rdate", SqlDbType.DateTime)};
            parameters[0].Value = Rsid;
            parameters[1].Value =Rdate;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LearnSite.Model.Result model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Result(");
            strSql.Append("Rnum,Rscore,Rdate,Rhistory,Rwrong,Rgrade,Rterm,Rsid)");
            strSql.Append(" values (");
            strSql.Append("@Rnum,@Rscore,@Rdate,@Rhistory,@Rwrong,@Rgrade,@Rterm,@Rsid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Rnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Rscore", SqlDbType.Int,4),
					new SqlParameter("@Rdate", SqlDbType.DateTime),
					new SqlParameter("@Rhistory", SqlDbType.NText),
					new SqlParameter("@Rwrong", SqlDbType.NText),
                    new SqlParameter("@Rgrade", SqlDbType.Int,4),
                    new SqlParameter("@Rterm", SqlDbType.Int,4),
					new SqlParameter("@Rsid", SqlDbType.Int,4)};
            parameters[0].Value = model.Rnum;
            parameters[1].Value = model.Rscore;
            parameters[2].Value = model.Rdate;
            parameters[3].Value = model.Rhistory;
            parameters[4].Value = model.Rwrong;
            parameters[5].Value = model.Rgrade;
            parameters[6].Value = model.Rterm;
            parameters[7].Value = model.Rsid;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新今天测验成绩，返回影响的行数
        /// </summary>
        /// <param name="Rnum"></param>
        /// <param name="Rscore"></param>
        /// <param name="Rdate"></param>
        /// <returns></returns>
        public int UpdateGood(string Rnum, int Rscore, DateTime Rdate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Result set Rscore=@Rscore");
            strSql.Append(" where Rnum=@Rnum and Rscore<@Rscore and Rdate=@Rdate ");

            SqlParameter[] parameters = {
					new SqlParameter("@Rnum", SqlDbType.NVarChar,50),
                    new SqlParameter("@Rscore", SqlDbType.Int,4),
					new SqlParameter("@Rdate", SqlDbType.DateTime)};
            parameters[0].Value = Rnum;
            parameters[1].Value = Rscore;
            parameters[2].Value = Rdate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据中的Rscore，Rhistory，Rwrong
        /// </summary>
        public bool UpdateToday(LearnSite.Model.Result model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Result set ");
            strSql.Append("Rscore=@Rscore,");
            strSql.Append("Rhistory=@Rhistory,");
            strSql.Append("Rwrong=@Rwrong");
            strSql.Append(" where Rsid=@Rsid and Rscore<@Rscore and Rdate=@Rdate");
            SqlParameter[] parameters = {
					new SqlParameter("@Rsid", SqlDbType.Int,4),
					new SqlParameter("@Rscore", SqlDbType.Int,4),
					new SqlParameter("@Rdate", SqlDbType.DateTime),
					new SqlParameter("@Rhistory", SqlDbType.NText),
					new SqlParameter("@Rwrong", SqlDbType.NText),
					new SqlParameter("@Rid", SqlDbType.Int,4)};
            parameters[0].Value = model.Rsid;
            parameters[1].Value = model.Rscore;
            parameters[2].Value = model.Rdate;
            parameters[3].Value = model.Rhistory;
            parameters[4].Value = model.Rwrong;
            parameters[5].Value = model.Rid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(LearnSite.Model.Result model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Result set ");
            strSql.Append("Rnum=@Rnum,");
            strSql.Append("Rscore=@Rscore,");
            strSql.Append("Rdate=@Rdate,");
            strSql.Append("Rhistory=@Rhistory,");
            strSql.Append("Rwrong=@Rwrong");
            strSql.Append(" where Rid=@Rid");
            SqlParameter[] parameters = {
					new SqlParameter("@Rnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Rscore", SqlDbType.Int,4),
					new SqlParameter("@Rdate", SqlDbType.DateTime),
					new SqlParameter("@Rhistory", SqlDbType.NText),
					new SqlParameter("@Rwrong", SqlDbType.NText),
					new SqlParameter("@Rid", SqlDbType.Int,4)};
            parameters[0].Value = model.Rnum;
            parameters[1].Value = model.Rscore;
            parameters[2].Value = model.Rdate;
            parameters[3].Value = model.Rhistory;
            parameters[4].Value = model.Rwrong;
            parameters[5].Value = model.Rid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Rid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Result ");
			strSql.Append(" where Rid=@Rid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Rid", SqlDbType.Int,4)};
			parameters[0].Value = Rid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 清除几年前的测验记录
        /// </summary>
        /// <param name="Wyear"></param>
        public int DeleteOldyear(int Wyear)
        {
            DateTime olddate = DateTime.Now.AddYears(-Wyear);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Result ");
            strSql.Append(" where Rdate <@Rdate");
            SqlParameter[] parameters = {
					new SqlParameter("@Rdate", SqlDbType.DateTime)};
            parameters[0].Value = olddate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public LearnSite.Model.Result GetModel(int Rid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Rid,Rnum,Rscore,Rdate,Rhistory,Rwrong,Rgrade,Rterm from Result ");
            strSql.Append(" where Rid=@Rid");
            SqlParameter[] parameters = {
					new SqlParameter("@Rid", SqlDbType.Int,4)
};
            parameters[0].Value = Rid;

            LearnSite.Model.Result model = new LearnSite.Model.Result();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Rid"].ToString() != "")
                {
                    model.Rid = int.Parse(ds.Tables[0].Rows[0]["Rid"].ToString());
                }
                model.Rnum = ds.Tables[0].Rows[0]["Rnum"].ToString();
                if (ds.Tables[0].Rows[0]["Rscore"].ToString() != "")
                {
                    model.Rscore = int.Parse(ds.Tables[0].Rows[0]["Rscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rdate"].ToString() != "")
                {
                    model.Rdate = DateTime.Parse(ds.Tables[0].Rows[0]["Rdate"].ToString());
                }
                model.Rhistory = ds.Tables[0].Rows[0]["Rhistory"].ToString();
                model.Rwrong = ds.Tables[0].Rows[0]["Rwrong"].ToString();
                if (ds.Tables[0].Rows[0]["Rgrade"].ToString() != "")
                {
                    model.Rgrade = int.Parse(ds.Tables[0].Rows[0]["Rgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rterm"].ToString() != "")
                {
                    model.Rterm = int.Parse(ds.Tables[0].Rows[0]["Rterm"].ToString());
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
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select Rid,Rnum,Rscore,Rdate,Rhistory,Rwrong,Rgrade,Rterm ");
			strSql.Append(" FROM Result ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获取本班今天常识测验排行榜
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet GetListTodayScore(int Sgrade,int Sclass)
        {
            DateTime dt = DateTime.Now;
            string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            DateTime Rdate = DateTime.Parse(today);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sgrade,Sclass,Snum,Sname,Rscore from Result,Students ");
            strSql.Append(" where Snum=Rnum and Sgrade=@Sgrade and Sclass=@Sclass and Rdate=@Rdate ");
            strSql.Append(" order by Rscore DESC ");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4),
                    new SqlParameter("@Rdate", SqlDbType.DateTime)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;
            parameters[2].Value = Rdate;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取本班今天常识测验未进行的同学姓名
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string GetListTodayNoScore(int Sgrade, int Sclass)
        {
            string restr = "";
            DateTime dtm = DateTime.Now;
            string today = dtm.Year.ToString() + "-" + dtm.Month.ToString() + "-" + dtm.Day;
            DateTime Rdate = DateTime.Parse(today);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sname from Students ");
            strSql.Append(" where Sgrade=@Sgrade and Sclass=@Sclass and Snum not in( ");
            strSql.Append(" select Rnum from Result where Rdate=@Rdate )");
            strSql.Append(" order by Snum");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4),
                    new SqlParameter("@Rdate", SqlDbType.DateTime)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;
            parameters[2].Value = Rdate;

            DataSet ds= DbHelperSQL.Query(strSql.ToString(), parameters);
            DataTable dt = ds.Tables[0];
            int cn = dt.Rows.Count;
            if (cn > 0)
            {
                for (int i=0;i<cn;i++)
                {
                    restr = restr + dt.Rows[i]["Sname"].ToString()+"、";
                    if ((i+1) % 8 == 0)
                    {
                        restr = restr + "<br/>";
                    }
                }
            }
            return restr;
        }
        /// <summary>
        /// 获得该学号的测验数据列表
        /// </summary>
        /// <param name="Rnum"></param>
        /// <returns></returns>
        public DataSet GetListScore(string Rnum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Rid, convert(nvarchar(10),Rdate,120) as Rdate,Rscore,Rgrade,Rterm from Result ");
            strSql.Append(" where Rnum=@Rnum ");
            strSql.Append(" order by Rid DESC ");
            SqlParameter[] parameters = {
					new SqlParameter("@Rnum", SqlDbType.NVarChar,50)};
            parameters[0].Value = Rnum;
            
            return DbHelperSQL.Query(strSql.ToString(),parameters);
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
            strSql.Append(" Rid,Rnum,Rscore,Rdate,Rhistory,Rwrong,Rgrade,Rterm ");
			strSql.Append(" FROM Result ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 根据学号求测验的总平均值
        /// </summary>
        /// <param name="Rnum"></param>
        /// <returns></returns>
        public void GetAverage(int Rsid, int Rgrade, int Rterm)
        {
            string strSql = "update Students set Squiz=(select avg(Rscore) as Squiz from Result where Rsid=" + Rsid + " and Rgrade=" + Rgrade + " and Rterm=" + Rterm+") where Sid="+Rsid;
            DbHelperSQL.ExecuteSql(strSql);
        }
        /// <summary>
        /// 根据学号求测验的最高值
        /// </summary>
        /// <param name="Rnum"></param>
        /// <returns></returns>
        public int GetMax(int Rsid,string Rgrade,string Rterm)
        {
            string strSql = "select Rscore  from Result where Rsid=" + Rsid + " and Rterm=" + Rterm + " and Rgrade=" + Rgrade +" order by  Rscore desc";
            string findStr = DbHelperSQL.FindString(strSql);
            if (findStr == "")
            {
                return 0;
            }
            else
            {
                return Int32.Parse(findStr);
            }        
        }

        /// <summary>
        /// 获取本次测验试卷编号记录
        /// </summary>
        /// <param name="Rid"></param>
        /// <returns></returns>
        public string GetRhistory(int Rid)
        {
            string mysql = "select Rhistory from Result where Rid="+Rid;
            return DbHelperSQL.FindString(mysql);
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
			parameters[0].Value = "Result";
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

