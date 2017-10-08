using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:SurveyItem
	/// </summary>
	public partial class SurveyItem
	{
		public SurveyItem()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Mid", "SurveyItem"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Mid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SurveyItem");
			strSql.Append(" where Mid=@Mid");
			SqlParameter[] parameters = {
					new SqlParameter("@Mid", SqlDbType.Int,4)
			};
			parameters[0].Value = Mid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在该调查题Mqid的选项记录
        /// </summary>
        public bool ExistsMqid(int Mqid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SurveyItem");
            strSql.Append(" where Mqid=@Mqid");
            SqlParameter[] parameters = {
					new SqlParameter("@Mqid", SqlDbType.Int,4)
			};
            parameters[0].Value = Mqid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.SurveyItem model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SurveyItem(");
			strSql.Append("Mqid,Mvid,Mitem,Mscore,Mcount,Mcid)");
			strSql.Append(" values (");
			strSql.Append("@Mqid,@Mvid,@Mitem,@Mscore,@Mcount,@Mcid)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Mqid", SqlDbType.Int,4),
					new SqlParameter("@Mvid", SqlDbType.Int,4),
					new SqlParameter("@Mitem", SqlDbType.NText),
					new SqlParameter("@Mscore", SqlDbType.Int,4),
					new SqlParameter("@Mcount", SqlDbType.Int,4),
					new SqlParameter("@Mcid", SqlDbType.Int,4)};
			parameters[0].Value = model.Mqid;
			parameters[1].Value = model.Mvid;
			parameters[2].Value = model.Mitem;
			parameters[3].Value = model.Mscore;
            parameters[4].Value = model.Mcount;
            parameters[5].Value = model.Mcid;

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
        /// 更新一条选项数据
        /// </summary>
        public bool UpdateItem(int Mid,string Mitem,int Mscore)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SurveyItem set ");
            strSql.Append("Mitem=@Mitem,");
            strSql.Append("Mscore=@Mscore");
            strSql.Append(" where Mid=@Mid");
            SqlParameter[] parameters = {
					new SqlParameter("@Mitem", SqlDbType.NText),
					new SqlParameter("@Mscore", SqlDbType.Int,4),
					new SqlParameter("@Mid", SqlDbType.Int,4)};
            parameters[0].Value = Mitem;
            parameters[1].Value = Mscore;
            parameters[2].Value = Mid;

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
		public bool Update(LearnSite.Model.SurveyItem model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SurveyItem set ");
			strSql.Append("Mqid=@Mqid,");
			strSql.Append("Mvid=@Mvid,");
			strSql.Append("Mitem=@Mitem,");
			strSql.Append("Mscore=@Mscore,");
			strSql.Append("Mcount=@Mcount");
			strSql.Append(" where Mid=@Mid");
			SqlParameter[] parameters = {
					new SqlParameter("@Mqid", SqlDbType.Int,4),
					new SqlParameter("@Mvid", SqlDbType.Int,4),
					new SqlParameter("@Mitem", SqlDbType.NText),
					new SqlParameter("@Mscore", SqlDbType.Int,4),
					new SqlParameter("@Mcount", SqlDbType.Int,4),
					new SqlParameter("@Mid", SqlDbType.Int,4)};
			parameters[0].Value = model.Mqid;
			parameters[1].Value = model.Mvid;
			parameters[2].Value = model.Mitem;
			parameters[3].Value = model.Mscore;
			parameters[4].Value = model.Mcount;
			parameters[5].Value = model.Mid;

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
		public bool Delete(int Mid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SurveyItem ");
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
			strSql.Append("delete from SurveyItem ");
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
        /// 删除该试题下的所有选项
        /// </summary>
        /// <param name="Mqid"></param>
        public void DelAllMqid(int Mqid)
        {
            string mysql = "delete from SurveyItem where Mqid=" + Mqid;
            DbHelperSQL.ExecuteSql(mysql);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.SurveyItem GetModel(int Mid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Mid,Mqid,Mvid,Mitem,Mscore,Mcount,Mcid from SurveyItem ");
			strSql.Append(" where Mid=@Mid");
			SqlParameter[] parameters = {
					new SqlParameter("@Mid", SqlDbType.Int,4)
			};
			parameters[0].Value = Mid;

			LearnSite.Model.SurveyItem model=new LearnSite.Model.SurveyItem();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Mid"]!=null && ds.Tables[0].Rows[0]["Mid"].ToString()!="")
				{
					model.Mid=int.Parse(ds.Tables[0].Rows[0]["Mid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Mqid"]!=null && ds.Tables[0].Rows[0]["Mqid"].ToString()!="")
				{
					model.Mqid=int.Parse(ds.Tables[0].Rows[0]["Mqid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Mvid"]!=null && ds.Tables[0].Rows[0]["Mvid"].ToString()!="")
				{
					model.Mvid=int.Parse(ds.Tables[0].Rows[0]["Mvid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Mitem"]!=null && ds.Tables[0].Rows[0]["Mitem"].ToString()!="")
				{
					model.Mitem=ds.Tables[0].Rows[0]["Mitem"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Mscore"]!=null && ds.Tables[0].Rows[0]["Mscore"].ToString()!="")
				{
					model.Mscore=int.Parse(ds.Tables[0].Rows[0]["Mscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Mcount"]!=null && ds.Tables[0].Rows[0]["Mcount"].ToString()!="")
				{
					model.Mcount=int.Parse(ds.Tables[0].Rows[0]["Mcount"].ToString());
				}
                if (ds.Tables[0].Rows[0]["Mcid"] != null && ds.Tables[0].Rows[0]["Mcid"].ToString() != "")
                {
                    model.Mid = int.Parse(ds.Tables[0].Rows[0]["Mcid"].ToString());
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
        public LearnSite.Model.SurveyItem GetModel(DataTable dt, int Tsort)
        {
            LearnSite.Model.SurveyItem model = new LearnSite.Model.SurveyItem();
            int Count = dt.Rows.Count;
            if (Count > 0)
            {
                if (Tsort < Count)
                {
                    if (dt.Rows[Tsort]["Mid"] != null && dt.Rows[Tsort]["Mid"].ToString() != "")
                    {
                        model.Mid = int.Parse(dt.Rows[Tsort]["Mid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Mqid"] != null && dt.Rows[Tsort]["Mqid"].ToString() != "")
                    {
                        model.Mqid = int.Parse(dt.Rows[Tsort]["Mqid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Mvid"] != null && dt.Rows[Tsort]["Mvid"].ToString() != "")
                    {
                        model.Mvid = int.Parse(dt.Rows[Tsort]["Mvid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Mitem"] != null && dt.Rows[Tsort]["Mitem"].ToString() != "")
                    {
                        model.Mitem = dt.Rows[Tsort]["Mitem"].ToString();
                    }
                    if (dt.Rows[Tsort]["Mscore"] != null && dt.Rows[Tsort]["Mscore"].ToString() != "")
                    {
                        model.Mscore = int.Parse(dt.Rows[Tsort]["Mscore"].ToString());
                    }
                    if (dt.Rows[Tsort]["Mcount"] != null && dt.Rows[Tsort]["Mcount"].ToString() != "")
                    {
                        model.Mcount = int.Parse(dt.Rows[Tsort]["Mcount"].ToString());
                    }
                    if (dt.Rows[Tsort]["Mcid"] != null && dt.Rows[Tsort]["Mcid"].ToString() != "")
                    {
                        model.Mcid = int.Parse(dt.Rows[Tsort]["Mcid"].ToString());
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
        /// 获得数据列表Mid,Mitem，随机排序
        /// </summary>
        public DataSet GetListItem(int Mqid)
        {
            string strSql = "select Mid,Mitem FROM SurveyItem where Mqid=" + Mqid + " order by NewID()";
            return DbHelperSQL.Query(strSql);
        }
        /// <summary>
        /// 获得数据列表Mid,Mitem,Mcount，按Mid排序
        /// </summary>
        public DataTable GetListItemByMvid(int Mvid)
        {
            string strSql = "select Mid,Mqid,Mitem,Mcount FROM SurveyItem where Mvid=" + Mvid + " order by Mid asc";
            DataSet ds = DbHelperSQL.Query(strSql);
            return ds.Tables[0];
        }
        /// <summary>
        /// 返回调查试题所有选项和选中次数selectStr | countStr
        /// </summary>
        /// <param name="Mvid"></param>
        /// <param name="Allselect"></param>
        /// <returns></returns>
        public string GetListItemAndCount(int Mvid, string Allselect)
        {
            string ffStr = "";
            string selectStr = "";
            string countStr="";
            string strSql = "select Mid FROM SurveyItem where Mvid=" + Mvid + " order by Mid asc";
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    string Mid = dt.Rows[i]["Mid"].ToString();
                    selectStr = selectStr + Mid+",";
                    countStr =countStr+ Common.WordProcess.StrCount(Allselect, Mid,',')+",";
                }
                if (selectStr.EndsWith(","))
                {
                    selectStr = selectStr.Substring(0, selectStr.Length - 1);
                }
                if (countStr.EndsWith(","))
                {
                    countStr = countStr.Substring(0, countStr.Length - 1);
                }
                ffStr = selectStr + "|" + countStr;
            }
            return ffStr;
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Mid,Mqid,Mvid,Mitem,Mscore,Mcount,Mcid ");
			strSql.Append(" FROM SurveyItem ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 统计选中记录分值
        /// </summary>
        public int GetItemScore(string fselect)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(Mscore) FROM SurveyItem ");
            string strWhere = " Mid in ("+fselect+") ";
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            strSql.Append(" Mid,Mqid,Mvid,Mitem,Mscore,Mcount,Mcid ");
			strSql.Append(" FROM SurveyItem ");
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
        public int GetItemCount(int Mqid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM SurveyItem ");
            string strWhere = " Mqid="+Mqid;
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM SurveyItem ");
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
			strSql.Append(")AS Row, T.*  from SurveyItem T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获取选项内容
        /// </summary>
        /// <param name="Mid"></param>
        /// <returns></returns>
        public string GetMitem(int Mid)
        {
            string mysql = "select Mitem from SurveyItem where Mid="+Mid;
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
			parameters[0].Value = "SurveyItem";
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

