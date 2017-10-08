using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类GroupWork。
	/// </summary>
	public class GroupWork
	{
		public GroupWork()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Gid", "GroupWork"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Gid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from GroupWork");
			strSql.Append(" where Gid=@Gid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4)};
			parameters[0].Value = Gid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        public bool Exists(string Gnum, int Gmid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  count(1) from GroupWork ");
            strSql.Append(" where Gmid=@Gmid and Gnum=@Gnum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Gnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Gmid", SqlDbType.Int,4)};
            parameters[0].Value = Gnum;
            parameters[1].Value = Gmid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.GroupWork model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into GroupWork(");
            strSql.Append("Gnum,Gstudents,Gterm,Ggrade,Gclass,Gcid,Gmid,Gfilename,Gtype,Gurl,Glengh,Gscore,Gtime,Gvote,Gcheck,Gnote,Grank,Ghit,Gip,Gdate,Ggroup)");
			strSql.Append(" values (");
            strSql.Append("@Gnum,@Gstudents,@Gterm,@Ggrade,@Gclass,@Gcid,@Gmid,@Gfilename,@Gtype,@Gurl,@Glengh,@Gscore,@Gtime,@Gvote,@Gcheck,@Gnote,@Grank,@Ghit,@Gip,@Gdate,@Ggroup)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Gnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Gstudents", SqlDbType.NVarChar,200),
					new SqlParameter("@Gterm", SqlDbType.Int,4),
					new SqlParameter("@Ggrade", SqlDbType.Int,4),
					new SqlParameter("@Gclass", SqlDbType.Int,4),
					new SqlParameter("@Gcid", SqlDbType.Int,4),
					new SqlParameter("@Gmid", SqlDbType.Int,4),
					new SqlParameter("@Gfilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Gtype", SqlDbType.NVarChar,50),
					new SqlParameter("@Gurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Glengh", SqlDbType.Int,4),
					new SqlParameter("@Gscore", SqlDbType.Int,4),
					new SqlParameter("@Gtime", SqlDbType.Int,4),
					new SqlParameter("@Gvote", SqlDbType.Int,4),
					new SqlParameter("@Gcheck", SqlDbType.Bit,1),
					new SqlParameter("@Gnote", SqlDbType.NText),
					new SqlParameter("@Grank", SqlDbType.Int,4),
					new SqlParameter("@Ghit", SqlDbType.Int,4),
					new SqlParameter("@Gip", SqlDbType.NVarChar,50),
					new SqlParameter("@Gdate", SqlDbType.DateTime),
					new SqlParameter("@Ggroup", SqlDbType.Int,4)};
			parameters[0].Value = model.Gnum;
			parameters[1].Value = model.Gstudents;
			parameters[2].Value = model.Gterm;
			parameters[3].Value = model.Ggrade;
			parameters[4].Value = model.Gclass;
			parameters[5].Value = model.Gcid;
			parameters[6].Value = model.Gmid;
			parameters[7].Value = model.Gfilename;
			parameters[8].Value = model.Gtype;
			parameters[9].Value = model.Gurl;
			parameters[10].Value = model.Glengh;
			parameters[11].Value = model.Gscore;
			parameters[12].Value = model.Gtime;
			parameters[13].Value = model.Gvote;
			parameters[14].Value = model.Gcheck;
			parameters[15].Value = model.Gnote;
			parameters[16].Value = model.Grank;
			parameters[17].Value = model.Ghit;
			parameters[18].Value = model.Gip;
			parameters[19].Value = model.Gdate;
            parameters[20].Value = model.Ggroup;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public void Update(LearnSite.Model.GroupWork model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update GroupWork set ");
			strSql.Append("Gnum=@Gnum,");
			strSql.Append("Gstudents=@Gstudents,");
			strSql.Append("Gterm=@Gterm,");
			strSql.Append("Ggrade=@Ggrade,");
			strSql.Append("Gclass=@Gclass,");
			strSql.Append("Gcid=@Gcid,");
			strSql.Append("Gmid=@Gmid,");
			strSql.Append("Gfilename=@Gfilename,");
			strSql.Append("Gtype=@Gtype,");
			strSql.Append("Gurl=@Gurl,");
			strSql.Append("Glengh=@Glengh,");
			strSql.Append("Gscore=@Gscore,");
			strSql.Append("Gtime=@Gtime,");
			strSql.Append("Gvote=@Gvote,");
			strSql.Append("Gcheck=@Gcheck,");
			strSql.Append("Gnote=@Gnote,");
			strSql.Append("Grank=@Grank,");
			strSql.Append("Ghit=@Ghit,");
			strSql.Append("Gip=@Gip,");
			strSql.Append("Gdate=@Gdate");
			strSql.Append(" where Gid=@Gid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4),
					new SqlParameter("@Gnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Gstudents", SqlDbType.NVarChar,200),
					new SqlParameter("@Gterm", SqlDbType.Int,4),
					new SqlParameter("@Ggrade", SqlDbType.Int,4),
					new SqlParameter("@Gclass", SqlDbType.Int,4),
					new SqlParameter("@Gcid", SqlDbType.Int,4),
					new SqlParameter("@Gmid", SqlDbType.Int,4),
					new SqlParameter("@Gfilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Gtype", SqlDbType.NVarChar,50),
					new SqlParameter("@Gurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Glengh", SqlDbType.Int,4),
					new SqlParameter("@Gscore", SqlDbType.Int,4),
					new SqlParameter("@Gtime", SqlDbType.Int,4),
					new SqlParameter("@Gvote", SqlDbType.Int,4),
					new SqlParameter("@Gcheck", SqlDbType.Bit,1),
					new SqlParameter("@Gnote", SqlDbType.NText),
					new SqlParameter("@Grank", SqlDbType.Int,4),
					new SqlParameter("@Ghit", SqlDbType.Int,4),
					new SqlParameter("@Gip", SqlDbType.NVarChar,50),
					new SqlParameter("@Gdate", SqlDbType.DateTime)};
			parameters[0].Value = model.Gid;
			parameters[1].Value = model.Gnum;
			parameters[2].Value = model.Gstudents;
			parameters[3].Value = model.Gterm;
			parameters[4].Value = model.Ggrade;
			parameters[5].Value = model.Gclass;
			parameters[6].Value = model.Gcid;
			parameters[7].Value = model.Gmid;
			parameters[8].Value = model.Gfilename;
			parameters[9].Value = model.Gtype;
			parameters[10].Value = model.Gurl;
			parameters[11].Value = model.Glengh;
			parameters[12].Value = model.Gscore;
			parameters[13].Value = model.Gtime;
			parameters[14].Value = model.Gvote;
			parameters[15].Value = model.Gcheck;
			parameters[16].Value = model.Gnote;
			parameters[17].Value = model.Grank;
			parameters[18].Value = model.Ghit;
			parameters[19].Value = model.Gip;
			parameters[20].Value = model.Gdate;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Gid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GroupWork ");
			strSql.Append(" where Gid=@Gid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4)};
			parameters[0].Value = Gid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.GroupWork GetModel(int Gid)
		{			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Gid,Gnum,Gstudents,Gterm,Ggrade,Gclass,Gcid,Gmid,Gfilename,Gtype,Gurl,Glengh,Gscore,Gtime,Gvote,Gcheck,Gnote,Grank,Ghit,Gip,Gdate from GroupWork ");
			strSql.Append(" where Gid=@Gid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4)};
			parameters[0].Value = Gid;

			LearnSite.Model.GroupWork model=new LearnSite.Model.GroupWork();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Gid"].ToString()!="")
				{
					model.Gid=int.Parse(ds.Tables[0].Rows[0]["Gid"].ToString());
				}
				model.Gnum=ds.Tables[0].Rows[0]["Gnum"].ToString();
				model.Gstudents=ds.Tables[0].Rows[0]["Gstudents"].ToString();
				if(ds.Tables[0].Rows[0]["Gterm"].ToString()!="")
				{
					model.Gterm=int.Parse(ds.Tables[0].Rows[0]["Gterm"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ggrade"].ToString()!="")
				{
					model.Ggrade=int.Parse(ds.Tables[0].Rows[0]["Ggrade"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Gclass"].ToString()!="")
				{
					model.Gclass=int.Parse(ds.Tables[0].Rows[0]["Gclass"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Gcid"].ToString()!="")
				{
					model.Gcid=int.Parse(ds.Tables[0].Rows[0]["Gcid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Gmid"].ToString()!="")
				{
					model.Gmid=int.Parse(ds.Tables[0].Rows[0]["Gmid"].ToString());
				}
				model.Gfilename=ds.Tables[0].Rows[0]["Gfilename"].ToString();
				model.Gtype=ds.Tables[0].Rows[0]["Gtype"].ToString();
				model.Gurl=ds.Tables[0].Rows[0]["Gurl"].ToString();
				if(ds.Tables[0].Rows[0]["Glengh"].ToString()!="")
				{
					model.Glengh=int.Parse(ds.Tables[0].Rows[0]["Glengh"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Gscore"].ToString()!="")
				{
					model.Gscore=int.Parse(ds.Tables[0].Rows[0]["Gscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Gtime"].ToString()!="")
				{
					model.Gtime=int.Parse(ds.Tables[0].Rows[0]["Gtime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Gvote"].ToString()!="")
				{
					model.Gvote=int.Parse(ds.Tables[0].Rows[0]["Gvote"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Gcheck"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Gcheck"].ToString()=="1")||(ds.Tables[0].Rows[0]["Gcheck"].ToString().ToLower()=="true"))
					{
						model.Gcheck=true;
					}
					else
					{
						model.Gcheck=false;
					}
				}
				model.Gnote=ds.Tables[0].Rows[0]["Gnote"].ToString();
				if(ds.Tables[0].Rows[0]["Grank"].ToString()!="")
				{
					model.Grank=int.Parse(ds.Tables[0].Rows[0]["Grank"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ghit"].ToString()!="")
				{
					model.Ghit=int.Parse(ds.Tables[0].Rows[0]["Ghit"].ToString());
				}
				model.Gip=ds.Tables[0].Rows[0]["Gip"].ToString();
				if(ds.Tables[0].Rows[0]["Gdate"].ToString()!="")
				{
					model.Gdate=DateTime.Parse(ds.Tables[0].Rows[0]["Gdate"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 根据组长学号和活动编号得到一个对象实体
        /// </summary>
        public LearnSite.Model.GroupWork GetModelBySnum(string Gnum,int Gmid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Gid,Gnum,Gstudents,Gterm,Ggrade,Gclass,Gcid,Gmid,Gfilename,Gtype,Gurl,Glengh,Gscore,Gtime,Gvote,Gcheck,Gnote,Grank,Ghit,Gip,Gdate from GroupWork ");
            strSql.Append(" where Gmid=@Gmid and Gnum=@Gnum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Gnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Gmid", SqlDbType.Int,4)};
            parameters[0].Value = Gnum;
            parameters[1].Value = Gmid;

            LearnSite.Model.GroupWork model = new LearnSite.Model.GroupWork();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Gid"].ToString() != "")
                {
                    model.Gid = int.Parse(ds.Tables[0].Rows[0]["Gid"].ToString());
                }
                model.Gnum = ds.Tables[0].Rows[0]["Gnum"].ToString();
                model.Gstudents = ds.Tables[0].Rows[0]["Gstudents"].ToString();
                if (ds.Tables[0].Rows[0]["Gterm"].ToString() != "")
                {
                    model.Gterm = int.Parse(ds.Tables[0].Rows[0]["Gterm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ggrade"].ToString() != "")
                {
                    model.Ggrade = int.Parse(ds.Tables[0].Rows[0]["Ggrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Gclass"].ToString() != "")
                {
                    model.Gclass = int.Parse(ds.Tables[0].Rows[0]["Gclass"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Gcid"].ToString() != "")
                {
                    model.Gcid = int.Parse(ds.Tables[0].Rows[0]["Gcid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Gmid"].ToString() != "")
                {
                    model.Gmid = int.Parse(ds.Tables[0].Rows[0]["Gmid"].ToString());
                }
                model.Gfilename = ds.Tables[0].Rows[0]["Gfilename"].ToString();
                model.Gtype = ds.Tables[0].Rows[0]["Gtype"].ToString();
                model.Gurl = ds.Tables[0].Rows[0]["Gurl"].ToString();
                if (ds.Tables[0].Rows[0]["Glengh"].ToString() != "")
                {
                    model.Glengh = int.Parse(ds.Tables[0].Rows[0]["Glengh"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Gscore"].ToString() != "")
                {
                    model.Gscore = int.Parse(ds.Tables[0].Rows[0]["Gscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Gtime"].ToString() != "")
                {
                    model.Gtime = int.Parse(ds.Tables[0].Rows[0]["Gtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Gvote"].ToString() != "")
                {
                    model.Gvote = int.Parse(ds.Tables[0].Rows[0]["Gvote"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Gcheck"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Gcheck"].ToString() == "1") || (ds.Tables[0].Rows[0]["Gcheck"].ToString().ToLower() == "true"))
                    {
                        model.Gcheck = true;
                    }
                    else
                    {
                        model.Gcheck = false;
                    }
                }
                model.Gnote = ds.Tables[0].Rows[0]["Gnote"].ToString();
                if (ds.Tables[0].Rows[0]["Grank"].ToString() != "")
                {
                    model.Grank = int.Parse(ds.Tables[0].Rows[0]["Grank"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ghit"].ToString() != "")
                {
                    model.Ghit = int.Parse(ds.Tables[0].Rows[0]["Ghit"].ToString());
                }
                model.Gip = ds.Tables[0].Rows[0]["Gip"].ToString();
                if (ds.Tables[0].Rows[0]["Gdate"].ToString() != "")
                {
                    model.Gdate = DateTime.Parse(ds.Tables[0].Rows[0]["Gdate"].ToString());
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
			strSql.Append("select Gid,Gnum,Gstudents,Gterm,Ggrade,Gclass,Gcid,Gmid,Gfilename,Gtype,Gurl,Glengh,Gscore,Gtime,Gvote,Gcheck,Gnote,Grank,Ghit,Gip,Gdate ");
			strSql.Append(" FROM GroupWork ");
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
			strSql.Append(" Gid,Gnum,Gstudents,Gterm,Ggrade,Gclass,Gcid,Gmid,Gfilename,Gtype,Gurl,Glengh,Gscore,Gtime,Gvote,Gcheck,Gnote,Grank,Ghit,Gip,Gdate ");
			strSql.Append(" FROM GroupWork ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获取自己参与小组的合作作品
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public DataSet GetMyWorks(string Snum)
        {
            string mysql = "select Gid,Gstudents,Gterm,Ggrade,Gclass,Gmid,Gfilename,Gurl,Gscore,Gcheck,Grank,Ghit,Gdate,Mtitle from GroupWork,Mission where Gmid=Mid and Gstudents like'%"+Snum+"%'  order by Ggrade asc ,Gterm asc";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 获取该学号组长提交作品的是否存在
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Gmid"></param>
        /// <returns></returns>
        public bool DoneGroupWork(string Snum, int Gmid)
        {
            string mysql = "select count(1) from GroupWork where  Gmid=" + Gmid + " and Gnum='" + Snum + "'";
            return DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 获取该组号提交作品的是否存在
        /// </summary>
        /// <param name="Ggroup"></param>
        /// <param name="Gmid"></param>
        /// <returns></returns>
        public bool DoneGroupWork(int Ggroup, int Gmid)
        {
            string mysql = "select count(1) from GroupWork where  Gmid=" + Gmid + " and Ggroup=" + Ggroup;
            return DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 获取该学号组长提交作品的是否存在
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Gmid"></param>
        /// <returns></returns>
        public string DoneGroupWorkUrl(string Snum, int Gmid)
        {
            string mysql = "select Gurl from GroupWork where  Gmid=" + Gmid + " and Gnum='" + Snum + "'";
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 判断作品是否评价
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Gmid"></param>
        /// <returns></returns>
        public bool CheckGroupWork(string Snum, int Gmid)
        {
            string mysql = "select count(1) from GroupWork where Gcheck=1 and  Gmid=" + Gmid + " and Gnum='" + Snum + "'";
            return DbHelperSQL.Exists(mysql);
        }

        /// <summary>
        /// 给小组作品评分
        /// </summary>
        /// <param name="Gid"></param>
        /// <param name="Gscore"></param>
        public void UpdateGscore(int Gid,int Gscore,int Cobj,int Cterm)
        {
            string mysql = "update GroupWork set Gcheck=1,Gscore="+Gscore+" where Gid="+Gid;
            DbHelperSQL.ExecuteSql(mysql);
            string sqlstr = "select Gstudents from GroupWork where Gid="+Gid;
            string findstr = DbHelperSQL.FindString(sqlstr);
            if (findstr != "")
            {
                BLL.Students sbll = new BLL.Students();
                sbll.TotalSgscore(findstr, Cobj, Cterm);//同时给小组成员统计
            }
        }
        /// <summary>
        /// 给小组作品取消评价
        /// </summary>
        /// <param name="Gid"></param>
        /// <param name="Gscore"></param>
        public void CancelGscore(int Gid, bool Gcheck)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GroupWork set ");
            strSql.Append("Gscore=0,");
            strSql.Append("Gcheck=@Gcheck");
            strSql.Append(" where Gid=@Gid");
            SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4),                   
					new SqlParameter("@Gcheck", SqlDbType.Bit,1)};
            parameters[0].Value = Gid;
            parameters[1].Value = Gcheck;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取某班级某活动小组合作作品列表
        /// </summary>
        /// <param name="Ggrade"></param>
        /// <param name="Gclass"></param>
        /// <param name="Gmid"></param>
        /// <returns></returns>
        public DataSet GetMissionGroup(int Ggrade, int Gclass, int Gmid)
        {
            string mysql = "select Gid,Gnum,Gstudents,Gterm,Ggrade,Gclass,Gcid,Gmid,Gfilename,Gtype,Gurl,Glengh,Gscore,Gtime,Gvote,Gcheck,Gnote,Grank,Ghit,Gip,Gdate,Sgtitle from GroupWork,Students where Ggrade=" + Ggrade + " and Gclass=" + Gclass + "  and Gmid=" + Gmid + " and Gnum=Snum and Ggrade=Sgrade and Sclass=Gclass ";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 获取本课程小组作品总分
        /// </summary>
        /// <param name="Gnum"></param>
        /// <param name="Gcid"></param>
        /// <returns></returns>
        public int  GetGscore( string Gnum,int Gcid)
        {
            string mysql = "select sum(Gscore) from GroupWork where Gcid=" + Gcid + " and Gnum='" + Gnum + "'";
            return DbHelperSQL.FindNum(mysql);
        }
        /// <summary>
        /// 获取本课小组作品总分
        /// </summary>
        /// <param name="Ggroup">组号</param>
        /// <param name="Gcid">学案编号</param>
        /// <returns></returns>
        public int GetGscore(int Ggroup, int Gcid)
        {
            string mysql = "select sum(Gscore) from GroupWork where Gcid=" + Gcid + " and Ggroup=" + Ggroup;
            return DbHelperSQL.FindNum(mysql);
        }
        /// <summary>
        /// 获取本班本学期本小组的合作作品总分
        /// </summary>
        /// <param name="Ggroup">组号</param>
        /// <param name="Ggrade"></param>
        /// <param name="Gclass"></param>
        /// <param name="Gterm"></param>
        /// <returns></returns>
        public int GetGscoreAll(int Ggroup, int Ggrade, int Gclass, int Gterm)
        {
            string mysql = "select sum(Gscore) from GroupWork where Ggroup=" + Ggroup + " and Ggrade=" + Ggrade + " and Gclass=" + Gclass + " and Gterm=" + Gterm;
            return DbHelperSQL.FindNum(mysql);
        }
        /// <summary>
        /// 初始化新增字段Ggroup 组号（小组作品表）
        /// </summary>
        /// <returns></returns>
        public int initGgroup()
        {
            string mysql = "update GroupWork set Ggroup=Sid from GroupWork,Students where Gnum=Snum";
            return DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 初始化新增字段Gyear 组长入学年度（小组作品表）
        /// </summary>
        /// <returns></returns>
        public int initGyear()
        {
            string mysql = "update GroupWork set Gyear=Syear  from GroupWork,Students where Gnum=Snum";
            return DbHelperSQL.ExecuteSql(mysql);
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
			parameters[0].Value = "GroupWork";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法
	}
}

