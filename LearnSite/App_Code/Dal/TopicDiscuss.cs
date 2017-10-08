using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:TopicDiscuss
	/// </summary>
	public class TopicDiscuss
	{
		public TopicDiscuss()
		{}
		#region  Method


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TopicDiscuss");
			strSql.Append(" where ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.TopicDiscuss model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TopicDiscuss(");
            strSql.Append("Tcid,Ttitle,Tcontent,Tcount,Tteacher,Tdate,Tclose)");
            strSql.Append(" values (");
            strSql.Append("@Tcid,@Ttitle,@Tcontent,@Tcount,@Tteacher,@Tdate,@Tclose)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Tcid", SqlDbType.Int,4),
					new SqlParameter("@Ttitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Tcontent", SqlDbType.NText),
					new SqlParameter("@Tcount", SqlDbType.Int,4),
					new SqlParameter("@Tteacher", SqlDbType.Int,4),
					new SqlParameter("@Tdate", SqlDbType.DateTime),
					new SqlParameter("@Tclose", SqlDbType.Bit,1)};
            parameters[0].Value = model.Tcid;
            parameters[1].Value = model.Ttitle;
            parameters[2].Value = model.Tcontent;
            parameters[3].Value = model.Tcount;
            parameters[4].Value = model.Tteacher;
            parameters[5].Value = model.Tdate;
            parameters[6].Value = model.Tclose;

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
        /// 更新老师总结
        /// </summary>
        /// <param name="Tid"></param>
        /// <param name="Tresult"></param>
        /// <returns></returns>
        public bool UpdateTresult(int Tid, string Tresult)
        {
            string mysql = "update TopicDiscuss set Tresult='" + Tresult + "' where Tid=" + Tid;
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
        /// 更新主题讨论的开关设置
        /// </summary>
        /// <param name="Tid"></param>
        /// <param name="Tclose"></param>
        /// <returns></returns>
        public bool UpdateTclose(int Tid,bool Tclose)
        {
            string mysql = "update TopicDiscuss set Tclose=@Tclose  where Tid=" + Tid;
            SqlParameter[] parameters = {
					new SqlParameter("@Tclose", SqlDbType.Bit,1)};
            parameters[0].Value = Tclose;

            int rowsAffected = DbHelperSQL.ExecuteSql(mysql,parameters);
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
        /// 更新主题讨论的开关设置
        /// </summary>
        /// <param name="Tid"></param>
        /// <returns></returns>
        public bool UpdateTclose(int Tid)
        {
            string mysql = "update TopicDiscuss set Tclose= Tclose^1  where Tid=" + Tid;
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
        /// 关闭该教师的所有主题讨论
        /// </summary>
        /// <param name="Tid"></param>
        /// <returns></returns>
        public bool CloseMyAllTopic(int hid)
        {
            string mysql = "update TopicDiscuss set Tclose= 1  where Tteacher=" + hid;
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
        /// 更新讨论主题
        /// </summary>
        /// <param name="Tid"></param>
        /// <param name="Ttitle"></param>
        /// <param name="Tcontent"></param>
        /// <param name="Tclose"></param>
        /// <returns></returns>
        public bool UpdateTopic(int Tid, string Ttitle, string Tcontent,bool Tclose)
        {
            string mysql = "update TopicDiscuss set Ttitle='"+Ttitle+"',Tcontent='"+Tcontent+"',Tclose=@Tclose where Tid="+Tid;
            SqlParameter[] parameters = {
					new SqlParameter("@Tclose", SqlDbType.Bit,1)};
            parameters[0].Value = Tclose;
            int rowsAffected = DbHelperSQL.ExecuteSql(mysql,parameters);
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
		public bool Update(LearnSite.Model.TopicDiscuss model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TopicDiscuss set ");
            strSql.Append("Tcid=@Tcid,");
            strSql.Append("Ttitle=@Ttitle,");
            strSql.Append("Tcontent=@Tcontent,");
            strSql.Append("Tcount=@Tcount,");
            strSql.Append("Tteacher=@Tteacher,");
            strSql.Append("Tdate=@Tdate,");
            strSql.Append("Tclose=@Tclose,");
            strSql.Append("Tresult=@Tresult");
            strSql.Append(" where Tid=@Tid");
            SqlParameter[] parameters = {
					new SqlParameter("@Tcid", SqlDbType.Int,4),
					new SqlParameter("@Ttitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Tcontent", SqlDbType.NText),
					new SqlParameter("@Tcount", SqlDbType.Int,4),
					new SqlParameter("@Tteacher", SqlDbType.Int,4),
					new SqlParameter("@Tdate", SqlDbType.DateTime),
					new SqlParameter("@Tclose", SqlDbType.Bit,1),
					new SqlParameter("@Tresult", SqlDbType.NText),
					new SqlParameter("@Tid", SqlDbType.Int,4)};
            parameters[0].Value = model.Tcid;
            parameters[1].Value = model.Ttitle;
            parameters[2].Value = model.Tcontent;
            parameters[3].Value = model.Tcount;
            parameters[4].Value = model.Tteacher;
            parameters[5].Value = model.Tdate;
            parameters[6].Value = model.Tclose;
            parameters[7].Value = model.Tresult;
            parameters[8].Value = model.Tid;

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
		public bool Delete(int Tid)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TopicDiscuss ");
            strSql.Append(" where Tid=@Tid");
            SqlParameter[] parameters = {
					new SqlParameter("@Tid", SqlDbType.Int,4)
			};
            parameters[0].Value = Tid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
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
		public bool DeleteList(string Tidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TopicDiscuss ");
			strSql.Append(" where Tid in ("+Tidlist + ")  ");
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
		public LearnSite.Model.TopicDiscuss GetModel(int Tid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" Tid,Tcid,Ttitle,Tcontent,Tcount,Tteacher,Tdate,Tclose,Tresult ");
			strSql.Append(" from TopicDiscuss ");
			strSql.Append(" where Tid="+Tid+"" );
			LearnSite.Model.TopicDiscuss model=new LearnSite.Model.TopicDiscuss();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Tid"].ToString()!="")
				{
					model.Tid=int.Parse(ds.Tables[0].Rows[0]["Tid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tcid"].ToString()!="")
				{
					model.Tcid=int.Parse(ds.Tables[0].Rows[0]["Tcid"].ToString());
				}
				model.Ttitle=ds.Tables[0].Rows[0]["Ttitle"].ToString();
				model.Tcontent=ds.Tables[0].Rows[0]["Tcontent"].ToString();
				if(ds.Tables[0].Rows[0]["Tcount"].ToString()!="")
				{
					model.Tcount=int.Parse(ds.Tables[0].Rows[0]["Tcount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tteacher"].ToString()!="")
				{
					model.Tteacher=int.Parse(ds.Tables[0].Rows[0]["Tteacher"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tdate"].ToString()!="")
				{
					model.Tdate=DateTime.Parse(ds.Tables[0].Rows[0]["Tdate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tclose"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Tclose"].ToString()=="1")||(ds.Tables[0].Rows[0]["Tclose"].ToString().ToLower()=="true"))
					{
						model.Tclose=true;
					}
					else
					{
						model.Tclose=false;
					}
				}
				model.Tresult=ds.Tables[0].Rows[0]["Tresult"].ToString();
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
        public LearnSite.Model.TopicDiscuss GetModel(DataTable dt, int Tsort)
        {
            LearnSite.Model.TopicDiscuss model = new LearnSite.Model.TopicDiscuss();
            int Count = dt.Rows.Count;
            if (Count > 0)
            {
                if (Tsort < Count)
                {
                    if (dt.Rows[Tsort]["Tid"].ToString() != "")
                    {
                        model.Tid = int.Parse(dt.Rows[Tsort]["Tid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Tcid"].ToString() != "")
                    {
                        model.Tcid = int.Parse(dt.Rows[Tsort]["Tcid"].ToString());
                    }
                    model.Ttitle = dt.Rows[Tsort]["Ttitle"].ToString();
                    model.Tcontent = dt.Rows[Tsort]["Tcontent"].ToString();
                    if (dt.Rows[Tsort]["Tcount"].ToString() != "")
                    {
                        model.Tcount = int.Parse(dt.Rows[Tsort]["Tcount"].ToString());
                    }
                    if (dt.Rows[Tsort]["Tteacher"].ToString() != "")
                    {
                        model.Tteacher = int.Parse(dt.Rows[Tsort]["Tteacher"].ToString());
                    }
                    if (dt.Rows[Tsort]["Tdate"].ToString() != "")
                    {
                        model.Tdate = DateTime.Parse(dt.Rows[Tsort]["Tdate"].ToString());
                    }
                    if (dt.Rows[Tsort]["Tclose"].ToString() != "")
                    {
                        if ((dt.Rows[Tsort]["Tclose"].ToString() == "1") || (dt.Rows[Tsort]["Tclose"].ToString().ToLower() == "true"))
                        {
                            model.Tclose = true;
                        }
                        else
                        {
                            model.Tclose = false;
                        }
                    }
                    model.Tresult = dt.Rows[Tsort]["Tresult"].ToString();
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
			strSql.Append("select Tid,Tcid,Ttitle,Tcontent,Tcount,Tteacher,Tdate,Tclose,Tresult ");
			strSql.Append(" FROM TopicDiscuss ");
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
			strSql.Append(" Tid,Tcid,Ttitle,Tcontent,Tcount,Tteacher,Tdate,Tclose,Tresult ");
			strSql.Append(" FROM TopicDiscuss ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/

		#endregion  Method
	}
}

