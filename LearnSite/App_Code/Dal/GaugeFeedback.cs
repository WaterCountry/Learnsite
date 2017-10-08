using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:GaugeFeedback
	/// </summary>
	public partial class GaugeFeedback
	{
		public GaugeFeedback()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Fid", "GaugeFeedback"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Fid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from GaugeFeedback");
			strSql.Append(" where Fid=@Fid");
			SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4)
			};
			parameters[0].Value = Fid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsFnum(int Fwid,int Fsid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from GaugeFeedback");
            strSql.Append(" where Fwid=@Fwid and Fsid=@Fsid");
            SqlParameter[] parameters = {
					new SqlParameter("@Fwid", SqlDbType.Int,4),                    
					new SqlParameter("@Fsid",  SqlDbType.Int,4)
			};
            parameters[0].Value = Fwid;
            parameters[1].Value = Fsid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsFselect(int Fgid, string Fselect)
        {
            string Mids ="%"+ Fselect + ","+"%";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from GaugeFeedback");
            strSql.Append(" where Fgid=@Fgid and Fselect like @Mids");
            SqlParameter[] parameters = {
					new SqlParameter("@Fgid", SqlDbType.Int,4),                    
					new SqlParameter("@Mids",  SqlDbType.NVarChar,50)
			};
            parameters[0].Value = Fgid;
            parameters[1].Value = Mids;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 求某个作品互评的平均分
        /// </summary>
        /// <param name="Fwid"></param>
        /// <returns></returns>
        public int AvgFwid(int Fwid)
        {
            string mysql = "select avg(Fscore) from GaugeFeedback where Fwid="+Fwid;
            string findstr = DbHelperSQL.FindString(mysql);
            if (findstr != "")
            {
                return Int32.Parse(findstr);
            }
            else
            {
                return 0;
            }
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.GaugeFeedback model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into GaugeFeedback(");
			strSql.Append("Fnum,Fgrade,Fclass,Fcid,Fmid,Fwid,Fgid,Fselect,Fscore,Fgood,Fdate,Fsid)");
			strSql.Append(" values (");
			strSql.Append("@Fnum,@Fgrade,@Fclass,@Fcid,@Fmid,@Fwid,@Fgid,@Fselect,@Fscore,@Fgood,@Fdate,@Fsid)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Fnum",  SqlDbType.NVarChar,50),
					new SqlParameter("@Fgrade", SqlDbType.Int,4),
					new SqlParameter("@Fclass", SqlDbType.Int,4),
					new SqlParameter("@Fcid", SqlDbType.Int,4),
					new SqlParameter("@Fmid", SqlDbType.Int,4),
					new SqlParameter("@Fwid", SqlDbType.Int,4),
					new SqlParameter("@Fgid", SqlDbType.Int,4),
					new SqlParameter("@Fselect", SqlDbType.NVarChar,50),
					new SqlParameter("@Fscore", SqlDbType.Int,4),
					new SqlParameter("@Fgood", SqlDbType.Bit,1),
					new SqlParameter("@Fdate", SqlDbType.DateTime),
					new SqlParameter("@Fsid", SqlDbType.Int,4)};
			parameters[0].Value = model.Fnum;
			parameters[1].Value = model.Fgrade;
			parameters[2].Value = model.Fclass;
			parameters[3].Value = model.Fcid;
			parameters[4].Value = model.Fmid;
			parameters[5].Value = model.Fwid;
			parameters[6].Value = model.Fgid;
			parameters[7].Value = model.Fselect;
			parameters[8].Value = model.Fscore;
			parameters[9].Value = model.Fgood;
			parameters[10].Value = model.Fdate;
            parameters[11].Value = model.Fsid;

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
		public bool Update(LearnSite.Model.GaugeFeedback model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update GaugeFeedback set ");
			strSql.Append("Fnum=@Fnum,");
			strSql.Append("Fgrade=@Fgrade,");
			strSql.Append("Fclass=@Fclass,");
			strSql.Append("Fcid=@Fcid,");
			strSql.Append("Fmid=@Fmid,");
			strSql.Append("Fwid=@Fwid,");
			strSql.Append("Fgid=@Fgid,");
			strSql.Append("Fselect=@Fselect,");
			strSql.Append("Fscore=@Fscore,");
			strSql.Append("Fgood=@Fgood,");
			strSql.Append("Fdate=@Fdate");
			strSql.Append(" where Fid=@Fid");
			SqlParameter[] parameters = {
					new SqlParameter("@Fnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Fgrade", SqlDbType.Int,4),
					new SqlParameter("@Fclass", SqlDbType.Int,4),
					new SqlParameter("@Fcid", SqlDbType.Int,4),
					new SqlParameter("@Fmid", SqlDbType.Int,4),
					new SqlParameter("@Fwid", SqlDbType.Int,4),
					new SqlParameter("@Fgid", SqlDbType.Int,4),
					new SqlParameter("@Fselect", SqlDbType.NVarChar,50),
					new SqlParameter("@Fscore", SqlDbType.Int,4),
					new SqlParameter("@Fgood", SqlDbType.Bit,1),
					new SqlParameter("@Fdate", SqlDbType.DateTime),
					new SqlParameter("@Fid", SqlDbType.Int,4)};
			parameters[0].Value = model.Fnum;
			parameters[1].Value = model.Fgrade;
			parameters[2].Value = model.Fclass;
			parameters[3].Value = model.Fcid;
			parameters[4].Value = model.Fmid;
			parameters[5].Value = model.Fwid;
			parameters[6].Value = model.Fgid;
			parameters[7].Value = model.Fselect;
			parameters[8].Value = model.Fscore;
			parameters[9].Value = model.Fgood;
			parameters[10].Value = model.Fdate;
			parameters[11].Value = model.Fid;

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
		public bool Delete(int Fid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GaugeFeedback ");
			strSql.Append(" where Fid=@Fid");
			SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4)
			};
			parameters[0].Value = Fid;

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
		public bool DeleteList(string Fidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GaugeFeedback ");
			strSql.Append(" where Fid in ("+Fidlist + ")  ");
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
		public LearnSite.Model.GaugeFeedback GetModel(int Fid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Fid,Fnum,Fgrade,Fclass,Fcid,Fmid,Fwid,Fgid,Fselect,Fscore,Fgood,Fdate from GaugeFeedback ");
			strSql.Append(" where Fid=@Fid");
			SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4)
			};
			parameters[0].Value = Fid;

			LearnSite.Model.GaugeFeedback model=new LearnSite.Model.GaugeFeedback();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Fid"]!=null && ds.Tables[0].Rows[0]["Fid"].ToString()!="")
				{
					model.Fid=int.Parse(ds.Tables[0].Rows[0]["Fid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fnum"]!=null && ds.Tables[0].Rows[0]["Fnum"].ToString()!="")
				{
					model.Fnum=ds.Tables[0].Rows[0]["Fnum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Fgrade"]!=null && ds.Tables[0].Rows[0]["Fgrade"].ToString()!="")
				{
					model.Fgrade=int.Parse(ds.Tables[0].Rows[0]["Fgrade"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fclass"]!=null && ds.Tables[0].Rows[0]["Fclass"].ToString()!="")
				{
					model.Fclass=int.Parse(ds.Tables[0].Rows[0]["Fclass"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fcid"]!=null && ds.Tables[0].Rows[0]["Fcid"].ToString()!="")
				{
					model.Fcid=int.Parse(ds.Tables[0].Rows[0]["Fcid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fmid"]!=null && ds.Tables[0].Rows[0]["Fmid"].ToString()!="")
				{
					model.Fmid=int.Parse(ds.Tables[0].Rows[0]["Fmid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fwid"]!=null && ds.Tables[0].Rows[0]["Fwid"].ToString()!="")
				{
					model.Fwid=int.Parse(ds.Tables[0].Rows[0]["Fwid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fgid"]!=null && ds.Tables[0].Rows[0]["Fgid"].ToString()!="")
				{
					model.Fgid=int.Parse(ds.Tables[0].Rows[0]["Fgid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fselect"]!=null && ds.Tables[0].Rows[0]["Fselect"].ToString()!="")
				{
					model.Fselect=ds.Tables[0].Rows[0]["Fselect"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Fscore"]!=null && ds.Tables[0].Rows[0]["Fscore"].ToString()!="")
				{
					model.Fscore=int.Parse(ds.Tables[0].Rows[0]["Fscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fgood"]!=null && ds.Tables[0].Rows[0]["Fgood"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Fgood"].ToString()=="1")||(ds.Tables[0].Rows[0]["Fgood"].ToString().ToLower()=="true"))
					{
						model.Fgood=true;
					}
					else
					{
						model.Fgood=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Fdate"]!=null && ds.Tables[0].Rows[0]["Fdate"].ToString()!="")
				{
					model.Fdate=DateTime.Parse(ds.Tables[0].Rows[0]["Fdate"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}
        /// <summary>
        /// 返回本作品的所有互评选项
        /// </summary>
        /// <param name="Fwid"></param>
        /// <returns></returns>
        public string GetWorkFeedback(int Fwid)
        {
            string feedback = "";
            string mysql = "select Fselect,Fgood from GaugeFeedback where Fwid=" + Fwid;
            DataSet ds = DbHelperSQL.Query(mysql);
            DataTable dt = ds.Tables[0];
            int rcount = dt.Rows.Count;
            if (rcount > 0)
            {
                for (int i = 0; i < rcount; i++)
                {
                    feedback = feedback + dt.Rows[i]["Fselect"].ToString();
                    if ((dt.Rows[i]["Fgood"].ToString() == "1") || (dt.Rows[i]["Fgood"].ToString().ToLower() == "true"))
                    {
                        feedback = feedback + "T,";
                    }
                }
            }
            return feedback;
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Fid,Fnum,Fgrade,Fclass,Fcid,Fmid,Fwid,Fgid,Fselect,Fscore,Fgood,Fdate ");
			strSql.Append(" FROM GaugeFeedback ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 返回自己的评价
        /// </summary>
        /// <param name="Fwid"></param>
        /// <param name="Fnum"></param>
        /// <returns></returns>
        public string GetMyFeedback(int Fwid, string Fnum)
        {
            string mysql = "select Fselect from GaugeFeedback where Fwid="+Fwid+" and Fnum='"+Fnum+"'";
            return DbHelperSQL.FindString(mysql);
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
			strSql.Append(" Fid,Fnum,Fgrade,Fclass,Fcid,Fmid,Fwid,Fgid,Fselect,Fscore,Fgood,Fdate ");
			strSql.Append(" FROM GaugeFeedback ");
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
			strSql.Append("select count(1) FROM GaugeFeedback ");
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
				strSql.Append("order by T.Fid desc");
			}
			strSql.Append(")AS Row, T.*  from GaugeFeedback T ");
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
			parameters[0].Value = "GaugeFeedback";
			parameters[1].Value = "Fid";
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

