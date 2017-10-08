using System;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类Works。
	/// </summary>
	public class Works
	{
		public Works()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Wid", "Works"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Wid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Works");
			strSql.Append(" where Wid=@Wid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4)};
			parameters[0].Value = Wid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsWcid(int Wcid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Works");
            strSql.Append(" where Wcid=@Wcid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4)};
            parameters[0].Value = Wcid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        
        /// <summary>
        /// 是否存在该学号任务作品
        /// </summary>
        public bool ExistsMyMissonWork(int Wmid,string Wnum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Works");
            strSql.Append(" where Wmid=@Wmid and Wnum=@Wnum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Wmid", SqlDbType.Int,4),
                    new SqlParameter("@Wnum", SqlDbType.NVarChar,50)};
            parameters[0].Value = Wmid;
            parameters[1].Value = Wnum;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该学号上一个任务作品,Wmsort为上一个可提交任务序号
        /// </summary>
        public bool ExistsMyFirstWork(int Wcid, string Wnum,int Wmsort)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) Wmsort from Works");
            strSql.Append(" where Wcid=@Wcid and Wnum=@Wnum and Wmsort=@Wmsort ");
            SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4),
                    new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
                    new SqlParameter("@Wmsort", SqlDbType.Int,4)};
            parameters[0].Value = Wcid;
            parameters[1].Value = Wnum;
            parameters[2].Value = Wmsort;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LearnSite.Model.Works model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Works(");
            strSql.Append("Wnum,Wcid,Wmid,Wmsort,Wfilename,Wtype,Wurl,Wlength,Wscore,Wdate,Wip,Wtime,Wvote,Wegg,Wcheck,Wgrade,Wterm,Woffice,Wflash)");
            strSql.Append(" values (");
            strSql.Append("@Wnum,@Wcid,@Wmid,@Wmsort,@Wfilename,@Wtype,@Wurl,@Wlength,@Wscore,@Wdate,@Wip,@Wtime,@Wvote,@Wegg,@Wcheck,@Wgrade,@Wterm,@Woffice,@Wflash)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Wcid", SqlDbType.Int,4),
					new SqlParameter("@Wmid", SqlDbType.Int,4),
					new SqlParameter("@Wmsort", SqlDbType.Int,4),
					new SqlParameter("@Wfilename", SqlDbType.NVarChar,50),
                    new SqlParameter("@Wtype", SqlDbType.NVarChar,50),
					new SqlParameter("@Wurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Wlength", SqlDbType.Int,4),
					new SqlParameter("@Wscore", SqlDbType.Int,4),
					new SqlParameter("@Wdate", SqlDbType.DateTime),
					new SqlParameter("@Wip", SqlDbType.NVarChar,50),
					new SqlParameter("@Wtime", SqlDbType.NVarChar,50),
					new SqlParameter("@Wvote", SqlDbType.Int,4),
					new SqlParameter("@Wegg", SqlDbType.SmallInt,2),
					new SqlParameter("@Wcheck", SqlDbType.Bit,1),
                    new SqlParameter("@Wgrade", SqlDbType.Int,4),
					new SqlParameter("@Wterm", SqlDbType.Int,4),
					new SqlParameter("@Woffice", SqlDbType.Bit,1),
					new SqlParameter("@Wflash", SqlDbType.Bit,1)};
            parameters[0].Value = model.Wnum;
            parameters[1].Value = model.Wcid;
            parameters[2].Value = model.Wmid;
            parameters[3].Value = model.Wmsort;
            parameters[4].Value = model.Wfilename;
            parameters[5].Value = model.Wtype;
            parameters[6].Value = model.Wurl;
            parameters[7].Value = model.Wlength;
            parameters[8].Value = model.Wscore;
            parameters[9].Value = model.Wdate;
            parameters[10].Value = model.Wip;
            parameters[11].Value = model.Wtime;
            parameters[12].Value = model.Wvote;
            parameters[13].Value = model.Wegg;
            parameters[14].Value = model.Wcheck;
            parameters[15].Value = model.Wgrade;
            parameters[16].Value = model.Wterm;
            parameters[17].Value = model.Woffice;
            parameters[18].Value = model.Wflash;

            object obj =DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public void Update(LearnSite.Model.Works model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Works set ");
            strSql.Append("Wnum=@Wnum,");
            strSql.Append("Wcid=@Wcid,");
            strSql.Append("Wmid=@Wmid,");
            strSql.Append("Wmsort=@Wmsort,");
            strSql.Append("Wfilename=@Wfilename,");
            strSql.Append("Wurl=@Wurl,");
            strSql.Append("Wlength=@Wlength,");
            strSql.Append("Wscore=@Wscore,");
            strSql.Append("Wdate=@Wdate,");
            strSql.Append("Wip=@Wip,");
            strSql.Append("Wtime=@Wtime,");
            strSql.Append("Wvote=@Wvote,");
            strSql.Append("Wegg=@Wegg,");
            strSql.Append("Wcheck=@Wcheck,");
            strSql.Append("Wflash=0,");
            strSql.Append("Werror=0");
            strSql.Append(" where Wid=@Wid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4),
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Wcid", SqlDbType.Int,4),
					new SqlParameter("@Wmid", SqlDbType.Int,4),
					new SqlParameter("@Wmsort", SqlDbType.Int,4),
					new SqlParameter("@Wfilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Wurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Wlength", SqlDbType.Int,4),
					new SqlParameter("@Wscore", SqlDbType.Int,4),
					new SqlParameter("@Wdate", SqlDbType.DateTime),
					new SqlParameter("@Wip", SqlDbType.NVarChar,50),
					new SqlParameter("@Wtime", SqlDbType.NVarChar,50),
					new SqlParameter("@Wvote", SqlDbType.Int,4),
					new SqlParameter("@Wegg", SqlDbType.SmallInt,2),
					new SqlParameter("@Wcheck", SqlDbType.Bit,1)};
            parameters[0].Value = model.Wid;
            parameters[1].Value = model.Wnum;
            parameters[2].Value = model.Wcid;
            parameters[3].Value = model.Wmid;
            parameters[4].Value = model.Wmsort;
            parameters[5].Value = model.Wfilename;
            parameters[6].Value = model.Wurl;
            parameters[7].Value = model.Wlength;
            parameters[8].Value = model.Wscore;
            parameters[9].Value = model.Wdate;
            parameters[10].Value = model.Wip;
            parameters[11].Value = model.Wtime;
            parameters[12].Value = model.Wvote;
            parameters[13].Value = model.Wegg;
            parameters[14].Value = model.Wcheck;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 初始化新添加字段Woffice
        /// </summary>
        /// <returns></returns>
        public int UpdateWoffice()
        {
            int upCount = 0;
            string mysql = "update Works set Woffice=1 where Woffice is null and (Wtype='doc' or Wtype='ppt' or Wtype='xls') ";
            upCount = DbHelperSQL.ExecuteSql(mysql);
            string sqlstr = "update Works set Wflash=0 where Wflash is null";
            DbHelperSQL.ExecuteSql(sqlstr);
            string sqlaa = "update Works set Werror=0 where Werror is null";
            DbHelperSQL.ExecuteSql(sqlaa);

            return upCount;
        }
        /// <summary>
        /// 初始化新添加字段Wfscore
        /// </summary>
        /// <returns></returns>
        public void InitWfscore()
        {
            string sqlaa = "update Works set Wfscore=0 where Wfscore is null";
            DbHelperSQL.ExecuteSql(sqlaa);
        }

        /// <summary>
        /// 更新字段Wfscore
        /// </summary>
        /// <returns></returns>
        public void UpdateWfscore(int Wid,int Wfscore)
        {
            string sqlstr = "update Works set Wfscore="+Wfscore+" where Wid="+Wid;
            DbHelperSQL.ExecuteSql(sqlstr);
        }
        /// <summary>
        /// 获取Wfscore
        /// </summary>
        /// <returns></returns>
        public int GetWfscore(int Wid)
        {
            string sqlstr = "select Wfscore from Works where Wid=" + Wid;
            return DbHelperSQL.FindNum(sqlstr);
        }
        /// <summary>
        /// 清除该班级该活动作品的异常转换标志
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wid"></param>
        /// <param name="Wcid"></param>
        public void ClearWflasherror(int Sgrade, int Sclass, int Wmid, int Wcid)
        {
            string mysql = "update Works set Werror=0,Wflash=0 where Werror=1 and Wmid="+Wmid+" and Wcid="+Wcid+" and Wnum in (select Snum from Students where Sgrade="+Sgrade+" and Sclass="+Sclass+" )";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 更新一条数据，给一个作品评价(参数传送 Wid,Wscore, Wcheck)
        /// </summary>
        public int ScoreOneWork(LearnSite.Model.Works model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Works set ");
            strSql.Append("Wscore=@Wscore,");
            strSql.Append("Wlemotion=0,");
            strSql.Append("Wcheck=@Wcheck");
            strSql.Append(" where Wid=@Wid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4),
					new SqlParameter("@Wscore", SqlDbType.Int,4),
					new SqlParameter("@Wcheck", SqlDbType.Bit,1)};
            parameters[0].Value = model.Wid;
            parameters[1].Value = model.Wscore;
            parameters[2].Value = model.Wcheck;

           return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Wid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Works ");
			strSql.Append(" where Wid=@Wid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4)};
			parameters[0].Value = Wid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 删除一个班级的作业记录
        /// </summary>
        public int DelClass(int Wgrade,int Wclass,int Wyear)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Works ");
            strSql.Append(" where Wgood=0 and Wgrade=@Wgrade and Wclass=@Wclass and Wyear=@Wyear ");
            SqlParameter[] parameters = {
					new SqlParameter("@Wgrade", SqlDbType.Int,4),                                        
					new SqlParameter("@Wclass", SqlDbType.Int,4),
					new SqlParameter("@Wyear", SqlDbType.Int,4)};
            parameters[0].Value = Wgrade;
            parameters[1].Value = Wclass;
            parameters[2].Value = Wyear;

           return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 清除几年前的未推荐的作品记录
        /// </summary>
        /// <param name="Wyear"></param>
        public int DeleteOldyear(int Wyear)
        { 
            DateTime olddate = DateTime.Now.AddYears(-Wyear);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Works ");
            strSql.Append(" where Wgood=0 and Wdate <@Wdate");
            SqlParameter[] parameters = {
					new SqlParameter("@Wdate", SqlDbType.DateTime)};
            parameters[0].Value = olddate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);        
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.Works GetModelByStu(int Mid,string Snum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Works ");
            strSql.Append(" where Wnum=@Wnum and Wmid=@Wmid");
            SqlParameter[] parameters = {
					new SqlParameter("@Wmid", SqlDbType.Int,4),
                    new SqlParameter("@Wnum", SqlDbType.NVarChar,50)
};
            parameters[0].Value = Mid;
            parameters[1].Value = Snum;

            LearnSite.Model.Works model = new LearnSite.Model.Works();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Wid"].ToString() != "")
                {
                    model.Wid = int.Parse(ds.Tables[0].Rows[0]["Wid"].ToString());
                }
                model.Wnum = ds.Tables[0].Rows[0]["Wnum"].ToString();
                if (ds.Tables[0].Rows[0]["Wcid"].ToString() != "")
                {
                    model.Wcid = int.Parse(ds.Tables[0].Rows[0]["Wcid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wmid"].ToString() != "")
                {
                    model.Wmid = int.Parse(ds.Tables[0].Rows[0]["Wmid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wmsort"].ToString() != "")
                {
                    model.Wmsort = int.Parse(ds.Tables[0].Rows[0]["Wmsort"].ToString());
                }
                model.Wfilename = ds.Tables[0].Rows[0]["Wfilename"].ToString();
                model.Wurl = ds.Tables[0].Rows[0]["Wurl"].ToString();
                if (ds.Tables[0].Rows[0]["Wlength"].ToString() != "")
                {
                    model.Wlength = int.Parse(ds.Tables[0].Rows[0]["Wlength"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wscore"].ToString() != "")
                {
                    model.Wscore = int.Parse(ds.Tables[0].Rows[0]["Wscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wdate"].ToString() != "")
                {
                    model.Wdate = DateTime.Parse(ds.Tables[0].Rows[0]["Wdate"].ToString());
                }
                model.Wip = ds.Tables[0].Rows[0]["Wip"].ToString();
                model.Wtime = ds.Tables[0].Rows[0]["Wtime"].ToString();
                if (ds.Tables[0].Rows[0]["Wvote"].ToString() != "")
                {
                    model.Wvote = int.Parse(ds.Tables[0].Rows[0]["Wvote"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wegg"].ToString() != "")
                {
                    model.Wegg = int.Parse(ds.Tables[0].Rows[0]["Wegg"].ToString());
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
                model.Wself = ds.Tables[0].Rows[0]["Wself"].ToString();
                if (ds.Tables[0].Rows[0]["Wcan"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Wcan"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wcan"].ToString().ToLower() == "true"))
                    {
                        model.Wcan = true;
                    }
                    else
                    {
                        model.Wcan = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Wgood"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Wgood"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wgood"].ToString().ToLower() == "true"))
                    {
                        model.Wgood = true;
                    }
                    else
                    {
                        model.Wgood = false;
                    }
                }
                model.Wtype = ds.Tables[0].Rows[0]["Wtype"].ToString();
                if (ds.Tables[0].Rows[0]["Wgrade"].ToString() != "")
                {
                    model.Wgrade = int.Parse(ds.Tables[0].Rows[0]["Wgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wterm"].ToString() != "")
                {
                    model.Wterm = int.Parse(ds.Tables[0].Rows[0]["Wterm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Whit"].ToString() != "")
                {
                    model.Whit = int.Parse(ds.Tables[0].Rows[0]["Whit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wlscore"].ToString() != "")
                {
                    model.Wlscore = int.Parse(ds.Tables[0].Rows[0]["Wlscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wlemotion"].ToString() != "")
                {
                    model.Wlemotion = int.Parse(ds.Tables[0].Rows[0]["Wlemotion"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Woffice"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Woffice"].ToString() == "1") || (ds.Tables[0].Rows[0]["Woffice"].ToString().ToLower() == "true"))
                    {
                        model.Woffice = true;
                    }
                    else
                    {
                        model.Woffice = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Wflash"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Wflash"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wflash"].ToString().ToLower() == "true"))
                    {
                        model.Wflash = true;
                    }
                    else
                    {
                        model.Wflash = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Werror"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Werror"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wflash"].ToString().ToLower() == "true"))
                    {
                        model.Wflash = true;
                    }
                    else
                    {
                        model.Wflash = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Wfscore"].ToString() != "")
                {
                    model.Wfscore = int.Parse(ds.Tables[0].Rows[0]["Wfscore"].ToString());
                }
                model.Wthumbnail = ds.Tables[0].Rows[0]["Wthumbnail"].ToString();
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
        public LearnSite.Model.Works GetModel(int Wid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Works ");
            strSql.Append(" where Wid=@Wid");
            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4)
};
            parameters[0].Value = Wid;

            LearnSite.Model.Works model = new LearnSite.Model.Works();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Wid"].ToString() != "")
                {
                    model.Wid = int.Parse(ds.Tables[0].Rows[0]["Wid"].ToString());
                }
                model.Wnum = ds.Tables[0].Rows[0]["Wnum"].ToString();
                if (ds.Tables[0].Rows[0]["Wcid"].ToString() != "")
                {
                    model.Wcid = int.Parse(ds.Tables[0].Rows[0]["Wcid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wmid"].ToString() != "")
                {
                    model.Wmid = int.Parse(ds.Tables[0].Rows[0]["Wmid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wmsort"].ToString() != "")
                {
                    model.Wmsort = int.Parse(ds.Tables[0].Rows[0]["Wmsort"].ToString());
                }
                model.Wfilename = ds.Tables[0].Rows[0]["Wfilename"].ToString();
                model.Wurl = ds.Tables[0].Rows[0]["Wurl"].ToString();
                if (ds.Tables[0].Rows[0]["Wlength"].ToString() != "")
                {
                    model.Wlength = int.Parse(ds.Tables[0].Rows[0]["Wlength"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wscore"].ToString() != "")
                {
                    model.Wscore = int.Parse(ds.Tables[0].Rows[0]["Wscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wdate"].ToString() != "")
                {
                    model.Wdate = DateTime.Parse(ds.Tables[0].Rows[0]["Wdate"].ToString());
                }
                model.Wip = ds.Tables[0].Rows[0]["Wip"].ToString();
                model.Wtime = ds.Tables[0].Rows[0]["Wtime"].ToString();
                if (ds.Tables[0].Rows[0]["Wvote"].ToString() != "")
                {
                    model.Wvote = int.Parse(ds.Tables[0].Rows[0]["Wvote"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wegg"].ToString() != "")
                {
                    model.Wegg = int.Parse(ds.Tables[0].Rows[0]["Wegg"].ToString());
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
                model.Wself = ds.Tables[0].Rows[0]["Wself"].ToString();
                if (ds.Tables[0].Rows[0]["Wcan"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Wcan"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wcan"].ToString().ToLower() == "true"))
                    {
                        model.Wcan = true;
                    }
                    else
                    {
                        model.Wcan = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Wgood"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Wgood"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wgood"].ToString().ToLower() == "true"))
                    {
                        model.Wgood = true;
                    }
                    else
                    {
                        model.Wgood = false;
                    }
                }
                model.Wtype = ds.Tables[0].Rows[0]["Wtype"].ToString();
                if (ds.Tables[0].Rows[0]["Wgrade"].ToString() != "")
                {
                    model.Wgrade = int.Parse(ds.Tables[0].Rows[0]["Wgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wterm"].ToString() != "")
                {
                    model.Wterm = int.Parse(ds.Tables[0].Rows[0]["Wterm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Whit"].ToString() != "")
                {
                    model.Whit = int.Parse(ds.Tables[0].Rows[0]["Whit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wlscore"].ToString() != "")
                {
                    model.Wlscore = int.Parse(ds.Tables[0].Rows[0]["Wlscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wlemotion"].ToString() != "")
                {
                    model.Wlemotion = int.Parse(ds.Tables[0].Rows[0]["Wlemotion"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Woffice"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Woffice"].ToString() == "1") || (ds.Tables[0].Rows[0]["Woffice"].ToString().ToLower() == "true"))
                    {
                        model.Woffice = true;
                    }
                    else
                    {
                        model.Woffice = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Wflash"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Wflash"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wflash"].ToString().ToLower() == "true"))
                    {
                        model.Wflash = true;
                    }
                    else
                    {
                        model.Wflash = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Werror"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Werror"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wflash"].ToString().ToLower() == "true"))
                    {
                        model.Wflash = true;
                    }
                    else
                    {
                        model.Wflash = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Wfscore"].ToString() != "")
                {
                    model.Wfscore = int.Parse(ds.Tables[0].Rows[0]["Wfscore"].ToString());
                }
                model.Wname = ds.Tables[0].Rows[0]["Wname"].ToString();
                if (ds.Tables[0].Rows[0]["Wdscore"].ToString() != "")
                {
                    model.Wdscore = int.Parse(ds.Tables[0].Rows[0]["Wdscore"].ToString());
                }
                model.Wthumbnail = ds.Tables[0].Rows[0]["Wthumbnail"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据学生表的年级、班级(不影响班级升学)
        /// 多表联合查询作品，返回dataset
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wcheck"></param>
        /// <returns></returns>
        public DataSet GetListWcheckWork(int Wcid, int Sgrade, int Sclass, int Wmid, bool Wcheck ,string sort)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Wid,Wnum,Wmid,Wmsort,Wurl,Wtype,Wscore,Wtime,Wvote,Wcheck,Wself,Wcan,Wgood,Wfscore,Wdscore,Sname");
            strSql.Append(" FROM Works,Students ");
            strSql.Append(" where Wnum=Snum and Wcid=@Wcid and Wmid=@Wmid and  Sgrade=@Sgrade and Sclass=@Sclass and Wcheck=@Wcheck");
            string sortstr = " order by " + sort;
            strSql.Append(sortstr);
            SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),                    
					new SqlParameter("@Wmid", SqlDbType.Int,4),
                    new SqlParameter("@Wcheck", SqlDbType.Bit,1)};
            parameters[0].Value = Wcid;
            parameters[1].Value = Sgrade;
            parameters[2].Value = Sclass;
            parameters[3].Value = Wmid;
            parameters[4].Value = Wcheck;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 显示所教班级该学案所有未评作品
        /// select Wid,Wnum,Wmid,Wmsort,Wurl,Wtype,Wscore,Wtime,Wvote,Wcheck,Wself,Wcan,Wgood,Sname,Sgrade,Sclass,Mtitle
        /// </summary>
        /// <param name="Wcid"></param>
        /// <returns></returns>
        public DataTable GetListNoWcheckWork(int Wcid,int Wclass)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct Wid,Wnum,Wcid,Wmid,Wurl,Wscore,Sname,Wcheck,Wself");
            strSql.Append(" FROM Works,Students ");
            strSql.Append(" where Wcheck=0 and  Wcid=@Wcid and Wsid=Sid and Wgrade=Sgrade");
            if (Wclass > 0)
                strSql.Append(" and Wclass=@Wclass");

            SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4),
                    new SqlParameter("@Wclass", SqlDbType.Int,4)};
            parameters[0].Value = Wcid;
            parameters[1].Value = Wclass;

            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
        }

        /// <summary>
        /// 显示所教班级该学案所有未评班级
        /// select Sclass
        /// </summary>
        /// <param name="Wcid"></param>
        /// <returns></returns>
        public DataTable GetListNoWcheckClass(int Wcid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct Sclass");
            strSql.Append(" FROM Works,Students ");
            strSql.Append(" where Wcheck=0 and  Wcid=@Wcid and Wsid=Sid and Wgrade=Sgrade");
            SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4)};
            parameters[0].Value = Wcid;            
           
            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
        }

        /// <summary>
        /// 显示所教某班级该学案所有未评作品
        /// select Wid,Wnum,Wmid,Wmsort,Wurl,Wtype,Wscore,Wtime,Wvote,Wcheck,Wself,Wcan,Wgood,Sname,Sgrade,Sclass,Mtitle
        /// </summary>
        /// <param name="Wcid"></param>
        /// <returns></returns>
        public DataTable GetListNoWcheckWork(int Wcid,int Wgrade,int Wclass)
        {
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                string Hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Wid,Wnum,Wcid,Wmid,Wurl,Wscore,Sname,Sgrade,Sclass,Mtitle");
                strSql.Append(" FROM Works,Students,Mission,Room ");
                strSql.Append(" where Wcheck=0 and Wgrade=@Wgrade and Wclass=@Wclass and Wnum=Snum and Wcid=@Wcid and Wmid=Mid and  Sgrade=Rgrade and Sclass=Rclass and Rhid=@Hid");
                SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4),
					new SqlParameter("@Hid", SqlDbType.Int,4),
					new SqlParameter("@Wgrade", SqlDbType.Int,4),
					new SqlParameter("@Wclass", SqlDbType.Int,4)};
                parameters[0].Value = Wcid;
                parameters[1].Value = Int32.Parse(Hid);
                parameters[2].Value = Wgrade;
                parameters[3].Value = Wclass;

                return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 显示所教学案所有未评作品的班级
        /// Wgrade,Wclass
        /// </summary>
        /// <param name="Wcid"></param>
        /// <returns></returns>
        public DataTable GetListNoWcheckWclass(int Wcid)
        {
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                string Hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select distinct Wgrade,Wclass");
                strSql.Append(" FROM Works,Students,Room ");
                strSql.Append(" where Wcheck=0 and Wcid=@Wcid and Wsid=Sid and Wgrade=Rgrade and Wclass=Rclass and  Rhid=@Hid");
                SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4),
					new SqlParameter("@Hid", SqlDbType.Int,4)};
                parameters[0].Value = Wcid;
                parameters[1].Value = Int32.Parse(Hid);

                return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
            }
            else
            {
                return null;
            }
        } 
        /// <summary>
        /// 根据学生生的年级、班级(不影响班级升学)
        /// 设置该班本学案未评价的活动全部积分为10
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        public void WorkSetA(int Wcid, int Sgrade, int Sclass, int Wmid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Works set ");
            strSql.Append("Wscore=10,");
            strSql.Append("Wcheck=1");
            strSql.Append(" where Wcheck=0 and Wcid=@Wcid and Wmid=@Wmid and Wnum in (select Snum from Students where Sgrade=@Sgrade and Sclass=@Sclass)");
            SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),                    
					new SqlParameter("@Wmid", SqlDbType.Int,4)};
            parameters[0].Value = Wcid;
            parameters[1].Value = Sgrade;
            parameters[2].Value = Sclass;
            parameters[3].Value = Wmid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据学生生的年级、班级(不影响班级升学)
        /// 设置该班本学案未评价的活动全部积分为6
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        public void WorkSetP(int Wcid, int Sgrade, int Sclass, int Wmid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Works set ");
            strSql.Append("Wscore=6,");
            strSql.Append("Wcheck=1");
            strSql.Append(" where Wcheck=0 and Wcid=@Wcid and Wmid=@Wmid and Wnum in (select Snum from Students where Sgrade=@Sgrade and Sclass=@Sclass)");
            SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),                    
					new SqlParameter("@Wmid", SqlDbType.Int,4)};
            parameters[0].Value = Wcid;
            parameters[1].Value = Sgrade;
            parameters[2].Value = Sclass;
            parameters[3].Value = Wmid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);        
        }
        /// <summary>
        /// 根据学生生的年级、班级(不影响班级升学)
        /// 设置该班本学案未评价的活动全部积分为Wscore
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wscore"></param>
        public void WorkSetScore(int Wcid, int Sgrade, int Sclass, int Wmid,int Wscore)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Works set ");
            strSql.Append("Wscore=@Wscore,");
            strSql.Append("Wlemotion=0,");
            strSql.Append("Wcheck=1");
            strSql.Append(" where Wcheck=0 and Wcid=@Wcid and Wmid=@Wmid and Wnum in (select Snum from Students where Sgrade=@Sgrade and Sclass=@Sclass)");
            SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),                    
					new SqlParameter("@Wmid", SqlDbType.Int,4),
                    new SqlParameter("@Wscore", SqlDbType.Int,4)};
            parameters[0].Value = Wcid;
            parameters[1].Value = Sgrade;
            parameters[2].Value = Sclass;
            parameters[3].Value = Wmid;
            parameters[4].Value = Wscore;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 将所教班级所有未评作品的评分都设置为P，即6分
        /// </summary>
        public void WorkNoScoreSetP()
        {
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"] != null)
            {
                string hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                int Rhid = Int32.Parse(hid);
                string strSql = "update Works set Wscore=6,Wcheck=1 where Wcheck=0 and Wnum in ( select Snum from Students,Room where Sgrade=Rgrade and Sclass=Rclass  and Rhid=" + Rhid+")";
                DbHelperSQL.ExecuteSql(strSql);
            }
        }

        /// <summary>
        /// 更新指定Wid作品的积分
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wscore"></param>
        public void ScoreWork(int Wid, int Wscore)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Works set ");
            strSql.Append("Wscore=@Wscore,");
            if (Wscore == 12)
                strSql.Append("Wgood=1,");
            else
                strSql.Append("Wgood=0,");
            strSql.Append("Wlemotion=0,");
            strSql.Append("Wcheck=1");
            strSql.Append(" where Wid=@Wid");
            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4),                   
					new SqlParameter("@Wscore", SqlDbType.Int,4)};
            parameters[0].Value = Wid;
            parameters[1].Value = Wscore;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取本班本活动学分列表
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wgrade"></param>
        /// <param name="Wclass"></param>
        /// <returns></returns>
        public DataTable getScoreList(int Wmid, int Wgrade, int Wclass)
        {
            string mysql = "select Wnum as Snum ,Wscore as Score from Works where Wmid="+Wmid+" and Wgrade="+Wgrade+" and Wclass="+Wclass+" order by Wnum";
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        /// 设置指定Wid作品的评价状态和积分为零
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wcheck"></param>
        public void CancleScoreWork(int Wid, bool Wcheck)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Works set ");
            strSql.Append("Wscore=0,");
            strSql.Append("Wcheck=@Wcheck");
            strSql.Append(" where Wid=@Wid");
            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4),                   
					new SqlParameter("@Wcheck", SqlDbType.Bit,1)};
            parameters[0].Value = Wid;
            parameters[1].Value = Wcheck;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 显示本年级优秀作品的50条记录
        /// </summary>
        /// <param name="Wgrade"></param>
        /// <param name="GridViewwork"></param>
        public  DataSet ShowBestWork(int Sgrade,int Syear,int Sterm)
        {
            string mysql = "Select top 30 Wid, Wname,Wgrade,Wclass,Wscore from Works where  Wgrade=" + Sgrade + " and Wyear=" + Syear + " and Wterm=" + Sterm + " and Wscore>9  ORDER BY Wdate DESC,Wscore DESC ";
            return DbHelperSQL.GetDataSet(mysql);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Wid,Wnum,Wcid,Wmid,Wmsort,Wfilename,Wurl,Wlength,Wscore,Wdate,Wip,Wtime,Wvote,Wegg,Wcheck,Wself,Wcan,Wgood,Wtype,Wgrade,Wterm,Whit,Wlscore,Wlemotion,Woffice,Wflash,Werror,Wfscore  ");
            strSql.Append(" FROM Works ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public string GetWid(string Wmid, string Wnum)
        {
            string mysql = "select top 1 Wid from Works where Wmid=" + Wmid + " and Wnum='" + Wnum + "'";
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Wid,Wnum,Wcid,Wmid,Wmsort,Wfilename,Wurl,Wlength,Wscore,Wdate,Wip,Wtime,Wvote,Wegg,Wcheck,Wself,Wcan,Wgood,Wtype,Wgrade,Wterm,Whit,Wlscore,Wlemotion,Woffice,Wflash,Werror,Wfscore   ");
            strSql.Append(" FROM Works ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 显示我的所有作品
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public DataTable ShowMywork(string Snum)
        {
            string mysql = "SELECT Wid,Wcid,Wmid,Wmsort,Wscore,Wvote,Wcheck,Wsid,Wdscore,Ctitle,Cobj,Cterm,Mtitle  FROM Works,Courses,Mission Where Wnum='" + Snum + "' and Cdelete=0 and Wcid=Cid and Wmid=Mid  ORDER BY Cobj DESC,Cterm DESC, Wdate DESC";
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        /// 显示我本年级本学期的所有作品
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wgrade"></param>
        /// <param name="Wterm"></param>
        /// <returns></returns>
        public DataSet ShowThisTermWorks(string Wnum,int Wgrade,int Wterm)
        {
            string mysql = "SELECT Wid,Wcid,Wmid,Wmsort,Wgrade,Wclass,Wterm,Wurl,Wscore,Wdate,Wvote,Wcheck,Wsid,Wdscore,Ctitle,Mtitle  FROM Works,Courses,Mission Where Wnum='" + Wnum + "' and Wgrade=" + Wgrade + " and Wterm=" + Wterm + " and Cdelete=0 and Wcid=Cid and Wmid=Mid and Cobj=Wgrade  ORDER BY  Wdate DESC";
            return DbHelperSQL.Query(mysql);
        }

        /// <summary>
        /// 显示我本年级本学期的所有作品
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wgrade"></param>
        /// <param name="Wterm"></param>
        /// <returns></returns>
        public DataSet ShowThisTermWorksCircle(string Wnum, int Wgrade, int Wterm)
        {
            string mysql = "SELECT Mtitle,Wurl  FROM Works,Courses,Mission Where Wnum='" + Wnum + "' and Wgrade=" + Wgrade + " and Wterm=" + Wterm + " and Cdelete=0 and Wcid=Cid and Wmid=Mid and Cobj=Wgrade  ORDER BY  Wdate DESC";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 显示我的所有作品
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public DataTable ShowMyAllWorks(string Wnum)
        {
            string mysql = "SELECT Wid,Wcid,Wmid,Wgrade,Wclass,Wterm,Wurl,Wscore,Wdate,Wvote,Wcheck,Wdscore,Ctitle,Mtitle  FROM Works,Courses,Mission Where Wnum='" + Wnum + "' and Cdelete=0 and Wcid=Cid and Wmid=Mid and Cobj=Wgrade  ORDER BY  Wid asc";
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        /// 列表我有所作品的学案活动代号和分值
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public DataSet ShowMyworkScore(string Wnum)
        {
            string mysql = "SELECT Wmid,Wscore,Wdscore FROM Works Where  Wnum='" + Wnum + "'   Wdate DESC";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 根据作品Wid获得学案名称
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public string GetCtitle(int Wid)
        {
            string mysql = "select Ctitle from Works, Courses  where Wcid=Cid and Wid="+Wid;
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 阅读量Whit加1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWhit(int Wid)
        {
            string mysql = "update Works set Whit=Whit+1 where  Wid=" + Wid;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 投票Wvote加1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWvote(int Wid)
        {
            string mysql = "update Works set Wvote=Wvote+1 where  Wid="+Wid;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 投票Wegg减1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWegg(int Wid)
        {
            string mysql = "update Works set Wegg=Wegg-1 where  Wid=" + Wid;
            DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 如果不是模拟学生账号，则投票Wegg减1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWegg(int Wmid,string Wnum)
        {
            if (!Wnum.StartsWith("s"))
            {
                string mysql = "update Works set Wegg=Wegg-1 where  Wmid=" + Wmid + " and Wnum='" + Wnum + "'";
                DbHelperSQL.ExecuteSql(mysql);
            }
        }


        /// <summary>
        /// 获得本作品Wegg
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public int GetWegg(int Wid)
        {
            string mysql = "select Wegg from Works where Wid="+Wid;
            string findstr = DbHelperSQL.FindString(mysql);
            if (findstr != null)
            {
                return Int32.Parse(findstr);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获得本作品Wegg
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public int GetWegg(int Wmid,string Wnum)
        {
            if (Wnum.StartsWith("s"))
            {
                return 9999;//如果是模拟学生账号，则提供9999个；
            }
            string mysql = "select Wegg from Works where Wmid=" + Wmid+" and Wnum='"+Wnum+"'";
            string findstr = DbHelperSQL.FindString(mysql);
            if (findstr != null)
            {
                return Int32.Parse(findstr);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 获得本作品Wegg
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public int GetWegg(int Wmid, int Wsid)
        {
            if (Wsid < 0)
            {
                return 9999;//如果是模拟学生账号，则提供9999个；
            }
            else
            {
                string mysql = "select Wegg from Works where Wmid=" + Wmid + " and Wsid=" + Wsid;
                return DbHelperSQL.FindNum(mysql);
            }
        }
        /// <summary>
        /// 显示该学案该学号完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string HowCidWorks(int Wcid, string Wnum)
        {
            string mysql = "select count(*) from Works where Wcid=" + Wcid + " and Wnum='" + Wnum +"'";
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 显示该学案该学号完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public int CountCidWorks(int Wcid, string Wnum)
        {
            string mysql = "select count(*) from Works where Wcid=" + Wcid + " and Wnum='" + Wnum + "'";
            return DbHelperSQL.FindNum(mysql);
        }
        /// <summary>
        /// 显示该学案该学号完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string HowCidWorks(int Wcid, int Wsid)
        {
            string mysql = "select count(*) from Works where Wcid=" + Wcid + " and Wsid=" + Wsid ;
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 显示该学案本班完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Snums"></param>
        /// <returns></returns>
        public string HowCourseWorks(int Wcid, string Snums)
        {
            if (Snums != "")
            {
                string mysql = "select count(*) from Works where Wcid=" + Wcid + " and Wnum in (" + Snums + ")";
                return DbHelperSQL.FindString(mysql);
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 显示该学案本班完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string HowCourseWorks(int Wcid, int Sgrade, int Sclass)
        {
            string mysql = "select count(*) from Works,Students where Wcid=" + Wcid + " and Sgrade=" + Sgrade + "and Sclass=" + Sclass + "and Wgrade=Sgrade and Wsid=Sid ";
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 显示该学案本任务本班完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public string HowWorks(int Syear,int Sgrade, int Sclass, int Wmid)
        {
            string mysql = "select count(*) from Works where Wmid=" + Wmid + " and Wyear=" + Syear + " and  Wgrade=" + Sgrade + " and Wclass=" + Sclass;
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 根据学号，获得本学案的作品列表，只返回Wid,Wmsort,Wtype,Wurl,Wscore,Wip,Wcheck,Wself,Wcan
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public DataSet MyHowWorks(int Wcid, string Wnum)
        {
            string mysql = "select Wid, Wmsort,Wtype,Wurl,Wscore,Wip,Wcheck,Wself,Wcan,Wgood,Wfscore  from Works where Wcid=" + Wcid + " and Wnum='" + Wnum + "'  order by Wmsort asc";
            return DbHelperSQL.Query(mysql);
        }

        /// <summary>
        /// 根据学号和活动mid，获得本活动的作品列表，只返回Wid,Wmsort,Wtype,Wurl,Wscore,Wip,Wcheck,Wself,Wcan
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public DataSet MyMissonWorks(int Wmid, string Wnum)
        {
            string mysql = "select Wid, Wmsort,Wtype,Wurl,Wscore,Wip,Wcheck,Wself,Wcan,Wgood,Wfscore,Wdscore  from Works where Wmid=" + Wmid + " and Wnum='" + Wnum + "'  order by Wmsort asc";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 获得该学案本任务本班完成作品
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataTable ShowMissionWorks(int Syear, int Sgrade, int Sclass, int Wmid)
        {
            string mysql = "select Wid,Wnum,Wname,Wtype,Wurl,Wvote,Wgood,Woffice,Wflash,Werror,Wfscore,Wdscore from Works where Wmid=" + Wmid + " and Wyear=" + Syear + " and  Wgrade=" + Sgrade + " and Wclass=" + Sclass + "  order by Wdate";
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int dtcount = dt.Rows.Count;
            if (dtcount > 0)
            {
                for (int i = 0; i < dtcount; i++)
                {
                    string Wnum = dt.Rows[i]["Wnum"].ToString();
                    if (Wnum.IndexOf('s') > -1)
                    {
                        dt.Rows[i].Delete();
                    }
                }
            }
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        /// 获得该学案本任务本班完成作品
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataTable ShowMissionWorksGroup(int Sgrade, int Sclass, int Wmid, int Sgroup)
        {
            if (Sgroup == 0)
            {
                string mysql = "select Wid,Wnum,Wname,Wtype,Wurl,Wvote,Wgood,Woffice,Wflash,Werror,Wfscore,Wdscore  from Works,Students where Wsid=Sid and  Wmid=" + Wmid + " and  Sgrade=" + Sgrade + " and Sclass=" + Sclass + " order by NEWID()";
                return DbHelperSQL.Query(mysql).Tables[0];
            }
            else
            {
                string mysql = "select Wid,Wnum,Wname,Wtype,Wurl,Wvote,Wgood,Woffice,Wflash,Werror,Wfscore,Wdscore  from Works,Students where Wsid=Sid and  Wmid=" + Wmid + " and  Sgrade=" + Sgrade + " and Sclass=" + Sclass + " and Sgroup=" + Sgroup + " order by NEWID()";
                return DbHelperSQL.Query(mysql).Tables[0];
            }
        }
        /// <summary>
        /// 查询今天本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl,Wvote,Wscore,Qwork</returns>
        public DataSet ShowTodayWorks(int Sgrade, int Sclass,int Wcid, int Wmid)
        {
            DateTime dt = DateTime.Now;
            string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            string mysql = "select distinct Wid,Sname,Wtype,Wurl,Wvote,Wscore,Wself,Wcan,Wgood,Qwork,Wcheck,Wlscore,Woffice,Wflash,Werror,Wfscore,Wdscore   from Works,Students,Signin where Qdate>'" + today + "' and Wnum=Qnum and  Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Wnum in ( select Snum from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass + ") order by Wid asc";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 查询本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl,Wvote,Wscore,Qwork</returns>
        public DataSet ShowClassWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            string mysql = "select distinct Wid,Sname,Wtype,Wurl,Wvote,Wscore,Wself,Wcan,Wgood,Wcheck,Wlscore,Woffice,Wflash,Werror,Wfscore,Wdscore   from Works,Students where  Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Wnum in ( select Snum from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass + ") order by Wid asc";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 查询本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl,Wvote,Wscore,Qwork</returns>
        public DataTable ShowClassWorksBySort(int Sgrade, int Sclass,int Wmid, string Sort)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Wid,Wnum,Wurl,Wscore,Wip,Wvote,Wcheck,Wself,Wcan,Wgood,Wtype,Wlscore,Wlemotion,Woffice,Wflash,Werror,Wfscore,Wdscore,Sname,Sgroup ");
            strSql.Append(" from Works,Students ");
            strSql.Append(" where Sgrade=@Sgrade and Sclass=@Sclass ");
            strSql.Append(" and Wsid=Sid and Wmid=@Wmid ");
            switch (Sort)
            {
                case "0":
                    strSql.Append(" ORDER BY Wid ASC ");//按时间
                    break;
                case "1":
                    strSql.Append("  ORDER BY Wnum ASC");//按学号
                    break;
                case "2":
                    strSql.Append("  ORDER BY Wip ASC");//按ＩＰ
                    break;
                case "3":
                    strSql.Append("  ORDER BY Sgroup ASC");//按小组
                    break;
                case "4":
                    strSql.Append("  ORDER BY Wvote ASC");//按投票
                    break;
            }
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),                    
					new SqlParameter("@Wmid", SqlDbType.Int,4)};
            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;
            parameters[2].Value = Wmid;

            DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];

            return dt;
        }

        /// <summary>
        /// 根据评分 查询本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl</returns>
        public DataTable ShowCircleScore(int Sgrade, int Sclass, int Wcid, int Wmid, int Score)
        {
            //string mysql = "select distinct Wid,Sname,Wurl,Wcheck from Works,Students where  Wgood=1 and Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Wnum in ( select Snum from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass + ") order by Wid asc";
            string mysql = "select distinct Wid,Sname,Wurl,Wcheck from Works,Students where  Wnum=Snum and Wscore=" + Score + " and Wcid=" + Wcid + " and Wmid=" + Wmid + " and  Sgrade=" + Sgrade + " and Sclass=" + Sclass + " order by Wid asc";
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int cn = dt.Rows.Count;
            if (cn > 0)
            {
                for (int i = 0; i < cn; i++)
                {
                    bool wcheck = bool.Parse(dt.Rows[i]["Wcheck"].ToString());
                    if (wcheck)
                        dt.Rows[i]["Sname"] = dt.Rows[i]["Sname"] + "√";
                }
            }
            return dt;
        }

        /// <summary>
        /// 查询本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl</returns>
        public DataTable ShowCircleGood(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            //string mysql = "select distinct Wid,Sname,Wurl,Wcheck from Works,Students where  Wgood=1 and Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Wnum in ( select Snum from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass + ") order by Wid asc";
            string mysql = "select distinct Wid,Sname,Wurl,Wcheck from Works,Students where  Wgood=1 and Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and  Sgrade=" + Sgrade + " and Sclass=" + Sclass + " order by Wid asc";
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int cn = dt.Rows.Count;
            if (cn > 0)
            {
                for (int i = 0; i < cn; i++)
                {
                    bool wcheck = bool.Parse(dt.Rows[i]["Wcheck"].ToString());
                    if (wcheck)
                        dt.Rows[i]["Sname"] = dt.Rows[i]["Sname"] + "√";
                }
            }
            return dt;
        }
        /// <summary>
        /// 查询本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl</returns>
        public DataTable ShowCircleWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            //string mysql = "select distinct Wid,Sname,Wurl,Wcheck from Works,Students where  Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Wnum in ( select Snum from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass + ") order by Wid asc";
            string mysql = "select distinct Wid,Sname,Wurl,Wcheck from Works,Students where  Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Sgrade=" + Sgrade + " and Sclass=" + Sclass + " order by Wid asc";
            
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int cn = dt.Rows.Count;
            if (cn > 0)
            {
                for (int i = 0; i < cn; i++)
                {
                    bool wcheck = bool.Parse(dt.Rows[i]["Wcheck"].ToString());
                    if (wcheck)
                        dt.Rows[i]["Sname"] = dt.Rows[i]["Sname"] + "√";
                }
            }
            return dt;
        }
        /// <summary>
        /// 查询本班本任务作品列表,office转flash专用
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public DataSet ShowClassFlashWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            string mysql = "select distinct Wid,Sname,Wurl from Works,Students where Woffice=1 and Wflash=1 and Werror=0 and  Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Wnum in ( select Snum from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass + ") order by Wid asc";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 查询本班本任务作品列表,office专用
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public DataSet ShowClassOfficeWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            string mysql = "select distinct Wid,Sname,Wurl from Works,Students where Woffice=1 and Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Wnum in ( select Snum from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass + ") order by Wid asc";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 查询本班本任务作品列表,Photo专用，包括jpg,png,bmp,gif
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public DataSet ShowClassPhotoWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            string mysql = "select distinct Wid,Sname,Wurl  from Works,Students where (Wtype='jpg' or Wtype='png' or Wtype='bmp' or Wtype='gif' or Wtype='psd') and  Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Wnum in ( select Snum from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass + ") order by Wid asc";
            return DbHelperSQL.Query(mysql);
        }

        /// <summary>
        /// 查询本班某类型的任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wtype">某类型如：doc</param>
        /// <returns></returns>
        public DataSet ShowClassWtypeWorks(int Sgrade, int Sclass, int Wcid, int Wmid,string Wtype)
        {
            string mysql = "select distinct Wid,Sname,Wurl  from Works,Students where Wtype='"+Wtype+"' and  Wnum=Snum and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Wnum in ( select Snum from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass + ") order by Wid asc";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 查询本班本任务未完成学生姓名列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataTable ShowClassNoWorks(int Syear, int Sgrade, int Sclass, int Wmid)
        {
            string mysql = "select Sid,Snum,Sname,Sscore from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass +" order by Snum asc";
            DataTable dtstu = DbHelperSQL.Query(mysql).Tables[0];
            string sqlstr = "select Wsid from Works where  Wmid=" + Wmid+" and Wyear=" + Syear + " and Wgrade= "+Sgrade+" and Wclass="+Sclass;
            DataTable haswork = DbHelperSQL.Query(sqlstr).Tables[0];
            int stucount = dtstu.Rows.Count;
            int hascount = haswork.Rows.Count;
            if (hascount > 0)
            {
                for (int i = 0; i < stucount; i++)
                {
                    string Sid = dtstu.Rows[i]["Sid"].ToString();
                    for (int j = 0; j < hascount; j++)
                    {
                        if (haswork.Rows[j]["Wsid"].ToString() == Sid)
                        {
                            dtstu.Rows[i].Delete();//学生表有作品的记录，删除掉
                        }                    
                    }                
                }
            }
            return dtstu;
        }
        /// <summary>
        /// 查询今天本班本任务未完成学生姓名列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataSet ShowTodayNotWorks(int Syear, int Sgrade, int Sclass, int Wmid)
        {
            string mysql = "select Snum,Sname,Sscore from Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass + " and  Snum not in ( select Wnum from Works where  Wyear=" + Syear + " and Wgrade=" + Sgrade + " and Wclass=" + Sclass + " and Wmid=" + Wmid + " ) order by Snum asc";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 查询今天本班本任务未完成学生姓名列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataSet ShowTodayNoWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            DateTime dt = DateTime.Now;
            string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            string mysql = "select Snum,Sname,Sscore from Students,Signin where   Sgrade=" + Sgrade + " and Sclass=" + Sclass + " and Snum=Qnum  and  Qdate>'" + today + "'  and  Snum not in ( select Wnum from Works where  Wcid=" + Wcid + " and Wmid=" + Wmid + " ) order by Snum asc";
            return DbHelperSQL.Query(mysql);
        }

        /// <summary>
        /// 查询今天本班学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string GetTodayCid(int Sgrade, int Sclass,int Syear)
        {
            string mysql = "select top 1 Wcid from  Works where  Wgrade=" + Sgrade + " and Wclass=" + Sclass + " and Wyear="+Syear+" order by Wid desc";
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 查询今天本班学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string GetTodayCidnew(int Sgrade, int Sclass)
        {
            DateTime dt = DateTime.Now;
            string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            string mysql = "select top 1 Wcid from  Works ,Students where  Wnum=Snum and  Wdate>'" + today + "' and Wnum in ( select Snum from Students where  Sgrade=" + Sgrade + " and Sclass=" + Sclass + ") order by Wid desc";
            return DbHelperSQL.FindString(mysql);
        }
       /// <summary>
        /// 查找本班本学案本任务本机Ip 已经完成的学号
       /// </summary>
       /// <param name="Sgrade"></param>
       /// <param name="Sclass"></param>
       /// <param name="Wcid"></param>
       /// <param name="Wmid"></param>
       /// <param name="Wip"></param>
       /// <returns></returns>
        public string IpWorkDoneSnum(int Sgrade, int Sclass, int Wcid, int Wmid,string Wip)
        {
            string mysql = "select top 1 Wnum from Works where Wcid="+Wcid +" and Wmid="+Wmid+" and Wip='"+Wip+"' and Wnum in (select Snum from Students where Sgrade="+Sgrade+" and Sclass="+Sclass+") order by Wdate desc";
            return DbHelperSQL.FindString(mysql);
        }


        /// <summary>
        /// 判断该学号本任务作品是否提交并记录,返回Wid的值
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public  string WorkDone(string Wnum, int Wcid, int Wmid)
        {
            string mysql = "Select Wid from Works Where Wnum='" + Wnum + "' and  Wcid=" + Wcid + " and Wmid=" + Wmid;
            return DbHelperSQL.FindString(mysql);

        }
        /// <summary>
        /// 根据学号和活动编号返回作品链接
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public string WorkUrl(string Wnum, int Wmid)
        {
            string mysql = "Select Wurl from Works Where Wnum='" + Wnum + "' and Wmid=" + Wmid;
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 判断该学号本任务作品是否提交评价
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public bool WorkDoneChecked(string Wnum, int Wcid, int Wmid)
        {
            string mysql = "Select count(1) from Works Where Wcheck=1 and Wnum='" + Wnum + "' and  Wcid=" + Wcid + " and Wmid=" + Wmid;
            return DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 检查该作品是否已经评分,是则返回真
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public  bool IsChecked(int Wid)
        {
            string mysql = "Select count(1) from Works Where Wcheck=1 and  Wid=" + Wid ;
            return DbHelperSQL.Exists(mysql);
        }

        /// <summary>
        /// 作品提交， 更新一条数据Wthumbnail='" + imgurl 
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wurl"></param>
        /// <param name="Wfilename"></param>
        /// <param name="Wlength"></param>
        /// <param name="Wdate"></param>
        /// <param name="Wcan"></param>
        public void UpdateWorkUp(int Wid,string Wurl,string Wfilename,int Wlength,DateTime Wdate,bool Wcan,string Wthumbnail)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Works set ");
            strSql.Append("Wurl=@Wurl,");
            strSql.Append("Wfilename=@Wfilename,");
            strSql.Append("Wlength=@Wlength,");
            strSql.Append("Wdate=@Wdate,");
            strSql.Append("Wcan=@Wcan,");
            strSql.Append("Wlemotion=1,");
            strSql.Append("Wflash=0,");
            strSql.Append("Werror=0,");
            strSql.Append("Wthumbnail=@Wthumbnail"); 
            strSql.Append(" where Wid=@Wid ");

            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4),
                    new SqlParameter("@Wurl", SqlDbType.NVarChar,200),
                    new SqlParameter("@Wfilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Wlength", SqlDbType.Int,4),
					new SqlParameter("@Wdate", SqlDbType.DateTime),
                    new SqlParameter("@Wcan", SqlDbType.Bit,1),
                    new SqlParameter("@Wthumbnail", SqlDbType.NVarChar,200)};
            parameters[0].Value = Wid;
            parameters[1].Value = Wurl;
            parameters[2].Value = Wfilename;
            parameters[3].Value = Wlength;
            parameters[4].Value = Wdate;
            parameters[5].Value = Wcan;
            parameters[6].Value = Wthumbnail;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 作品提交， 更新一条数据Wthumbnail='" + imgurl 
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wurl"></param>
        /// <param name="Wfilename"></param>
        /// <param name="Wlength"></param>
        /// <param name="Wdate"></param>
        /// <param name="Wcan"></param>
        public void UpdateWorkUp(int Wid, string Wurl, string Wfilename, int Wlength, DateTime Wdate, bool Wcan, string Wthumbnail,string Wtitle)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Works set ");
            strSql.Append("Wurl=@Wurl,");
            strSql.Append("Wfilename=@Wfilename,");
            strSql.Append("Wlength=@Wlength,");
            strSql.Append("Wdate=@Wdate,");
            strSql.Append("Wcan=@Wcan,");
            strSql.Append("Wlemotion=1,");
            strSql.Append("Wflash=0,");
            strSql.Append("Werror=0,");
            strSql.Append("Wthumbnail=@Wthumbnail,");
            strSql.Append("Wtitle=@Wtitle");
            strSql.Append(" where Wid=@Wid ");

            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4),
                    new SqlParameter("@Wurl", SqlDbType.NVarChar,200),
                    new SqlParameter("@Wfilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Wlength", SqlDbType.Int,4),
					new SqlParameter("@Wdate", SqlDbType.DateTime),
                    new SqlParameter("@Wcan", SqlDbType.Bit,1),
                    new SqlParameter("@Wthumbnail", SqlDbType.NVarChar,200),
                    new SqlParameter("@Wtitle", SqlDbType.NVarChar,200)};
            parameters[0].Value = Wid;
            parameters[1].Value = Wurl;
            parameters[2].Value = Wfilename;
            parameters[3].Value = Wlength;
            parameters[4].Value = Wdate;
            parameters[5].Value = Wcan;
            parameters[6].Value = Wthumbnail;
            parameters[7].Value = Wtitle;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        ///作品提交 增加一条数据
        ///(Wnum, Wcid,Wmid,Wmsort, Wfilename,Wtype, Wurl,Wlength, Wdate, Wip, Wtime)
        /// </summary>
        /// 
        public int AddWorkUp(LearnSite.Model.Works model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Works(");
            strSql.Append("Wnum,Wcid,Wmid,Wmsort,Wfilename,Wtype,Wurl,Wlength,Wdate,Wip,Wtime,Wegg,Wgrade,Wterm,Woffice,Wflash,Wsid,Wclass,Wname,Wyear,Wthumbnail,Wtitle)");
            strSql.Append(" values (");
            strSql.Append("@Wnum,@Wcid,@Wmid,@Wmsort,@Wfilename,@Wtype,@Wurl,@Wlength,@Wdate,@Wip,@Wtime,@Wegg,@Wgrade,@Wterm,@Woffice,@Wflash,@Wsid,@Wclass,@Wname,@Wyear,@Wthumbnail,@Wtitle)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Wcid", SqlDbType.Int,4),
					new SqlParameter("@Wmid", SqlDbType.Int,4),
					new SqlParameter("@Wmsort", SqlDbType.Int,4),
					new SqlParameter("@Wfilename", SqlDbType.NVarChar,50),
                    new SqlParameter("@Wtype", SqlDbType.NVarChar,50),
					new SqlParameter("@Wurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Wlength", SqlDbType.Int,4),
					new SqlParameter("@Wdate", SqlDbType.DateTime),
					new SqlParameter("@Wip", SqlDbType.NVarChar,50),
					new SqlParameter("@Wtime", SqlDbType.NVarChar,50),
                    new SqlParameter("@Wgrade", SqlDbType.Int,4),
					new SqlParameter("@Wterm", SqlDbType.Int,4),
					new SqlParameter("@Woffice", SqlDbType.Bit,1),
					new SqlParameter("@Wflash", SqlDbType.Bit,1),
                    new SqlParameter("@Wegg", SqlDbType.SmallInt,2),
                    new SqlParameter("@Wsid", SqlDbType.Int,4),
                    new SqlParameter("@Wclass", SqlDbType.Int,4),
					new SqlParameter("@Wname", SqlDbType.NVarChar,50),
                    new SqlParameter("@Wyear", SqlDbType.Int,4),
					new SqlParameter("@Wthumbnail", SqlDbType.NVarChar,200),
                    new SqlParameter("@Wtitle", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.Wnum;
            parameters[1].Value = model.Wcid;
            parameters[2].Value = model.Wmid;
            parameters[3].Value = model.Wmsort;
            parameters[4].Value = model.Wfilename;
            parameters[5].Value = model.Wtype;
            parameters[6].Value = model.Wurl;
            parameters[7].Value = model.Wlength;
            parameters[8].Value = model.Wdate;
            parameters[9].Value = model.Wip;
            parameters[10].Value = model.Wtime;
            parameters[11].Value = model.Wgrade;
            parameters[12].Value = model.Wterm;
            parameters[13].Value = model.Woffice;
            parameters[14].Value = model.Wflash;
            parameters[15].Value = model.Wegg;
            parameters[16].Value = model.Wsid;
            parameters[17].Value = model.Wclass;
            parameters[18].Value = model.Wname;
            parameters[19].Value = model.Wyear;
            parameters[20].Value = model.Wthumbnail;
            parameters[21].Value =model.Wtitle;

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
        /// 更新自我评价
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wself"></param>
        public void UpdateWself(int Wid,string Wself)
        {
            string mysql = "update Works set Wself=@Wself where Wid=@Wid ";
            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4),      
					new SqlParameter("@Wself", SqlDbType.NVarChar,200)};

            parameters[0].Value =Wid;
            parameters[1].Value =Wself;

            DbHelperSQL.ExecuteSql(mysql,parameters);        
        }
        /// <summary>
        /// 更新是否要求教师评价作品
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <param name="Wcan"></param>
        public void UpdateWcan(int Wmid, int Wnum, bool Wcan)
        {
            string mysql = "update Works set Wcan=@Wcan  where Wmid=@Wmid and Wnum=@Wnum";
            SqlParameter[] parameters = {
					new SqlParameter("@Wmid", SqlDbType.Int,4),                    
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Wcan", SqlDbType.Bit,1)};

            parameters[0].Value = Wmid;
            parameters[1].Value = Wnum;
            parameters[2].Value = Wcan;

            DbHelperSQL.ExecuteSql(mysql, parameters); 
        }
        /// <summary>
        /// 最佳作品推荐字段自动取反
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWgood(int Wid)
        {
            string mysql = "update Works set Wgood=abs(Wgood-1)  where Wid=@Wid ";
            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4)};

            parameters[0].Value = Wid;
            DbHelperSQL.ExecuteSql(mysql, parameters); 
        }
        /// <summary>
        /// 最佳作品推荐字段设置为真
        /// </summary>
        /// <param name="Wid"></param>
        public void WgoodBest(int Wid)
        {
            string mysql = "update Works set Wgood=1  where Wid=@Wid ";
            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4)};

            parameters[0].Value = Wid;
            DbHelperSQL.ExecuteSql(mysql, parameters); 
        }
        /// <summary>
        /// 最佳作品推荐字段设置为假
        /// </summary>
        /// <param name="Wid"></param>
        public void WgoodNormal(int Wid)
        {
            string mysql = "update Works set Wgood=0  where Wid=@Wid ";
            SqlParameter[] parameters = {
					new SqlParameter("@Wid", SqlDbType.Int,4)};

            parameters[0].Value = Wid;
            DbHelperSQL.ExecuteSql(mysql, parameters);
        }
        /// <summary>
        /// 获得该学案最佳作品列表Wid,Sname,Wurl,Wvote,Wgood
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public DataSet ShowCourseBestWorks(int Wcid,int Sgrade)
        {
            string mysql = "select Wid,Sname,Wtype,Wurl,Wvote,Wgood,Woffice,Wflash,Wfscore  from Works,Students where Wgood=1 and Wnum=Snum and Wcid=" + Wcid + " and Sgrade=" + Sgrade;
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 显示本学案未评数
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public string ShowNotCheckCounts(int Wcid, int Sgrade)
        {
            string mysql = "select count(*) from Works where Wcheck=0 and Wcid=" + Wcid + " and Wsid in ( select Sid from Students where Sgrade=" + Sgrade + ")";
            return DbHelperSQL.FindString(mysql);
        }

        /// <summary>
        /// 显示该学案本任务本班未评数
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public string ClassNotCheckWorks(int Wcid, int Sgrade, int Sclass, int Wmid)
        {
            string mysql = "select count(*) from Works where Wcheck=0 and Wcid=" + Wcid + " and Wmid=" + Wmid + " and Wnum in ( select Snum from Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass + ")";
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 获取今天作业的平均分
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public int GetTodayWorkScores(string Wnum)
        {
            int ncount = 0;
            int allscore = 0;
            DateTime tdate = DateTime.Parse(DateTime.Now.ToShortDateString());
            string sqlstr = "select count(*) from Mission where Mupload=1 and Mpublish=1 and Mdelete=0 and Mcid=(select top 1 Wcid from Works where Wnum='" + Wnum + "' and Wdate>'" + tdate + "')";
            string fcount = DbHelperSQL.FindString(sqlstr);
            if (fcount != "")
                ncount = Int32.Parse(fcount);

            string mysql = "select sum(Wscore)  from Works where Wnum='" + Wnum + "' and Wdate>'" + tdate + "'";
            string str = DbHelperSQL.FindString(mysql);
            if (str != "")
                allscore = Int32.Parse(str);

            if (ncount != 0 && allscore != 0)
            {
                double dd = allscore / ncount;
                return Convert.ToInt32(dd);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        ///教师未评时可给组内成员作品评分
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wlscore"></param>
        public void Updatelscore(int Wid, int Wlscore)
        {
            string mysql = "update Works set Wlscore="+Wlscore+" where Wcheck=0 and  Wid="+Wid;
            DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 给学生作品打分
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <param name="Wlscore"></param>
        public void Updatemscore(int Wmid, string Wnum, int Wlscore)
        {
            string mysql = "update Works set Wscore=" + Wlscore + ",Wgood=0,Wcheck=1 where  Wmid=" + Wmid + " and Wnum='" + Wnum + "'";
           
            if (Wlscore == 12)
            {
                mysql = "update Works set Wscore=" + Wlscore + ",Wgood=1,Wcheck=1 where  Wmid=" + Wmid + " and Wnum='" + Wnum + "'";
            }
           
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 给学生作品打分并评语
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <param name="Wlscore"></param>
        /// <param name="Wself"></param>
        public void Updatemscoreself(int Wmid, string Wnum, int Wlscore,string Wself,int Wdscore)
        {
            string mysql = "update Works set Wscore=" + Wlscore + ",Wself='"+Wself+"',Wgood=0,Wcheck=1,Wdscore="+Wdscore+" where  Wmid=" + Wmid + " and Wnum='" + Wnum + "'";

            if (Wlscore == 12)
            {
                mysql = "update Works set Wscore=" + Wlscore + ",Wself='" + Wself + "',Wgood=1,Wcheck=1,Wdscore=" + Wdscore + "  where  Wmid=" + Wmid + " and Wnum='" + Wnum + "'";
            }

            DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 给学生作品打分并评语
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <param name="Wlscore"></param>
        /// <param name="Wself"></param>
        /// <param name="Wdscore"></param>
        public void Updatemscoreself(string Wcid, string Wmid, string Wnum, int Wlscore, string Wself,int Wdscore)
        {
            string temp = " Wmid=" + Wmid;
            if (string.IsNullOrEmpty(Wmid))
                temp = " Wcid=" + Wcid;
            string mysql = "update Works set Wscore=" + Wlscore + ",Wself='" + Wself + "',Wgood=0,Wcheck=1,Wdscore=" + Wdscore + " where " + temp + " and Wnum='" + Wnum + "'";

            if (Wlscore == 12)
            {
                mysql = "update Works set Wscore=" + Wlscore + ",Wself='" + Wself + "',Wgood=1,Wcheck=1,Wdscore=" + Wdscore + " where " + temp + " and Wnum='" + Wnum + "'";
            }

            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 根据学号和任务ID返回成绩值
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string GetmyScore(int Wmid, string Wnum)
        {
            string mysql = "select Wscore from Works where Wmid="+Wmid+" and Wnum='"+Wnum+"'";
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 删除该学号该活动的作品
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        public void Delmywork(int Wmid, string Wnum)
        {
            string mysql = "delete Works where Wmid=" + Wmid + " and Wnum='" + Wnum + "'";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 根据学号和任务ID返回成绩值和评语
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string[] GetmyScoreWself(int Wmid, string Wnum)
        {
            string[] tem = { "", "","" };
            string mysql = "select Wscore,Wself,Wdscore from Works where Wcheck=1 and Wmid=" + Wmid + " and Wnum='" + Wnum + "'";
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Wscore"] != null)
                    tem[0] = dt.Rows[0]["Wscore"].ToString();
                if (dt.Rows[0]["Wself"] != null)
                    tem[1] = dt.Rows[0]["Wself"].ToString();
                if (dt.Rows[0]["Wdscore"] != null)
                    tem[2] = dt.Rows[0]["Wdscore"].ToString();
            }
            return tem;
        }

        /// <summary>
        /// 根据学号和任务ID返回成绩值和评语
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string[] GetmyScoreWself(string Cid, string Wmid, string Wnum)
        {
            string[] tem = { "", "","" };
            string mysql = "select Wscore,Wself,Wdscore from Works where Wcheck=1 and Wmid=" + Wmid + " and Wnum='" + Wnum + "'";
            if (string.IsNullOrEmpty(Wmid))
                mysql = "select Wscore,Wself,Wdscore from Works where Wcheck=1 and Wcid=" + Cid + " and Wnum='" + Wnum + "'";
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Wscore"] != null)
                    tem[0] = dt.Rows[0]["Wscore"].ToString();
                if (dt.Rows[0]["Wself"] != null)
                    tem[1] = dt.Rows[0]["Wself"].ToString();
                if (dt.Rows[0]["Wdscore"] != null)
                    tem[2] = dt.Rows[0]["Wdscore"].ToString();
            }
            return tem;
        }
        /// <summary>
        /// 获取本组内同学作品
        /// </summary>
        /// <param name="Sgroup"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataSet GetGroupWorks(int Sgroup, int Wmid)
        {
            string mysql = "select Wid,Wnum,Wurl,Wlscore,Wdscore,Sname from Works,Students where Wnum=Snum and Wmid=" + Wmid + " and Snum in (select Snum from Students where Sgroup=" + Sgroup + ")";
            return DbHelperSQL.Query(mysql);
        }

        /// <summary>
        /// 显示本学期我未学外的某课所有优秀作品
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Cobj"></param>
        /// <param name="Cid"></param>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public DataSet ShowAllGood(string Wnum, int Cobj, int Cid, int Sgrade)
        {
            int Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Wid,Wcid,Wmid,Wtype,Woffice,Wflash,Ctitle,Cobj,Cterm,Mtitle,Sname ");
            strSql.Append("FROM Works,Courses,Mission,Students ");
            strSql.Append("Where Wcid=@Cid and Wnum=Snum and Wcid=Cid and Wmid=Mid and Cobj=@Cobj and Wscore=12 and Wcid not in ");
            strSql.Append("(select Cid from Courses ");
            strSql.Append("where Cobj=@Sgrade and Cterm=@Cterm and Cid not in ");
            strSql.Append("(select Wcid from Works where Wnum=@Wnum))");
            SqlParameter[] parameters = {
					new SqlParameter("@Wnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Cobj", SqlDbType.Int,4),
					new SqlParameter("@Cid", SqlDbType.Int,4),
                    new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Cterm", SqlDbType.Int,4)};
            parameters[0].Value = Wnum;
            parameters[1].Value = Cobj;
            parameters[2].Value = Cid;
            parameters[3].Value = Sgrade;
            parameters[4].Value = Cterm;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        
        /// <summary>
        /// 显示该学案的所有优秀推荐作品
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public DataSet ShowCourseGoodWorks(int Wcid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Wid,Wtype,Wurl,Wname,Wgrade,Wclass ");
            strSql.Append("FROM Works ");
            strSql.Append("Where  Wcid=@Wcid and Wgood=1 ");
            strSql.Append("ORDER BY Wdate");
            SqlParameter[] parameters = {
					new SqlParameter("@Wcid", SqlDbType.Int,4)};

            parameters[0].Value = Wcid;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取Wurl
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public string GetWorkWurl(int Wid)
        {
            string strsql = "select top 1 Wurl from Works where Wid="+Wid;
            return DbHelperSQL.FindString(strsql);
        }
        /// <summary>
        /// 获取当前班级学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowDoneWorkCids(int Sgrade,int Sclass,int Wterm, int Wyear)
        {
            string mysql = "SELECT DISTINCT Wcid FROM Works where Wterm=" + Wterm + " and Wgrade=" + Sgrade + " and Wclass="+Sclass+" and Wyear=" + Wyear+" order by Wcid";
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string strtemp = "";
                for (int i = 0; i < n; i++)
                {
                    strtemp = strtemp + dt.Rows[i]["Wcid"].ToString() + ",";
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
        public string ShowStuDoneWorkCids(string Snum, int Wterm, int Wgrade)
        {
            string mysql = "SELECT DISTINCT Wcid FROM Works where Wnum='" + Snum + "' and Wterm=" + Wterm + " and Wgrade=" + Wgrade;
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string strtemp = "";
                for (int i = 0; i < n; i++)
                {
                    strtemp = strtemp + dt.Rows[i]["Wcid"].ToString() + ",";
                }
                return strtemp;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取最新的作品评语
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public string[] ShowLastWorkSelf(int Sid)
        {
            string[] tem = { "", "" };
            string mysql = "select top 1 Wid, Wself from Works where Wsid="+Sid+" order by Wid desc ";//2012-12-14修 by Wdate
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Wid"] != null)
                    tem[0] = dt.Rows[0]["Wid"].ToString();
                if (dt.Rows[0]["Wself"] != null)
                    tem[1] = dt.Rows[0]["Wself"].ToString();
            }
            return tem;
        }
        /// <summary>
        /// 获取该学生最新的作业列表8个记录
        /// </summary>
        /// <param name="Sid"></param>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public DataTable ShowLastWorks(int Sid,int Sgrade,int Term, int Cid)
        {
            string mysql = "select top 8 Ctitle,Cks,Mtitle,Wurl,Wscore from Courses,Mission,Works where Wsid=" + Sid + " and Wgrade=" + Sgrade + " and Wterm=" + Term + " and Wmid=Mid and Wcid=Cid and Wcid<>" + Cid + " order by Wid desc";
            return DbHelperSQL.Query(mysql).Tables[0];
        }

        public DataTable ShowWyears()
        {
            string mysql = "select distinct Wyear from Works where Wyear is not null order by Wyear asc";
            return DbHelperSQL.Query(mysql).Tables[0];
        }

        public DataTable ShowWgrades(int Wyear)
        {
            string mysql = "select distinct Wgrade from Works where Wyear=" + Wyear + " and Wgrade is not null order by Wgrade asc";
            return DbHelperSQL.Query(mysql).Tables[0];
        }

        public DataTable ShowWclass(int Wyear,int Wgrade)
        {
            string mysql = "select distinct Wclass from Works where  Wyear=" + Wyear + " and Wgrade=" + Wgrade + " and Wclass is not null order by Wclass asc";
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        public DataTable ShowWclassWcids(int Wyear, int Wgrade, int Wclass, int Wterm)
        {
            string mysql = "select distinct Wcid from Works where  Wyear=" + Wyear + " and Wgrade=" + Wgrade + " and Wclass=" + Wclass + " and Wterm=" + Wterm + " order by Wcid asc";
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        public DataTable ShowWclassWmids(int Wyear, int Wgrade, int Wclass, int Wterm,int Wcid)
        {
            string mysql = "select distinct Wmid from Works where  Wyear=" + Wyear + " and Wgrade=" + Wgrade + " and Wclass=" + Wclass + " and Wterm=" + Wterm + " and Wcid="+Wcid+" order by Wmid asc";
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        public DataTable ShowWclassWorks(int Wyear, int Wgrade,int Wclass,int Wterm,int Wmid)
        {
            string mysql = "select Wid,Wurl,Wname from Works where  Wyear=" + Wyear + " and Wgrade=" + Wgrade + " and Wclass=" + Wclass + " and Wterm=" + Wterm + " and Wmid="+Wmid+" order by Wscore desc";
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        /// 获取所有得分12的优秀作品列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetListGoodWorks()
        {
            string mysql = "select Wid,Wcid,Wmid,Wurl,Wname,Wgrade,Wclass,Wyear,Wtype,Wscore,Wdate,Ctitle from Works,Courses where Wscore=12 and (Wyear is not null) and Wtype<>'htm' and Wcid=Cid order by Wyear,Wcid,Wdate";
            return DbHelperSQL.Query(mysql).Tables[0];
        }

        public DataTable GetCourseWorks(int Wcid)
        {
            string mysql = "select Wid,Wurl,Wname,Wscore from Works where Wcid=" + Wcid + " order by Wid";
            return DbHelperSQL.Query(mysql).Tables[0];        
        }

        public string GetHtmMid(int Wcid, string Wnum)
        {
            string mysql = "select  Wmid from Works where Wcid=" + Wcid + " and Wnum='" + Wnum + "'";
            return DbHelperSQL.FindString(mysql);
        }

        public string  GetHtmCid(string Wnum)
        {
            string mysql = "select Wcid from Works where Wnum='" + Wnum + "' order by Wid desc";
            return DbHelperSQL.FindString(mysql);
        }

        public void initWdscore()
        {
            string mysql = "update Works set Wdscore=0 where Wdscore is null";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 标记缩略图
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wmid"></param>
        /// <param name="imgurl"></param>
        public void upWthumbnail(string Wnum, string Wmid, string imgurl)
        {
            string mysql = "update Works set Wthumbnail='" + imgurl + "'  where Wmid=" + Wmid + " and Wnum='" + Wnum + "'";
            DbHelperSQL.ExecuteSql(mysql);
        }

        public int GetSbCount()
        {
            string strWhere = " Wtype='sb2' and Wurl is not null and Wsid>0 ";
            return GetRecordCount(strWhere);
        }

        public DataTable GetSb(int page,int sbcount)
        {
            DateTime Weekday = DateTime.Now.AddDays(-12);
            string strWhere = " Wtype='sb2' and Wurl is not null and Wsid>0 and Wdate <'" + Weekday.ToShortDateString() + "' ";
            if (sbcount < 300)
                strWhere = " Wtype='sb2' and Wurl is not null and Wsid>0 ";
            string orderby = " Wdate desc,Whit desc ";
            int sindex = 12 * page + 1;
            int eindex = sindex + 11;

            return GetListByPage(strWhere, orderby, sindex, eindex).Tables[0];
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Works ");
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Wid,Wtype,Wscore,Wyear,Wgrade,Wclass,Wname,Wurl,Wthumbnail,Wtitle,Wdate,Whit FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Wid desc");
            }
            strSql.Append(")AS Row, T.*  from Works T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /////////////////////////////////////////////////////////////////////////////
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
            parameters[0].Value = "Works";
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
