using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:Ip
	/// </summary>
	public partial class Ip
	{
		public Ip()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Iid", "Ip"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Iid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Ip");
			strSql.Append(" where Iid=@Iid");
			SqlParameter[] parameters = {
					new SqlParameter("@Iid", SqlDbType.Int,4)};
			parameters[0].Value = Iid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsIp(int Ihid, string Iip)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ip");
            strSql.Append(" where Ihid=@Ihid and Iip=@Iip");
            SqlParameter[] parameters = {
					new SqlParameter("@Ihid", SqlDbType.Int,4),
					new SqlParameter("@Iip", SqlDbType.NVarChar,50)};
            parameters[0].Value = Ihid;
            parameters[1].Value = Iip;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Ip model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Ip(");
			strSql.Append("Ihid,Inum,Iip)");
			strSql.Append(" values (");
			strSql.Append("@Ihid,@Inum,@Iip)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Ihid", SqlDbType.Int,4),
					new SqlParameter("@Inum", SqlDbType.Int,4),
					new SqlParameter("@Iip", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Ihid;
			parameters[1].Value = model.Inum;
			parameters[2].Value = model.Iip;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(LearnSite.Model.Ip model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Ip set ");
			strSql.Append("Ihid=@Ihid,");
			strSql.Append("Inum=@Inum,");
			strSql.Append("Iip=@Iip");
			strSql.Append(" where Iid=@Iid");
			SqlParameter[] parameters = {
					new SqlParameter("@Ihid", SqlDbType.Int,4),
					new SqlParameter("@Inum", SqlDbType.Int,4),
					new SqlParameter("@Iip", SqlDbType.NVarChar,50),
					new SqlParameter("@Iid", SqlDbType.Int,4)};
			parameters[0].Value = model.Ihid;
			parameters[1].Value = model.Inum;
			parameters[2].Value = model.Iip;
			parameters[3].Value = model.Iid;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool UpdateIip(string Iip, int Iid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ip set ");
            strSql.Append("Iip=@Iip");
            strSql.Append(" where Iid=@Iid");
            SqlParameter[] parameters = {
					new SqlParameter("@Iip", SqlDbType.NVarChar,50),
					new SqlParameter("@Iid", SqlDbType.Int,4)};
            parameters[0].Value = Iip;
            parameters[1].Value = Iid;

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
        /// 删除该机房所有IP记录
        /// </summary>
        public bool DeleteIhid(int Ihid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ip ");
            strSql.Append(" where Ihid=@Ihid");
            SqlParameter[] parameters = {
					new SqlParameter("@Ihid", SqlDbType.Int,4)
};
            parameters[0].Value = Ihid;

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
		public bool Delete(int Iid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Ip ");
			strSql.Append(" where Iid=@Iid");
			SqlParameter[] parameters = {
					new SqlParameter("@Iid", SqlDbType.Int,4)
};
			parameters[0].Value = Iid;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string Iidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Ip ");
			strSql.Append(" where Iid in ("+Iidlist + ")  ");
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
		public LearnSite.Model.Ip GetModel(int Iid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Iid,Ihid,Inum,Iip from Ip ");
			strSql.Append(" where Iid=@Iid");
			SqlParameter[] parameters = {
					new SqlParameter("@Iid", SqlDbType.Int,4)
};
			parameters[0].Value = Iid;

			LearnSite.Model.Ip model=new LearnSite.Model.Ip();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Iid"].ToString()!="")
				{
					model.Iid=int.Parse(ds.Tables[0].Rows[0]["Iid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ihid"].ToString()!="")
				{
					model.Ihid=int.Parse(ds.Tables[0].Rows[0]["Ihid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Inum"].ToString()!="")
				{
					model.Inum=int.Parse(ds.Tables[0].Rows[0]["Inum"].ToString());
				}
				model.Iip=ds.Tables[0].Rows[0]["Iip"].ToString();
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
			strSql.Append("select Iid,Ihid,Inum,Iip ");
			strSql.Append(" FROM Ip ");
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
			strSql.Append(" Iid,Ihid,Inum,Iip ");
			strSql.Append(" FROM Ip ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获取本班当天签到同学的 Qnum,Qname,Inum
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataTable GetSiginStudents(int Sgrade, int Sclass, int Ihid)
        {
            DateTime today = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct Qnum,Qname,Inum ");
            strSql.Append(" FROM Signin,Ip ");
            strSql.Append(" WHERE Qgrade=@Qgrade AND Qclass=@Qclass AND Qyear=@Qyear AND Qmonth=@Qmonth AND Qday=@Qday AND Qip=Iip AND Ihid=@Ihid ");
            strSql.Append(" order by Inum asc ");

            SqlParameter[] parameters = {
					new SqlParameter("@Qgrade", SqlDbType.Int,4),
                    new SqlParameter("@Qclass", SqlDbType.Int,4),
                    new SqlParameter("@Qyear", SqlDbType.Int,4),                    
                    new SqlParameter("@Qmonth", SqlDbType.Int,4),                    
                    new SqlParameter("@Qday", SqlDbType.Int,4),                    
                    new SqlParameter("@Ihid", SqlDbType.Int,4)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;
            parameters[2].Value = today.Year;
            parameters[3].Value = today.Month;
            parameters[4].Value = today.Day;
            parameters[5].Value = Ihid;

            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
        }

        /// <summary>
        /// 获取本班当天签到同学的Inum- Qnum- Qname-phototype| 形式的字符串
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string GetSiginStudentStr(int Sgrade, int Sclass, int Ihid, bool isshow)
        {
            string stustr = "";
            DateTime today = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct Qnum,Qname,Qmachine,Inum ");
            strSql.Append(" FROM Signin,Ip ");
            strSql.Append(" WHERE Qgrade=@Qgrade AND Qclass=@Qclass AND Qyear=@Qyear AND Qmonth=@Qmonth AND Qday=@Qday AND Qip=Iip AND Ihid=@Ihid ");
            strSql.Append(" order by Inum asc ");

            SqlParameter[] parameters = {
					new SqlParameter("@Qgrade", SqlDbType.Int,4),
                    new SqlParameter("@Qclass", SqlDbType.Int,4),
                    new SqlParameter("@Qyear", SqlDbType.Int,4),                    
                    new SqlParameter("@Qmonth", SqlDbType.Int,4),                    
                    new SqlParameter("@Qday", SqlDbType.Int,4),                    
                    new SqlParameter("@Ihid", SqlDbType.Int,4)};

            parameters[0].Value = Sgrade;
            parameters[1].Value = Sclass;
            parameters[2].Value = today.Year;
            parameters[3].Value = today.Month;
            parameters[4].Value = today.Day;
            parameters[5].Value = Ihid;

            DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
            int dcount = dt.Rows.Count;
            if (dcount > 0)
            {
                for (int i = 0; i < dcount; i++)
                {
                    string inum = dt.Rows[i]["Inum"].ToString();
                    string qnum = dt.Rows[i]["Qnum"].ToString();
                    string qname = dt.Rows[i]["Qname"].ToString();
                    string qmachine = " ";
                    if (dt.Rows[i]["Qmachine"] != null)
                    {
                        qmachine = dt.Rows[i]["Qmachine"].ToString();
                        qmachine = qmachine.Replace("-", "");
                        qmachine = qmachine.Replace("_", "");
                        qmachine = qmachine.Replace("|", "");//去掉这两个分隔符
                    }
                    if (isshow)
                        qname = qname + "_" + qmachine;
                    string phototype = LearnSite.Common.Photo.ExistStuPhotoIntStr(qnum);
                    stustr = stustr + inum + "-" + qnum + "-" + qname + "-" + phototype;
                    if (i < dcount - 1)
                        stustr = stustr + "|";
                }
            }
            return stustr;
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
			parameters[0].Value = "Ip";
			parameters[1].Value = "Iid";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

