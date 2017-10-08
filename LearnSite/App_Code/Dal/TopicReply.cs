using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:TopicReply
	/// </summary>
	public class TopicReply
	{
		public TopicReply()
		{}
		#region  Method


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TopicReply");
			strSql.Append(" where ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.TopicReply model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TopicReply(");
            strSql.Append("Rtid,Rsnum,Rwords,Rtime,Rip,Rscore,Rban,Rgrade,Rterm,Rcid,Rclass,Rsid,Ryear,Redit,Ragree)");
            strSql.Append(" values (");
            strSql.Append("@Rtid,@Rsnum,@Rwords,@Rtime,@Rip,@Rscore,@Rban,@Rgrade,@Rterm,@Rcid,@Rclass,@Rsid,@Ryear,@Redit,@Ragree)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Rtid", SqlDbType.Int,4),
					new SqlParameter("@Rsnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Rwords", SqlDbType.NText),
					new SqlParameter("@Rtime", SqlDbType.DateTime),
					new SqlParameter("@Rip", SqlDbType.NVarChar,50),
					new SqlParameter("@Rscore", SqlDbType.Int,4),
					new SqlParameter("@Rban", SqlDbType.Bit,1),
					new SqlParameter("@Rgrade", SqlDbType.Int,4),
					new SqlParameter("@Rterm", SqlDbType.Int,4),
                    new SqlParameter("@Rcid", SqlDbType.Int,4),
					new SqlParameter("@Rclass", SqlDbType.Int,4),
					new SqlParameter("@Rsid", SqlDbType.Int,4),
					new SqlParameter("@Ryear", SqlDbType.Int,4),
					new SqlParameter("@Redit", SqlDbType.Int,4),
					new SqlParameter("@Ragree", SqlDbType.Int,4)};
            parameters[0].Value = model.Rtid;
            parameters[1].Value = model.Rsnum;
            parameters[2].Value = model.Rwords;
            parameters[3].Value = model.Rtime;
            parameters[4].Value = model.Rip;
            parameters[5].Value = model.Rscore;
            parameters[6].Value = model.Rban;
            parameters[7].Value = model.Rgrade;
            parameters[8].Value = model.Rterm;
            parameters[9].Value = model.Rcid;
            parameters[10].Value = model.Rclass;
            parameters[11].Value = model.Rsid;
            parameters[12].Value = model.Ryear;
            parameters[13].Value = model.Redit;
            parameters[14].Value = model.Ragree;

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
        /// 给回复评分
        /// </summary>
        /// <param name="Rid"></param>
        public bool Lessscore(int Rid)
        {
            string mysql = "update TopicReply set Rscore=Rscore-2 where Rid=" + Rid;
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
        /// 给回复评分
        /// </summary>
        /// <param name="Rid"></param>
        public bool Updatescore(int Rid)
        {
            string mysql = "update TopicReply set Rscore=Rscore+2 where Rid="+Rid;
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
        public bool InitReditagree()
        {
            string mysql = "update TopicReply set Redit=0 and Ragree=0 where Redit is null and Ragree is null";
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
        /// 给本班所有回复评分+2
        /// </summary>
        /// <param name="Rid"></param>
        public int UpdateAllscore(int Rtid, int Rgrade, int Rclass, int Ryear)
        {
            string mysql = "update TopicReply set Rscore=2 where Rscore=0 and Rtid=" + Rtid + " and Rgrade=" + Rgrade + " and Rclass=" + Rclass + " and Ryear=" + Ryear;
            return DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 给该贴子加禁言标记
        /// </summary>
        /// <param name="Rid"></param>
        public bool UpdateBan(int Rid)
        {
            string mysql = "update TopicReply set Rban=1 where Rid=" + Rid;
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
        /// 给该贴子允许修改
        /// </summary>
        /// <param name="Rid"></param>
        public bool UpdateEdit(int Rid)
        {
            string mysql = "update TopicReply set Redit=Redit^1 where Rid=" + Rid;
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
        /// 给该贴子点赞
        /// </summary>
        /// <param name="Rid"></param>
        /// <returns></returns>
        public bool UpdateAgree(int Rid)
        {
            string mysql = "update TopicReply set Ragree=Ragree+1 where Rid=" + Rid;
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
        /// 更新老师总结
        /// </summary>
        /// <param name="Rtid"></param>
        /// <param name="Rnum"></param>
        /// <param name="Rwords"></param>
        /// <returns></returns>
        public bool UpdateTeacher(int Rtid, int Rsid, string Rwords)
        {
            string mysql = "update TopicReply set Rwords='" + Rwords + "' where Rsid=" + Rsid + " and Rtid=" + Rtid;
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
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.TopicReply model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TopicReply set ");
            strSql.Append("Rtid=@Rtid,");
            strSql.Append("Rsnum=@Rsnum,");
            strSql.Append("Rwords=@Rwords,");
            strSql.Append("Rtime=@Rtime,");
            strSql.Append("Rip=@Rip,");
            strSql.Append("Rscore=@Rscore,");
            strSql.Append("Rban=@Rban,");
            strSql.Append("Rgrade=@Rgrade,");
            strSql.Append("Rterm=@Rterm");
            strSql.Append(" where Rid=@Rid");
            SqlParameter[] parameters = {
					new SqlParameter("@Rtid", SqlDbType.Int,4),
					new SqlParameter("@Rsnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Rwords", SqlDbType.NText),
					new SqlParameter("@Rtime", SqlDbType.DateTime),
					new SqlParameter("@Rip", SqlDbType.NVarChar,50),
					new SqlParameter("@Rscore", SqlDbType.Int,4),
					new SqlParameter("@Rban", SqlDbType.Bit,1),
					new SqlParameter("@Rgrade", SqlDbType.Int,4),
					new SqlParameter("@Rterm", SqlDbType.Int,4),
					new SqlParameter("@Rid", SqlDbType.Int,4)};
            parameters[0].Value = model.Rtid;
            parameters[1].Value = model.Rsnum;
            parameters[2].Value = model.Rwords;
            parameters[3].Value = model.Rtime;
            parameters[4].Value = model.Rip;
            parameters[5].Value = model.Rscore;
            parameters[6].Value = model.Rban;
            parameters[7].Value = model.Rgrade;
            parameters[8].Value = model.Rterm;
            parameters[9].Value = model.Rid;

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
        ///Rid 更新一条数据 Rwords Rtime Redit Ragree
        ///rtid, rsid
        /// </summary>
        public bool UpdateOne(LearnSite.Model.TopicReply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TopicReply set ");            
            strSql.Append("Rwords=@Rwords,");
            strSql.Append("Rtime=@Rtime,");
            strSql.Append("Redit=@Redit,");
            strSql.Append("Ragree=@Ragree");
            strSql.Append(" where Rtid=@Rtid and Rsid=@Rsid");
            SqlParameter[] parameters = {					
					new SqlParameter("@Rwords", SqlDbType.NText),
					new SqlParameter("@Rtime", SqlDbType.DateTime),
					new SqlParameter("@Redit", SqlDbType.Bit,1),
					new SqlParameter("@Ragree", SqlDbType.Int,4),
					new SqlParameter("@Rtid", SqlDbType.Int,4),
					new SqlParameter("@Rsid", SqlDbType.Int,4)};
            parameters[0].Value = model.Rwords;
            parameters[1].Value = model.Rtime;
            parameters[2].Value = model.Redit;
            parameters[3].Value = model.Ragree;
            parameters[4].Value = model.Rtid;
            parameters[5].Value = model.Rsid;

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
		public bool Delete(int Rid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TopicReply ");
			strSql.Append(" where Rid="+Rid+"" );
			int rowsAffected=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Ridlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TopicReply ");
			strSql.Append(" where Rid in ("+Ridlist + ")  ");
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
        /// 删除一个班级的讨论记录
        /// </summary>
        public int DelClass(int Rgrade, int Rclass, int Ryear)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TopicReply ");
            strSql.Append(" where Rgrade=@Rgrade and Rclass=@Rclass and Ryear=@Ryear ");
            SqlParameter[] parameters = {
					new SqlParameter("@Rgrade", SqlDbType.Int,4),                                        
					new SqlParameter("@Rclass", SqlDbType.Int,4),
					new SqlParameter("@Ryear", SqlDbType.Int,4)};
            parameters[0].Value = Rgrade;
            parameters[1].Value = Rclass;
            parameters[2].Value = Ryear;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.TopicReply GetModel(int Rid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
            strSql.Append(" Rid,Rtid,Rsnum,Rwords,Rtime,Rip,Rscore,Rban,Rgrade,Rterm,Rcid,Rclass ");
			strSql.Append(" from TopicReply ");
			strSql.Append(" where Rid="+Rid+"" );
			LearnSite.Model.TopicReply model=new LearnSite.Model.TopicReply();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Rid"].ToString()!="")
				{
					model.Rid=int.Parse(ds.Tables[0].Rows[0]["Rid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Rtid"].ToString()!="")
				{
					model.Rtid=int.Parse(ds.Tables[0].Rows[0]["Rtid"].ToString());
				}
				model.Rsnum=ds.Tables[0].Rows[0]["Rsnum"].ToString();
				model.Rwords=ds.Tables[0].Rows[0]["Rwords"].ToString();
				if(ds.Tables[0].Rows[0]["Rtime"].ToString()!="")
				{
					model.Rtime=DateTime.Parse(ds.Tables[0].Rows[0]["Rtime"].ToString());
				}
				model.Rip=ds.Tables[0].Rows[0]["Rip"].ToString();
				if(ds.Tables[0].Rows[0]["Rscore"].ToString()!="")
				{
					model.Rscore=int.Parse(ds.Tables[0].Rows[0]["Rscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Rban"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Rban"].ToString()=="1")||(ds.Tables[0].Rows[0]["Rban"].ToString().ToLower()=="true"))
					{
						model.Rban=true;
					}
					else
					{
						model.Rban=false;
					}
				}
                if (ds.Tables[0].Rows[0]["Rgrade"].ToString() != "")
                {
                    model.Rgrade = int.Parse(ds.Tables[0].Rows[0]["Rgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rterm"].ToString() != "")
                {
                    model.Rterm = int.Parse(ds.Tables[0].Rows[0]["Rterm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rcid"].ToString() != "")
                {
                    model.Rcid = int.Parse(ds.Tables[0].Rows[0]["Rcid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rclass"].ToString() != "")
                {
                    model.Rclass = int.Parse(ds.Tables[0].Rows[0]["Rclass"].ToString());
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
            strSql.Append("select Rid,Rtid,Rsnum,Rwords,Rtime,Rip,Rscore,Rban,Rgrade,Rterm,Rcid,Rclass ");
			strSql.Append(" FROM TopicReply ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获得该主题本班回复数据列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Rtid"></param>
        /// <returns></returns>
        public DataSet GetClassList(int Sgrade,int Sclass,int Rtid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  Rid,Rtid,Rsnum,Rwords,Rtime,Rip,Rscore,Rban,Rgrade,Rterm,Redit,Ragree,Sname ");
            strSql.Append(" FROM TopicReply,Students ");
            string strWhere = " Rsnum=Snum and Rban=0 and Rtid="+Rtid+" and Sgrade="+Sgrade+ " and Sclass="+Sclass +" order by Rtime asc";
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取本班未回复的同学姓名列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Rtid"></param>
        /// <returns></returns>
        public DataTable GetNoReplay(int Sgrade, int Sclass, int Rtid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  Sname ");
            strSql.Append(" FROM Students ");
            strSql.Append(" where  Sid not in (");
            strSql.Append(" select Rsid FROM TopicReply where Rtid=@Rtid  and Rgrade=@Sgrade  and Rclass=@Sclass ");
            strSql.Append(") and Sgrade=@Sgrade  and Sclass=@Sclass ");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),                                        
					new SqlParameter("@Sclass", SqlDbType.Int,4),
					new SqlParameter("@Rtid", SqlDbType.Int,4)};
            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;
            parameters[2].Value = Rtid;

            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
        }
        /// <summary>
        /// 获得该主题本班回复数据列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Rtid"></param>
        /// <returns></returns>
        public DataTable GetClassListScore(int Sgrade, int Sclass, int Rtid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  Rsnum as Snum,Rscore as Score ");
            strSql.Append(" FROM TopicReply ");
            string strWhere = " Rtid=" + Rtid + " and Rgrade=" + Sgrade + " and Rclass=" + Sclass + " order by Rsnum ";
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
        /// <summary>
        /// 获取教师模拟学生回复作为总结
        /// </summary>
        /// <param name="Rtid"></param>
        /// <param name="Rsnum"></param>
        /// <returns></returns>
        public string getteareply(int Rtid, string Rsnum)
        {
            string mysql = "select Rwords from TopicReply where Rtid="+Rtid+" and Rsnum='"+Rsnum+"'";
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 判断该学号本题回复是否被禁言过
        /// </summary>
        /// <param name="Rtid"></param>
        /// <param name="Rsnum"></param>
        /// <returns></returns>
        public bool Isban(int Rtid, int Rsid)
        {
            string mysql = "select  count(1) from TopicReply where Rban=1 and  Rtid=" + Rtid + " and Rsid=" + Rsid;
            return DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 判断该学号本题回复是否允许回复修改
        /// </summary>
        /// <param name="Rtid"></param>
        /// <param name="Rsid"></param>
        /// <returns></returns>
        public bool Isedit(int Rtid, int Rsid)
        {
            string mysql = "select  count(1) from TopicReply where Redit=1 and  Rtid=" + Rtid + " and Rsid=" + Rsid;
            return DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 判断该学号本题回复次数
        /// </summary>
        /// <param name="Rtid"></param>
        /// <param name="Rsnum"></param>
        /// <returns></returns>
        public int ReplyCount(int Rtid, int Rsid)
        {
            string mysql = "select count(*) from TopicReply where   Rtid=" + Rtid + " and Rsid=" + Rsid ;
            string strCount = DbHelperSQL.FindString(mysql);
            if (strCount != "")
                return Int32.Parse(strCount);
            else
                return 0;
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
            strSql.Append(" Rid,Rtid,Rsnum,Rwords,Rtime,Rip,Rscore,Rban,Rgrade,Rterm,Rcid,Rclass");
			strSql.Append(" FROM TopicReply ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 初始化新增Rcid字段
        /// </summary>
        public void InitRcid()
        {
            string mysql = "update TopicReply set Rcid=Tcid from TopicReply,TopicDiscuss where Rtid=Tid ";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 初始化新增Rclass字段
        /// </summary>
        public void InitRclass()
        {
            string mysql = "update TopicReply set Rclass=Sclass from TopicReply,Students where Rsnum=Snum ";
            DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 获取当前班级学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowDoneReplyCids(int Rgrade, int Rclass, int Rterm, int Ryear)
        {
            string mysql = "SELECT DISTINCT Rcid FROM TopicReply where Rterm=" + Rterm + " and Rgrade=" + Rgrade + " and Rclass=" + Rclass + " and Ryear=" + Ryear;
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string strtemp = "";
                for (int i = 0; i < n; i++)
                {
                    strtemp = strtemp + dt.Rows[i]["Rcid"].ToString() + ",";
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
        public string ShowStuDoneReplyCids(string Snum, int Rterm, int Rgrade)
        {
            string mysql = "SELECT DISTINCT Rcid FROM TopicReply where Rsnum='" + Snum + "' and Rterm=" + Rterm + " and Rgrade=" + Rgrade;
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string strtemp = "";
                for (int i = 0; i < n; i++)
                {
                    strtemp = strtemp + dt.Rows[i]["Rcid"].ToString() + ",";
                }
                return strtemp;
            }
            else
            {
                return "";
            }
        }
		/*
		*/

		#endregion  Method
	}
}

