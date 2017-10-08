using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类QuizGrade。
	/// </summary>
	public class QuizGrade
	{
		public QuizGrade()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Qid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from QuizGrade");
			strSql.Append(" where Qid=@Qid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4)};
			parameters[0].Value = Qid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public string ExistsQobj(int Qobj,int Qhid)
        {
            string strSql = "select Qid from QuizGrade where Qobj=" + Qobj + " and Qhid=" + Qhid;
            return DbHelperSQL.FindString(strSql);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LearnSite.Model.QuizGrade model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QuizGrade(");
            strSql.Append("Qobj,Qclass,Qhid,Qonly,Qmore,Qjudge,Qopen,Qanswer)");
            strSql.Append(" values (");
            strSql.Append("@Qobj,@Qclass,@Qhid,@Qonly,@Qmore,@Qjudge,@Qopen,@Qanswer)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Qobj", SqlDbType.Int,4),
					new SqlParameter("@Qclass", SqlDbType.NText),
					new SqlParameter("@Qhid", SqlDbType.Int,4),
					new SqlParameter("@Qonly", SqlDbType.Int,4),
					new SqlParameter("@Qmore", SqlDbType.Int,4),
					new SqlParameter("@Qjudge", SqlDbType.Int,4),
					new SqlParameter("@Qopen", SqlDbType.Bit,1),
                    new SqlParameter("@Qanswer", SqlDbType.Bit,1)};
            parameters[0].Value = model.Qobj;
            parameters[1].Value = model.Qclass;
            parameters[2].Value = model.Qhid;
            parameters[3].Value = model.Qonly;
            parameters[4].Value = model.Qmore;
            parameters[5].Value = model.Qjudge;
            parameters[6].Value = model.Qopen;
            parameters[7].Value = model.Qanswer;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(LearnSite.Model.QuizGrade model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QuizGrade set ");
            strSql.Append("Qobj=@Qobj,");
            strSql.Append("Qclass=@Qclass,");
            strSql.Append("Qhid=@Qhid,");
            strSql.Append("Qonly=@Qonly,");
            strSql.Append("Qmore=@Qmore,");
            strSql.Append("Qjudge=@Qjudge,");
            strSql.Append("Qopen=@Qopen,");
            strSql.Append("Qanswer=@Qanswer");
            strSql.Append(" where Qid=@Qid");
            SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4),
					new SqlParameter("@Qobj", SqlDbType.Int,4),
					new SqlParameter("@Qclass", SqlDbType.NText),
					new SqlParameter("@Qhid", SqlDbType.Int,4),
					new SqlParameter("@Qonly", SqlDbType.Int,4),
					new SqlParameter("@Qmore", SqlDbType.Int,4),
					new SqlParameter("@Qjudge", SqlDbType.Int,4),
					new SqlParameter("@Qopen", SqlDbType.Bit,1),
					new SqlParameter("@Qanswer", SqlDbType.Bit,1)};
            parameters[0].Value = model.Qid;
            parameters[1].Value = model.Qobj;
            parameters[2].Value = model.Qclass;
            parameters[3].Value = model.Qhid;
            parameters[4].Value = model.Qonly;
            parameters[5].Value = model.Qmore;
            parameters[6].Value = model.Qjudge;
            parameters[7].Value = model.Qopen;
            parameters[8].Value = model.Qanswer;

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
        public bool Delete(int Qid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from QuizGrade ");
            strSql.Append(" where Qid=@Qid");
            SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4)};
            parameters[0].Value = Qid;

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
        /// 删除Qhid空字段记录数据
        /// </summary>
        public void DeleteNull()
        {
            string strSql = "delete from QuizGrade where Qhid is null";
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.QuizGrade GetModel(int Qid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Qid,Qobj,Qclass,Qhid,Qonly,Qmore,Qjudge,Qopen,Qanswer from QuizGrade ");
            strSql.Append(" where Qid=@Qid");
            SqlParameter[] parameters = {
					new SqlParameter("@Qid", SqlDbType.Int,4)
};
            parameters[0].Value = Qid;

            LearnSite.Model.QuizGrade model = new LearnSite.Model.QuizGrade();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Qid"].ToString() != "")
                {
                    model.Qid = int.Parse(ds.Tables[0].Rows[0]["Qid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qobj"].ToString() != "")
                {
                    model.Qobj = int.Parse(ds.Tables[0].Rows[0]["Qobj"].ToString());
                }
                model.Qclass = ds.Tables[0].Rows[0]["Qclass"].ToString();
                if (ds.Tables[0].Rows[0]["Qhid"].ToString() != "")
                {
                    model.Qhid = int.Parse(ds.Tables[0].Rows[0]["Qhid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qonly"].ToString() != "")
                {
                    model.Qonly = int.Parse(ds.Tables[0].Rows[0]["Qonly"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qmore"].ToString() != "")
                {
                    model.Qmore = int.Parse(ds.Tables[0].Rows[0]["Qmore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qjudge"].ToString() != "")
                {
                    model.Qjudge = int.Parse(ds.Tables[0].Rows[0]["Qjudge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qopen"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Qopen"].ToString() == "1") || (ds.Tables[0].Rows[0]["Qopen"].ToString().ToLower() == "true"))
                    {
                        model.Qopen = true;
                    }
                    else
                    {
                        model.Qopen = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Qanswer"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Qanswer"].ToString() == "1") || (ds.Tables[0].Rows[0]["Qanswer"].ToString().ToLower() == "true"))
                    {
                        model.Qanswer = true;
                    }
                    else
                    {
                        model.Qanswer = false;
                    }
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
        public LearnSite.Model.QuizGrade GetModelByQobj(int Qobj)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Qid,Qobj,Qclass,Qhid,Qonly,Qmore,Qjudge,Qopen,Qanswer from QuizGrade ");
            strSql.Append(" where Qobj=@Qobj ");
            SqlParameter[] parameters = {
					new SqlParameter("@Qobj", SqlDbType.Int,4)};
            parameters[0].Value = Qobj;

            LearnSite.Model.QuizGrade model = new LearnSite.Model.QuizGrade();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Qid"].ToString() != "")
                {
                    model.Qid = int.Parse(ds.Tables[0].Rows[0]["Qid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qobj"].ToString() != "")
                {
                    model.Qobj = int.Parse(ds.Tables[0].Rows[0]["Qobj"].ToString());
                }
                model.Qclass = ds.Tables[0].Rows[0]["Qclass"].ToString();
                if (ds.Tables[0].Rows[0]["Qhid"].ToString() != "")
                {
                    model.Qhid = int.Parse(ds.Tables[0].Rows[0]["Qhid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qonly"].ToString() != "")
                {
                    model.Qonly = int.Parse(ds.Tables[0].Rows[0]["Qonly"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qmore"].ToString() != "")
                {
                    model.Qmore = int.Parse(ds.Tables[0].Rows[0]["Qmore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qjudge"].ToString() != "")
                {
                    model.Qjudge = int.Parse(ds.Tables[0].Rows[0]["Qjudge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qopen"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Qopen"].ToString() == "1") || (ds.Tables[0].Rows[0]["Qopen"].ToString().ToLower() == "true"))
                    {
                        model.Qopen = true;
                    }
                    else
                    {
                        model.Qopen = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Qanswer"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Qanswer"].ToString() == "1") || (ds.Tables[0].Rows[0]["Qanswer"].ToString().ToLower() == "true"))
                    {
                        model.Qanswer = true;
                    }
                    else
                    {
                        model.Qanswer = false;
                    }
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据年级，教师编号得到一个对象实体
        /// </summary>
        public LearnSite.Model.QuizGrade GetModelByQobjQhid(int Qobj,int Qhid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Qid,Qobj,Qclass,Qhid,Qonly,Qmore,Qjudge,Qopen,Qanswer from QuizGrade ");
            strSql.Append(" where Qobj=@Qobj and Qhid=@Qhid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Qobj", SqlDbType.Int,4),
                    new SqlParameter("@Qhid", SqlDbType.Int,4)};
            parameters[0].Value = Qobj;
            parameters[1].Value =Qhid;

            LearnSite.Model.QuizGrade model = new LearnSite.Model.QuizGrade();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Qid"].ToString() != "")
                {
                    model.Qid = int.Parse(ds.Tables[0].Rows[0]["Qid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qobj"].ToString() != "")
                {
                    model.Qobj = int.Parse(ds.Tables[0].Rows[0]["Qobj"].ToString());
                }
                model.Qclass = ds.Tables[0].Rows[0]["Qclass"].ToString();
                if (ds.Tables[0].Rows[0]["Qhid"].ToString() != "")
                {
                    model.Qhid = int.Parse(ds.Tables[0].Rows[0]["Qhid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qonly"].ToString() != "")
                {
                    model.Qonly = int.Parse(ds.Tables[0].Rows[0]["Qonly"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qmore"].ToString() != "")
                {
                    model.Qmore = int.Parse(ds.Tables[0].Rows[0]["Qmore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qjudge"].ToString() != "")
                {
                    model.Qjudge = int.Parse(ds.Tables[0].Rows[0]["Qjudge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qopen"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Qopen"].ToString() == "1") || (ds.Tables[0].Rows[0]["Qopen"].ToString().ToLower() == "true"))
                    {
                        model.Qopen = true;
                    }
                    else
                    {
                        model.Qopen = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Qanswer"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Qanswer"].ToString() == "1") || (ds.Tables[0].Rows[0]["Qanswer"].ToString().ToLower() == "true"))
                    {
                        model.Qanswer = true;
                    }
                    else
                    {
                        model.Qanswer = false;
                    }
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据年级，班级得到一个对象实体
        /// </summary>
        public LearnSite.Model.QuizGrade GetModelByQobjRclass(int Qobj, int Rclass)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Qid,Qobj,Qclass,Qhid,Qonly,Qmore,Qjudge,Qopen,Qanswer from QuizGrade,Room ");
            strSql.Append(" where Qobj=@Qobj and Qhid=Rhid and Rgrade=Qobj and Rclass=@Rclass ");
            SqlParameter[] parameters = {
					new SqlParameter("@Qobj", SqlDbType.Int,4),
                    new SqlParameter("@Rclass", SqlDbType.Int,4)};
            parameters[0].Value = Qobj;
            parameters[1].Value =Rclass;

            LearnSite.Model.QuizGrade model = new LearnSite.Model.QuizGrade();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Qid"].ToString() != "")
                {
                    model.Qid = int.Parse(ds.Tables[0].Rows[0]["Qid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qobj"].ToString() != "")
                {
                    model.Qobj = int.Parse(ds.Tables[0].Rows[0]["Qobj"].ToString());
                }
                model.Qclass = ds.Tables[0].Rows[0]["Qclass"].ToString();
                if (ds.Tables[0].Rows[0]["Qhid"].ToString() != "")
                {
                    model.Qhid = int.Parse(ds.Tables[0].Rows[0]["Qhid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qonly"].ToString() != "")
                {
                    model.Qonly = int.Parse(ds.Tables[0].Rows[0]["Qonly"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qmore"].ToString() != "")
                {
                    model.Qmore = int.Parse(ds.Tables[0].Rows[0]["Qmore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qjudge"].ToString() != "")
                {
                    model.Qjudge = int.Parse(ds.Tables[0].Rows[0]["Qjudge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qopen"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Qopen"].ToString() == "1") || (ds.Tables[0].Rows[0]["Qopen"].ToString().ToLower() == "true"))
                    {
                        model.Qopen = true;
                    }
                    else
                    {
                        model.Qopen = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Qanswer"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Qanswer"].ToString() == "1") || (ds.Tables[0].Rows[0]["Qanswer"].ToString().ToLower() == "true"))
                    {
                        model.Qanswer = true;
                    }
                    else
                    {
                        model.Qanswer = false;
                    }
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Qid,Qobj,Qclass,Qhid,Qonly,Qmore,Qjudge,Qopen,Qanswer ");
            strSql.Append(" FROM QuizGrade ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
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
            strSql.Append(" Qid,Qobj,Qclass,Qhid,Qonly,Qmore,Qjudge,Qopen,Qanswer ");
            strSql.Append(" FROM QuizGrade ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 根据Qobj查询Qclass，如果无则返回空字符""
        /// </summary>
        /// <param name="Qobj"></param>
        /// <returns></returns>
        public string GetQclass(int Qobj)
        {
            string mysql = "select Qclass from QuizGrade where Qobj="+Qobj;
            return DbHelperSQL.FindString(mysql);
        }
        /// <summary>
        /// 获取年级测验答案显示开关
        /// </summary>
        /// <param name="Qobj"></param>
        /// <param name="Rclass"></param>
        /// <returns></returns>
        public bool GetQanswer(int Qobj,int Rclass)
        {
            bool isok = false;
            string mysql = "select Qanswer from QuizGrade,Room where Qobj=" + Qobj +"and Qhid=Rhid and Qobj=Rgrade and Rclass="+Rclass;
            string retstr= DbHelperSQL.FindString(mysql);
            if (retstr != "")
            {
                isok = bool.Parse(retstr);
            }
            return isok;
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
			parameters[0].Value = "QuizGrade";
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

