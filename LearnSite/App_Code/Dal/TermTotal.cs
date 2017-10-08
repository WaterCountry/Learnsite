using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类TermTotal。
	/// </summary>
	public class TermTotal
	{
		public TermTotal()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Tid", "TermTotal"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Tid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TermTotal");
			strSql.Append(" where Tid=@Tid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Tid", SqlDbType.Int,4)};
			parameters[0].Value = Tid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 查询记录是否存在
        /// </summary>
        /// <param name="Tnum"></param>
        /// <param name="Tterm"></param>
        /// <param name="Tgrade"></param>
        /// <returns></returns>
        public bool ExistsTerm(string Tnum, int Tterm, int Tgrade)
        {
            string mysql = "select  count(1) from TermTotal where Tnum='" + Tnum + "' and Tterm=" + Tterm + " and Tgrade=" + Tgrade;
            return DbHelperSQL.Exists(mysql);      
        }
       /// <summary>
        /// 添加一条记录Tnum,Tterm,Tgrade,Tsid,Tyear,Tclass,Tname
       /// </summary>
       /// <param name="Tnum"></param>
       /// <param name="Tterm"></param>
       /// <param name="Tgrade"></param>
       /// <param name="Tsid"></param>
       /// <param name="Tyear"></param>
       /// <param name="Tclass"></param>
       /// <param name="Tname"></param>
        public void AddOne(string Tnum, int Tterm, int Tgrade, int Tsid,int Tyear,int Tclass,string Tname)
        {
            string mysql = "insert into TermTotal (Tnum,Tterm,Tgrade,Tsid,Tyear,Tclass,Tname) values('" + Tnum + "'," + Tterm + "," + Tgrade + "," + Tsid + "," + Tyear + "," + Tclass + ",'" +Tname+ "')";
            DbHelperSQL.ExecuteSql(mysql);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.TermTotal model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TermTotal(");
			strSql.Append("Tnum,Tterm,Tgrade,Tscore,Tgscore,Tquiz,Tattitude,Twscore,Ttscore,Tpscore,Tallscore,Tape)");
			strSql.Append(" values (");
			strSql.Append("@Tnum,@Tterm,@Tgrade,@Tscore,@Tgscore,@Tquiz,@Tattitude,@Twscore,@Ttscore,@Tpscore,@Tallscore,@Tape)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Tnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Tterm", SqlDbType.Int,4),
					new SqlParameter("@Tgrade", SqlDbType.Int,4),
					new SqlParameter("@Tscore", SqlDbType.Int,4),
					new SqlParameter("@Tgscore", SqlDbType.Int,4),
					new SqlParameter("@Tquiz", SqlDbType.Int,4),
					new SqlParameter("@Tattitude", SqlDbType.Int,4),
					new SqlParameter("@Twscore", SqlDbType.Int,4),
					new SqlParameter("@Ttscore", SqlDbType.Int,4),
					new SqlParameter("@Tpscore", SqlDbType.Int,4),
					new SqlParameter("@Tallscore", SqlDbType.Int,4),
					new SqlParameter("@Tape", SqlDbType.NVarChar,1)};
			parameters[0].Value = model.Tnum;
			parameters[1].Value = model.Tterm;
			parameters[2].Value = model.Tgrade;
			parameters[3].Value = model.Tscore;
			parameters[4].Value = model.Tgscore;
			parameters[5].Value = model.Tquiz;
			parameters[6].Value = model.Tattitude;
			parameters[7].Value = model.Twscore;
			parameters[8].Value = model.Ttscore;
			parameters[9].Value = model.Tpscore;
			parameters[10].Value = model.Tallscore;
			parameters[11].Value = model.Tape;

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
		public void Update(LearnSite.Model.TermTotal model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TermTotal set ");
			strSql.Append("Tnum=@Tnum,");
			strSql.Append("Tterm=@Tterm,");
			strSql.Append("Tgrade=@Tgrade,");
			strSql.Append("Tscore=@Tscore,");
			strSql.Append("Tgscore=@Tgscore,");
			strSql.Append("Tquiz=@Tquiz,");
			strSql.Append("Tattitude=@Tattitude,");
			strSql.Append("Twscore=@Twscore,");
			strSql.Append("Ttscore=@Ttscore,");
			strSql.Append("Tpscore=@Tpscore,");
			strSql.Append("Tallscore=@Tallscore,");
			strSql.Append("Tape=@Tape");
			strSql.Append(" where Tid=@Tid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Tid", SqlDbType.Int,4),
					new SqlParameter("@Tnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Tterm", SqlDbType.Int,4),
					new SqlParameter("@Tgrade", SqlDbType.Int,4),
					new SqlParameter("@Tscore", SqlDbType.Int,4),
					new SqlParameter("@Tgscore", SqlDbType.Int,4),
					new SqlParameter("@Tquiz", SqlDbType.Int,4),
					new SqlParameter("@Tattitude", SqlDbType.Int,4),
					new SqlParameter("@Twscore", SqlDbType.Int,4),
					new SqlParameter("@Ttscore", SqlDbType.Int,4),
					new SqlParameter("@Tpscore", SqlDbType.Int,4),
					new SqlParameter("@Tallscore", SqlDbType.Int,4),
					new SqlParameter("@Tape", SqlDbType.NVarChar,1)};
			parameters[0].Value = model.Tid;
			parameters[1].Value = model.Tnum;
			parameters[2].Value = model.Tterm;
			parameters[3].Value = model.Tgrade;
			parameters[4].Value = model.Tscore;
			parameters[5].Value = model.Tgscore;
			parameters[6].Value = model.Tquiz;
			parameters[7].Value = model.Tattitude;
			parameters[8].Value = model.Twscore;
			parameters[9].Value = model.Ttscore;
			parameters[10].Value = model.Tpscore;
			parameters[11].Value = model.Tallscore;
			parameters[12].Value = model.Tape;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Tid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TermTotal ");
			strSql.Append(" where Tid=@Tid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Tid", SqlDbType.Int,4)};
			parameters[0].Value = Tid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.TermTotal GetModel(int Tid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Tid,Tnum,Tterm,Tgrade,Tscore,Tgscore,Tquiz,Tattitude,Twscore,Ttscore,Tpscore,Tallscore,Tape from TermTotal ");
			strSql.Append(" where Tid=@Tid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Tid", SqlDbType.Int,4)};
			parameters[0].Value = Tid;

			LearnSite.Model.TermTotal model=new LearnSite.Model.TermTotal();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Tid"].ToString()!="")
				{
					model.Tid=int.Parse(ds.Tables[0].Rows[0]["Tid"].ToString());
				}
				model.Tnum=ds.Tables[0].Rows[0]["Tnum"].ToString();
				if(ds.Tables[0].Rows[0]["Tterm"].ToString()!="")
				{
					model.Tterm=int.Parse(ds.Tables[0].Rows[0]["Tterm"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tgrade"].ToString()!="")
				{
					model.Tgrade=int.Parse(ds.Tables[0].Rows[0]["Tgrade"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tscore"].ToString()!="")
				{
					model.Tscore=int.Parse(ds.Tables[0].Rows[0]["Tscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tgscore"].ToString()!="")
				{
					model.Tgscore=int.Parse(ds.Tables[0].Rows[0]["Tgscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tquiz"].ToString()!="")
				{
					model.Tquiz=int.Parse(ds.Tables[0].Rows[0]["Tquiz"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tattitude"].ToString()!="")
				{
					model.Tattitude=int.Parse(ds.Tables[0].Rows[0]["Tattitude"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Twscore"].ToString()!="")
				{
					model.Twscore=int.Parse(ds.Tables[0].Rows[0]["Twscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ttscore"].ToString()!="")
				{
					model.Ttscore=int.Parse(ds.Tables[0].Rows[0]["Ttscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tpscore"].ToString()!="")
				{
					model.Tpscore=int.Parse(ds.Tables[0].Rows[0]["Tpscore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tallscore"].ToString()!="")
				{
					model.Tallscore=int.Parse(ds.Tables[0].Rows[0]["Tallscore"].ToString());
				}
				model.Tape=ds.Tables[0].Rows[0]["Tape"].ToString();
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
			strSql.Append("select Tid,Tnum,Tterm,Tgrade,Tscore,Tgscore,Tquiz,Tattitude,Twscore,Ttscore,Tpscore,Tallscore,Tape ");
			strSql.Append(" FROM TermTotal ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 根据学号获取各学期成绩单
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public DataSet GetSnumList(string Snum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Tid,Tnum,Tterm,Tgrade,Tscore,Tgscore,Tquiz,Tattitude,Twscore,Ttscore,Tpscore,Tallscore,Tvscore,Tape,Sname ");
            strSql.Append(" FROM TermTotal,Students ");
            string strWhere = " Tnum=Snum and Tnum='"+Snum+"' order by Tgrade asc,Tterm asc";
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
			strSql.Append(" Tid,Tnum,Tterm,Tgrade,Tscore,Tgscore,Tquiz,Tattitude,Twscore,Ttscore,Tpscore,Tallscore,Tape ");
			strSql.Append(" FROM TermTotal ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 生成学期统计表
        /// </summary>
        public void TermScore()
        {
            string strSql = " select Sid,Snum,Sname,Syear,Sgrade,Sclass from Students ";
            DataSet ds = DbHelperSQL.GetDataSet(strSql);
            int counts = ds.Tables[0].Rows.Count;
            int Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());//获取当前学期值
            if (counts > 0)
            {
                for (int i = 0; i < counts; i++)
                {
                    string Snum = ds.Tables[0].Rows[i]["Snum"].ToString();
                    if (ds.Tables[0].Rows[i]["Sgrade"].ToString() != "" && ds.Tables[0].Rows[i]["Syear"].ToString() != "")
                    {
                        int Sid = Int32.Parse(ds.Tables[0].Rows[i]["Sid"].ToString());
                        int Syear = Int32.Parse(ds.Tables[0].Rows[i]["Syear"].ToString());
                        int Sgrade = Int32.Parse(ds.Tables[0].Rows[i]["Sgrade"].ToString());
                        int Sclass = Int32.Parse(ds.Tables[0].Rows[i]["Sclass"].ToString());
                        string Sname = ds.Tables[0].Rows[i]["Sname"].ToString();
                        if (!ExistsTerm(Snum, Cterm, Sgrade))
                        {
                            AddOne(Snum, Cterm, Sgrade, Sid, Syear, Sclass, Sname);//本学期本年级学号，如果不存在则添加
                        }
                    }
                }
            }

            string mysql = "update TermTotal set Tscore=Sscore,Tgscore=Sgscore,Tquiz=Squiz,Tattitude=Sattitude,Twscore=Swscore,Ttscore=Stscore,Tpscore=Spscore,Tallscore=Sallscore,Tape=Sape,Tfscore=Sfscore,Tvscore=Svscore,Ttxtform=Stxtform,Tchinese=Schinese  from Students where Tnum=Snum and Tgrade=Sgrade and Tterm="+Cterm;
            DbHelperSQL.ExecuteSql(mysql);//同步更新学生表数据到学期表
        }
        /// <summary>
        /// 获取该入学年度学生，Tnum,Tclass,Tname排序order by Tclass asc,Tnum asc
        /// </summary>
        /// <param name="Tyear"></param>
        /// <returns></returns>
        public DataTable GetTyearStu(int Tyear)
        {
            //Tnum,Tclass,Tname
            string mysql = "select distinct Tnum,Tclass,Tname from TermTotal where Tyear=" + Tyear + " order by Tclass asc,Tnum asc ";// and  Tclass is not null
            return DbHelperSQL.Query(mysql).Tables[0];
        }

        public DataTable GetTyearStuTwo(int Syear)
        {
            string mysql = "select Skaoxu,Snum,Sclass,Sname from Students where Syear=" + Syear + " order by Sclass asc,Snum asc ";// and  Tclass is not null
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        ///  获取当前班级在各年级各学期的期末成绩
        /// </summary>
        /// <param name="Tyear"></param>
        /// <param name="Tgrade"></param>
        /// <param name="Tclass"></param>
        /// <param name="Tterm"></param>
        /// <returns></returns>
        public DataSet GetGradeTermScore(int Tyear, int Tgrade, int Tclass, int Tterm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Tid,Tnum,Tterm,Tgrade,Tscore,Tgscore,Tquiz,Tattitude,Twscore,Ttscore,Tpscore,Tallscore,Tape,Tfscore,Tvscore,Tyear,Tclass,Tname,Ttxtform,Tchinese ");
            strSql.Append(" FROM TermTotal ");
            strSql.Append(" where Tyear=@Tyear and Tgrade=@Tgrade and Tclass=@Tclass and Tterm=@Tterm ");

            SqlParameter[] parameters = {
					new SqlParameter("@Tyear", SqlDbType.Int,4),
                    new SqlParameter("@Tgrade", SqlDbType.Int,4),
                    new SqlParameter("@Tclass", SqlDbType.Int,4),
                    new SqlParameter("@Tterm", SqlDbType.Int,4)};
            parameters[0].Value = Tyear;
            parameters[1].Value = Tgrade;
            parameters[2].Value = Tclass;
            parameters[3].Value = Tterm;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取本年级所有学期存档成绩，按Tnum排序
        /// </summary>
        /// <param name="Tyear">入学年度</param>
        /// <returns></returns>
        public DataTable GetGradeAllScores(int Tyear)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Tnum,Tgrade,Tterm,Tape,Tname");
            strSql.Append(" FROM TermTotal ");
            strSql.Append(" where  Tyear=@Tyear order by Tnum ");

            SqlParameter[] parameters = {
					new SqlParameter("@Tyear", SqlDbType.Int,4)};
            parameters[0].Value = Tyear;

            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0]; 
        }
        /// <summary>
        /// 导出该年级在某年级时某学期的成绩评定
        /// </summary>
        /// <param name="Tyear"></param>
        /// <param name="Tgrade"></param>
        /// <param name="Tterm"></param>
        public void TotalTermExcel(int Tyear,int Tgrade,int Tterm)
        {
            DateTime dt = DateTime.Now;
            string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            string FileName =Tyear.ToString()+ "-grade"+Tgrade.ToString()+"-term"+Tterm.ToString()+"-"+ today;
            string strSql = "select  Tnum as 学号,Tgrade as 年级,Tclass as 班级,Tterm as 学期,Tname as 姓名,Tscore as 作品,Tgscore as 小组,Tpscore as 讨论,Ttxtform as 表单,Tvscore as 调查,Twscore as 网页,Tquiz as 测验,Tchinese as 拼音,Tfscore as 英语,Ttscore as 中文,Tattitude as 表现,Tallscore as 总分折算,Tape as 评定等级,Tyear as 入学年度 FROM TermTotal where  Tyear="+Tyear+" and Tgrade="+Tgrade+" and Tterm="+Tterm+" order by Tclass asc,Tnum asc  ";
            DataSet ds = DbHelperSQL.Query(strSql);
            Common.DataExcel.DataSetToExcel(ds, FileName);
        }
        /// <summary>
        /// 初始化新增字段TyearTclassTname
        /// </summary>
        /// <returns></returns>
        public int initTyearTclassTname()
        {
            string mysql = "update TermTotal set Tyear=Syear,Tclass=Sclass,Tname=Sname from TermTotal,Students where Tnum=Snum";
            return DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 获取入学年度列表
        /// </summary>
        /// <returns></returns>
        public DataTable TyearList()
        {
            string mysql = "select distinct Tyear from TermTotal where Tyear is not null order by Tyear desc";
            return DbHelperSQL.Query(mysql).Tables[0];
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
			parameters[0].Value = "TermTotal";
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

