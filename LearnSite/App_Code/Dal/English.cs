using System;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类English。
	/// </summary>
	public class English
	{
		public English()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Eid", "English"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Eid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from English");
			strSql.Append(" where Eid=@Eid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Eid", SqlDbType.Int,4)};
			parameters[0].Value = Eid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 统计相应级别的单词数
        /// </summary>
        /// <param name="Elevel"></param>
        /// <returns></returns>
        public int CountLevel(int Elevel)
        {
            string mysql = "select count(*) from English where Elevel="+Elevel;
            string result = DbHelperSQL.FindString(mysql);
            if (result != "")
                return Int32.Parse(result);
            else
                return 0;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LearnSite.Model.English model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into English(");
            strSql.Append("Eword,Emeaning,Elevel)");
            strSql.Append(" values (");
            strSql.Append("@Eword,@Emeaning,@Elevel)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Eword", SqlDbType.NVarChar,50),
					new SqlParameter("@Emeaning", SqlDbType.NText),
					new SqlParameter("@Elevel", SqlDbType.Int,4)};
            parameters[0].Value = model.Eword;
            parameters[1].Value = model.Emeaning;
            parameters[2].Value = model.Elevel;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(LearnSite.Model.English model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update English set ");
            strSql.Append("Eword=@Eword,");
            strSql.Append("Emeaning=@Emeaning,");
            strSql.Append("Elevel=@Elevel");
            strSql.Append(" where Eid=@Eid");
            SqlParameter[] parameters = {
					new SqlParameter("@Eid", SqlDbType.Int,4),
					new SqlParameter("@Eword", SqlDbType.NVarChar,50),
					new SqlParameter("@Emeaning", SqlDbType.NText),
					new SqlParameter("@Elevel", SqlDbType.Int,4)};
            parameters[0].Value = model.Eid;
            parameters[1].Value = model.Eword;
            parameters[2].Value = model.Emeaning;
            parameters[3].Value = model.Elevel;

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
        /// 将该词标记为相应级别
        /// </summary>
        /// <param name="Eword"></param>
        /// <param name="Elevel"></param>
        public bool UpdateElevel(string Eword,int Elevel)
        {
            string mysql = "update English set Elevel="+Elevel+" where Elevel is null or Elevel>"+Elevel+" and Eword='"+Eword+"'";
            int rows = DbHelperSQL.ExecuteSql(mysql);
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
		public void Delete(int Eid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from English ");
			strSql.Append(" where Eid=@Eid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Eid", SqlDbType.Int,4)};
			parameters[0].Value = Eid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteAll()
        {
            string strSql = "delete from English ";
            DbHelperSQL.ExecuteSql(strSql);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.English GetModel(int Eid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Eid,Eword,Emeaning,Elevel from English ");
            strSql.Append(" where Eid=@Eid");
            SqlParameter[] parameters = {
					new SqlParameter("@Eid", SqlDbType.Int,4)
};
            parameters[0].Value = Eid;

            LearnSite.Model.English model = new LearnSite.Model.English();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Eid"].ToString() != "")
                {
                    model.Eid = int.Parse(ds.Tables[0].Rows[0]["Eid"].ToString());
                }
                model.Eword = ds.Tables[0].Rows[0]["Eword"].ToString();
                model.Emeaning = ds.Tables[0].Rows[0]["Emeaning"].ToString();
                if (ds.Tables[0].Rows[0]["Elevel"].ToString() != "")
                {
                    model.Elevel = int.Parse(ds.Tables[0].Rows[0]["Elevel"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 随机获取一个单词实体
        /// </summary>
        /// <returns></returns>
        public LearnSite.Model.English GetRndModel(int Elevel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Eid,Eword,Emeaning,Elevel from English where Elevel=@Elevel ");
            strSql.Append(" order by newid() ");
            SqlParameter[] parameters = {
					new SqlParameter("@Elevel", SqlDbType.Int,4)
};
            parameters[0].Value = Elevel;

            LearnSite.Model.English model = new LearnSite.Model.English();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(),parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Eid"].ToString() != "")
                {
                    model.Eid = int.Parse(ds.Tables[0].Rows[0]["Eid"].ToString());
                }
                model.Eword = ds.Tables[0].Rows[0]["Eword"].ToString();
                model.Emeaning = ds.Tables[0].Rows[0]["Emeaning"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 随机获取一个单词实体
        /// </summary>
        /// <returns></returns>
        public LearnSite.Model.English GetNextModel(int Eid,int Elevel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Eid,Eword,Emeaning,Elevel from English where Elevel=@Elevel and Eid>@Eid order by Eid asc");
            SqlParameter[] parameters = {
					new SqlParameter("@Eid", SqlDbType.Int,4),
                    new SqlParameter("@Elevel", SqlDbType.Int,4)
};
            parameters[0].Value = Eid;
            parameters[1].Value = Elevel;

            LearnSite.Model.English model = new LearnSite.Model.English();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(),parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Eid"].ToString() != "")
                {
                    model.Eid = int.Parse(ds.Tables[0].Rows[0]["Eid"].ToString());
                }
                model.Eword = ds.Tables[0].Rows[0]["Eword"].ToString();
                model.Emeaning = ds.Tables[0].Rows[0]["Emeaning"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取某等级所有单词和意思
        /// </summary>
        /// <returns></returns>
        public string GetLevelwords(int Elevel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Eid, Eword ,Emeaning from English where Elevel=@Elevel order by Eid asc");
            SqlParameter[] parameters = {
                    new SqlParameter("@Elevel", SqlDbType.Int,4)
};
            parameters[0].Value = Elevel;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            int count = ds.Tables[0].Rows.Count;
            string wstr = "";
            string mstr = "";
            string wmstr = "";
            if (count > 0)
            {

                for (int i = 0; i < count; i++)
                {
                    string ewd = ds.Tables[0].Rows[i]["Eword"].ToString();
                    string emg = ds.Tables[0].Rows[i]["Emeaning"].ToString();
                    wstr = wstr + ewd;
                    mstr = mstr + System.Web.HttpUtility.HtmlEncode(emg);
                    if (i != count - 1)
                    {
                        wstr = wstr + "|";
                        mstr = mstr + "|";
                    }
                }
                wmstr = wstr + "==" + mstr;
            }
            return wmstr;
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select Eid,Eword,Emeaning,Elevel ");
			strSql.Append(" FROM English ");
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
            strSql.Append(" Eid,Eword,Emeaning,Elevel ");
			strSql.Append(" FROM English ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
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
			parameters[0].Value = "English";
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

