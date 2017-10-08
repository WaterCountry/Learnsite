using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:Summary
	/// </summary>
	public partial class Summary
	{
		public Summary()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Sid", "Summary"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Sid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Summary");
			strSql.Append(" where Sid=@Sid");
			SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4)
};
			parameters[0].Value = Sid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Summary model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Summary(");
			strSql.Append("Scid,Shid,Scontent,Sdate,Sgrade,Sclass,Syear,Sshow)");
			strSql.Append(" values (");
			strSql.Append("@Scid,@Shid,@Scontent,@Sdate,@Sgrade,@Sclass,@Syear,@Sshow)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Scid", SqlDbType.Int,4),
					new SqlParameter("@Shid", SqlDbType.Int,4),
					new SqlParameter("@Scontent", SqlDbType.NText),
					new SqlParameter("@Sdate", SqlDbType.DateTime),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),
					new SqlParameter("@Syear", SqlDbType.Int,4),
					new SqlParameter("@Sshow", SqlDbType.Bit,1)};
			parameters[0].Value = model.Scid;
			parameters[1].Value = model.Shid;
			parameters[2].Value = model.Scontent;
			parameters[3].Value = model.Sdate;
			parameters[4].Value = model.Sgrade;
			parameters[5].Value = model.Sclass;
			parameters[6].Value = model.Syear;
			parameters[7].Value = model.Sshow;

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
		public bool Update(LearnSite.Model.Summary model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Summary set ");
			strSql.Append("Scid=@Scid,");
			strSql.Append("Shid=@Shid,");
			strSql.Append("Scontent=@Scontent,");
			strSql.Append("Sdate=@Sdate,");
			strSql.Append("Sgrade=@Sgrade,");
			strSql.Append("Sclass=@Sclass,");
			strSql.Append("Syear=@Syear,");
			strSql.Append("Sshow=@Sshow");
			strSql.Append(" where Sid=@Sid");
			SqlParameter[] parameters = {
					new SqlParameter("@Scid", SqlDbType.Int,4),
					new SqlParameter("@Shid", SqlDbType.Int,4),
					new SqlParameter("@Scontent", SqlDbType.NText),
					new SqlParameter("@Sdate", SqlDbType.DateTime),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),
					new SqlParameter("@Syear", SqlDbType.Int,4),
					new SqlParameter("@Sshow", SqlDbType.Bit,1),
					new SqlParameter("@Sid", SqlDbType.Int,4)};
			parameters[0].Value = model.Scid;
			parameters[1].Value = model.Shid;
			parameters[2].Value = model.Scontent;
			parameters[3].Value = model.Sdate;
			parameters[4].Value = model.Sgrade;
			parameters[5].Value = model.Sclass;
			parameters[6].Value = model.Syear;
			parameters[7].Value = model.Sshow;
			parameters[8].Value = model.Sid;

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
		public bool Delete(int Sid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Summary ");
			strSql.Append(" where Sid=@Sid");
			SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4)
};
			parameters[0].Value = Sid;

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
		public bool DeleteList(string Sidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Summary ");
			strSql.Append(" where Sid in ("+Sidlist + ")  ");
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
        /// 得到指定活动内容的总结一个对象实体
        /// </summary>
        public LearnSite.Model.Summary GetModelByClass(int Scid,int Shid,int Sgrade,int Sclass)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Sid,Scid,Shid,Scontent,Sdate,Sgrade,Sclass,Syear,Sshow from Summary ");
            strSql.Append(" where Scid=@Scid and Shid=@Shid and Sgrade=@Sgrade and Sclass=@Sclass ");
            SqlParameter[] parameters = {
					new SqlParameter("@Scid", SqlDbType.Int,4),
                    new SqlParameter("@Shid", SqlDbType.Int,4),
                    new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4)
};
            parameters[0].Value = Scid;
            parameters[1].Value = Shid;
            parameters[2].Value = Sgrade;
            parameters[3].Value = Sclass;

            LearnSite.Model.Summary model = new LearnSite.Model.Summary();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Sid"].ToString() != "")
                {
                    model.Sid = int.Parse(ds.Tables[0].Rows[0]["Sid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Scid"].ToString() != "")
                {
                    model.Scid = int.Parse(ds.Tables[0].Rows[0]["Scid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Shid"].ToString() != "")
                {
                    model.Shid = int.Parse(ds.Tables[0].Rows[0]["Shid"].ToString());
                }
                model.Scontent = ds.Tables[0].Rows[0]["Scontent"].ToString();
                if (ds.Tables[0].Rows[0]["Sdate"].ToString() != "")
                {
                    model.Sdate = DateTime.Parse(ds.Tables[0].Rows[0]["Sdate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sgrade"].ToString() != "")
                {
                    model.Sgrade = int.Parse(ds.Tables[0].Rows[0]["Sgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sclass"].ToString() != "")
                {
                    model.Sclass = int.Parse(ds.Tables[0].Rows[0]["Sclass"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Syear"].ToString() != "")
                {
                    model.Syear = int.Parse(ds.Tables[0].Rows[0]["Syear"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sshow"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Sshow"].ToString() == "1") || (ds.Tables[0].Rows[0]["Sshow"].ToString().ToLower() == "true"))
                    {
                        model.Sshow = true;
                    }
                    else
                    {
                        model.Sshow = false;
                    }
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
		public LearnSite.Model.Summary GetModel(int Sid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Sid,Scid,Shid,Scontent,Sdate,Sgrade,Sclass,Syear,Sshow from Summary ");
			strSql.Append(" where Sid=@Sid");
			SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4)
};
			parameters[0].Value = Sid;

			LearnSite.Model.Summary model=new LearnSite.Model.Summary();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Sid"].ToString()!="")
				{
					model.Sid=int.Parse(ds.Tables[0].Rows[0]["Sid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Scid"].ToString()!="")
				{
					model.Scid=int.Parse(ds.Tables[0].Rows[0]["Scid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Shid"].ToString()!="")
				{
					model.Shid=int.Parse(ds.Tables[0].Rows[0]["Shid"].ToString());
				}
				model.Scontent=ds.Tables[0].Rows[0]["Scontent"].ToString();
				if(ds.Tables[0].Rows[0]["Sdate"].ToString()!="")
				{
					model.Sdate=DateTime.Parse(ds.Tables[0].Rows[0]["Sdate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sgrade"].ToString()!="")
				{
					model.Sgrade=int.Parse(ds.Tables[0].Rows[0]["Sgrade"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sclass"].ToString()!="")
				{
					model.Sclass=int.Parse(ds.Tables[0].Rows[0]["Sclass"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Syear"].ToString()!="")
				{
					model.Syear=int.Parse(ds.Tables[0].Rows[0]["Syear"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sshow"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Sshow"].ToString()=="1")||(ds.Tables[0].Rows[0]["Sshow"].ToString().ToLower()=="true"))
					{
						model.Sshow=true;
					}
					else
					{
						model.Sshow=false;
					}
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
			strSql.Append("select Sid,Scid,Shid,Scontent,Sdate,Sgrade,Sclass,Syear,Sshow ");
			strSql.Append(" FROM Summary ");
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
			strSql.Append(" Sid,Scid,Shid,Scontent,Sdate,Sgrade,Sclass,Syear,Sshow ");
			strSql.Append(" FROM Summary ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[0].Value = "Summary";
			parameters[1].Value = "Sid";
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

