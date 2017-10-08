using System;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类Students。
	/// </summary>
	public class Students
	{
		public Students()
		{}
		#region  成员方法
        /// <summary>
        /// 初始化中文拼音总计
        /// </summary>
        public void initSchinese()
        {
            string mysql = "update Students set Schinese=0 where Schinese is null";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 初始化表单分数总计
        /// </summary>
        public void initStxtform()
        {
            string mysql = "update Students set Stxtform=0 where Stxtform is null";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 初始化作业加分总计
        /// </summary>
        public void initSwdscore()
        {
            string mysql = "update Students set Swdscore=0 where Swdscore is null";
            DbHelperSQL.ExecuteSql(mysql);
        }
		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Sid", "Students"); 
		}

        public long maxGradeSnumInit(int Sgrade, int Sclass)
        {
            string strWhere = " Sgrade=" + Sgrade;
            string gsnum=DateTime.Now.Year.ToString()+Sgrade.ToString()+Sclass.ToString()+"001";
            long test = DbHelperSQL.FieldMaxValue("Snum", "Students", strWhere);
            if (test == 1)
                return long.Parse(gsnum);
            else
                return test + 1;
        }


        /// <summary>
        /// 得到最大Snum　；不取班级了
        /// </summary>
        public long GetMaxSnum(int Sgrade, int Sclass)
        {
            string strGradeWhere = " Sgrade=" + Sgrade;
            long maxGradeSnum = maxGradeSnumInit(Sgrade, Sclass);

            if (!ExistsSnum(maxGradeSnum.ToString()))
            {
                return maxGradeSnum;//不存在，则返回年级学号最大值
            }
            else
            {
                long tt = DbHelperSQL.FieldMaxValueNoWhere("Snum", "Students") + 1;
                return tt;//不存在，则返回全校学号最大值
            }
        }
        /// <summary>
        /// 是否存在该学号
        /// </summary>
        public bool ExistsSnum(string Snum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Students");
            strSql.Append(" where Snum=@Snum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Snum", SqlDbType.NVarChar,50)};
            parameters[0].Value = Snum;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Sid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Students");
			strSql.Append(" where Sid=@Sid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4)};
			parameters[0].Value = Sid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Students model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Students(");
            strSql.Append("Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape)");
            strSql.Append(" values (");
            strSql.Append("@Snum,@Syear,@Sgrade,@Sclass,@Sname,@Spwd,@Sex,@Saddress,@Sphone,@Sparents,@Sheadtheacher,@Sscore,@Squiz,@Sattitude,@Sape)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Snum", SqlDbType.NVarChar,50),
					new SqlParameter("@Syear", SqlDbType.Int,4),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),
					new SqlParameter("@Sname", SqlDbType.NVarChar,50),
					new SqlParameter("@Spwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NVarChar,2),
					new SqlParameter("@Saddress", SqlDbType.NVarChar,200),
					new SqlParameter("@Sphone", SqlDbType.NVarChar,50),
					new SqlParameter("@Sparents", SqlDbType.NVarChar,50),
					new SqlParameter("@Sheadtheacher", SqlDbType.NVarChar,50),
					new SqlParameter("@Sscore", SqlDbType.Int,4),
					new SqlParameter("@Squiz", SqlDbType.Int,4),
					new SqlParameter("@Sattitude", SqlDbType.Int,4),
					new SqlParameter("@Sape", SqlDbType.NVarChar,1)};
            parameters[0].Value = model.Snum;
            parameters[1].Value = model.Syear;
            parameters[2].Value = model.Sgrade;
            parameters[3].Value = model.Sclass;
            parameters[4].Value = model.Sname;
            parameters[5].Value = model.Spwd;
            parameters[6].Value = model.Sex;
            parameters[7].Value = model.Saddress;
            parameters[8].Value = model.Sphone;
            parameters[9].Value = model.Sparents;
            parameters[10].Value = model.Sheadtheacher;
            parameters[11].Value = model.Sscore;
            parameters[12].Value = model.Squiz;
            parameters[13].Value = model.Sattitude;
            parameters[14].Value = model.Sape;

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
        /// 增加一位学生
        /// </summary>
        public int AddStudent(LearnSite.Model.Students model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("if not exists(select Snum from Students where Snum=@Snum) ");
            strSql.Append("insert into Students(");
            strSql.Append("Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Sattitude)");
            strSql.Append(" values (");
            strSql.Append("@Snum,@Syear,@Sgrade,@Sclass,@Sname,@Spwd,@Sex,@Saddress,@Sphone,@Sparents,@Sheadtheacher,@Sscore,@Sattitude)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Snum", SqlDbType.NVarChar,50),
					new SqlParameter("@Syear", SqlDbType.Int,4),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),
					new SqlParameter("@Sname", SqlDbType.NVarChar,50),
					new SqlParameter("@Spwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NVarChar,2),
					new SqlParameter("@Saddress", SqlDbType.NVarChar,200),
					new SqlParameter("@Sphone", SqlDbType.NVarChar,50),
					new SqlParameter("@Sparents", SqlDbType.NVarChar,50),
					new SqlParameter("@Sheadtheacher", SqlDbType.NVarChar,50),
					new SqlParameter("@Sscore", SqlDbType.Int,4),
					new SqlParameter("@Sattitude", SqlDbType.Int,4)};
            parameters[0].Value = model.Snum;
            parameters[1].Value = model.Syear;
            parameters[2].Value = model.Sgrade;
            parameters[3].Value = model.Sclass;
            parameters[4].Value = model.Sname;
            parameters[5].Value = model.Spwd;
            parameters[6].Value = model.Sex;
            parameters[7].Value = model.Saddress;
            parameters[8].Value = model.Sphone;
            parameters[9].Value = model.Sparents;
            parameters[10].Value = model.Sheadtheacher;
            parameters[11].Value = model.Sscore;
            parameters[12].Value = model.Sattitude;

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
        /// 更新本班学生的密码
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        public void UpdateMyClassPwd(int Sgrade,int Sclass)
        {
            string mysql = "update Students set Spwd='12345'  where Sgrade=" + Sgrade + " and Sclass=" + Sclass;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 更新该学号学生的密码
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Spwd"></param>
        public void UpdatePwd(string Snum, string Spwd)
        {
            string mysql = "update Students set Spwd='" + Spwd + "'  where Snum='" + Snum + "'";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 更新该Sid学生的密码
        /// </summary>
        /// <param name="Sid"></param>
        /// <param name="Spwd"></param>
        public void UpdateSidPwd(string Sid, string Spwd)
        {
            string mysql = "update Students set Spwd='" + Spwd + "'  where Sid=" + Sid ;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 更新该学号学生的性别
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Sex"></param>
        public void UpdateSex(string Snum, string Sex)
        {
            string mysql = "update Students set Sex='" + Sex + "'  where Snum='" + Snum + "'";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 对该生换班
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sname"></param>
        /// <returns></returns>
        public bool UpdateDivide(int Sgrade, int Sclass, string Sname)
        {
            bool Okd = false;
            string sqlstr = "select count(*) from Students where Sgrade=" + Sgrade + " and Sname='" + Sname + "'";
            string fstr = DbHelperSQL.FindString(sqlstr);//查找该年级姓名学生人数
            int fcount = 0;
            if (fstr != "")
                fcount = Int32.Parse(fstr);
            if (fcount == 1)//如果刚好一位，则进行分班
            {
                string mysql = "update Students set Sclass=" + Sclass + " where Sgrade=" + Sgrade + " and Sname='" + Sname + "'";
                DbHelperSQL.ExecuteSql(mysql);
                Okd = true;
            }
            return Okd;
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Students model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Students set ");
			strSql.Append("Syear=@Syear,");
			strSql.Append("Sgrade=@Sgrade,");
			strSql.Append("Sclass=@Sclass,");
			strSql.Append("Sname=@Sname,");
			strSql.Append("Spwd=@Spwd,");
			strSql.Append("Sex=@Sex,");
			strSql.Append("Saddress=@Saddress,");
			strSql.Append("Sphone=@Sphone,");
			strSql.Append("Sparents=@Sparents,");
			strSql.Append("Sheadtheacher=@Sheadtheacher");
			strSql.Append(" where Sid=@Sid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4),
					new SqlParameter("@Syear", SqlDbType.Int,4),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),
					new SqlParameter("@Sname", SqlDbType.NVarChar,50),
					new SqlParameter("@Spwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NVarChar,2),
					new SqlParameter("@Saddress", SqlDbType.NVarChar,200),
					new SqlParameter("@Sphone", SqlDbType.NVarChar,50),
					new SqlParameter("@Sparents", SqlDbType.NVarChar,50),
					new SqlParameter("@Sheadtheacher", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Sid;
			parameters[1].Value = model.Syear;
			parameters[2].Value = model.Sgrade;
			parameters[3].Value = model.Sclass;
			parameters[4].Value = model.Sname;
			parameters[5].Value = model.Spwd;
			parameters[6].Value = model.Sex;
			parameters[7].Value = model.Saddress;
			parameters[8].Value = model.Sphone;
			parameters[9].Value = model.Sparents;
			parameters[10].Value = model.Sheadtheacher;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条学生数据（删除学生表，网页表中的同学号）
		/// </summary>
		public void Delete(int Sid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Students ");
			strSql.Append(" where Sid=@Sid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4)};
			parameters[0].Value = Sid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);

            string mysql = "delete from Webstudy where Wnum in (select Snum from Students where Sid=@Sid)";
            DbHelperSQL.ExecuteSql(mysql,parameters);
		}

        /// <summary>
        /// 根据年级和班级 随机得到该班某个学生一个对象实体
        /// </summary>
        public LearnSite.Model.Students GetRndModel(int Sgrade, int Sclass)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Sid,Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape from Students ");
            strSql.Append(" where Sgrade=@Sgrade and Sclass=@Sclass ");
            strSql.Append(" order by  NewID()");
            SqlParameter[] parameters = {
                    new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4)};
            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;

            LearnSite.Model.Students model = new LearnSite.Model.Students();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Sid"].ToString() != "")
                {
                    model.Sid = int.Parse(ds.Tables[0].Rows[0]["Sid"].ToString());
                }
                model.Snum = ds.Tables[0].Rows[0]["Snum"].ToString();
                if (ds.Tables[0].Rows[0]["Syear"].ToString() != "")
                {
                    model.Syear = int.Parse(ds.Tables[0].Rows[0]["Syear"].ToString());
                }

                if (ds.Tables[0].Rows[0]["Sgrade"].ToString() != "")
                {
                    model.Sgrade = int.Parse(ds.Tables[0].Rows[0]["Sgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sclass"].ToString() != "")
                {
                    model.Sclass = int.Parse(ds.Tables[0].Rows[0]["Sclass"].ToString());
                }
                model.Sname = ds.Tables[0].Rows[0]["Sname"].ToString();
                model.Spwd = ds.Tables[0].Rows[0]["Spwd"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                model.Saddress = ds.Tables[0].Rows[0]["Saddress"].ToString();
                model.Sphone = ds.Tables[0].Rows[0]["Sphone"].ToString();
                model.Sparents = ds.Tables[0].Rows[0]["Sparents"].ToString();
                model.Sheadtheacher = ds.Tables[0].Rows[0]["Sheadtheacher"].ToString();
                if (ds.Tables[0].Rows[0]["Sscore"].ToString() != "")
                {
                    model.Sscore = int.Parse(ds.Tables[0].Rows[0]["Sscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Squiz"].ToString() != "")
                {
                    model.Squiz = int.Parse(ds.Tables[0].Rows[0]["Squiz"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sattitude"].ToString() != "")
                {
                    model.Sattitude = int.Parse(ds.Tables[0].Rows[0]["Sattitude"].ToString());
                }
                model.Sape = ds.Tables[0].Rows[0]["Sape"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 根据学号和密码 得到学生一个对象实体
        /// </summary>
        public LearnSite.Model.Students GetStudentModel(string Snum,string Spwd)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Sid,Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape from Students ");
            strSql.Append(" where Snum=@Snum and Spwd=@Spwd ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Snum", SqlDbType.NVarChar,50),
                    new SqlParameter("@Spwd", SqlDbType.NVarChar,50)};
            parameters[0].Value = Snum;
            parameters[1].Value = Spwd;

            LearnSite.Model.Students model = new LearnSite.Model.Students();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Sid"].ToString() != "")
                {
                    model.Sid = int.Parse(ds.Tables[0].Rows[0]["Sid"].ToString());
                }
                model.Snum = ds.Tables[0].Rows[0]["Snum"].ToString();
                if (ds.Tables[0].Rows[0]["Syear"].ToString() != "")
                {
                    model.Syear = int.Parse(ds.Tables[0].Rows[0]["Syear"].ToString());
                }

                if (ds.Tables[0].Rows[0]["Sgrade"].ToString() != "")
                {
                    model.Sgrade = int.Parse(ds.Tables[0].Rows[0]["Sgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sclass"].ToString() != "")
                {
                    model.Sclass = int.Parse(ds.Tables[0].Rows[0]["Sclass"].ToString());
                }
                model.Sname = ds.Tables[0].Rows[0]["Sname"].ToString();
                model.Spwd = ds.Tables[0].Rows[0]["Spwd"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                model.Saddress = ds.Tables[0].Rows[0]["Saddress"].ToString();
                model.Sphone = ds.Tables[0].Rows[0]["Sphone"].ToString();
                model.Sparents = ds.Tables[0].Rows[0]["Sparents"].ToString();
                model.Sheadtheacher = ds.Tables[0].Rows[0]["Sheadtheacher"].ToString();
                if (ds.Tables[0].Rows[0]["Sscore"].ToString() != "")
                {
                    model.Sscore = int.Parse(ds.Tables[0].Rows[0]["Sscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Squiz"].ToString() != "")
                {
                    model.Squiz = int.Parse(ds.Tables[0].Rows[0]["Squiz"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sattitude"].ToString() != "")
                {
                    model.Sattitude = int.Parse(ds.Tables[0].Rows[0]["Sattitude"].ToString());
                }
                model.Sape = ds.Tables[0].Rows[0]["Sape"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据自动编号返回姓名
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public string GetSnameBySid(int Sid)
        {
            string mysql = "select Sname from Students where Sid="+Sid;
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 根据学号返回姓名
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public string GetSnameBySnum(string Snum)
        {
            string mysql = "select Sname from Students where Snum='" + Snum+"'";
            return DbHelperSQL.FindString(mysql);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Students GetModel(int Sid)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Sid,Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape,Swscore,Stscore,Sallscore,Spscore,Sgroup,Sleader,Svote,Sgscore from Students ");
            strSql.Append(" where Sid=@Sid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4)};
            parameters[0].Value = Sid;

            LearnSite.Model.Students model = new LearnSite.Model.Students();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Sid"].ToString() != "")
                {
                    model.Sid = int.Parse(ds.Tables[0].Rows[0]["Sid"].ToString());
                }
                model.Snum = ds.Tables[0].Rows[0]["Snum"].ToString();
                if (ds.Tables[0].Rows[0]["Syear"].ToString() != "")
                {
                    model.Syear = int.Parse(ds.Tables[0].Rows[0]["Syear"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sgrade"].ToString() != "")
                {
                    model.Sgrade = int.Parse(ds.Tables[0].Rows[0]["Sgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sclass"].ToString() != "")
                {
                    model.Sclass = int.Parse(ds.Tables[0].Rows[0]["Sclass"].ToString());
                }
                model.Sname = ds.Tables[0].Rows[0]["Sname"].ToString();
                model.Spwd = ds.Tables[0].Rows[0]["Spwd"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                model.Saddress = ds.Tables[0].Rows[0]["Saddress"].ToString();
                model.Sphone = ds.Tables[0].Rows[0]["Sphone"].ToString();
                model.Sparents = ds.Tables[0].Rows[0]["Sparents"].ToString();
                model.Sheadtheacher = ds.Tables[0].Rows[0]["Sheadtheacher"].ToString();
                if (ds.Tables[0].Rows[0]["Sscore"].ToString() != "")
                {
                    model.Sscore = int.Parse(ds.Tables[0].Rows[0]["Sscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Squiz"].ToString() != "")
                {
                    model.Squiz = int.Parse(ds.Tables[0].Rows[0]["Squiz"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sattitude"].ToString() != "")
                {
                    model.Sattitude = int.Parse(ds.Tables[0].Rows[0]["Sattitude"].ToString());
                }
                model.Sape = ds.Tables[0].Rows[0]["Sape"].ToString();
                if (ds.Tables[0].Rows[0]["Swscore"].ToString() != "")
                {
                    model.Swscore = int.Parse(ds.Tables[0].Rows[0]["Swscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Stscore"].ToString() != "")
                {
                    model.Stscore = int.Parse(ds.Tables[0].Rows[0]["Stscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sallscore"].ToString() != "")
                {
                    model.Sallscore = int.Parse(ds.Tables[0].Rows[0]["Sallscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Spscore"].ToString() != "")
                {
                    model.Spscore = int.Parse(ds.Tables[0].Rows[0]["Spscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sgroup"].ToString() != "")
                {
                    model.Sgroup = int.Parse(ds.Tables[0].Rows[0]["Sgroup"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sleader"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Sleader"].ToString() == "1") || (ds.Tables[0].Rows[0]["Sleader"].ToString().ToLower() == "true"))
                    {
                        model.Sleader = true;
                    }
                    else
                    {
                        model.Sleader = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Svote"].ToString() != "")
                {
                    model.Svote = int.Parse(ds.Tables[0].Rows[0]["Svote"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sgscore"].ToString() != "")
                {
                    model.Sgscore = int.Parse(ds.Tables[0].Rows[0]["Sgscore"].ToString());
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
        public LearnSite.Model.Students SnumGetModel(string Snum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Sid,Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape,Swscore,Stscore,Sallscore,Spscore,Sgroup,Sleader,Svote,Sgscore from Students");
            strSql.Append(" where Snum=@Snum ");
            SqlParameter[] parameters = {
					new SqlParameter("@Snum", SqlDbType.NVarChar,50)};
            parameters[0].Value = Snum;

            LearnSite.Model.Students model = new LearnSite.Model.Students();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Sid"].ToString() != "")
                {
                    model.Sid = int.Parse(ds.Tables[0].Rows[0]["Sid"].ToString());
                }
                model.Snum = ds.Tables[0].Rows[0]["Snum"].ToString();
                if (ds.Tables[0].Rows[0]["Syear"].ToString() != "")
                {
                    model.Syear = int.Parse(ds.Tables[0].Rows[0]["Syear"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sgrade"].ToString() != "")
                {
                    model.Sgrade = int.Parse(ds.Tables[0].Rows[0]["Sgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sclass"].ToString() != "")
                {
                    model.Sclass = int.Parse(ds.Tables[0].Rows[0]["Sclass"].ToString());
                }
                model.Sname = ds.Tables[0].Rows[0]["Sname"].ToString();
                model.Spwd = ds.Tables[0].Rows[0]["Spwd"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                model.Saddress = ds.Tables[0].Rows[0]["Saddress"].ToString();
                model.Sphone = ds.Tables[0].Rows[0]["Sphone"].ToString();
                model.Sparents = ds.Tables[0].Rows[0]["Sparents"].ToString();
                model.Sheadtheacher = ds.Tables[0].Rows[0]["Sheadtheacher"].ToString();
                if (ds.Tables[0].Rows[0]["Sscore"].ToString() != "")
                {
                    model.Sscore = int.Parse(ds.Tables[0].Rows[0]["Sscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Squiz"].ToString() != "")
                {
                    model.Squiz = int.Parse(ds.Tables[0].Rows[0]["Squiz"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sattitude"].ToString() != "")
                {
                    model.Sattitude = int.Parse(ds.Tables[0].Rows[0]["Sattitude"].ToString());
                }
                model.Sape = ds.Tables[0].Rows[0]["Sape"].ToString();
                if (ds.Tables[0].Rows[0]["Swscore"].ToString() != "")
                {
                    model.Swscore = int.Parse(ds.Tables[0].Rows[0]["Swscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Stscore"].ToString() != "")
                {
                    model.Stscore = int.Parse(ds.Tables[0].Rows[0]["Stscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sallscore"].ToString() != "")
                {
                    model.Sallscore = int.Parse(ds.Tables[0].Rows[0]["Sallscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Spscore"].ToString() != "")
                {
                    model.Spscore = int.Parse(ds.Tables[0].Rows[0]["Spscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sgroup"].ToString() != "")
                {
                    model.Sgroup = int.Parse(ds.Tables[0].Rows[0]["Sgroup"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sleader"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Sleader"].ToString() == "1") || (ds.Tables[0].Rows[0]["Sleader"].ToString().ToLower() == "true"))
                    {
                        model.Sleader = true;
                    }
                    else
                    {
                        model.Sleader = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Svote"].ToString() != "")
                {
                    model.Svote = int.Parse(ds.Tables[0].Rows[0]["Svote"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sgscore"].ToString() != "")
                {
                    model.Sgscore = int.Parse(ds.Tables[0].Rows[0]["Sgscore"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 学生表基本数据导出Excel
        /// </summary>
        public void StudentsToExcel()
        {
            DateTime dt = DateTime.Now;
            string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            string FileName = "StudentsToExcel" + today;
            string strSql = "select Snum as 学号,Syear as 入学年度, Sgrade as 年级,Sclass as 班级, Sname as 姓名,Spwd as 密码,Sex as 性别,Saddress as 家庭住址,Sphone as 联系电话,Sparents as 家长姓名,Sheadtheacher as 班主任  FROM Students order by Sgrade asc,Sclass asc,Snum asc  ";
            DataSet ds = DbHelperSQL.Query(strSql);
            Common.DataExcel.DataSetToExcel(ds, FileName);
        }

        /// <summary>
        /// 最终成绩评定导出Excel
        /// </summary>
         public void TermExcel()
        {
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"] != null)
            {
                string hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                DateTime dt = DateTime.Now;
                string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
                string FileName = "TermExcel" + today;
                string strSql = "select  Snum as 学号,(cast(Sgrade as nvarchar(2))+'.'+cast(Sclass as  nvarchar(2))) as 班级,Sname as 姓名,Sscore as 作品,Sgscore as 小组,Spscore as 讨论,Stxtform as 表单,Svscore as 调查,Swscore as 网页, Squiz as 测验,Schinese as 拼音,Sfscore as 英语,Stscore as 中文,Sattitude as 表现,Sallscore as 总分折算,Sape as 评定  FROM Students,Room where Sgrade=Rgrade and Sclass=Rclass and Rhid=" + hid + " order by Sgrade asc,Sclass asc,Snum asc  ";
                DataSet ds = DbHelperSQL.Query(strSql);
                Common.DataExcel.DataSetToExcel(ds, FileName);
            }
        }

        /// <summary>
        /// 批量更新所有学期总积分
        /// </summary>
        public void TeamScores()
        {
            string strSql = "SELECT Snum From Students ";
            DataSet ds = DbHelperSQL.GetDataSet(strSql);
            DataTable dt = ds.Tables[0];
            int counts = dt.Rows.Count;
            if (counts > 0)
            {
                for (int i = 0; i < counts; i++)
                {
                string  Snum = dt.Rows[i]["Snum"].ToString();
                string mysql = " UPDATE Students SET Sscore=(SELECT SUM(Wscore) FROM Works WHERE Wnum='" + Snum + "'" + ") FROM Students WHERE Snum='" + Snum + "'";
                DbHelperSQL.ExecuteSql(mysql);
                }
            }
        }
        /// <summary>
        /// 批量更新所教班级当前学期小组合作分
        /// </summary>
        public void ThisTeamGroupScores()
        {
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"] != null)
            {
                string hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                int Rhid = Int32.Parse(hid);
                string strSql = " select Snum,Sgrade from Students,Room where Sgrade=Rgrade and Sclass=Rclass  and Rhid=" + Rhid;
                DataSet ds = DbHelperSQL.GetDataSet(strSql);
                int counts = ds.Tables[0].Rows.Count;
                int Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
                if (counts > 0)
                {
                    for (int i = 0; i < counts; i++)
                    {
                        string Snum = ds.Tables[0].Rows[i]["Snum"].ToString();
                        if (ds.Tables[0].Rows[i]["Sgrade"].ToString() != "")
                        {
                            int Cobj = Int32.Parse(ds.Tables[0].Rows[i]["Sgrade"].ToString());
                            TotalmySgscore(Snum, Cterm, Cobj);                           
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 统计单个学生小组合作得分
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Cterm"></param>
        /// <param name="Cobj"></param>
        public void TotalmySgscore(string Snum, int Cterm, int Cobj)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Students SET Sgscore=");
            strSql.Append("(select ISNULL(sum(Gscore),0) from GroupWork WHERE ");
            strSql.Append("Ggrade=@Cobj  and Gterm=@Cterm  ");
            string str = "and Gstudents like '%" + Snum + "%' )";
            strSql.Append(str);
            strSql.Append(" where Snum=@Snum ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Snum", SqlDbType.NVarChar,50),
                    new SqlParameter("@Cterm", SqlDbType.Int,4),
					new SqlParameter("@Cobj", SqlDbType.Int,4)};

            parameters[0].Value = Snum;
            parameters[1].Value = Cterm;
            parameters[2].Value = Cobj;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 统计单个学生小组合作得分，这个有问题，统计不成功2013-4-11号发现
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Cterm"></param>
        /// <param name="Cobj"></param>
        public void TotalmySgscorenew(string Snum, int Cterm, int Cobj)
        {
            string mysql = "select Snum from Students where Sid=(select top 1 Sgroup from Students where Snum='" + Snum + "')";
            string leadSnum = DbHelperSQL.FindString(mysql);
            if (!string.IsNullOrEmpty(leadSnum))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("UPDATE Students SET Sgscore=");
                strSql.Append("(select ISNULL(sum(Gscore),0) from GroupWork WHERE Gnum=@leadSnum and Gstudents like '%@Snum%' ");
                strSql.Append(" and Ggrade=@Cobj  and Gterm=@Cterm ) ");
                strSql.Append(" where Snum=@Snum ");
                SqlParameter[] parameters = {
                    new SqlParameter("@Snum", SqlDbType.NVarChar,50),
                    new SqlParameter("@Cterm", SqlDbType.Int,4),
					new SqlParameter("@Cobj", SqlDbType.Int,4),
                    new SqlParameter("@leadSnum", SqlDbType.NVarChar,50)};

                parameters[0].Value = Snum;
                parameters[1].Value = Cterm;
                parameters[2].Value = Cobj;
                parameters[3].Value = leadSnum;
                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }
        }


        public void TotalSgscore(string gstudnets,int Cobj,int Cterm)
        {
            string[] gstu = gstudnets.Split('_');
            int gcount = gstu.Length;
            if (gcount > 0)
            {
                foreach (string stu in gstu)
                {
                    string sqlgw = "update Students set Sgscore=(select ISNULL(sum(Gscore),0) from GroupWork where Gterm=" + Cterm + " and Ggrade=" + Cobj + " and Gstudents like '%" + stu + "%' ) where Snum='" + stu + "'";
                    DbHelperSQL.ExecuteSql(sqlgw);//统计单个学生小组合作得分
                }
            }
        }     
        /// <summary>
        /// 批量更新所教班级当前学期作品总积分和表现总积分、调查测验分、表单得分//ISNULL  COALESCE
        /// </summary>
        public void ThisTeamScoresNew()
        {
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"] != null)
            {
                string hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                int Rhid = Int32.Parse(hid);
                string strSql = " select distinct Syear,Sgrade from Students,Room where Sgrade=Rgrade and Sclass=Rclass  and Rhid=" + Rhid;
                DataSet ds = DbHelperSQL.GetDataSet(strSql);
                int counts = ds.Tables[0].Rows.Count;
                int Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
                if (counts > 0)
                {
                    for (int i = 0; i < counts; i++)
                    {
                        if (ds.Tables[0].Rows[i]["Sgrade"].ToString() != "")
                        {
                            int Syear = Int32.Parse(ds.Tables[0].Rows[i]["Syear"].ToString());
                            int Sgrade = Int32.Parse(ds.Tables[0].Rows[i]["Sgrade"].ToString());
                            AllClassTeamScoresNew(Sgrade, Syear, Cterm);//调用批量
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 批量更新所有班级当前学期作品总积分和表现总积分、调查测验分、表单得分等（批量group方法）
        /// </summary>
        private void AllClassTeamScoresNew(int Sgrade, int Syear, int Cterm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Students set Sscore=nc.ws  from( SELECT Wnum, ISNULL(SUM(Wscore)+SUM(Wdscore),0)as ws FROM Works ");
            strSql.Append(" where Wyear=@Syear and Wcid in ( ");
            strSql.Append("select Cid from Courses where Cterm=@Cterm and Cobj=@Sgrade");
            strSql.Append(" ) group by Wnum )as nc ");
            strSql.Append(" where Snum=nc.Wnum and Sgrade=@Sgrade ");

            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Cterm", SqlDbType.Int,4),
                    new SqlParameter("@Syear", SqlDbType.Int,4)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Cterm;
            parameters[2].Value = Syear;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);//统计班级当前学期作品总积分

            StringBuilder mySql = new StringBuilder();
            mySql.Append("update Students set Sattitude=qt.sqe  from( SELECT Qnum, ISNULL(SUM(Qattitude),0)as sqe FROM Signin ");
            mySql.Append(" where Qsyear=@Syear and Qgrade=@Sgrade and Qterm=@Cterm  group by Qnum )as qt ");
            mySql.Append(" where Snum=qt.Qnum and Sgrade=@Sgrade ");

            DbHelperSQL.ExecuteSql(mySql.ToString(), parameters);//统计班级当前学期表现分

            StringBuilder mySqlv = new StringBuilder();
            mySqlv.Append("update Students set Svscore=sv.sqe  from( SELECT Fnum, ISNULL(SUM(Fscore),0)as sqe FROM SurveyFeedback ");
            mySqlv.Append(" where Fvtype=1 and Fyear=@Syear and Fgrade=@Sgrade and Fterm=@Cterm  group by Fnum )as sv ");
            mySqlv.Append(" where Snum=sv.Fnum and Sgrade=@Sgrade ");

            DbHelperSQL.ExecuteSql(mySqlv.ToString(), parameters);//统计班级当前学期调查分

            StringBuilder mySqlp = new StringBuilder();
            mySqlp.Append("update Students set Spscore=sp.sqe  from( SELECT Rsnum, ISNULL(SUM(Rscore),0)as sqe FROM TopicReply ");
            mySqlp.Append(" where Ryear=@Syear and Rgrade=@Sgrade and Rterm=@Cterm  group by Rsnum )as sp ");
            mySqlp.Append(" where Snum=sp.Rsnum and Sgrade=@Sgrade ");

            DbHelperSQL.ExecuteSql(mySqlp.ToString(), parameters);//统计班级当前学期主题讨论分

            StringBuilder mySqlf = new StringBuilder();
            mySqlf.Append("update Students set Stxtform=sp.sqe  from( SELECT Rsnum, ISNULL(SUM(Rscore),0)as sqe FROM TxtFormBack ");
            mySqlf.Append(" where Ryear=@Syear and Rgrade=@Sgrade and Rterm=@Cterm  group by Rsnum )as sp ");
            mySqlf.Append(" where Snum=sp.Rsnum and Sgrade=@Sgrade ");

            DbHelperSQL.ExecuteSql(mySqlf.ToString(), parameters);//统计班级当前学期表单得分

        }
        /// <summary>
        /// 批量更新该班级当前学期作品总积分和表现总积分（批量group方法）
        /// </summary>
        public void ThisClassTeamScoresNew(int Sgrade, int Sclass)
        {
            int Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
            int Syear = GetYear(Sgrade,Sclass);
            string mysqla = "update Students set Sscore=0,Sattitude=0 where Sgrade=" + Sgrade + " and Sclass=" + Sclass;
            DbHelperSQL.ExecuteSql(mysqla);//统计前将成绩清空
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Students set Sscore=nc.ws  from( SELECT Wnum, ISNULL(SUM(Wscore)+SUM(Wdscore),0)as ws FROM Works ");
            strSql.Append(" where Wyear=@Syear and Wcid in ( ");
            strSql.Append("select Cid from Courses where Cterm=@Cterm and Cobj=@Sgrade");
            strSql.Append(" ) group by Wnum )as nc ");
            strSql.Append(" where Snum=nc.Wnum and Sgrade=@Sgrade and Sclass=@Sclass ");

            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4),
                    new SqlParameter("@Cterm", SqlDbType.Int,4),
                    new SqlParameter("@Syear", SqlDbType.Int,4)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;
            parameters[2].Value = Cterm;
            parameters[3].Value = Syear;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);//统计班级当前学期作品总积分

            StringBuilder mySql = new StringBuilder();
            mySql.Append("update Students set Sattitude=qt.sqe  from( SELECT Qnum, ISNULL(SUM(Qattitude),0)as sqe FROM Signin ");
            mySql.Append(" where Qsyear=@Syear and Qgrade=@Sgrade and Qclass=@Sclass and Qterm=@Cterm  group by Qnum )as qt ");
            mySql.Append(" where Snum=qt.Qnum and Sgrade=@Sgrade and Sclass=@Sclass ");

            DbHelperSQL.ExecuteSql(mySql.ToString(), parameters);//

        }

        /// <summary>
        /// 统计单个学生主题讨论得分
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Cterm"></param>
        /// <param name="Cobj"></param>
        public void TotalmySpscore(string Snum, int Cterm, int Cobj)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Students SET Spscore=");
            strSql.Append("(select ISNULL(sum(Rscore),0) from TopicReply WHERE Rnum=@Snum ");
            strSql.Append(" and Rgrade=@Cobj  and Rterm=@Cterm ) ");
            strSql.Append(" where Snum=@Snum ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Snum", SqlDbType.NVarChar,50),
                    new SqlParameter("@Cterm", SqlDbType.Int,4),
					new SqlParameter("@Cobj", SqlDbType.Int,4)};

            parameters[0].Value = Snum;
            parameters[1].Value = Cterm;
            parameters[2].Value = Cobj;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 统计单个学生的调查测验分
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Cterm"></param>
        /// <param name="Cobj"></param>
        public void TotalmySvscore(string Snum, int Cterm, int Cobj)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Students SET Svscore=");
            strSql.Append("(select ISNULL(sum(Fscore),0) from SurveyFeedback WHERE Fvtype=1 and Fnum=@Snum ");
            strSql.Append(" and Fgrade=@Cobj  and Fterm=@Cterm ) ");
            strSql.Append(" where Snum=@Snum ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Snum", SqlDbType.NVarChar,50),
                    new SqlParameter("@Cterm", SqlDbType.Int,4),
					new SqlParameter("@Cobj", SqlDbType.Int,4)};

            parameters[0].Value = Snum;
            parameters[1].Value = Cterm;
            parameters[2].Value = Cobj;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 统计一个学生的表现分
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Cterm"></param>
        /// <param name="Cobj"></param>
        public void TotalmySattitude(string Snum, int Cterm, int Cobj)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Students SET Sattitude=");
            strSql.Append("(SELECT ISNULL(SUM(Qattitude),0) FROM Signin WHERE Qnum=@Snum ");
            strSql.Append(" and Qgrade=@Cobj  and Qterm=@Cterm ) ");
            strSql.Append(" where Snum=@Snum ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Snum", SqlDbType.NVarChar,50),
                    new SqlParameter("@Cterm", SqlDbType.Int,4),
					new SqlParameter("@Cobj", SqlDbType.Int,4)};

            parameters[0].Value = Snum;
            parameters[1].Value = Cterm;
            parameters[2].Value = Cobj;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 统计一个学生的作品分
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Cterm"></param>
        /// <param name="Cobj"></param>
        public void TotalmySscores(string Snum, int Cterm, int Cobj)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Students SET Sscore=");
            strSql.Append("(SELECT ISNULL(SUM(Wscore),0) FROM Works WHERE Wnum=@Snum and ");
            strSql.Append(" Wcid in (select Cid from Courses where Cterm=@Cterm and Cobj=@Cobj ))");
            strSql.Append(" FROM Students WHERE Snum=@Snum");
            SqlParameter[] parameters = {
                    new SqlParameter("@Snum", SqlDbType.NVarChar,50),
                    new SqlParameter("@Cterm", SqlDbType.Int,4),
					new SqlParameter("@Cobj", SqlDbType.Int,4)};

            parameters[0].Value = Snum;
            parameters[1].Value = Cterm;
            parameters[2].Value = Cobj;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 终结性评定
        /// </summary>
        /// <param name="perA"></param>
        /// <param name="perE"></param>
        public void TermAPE(int perA,int perE)
        {
            string strSql = "UPDATE Students SET Sape='P'";
            DbHelperSQL.ExecuteSql(strSql);

            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            int SgradeMin = rm.GetMinRgrade();
            int SgradeMax = rm.GetMaxRgrade();
            int SclassMin = rm.GetMinRclass();
            int SclassMax = rm.GetMaxRclass();
            for (int i = SgradeMin; i < SgradeMax+1; i++)
            {
                for (int j = SclassMin; j < SclassMax+1; j++)
                {
                    int Sgrade = i;
                    int Sclass = j;
                    int Scount=0;
                    string mysql = "SELECT MAX(Sallscore) From Students WHERE Sgrade=" + Sgrade + " AND Sclass=" + Sclass;
                    int Sallscore = DbHelperSQL.FindNum(mysql);

                    string strcount = "SELECT COUNT(*) From Students WHERE Sgrade=" + Sgrade + " AND Sclass=" + Sclass ;
                    object objcount= DbHelperSQL.GetSingle(strcount);
                    if (objcount != null)
                        Scount = Int32.Parse(objcount.ToString());
                    else
                    {
                        break;
                    }
                    int setA = Scount *perA/100;
                    //int setE = Scount * perE/100;
                    int EscoreLimit = Sallscore * perE / 100;
                    string strA = "UPDATE Students SET Sape='A' WHERE  Sid IN (SELECT TOP " + setA + " Sid FROM Students WHERE Sgrade='" + Sgrade + "' AND Sclass='" + Sclass + "' ORDER BY Sallscore DESC) ";
                    DbHelperSQL.ExecuteSql(strA);
                    //string strE = "UPDATE Students SET Sape='E' WHERE  Sid IN (SELECT TOP " + setE + " Sid FROM Students WHERE Sgrade='" + Sgrade + "' AND Sclass='" + Sclass + "' ORDER BY Sallscore ASC) ";
                    //DbHelperSQL.ExecuteSql(strE);
                    string strKill = "UPDATE Students SET Sape='E' WHERE Sallscore<" + EscoreLimit;//总分低于Ｅ的分值时就评为Ｅ
                    DbHelperSQL.ExecuteSql(strKill);
                }
            }
        }
        /// <summary>
        /// 获得作品总评数据列表
        /// </summary>
        public DataSet GetListTerm(int Sgrade, int Sclass)
		{
            StringBuilder strSql=new StringBuilder();
            strSql.Append("select Sid,Snum,(STR(Sgrade)+'.'+STR(Sclass)) as Sgradeclass,Sname,Sscore,Squiz,Sattitude,Swscore,Stscore,Sallscore,Sape,Spscore,Sgscore,Sfscore,Svscore,Stxtform,Schinese ");           
            strSql.Append(" FROM Students ");
            strSql.Append(" where Sgrade=@Sgrade and Sclass=@Sclass ");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;

            return DbHelperSQL.Query(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sid,Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape,Swscore,Stscore,Sallscore,Spscore,Sgroup,Sleader,Svote,Sgscore ");
            strSql.Append(" FROM Students ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得条件数据列表
        /// </summary>
        public DataSet GetSqlList(string strSql)
        {
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得单个学生数据
        /// </summary>
        public DataSet GetOneStudent(int Sid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sid,Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape,Swscore,Stscore,Sallscore,Spscore,Sgroup,Sleader,Svote,Sgscore ");
            strSql.Append(" FROM Students ");
            strSql.Append(" where Sid=@Sid");
            SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4)};

            parameters[0].Value = Sid;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得当前班级学生表现数据列表
        /// </summary>
        /// <param name="Syear"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataTable GetListSattitude(int Syear, int Sgrade, int Sclass)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sid,Snum,Sgrade,Sclass,Sname,Sattitude ");
            strSql.Append(" FROM Students ");
            strSql.Append(" where Syear=@Syear and Sgrade=@Sgrade and Sclass=@Sclass  order by Snum asc");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4),
                    new SqlParameter("@Syear", SqlDbType.Int,4)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;
            parameters[2].Value = Syear;

            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
        }
        /// <summary>
        /// 获得学生管理页数据列表
        /// </summary>
        public DataSet GetListStudents(int Sgrade,int Sclass)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sid,Snum,Spwd,Sgrade,Sclass,Sname,Sex,Sphone,Sscore,Squiz,Sattitude,Sleader,Sgroup ");
            strSql.Append(" FROM Students ");
            strSql.Append(" where Sgrade=@Sgrade and Sclass=@Sclass");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得本班学生学号和姓名数据列表
        /// </summary>
        public DataSet GetStudentsSnumSname(int Sgrade, int Sclass)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Snum,Sname");
            strSql.Append(" FROM Students ");
            strSql.Append(" where Sgrade=@Sgrade and Sclass=@Sclass");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
                    new SqlParameter("@Sclass", SqlDbType.Int,4)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得本年级所有学生列表
        /// </summary>
        public DataTable GetStudentsSnumSname(int Sgrade)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Snum,Sclass,Sname");
            strSql.Append(" FROM Students ");
            strSql.Append(" where Sgrade=@Sgrade order by Sclass asc,Snum asc");
            SqlParameter[] parameters = {
					new SqlParameter("@Sgrade", SqlDbType.Int,4)};
            parameters[0].Value = Sgrade;

            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
        }
        /// <summary>
        /// 获得全体学生的学年列表
        /// </summary>
        public DataSet GetAllYears()
        {
            string strSql = "select distinct Syear FROM Students order by Syear asc ";
            return DbHelperSQL.Query(strSql);
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
            strSql.Append(" Sid,Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape,Swscore,Stscore,Sallscore,Spscore,Sgroup,Sleader,Svote,Sgscore ");
            strSql.Append(" FROM Students ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 从学生表得到该年份的记录数
        /// </summary>
        /// <param name="Syear"></param>
        /// <returns></returns>
        public int FindCount(int Syear)
        {
            int fcount = 0;
            string strSql = "select count(*) from Students where Syear="+Syear;
            string findstr = DbHelperSQL.FindString(strSql);
            if (findstr != "")
            {
                fcount = Int32.Parse(findstr);
            }
            return fcount;
        }
        /// <summary>
        /// 学生表所有学生年级都升一级，并删除学生表中超过班级表最高年级+2的学生
        /// </summary>
        public void Upgrade()
        {
            string strSql = "update Students set Sgrade=Sgrade+1";
            DbHelperSQL.ExecuteSql(strSql);
            System.Threading.Thread.Sleep(1000);
            BLL.Room rbll = new BLL.Room();
            int maxGrade = rbll.GetMaxRgrade()+2;
            string mysql = "delete Students where Sgrade>" + maxGrade;
            DbHelperSQL.ExecuteSql(mysql);            
        }


        /// <summary>
        /// 获得该年级的入学年份
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string GetYear(int Sgrade)
        {
            string mysql = "select top 1  Syear from Students where Sgrade=" + Sgrade ;
            string getstr = DbHelperSQL.FindString(mysql);
            if (getstr == "")
                getstr = DateTime.Now.Year.ToString();
            return getstr;
        }
        /// <summary>
        /// 获得该年级的入学年份
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public int GetYear(int Sgrade,int Sclass)
        {
            string mysql = "select top 1 Syear from Students where Sgrade=" + Sgrade+" and Sclass="+Sclass;
            object aa = DbHelperSQL.GetSingle(mysql);
            if (aa != null)
            {
                return int.Parse(aa.ToString());
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据年级、班级获得姓名和学号
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet GetNameNum(int Sgrade, int Sclass)
        {
            string mysql = "select Snum,Sname from Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass;
            return DbHelperSQL.GetDataSet(mysql);      
        }

        /// <summary>
        /// 显示本年级积分最高的50条记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="GridViewscore"></param>
        public  DataSet ShowTopScore(int Sgrade)
        {
            string mysql = "Select top 30 Snum,Sgrade,Sclass,Sname,Sscore from Students where Sgrade=" + Sgrade + " and Sscore>0  ORDER BY Sscore DESC";
            return DbHelperSQL.GetDataSet(mysql);
        }
        /// <summary>
        /// 显示本班级所有积分记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet ShowMyclassScore(int Sgrade, int Sclass)
        {
            string mysql = "Select Sname,Sscore from Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass + " and Sscore>0  ORDER BY Sscore DESC";
            return DbHelperSQL.GetDataSet(mysql);        
        }
        /// <summary>
        /// 查询学生表的记录总数
        /// </summary>
        /// <returns></returns>
        public int GetCounts()
        {
           return DbHelperSQL.TableCounts("Students");
        }
        /// <summary>
        /// 更新该学号学生的测验成绩
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Squiz"></param>
        public void SetSquiz(int Rsid, int Squiz)
        {
            string mysql = "update Students set Squiz=" + Squiz + "  where Sid=" + Rsid ;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 获得本年级测验成绩最高的50条记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public DataSet TopGradeQuiz(int Sgrade)
        {
            string mysql = "Select top 50 Snum,(STR(Sgrade)+STR(Sclass)) as Sgradeclass,Sname,Squiz from Students where Sgrade=" + Sgrade + " and Squiz>0  ORDER BY Squiz DESC";
            return DbHelperSQL.GetDataSet(mysql);        
        }
        /// <summary>
        /// 获得本班级测验成绩所有记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet TopClassQuiz(int Sgrade, int Sclass)
        {
            string mysql = "Select  Snum,(STR(Sgrade)+STR(Sclass)) as Sgradeclass,Sname,Squiz from Students where Sgrade=" + Sgrade + " and Sclass="+Sclass+" and Squiz>0  ORDER BY Squiz DESC";
            return DbHelperSQL.GetDataSet(mysql);
        }
        /// <summary>
        /// 获得我的测验平均成绩
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public string MySquiz(string Snum)
        {
            string mysql = "select Squiz from Students where Snum='"+Snum+"'";
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 根据学号和班级密码，判断该学号是否存在
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Rpwd"></param>
        /// <returns></returns>
        public bool ExistsLogin(string Snum,string Rpwd)
        {
            string mysql = "select count(1) from Students,Room where Sgrade=Rgrade and Sclass=Rclass and Snum='"+Snum+"' and Rpwd='"+Rpwd+"'";
            return DbHelperSQL.Exists(mysql);        
        }
        /// <summary>
        /// 根据学号和个人密码，判断该学号是否存在
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Spwd"></param>
        /// <returns></returns>
        public bool ExistsLoginSelf(string Snum, string Spwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Students");
            strSql.Append(" where Snum=@Snum and Spwd=@Spwd");
            SqlParameter[] parameters = {
                    new SqlParameter("@Snum", SqlDbType.NVarChar,50),
					new SqlParameter("@Spwd", SqlDbType.NVarChar,50)};
            parameters[0].Value = Snum;
            parameters[1].Value = Spwd;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新学生表中网页制作成绩
        /// </summary>
        public void UpdateWebScore()
        {
            string mysql = "update Students set Students.Swscore= Webstudy.Wscore from Students,Webstudy where Snum=Wnum and Webstudy.Wscore is not null";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 登录账号教师所教班级按设定百分比计算总分
        /// </summary>
        /// <param name="persscore"></param>
        /// <param name="persquiz"></param>
        /// <param name="perswscore"></param>
        /// <param name="perstscore"></param>
        /// <param name="perattitude"></param>
        public void UpdateAllScore(int persscore,int persquiz,int perswscore,int perstscore,int perattitude ,int persurvey)
        {
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"] != null)
            {
                string hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                int Rhid = Int32.Parse(hid);
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Students set Sallscore= ");
                strSql.Append("(Sscore+Sgscore+Spscore+Stxtform)*@persscore/100+Svscore*@persurvey/100+Squiz*@persquiz/100+Sattitude*@perattitude/100+Swscore*@perswscore/100+Stscore*@perstscore/100+Sfscore+Schinese");
                string aa = " from Students,Room where Sgrade=Rgrade and Sclass=Rclass  and Rhid=@Rhid ";
                strSql.Append(aa);
                SqlParameter[] parameters = {					
					new SqlParameter("@persscore", SqlDbType.Int,4),
					new SqlParameter("@persquiz", SqlDbType.Int,4),
					new SqlParameter("@perswscore", SqlDbType.Int,4),
					new SqlParameter("@perstscore", SqlDbType.Int,4),
                    new SqlParameter("@perattitude", SqlDbType.Int,4),
                    new SqlParameter("@persurvey", SqlDbType.Int,4),
                    new SqlParameter("@Rhid", SqlDbType.Int,4)};

                parameters[0].Value = persscore;
                parameters[1].Value = persquiz;
                parameters[2].Value = perswscore;
                parameters[3].Value = perstscore;
                parameters[4].Value = perattitude;
                parameters[5].Value = persurvey;
                parameters[6].Value = Rhid;

                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }
        }
        /// <summary>
        /// 更新学生表的打字成绩
        /// </summary>
        public void UpdateStscore()
        {
            //string sqlstr = "update Students set Stscore=0";
            //DbHelperSQL.ExecuteSql(sqlstr);//先清空
            string nowterm = LearnSite.Common.XmlHelp.GetTerm();
            string mysql = "update Students set Stscore=Pdegree from Students,Ptyper where Snum=Psnum and Sgrade=Pgrade and Pterm=" + nowterm;
            DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 根据学号，更新班级
        /// </summary>
        /// <param name="Sclass"></param>
        /// <param name="Snum"></param>
        public void UpdateStuclass(int Sclass, string Snum)
        {
            string sqlstr = "update Students set Sclass="+Sclass+" where Snum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(sqlstr);
        }
        /// <summary>
        /// 根据教师自动编号Hid，返回所教班级的Syear,Sclass数据集
        /// </summary>
        /// <param name="Rhid"></param>
        /// <returns></returns>
        public DataSet TeacherSyearSclass(int hid)
        {
            string mysql = "select distinct Syear,Sgrade,Sclass from Students where Sclass in (select Rclass from Room where Rhid="+hid+")";
            return DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 将所教班级的学生密码如果为原初始化密码则更新转换为姓名拼音缩写（如果转换的不是字母或数字，则不更新密码）
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="Spwd"></param>
        public string SpwdToSpell(int hid,string Spwd)
        {
            string str = "";
            string mysql = "select Snum,Sname from Students,Room where Sgrade=Rgrade and Sclass=Rclass and Rhid="+hid+" and Spwd='"+Spwd+"'";
            DataSet ds = DbHelperSQL.Query(mysql);
            int counts = ds.Tables[0].Rows.Count;
            int right = 0;
            if (counts > 0)
            {
                for (int i = 0; i < counts; i++)
                {
                    string mySnum = ds.Tables[0].Rows[i]["Snum"].ToString();
                    string mySname = ds.Tables[0].Rows[i]["Sname"].ToString();
                    string spellname = Common.Gbk2Spell.Chinese.FirstLetter(mySname.Replace(" ", ""));//取姓名的拼音缩写为密码
                    if (LearnSite.Common.WordProcess.IsEnNum(spellname))
                    {
                        BLL.Students sbll = new BLL.Students();
                        sbll.UpdatePwd(mySnum, spellname);//如果缩写为字母或数字则更新
                        right++;
                    }
                }
            }
            str = "符合原初始化密码的所教学生总数为："+counts.ToString()+"位 转换成功："+right.ToString();
            return str;
        }
        /// <summary>
        /// 将所有学生的测验统计成绩更新为其测验最高分
        /// </summary>
        public void UpdateBestSquiz()
        {
            string mysql = "select Sid,Sgrade from Students ";
            DataSet ds = DbHelperSQL.Query(mysql);
            int counts = ds.Tables[0].Rows.Count;
            if (counts > 0)
            {
                BLL.Result rbll = new BLL.Result();
                string Rterm = LearnSite.Common.XmlHelp.GetTerm();

                for (int i = 0; i < counts; i++)
                {
                    int  mySid =Int32.Parse( ds.Tables[0].Rows[i]["Sid"].ToString());
                    string mySgrade = ds.Tables[0].Rows[i]["Sgrade"].ToString();
                    if (!string.IsNullOrEmpty(mySgrade))
                    {
                        int rscorebest = rbll.GetMax(mySid, mySgrade, Rterm);
                        SetSquiz(mySid, rscorebest);
                    }
                }
            }
        }
        /// <summary>
        /// 初始化Sleader值，数据库升级时用
        /// </summary>
        public void InitSleader()
        {
            string mysql = "update Students set Sleader=0 where Sleader is null";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 任命或卸任组长
        /// </summary>
        /// <param name="Snum"></param>
        public void ChangeSleader(int Sid)
        {
            string mysql = "update Students set Sleader=Sleader^1 where Sid=" + Sid;
            DbHelperSQL.ExecuteSql(mysql);

            string wrdsql = "update Students set Sgroup=null where Sgroup=" + Sid; ;//将本组的成员全退组（无论任命或卸任）
            DbHelperSQL.ExecuteSql(wrdsql);

            string strsql = "update Students set Sgroup=Sid where Sleader=1 and Sid=" + Sid;//更新组号为自动编号
            DbHelperSQL.ExecuteSql(strsql);

            string gtitlesql = "update Students set Sgtitle=Sname where Sleader=1 and Sgtitle is null and Sid=" + Sid;//更新组号为自动编号
            DbHelperSQL.ExecuteSql(gtitlesql);
        }
        /// <summary>
        /// 获取班级所有小组队长信息
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet ClassGroup(int Sgrade, int Sclass)
        {
            string mysql = "select Sid,Snum,Sname,Sgroup,Sgtitle from Students where Sleader=1 and Sgrade="+Sgrade+" and Sclass="+Sclass+" order by Snum asc";
            return DbHelperSQL.Query(mysql);        
        }
        /// <summary>
        /// 获取本小组成员名单
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sgroup"></param>
        /// <returns></returns>
        public string GroupMember(int Sgrade, int Sclass, int Sgroup)
        {
            string mysql = "select Sname from Students where Sleader=0 and Sgroup="+Sgroup+" and Sgrade=" + Sgrade + " and Sclass=" + Sclass + " order by Snum asc";
            DataSet ds = DbHelperSQL.Query(mysql);
            string str = "";
            int counts = ds.Tables[0].Rows.Count;
            if (counts > 0)
            {
                for (int i = 0; i < counts; i++)
                {
                    string Sname = ds.Tables[0].Rows[i]["Sname"].ToString();
                    str = str + Sname;
                    if (i < counts - 1)
                    {
                        str = str + "、";
                    }
                }
            }
            return str;
        }
        /// <summary>
        /// 根据学号获取同组成员的所有学号
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public string GroupSnum(string Snum)
        {
            string mysql = "select Snum from Students where Sgroup=(select top 1 Sgroup from Students where Snum='"+Snum+"')";
            DataSet ds = DbHelperSQL.Query(mysql);
            string str = "";
            int counts = ds.Tables[0].Rows.Count;
            if (counts > 0)
            {
                for (int i = 0; i < counts; i++)
                {
                    string Sname = ds.Tables[0].Rows[i]["Snum"].ToString();
                    str = str + Sname;
                    if (i < counts - 1)
                    {
                        str = str + "_";
                    }
                }
            }
            return str;
        }
        /// <summary>
        /// 更新该学号的小组号
        /// 如果原组长已卸任则加入新小组
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Sgroup"></param>
        public void AddThisGroup(string Snum, int Sgroup)
        {
            if (!FindLeader(Snum))//如果原组长已卸任则加入新小组
            {
                string mysql = "update Students set Sgroup=" + Sgroup + " where   Snum='" + Snum + "'";
                DbHelperSQL.ExecuteSql(mysql);
            }
        }
        public void AddThisGroup(int Sid, int Sgroup)
        {
            string mysql = "update Students set Sgroup=" + Sgroup + " where   Sid=" + Sid;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 获取本班本小组人数
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sgroup"></param>
        /// <returns></returns>
        public int GetGroupCount(int Sgrade, int Sclass, int Sgroup)
        {
            string mysql = "select count(*) from Students where Sgrade="+Sgrade+" and Sclass="+Sclass+" and Sgroup="+Sgroup;
            string str = DbHelperSQL.FindString(mysql);
            if (str == "")
                return 0;
            else
                return Int32.Parse(str);
        }
        /// <summary>
        /// 将该学号非组长同学退组
        /// </summary>
        /// <param name="Snum"></param>
        public void QuitThitGroup(string Snum)
        {
            string mysql = "update Students set Sgroup=null where Sleader=0 and Snum='"+Snum+"'";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 将该学号非组长同学退组
        /// </summary>
        /// <param name="Sid"></param>
        public void QuitThitGroup(int Sid)
        {
            string mysql = "update Students set Sgroup=null where Sleader=0 and Sid=" + Sid;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 寻索该组号的组长是否存在
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        private bool FindLeader(string Snum)
        {
            string mysql = "select count(1) from Students where Sleader=1 and Sid=( select top 1 Sgroup from Students where Snum='"+Snum+"')";
            return DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 是否组长
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public bool IsLeader(string Snum)
        {
            string mysql = "select count(1) from Students where Sleader=1 and Snum='" + Snum + "'";
            return DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 是否组长
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public bool IsLeaderSid(int Sid)
        {
            string mysql = "select count(1) from Students where Sleader=1 and Sid=" + Sid;
            return DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 获取小组名称
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public string GetSgtitle(int Sgroup)
        {
            string mysql = "select Sgtitle from Students where Sleader=1 and Sid=" + Sgroup;
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 更新小组名称
        /// </summary>
        /// <param name="Sid"></param>
        /// <param name="Sgtitle"></param>
        /// <returns></returns>
        public int UpdateSgtitle(int Sgroup, string Sgtitle)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Students SET Sgtitle=@Sgtitle");
            strSql.Append(" where Sgroup=@Sgroup ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Sgroup", SqlDbType.Int,4),
                    new SqlParameter("@Sgtitle", SqlDbType.NVarChar,50)};

            parameters[0].Value = Sgroup;
            parameters[1].Value = Sgtitle;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取该学号年级
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public int GetSgrade(string Snum)
        {
            string mysql = "select top 1 Sgrade from Students where  Snum='" + Snum + "'";
            string restr = DbHelperSQL.FindString(mysql);
            if (restr != "")
            {
                return Int32.Parse(restr);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据学号，修改姓名
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Sname"></param>
        /// <returns></returns>
        public int ChangeSname(string Snum, string Sname)
        {
            string mysql = "update Students set Sname='"+Sname+"' where Snum='"+Snum+"'";
            return DbHelperSQL.ExecuteSql(mysql);
        }

        public int InitSfscore()
        {
            string mysql = "update Students set Sfscore=0 where  Sfscore is null";
            return DbHelperSQL.ExecuteSql(mysql);
        }

        public int InitSvscore()
        {
            string mysql = "update Students set Svscore=0 where  Svscore is null";
            return DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 更新指法成绩
        /// </summary>
        /// <returns></returns>
        public int UpdateSfscore()
        {
            string nowterm = LearnSite.Common.XmlHelp.GetTerm();
            string mysql = "update Students set Sfscore=Pspd from Students,Pfinger where Snum=Psnum and Sgrade=Pgrade and Pterm="+nowterm;
            return DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 更新中文拼音成绩
        /// </summary>
        /// <returns></returns>
        public int UpdateSchinese()
        {
            string nowterm = LearnSite.Common.XmlHelp.GetTerm();
            string mysql = "update Students set Schinese=Ptotal/200  from Students,Pchinese where Snum=Psnum and Sgrade=Pgrade and Pterm=" + nowterm;
            return DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 统计前，先清空学生成绩
        /// </summary>
        public void ClearAllScores()
        {
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"] != null)
            {
                string hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                string strsql = "update Students set Sscore=0,Squiz=0,Sattitude=0,Sape='',Swscore=0,Stscore=0,Sallscore=0,Spscore=0,Sgscore=0,Sfscore=0,Svscore=0,Stxtform=0 from Students,Room where Sgrade=Rgrade and Sclass=Rclass  and Rhid=" + hid;
                DbHelperSQL.ExecuteSql(strsql);//先清空
            }
        }

        public string GetLeader(int Sid)
        {
            string mysql = "select Sname from Students where Sid=(select top 1 Sgroup from Students where Sid=" + Sid + ")";
            return DbHelperSQL.FindString(mysql);
        }
        public string GetLeaderByGroup(int Sgroup)
        {
            string mysql = "select Sname from Students where Sid=" + Sgroup;
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 解除分组
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public int NoGroup(int Sgrade, int Sclass)
        {
            string mysql = "update Students set Sgroup=null,Sleader=0 where  Sgrade=" + Sgrade + " and Sclass=" + Sclass;
            return DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 获取当前班级学号集合用,分隔
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowClassSnums(int Sgrade, int Sclass)
        {
            string mysql = "SELECT Snum FROM Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass ;
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string strtemp = "";
                for (int i = 0; i < n; i++)
                {
                    strtemp = strtemp +"'"+ dt.Rows[i]["Snum"].ToString() + "',";
                }
                if (strtemp.EndsWith(","))
                    strtemp = strtemp.Substring(0, strtemp.Length - 1);
                return strtemp;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取当前班级学生编号集合用,分隔
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowClassSids(int Sgrade, int Sclass)
        {
            string mysql = "SELECT Sid FROM Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass;
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string strtemp = "";
                for (int i = 0; i < n; i++)
                {
                    strtemp = strtemp + dt.Rows[i]["Sid"].ToString()+",";
                }
                if (strtemp.EndsWith(","))
                    strtemp = strtemp.Substring(0, strtemp.Length - 1);
                return strtemp;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取当前年级学生编号集合用,分隔
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public string ShowGradeSids(int Sgrade)
        {
            string mysql = "SELECT Sid FROM Students where Sgrade=" + Sgrade;
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string strtemp = "";
                for (int i = 0; i < n; i++)
                {
                    strtemp = strtemp + dt.Rows[i]["Sid"].ToString() + ",";
                }
                if (strtemp.EndsWith(","))
                    strtemp = strtemp.Substring(0, strtemp.Length - 1);
                return strtemp;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取该班级的人数
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public int CountClassMate(int Sgrade, int Sclass)
        {
            string mysql = "select count(*) from Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass;
            object ob = DbHelperSQL.GetSingle(mysql);
            if (ob != null)
                return Convert.ToInt32(ob);
            else
                return 0;//如果没有，则返回零
        }
        /// <summary>
        /// 清除该班级的所有学生记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public int DeleteClassMate(int Sgrade, int Sclass)
        {
            string mysql = "delete Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass;
            return DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 修正因某些原因引起的组长的组号为0的情况
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        private void FixGroupSid(int Sgrade, int Sclass)
        {
            string mysql = "update Students set Sgroup=Sid where Sleader=1 and Sgroup=0 and  Sgrade=" + Sgrade + " and Sclass=" + Sclass;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 汇总表小组统计
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="dttotal"></param>
        /// <returns></returns>
        public DataTable groupscores(int Sgrade, int Sclass, DataTable dttotal, int Gcid)
        {
            FixGroupSid(Sgrade, Sclass);
            string mysql = "select Sid,Snum,Sname,Sgtitle from Students where Sleader=1 and  Sgrade=" + Sgrade + " and Sclass=" + Sclass;
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            dt.Columns.Add("Sgscore", typeof(int));
            dt.Columns.Add("Svscore", typeof(float));
            dt.Columns.Add("Sgwork", typeof(int));
            dt.Columns.Add("Sgattitude", typeof(int));//小组表现分
            if (dttotal.Columns.Count > 2 && n > 2)
            {
                string sqlstr = "select Snum,Sgroup from Students where Sgroup>0 and  Sgrade=" + Sgrade + " and Sclass=" + Sclass;
                DataTable dmp = DbHelperSQL.Query(sqlstr).Tables[0];
                int p = dmp.Rows.Count;
                if (p > 0)
                {
                    dttotal.Columns["汇总"].ColumnName = "Stotals";
                    dttotal.Columns.Add("Sgroup", typeof(int));
                    GetSgroups(dttotal, dmp); //将汇总表新列赋值
                    LearnSite.BLL.GroupWork gbll = new BLL.GroupWork();
                    LearnSite.BLL.Signin sgbll = new BLL.Signin();
                    for (int i = 0; i < n; i++)
                    {
                        string sid = dt.Rows[i][0].ToString();
                        string snum = dt.Rows[i][1].ToString();
                        string fliter = "Sgroup=" + sid;
                        dt.Rows[i][4] = dttotal.Compute("sum(Stotals)", fliter).ToString();
                        double num = Convert.ToDouble(dttotal.Compute("avg(Stotals)", fliter).ToString());
                        dt.Rows[i][5] = num.ToString("0.0");
                        int sgroupnum = Int32.Parse(sid);
                        dt.Rows[i][6] = gbll.GetGscore(sgroupnum, Gcid);
                        dt.Rows[i][7] = sgbll.GetLeaderQgroup(sgroupnum, Gcid);
                    }
                }
                dmp.Dispose();
            }
            return dt;
        }
        private void GetSgroups(DataTable dttotal, DataTable dmp)
        {
            int scount = dttotal.Rows.Count;
            for (int i = 0; i < scount; i++)
            {
                string snum = dttotal.Rows[i]["学号"].ToString();
                int dcount = dmp.Rows.Count;
                for (int j = 0; j < dcount; j++)
                {
                    string xsnum = dmp.Rows[j]["Snum"].ToString();//第2张表获取的学号字段要重命名为Snum
                    string mygroup = dmp.Rows[j]["Sgroup"].ToString();//第2张表获取的分值字段要重命名为Sgroup
                    if (snum == xsnum)
                    {
                        dttotal.Rows[i]["Sgroup"] = mygroup;
                        break;
                    }
                }
            }         
        }
        /// <summary>
        /// 获取未参组班级内学生
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataTable NoGroupStudents(int Sgrade, int Sclass,string sort)
        {
            string mysql = "";
            switch (sort)
            { 
                case "0":
                    mysql = "select Sid,Snum,Sname,Sscore from Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass + " and (Sgroup =0 or Sgroup is null) order by Sscore desc";
                    break;
                case "1":
                    mysql = "select Sid,Snum,Sname,Sscore from Students where Sgrade=" + Sgrade + " and Sclass=" + Sclass + " and (Sgroup =0 or Sgroup is null) order by Snum desc";
                    break;            
            }
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        /// 获取本小组成员
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sgroup"></param>
        /// <returns></returns>
        public DataTable GroupMembers(int Sgrade, int Sclass, int Sgroup)
        {
            string mysql = "select Sid,Sname,Sscore from Students where Sleader=0 and Sgroup=" + Sgroup + " and Sgrade=" + Sgrade + " and Sclass=" + Sclass + " order by Sscore desc";
            return  DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        /// 获取本小组网盘成员（包括组长）Sid,Snum,Sname
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sgroup"></param>
        /// <returns></returns>
        public DataTable GroupDiskMembers(int Sgrade, int Sclass, int Sgroup)
        {
            string mysql = "select Sid,Snum,Sname from Students where  Sgroup=" + Sgroup + " and Sgrade=" + Sgrade + " and Sclass=" + Sclass + " order by Sid";
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        /// 获取本组内的作品平均分
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sgroup"></param>
        /// <returns></returns>
        public string MyGroupSscores(int Sgrade, int Sclass, int Sgroup)
        {
            string mysql = "select avg(Sscore) from Students where  Sgroup=" + Sgroup + " and Sgrade=" + Sgrade + " and Sclass=" + Sclass;
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 初始化小组名称为组长姓名
        /// </summary>
        /// <returns></returns>
        public int InitSgtitle()
        {
            string mysql = "update Students set Sgtitle=Sname where Sleader=1 and  Sgtitle is null";
            return DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 根据Sid获取组号
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public int GetSgroup(int Sid)
        {
            string mysql = "select Sgroup from Students where Sid=" + Sid;
            return DbHelperSQL.FindNum(mysql);
        }
        public void UpdateKaoxu(string kaoxu, string Sname)
        {
            string mysql = "update Students set Skaoxu='"+kaoxu+"' where Sname='"+Sname+"'";
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
			parameters[0].Value = "Students";
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

