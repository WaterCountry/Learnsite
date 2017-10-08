using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:SurveyQuestion
	/// </summary>
	public partial class SurveyQuestion
	{
		public SurveyQuestion()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Qid", "SurveyQuestion"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Qid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SurveyQuestion");
			strSql.Append(" where Qid=@Qid");
			SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4)
			};
			parameters[0].Value = Qid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在该调查卷试题记录
        /// </summary>
        public bool ExistsByQvid(int Qvid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SurveyQuestion");
            strSql.Append(" where Qvid=@Qvid");
            SqlParameter[] parameters = {
					new SqlParameter("@Qvid", SqlDbType.Int,4)
			};
            parameters[0].Value = Qvid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.SurveyQuestion model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SurveyQuestion(");
			strSql.Append("Qvid,Qcid,Qtitle,Qcount)");
			strSql.Append(" values (");
			strSql.Append("@Qvid,@Qcid,@Qtitle,@Qcount)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Qvid", SqlDbType.Int,4),
					new SqlParameter("@Qcid", SqlDbType.Int,4),
					new SqlParameter("@Qtitle", SqlDbType.NText),
					new SqlParameter("@Qcount", SqlDbType.Int,4)};
			parameters[0].Value = model.Qvid;
			parameters[1].Value = model.Qcid;
			parameters[2].Value = model.Qtitle;
			parameters[3].Value = model.Qcount;

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
        public bool UpdateQtitle(int Qid,string Qtitle)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SurveyQuestion set ");
            strSql.Append("Qtitle=@Qtitle ");
            strSql.Append(" where Qid=@Qid");
            SqlParameter[] parameters = {
					new SqlParameter("@Qtitle", SqlDbType.NText),
					new SqlParameter("@Qid", SqlDbType.Int,4)};
            parameters[0].Value = Qtitle;
            parameters[1].Value = Qid;

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
		public bool Update(LearnSite.Model.SurveyQuestion model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SurveyQuestion set ");
			strSql.Append("Qvid=@Qvid,");
			strSql.Append("Qcid=@Qcid,");
			strSql.Append("Qtitle=@Qtitle,");
			strSql.Append("Qcount=@Qcount");
			strSql.Append(" where Qid=@Qid");
			SqlParameter[] parameters = {
					new SqlParameter("@Qvid", SqlDbType.Int,4),
					new SqlParameter("@Qcid", SqlDbType.Int,4),
					new SqlParameter("@Qtitle", SqlDbType.NText),
					new SqlParameter("@Qcount", SqlDbType.Int,4),
					new SqlParameter("@Qid", SqlDbType.Int,4)};
			parameters[0].Value = model.Qvid;
			parameters[1].Value = model.Qcid;
			parameters[2].Value = model.Qtitle;
			parameters[3].Value = model.Qcount;
			parameters[4].Value = model.Qid;

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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Qid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SurveyQuestion ");
			strSql.Append(" where Qid=@Qid");
			SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4)
			};
			parameters[0].Value = Qid;

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
		public bool DeleteList(string Qidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SurveyQuestion ");
			strSql.Append(" where Qid in ("+Qidlist + ")  ");
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
		public LearnSite.Model.SurveyQuestion GetModel(int Qid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Qid,Qvid,Qcid,Qtitle,Qcount from SurveyQuestion ");
			strSql.Append(" where Qid=@Qid");
			SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4)
			};
			parameters[0].Value = Qid;

			LearnSite.Model.SurveyQuestion model=new LearnSite.Model.SurveyQuestion();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Qid"]!=null && ds.Tables[0].Rows[0]["Qid"].ToString()!="")
				{
					model.Qid=int.Parse(ds.Tables[0].Rows[0]["Qid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Qvid"]!=null && ds.Tables[0].Rows[0]["Qvid"].ToString()!="")
				{
					model.Qvid=int.Parse(ds.Tables[0].Rows[0]["Qvid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Qcid"]!=null && ds.Tables[0].Rows[0]["Qcid"].ToString()!="")
				{
					model.Qcid=int.Parse(ds.Tables[0].Rows[0]["Qcid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Qtitle"]!=null && ds.Tables[0].Rows[0]["Qtitle"].ToString()!="")
				{
					model.Qtitle=ds.Tables[0].Rows[0]["Qtitle"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Qcount"]!=null && ds.Tables[0].Rows[0]["Qcount"].ToString()!="")
				{
					model.Qcount=int.Parse(ds.Tables[0].Rows[0]["Qcount"].ToString());
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
        public LearnSite.Model.SurveyQuestion GetModel(DataTable dt, int Tsort)
        {
            LearnSite.Model.SurveyQuestion model = new LearnSite.Model.SurveyQuestion();
            int Count = dt.Rows.Count;
            if (Count > 0)
            {
                if (Tsort < Count)
                {
                    if (dt.Rows[Tsort]["Qid"] != null && dt.Rows[Tsort]["Qid"].ToString() != "")
                    {
                        model.Qid = int.Parse(dt.Rows[Tsort]["Qid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Qvid"] != null && dt.Rows[Tsort]["Qvid"].ToString() != "")
                    {
                        model.Qvid = int.Parse(dt.Rows[Tsort]["Qvid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Qcid"] != null && dt.Rows[Tsort]["Qcid"].ToString() != "")
                    {
                        model.Qcid = int.Parse(dt.Rows[Tsort]["Qcid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Qtitle"] != null && dt.Rows[Tsort]["Qtitle"].ToString() != "")
                    {
                        model.Qtitle = dt.Rows[Tsort]["Qtitle"].ToString();
                    }
                    if (dt.Rows[Tsort]["Qcount"] != null && dt.Rows[Tsort]["Qcount"].ToString() != "")
                    {
                        model.Qcount = int.Parse(dt.Rows[Tsort]["Qcount"].ToString());
                    }
                    return model;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据ＩＤ返回调查题目
        /// </summary>
        /// <param name="Qid"></param>
        /// <returns></returns>
        public string GetTitle(int Qid)
        {
            string myql = "select Qtitle  FROM SurveyQuestion where Qid="+Qid;
            return DbHelperSQL.FindString(myql);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Qid,Qvid,Qcid,Qtitle,Qcount ");
			strSql.Append(" FROM SurveyQuestion ");
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
			strSql.Append(" Qid,Qvid,Qcid,Qtitle,Qcount ");
			strSql.Append(" FROM SurveyQuestion ");
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
			strSql.Append("select count(1) FROM SurveyQuestion ");
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
				strSql.Append("order by T.Qid desc");
			}
			strSql.Append(")AS Row, T.*  from SurveyQuestion T ");
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
			parameters[0].Value = "SurveyQuestion";
			parameters[1].Value = "Qid";
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

