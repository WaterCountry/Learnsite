using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:Gauge
	/// </summary>
	public partial class Gauge
	{
		public Gauge()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Gid", "Gauge"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Gid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Gauge");
			strSql.Append(" where Gid=@Gid");
			SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4)
			};
			parameters[0].Value = Gid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Gauge model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Gauge(");
			strSql.Append("Ghid,Gtype,Gtitle,Gcount,Gdate)");
			strSql.Append(" values (");
			strSql.Append("@Ghid,@Gtype,@Gtitle,@Gcount,@Gdate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Ghid", SqlDbType.Int,4),
					new SqlParameter("@Gtype", SqlDbType.NVarChar,50),
					new SqlParameter("@Gtitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Gcount", SqlDbType.Int,4),
					new SqlParameter("@Gdate", SqlDbType.DateTime)};
			parameters[0].Value = model.Ghid;
			parameters[1].Value = model.Gtype;
			parameters[2].Value = model.Gtitle;
			parameters[3].Value = model.Gcount;
			parameters[4].Value = model.Gdate;

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
		public bool Update(LearnSite.Model.Gauge model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Gauge set ");
			strSql.Append("Ghid=@Ghid,");
			strSql.Append("Gtype=@Gtype,");
			strSql.Append("Gtitle=@Gtitle,");
			strSql.Append("Gcount=@Gcount,");
			strSql.Append("Gdate=@Gdate");
			strSql.Append(" where Gid=@Gid");
			SqlParameter[] parameters = {
					new SqlParameter("@Ghid", SqlDbType.Int,4),
					new SqlParameter("@Gtype", SqlDbType.NVarChar,50),
					new SqlParameter("@Gtitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Gcount", SqlDbType.Int,4),
					new SqlParameter("@Gdate", SqlDbType.DateTime),
					new SqlParameter("@Gid", SqlDbType.Int,4)};
			parameters[0].Value = model.Ghid;
			parameters[1].Value = model.Gtype;
			parameters[2].Value = model.Gtitle;
			parameters[3].Value = model.Gcount;
			parameters[4].Value = model.Gdate;
			parameters[5].Value = model.Gid;

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
		public bool Delete(int Gid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Gauge ");
			strSql.Append(" where Gid=@Gid");
			SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4)
			};
			parameters[0].Value = Gid;

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
		public bool DeleteList(string Gidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Gauge ");
			strSql.Append(" where Gid in ("+Gidlist + ")  ");
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
		public LearnSite.Model.Gauge GetModel(int Gid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Gid,Ghid,Gtype,Gtitle,Gcount,Gdate from Gauge ");
			strSql.Append(" where Gid=@Gid");
			SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4)
			};
			parameters[0].Value = Gid;

			LearnSite.Model.Gauge model=new LearnSite.Model.Gauge();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Gid"]!=null && ds.Tables[0].Rows[0]["Gid"].ToString()!="")
				{
					model.Gid=int.Parse(ds.Tables[0].Rows[0]["Gid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ghid"]!=null && ds.Tables[0].Rows[0]["Ghid"].ToString()!="")
				{
					model.Ghid=int.Parse(ds.Tables[0].Rows[0]["Ghid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Gtype"]!=null && ds.Tables[0].Rows[0]["Gtype"].ToString()!="")
				{
					model.Gtype=ds.Tables[0].Rows[0]["Gtype"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Gtitle"]!=null && ds.Tables[0].Rows[0]["Gtitle"].ToString()!="")
				{
					model.Gtitle=ds.Tables[0].Rows[0]["Gtitle"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Gcount"]!=null && ds.Tables[0].Rows[0]["Gcount"].ToString()!="")
				{
					model.Gcount=int.Parse(ds.Tables[0].Rows[0]["Gcount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Gdate"]!=null && ds.Tables[0].Rows[0]["Gdate"].ToString()!="")
				{
					model.Gdate=DateTime.Parse(ds.Tables[0].Rows[0]["Gdate"].ToString());
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
			strSql.Append("select Gid,Ghid,Gtype,Gtitle,Gcount,Gdate ");
			strSql.Append(" FROM Gauge ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获取标题
        /// </summary>
        /// <param name="Gid"></param>
        /// <returns></returns>
        public string GetGtitle(int Gid)
        {
            string mysql = "select Gtitle from Gauge where Gid=" + Gid;
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 返回该学案类型该教师的自定义量化评价标准的Gid,Gtitle
        /// </summary>
        /// <param name="Gtype"></param>
        /// <param name="Ghid"></param>
        /// <returns></returns>
        public DataTable GetListGauge(int Ghid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Gid,Gtitle from Gauge ");
            strSql.Append(" where Ghid=@Ghid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Ghid", SqlDbType.Int,4)
			};
            parameters[0].Value = Ghid;
            DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
            dt.Rows.Add(0, "请选择...");
            dt.DefaultView.Sort = "Gid asc";
            DataTable dtNew = dt.DefaultView.ToTable();
            dt.Dispose();
            return dtNew;
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
			strSql.Append(" Gid,Ghid,Gtype,Gtitle,Gcount,Gdate ");
			strSql.Append(" FROM Gauge ");
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
			strSql.Append("select count(1) FROM Gauge ");
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
				strSql.Append("order by T.Gid desc");
			}
			strSql.Append(")AS Row, T.*  from Gauge T ");
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
			parameters[0].Value = "Gauge";
			parameters[1].Value = "Gid";
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

