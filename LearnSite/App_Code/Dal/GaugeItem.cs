using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:GaugeItem
	/// </summary>
	public partial class GaugeItem
	{
		public GaugeItem()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Mid", "GaugeItem"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Mid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from GaugeItem");
			strSql.Append(" where Mid=@Mid");
			SqlParameter[] parameters = {
					new SqlParameter("@Mid", SqlDbType.Int,4)
			};
			parameters[0].Value = Mid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsMgid(int Mgid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from GaugeItem");
            strSql.Append(" where Mgid=@Mgid");
            SqlParameter[] parameters = {
					new SqlParameter("@Mgid", SqlDbType.Int,4)
			};
            parameters[0].Value = Mgid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.GaugeItem model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into GaugeItem(");
			strSql.Append("Mgid,Mitem,Mscore,Msort)");
			strSql.Append(" values (");
			strSql.Append("@Mgid,@Mitem,@Mscore,@Msort)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Mgid", SqlDbType.Int,4),
					new SqlParameter("@Mitem", SqlDbType.NVarChar,50),
					new SqlParameter("@Mscore", SqlDbType.Int,4),
					new SqlParameter("@Msort", SqlDbType.Int,4)};
			parameters[0].Value = model.Mgid;
			parameters[1].Value = model.Mitem;
			parameters[2].Value = model.Mscore;
			parameters[3].Value = model.Msort;

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
		public bool Update(LearnSite.Model.GaugeItem model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update GaugeItem set ");
			strSql.Append("Mgid=@Mgid,");
			strSql.Append("Mitem=@Mitem,");
			strSql.Append("Mscore=@Mscore,");
			strSql.Append("Msort=@Msort");
			strSql.Append(" where Mid=@Mid");
			SqlParameter[] parameters = {
					new SqlParameter("@Mgid", SqlDbType.Int,4),
					new SqlParameter("@Mitem", SqlDbType.NVarChar,50),
					new SqlParameter("@Mscore", SqlDbType.Int,4),
					new SqlParameter("@Msort", SqlDbType.Int,4),
					new SqlParameter("@Mid", SqlDbType.Int,4)};
			parameters[0].Value = model.Mgid;
			parameters[1].Value = model.Mitem;
			parameters[2].Value = model.Mscore;
			parameters[3].Value = model.Msort;
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
        /// 更新一条数据 注意： model只赋值Mitem,Mscore,Mid
        /// </summary>
        public bool UpdateMitem(LearnSite.Model.GaugeItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GaugeItem set ");
            strSql.Append("Mitem=@Mitem,");
            strSql.Append("Mscore=@Mscore");
            strSql.Append(" where Mid=@Mid");
            SqlParameter[] parameters = {
					new SqlParameter("@Mitem", SqlDbType.NVarChar,50),
					new SqlParameter("@Mscore", SqlDbType.Int,4),
					new SqlParameter("@Mid", SqlDbType.Int,4)};
            parameters[0].Value = model.Mitem;
            parameters[1].Value = model.Mscore;
            parameters[2].Value = model.Mid;

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
		public bool Delete(int Mid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GaugeItem ");
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
			strSql.Append("delete from GaugeItem ");
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
		public LearnSite.Model.GaugeItem GetModel(int Mid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Mid,Mgid,Mitem,Mscore,Msort from GaugeItem ");
			strSql.Append(" where Mid=@Mid");
			SqlParameter[] parameters = {
					new SqlParameter("@Mid", SqlDbType.Int,4)
			};
			parameters[0].Value = Mid;

			LearnSite.Model.GaugeItem model=new LearnSite.Model.GaugeItem();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Mid"]!=null && ds.Tables[0].Rows[0]["Mid"].ToString()!="")
				{
					model.Mid=int.Parse(ds.Tables[0].Rows[0]["Mid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Mgid"]!=null && ds.Tables[0].Rows[0]["Mgid"].ToString()!="")
				{
					model.Mgid=int.Parse(ds.Tables[0].Rows[0]["Mgid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Mitem"]!=null && ds.Tables[0].Rows[0]["Mitem"].ToString()!="")
				{
					model.Mitem=ds.Tables[0].Rows[0]["Mitem"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Mscore"]!=null && ds.Tables[0].Rows[0]["Mscore"].ToString()!="")
				{
					model.Mscore=int.Parse(ds.Tables[0].Rows[0]["Mscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Msort"]!=null && ds.Tables[0].Rows[0]["Msort"].ToString()!="")
				{
					model.Msort=int.Parse(ds.Tables[0].Rows[0]["Msort"].ToString());
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
			strSql.Append("select Mid,Mgid,Mitem,Mscore,Msort ");
			strSql.Append(" FROM GaugeItem ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 当活动中未指定互评评价标准时，自动选取相应作品类型中的第一条评价标准
        /// </summary>
        public DataSet GetListAutoGtype(string Gtype)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  Mid,Mgid,Mitem,Mscore,Msort ");
            strSql.Append(" FROM GaugeItem ");
            strSql.Append(" where Mgid in (select top 1 Gid from Gauge where  Gtype=@Gtype ) ");
            strSql.Append("  order by Msort asc ");
            SqlParameter[] parameters = {
					new SqlParameter("@Gtype", SqlDbType.NVarChar,50)};
			parameters[0].Value = Gtype;

            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }
        /// <summary>
        /// 获取自评选项
        /// </summary>
        /// <param name="Fwid"></param>
        /// <param name="Fnum"></param>
        /// <returns></returns>
        public DataSet GetMySelfMitems(string Fselect)
        {
            string mysql = "select Mitem from GaugeItem where Mid in ("+Fselect+")";
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
			strSql.Append(" Mid,Mgid,Mitem,Mscore,Msort ");
			strSql.Append(" FROM GaugeItem ");
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
			strSql.Append("select count(1) FROM GaugeItem ");
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
			strSql.Append(")AS Row, T.*  from GaugeItem T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

        public DataSet GetListMitems(int Mid)
        {
            string mysql = "select GaugeItem.Mid,GaugeItem.Mitem from GaugeItem,Mission where  Mission.Mid="+Mid+" and GaugeItem.Mgid=Mission.Mgid order by GaugeItem.Msort asc";
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
			parameters[0].Value = "GaugeItem";
			parameters[1].Value = "Mid";
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

