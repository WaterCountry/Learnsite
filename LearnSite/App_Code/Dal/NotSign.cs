using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类NotSign
	/// </summary>
	public class NotSign
	{
		public NotSign()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Nid", "NotSign"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Nid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from NotSign");
			strSql.Append(" where Nid=@Nid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Nid", SqlDbType.Int,4)};
			parameters[0].Value = Nid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在今天未签到记录
        /// </summary>
        public bool ExistsToday(string Nnum)
        {
            DateTime dt = DateTime.Now;
            int Nyear = dt.Year;
            int Nmonth = dt.Month;
            int Nday = dt.Day;
            string strSql = "select count(1) from NotSign where Nnum='" + Nnum + "' and Nyear=" + Nyear + " and Nmonth=" + Nmonth + " and Nday=" + Nday;
            
            return DbHelperSQL.Exists(strSql);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.NotSign model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into NotSign(");
			strSql.Append("Nnum,Ndate,Nyear,Nmonth,Nday,Nweek,Nnote,Ngrade,Nterm)");
			strSql.Append(" values (");
			strSql.Append("@Nnum,@Ndate,@Nyear,@Nmonth,@Nday,@Nweek,@Nnote,@Ngrade,@Nterm)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Nnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Ndate", SqlDbType.DateTime),
					new SqlParameter("@Nyear", SqlDbType.Int,4),
					new SqlParameter("@Nmonth", SqlDbType.Int,4),
					new SqlParameter("@Nday", SqlDbType.Int,4),
					new SqlParameter("@Nweek", SqlDbType.NVarChar,50),
					new SqlParameter("@Nnote", SqlDbType.NVarChar),
                    new SqlParameter("@Ngrade", SqlDbType.Int,4),
					new SqlParameter("@Nterm", SqlDbType.Int,4)};
			parameters[0].Value = model.Nnum;
			parameters[1].Value = model.Ndate;
			parameters[2].Value = model.Nyear;
			parameters[3].Value = model.Nmonth;
			parameters[4].Value = model.Nday;
			parameters[5].Value = model.Nweek;
			parameters[6].Value = model.Nnote;
            parameters[7].Value = model.Ngrade;
            parameters[8].Value = model.Nterm;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public void Update(LearnSite.Model.NotSign model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update NotSign set ");
			strSql.Append("Nnum=@Nnum,");
			strSql.Append("Ndate=@Ndate,");
			strSql.Append("Nyear=@Nyear,");
			strSql.Append("Nmonth=@Nmonth,");
			strSql.Append("Nday=@Nday,");
			strSql.Append("Nweek=@Nweek,");
			strSql.Append("Nnote=@Nnote");
            strSql.Append("Ngrade=@Ngrade");
            strSql.Append("Nterm=@Nterm");
			strSql.Append(" where Nid=@Nid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Nid", SqlDbType.Int,4),
					new SqlParameter("@Nnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Ndate", SqlDbType.DateTime),
					new SqlParameter("@Nyear", SqlDbType.Int,4),
					new SqlParameter("@Nmonth", SqlDbType.Int,4),
					new SqlParameter("@Nday", SqlDbType.Int,4),
					new SqlParameter("@Nweek", SqlDbType.NVarChar,50),
					new SqlParameter("@Nnote", SqlDbType.NVarChar),                    
                    new SqlParameter("@Ngrade", SqlDbType.Int,4),
					new SqlParameter("@Nterm", SqlDbType.Int,4)};
			parameters[0].Value = model.Nid;
			parameters[1].Value = model.Nnum;
			parameters[2].Value = model.Ndate;
			parameters[3].Value = model.Nyear;
			parameters[4].Value = model.Nmonth;
			parameters[5].Value = model.Nday;
			parameters[6].Value = model.Nweek;
			parameters[7].Value = model.Nnote;
            parameters[8].Value = model.Ngrade;
            parameters[9].Value = model.Nterm;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 更新备注一条数据
        /// </summary>
        public void UpdateNote(string Nnum,string Nnote)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update NotSign set ");  
            strSql.Append(" Nnote=@Nnote");
            strSql.Append(" where Nnum=@Nnum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Nnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Nnote", SqlDbType.NVarChar)};
            parameters[0].Value = Nnum;
            parameters[1].Value = Nnote;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Nid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from NotSign ");
			strSql.Append(" where Nid=@Nid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Nid", SqlDbType.Int,4)};
			parameters[0].Value = Nid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.NotSign GetModel(int Nid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Nid,Nnum,Ndate,Nyear,Nmonth,Nday,Nweek,Nnote,Ngrade,Nterm from NotSign ");
			strSql.Append(" where Nid=@Nid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Nid", SqlDbType.Int,4)};
			parameters[0].Value = Nid;

			LearnSite.Model.NotSign model=new LearnSite.Model.NotSign();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Nid"].ToString()!="")
				{
					model.Nid=int.Parse(ds.Tables[0].Rows[0]["Nid"].ToString());
				}
				model.Nnum=ds.Tables[0].Rows[0]["Nnum"].ToString();
				if(ds.Tables[0].Rows[0]["Ndate"].ToString()!="")
				{
					model.Ndate=DateTime.Parse(ds.Tables[0].Rows[0]["Ndate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Nyear"].ToString()!="")
				{
					model.Nyear=int.Parse(ds.Tables[0].Rows[0]["Nyear"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Nmonth"].ToString()!="")
				{
					model.Nmonth=int.Parse(ds.Tables[0].Rows[0]["Nmonth"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Nday"].ToString()!="")
				{
					model.Nday=int.Parse(ds.Tables[0].Rows[0]["Nday"].ToString());
				}
				model.Nweek=ds.Tables[0].Rows[0]["Nweek"].ToString();
				model.Nnote=ds.Tables[0].Rows[0]["Nnote"].ToString();
                if (ds.Tables[0].Rows[0]["Ngrade"].ToString() != "")
                {
                    model.Ngrade = int.Parse(ds.Tables[0].Rows[0]["Ngrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Nterm"].ToString() != "")
                {
                    model.Nterm = int.Parse(ds.Tables[0].Rows[0]["Nterm"].ToString());
                }
				return model;
			}
			else
			{
				return null;
			}
		}
        /// <summary>
        /// 获取今天未签到备注
        /// </summary>
        /// <param name="Nnum"></param>
        /// <returns></returns>
        public string GetNoteToday(string Nnum)
        {
            DateTime dt = DateTime.Now;
            int Nyear = dt.Year;
            int Nmonth = dt.Month;
            int Nday = dt.Day;
            string mysql = "select Nnote from NotSign where Nnum='" + Nnum + "' and Nyear=" + Nyear + " and Nmonth=" + Nmonth + " and Nday=" + Nday;

            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 获取某天未签到备注
        /// </summary>
        /// <param name="Nnum"></param>
        /// <returns></returns>
        public string GetNoteThisday(string Nnum,int Nyear,int Nmonth,int Nday)
        {
            string mysql = "select Nnote from NotSign where Nnum='" + Nnum + "' and Nyear=" + Nyear + " and Nmonth=" + Nmonth + " and Nday=" + Nday;

            return DbHelperSQL.FindString(mysql);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select Nid,Nnum,Ndate,Nyear,Nmonth,Nday,Nweek,Nnote,Ngrade,Nterm ");
			strSql.Append(" FROM NotSign ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 根据学号获取缺席记录列表，按日期排序
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public DataSet NotSignSnumdetail(string Snum)
        {
            string mysql = "SELECT  Snum,Sgrade,Sclass,Sname,Sex,Sheadtheacher,Nnote,Ndate from Students,NotSign where Snum=Nnum and Snum='" + Snum + "' order by Ndate desc";
            return DbHelperSQL.Query(mysql);
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
            strSql.Append(" Nid,Nnum,Ndate,Nyear,Nmonth,Nday,Nweek,Nnote,Ngrade,Nterm ");
			strSql.Append(" FROM NotSign ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 增加字段初始化
        /// </summary>
        public void upgradefix()
        {
            string Nterm = LearnSite.Common.XmlHelp.GetTerm();

            string mysql = "update NotSign set Nterm="+Nterm+",Ngrade=Sgrade from NotSign,Students where Nnum=Snum and Nyear=2011 and Nmonth between 9 and 10";
            DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 某班级签到导出到Excel
        /// </summary>
        public void NotSignExcel(int Sgrade, int Sclass, int Nterm)
        {
            DateTime dt = DateTime.Now;
            string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            string FileName = Sgrade.ToString() + "_" + Sclass.ToString() + "_" + Nterm.ToString() + System.Web.HttpUtility.UrlEncode("缺席") + today;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Nnum as 学号, Sname as 姓名,Sgrade as 年级,Sclass as 班级,Nnote as 缺席原因,Ndate as 日期");
            strSql.Append(" FROM NotSign,Students ");
            strSql.Append(" WHERE Nnum=Snum AND Sgrade=Ngrade AND Sgrade=@Sgrade AND Sclass=@Sclass AND Nterm=@Nterm ORDER BY Nnum ASC");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4),
                    new SqlParameter("@Nterm", SqlDbType.Int,4)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;
            parameters[2].Value = Nterm;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            Common.DataExcel.DataSetToExcel(ds, FileName);
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
			parameters[0].Value = "NotSign";
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

