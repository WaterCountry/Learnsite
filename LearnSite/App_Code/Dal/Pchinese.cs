using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:Pchinese
	/// </summary>
	public partial class Pchinese
	{
		public Pchinese()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Pid", "Pchinese"); 
		}
        /// <summary>
        /// 是否存在该记录，不存在则添加
        /// </summary>
        public bool NoExistsAdd(string Psnum, int Pyear, int Pgrade, int Pclass, int Psid, int Pterm)
        {
            DateTime Pdate = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Pchinese");
            strSql.Append(" where Psid=@Psid and Pterm=@Pterm and Pgrade=@Pgrade");

            SqlParameter[] parameters = {
					new SqlParameter("@Psid", SqlDbType.Int,4),
					new SqlParameter("@Psnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Pyear", SqlDbType.Int,4),
					new SqlParameter("@Pgrade", SqlDbType.Int,4),
					new SqlParameter("@Pclass", SqlDbType.Int,4),
					new SqlParameter("@Pterm", SqlDbType.Int,4),
					new SqlParameter("@Pdate", SqlDbType.DateTime)};
            parameters[0].Value = Psid;
            parameters[1].Value = Psnum;
            parameters[2].Value = Pyear;
            parameters[3].Value = Pgrade;
            parameters[4].Value = Pclass;
            parameters[5].Value = Pterm;
            parameters[6].Value = Pdate;
            if (!DbHelperSQL.Exists(strSql.ToString(), parameters))
            {
                StringBuilder addSql = new StringBuilder();
                addSql.Append("insert into Pchinese(");
                addSql.Append("Psid,Psnum,Pyear,Pgrade,Pclass,Pterm,Pdate)");
                addSql.Append(" values (");
                addSql.Append("@Psid,@Psnum,@Pyear,@Pgrade,@Pclass,@Pterm,@Pdate)");
                DbHelperSQL.GetSingle(addSql.ToString(), parameters);//如果不存在本学期记录则添加
            }

            return true;
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Pid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Pchinese");
			strSql.Append(" where Pid=@Pid");
			SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4)
			};
			parameters[0].Value = Pid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Pchinese model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Pchinese(");
			strSql.Append("Psid,Psnum,Ptotal,Pspeed,Pyear,Pgrade,Pclass,Pterm,Pdate)");
			strSql.Append(" values (");
			strSql.Append("@Psid,@Psnum,@Ptotal,@Pspeed,@Pyear,@Pgrade,@Pclass,@Pterm,@Pdate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Psid", SqlDbType.Int,4),
					new SqlParameter("@Psnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Ptotal", SqlDbType.Int,4),
					new SqlParameter("@Pspeed", SqlDbType.Int,4),
					new SqlParameter("@Pyear", SqlDbType.Int,4),
					new SqlParameter("@Pgrade", SqlDbType.Int,4),
					new SqlParameter("@Pclass", SqlDbType.Int,4),
					new SqlParameter("@Pterm", SqlDbType.Int,4),
					new SqlParameter("@Pdate", SqlDbType.DateTime)};
			parameters[0].Value = model.Psid;
			parameters[1].Value = model.Psnum;
			parameters[2].Value = model.Ptotal;
			parameters[3].Value = model.Pspeed;
			parameters[4].Value = model.Pyear;
			parameters[5].Value = model.Pgrade;
			parameters[6].Value = model.Pclass;
			parameters[7].Value = model.Pterm;
			parameters[8].Value = model.Pdate;

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
		public bool Update(LearnSite.Model.Pchinese model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Pchinese set ");
            strSql.Append("Ptotal=Ptotal+@Papple,");
			strSql.Append("Pspeed=@Pspeed,");
			strSql.Append("Pdate=@Pdate");
            strSql.Append(" where Psid=@Psid and Pterm=@Pterm and Pgrade=@Pgrade");
			SqlParameter[] parameters = {
					new SqlParameter("@Psid", SqlDbType.Int,4),
					new SqlParameter("@Pspeed", SqlDbType.Int,4),
					new SqlParameter("@Pgrade", SqlDbType.Int,4),
					new SqlParameter("@Pterm", SqlDbType.Int,4),
					new SqlParameter("@Pdate", SqlDbType.DateTime),
					new SqlParameter("@Papple", SqlDbType.Int,4)};
			parameters[0].Value = model.Psid;
			parameters[1].Value = model.Pspeed;
			parameters[2].Value = model.Pgrade;
			parameters[3].Value = model.Pterm;
            parameters[4].Value = model.Pdate;
            parameters[5].Value = model.Papple;

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
        /// 更新一条数据，并返回更新结果值
        /// </summary>
        public int UpdateTotal(LearnSite.Model.Pchinese model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Pchinese set ");
            strSql.Append("Ptotal=Ptotal+@Papple,");
            strSql.Append("Pspeed=@Pspeed,");
            strSql.Append("Pdate=@Pdate");
            strSql.Append(" OUTPUT Inserted.Ptotal ");
            strSql.Append(" where Psid=@Psid and Pterm=@Pterm and Pgrade=@Pgrade");
            SqlParameter[] parameters = {
					new SqlParameter("@Psid", SqlDbType.Int,4),
					new SqlParameter("@Pspeed", SqlDbType.Int,4),
					new SqlParameter("@Pgrade", SqlDbType.Int,4),
					new SqlParameter("@Pterm", SqlDbType.Int,4),
					new SqlParameter("@Pdate", SqlDbType.DateTime),
					new SqlParameter("@Papple", SqlDbType.Int,4)};
            parameters[0].Value = model.Psid;
            parameters[1].Value = model.Pspeed;
            parameters[2].Value = model.Pgrade;
            parameters[3].Value = model.Pterm;
            parameters[4].Value = model.Pdate;
            parameters[5].Value = model.Papple;

            object result=DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (result != null)
                return Convert.ToInt32(result);
            else
                return 0;
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Pid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pchinese ");
			strSql.Append(" where Pid=@Pid");
			SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4)
			};
			parameters[0].Value = Pid;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Pidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pchinese ");
			strSql.Append(" where Pid in ("+Pidlist + ")  ");
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
		public LearnSite.Model.Pchinese GetModel(int Pid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Pid,Psid,Psnum,Papple,Ptotal,Pspeed,Pdegree,Pyear,Pgrade,Pclass,Pterm,Pdate from Pchinese ");
			strSql.Append(" where Pid=@Pid");
			SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4)
			};
			parameters[0].Value = Pid;

			LearnSite.Model.Pchinese model=new LearnSite.Model.Pchinese();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Pchinese DataRowToModel(DataRow row)
		{
			LearnSite.Model.Pchinese model=new LearnSite.Model.Pchinese();
			if (row != null)
			{
				if(row["Pid"]!=null && row["Pid"].ToString()!="")
				{
					model.Pid=int.Parse(row["Pid"].ToString());
				}
				if(row["Psid"]!=null && row["Psid"].ToString()!="")
				{
					model.Psid=int.Parse(row["Psid"].ToString());
				}
				if(row["Psnum"]!=null)
				{
					model.Psnum=row["Psnum"].ToString();
				}
				if(row["Papple"]!=null && row["Papple"].ToString()!="")
				{
					model.Papple=int.Parse(row["Papple"].ToString());
				}
				if(row["Ptotal"]!=null && row["Ptotal"].ToString()!="")
				{
					model.Ptotal=int.Parse(row["Ptotal"].ToString());
				}
				if(row["Pspeed"]!=null && row["Pspeed"].ToString()!="")
				{
					model.Pspeed=int.Parse(row["Pspeed"].ToString());
				}
				if(row["Pdegree"]!=null && row["Pdegree"].ToString()!="")
				{
					model.Pdegree=int.Parse(row["Pdegree"].ToString());
				}
				if(row["Pyear"]!=null && row["Pyear"].ToString()!="")
				{
					model.Pyear=int.Parse(row["Pyear"].ToString());
				}
				if(row["Pgrade"]!=null && row["Pgrade"].ToString()!="")
				{
					model.Pgrade=int.Parse(row["Pgrade"].ToString());
				}
				if(row["Pclass"]!=null && row["Pclass"].ToString()!="")
				{
					model.Pclass=int.Parse(row["Pclass"].ToString());
				}
				if(row["Pterm"]!=null && row["Pterm"].ToString()!="")
				{
					model.Pterm=int.Parse(row["Pterm"].ToString());
				}
				if(row["Pdate"]!=null && row["Pdate"].ToString()!="")
				{
					model.Pdate=DateTime.Parse(row["Pdate"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Pid,Psid,Psnum,Papple,Ptotal,Pspeed,Pdegree,Pyear,Pgrade,Pclass,Pterm,Pdate ");
			strSql.Append(" FROM Pchinese ");
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
			strSql.Append(" Pid,Psid,Psnum,Papple,Ptotal,Pspeed,Pdegree,Pyear,Pgrade,Pclass,Pterm,Pdate ");
			strSql.Append(" FROM Pchinese ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Pchinese ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Pid desc");
			}
			strSql.Append(")AS Row, T.*  from Pchinese T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
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
			parameters[0].Value = "Pchinese";
			parameters[1].Value = "Pid";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

        /// <summary>
        /// 获取某学号收集的苹果总数
        /// </summary>
        /// <param name="Psnum"></param>
        /// <returns></returns>
        public int GetTotalApple(string Psnum)
        {
            string mysql = "select Ptotal from Pchinese where Psnum='" + Psnum + "'";
            return DbHelperSQL.FindNum(mysql);        
        }

        /// <summary>
        /// 更新苹果总数
        /// </summary>
        /// <param name="Psnum"></param>
        /// <param name="Papple"></param>
        /// <param name="Pspeed"></param>
        /// <param name="Pterm"></param>
        /// <param name="Pgrade"></param>
        public void UpdateTotalApple(string Psnum, int Papple,int Pspeed,int Pterm,int Pgrade)
        {
            DateTime Pdate = DateTime.Now;
            string mysql = "update Pchinese set Ptotal=Ptotal+" + Papple + ",Pspeed=" + Pspeed + ",Pdate='"+Pdate.ToString()+"'  where  Psnum='" + Psnum + "' and Pterm=" + Pterm + " and Pgrade=" + Pgrade;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 返回０为无效，１为更新记录，２为插入记录
        /// </summary>
        /// <param name="Psid"></param>
        /// <param name="Psnum"></param>
        /// <param name="Pyear"></param>
        /// <param name="Pgrade"></param>
        /// <param name="Pclass"></param>
        /// <param name="Pterm"></param>
        /// <param name="Papple"></param>
        /// <param name="Pspeed"></param>
        /// <returns></returns>
        public int UpdateTotalAppleNew(int Psid, string Psnum, int Pyear, int Pgrade, int Pclass, int Pterm, int Papple, int Pspeed)
        {
            int IsOk = 0;
            DateTime Pdate = DateTime.Now;
            SqlParameter[] parameters = {
					new SqlParameter("@Psid", SqlDbType.Int,4),
					new SqlParameter("@Psnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Pyear", SqlDbType.Int,4),
					new SqlParameter("@Pgrade", SqlDbType.Int,4),
					new SqlParameter("@Pclass", SqlDbType.Int,4),
					new SqlParameter("@Pterm", SqlDbType.Int,4),                                        
					new SqlParameter("@Papple", SqlDbType.Int,4),
					new SqlParameter("@Pspeed", SqlDbType.Int,4),
					new SqlParameter("@Pdate", SqlDbType.DateTime)};
            parameters[0].Value = Psid;
            parameters[1].Value = Psnum;
            parameters[2].Value = Pyear;
            parameters[3].Value = Pgrade;
            parameters[4].Value = Pclass;
            parameters[5].Value = Pterm;
            parameters[6].Value = Papple;
            parameters[7].Value = Pspeed;
            parameters[8].Value = Pdate;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Pchinese set ");
            strSql.Append("Ptotal=Ptotal+@Papple,");
            strSql.Append("Pspeed=@Pspeed,");
            strSql.Append("Pdate=@Pdate");
            strSql.Append(" where Psid=@Psid and Pterm=@Pterm and Pgrade=@Pgrade");

            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (result > 0)
            {
                IsOk = 1;
            }
            else
            {
                StringBuilder addSql = new StringBuilder();
                addSql.Append("insert into Pchinese(");
                addSql.Append("Psid,Psnum,Pyear,Pgrade,Pclass,Pterm,Pdate)");
                addSql.Append(" values (");
                addSql.Append("@Psid,@Psnum,@Pyear,@Pgrade,@Pclass,@Pterm,@Pdate)");
                DbHelperSQL.GetSingle(addSql.ToString(), parameters);//如果不存在本学期记录则添加
                IsOk = 2;
            }
            return IsOk;
        }

        /// <summary>
        /// 获取所有拼音成绩
        /// </summary>
        /// <param name="area"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Pterm"></param>
        /// <returns></returns>
        public DataTable ShowAllChineseApple(string area, int Sgrade, int Sclass,int Pterm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Pid,Psnum,Ptotal,Pspeed,Pterm,Pdate,Sgrade,Sclass,Sname ");
            strSql.Append(" FROM Pchinese,Students WHERE Psid=Sid and Pgrade=Sgrade and Pterm="+Pterm);
            switch (area)
            {
                default:
                    break;
                case "2":
                    strSql.Append(" and Sgrade=" + Sgrade);
                    break;
                case "3":
                    strSql.Append(" and Sgrade=" + Sgrade + " and Sclass=" + Sclass);
                    break;
            }
            strSql.Append("  ORDER BY Ptotal DESC,Pspeed DESC,Pdate DESC");

            return DbHelperSQL.Query(strSql.ToString()).Tables[0];

        }
		#endregion  ExtensionMethod
	}
}

