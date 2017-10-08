
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:Research
	/// </summary>
	public partial class Research
	{
		public Research()
		{}
		#region  BasicMethod

        public bool Exist(int Rsid)
        {
            string mysql = "select Rid from Research where Rsid=" + Rsid;
            return DbHelperSQL.Exists(mysql);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Research model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Research(");
			strSql.Append("Rsid,Ryear,Rgrade,Rclass,Rterm,Rlearn,Rplay,Rsleep,Rfree,Rdate)");
			strSql.Append(" values (");
			strSql.Append("@Rsid,@Ryear,@Rgrade,@Rclass,@Rterm,@Rlearn,@Rplay,@Rsleep,@Rfree,@Rdate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Rsid", SqlDbType.Int,4),
					new SqlParameter("@Ryear", SqlDbType.Int,4),
					new SqlParameter("@Rgrade", SqlDbType.Int,4),
					new SqlParameter("@Rclass", SqlDbType.Int,4),
					new SqlParameter("@Rterm", SqlDbType.Int,4),
					new SqlParameter("@Rlearn", SqlDbType.SmallMoney),
					new SqlParameter("@Rplay", SqlDbType.SmallMoney),
					new SqlParameter("@Rsleep",SqlDbType.SmallMoney),
					new SqlParameter("@Rfree", SqlDbType.SmallMoney),
					new SqlParameter("@Rdate", SqlDbType.DateTime)};
			parameters[0].Value = model.Rsid;
			parameters[1].Value = model.Ryear;
			parameters[2].Value = model.Rgrade;
			parameters[3].Value = model.Rclass;
			parameters[4].Value = model.Rterm;
			parameters[5].Value = model.Rlearn;
			parameters[6].Value = model.Rplay;
			parameters[7].Value = model.Rsleep;
			parameters[8].Value = model.Rfree;
			parameters[9].Value = model.Rdate;

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
		public bool Update(LearnSite.Model.Research model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Research set ");
			strSql.Append("Rsid=@Rsid,");
			strSql.Append("Ryear=@Ryear,");
			strSql.Append("Rgrade=@Rgrade,");
			strSql.Append("Rclass=@Rclass,");
			strSql.Append("Rterm=@Rterm,");
			strSql.Append("Rlearn=@Rlearn,");
			strSql.Append("Rplay=@Rplay,");
			strSql.Append("Rsleep=@Rsleep,");
			strSql.Append("Rfree=@Rfree,");
			strSql.Append("Rdate=@Rdate");
			strSql.Append(" where Rid=@Rid");
			SqlParameter[] parameters = {
					new SqlParameter("@Rsid", SqlDbType.Int,4),
					new SqlParameter("@Ryear", SqlDbType.Int,4),
					new SqlParameter("@Rgrade", SqlDbType.Int,4),
					new SqlParameter("@Rclass", SqlDbType.Int,4),
					new SqlParameter("@Rterm", SqlDbType.Int,4),
					new SqlParameter("@Rlearn", SqlDbType.SmallMoney),
					new SqlParameter("@Rplay", SqlDbType.SmallMoney),
					new SqlParameter("@Rsleep",SqlDbType.SmallMoney),
					new SqlParameter("@Rfree", SqlDbType.SmallMoney),
					new SqlParameter("@Rdate", SqlDbType.DateTime),
					new SqlParameter("@Rid", SqlDbType.Int,4)};
			parameters[0].Value = model.Rsid;
			parameters[1].Value = model.Ryear;
			parameters[2].Value = model.Rgrade;
			parameters[3].Value = model.Rclass;
			parameters[4].Value = model.Rterm;
			parameters[5].Value = model.Rlearn;
			parameters[6].Value = model.Rplay;
			parameters[7].Value = model.Rsleep;
			parameters[8].Value = model.Rfree;
			parameters[9].Value = model.Rdate;
			parameters[10].Value = model.Rid;

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
		public bool Delete(int Rid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Research ");
			strSql.Append(" where Rid=@Rid");
			SqlParameter[] parameters = {
					new SqlParameter("@Rid", SqlDbType.Int,4)
			};
			parameters[0].Value = Rid;

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
		public bool DeleteList(string Ridlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Research ");
			strSql.Append(" where Rid in ("+Ridlist + ")  ");
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
		public LearnSite.Model.Research GetModel(int Rid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Rid,Rsid,Ryear,Rgrade,Rclass,Rterm,Rlearn,Rplay,Rsleep,Rfree,Rdate from Research ");
			strSql.Append(" where Rid=@Rid");
			SqlParameter[] parameters = {
					new SqlParameter("@Rid", SqlDbType.Int,4)
			};
			parameters[0].Value = Rid;

			LearnSite.Model.Research model=new LearnSite.Model.Research();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Research DataRowToModel(DataRow row)
		{
			LearnSite.Model.Research model=new LearnSite.Model.Research();
			if (row != null)
			{
				if(row["Rid"]!=null && row["Rid"].ToString()!="")
				{
					model.Rid=int.Parse(row["Rid"].ToString());
				}
				if(row["Rsid"]!=null && row["Rsid"].ToString()!="")
				{
					model.Rsid=int.Parse(row["Rsid"].ToString());
				}
				if(row["Ryear"]!=null && row["Ryear"].ToString()!="")
				{
					model.Ryear=int.Parse(row["Ryear"].ToString());
				}
				if(row["Rgrade"]!=null && row["Rgrade"].ToString()!="")
				{
					model.Rgrade=int.Parse(row["Rgrade"].ToString());
				}
				if(row["Rclass"]!=null && row["Rclass"].ToString()!="")
				{
					model.Rclass=int.Parse(row["Rclass"].ToString());
				}
				if(row["Rterm"]!=null && row["Rterm"].ToString()!="")
				{
					model.Rterm=int.Parse(row["Rterm"].ToString());
				}
				if(row["Rlearn"]!=null && row["Rlearn"].ToString()!="")
				{
					model.Rlearn=decimal.Parse(row["Rlearn"].ToString());
				}
				if(row["Rplay"]!=null && row["Rplay"].ToString()!="")
				{
					model.Rplay=decimal.Parse(row["Rplay"].ToString());
				}
				if(row["Rsleep"]!=null && row["Rsleep"].ToString()!="")
				{
                    model.Rsleep = decimal.Parse(row["Rsleep"].ToString());
				}
				if(row["Rfree"]!=null && row["Rfree"].ToString()!="")
				{
                    model.Rfree = decimal.Parse(row["Rfree"].ToString());
				}
				if(row["Rdate"]!=null && row["Rdate"].ToString()!="")
				{
					model.Rdate=DateTime.Parse(row["Rdate"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Rid,Rsid,Ryear,Rgrade,Rclass,Rterm,Rlearn,Rplay,Rsleep,Rfree,Rdate,Sname ");
			strSql.Append(" FROM Research,Students ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where Rsid=Sid and "+strWhere);
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
			strSql.Append(" Rid,Rsid,Ryear,Rgrade,Rclass,Rterm,Rlearn,Rplay,Rsleep,Rfree,Rdate ");
			strSql.Append(" FROM Research ");
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
			strSql.Append("select count(1) FROM Research ");
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
				strSql.Append("order by T.Rid desc");
			}
			strSql.Append(")AS Row, T.*  from Research T ");
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
			parameters[0].Value = "Research";
			parameters[1].Value = "Rid";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

