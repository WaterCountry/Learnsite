using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:SoftCategory
	/// </summary>
	public partial class SoftCategory
	{
		public SoftCategory()
		{}
		#region  BasicMethod

        public int GetMaxYsort()
        {
            string strsql = "select max(Ysort)+1 from SoftCategory";
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
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.SoftCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SoftCategory(");
			strSql.Append("Ysort,Ytitle,Ycontent,Yopen)");
			strSql.Append(" values (");
			strSql.Append("@Ysort,@Ytitle,@Ycontent,@Yopen)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Ysort", SqlDbType.Int,4),
					new SqlParameter("@Ytitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Ycontent", SqlDbType.NVarChar,200),
					new SqlParameter("@Yopen", SqlDbType.Bit,1)};
			parameters[0].Value = model.Ysort;
			parameters[1].Value = model.Ytitle;
			parameters[2].Value = model.Ycontent;
			parameters[3].Value = model.Yopen;

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
        /// 更新分类名称
        /// </summary>
        /// <param name="Yid"></param>
        /// <param name="Ytitle"></param>
        /// <returns></returns>
        public bool UpdateYtitle(int Yid, string Ytitle)
        {
            string sql = "update SoftCategory set Ytitle='"+Ytitle+"' where Yid="+Yid;
            int row = DbHelperSQL.ExecuteSql(sql);
            if (row > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(int Yid,string Ytitle)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SoftCategory set ");
            strSql.Append("Ytitle=@Ytitle,");
            strSql.Append(" where Yid=@Yid");
            SqlParameter[] parameters = {
					new SqlParameter("@Ytitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Yid", SqlDbType.Int,4)};
            parameters[0].Value = Ytitle;
            parameters[1].Value = Yid;

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
		public bool Update(LearnSite.Model.SoftCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SoftCategory set ");
			strSql.Append("Ysort=@Ysort,");
			strSql.Append("Ytitle=@Ytitle,");
			strSql.Append("Ycontent=@Ycontent,");
			strSql.Append("Yopen=@Yopen");
			strSql.Append(" where Yid=@Yid");
			SqlParameter[] parameters = {
					new SqlParameter("@Ysort", SqlDbType.Int,4),
					new SqlParameter("@Ytitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Ycontent", SqlDbType.NVarChar,200),
					new SqlParameter("@Yopen", SqlDbType.Bit,1),
					new SqlParameter("@Yid", SqlDbType.Int,4)};
			parameters[0].Value = model.Ysort;
			parameters[1].Value = model.Ytitle;
			parameters[2].Value = model.Ycontent;
			parameters[3].Value = model.Yopen;
			parameters[4].Value = model.Yid;

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
		public bool Delete(int Yid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SoftCategory ");
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
			strSql.Append("delete from SoftCategory ");
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
		public LearnSite.Model.SoftCategory GetModel(int Yid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Yid,Ysort,Ytitle,Ycontent,Yopen from SoftCategory ");
			strSql.Append(" where Yid=@Yid");
			SqlParameter[] parameters = {
					new SqlParameter("@Yid", SqlDbType.Int,4)
			};
			parameters[0].Value = Yid;

			LearnSite.Model.SoftCategory model=new LearnSite.Model.SoftCategory();
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
		public LearnSite.Model.SoftCategory DataRowToModel(DataRow row)
		{
			LearnSite.Model.SoftCategory model=new LearnSite.Model.SoftCategory();
			if (row != null)
			{
				if(row["Yid"]!=null && row["Yid"].ToString()!="")
				{
					model.Yid=int.Parse(row["Yid"].ToString());
				}
				if(row["Ysort"]!=null && row["Ysort"].ToString()!="")
				{
					model.Ysort=int.Parse(row["Ysort"].ToString());
				}
				if(row["Ytitle"]!=null)
				{
					model.Ytitle=row["Ytitle"].ToString();
				}
				if(row["Ycontent"]!=null)
				{
					model.Ycontent=row["Ycontent"].ToString();
				}
				if(row["Yopen"]!=null && row["Yopen"].ToString()!="")
				{
					if((row["Yopen"].ToString()=="1")||(row["Yopen"].ToString().ToLower()=="true"))
					{
						model.Yopen=true;
					}
					else
					{
						model.Yopen=false;
					}
				}
			}
			return model;
		}
        /// <summary>
        /// 初始化序号
        /// </summary>
        public void initYsort()
        {
            string mysql = "select Yid from SoftCategory  order by Ysort desc,Yid desc";
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int cn = dt.Rows.Count;
            if (cn > 0)
            {
                for (int i = 0; i < cn; i++)
                {
                    string Yid = dt.Rows[i][0].ToString();
                    int ls = i + 1;
                    string sql = "update SoftCategory set Ysort= " + ls + " where Yid=" + Yid;
                    DbHelperSQL.ExecuteSql(sql);
                }
            }        
        }
        /// <summary>
        /// 更改导航次序 updown 值0向上 减，1向下　增
        /// </summary>
        /// <param name="Lid"></param>
        /// <param name="updown"></param>
        public void UpdateYsort(int Yid, bool updown)
        {
            string mysql = "update SoftCategory set Ysort=Ysort-1 where Yid=" + Yid;
            if (updown)
                mysql = "update SoftCategory set Ysort=Ysort+1 where Yid=" + Yid;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 获得数据列表按序号和编号排序
        /// </summary>
        public DataSet GetListCategory()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Yid,Ytitle ");
            strSql.Append(" FROM SoftCategory ");
            strSql.Append(" ORDER BY Ysort ASC,Yid ASC ");

            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表按序号和编号排序
        /// </summary>
        public DataSet GetListOrder()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Yid,Ysort,Ytitle,Ycontent,Yopen ");
            strSql.Append(" FROM SoftCategory ");
            strSql.Append(" ORDER BY Ysort ASC,Yid ASC ");

            return DbHelperSQL.Query(strSql.ToString());
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Yid,Ysort,Ytitle,Ycontent,Yopen ");
			strSql.Append(" FROM SoftCategory ");
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
			strSql.Append(" Yid,Ysort,Ytitle,Ycontent,Yopen ");
			strSql.Append(" FROM SoftCategory ");
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
			strSql.Append("select count(1) FROM SoftCategory ");
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
			strSql.Append(")AS Row, T.*  from SoftCategory T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获取标题
        /// </summary>
        /// <param name="Yid"></param>
        /// <returns></returns>
        public string GetTitle(int Yid)
        {
            string sql = "select Ytitle from SoftCategory where Yid=" + Yid;
            return DbHelperSQL.FindString(sql);
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
			parameters[0].Value = "SoftCategory";
			parameters[1].Value = "Yid";
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

