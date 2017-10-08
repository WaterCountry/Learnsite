using System;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类Webstudy。
	/// </summary>
	public class Webstudy
	{
		public Webstudy()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Wid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Webstudy");
			strSql.Append(" where Wid=@Wid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4)};
			parameters[0].Value = Wid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsWnum(string Wnum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Webstudy");
            strSql.Append(" where Wnum=@Wnum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50)};
            parameters[0].Value = Wnum;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wpwd"></param>
        public void AddOne(string Wnum, string Wpwd)
        {
            string mysql = "insert into Webstudy (Wnum,Wpwd) values (@Wnum,@Wpwd) ";
            SqlParameter[] parameters = {
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Wpwd", SqlDbType.NVarChar,50)};
            parameters[0].Value = Wnum;
            parameters[1].Value = Wpwd;

            DbHelperSQL.ExecuteSql(mysql,parameters);
        }
        /// <summary>
        /// 增加一条数据模拟学生账号
        /// </summary>
        public int AddSimulation(string Wnum, string Wpwd, string Wurl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Webstudy(");
            strSql.Append("Wnum,Wpwd,Wurl)");
            strSql.Append(" values (");
            strSql.Append("@Wnum,@Wpwd,@Wurl)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Wpwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Wurl", SqlDbType.NVarChar,50)};
            parameters[0].Value = Wnum;
            parameters[1].Value = Wpwd;
            parameters[2].Value = Wurl;

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
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Webstudy model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Webstudy(");
			strSql.Append("Wnum,Wpwd,Wvote,Wegg,Wscore,Wcheck,WquotaCurrent)");
			strSql.Append(" values (");
			strSql.Append("@Wnum,@Wpwd,@Wvote,@Wegg,@Wscore,@Wcheck,@WquotaCurrent)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Wpwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Wvote", SqlDbType.Int,4),
					new SqlParameter("@Wegg", SqlDbType.Int,4),
					new SqlParameter("@Wscore", SqlDbType.Int,4),
					new SqlParameter("@Wcheck", SqlDbType.Bit,1),
					new SqlParameter("@WquotaCurrent", SqlDbType.Int,4)};
			parameters[0].Value = model.Wnum;
			parameters[1].Value = model.Wpwd;
			parameters[2].Value = model.Wvote;
			parameters[3].Value = model.Wegg;
			parameters[4].Value = model.Wscore;
			parameters[5].Value = model.Wcheck;
			parameters[6].Value = model.WquotaCurrent;

			object obj =DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
        /// 从学生表读取Webstudy表中不存在的数据，插入Webstudy表中
        /// </summary>
        public void AddAll()
        {
            string mysql = "insert into Webstudy(Wnum,Wpwd,Wsid) select Snum as Wnum,Spwd as Wpwd,Sid as Wsid from Students where  Sid not in(select Wsid from Webstudy) ";
            DbHelperSQL.ExecuteSql(mysql);
            System.Threading.Thread.Sleep(500);
            WebUpdateWurl();//重新生成Wurl
        }
        /// <summary>
        /// 根据网站编号，给该网站加分
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wscore"></param>
        public void UpdateOne(int Wid, int Wscore)
        {
            string mysql = "update Webstudy set Wcheck=1,Wscore=Wscore+" + Wscore + " where Wcheck=0 and Wid=" + Wid;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 将该班级所有未评的网站全评为P，即6分
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        public void UpdateOneClass(int Sgrade, int Sclass)
        {
            DateTime dt = DateTime.Now;
            string Wupdate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            string mysql = "update Webstudy set Wcheck=1,Wscore=Wscore+6 where Wcheck=0 and WquotaCurrent>0 and Wupdate>'"+Wupdate+"'and Wnum in ( select Snum from Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass + ")";
            DbHelperSQL.ExecuteSql(mysql);        
        }
        /// <summary>
        /// 更新某个学号的ftp密码跟学号密码一致
        /// </summary>
        /// <param name="Wnum"></param>
        public void UpdateWpwd(string Wnum)
        {
            string mysql = "update Webstudy set Wpwd=Spwd from Webstudy,Students where Wnum='"+Wnum+"' and Wnum=Snum and Wpwd<>Spwd";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 更新某个学号的ftp登录密码
        /// </summary>
        /// <param name="Wnum"></param>
        public void UpdateWpwd2(string Wnum,string Wpwd)
        {
            string mysql = "update Webstudy set Wpwd='"+Wpwd+"' where Wnum='" + Wnum + "'";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 更新網站評價狀態，取反
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWcheck(string Wid)
        {
            string mysql = "update Webstudy set Wcheck=~Wcheck where Wid="+Wid;//Wid為整形不加單引號
            DbHelperSQL.ExecuteSql(mysql);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Webstudy model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Webstudy set ");
			strSql.Append("Wnum=@Wnum,");
			strSql.Append("Wpwd=@Wpwd,");
			strSql.Append("Wvote=@Wvote,");
			strSql.Append("Wegg=@Wegg,");
			strSql.Append("Wscore=@Wscore,");
			strSql.Append("Wcheck=@Wcheck,");
			strSql.Append("WquotaCurrent=@WquotaCurrent");
			strSql.Append(" where Wid=@Wid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4),
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Wpwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Wvote", SqlDbType.Int,4),
					new SqlParameter("@Wegg", SqlDbType.Int,4),
					new SqlParameter("@Wscore", SqlDbType.Int,4),
					new SqlParameter("@Wcheck", SqlDbType.Bit,1),
					new SqlParameter("@WquotaCurrent", SqlDbType.Int,4)};
			parameters[0].Value = model.Wid;
			parameters[1].Value = model.Wnum;
			parameters[2].Value = model.Wpwd;
			parameters[3].Value = model.Wvote;
			parameters[4].Value = model.Wegg;
			parameters[5].Value = model.Wscore;
			parameters[6].Value = model.Wcheck;
			parameters[7].Value = model.WquotaCurrent;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 重置所教班级网站投票次数
        /// </summary>
        public void ResetWegg(int eggs)
        {
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"] != null)
            {
                string hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                int Rhid = Int32.Parse(hid);
                string strSql = "update Webstudy set  Wcheck=0, Wegg="+eggs+" where Wnum in (select Snum from Students,Room where Sgrade=Rgrade and Sclass=Rclass and Rset=1 and Rhid=" + Rhid + ")";

                DbHelperSQL.ExecuteSql(strSql);
            }
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Wid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Webstudy ");
			strSql.Append(" where Wid=@Wid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4)};
			parameters[0].Value = Wid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Webstudy GetModel(int Wid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Wid,Wnum,Wpwd,Wvote,Wegg,Wscore,Wcheck,WquotaCurrent from Webstudy ");
			strSql.Append(" where Wid=@Wid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4)};
			parameters[0].Value = Wid;

			LearnSite.Model.Webstudy model=new LearnSite.Model.Webstudy();
            DataSet ds =DbHelperSQL.Query(strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Wid"].ToString()!="")
				{
					model.Wid=int.Parse(ds.Tables[0].Rows[0]["Wid"].ToString());
				}
				model.Wnum=ds.Tables[0].Rows[0]["Wnum"].ToString();
				model.Wpwd=ds.Tables[0].Rows[0]["Wpwd"].ToString();
				if(ds.Tables[0].Rows[0]["Wvote"].ToString()!="")
				{
					model.Wvote=int.Parse(ds.Tables[0].Rows[0]["Wvote"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Wegg"].ToString()!="")
				{
					model.Wegg=int.Parse(ds.Tables[0].Rows[0]["Wegg"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Wscore"].ToString()!="")
				{
					model.Wscore=int.Parse(ds.Tables[0].Rows[0]["Wscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Wcheck"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Wcheck"].ToString()=="1")||(ds.Tables[0].Rows[0]["Wcheck"].ToString().ToLower()=="true"))
					{
						model.Wcheck=true;
					}
					else
					{
						model.Wcheck=false;
					}
				}
				if(ds.Tables[0].Rows[0]["WquotaCurrent"].ToString()!="")
				{
					model.WquotaCurrent=int.Parse(ds.Tables[0].Rows[0]["WquotaCurrent"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 根据学号，得到一个对象实体
        /// </summary>
        public LearnSite.Model.Webstudy GetModelByWnum(string Wnum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Wid,Wnum,Wpwd,Wvote,Wegg,Wscore,Wcheck,WquotaCurrent from Webstudy ");
            strSql.Append(" where Wnum=@Wnum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50)};
            parameters[0].Value = Wnum;

            LearnSite.Model.Webstudy model = new LearnSite.Model.Webstudy();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Wid"].ToString() != "")
                {
                    model.Wid = int.Parse(ds.Tables[0].Rows[0]["Wid"].ToString());
                }
                model.Wnum = ds.Tables[0].Rows[0]["Wnum"].ToString();
                model.Wpwd = ds.Tables[0].Rows[0]["Wpwd"].ToString();
                if (ds.Tables[0].Rows[0]["Wvote"].ToString() != "")
                {
                    model.Wvote = int.Parse(ds.Tables[0].Rows[0]["Wvote"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wegg"].ToString() != "")
                {
                    model.Wegg = int.Parse(ds.Tables[0].Rows[0]["Wegg"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wscore"].ToString() != "")
                {
                    model.Wscore = int.Parse(ds.Tables[0].Rows[0]["Wscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wcheck"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Wcheck"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wcheck"].ToString().ToLower() == "true"))
                    {
                        model.Wcheck = true;
                    }
                    else
                    {
                        model.Wcheck = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["WquotaCurrent"].ToString() != "")
                {
                    model.WquotaCurrent = int.Parse(ds.Tables[0].Rows[0]["WquotaCurrent"].ToString());
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
			strSql.Append("select Wid,Wnum,Wpwd,Wvote,Wegg,Wscore,Wcheck,WquotaCurrent ");
			strSql.Append(" FROM Webstudy ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return DbHelperSQL.Query(strSql.ToString());
		}

        
        /// <summary>
        /// 获得条件数据列表
        /// </summary>
        public DataSet GetListWeb(int Sgrade, int Sclass)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Snum,Sname,Syear,Sgrade,Sclass, Wid,Wnum,Wpwd,Wvote,Wegg,Wscore,Wcheck,WquotaCurrent,Wupdate,Wurl");
            strSql.Append(" FROM Students,Webstudy ");
            strSql.Append(" where Snum=Wnum and Sgrade=@Sgrade and Sclass=@Sclass order by Snum ASC");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4)};
            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
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
			strSql.Append(" Wid,Wnum,Wpwd,Wvote,Wegg,Wscore,Wcheck,WquotaCurrent ");
			strSql.Append(" FROM Webstudy ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 学年升级，删除Webstudy中学号不在Students的记录
        /// </summary>
        public void Upgrade()
        {
            string websql = "delete Webstudy where Wnum not in (select Snum from Students)";
            DbHelperSQL.ExecuteSql(websql);
        }

        /// <summary>
        /// 给网站投票加1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWvote(int Wid)
        {
            string mysql = "update Webstudy set Wvote=Wvote+1 where Wid="+Wid;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 给网站蛋数减1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateMyWegg(string Wnum)
        {
            string mysql = "update Webstudy set Wegg=Wegg-1 where Wnum='"+Wnum+"'";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 获取网站投票数
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public int GetMyWegg(string Wnum)
        {
            string mysql = "select top 1 Wegg from Webstudy where Wnum='"+Wnum+"'";
            string getstr= DbHelperSQL.FindString(mysql);
            if (getstr != "")
            {
                return int.Parse(getstr);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 获得本班级网站列表，返回Sname,Snum,Wid,Wvote,Wscore,Wupdate,Wurl
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Mynum"></param>
        /// <returns></returns>
        public DataSet GetAllSite(int Sgrade, int Sclass,string Mynum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sname,Snum,Wid,Wvote,Wscore,Wupdate,Wurl");
            strSql.Append(" FROM Students,Webstudy ");
            strSql.Append(" where Snum=Wnum and Sgrade=@Sgrade and Sclass=@Sclass and Snum<>@Mynum ");
            strSql.Append(" order by Wvote asc");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4),
                    new SqlParameter("@Mynum", SqlDbType.NVarChar,50)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;
            parameters[2].Value = Mynum;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询WebFtp用户表密码
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public  string FindWebFtpPwd(string Wnum)
        {
            string mysql = "SELECT Wpwd FROM Webstudy WHERE Wnum='" + Wnum + "'";
            return DbHelperSQL.FindString(mysql);
        }

        public void WebSiteUpdateCheck(string htmlname)
        {
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                int Rhid = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString());//教师编号
                string mysql = "select Syear,Sclass,Sid,Snum from Students,Room where Sgrade=Rgrade and Sclass=Rclass and Rset=1 and Rhid=" + Rhid;
                DataSet ds = DbHelperSQL.Query(mysql);
                int dscount = ds.Tables[0].Rows.Count;
                if (dscount > 0)
                {
                    for (int i = 0; i < dscount; i++)
                    {
                        int Syear = int.Parse(ds.Tables[0].Rows[i]["Syear"].ToString());
                        int Sclass = int.Parse(ds.Tables[0].Rows[i]["Sclass"].ToString());
                        string Snum = ds.Tables[0].Rows[i]["Snum"].ToString();
                        int Sid =int.Parse( ds.Tables[0].Rows[i]["Sid"].ToString());
                        string updatetime = LearnSite.Common.Htmlcheck.HtmlUpdatetime(Syear, Sclass, Snum, htmlname);
                        if (updatetime != "")
                        {
                            UpdateWebTime(Snum, updatetime);//更新Webstudy表该学号日期
                            LearnSite.BLL.Signin sn = new LearnSite.BLL.Signin();
                            sn.UpdateQwork(Sid, 1);//签到表中增加作品数量为1
                        }
                    }
                }
                ClearNoSiteVote();
            }
        }

        /// <summary>
        /// 更新网站更新日期空间占用大小
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="updatetime"></param>
        public void UpdateWebTimeSize(string Wnum, DateTime Wupdate, int WquotaCurrent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Webstudy set Wupdate=@Wupdate,WquotaCurrent=@WquotaCurrent where Wnum=@Wnum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
                    new SqlParameter("@Wupdate", SqlDbType.DateTime),
                    new SqlParameter("@WquotaCurrent", SqlDbType.Int,4)};

            parameters[0].Value = Wnum;
            parameters[1].Value = Wupdate;
            parameters[2].Value = WquotaCurrent;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新网站更新日期
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="updatetime"></param>
        public void UpdateWebTime(string Wnum, string updatetime)
        {
            string mysql = "update Webstudy set  Wupdate='"+updatetime+"' where Wnum='"+Wnum+"'";
            DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        ///  更新网站空间占用大小
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="WquotaCurrent"></param>
        public void UpdateWebSize(string Wnum, int WquotaCurrent)
        {
            string mysql = "update Webstudy set  WquotaCurrent=" + WquotaCurrent + " where Wnum='" + Wnum + "'";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 将无网站制作内容的得票清零
        /// </summary>
        public void ClearNoSiteVote()
        {
            string mysql = "update Webstudy set Wvote=0,Wscore=0 where WquotaCurrent=0";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 显示指定数量的网站得票排行列表
        /// </summary>
        /// <param name="TopNum"></param>
        /// <returns></returns>
        public DataSet WebTopShow(int TopNum)
        {
            StringBuilder strSql = new StringBuilder();
            string aa = "select top  " + TopNum.ToString() + "  Snum,Sname,Syear,Sgrade,Sclass, Wnum,Wvote,Wegg,Wscore,Wcheck,Wurl ";
            strSql.Append(aa);
            strSql.Append("  FROM Students,Webstudy ");
            strSql.Append("  where Snum=Wnum  order by Wscore DESC, Wvote DESC");

            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 重新生成Webstudy表中Wurl
        /// </summary>
        public void WebUpdateWurl(int Hid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Webstudy set Webstudy.Wurl=ts.Turl ");
            strSql.Append(" from( select Snum,('~/FtpSpace/'+ convert(varchar(10),Syear) ");
            strSql.Append(" +'/'+convert(varchar(10),Sclass)+'/'+Snum");
            strSql.Append(") as Turl from Students,Room where Sgrade=Rgrade and Sclass=Rclass and Rhid=@Hid) as ts ");
            strSql.Append("where Webstudy.Wnum=ts.Snum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Hid", SqlDbType.Int,4)};
            parameters[0].Value = Hid;

            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
        }
        /// <summary>
        /// 重新生成Webstudy表中Wurl
        /// </summary>
        public void WebUpdateWurl()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Webstudy set Webstudy.Wurl=ts.Turl ");
            strSql.Append(" from( select Snum,('~/FtpSpace/'+ convert(varchar(10),Syear) ");
            strSql.Append(" +'/'+convert(varchar(10),Sclass)+'/'+Snum");
            strSql.Append(") as Turl from Students) as ts ");
            strSql.Append("where Webstudy.Wnum=ts.Snum ");

            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 清除该班级的webstudy表中的记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        public void DelWebClass(int Sgrade, int Sclass)
        {
            string mysql = "delete Webstudy where Wnum in (select Snum from Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass + ")";
            DbHelperSQL.ExecuteSql(mysql);
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
			parameters[0].Value = "Webstudy";
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

