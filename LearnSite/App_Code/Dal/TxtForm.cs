
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:TxtForm
	/// </summary>
	public partial class TxtForm
	{
		public TxtForm()
		{}
		#region  BasicMethod

        public string GetMtitle(int Mid)
        {
            string mysql = "select Mtitle from TxtForm where Mid=" + Mid;
            return DbHelperSQL.FindString(mysql);        
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.TxtForm model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TxtForm(");
			strSql.Append("Mtitle,Mcid,Mcontent,Mdate,Mhit,Mpublish,Mdelete)");
			strSql.Append(" values (");
			strSql.Append("@Mtitle,@Mcid,@Mcontent,@Mdate,@Mhit,@Mpublish,@Mdelete)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Mtitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Mcid", SqlDbType.Int,4),
					new SqlParameter("@Mcontent", SqlDbType.NText),
					new SqlParameter("@Mdate", SqlDbType.DateTime),
					new SqlParameter("@Mhit", SqlDbType.Int,4),
					new SqlParameter("@Mpublish", SqlDbType.Bit,1),
					new SqlParameter("@Mdelete", SqlDbType.Bit,1)};
			parameters[0].Value = model.Mtitle;
			parameters[1].Value = model.Mcid;
			parameters[2].Value = model.Mcontent;
			parameters[3].Value = model.Mdate;
			parameters[4].Value = model.Mhit;
			parameters[5].Value = model.Mpublish;
			parameters[6].Value = model.Mdelete;

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
		/// 更新一条数据 Mtitle,Mcontent,Mdate,Mpublish,Mid
		/// </summary>
		public bool Update(LearnSite.Model.TxtForm model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TxtForm set ");
			strSql.Append("Mtitle=@Mtitle,");
			strSql.Append("Mcontent=@Mcontent,");
			strSql.Append("Mdate=@Mdate,");
			strSql.Append("Mpublish=@Mpublish ");
			strSql.Append(" where Mid=@Mid");
			SqlParameter[] parameters = {
					new SqlParameter("@Mtitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Mcontent", SqlDbType.NText),
					new SqlParameter("@Mdate", SqlDbType.DateTime),
					new SqlParameter("@Mpublish", SqlDbType.Bit,1),
					new SqlParameter("@Mid", SqlDbType.Int,4)};
			parameters[0].Value = model.Mtitle;
			parameters[1].Value = model.Mcontent;
			parameters[2].Value = model.Mdate;
			parameters[3].Value = model.Mpublish;
			parameters[4].Value = model.Mid;

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
        /// 表单状态：发布或回收
        /// </summary>
        /// <param name="Mid"></param>
        public void UpdateMpublish(int Mid)
        {
            string strSql = "update TxtForm set Mpublish=Mpublish^1 where Mid=" + Mid;
            DbHelperSQL.ExecuteSql(strSql);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Mid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TxtForm ");
			strSql.Append(" where Mid=@Mid");
			SqlParameter[] parameters = {
					new SqlParameter("@Mid", SqlDbType.Int,4)
			};
			parameters[0].Value = Mid;

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
		public bool DeleteList(string Midlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TxtForm ");
			strSql.Append(" where Mid in ("+Midlist + ")  ");
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
		public LearnSite.Model.TxtForm GetModel(int Mid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Mid,Mtitle,Mcid,Mcontent,Mdate,Mhit,Mpublish,Mdelete from TxtForm ");
			strSql.Append(" where Mid=@Mid");
			SqlParameter[] parameters = {
					new SqlParameter("@Mid", SqlDbType.Int,4)
			};
			parameters[0].Value = Mid;

			LearnSite.Model.TxtForm model=new LearnSite.Model.TxtForm();
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
		public LearnSite.Model.TxtForm DataRowToModel(DataRow row)
		{
			LearnSite.Model.TxtForm model=new LearnSite.Model.TxtForm();
			if (row != null)
			{
				if(row["Mid"]!=null && row["Mid"].ToString()!="")
				{
					model.Mid=int.Parse(row["Mid"].ToString());
				}
				if(row["Mtitle"]!=null)
				{
					model.Mtitle=row["Mtitle"].ToString();
				}
				if(row["Mcid"]!=null && row["Mcid"].ToString()!="")
				{
					model.Mcid=int.Parse(row["Mcid"].ToString());
				}
				if(row["Mcontent"]!=null)
				{
					model.Mcontent=row["Mcontent"].ToString();
				}
				if(row["Mdate"]!=null && row["Mdate"].ToString()!="")
				{
					model.Mdate=DateTime.Parse(row["Mdate"].ToString());
				}
				if(row["Mhit"]!=null && row["Mhit"].ToString()!="")
				{
					model.Mhit=int.Parse(row["Mhit"].ToString());
				}
				if(row["Mpublish"]!=null && row["Mpublish"].ToString()!="")
				{
					if((row["Mpublish"].ToString()=="1")||(row["Mpublish"].ToString().ToLower()=="true"))
					{
						model.Mpublish=true;
					}
					else
					{
						model.Mpublish=false;
					}
				}
				if(row["Mdelete"]!=null && row["Mdelete"].ToString()!="")
				{
					if((row["Mdelete"].ToString()=="1")||(row["Mdelete"].ToString().ToLower()=="true"))
					{
						model.Mdelete=true;
					}
					else
					{
						model.Mdelete=false;
					}
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
			strSql.Append("select Mid,Mtitle,Mcid,Mcontent,Mdate,Mhit,Mpublish,Mdelete ");
			strSql.Append(" FROM TxtForm ");
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
			strSql.Append(" Mid,Mtitle,Mcid,Mcontent,Mdate,Mhit,Mpublish,Mdelete ");
			strSql.Append(" FROM TxtForm ");
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
			strSql.Append("select count(1) FROM TxtForm ");
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
				strSql.Append("order by T.Mid desc");
			}
			strSql.Append(")AS Row, T.*  from TxtForm T ");
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
			parameters[0].Value = "TxtForm";
			parameters[1].Value = "Mid";
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

