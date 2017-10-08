using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
using System.Collections;
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:SurveyFeedback
	/// </summary>
	public partial class SurveyFeedback
	{
		public SurveyFeedback()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Fid", "SurveyFeedback"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Fid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SurveyFeedback");
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
        public int ExistsScore(int Fvid,string Fnum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 Fscore from SurveyFeedback");
            strSql.Append(" where Fvid=@Fvid and Fnum=@Fnum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Fvid", SqlDbType.Int,4),
                    new SqlParameter("@Fnum", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = Fvid;
            parameters[1].Value = Fnum;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                int rs = -1024;
                return rs;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.SurveyFeedback model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SurveyFeedback(");
			strSql.Append("Fnum,Fyear,Fgrade,Fclass,Fterm,Fcid,Fvid,Fvtype,Fselect,Fscore,Fdate,Fsid)");
			strSql.Append(" values (");
			strSql.Append("@Fnum,@Fyear,@Fgrade,@Fclass,@Fterm,@Fcid,@Fvid,@Fvtype,@Fselect,@Fscore,@Fdate,@Fsid)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Fnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Fyear", SqlDbType.Int,4),
					new SqlParameter("@Fgrade", SqlDbType.Int,4),
					new SqlParameter("@Fclass", SqlDbType.Int,4),
					new SqlParameter("@Fterm", SqlDbType.Int,4),
					new SqlParameter("@Fcid", SqlDbType.Int,4),
					new SqlParameter("@Fvid", SqlDbType.Int,4),
					new SqlParameter("@Fvtype", SqlDbType.Int,4),
					new SqlParameter("@Fselect", SqlDbType.NText),
					new SqlParameter("@Fscore", SqlDbType.Int,4),
					new SqlParameter("@Fdate", SqlDbType.DateTime),
					new SqlParameter("@Fsid", SqlDbType.Int,4)};
			parameters[0].Value = model.Fnum;
			parameters[1].Value = model.Fyear;
			parameters[2].Value = model.Fgrade;
			parameters[3].Value = model.Fclass;
			parameters[4].Value = model.Fterm;
			parameters[5].Value = model.Fcid;
			parameters[6].Value = model.Fvid;
			parameters[7].Value = model.Fvtype;
			parameters[8].Value = model.Fselect;
			parameters[9].Value = model.Fscore;
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
        /// 批量更新调查结果表中的调查类型
        /// </summary>
        /// <param name="Fvid"></param>
        /// <param name="Fvtype"></param>
        /// <returns></returns>
        public bool UpdateFvtype(int Fvid,int Fvtype)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SurveyFeedback set ");
            strSql.Append("Fvtype=@Fvtype ");
            strSql.Append(" where Fvid=@Fvid");
            SqlParameter[] parameters = {					
					new SqlParameter("@Fvid", SqlDbType.Int,4),
					new SqlParameter("@Fvtype", SqlDbType.Int,4)};
            parameters[0].Value =Fvid;
            parameters[1].Value = Fvtype;
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
		public bool Update(LearnSite.Model.SurveyFeedback model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SurveyFeedback set ");
			strSql.Append("Fnum=@Fnum,");
			strSql.Append("Fyear=@Fyear,");
			strSql.Append("Fgrade=@Fgrade,");
			strSql.Append("Fclass=@Fclass,");
			strSql.Append("Fterm=@Fterm,");
			strSql.Append("Fcid=@Fcid,");
			strSql.Append("Fvid=@Fvid,");
			strSql.Append("Fvtype=@Fvtype,");
			strSql.Append("Fselect=@Fselect,");
			strSql.Append("Fscore=@Fscore,");
			strSql.Append("Fdate=@Fdate");
			strSql.Append(" where Fid=@Fid");
			SqlParameter[] parameters = {
					new SqlParameter("@Fnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Fyear", SqlDbType.Int,4),
					new SqlParameter("@Fgrade", SqlDbType.Int,4),
					new SqlParameter("@Fclass", SqlDbType.Int,4),
					new SqlParameter("@Fterm", SqlDbType.Int,4),
					new SqlParameter("@Fcid", SqlDbType.Int,4),
					new SqlParameter("@Fvid", SqlDbType.Int,4),
					new SqlParameter("@Fvtype", SqlDbType.Int,4),
					new SqlParameter("@Fselect", SqlDbType.NText),
					new SqlParameter("@Fscore", SqlDbType.Int,4),
					new SqlParameter("@Fdate", SqlDbType.DateTime),
					new SqlParameter("@Fid", SqlDbType.Int,4)};
			parameters[0].Value = model.Fnum;
			parameters[1].Value = model.Fyear;
			parameters[2].Value = model.Fgrade;
			parameters[3].Value = model.Fclass;
			parameters[4].Value = model.Fterm;
			parameters[5].Value = model.Fcid;
			parameters[6].Value = model.Fvid;
			parameters[7].Value = model.Fvtype;
			parameters[8].Value = model.Fselect;
			parameters[9].Value = model.Fscore;
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
			strSql.Append("delete from SurveyFeedback ");
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
        /// 删除一个班级的调查记录
        /// </summary>
        public int DelClass(int Fgrade, int Fclass, int Fyear)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SurveyFeedback ");
            strSql.Append(" where Fgrade=@Fgrade and Fclass=@Fclass and Fyear=@Fyear ");
            SqlParameter[] parameters = {
					new SqlParameter("@Fgrade", SqlDbType.Int,4),                                        
					new SqlParameter("@Fclass", SqlDbType.Int,4),
					new SqlParameter("@Fyear", SqlDbType.Int,4)};
            parameters[0].Value = Fgrade;
            parameters[1].Value = Fclass;
            parameters[2].Value = Fyear;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Fidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SurveyFeedback ");
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
        /// 已参加调查的本班同学人数
        /// </summary>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <param name="Fcid"></param>
        /// <param name="Fvid"></param>
        /// <returns></returns>
        public int GetSurveyStu(int Fgrade, int Fclass, int Fcid, int Fvid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from SurveyFeedback  ");
            strSql.Append(" where  Fvid=@Fvid and Fcid=@Fcid and Fnum in ( ");
            strSql.Append(" select Snum from Students where Sgrade=@Fgrade and Sclass=@Fclass )");

            SqlParameter[] parameters = {
					new SqlParameter("@Fgrade", SqlDbType.Int,4),
                    new SqlParameter("@Fclass", SqlDbType.Int,4),
                    new SqlParameter("@Fcid", SqlDbType.Int,4),
                    new SqlParameter("@Fvid", SqlDbType.Int,4)};

            parameters[0].Value = Fgrade;
            parameters[1].Value = Fclass;
            parameters[2].Value = Fcid;
            parameters[3].Value = Fvid;

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
        /// 未参加调查的本班同学列表
        /// </summary>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <param name="Fcid"></param>
        /// <param name="Fvid"></param>
        /// <returns></returns>
        public string GetNoSurveyStu(int Fgrade, int Fclass,int Fcid,int Fvid)
        {
            string restr = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sname from Students ");
            strSql.Append(" where Sgrade=@Fgrade and Sclass=@Fclass and Snum not in( ");
            strSql.Append(" select Fnum from Students,SurveyFeedback where Sgrade=@Fgrade and Sclass=@Fclass and Snum=Fnum and Sgrade=Fgrade and Sclass=Fclass and Fcid=@Fcid and Fvid=@Fvid )");
            strSql.Append(" order by Snum");
            SqlParameter[] parameters = {
					new SqlParameter("@Fgrade", SqlDbType.Int,4),
                    new SqlParameter("@Fclass", SqlDbType.Int,4),
                    new SqlParameter("@Fcid", SqlDbType.Int,4),
                    new SqlParameter("@Fvid", SqlDbType.Int,4)};

            parameters[0].Value = Fgrade;
            parameters[1].Value = Fclass;
            parameters[2].Value = Fcid;
            parameters[3].Value = Fvid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            DataTable dt = ds.Tables[0];
            int cn = dt.Rows.Count;
            if (cn > 0)
            {
                for (int i = 0; i < cn; i++)
                {
                    restr = restr + dt.Rows[i]["Sname"].ToString() + "、";
                    if ((i + 1) % 8 == 0)
                    {
                        restr = restr + "<br/>";
                    }
                }
            }
            return restr;
        }        
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.SurveyFeedback GetModel(int Fid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Fid,Fnum,Fyear,Fgrade,Fclass,Fterm,Fcid,Fvid,Fvtype,Fselect,Fscore,Fdate from SurveyFeedback ");
			strSql.Append(" where Fid=@Fid");
			SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4)
			};
			parameters[0].Value = Fid;

			LearnSite.Model.SurveyFeedback model=new LearnSite.Model.SurveyFeedback();
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
				if(ds.Tables[0].Rows[0]["Fyear"]!=null && ds.Tables[0].Rows[0]["Fyear"].ToString()!="")
				{
					model.Fyear=int.Parse(ds.Tables[0].Rows[0]["Fyear"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fgrade"]!=null && ds.Tables[0].Rows[0]["Fgrade"].ToString()!="")
				{
					model.Fgrade=int.Parse(ds.Tables[0].Rows[0]["Fgrade"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fclass"]!=null && ds.Tables[0].Rows[0]["Fclass"].ToString()!="")
				{
					model.Fclass=int.Parse(ds.Tables[0].Rows[0]["Fclass"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fterm"]!=null && ds.Tables[0].Rows[0]["Fterm"].ToString()!="")
				{
					model.Fterm=int.Parse(ds.Tables[0].Rows[0]["Fterm"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fcid"]!=null && ds.Tables[0].Rows[0]["Fcid"].ToString()!="")
				{
					model.Fcid=int.Parse(ds.Tables[0].Rows[0]["Fcid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fvid"]!=null && ds.Tables[0].Rows[0]["Fvid"].ToString()!="")
				{
                    model.Fvid = int.Parse(ds.Tables[0].Rows[0]["Fvid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fvtype"]!=null && ds.Tables[0].Rows[0]["Fvtype"].ToString()!="")
				{
					model.Fvtype=int.Parse(ds.Tables[0].Rows[0]["Fvtype"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Fselect"]!=null && ds.Tables[0].Rows[0]["Fselect"].ToString()!="")
				{
					model.Fselect=ds.Tables[0].Rows[0]["Fselect"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Fscore"]!=null && ds.Tables[0].Rows[0]["Fscore"].ToString()!="")
				{
					model.Fscore=int.Parse(ds.Tables[0].Rows[0]["Fscore"].ToString());
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Fid,Fnum,Fyear,Fgrade,Fclass,Fterm,Fcid,Fvid,Fvtype,Fselect,Fscore,Fdate ");
			strSql.Append(" FROM SurveyFeedback ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 将本班的学生调查选项合并到字符串中  
        /// </summary>
        /// <param name="Fyear"></param>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <param name="Fterm"></param>
        /// <param name="Fvid"></param>
        /// <returns></returns>
        public string GetClassFselect(int Fgrade, int Fclass, int Fvid)
        {
            string allselect = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Fselect ");
            strSql.Append(" FROM SurveyFeedback ");
            strSql.Append(" where Fvid=@Fvid ");
            strSql.Append(" and Fnum in (select Snum from Students where Fgrade=@Fgrade and Fclass=@Fclass) ");

            SqlParameter[] parameters = {
					new SqlParameter("@Fgrade", SqlDbType.Int,4),
					new SqlParameter("@Fclass", SqlDbType.Int,4),
					new SqlParameter("@Fvid", SqlDbType.Int,4)};
            parameters[0].Value = Fgrade;
            parameters[1].Value = Fclass;
            parameters[2].Value = Fvid;
            DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    allselect = allselect + dt.Rows[i]["Fselect"].ToString() + ",";  //将本班的学生调查选项合并到字符串中              
                }
            }

            return allselect;
        }
        /// <summary>
        /// 获取班级所有选中项平均分值
        /// </summary>
        /// <param name="Fyear"></param>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <param name="Fterm"></param>
        /// <param name="Fvid"></param>
        /// <returns></returns>
        public int GetClassYscore(int Fyear, int Fgrade, int Fclass, int Fterm, int Fvid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select avg(Fscore) ");
            strSql.Append(" FROM SurveyFeedback ");
            strSql.Append(" where Fyear=@Fyear and Fgrade=@Fgrade and Fclass=@Fclass and Fterm=@Fterm and Fvid=@Fvid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Fyear", SqlDbType.Int,4),
					new SqlParameter("@Fgrade", SqlDbType.Int,4),
					new SqlParameter("@Fclass", SqlDbType.Int,4),
					new SqlParameter("@Fterm", SqlDbType.Int,4),
					new SqlParameter("@Fvid", SqlDbType.Int,4)};
            parameters[0].Value = Fyear;
            parameters[1].Value = Fgrade;
            parameters[2].Value = Fclass;
            parameters[3].Value = Fterm;
            parameters[4].Value = Fvid;

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
        /// 获取本班测验结果（包含调查结果）
        /// </summary>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <param name="Fvid"></param>
        /// <returns></returns>
        public DataSet GetClassFscore(int Fgrade, int Fclass, int Fvid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Fscore,Snum,Sname,Sgrade,Sclass ");
            strSql.Append(" FROM SurveyFeedback,Students ");
            strSql.Append(" where Fvid=@Fvid and Fnum=Snum and Fnum in(");
            strSql.Append("select Snum from Students where Sgrade=@Fgrade and Sclass=@Fclass)");
            strSql.Append(" order by Fscore desc");
            SqlParameter[] parameters = {
					new SqlParameter("@Fgrade", SqlDbType.Int,4),
					new SqlParameter("@Fclass", SqlDbType.Int,4),
					new SqlParameter("@Fvid", SqlDbType.Int,4)};
            parameters[0].Value = Fgrade;
            parameters[1].Value = Fclass;
            parameters[2].Value = Fvid;

            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }
        /// <summary>
        /// 获取本班调查成绩
        /// </summary>
        /// <param name="Fvid"></param>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <returns></returns>
        public DataTable GetClassScore(int Fvid, int Fgrade, int Fclass)
        {
            string mysql = "select Fnum as Snum,Fscore as Score FROM SurveyFeedback  where Fvid=" + Fvid + " and Fgrade=" + Fgrade + " and Fclass=" + Fclass + " order by Fnum";
            return DbHelperSQL.Query(mysql).Tables[0];
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
			strSql.Append(" Fid,Fnum,Fyear,Fgrade,Fclass,Fterm,Fcid,Fvid,Fvtype,Fselect,Fscore,Fdate ");
			strSql.Append(" FROM SurveyFeedback ");
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
			strSql.Append("select count(1) FROM SurveyFeedback ");
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
			strSql.Append(")AS Row, T.*  from SurveyFeedback T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获取当前班级学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowFeedbackCids(int Fgrade, int Fclass, int Fterm, int Fyear)
        {
            string mysql = "SELECT DISTINCT Fcid FROM SurveyFeedback where Fterm=" + Fterm + " and Fgrade=" + Fgrade + " and Fclass=" + Fclass + " and Fyear=" + Fyear;
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string strtemp = "";
                for (int i = 0; i < n; i++)
                {
                    strtemp = strtemp + dt.Rows[i]["Fcid"].ToString() + ",";
                }
                return strtemp;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取某学生学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowStuFeedbackCids(string Snum,int Fterm,int Fgrade)
        {
            string mysql = "SELECT DISTINCT Fcid FROM SurveyFeedback where Fnum='" + Snum + "' and Fterm="+Fterm+" and Fgrade="+Fgrade;
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string strtemp = "";
                for (int i = 0; i < n; i++)
                {
                    strtemp = strtemp + dt.Rows[i]["Fcid"].ToString() + ",";
                }
                return strtemp;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取该调查同选项的同班同学Key为学号，Value为姓名
        /// </summary>
        /// <param name="Syear"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Vid"></param>
        /// <param name="Mid"></param>
        /// <returns></returns>
        public Hashtable ShowItemClassMate(int Syear, int Sgrade, int Sclass, int Vid, int Mid)
        {
            Hashtable hast = new Hashtable();
            string mysql = "SELECT Sid,Snum,Sname,Fselect FROM Students,SurveyFeedback where Syear=" + Syear + " and Sgrade=" + Sgrade + " and Sclass=" + Sclass + " and Fvid=" + Vid + " and Sid=Fsid order by Snum asc";
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string theMid = Mid.ToString()+",";
                for (int i = 0; i < n; i++)
                {
                  string tempstr =  dt.Rows[i]["Fselect"].ToString() + ",";
                  if (tempstr.IndexOf(theMid) > -1)
                  {
                      //说明该同学的选项集中有该选项
                      string snum = dt.Rows[i]["Snum"].ToString();
                      if (!hast.ContainsKey(snum))
                          hast.Add(snum, dt.Rows[i]["Sname"].ToString());
                  }
                }
            }
            dt.Dispose();
            return hast;
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
			parameters[0].Value = "SurveyFeedback";
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

