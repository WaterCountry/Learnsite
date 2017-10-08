using System;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类Quiz。
	/// </summary>
	public class Quiz
	{
		public Quiz()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Qid", "Quiz"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Qid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Quiz");
			strSql.Append(" where Qid=@Qid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4)};
			parameters[0].Value = Qid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Quiz model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Quiz(");
            strSql.Append("Qtype,Question,Qanswer,Qanalyze,Qscore,Qclass,Qselect)");
            strSql.Append(" values (");
            strSql.Append("@Qtype,@Question,@Qanswer,@Qanalyze,@Qscore,@Qclass,@Qselect)");
            SqlParameter[] parameters = {
					new SqlParameter("@Qtype", SqlDbType.Int,4),
					new SqlParameter("@Question", SqlDbType.NText),
					new SqlParameter("@Qanswer", SqlDbType.NVarChar,50),
					new SqlParameter("@Qanalyze", SqlDbType.NVarChar,50),
					new SqlParameter("@Qscore", SqlDbType.Int,4),
					new SqlParameter("@Qclass", SqlDbType.NVarChar,50),
					new SqlParameter("@Qselect", SqlDbType.Bit,1)};
            parameters[0].Value = model.Qtype;
            parameters[1].Value = model.Question;
            parameters[2].Value = model.Qanswer;
            parameters[3].Value = model.Qanalyze;
            parameters[4].Value = model.Qscore;
            parameters[5].Value = model.Qclass;
            parameters[6].Value = model.Qselect;

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
        /// 初始化正确和错误统计
        /// </summary>
        public void initQuizRW()
        {
            string mysql = "update Quiz set Qright=0,Qwrong=0 ";
            DbHelperSQL.ExecuteSql(mysql);
        }
        public void UpdateQright(string Quizrights)
        {
            string mysql = "update Quiz set Qright=Qright+1 where Qid in("+Quizrights+")";
            DbHelperSQL.ExecuteSql(mysql);
        }
        public void UpdateQwrong(string Quizwrongs)
        {
            string mysql = "update Quiz set Qwrong=Qwrong+1 where Qid in(" + Quizwrongs + ")";
            DbHelperSQL.ExecuteSql(mysql);
        }
        public void UpdateQaccuracy(string Quizqid)
        {
            string mysql = "update Quiz set Qaccuracy=(Qright*100/(Qright+Qwrong)) where Qid in ("+Quizqid+")";
            DbHelperSQL.ExecuteSql(mysql);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Quiz model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Quiz set ");
			strSql.Append("Qtype=@Qtype,");
			strSql.Append("Question=@Question,");
			strSql.Append("Qanswer=@Qanswer,");
			strSql.Append("Qanalyze=@Qanalyze,");
			strSql.Append("Qscore=@Qscore,");
            strSql.Append("Qclass=@Qclass,");
            strSql.Append("Qselect=@Qselect");
			strSql.Append(" where Qid=@Qid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4),
					new SqlParameter("@Qtype", SqlDbType.Int,4),
					new SqlParameter("@Question", SqlDbType.NText),
					new SqlParameter("@Qanswer", SqlDbType.NVarChar,50),
					new SqlParameter("@Qanalyze", SqlDbType.NVarChar,50),
					new SqlParameter("@Qscore", SqlDbType.Int,4),
                    new SqlParameter("@Qclass", SqlDbType.NVarChar,50),
					new SqlParameter("@Qselect", SqlDbType.Bit,1)};

			parameters[0].Value = model.Qid;
			parameters[1].Value = model.Qtype;
			parameters[2].Value = model.Question;
			parameters[3].Value = model.Qanswer;
			parameters[4].Value = model.Qanalyze;
            parameters[5].Value = model.Qscore;
            parameters[6].Value = model.Qclass;
            parameters[7].Value = model.Qselect;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Qid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Quiz ");
			strSql.Append(" where Qid=@Qid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4)};
			parameters[0].Value = Qid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.Quiz GetModel(int Qid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Qid,Qtype,Question,Qanswer,Qanalyze,Qscore,Qclass,Qselect,Qright,Qwrong,Qaccuracy from Quiz ");
            strSql.Append(" where Qid=@Qid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4)};
            parameters[0].Value = Qid;

            LearnSite.Model.Quiz model = new LearnSite.Model.Quiz();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Qid"].ToString() != "")
                {
                    model.Qid = int.Parse(ds.Tables[0].Rows[0]["Qid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qtype"].ToString() != "")
                {
                    model.Qtype = int.Parse(ds.Tables[0].Rows[0]["Qtype"].ToString());
                }
                model.Question = ds.Tables[0].Rows[0]["Question"].ToString();
                model.Qanswer = ds.Tables[0].Rows[0]["Qanswer"].ToString();
                model.Qanalyze = ds.Tables[0].Rows[0]["Qanalyze"].ToString();
                if (ds.Tables[0].Rows[0]["Qscore"].ToString() != "")
                {
                    model.Qscore = int.Parse(ds.Tables[0].Rows[0]["Qscore"].ToString());
                }
                model.Qclass = ds.Tables[0].Rows[0]["Qclass"].ToString();
                if (ds.Tables[0].Rows[0]["Qselect"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Qselect"].ToString() == "1") || (ds.Tables[0].Rows[0]["Qselect"].ToString().ToLower() == "true"))
                    {
                        model.Qselect = true;
                    }
                    else
                    {
                        model.Qselect = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Qright"].ToString() != "")
                {
                    model.Qright = int.Parse(ds.Tables[0].Rows[0]["Qright"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qwrong"].ToString() != "")
                {
                    model.Qwrong = int.Parse(ds.Tables[0].Rows[0]["Qwrong"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qaccuracy"].ToString() != "")
                {
                    model.Qaccuracy = int.Parse(ds.Tables[0].Rows[0]["Qaccuracy"].ToString());
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
            strSql.Append("select Qid,Qtype,Question,Qanswer,Qanalyze,Qscore,Qclass,Qselect,Qright,Qwrong,Qaccuracy ");
			strSql.Append(" FROM Quiz ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            if (Top > 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select ");
                strSql.Append(" top " + Top.ToString());
                strSql.Append(" Qid,Qtype,Question,Qanswer,Qanalyze,Qscore,Qclass,Qselect,Qright,Qwrong,Qaccuracy ");
                strSql.Append(" FROM Quiz ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(" order by " + filedOrder);
                return DbHelperSQL.Query(strSql.ToString());
            }
            else
                return null;
        }
        /// <summary>
        /// 根据试题类型返回查询dataset
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByQtype(int Qtype)
        {
            string strWhere = " Qtype="+Qtype;
            return GetList(strWhere);
        }

        /// <summary>
        /// 根据试题类型和学案类型返回查询dataset
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByQtypeQclass(int Qtype,string Qclass)
        {
            string strWhere = " Qtype=" + Qtype+" and Qclass='"+Qclass+"'";
            return GetList(strWhere);
        }

        /// <summary>
        /// 根据数组查询返回试题
        /// </summary>
        /// <param name="quizQid"></param>
        /// <returns></returns>
        public DataSet GetListByQidArray(string[] quizQid)
        {
            string strWhere = " Qid in (" + LearnSite.Common.WordProcess.ArraySqlstr(quizQid) + ")  order by Qtype";
            return GetList(strWhere);
        }

        /// <summary>
        /// 根据试题类型，试题数量，返回随机查询dataset
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByQtypeNum(int Qtype, int num, string selectclass)
        {
            if (!string.IsNullOrEmpty(selectclass))
            {
                string strWhere = " Qtype=" + Qtype + " and  Qclass in (" + selectclass + ") ";
                string filedOrder = " NewID()";
                return GetList(num, strWhere, filedOrder);
            }
            else
            {
                return null;
            }
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
			parameters[0].Value = "Quiz";
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

