using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:Chinese
	/// </summary>
	public partial class Chinese
	{
		public Chinese()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Chinese model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Chinese(");
			strSql.Append("Ntitle,Ncontent)");
			strSql.Append(" values (");
            strSql.Append("@Ntitle,@Ncontent)");
            strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Ntitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Ncontent", SqlDbType.NText)};
			parameters[0].Value = model.Ntitle;
			parameters[1].Value = model.Ncontent;

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
        public bool Update(LearnSite.Model.Chinese model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Chinese set ");
            strSql.Append("Ntitle=@Ntitle,");
            strSql.Append("Ncontent=@Ncontent");
            strSql.Append(" where Nid=@Nid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Ntitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Ncontent", SqlDbType.NText),
					new SqlParameter("@Nid", SqlDbType.Int,4)};
            parameters[0].Value = model.Ntitle;
            parameters[1].Value = model.Ncontent;
            parameters[2].Value = model.Nid;

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
        public bool Delete(int Nid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Chinese ");
            strSql.Append(" where Nid=@Nid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Nid", SqlDbType.Int,4)			};
            parameters[0].Value = Nid;

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
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.Chinese GetModel(int Nid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Nid,Ntitle,Ncontent from Chinese ");
            strSql.Append(" where Nid=@Nid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Nid", SqlDbType.Int,4)			};
            parameters[0].Value = Nid;

            LearnSite.Model.Chinese model = new LearnSite.Model.Chinese();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
		public LearnSite.Model.Chinese DataRowToModel(DataRow row)
		{
			LearnSite.Model.Chinese model=new LearnSite.Model.Chinese();
			if (row != null)
			{
				if(row["Nid"]!=null && row["Nid"].ToString()!="")
				{
					model.Nid=int.Parse(row["Nid"].ToString());
				}
				if(row["Ntitle"]!=null)
				{
					model.Ntitle=row["Ntitle"].ToString();
				}
				if(row["Ncontent"]!=null)
				{
					model.Ncontent=row["Ncontent"].ToString();
				}
			}
			return model;
		}

        /// <summary>
        /// 获得数据列表,不包含Ncontent内容
        /// </summary>
        public DataSet GetListTitle(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Nid,Ntitle ");
            strSql.Append(" FROM Chinese ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Nid ASC");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 将指定拼音文章Nid绑定到datalist中
        /// </summary>
        /// <param name="DLTid"></param>
        public DataSet ShowAllNid(string Nids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Nid,Ntitle ");
            strSql.Append(" FROM Chinese ");
            if (Nids.Trim() != "")
            {
                strSql.Append(" WHERE Nid in(" + Nids+") ");
            }
            strSql.Append(" ORDER BY Nid ASC");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取指定的拼音词语内容，如果Nid为空则自动返回一条内容
        /// </summary>
        /// <param name="Nid"></param>
        /// <returns></returns>
        public string GetContent(string Nid)
        {
            string str = "";
            string mysql = "select top 1 Ncontent FROM Chinese ";
            if (!String.IsNullOrEmpty(Nid))
                mysql = mysql + " where Nid="+Nid;
            str = DbHelperSQL.FindString(mysql);
            return str;
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Nid,Ntitle,Ncontent ");
			strSql.Append(" FROM Chinese ");
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
			strSql.Append(" Nid,Ntitle,Ncontent ");
			strSql.Append(" FROM Chinese ");
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
			strSql.Append("select count(1) FROM Chinese ");
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
				strSql.Append("order by T. desc");
			}
			strSql.Append(")AS Row, T.*  from Chinese T ");
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
			parameters[0].Value = "Chinese";
			parameters[1].Value = "";
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

