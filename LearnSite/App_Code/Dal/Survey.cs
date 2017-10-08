using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:Survey
	/// </summary>
	public partial class Survey
	{
		public Survey()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Vid", "Survey"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Vid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Survey");
			strSql.Append(" where Vid=@Vid");
			SqlParameter[] parameters = {
					new SqlParameter("@Vid", SqlDbType.Int,4)
			};
			parameters[0].Value = Vid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Addsurvey(LearnSite.Model.Survey model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Survey(");
            strSql.Append("Vcid,Vhid,Vtitle,Vcontent,Vtype,Vclose,Vpoint,Vdate)");
            strSql.Append(" values (");
            strSql.Append("@Vcid,@Vhid,@Vtitle,@Vcontent,@Vtype,@Vclose,@Vpoint,@Vdate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Vcid", SqlDbType.Int,4),
					new SqlParameter("@Vhid", SqlDbType.Int,4),
					new SqlParameter("@Vtitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Vcontent", SqlDbType.NText),
					new SqlParameter("@Vtype", SqlDbType.Int,4),
					new SqlParameter("@Vclose", SqlDbType.Bit,1),
					new SqlParameter("@Vpoint", SqlDbType.Bit,1),
					new SqlParameter("@Vdate", SqlDbType.DateTime)};
            parameters[0].Value = model.Vcid;
            parameters[1].Value = model.Vhid;
            parameters[2].Value = model.Vtitle;
            parameters[3].Value = model.Vcontent;
            parameters[4].Value = model.Vtype;
            parameters[5].Value = model.Vclose;
            parameters[6].Value = model.Vpoint;
            parameters[7].Value = model.Vdate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Survey model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Survey(");
			strSql.Append("Vcid,Vhid,Vtitle,Vcontent,Vtype,Vtotal,Vscore,Vclose,Vpoint,Vdate)");
			strSql.Append(" values (");
			strSql.Append("@Vcid,@Vhid,@Vtitle,@Vcontent,@Vtype,@Vtotal,@Vscore,@Vclose,@Vpoint,@Vdate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Vcid", SqlDbType.Int,4),
					new SqlParameter("@Vhid", SqlDbType.Int,4),
					new SqlParameter("@Vtitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Vcontent", SqlDbType.NText),
					new SqlParameter("@Vtype", SqlDbType.Int,4),
					new SqlParameter("@Vtotal", SqlDbType.Int,4),
					new SqlParameter("@Vscore", SqlDbType.Int,4),
					new SqlParameter("@Vclose", SqlDbType.Bit,1),
					new SqlParameter("@Vpoint", SqlDbType.Bit,1),
					new SqlParameter("@Vdate", SqlDbType.DateTime)};
			parameters[0].Value = model.Vcid;
			parameters[1].Value = model.Vhid;
			parameters[2].Value = model.Vtitle;
			parameters[3].Value = model.Vcontent;
			parameters[4].Value = model.Vtype;
			parameters[5].Value = model.Vtotal;
			parameters[6].Value = model.Vscore;
			parameters[7].Value = model.Vclose;
			parameters[8].Value = model.Vpoint;
			parameters[9].Value = model.Vdate;

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
        /// 关闭该教师的所有调查
        /// </summary>
        /// <param name="Vid"></param>
        public void SetClose(int Vhid)
        {
            string mysql = "update Survey set Vclose=1 where Vhid=" + Vhid;
            DbHelperSQL.ExecuteSql(mysql);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.Survey model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Survey set ");
			strSql.Append("Vcid=@Vcid,");
			strSql.Append("Vhid=@Vhid,");
			strSql.Append("Vtitle=@Vtitle,");
			strSql.Append("Vcontent=@Vcontent,");
			strSql.Append("Vtype=@Vtype,");
			strSql.Append("Vtotal=@Vtotal,");
			strSql.Append("Vscore=@Vscore,");
			strSql.Append("Vaverage=@Vaverage,");
			strSql.Append("Vclose=@Vclose,");
			strSql.Append("Vpoint=@Vpoint,");
			strSql.Append("Vdate=@Vdate");
			strSql.Append(" where Vid=@Vid");
			SqlParameter[] parameters = {
					new SqlParameter("@Vcid", SqlDbType.Int,4),
					new SqlParameter("@Vhid", SqlDbType.Int,4),
					new SqlParameter("@Vtitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Vcontent", SqlDbType.NText),
					new SqlParameter("@Vtype", SqlDbType.Int,4),
					new SqlParameter("@Vtotal", SqlDbType.Int,4),
					new SqlParameter("@Vscore", SqlDbType.Int,4),
					new SqlParameter("@Vaverage", SqlDbType.Int,4),
					new SqlParameter("@Vclose", SqlDbType.Bit,1),
					new SqlParameter("@Vpoint", SqlDbType.Bit,1),
					new SqlParameter("@Vdate", SqlDbType.DateTime),
					new SqlParameter("@Vid", SqlDbType.Int,4)};
			parameters[0].Value = model.Vcid;
			parameters[1].Value = model.Vhid;
			parameters[2].Value = model.Vtitle;
			parameters[3].Value = model.Vcontent;
			parameters[4].Value = model.Vtype;
			parameters[5].Value = model.Vtotal;
			parameters[6].Value = model.Vscore;
			parameters[7].Value = model.Vaverage;
			parameters[8].Value = model.Vclose;
			parameters[9].Value = model.Vpoint;
			parameters[10].Value = model.Vdate;
			parameters[11].Value = model.Vid;

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateSurvey(LearnSite.Model.Survey model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Survey set ");
            strSql.Append("Vcid=@Vcid,");
            strSql.Append("Vhid=@Vhid,");
            strSql.Append("Vtitle=@Vtitle,");
            strSql.Append("Vcontent=@Vcontent,");
            strSql.Append("Vtype=@Vtype,");
            strSql.Append("Vclose=@Vclose,");
            strSql.Append("Vpoint=@Vpoint,");
            strSql.Append("Vdate=@Vdate");
            strSql.Append(" where Vid=@Vid");
            SqlParameter[] parameters = {
					new SqlParameter("@Vcid", SqlDbType.Int,4),
					new SqlParameter("@Vhid", SqlDbType.Int,4),
					new SqlParameter("@Vtitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Vcontent", SqlDbType.NText),
					new SqlParameter("@Vtype", SqlDbType.Int,4),
					new SqlParameter("@Vclose", SqlDbType.Bit,1),
					new SqlParameter("@Vpoint", SqlDbType.Bit,1),
					new SqlParameter("@Vdate", SqlDbType.DateTime),
					new SqlParameter("@Vid", SqlDbType.Int,4)};
            parameters[0].Value = model.Vcid;
            parameters[1].Value = model.Vhid;
            parameters[2].Value = model.Vtitle;
            parameters[3].Value = model.Vcontent;
            parameters[4].Value = model.Vtype;
            parameters[5].Value = model.Vclose;
            parameters[6].Value = model.Vpoint;
            parameters[7].Value = model.Vdate;
            parameters[8].Value = model.Vid;

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
        /// 开关调查,取反
        /// </summary>
        /// <param name="Vid"></param>
        /// <returns></returns>
        public bool UpdateVclose(int Vid)
        {
            string mysql = "update Survey set Vclose= Vclose^1  where Vid=" + Vid;
            int rowsAffected = DbHelperSQL.ExecuteSql(mysql);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 统计调查卷下的试题数
        /// </summary>
        /// <param name="Vid"></param>
        /// <returns></returns>
        public bool Updatevtotal(int Vid)
        {
            string mysql = "update Survey set Vtotal= (select count(*) from SurveyQuestion where Qvid="+Vid+")  where Vid=" + Vid;
            int rowsAffected = DbHelperSQL.ExecuteSql(mysql);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 统计调查卷下的试题总分
        /// </summary>
        /// <param name="Vid"></param>
        /// <returns></returns>
        public bool Updatevscore(int Vid)
        {
            string mysql = "update Survey set Vtotal= (select sum(Mscore) from SurveyItem where Mvid=" + Vid + ")  where Vid=" + Vid;
            int rowsAffected = DbHelperSQL.ExecuteSql(mysql);
            if (rowsAffected > 0)
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
		public bool Delete(int Vid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Survey ");
			strSql.Append(" where Vid=@Vid");
			SqlParameter[] parameters = {
					new SqlParameter("@Vid", SqlDbType.Int,4)
			};
			parameters[0].Value = Vid;

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
		public bool DeleteList(string Vidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Survey ");
			strSql.Append(" where Vid in ("+Vidlist + ")  ");
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
        /// 返回调查名称Vtitle
        /// </summary>
        /// <param name="Vid"></param>
        /// <returns></returns>
        public string GetVtitle(int Vid)
        {
            string mysql = "select Vtitle from Survey where Vid="+Vid;
            return DbHelperSQL.FindString(mysql);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Survey GetModel(int Vid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Vid,Vcid,Vhid,Vtitle,Vcontent,Vtype,Vtotal,Vscore,Vaverage,Vclose,Vpoint,Vdate from Survey ");
			strSql.Append(" where Vid=@Vid");
			SqlParameter[] parameters = {
					new SqlParameter("@Vid", SqlDbType.Int,4)
			};
			parameters[0].Value = Vid;

			LearnSite.Model.Survey model=new LearnSite.Model.Survey();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Vid"]!=null && ds.Tables[0].Rows[0]["Vid"].ToString()!="")
				{
					model.Vid=int.Parse(ds.Tables[0].Rows[0]["Vid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Vcid"]!=null && ds.Tables[0].Rows[0]["Vcid"].ToString()!="")
				{
					model.Vcid=int.Parse(ds.Tables[0].Rows[0]["Vcid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Vhid"]!=null && ds.Tables[0].Rows[0]["Vhid"].ToString()!="")
				{
					model.Vhid=int.Parse(ds.Tables[0].Rows[0]["Vhid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Vtitle"]!=null && ds.Tables[0].Rows[0]["Vtitle"].ToString()!="")
				{
					model.Vtitle=ds.Tables[0].Rows[0]["Vtitle"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Vcontent"]!=null && ds.Tables[0].Rows[0]["Vcontent"].ToString()!="")
				{
					model.Vcontent=ds.Tables[0].Rows[0]["Vcontent"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Vtype"]!=null && ds.Tables[0].Rows[0]["Vtype"].ToString()!="")
				{
					model.Vtype=int.Parse(ds.Tables[0].Rows[0]["Vtype"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Vtotal"]!=null && ds.Tables[0].Rows[0]["Vtotal"].ToString()!="")
				{
					model.Vtotal=int.Parse(ds.Tables[0].Rows[0]["Vtotal"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Vscore"]!=null && ds.Tables[0].Rows[0]["Vscore"].ToString()!="")
				{
					model.Vscore=int.Parse(ds.Tables[0].Rows[0]["Vscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Vaverage"]!=null && ds.Tables[0].Rows[0]["Vaverage"].ToString()!="")
				{
					model.Vaverage=int.Parse(ds.Tables[0].Rows[0]["Vaverage"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Vclose"]!=null && ds.Tables[0].Rows[0]["Vclose"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Vclose"].ToString()=="1")||(ds.Tables[0].Rows[0]["Vclose"].ToString().ToLower()=="true"))
					{
						model.Vclose=true;
					}
					else
					{
						model.Vclose=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Vpoint"]!=null && ds.Tables[0].Rows[0]["Vpoint"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Vpoint"].ToString()=="1")||(ds.Tables[0].Rows[0]["Vpoint"].ToString().ToLower()=="true"))
					{
						model.Vpoint=true;
					}
					else
					{
						model.Vpoint=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Vdate"]!=null && ds.Tables[0].Rows[0]["Vdate"].ToString()!="")
				{
					model.Vdate=DateTime.Parse(ds.Tables[0].Rows[0]["Vdate"].ToString());
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
        public LearnSite.Model.Survey GetModel(DataTable dt, int Tsort)
        {
            LearnSite.Model.Survey model = new LearnSite.Model.Survey();
            int Count = dt.Rows.Count;
            if (Count > 0)
            {
                if (Tsort < Count)
                {
                    if (dt.Rows[Tsort]["Vid"] != null && dt.Rows[Tsort]["Vid"].ToString() != "")
                    {
                        model.Vid = int.Parse(dt.Rows[Tsort]["Vid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Vcid"] != null && dt.Rows[Tsort]["Vcid"].ToString() != "")
                    {
                        model.Vcid = int.Parse(dt.Rows[Tsort]["Vcid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Vhid"] != null && dt.Rows[Tsort]["Vhid"].ToString() != "")
                    {
                        model.Vhid = int.Parse(dt.Rows[Tsort]["Vhid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Vtitle"] != null && dt.Rows[Tsort]["Vtitle"].ToString() != "")
                    {
                        model.Vtitle = dt.Rows[Tsort]["Vtitle"].ToString();
                    }
                    if (dt.Rows[Tsort]["Vcontent"] != null && dt.Rows[Tsort]["Vcontent"].ToString() != "")
                    {
                        model.Vcontent = dt.Rows[Tsort]["Vcontent"].ToString();
                    }
                    if (dt.Rows[Tsort]["Vtype"] != null && dt.Rows[Tsort]["Vtype"].ToString() != "")
                    {
                        model.Vtype = int.Parse(dt.Rows[Tsort]["Vtype"].ToString());
                    }
                    if (dt.Rows[Tsort]["Vtotal"] != null && dt.Rows[Tsort]["Vtotal"].ToString() != "")
                    {
                        model.Vtotal = int.Parse(dt.Rows[Tsort]["Vtotal"].ToString());
                    }
                    if (dt.Rows[Tsort]["Vscore"] != null && dt.Rows[Tsort]["Vscore"].ToString() != "")
                    {
                        model.Vscore = int.Parse(dt.Rows[Tsort]["Vscore"].ToString());
                    }
                    if (dt.Rows[Tsort]["Vaverage"] != null && dt.Rows[Tsort]["Vaverage"].ToString() != "")
                    {
                        model.Vaverage = int.Parse(dt.Rows[Tsort]["Vaverage"].ToString());
                    }
                    if (dt.Rows[Tsort]["Vclose"] != null && dt.Rows[Tsort]["Vclose"].ToString() != "")
                    {
                        if ((dt.Rows[Tsort]["Vclose"].ToString() == "1") || (dt.Rows[Tsort]["Vclose"].ToString().ToLower() == "true"))
                        {
                            model.Vclose = true;
                        }
                        else
                        {
                            model.Vclose = false;
                        }
                    }
                    if (dt.Rows[Tsort]["Vpoint"] != null && dt.Rows[Tsort]["Vpoint"].ToString() != "")
                    {
                        if ((dt.Rows[Tsort]["Vpoint"].ToString() == "1") || (dt.Rows[Tsort]["Vpoint"].ToString().ToLower() == "true"))
                        {
                            model.Vpoint = true;
                        }
                        else
                        {
                            model.Vpoint = false;
                        }
                    }
                    if (dt.Rows[Tsort]["Vdate"] != null && dt.Rows[Tsort]["Vdate"].ToString() != "")
                    {
                        model.Vdate = DateTime.Parse(dt.Rows[Tsort]["Vdate"].ToString());
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Vid,Vcid,Vhid,Vtitle,Vcontent,Vtype,Vtotal,Vscore,Vaverage,Vclose,Vpoint,Vdate ");
			strSql.Append(" FROM Survey ");
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
			strSql.Append(" Vid,Vcid,Vhid,Vtitle,Vcontent,Vtype,Vtotal,Vscore,Vaverage,Vclose,Vpoint,Vdate ");
			strSql.Append(" FROM Survey ");
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
			strSql.Append("select count(1) FROM Survey ");
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
				strSql.Append("order by T.Vid desc");
			}
			strSql.Append(")AS Row, T.*  from Survey T ");
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
			parameters[0].Value = "Survey";
			parameters[1].Value = "Vid";
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

