using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:SurveyClass
	/// </summary>
	public partial class SurveyClass
	{
		public SurveyClass()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Yid", "SurveyClass"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Yid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SurveyClass");
			strSql.Append(" where Yid=@Yid");
			SqlParameter[] parameters = {
					new SqlParameter("@Yid", SqlDbType.Int,4)
			};
			parameters[0].Value = Yid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在该班级记录
        /// </summary>
        public int ExistsClass(int Yyear,int Ygrade,int Yclass,int Yterm,int Yvid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Yid from SurveyClass");
            strSql.Append(" where Yyear=@Yyear and Ygrade=@Ygrade and Yclass=@Yclass and Yterm=@Yterm and Yvid=@Yvid");
            SqlParameter[] parameters = {
					new SqlParameter("@Yyear", SqlDbType.Int,4),
					new SqlParameter("@Ygrade", SqlDbType.Int,4),
					new SqlParameter("@Yclass", SqlDbType.Int,4),
					new SqlParameter("@Yterm", SqlDbType.Int,4),
					new SqlParameter("@Yvid", SqlDbType.Int,4)
			};
            parameters[0].Value = Yyear;
            parameters[1].Value = Ygrade;
            parameters[2].Value = Yclass;
            parameters[3].Value = Yterm;
            parameters[4].Value = Yvid;

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
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.SurveyClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SurveyClass(");
			strSql.Append("Yyear,Ygrade,Yclass,Yterm,Ycid,Yvid,Yselect,Ycount,Yscore,Ydate)");
			strSql.Append(" values (");
			strSql.Append("@Yyear,@Ygrade,@Yclass,@Yterm,@Ycid,@Yvid,@Yselect,@Ycount,@Yscore,@Ydate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Yyear", SqlDbType.Int,4),
					new SqlParameter("@Ygrade", SqlDbType.Int,4),
					new SqlParameter("@Yclass", SqlDbType.Int,4),
					new SqlParameter("@Yterm", SqlDbType.Int,4),
					new SqlParameter("@Ycid", SqlDbType.Int,4),
					new SqlParameter("@Yvid", SqlDbType.Int,4),
					new SqlParameter("@Yselect", SqlDbType.NText),
					new SqlParameter("@Ycount", SqlDbType.NText),
					new SqlParameter("@Yscore", SqlDbType.Int,4),
					new SqlParameter("@Ydate", SqlDbType.DateTime)};
			parameters[0].Value = model.Yyear;
			parameters[1].Value = model.Ygrade;
			parameters[2].Value = model.Yclass;
			parameters[3].Value = model.Yterm;
			parameters[4].Value = model.Ycid;
			parameters[5].Value = model.Yvid;
			parameters[6].Value = model.Yselect;
			parameters[7].Value = model.Ycount;
			parameters[8].Value = model.Yscore;
			parameters[9].Value = model.Ydate;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.SurveyClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SurveyClass set ");
			strSql.Append("Yyear=@Yyear,");
			strSql.Append("Ygrade=@Ygrade,");
			strSql.Append("Yclass=@Yclass,");
			strSql.Append("Yterm=@Yterm,");
			strSql.Append("Ycid=@Ycid,");
			strSql.Append("Yvid=@Yvid,");
			strSql.Append("Yselect=@Yselect,");
			strSql.Append("Ycount=@Ycount,");
			strSql.Append("Yscore=@Yscore,");
			strSql.Append("Ydate=@Ydate");
			strSql.Append(" where Yid=@Yid");
			SqlParameter[] parameters = {
					new SqlParameter("@Yyear", SqlDbType.Int,4),
					new SqlParameter("@Ygrade", SqlDbType.Int,4),
					new SqlParameter("@Yclass", SqlDbType.Int,4),
					new SqlParameter("@Yterm", SqlDbType.Int,4),
					new SqlParameter("@Ycid", SqlDbType.Int,4),
					new SqlParameter("@Yvid", SqlDbType.Int,4),
					new SqlParameter("@Yselect", SqlDbType.NText),
					new SqlParameter("@Ycount", SqlDbType.NText),
					new SqlParameter("@Yscore", SqlDbType.Int,4),
					new SqlParameter("@Ydate", SqlDbType.DateTime),
					new SqlParameter("@Yid", SqlDbType.Int,4)};
			parameters[0].Value = model.Yyear;
			parameters[1].Value = model.Ygrade;
			parameters[2].Value = model.Yclass;
			parameters[3].Value = model.Yterm;
			parameters[4].Value = model.Ycid;
			parameters[5].Value = model.Yvid;
			parameters[6].Value = model.Yselect;
			parameters[7].Value = model.Ycount;
			parameters[8].Value = model.Yscore;
			parameters[9].Value = model.Ydate;
			parameters[10].Value = model.Yid;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        public bool UpdateClass(LearnSite.Model.SurveyClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SurveyClass set ");
            strSql.Append("Yselect=@Yselect,");
            strSql.Append("Ycount=@Ycount,");
            strSql.Append("Yscore=@Yscore,");
            strSql.Append("Ydate=@Ydate");
            strSql.Append(" where Yid=@Yid");
            SqlParameter[] parameters = {
					new SqlParameter("@Yselect", SqlDbType.NText),
					new SqlParameter("@Ycount", SqlDbType.NText),
					new SqlParameter("@Yscore", SqlDbType.Int,4),
					new SqlParameter("@Ydate", SqlDbType.DateTime),
					new SqlParameter("@Yid", SqlDbType.Int,4)};
            parameters[0].Value = model.Yselect;
            parameters[1].Value = model.Ycount;
            parameters[2].Value = model.Yscore;
            parameters[3].Value = model.Ydate;
            parameters[4].Value = model.Yid;

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
		public bool Delete(int Yid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SurveyClass ");
			strSql.Append(" where Yid=@Yid");
			SqlParameter[] parameters = {
					new SqlParameter("@Yid", SqlDbType.Int,4)
			};
			parameters[0].Value = Yid;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Yidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SurveyClass ");
			strSql.Append(" where Yid in ("+Yidlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.SurveyClass GetModelByClass(int Yyear,int Ygrade,int Yclass,int Yterm,int Ycid,int Yvid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Yid,Yyear,Ygrade,Yclass,Yterm,Ycid,Yvid,Yselect,Ycount,Yscore,Ydate from SurveyClass,SurveyFeedback ");
            strSql.Append(" where Yvid=@Yvid and Ycid=@Ycid and Yyear=@Yyear and Yvid=Fvid and Ycid=Fcid ");
            strSql.Append(" and Fnum in (select Snum from Students where Sgrade=@Ygrade and Sclass=@Yclass)");
            SqlParameter[] parameters = {
					new SqlParameter("@Yyear", SqlDbType.Int,4),
					new SqlParameter("@Ygrade", SqlDbType.Int,4),
					new SqlParameter("@Yclass", SqlDbType.Int,4),
					new SqlParameter("@Yterm", SqlDbType.Int,4),
					new SqlParameter("@Ycid", SqlDbType.Int,4),
					new SqlParameter("@Yvid", SqlDbType.Int,4)};
            parameters[0].Value = Yyear;
            parameters[1].Value = Ygrade;
            parameters[2].Value = Yclass;
            parameters[3].Value = Yterm;
            parameters[4].Value = Ycid;
            parameters[5].Value = Yvid;

            LearnSite.Model.SurveyClass model = new LearnSite.Model.SurveyClass();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Yid"] != null && ds.Tables[0].Rows[0]["Yid"].ToString() != "")
                {
                    model.Yid = int.Parse(ds.Tables[0].Rows[0]["Yid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yyear"] != null && ds.Tables[0].Rows[0]["Yyear"].ToString() != "")
                {
                    model.Yyear = int.Parse(ds.Tables[0].Rows[0]["Yyear"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ygrade"] != null && ds.Tables[0].Rows[0]["Ygrade"].ToString() != "")
                {
                    model.Ygrade = int.Parse(ds.Tables[0].Rows[0]["Ygrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yclass"] != null && ds.Tables[0].Rows[0]["Yclass"].ToString() != "")
                {
                    model.Yclass = int.Parse(ds.Tables[0].Rows[0]["Yclass"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yterm"] != null && ds.Tables[0].Rows[0]["Yterm"].ToString() != "")
                {
                    model.Yterm = int.Parse(ds.Tables[0].Rows[0]["Yterm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ycid"] != null && ds.Tables[0].Rows[0]["Ycid"].ToString() != "")
                {
                    model.Ycid = int.Parse(ds.Tables[0].Rows[0]["Ycid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yvid"] != null && ds.Tables[0].Rows[0]["Yvid"].ToString() != "")
                {
                    model.Yvid = int.Parse(ds.Tables[0].Rows[0]["Yvid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yselect"] != null && ds.Tables[0].Rows[0]["Yselect"].ToString() != "")
                {
                    model.Yselect = ds.Tables[0].Rows[0]["Yselect"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Ycount"] != null && ds.Tables[0].Rows[0]["Ycount"].ToString() != "")
                {
                    model.Ycount = ds.Tables[0].Rows[0]["Ycount"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Yscore"] != null && ds.Tables[0].Rows[0]["Yscore"].ToString() != "")
                {
                    model.Yscore = int.Parse(ds.Tables[0].Rows[0]["Yscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ydate"] != null && ds.Tables[0].Rows[0]["Ydate"].ToString() != "")
                {
                    model.Ydate = DateTime.Parse(ds.Tables[0].Rows[0]["Ydate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.SurveyClass GetModel(int Yid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Yid,Yyear,Ygrade,Yclass,Yterm,Ycid,Yvid,Yselect,Ycount,Yscore,Ydate from SurveyClass ");
			strSql.Append(" where Yid=@Yid");
			SqlParameter[] parameters = {
					new SqlParameter("@Yid", SqlDbType.Int,4)
			};
			parameters[0].Value = Yid;

			LearnSite.Model.SurveyClass model=new LearnSite.Model.SurveyClass();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Yid"]!=null && ds.Tables[0].Rows[0]["Yid"].ToString()!="")
				{
					model.Yid=int.Parse(ds.Tables[0].Rows[0]["Yid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Yyear"]!=null && ds.Tables[0].Rows[0]["Yyear"].ToString()!="")
				{
					model.Yyear=int.Parse(ds.Tables[0].Rows[0]["Yyear"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ygrade"]!=null && ds.Tables[0].Rows[0]["Ygrade"].ToString()!="")
				{
					model.Ygrade=int.Parse(ds.Tables[0].Rows[0]["Ygrade"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Yclass"]!=null && ds.Tables[0].Rows[0]["Yclass"].ToString()!="")
				{
					model.Yclass=int.Parse(ds.Tables[0].Rows[0]["Yclass"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Yterm"]!=null && ds.Tables[0].Rows[0]["Yterm"].ToString()!="")
				{
					model.Yterm=int.Parse(ds.Tables[0].Rows[0]["Yterm"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ycid"]!=null && ds.Tables[0].Rows[0]["Ycid"].ToString()!="")
				{
					model.Ycid=int.Parse(ds.Tables[0].Rows[0]["Ycid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Yvid"]!=null && ds.Tables[0].Rows[0]["Yvid"].ToString()!="")
				{
					model.Yvid=int.Parse(ds.Tables[0].Rows[0]["Yvid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Yselect"]!=null && ds.Tables[0].Rows[0]["Yselect"].ToString()!="")
				{
					model.Yselect=ds.Tables[0].Rows[0]["Yselect"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Ycount"]!=null && ds.Tables[0].Rows[0]["Ycount"].ToString()!="")
				{
					model.Ycount=ds.Tables[0].Rows[0]["Ycount"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Yscore"]!=null && ds.Tables[0].Rows[0]["Yscore"].ToString()!="")
				{
					model.Yscore=int.Parse(ds.Tables[0].Rows[0]["Yscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ydate"]!=null && ds.Tables[0].Rows[0]["Ydate"].ToString()!="")
				{
					model.Ydate=DateTime.Parse(ds.Tables[0].Rows[0]["Ydate"].ToString());
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
			strSql.Append("select Yid,Yyear,Ygrade,Yclass,Yterm,Ycid,Yvid,Yselect,Ycount,Yscore,Ydate ");
			strSql.Append(" FROM SurveyClass ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
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
			strSql.Append(" Yid,Yyear,Ygrade,Yclass,Yterm,Ycid,Yvid,Yselect,Ycount,Yscore,Ydate ");
			strSql.Append(" FROM SurveyClass ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM SurveyClass ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Yid desc");
			}
			strSql.Append(")AS Row, T.*  from SurveyClass T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
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
			parameters[0].Value = "SurveyClass";
			parameters[1].Value = "Yid";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

