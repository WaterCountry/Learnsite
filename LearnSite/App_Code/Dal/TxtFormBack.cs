using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:TxtFormBack
	/// </summary>
	public partial class TxtFormBack
	{
		public TxtFormBack()
		{}
		#region  BasicMethod
        /// <summary>
        ///判断是否已提交
        /// </summary>
        /// <param name="Rsid"></param>
        /// <param name="Rmid"></param>
        /// <returns></returns>
        public int GetRid(string Rsid, string Rmid)
        {
            string mysql = "select Rid from TxtFormBack where Rsid=" + Rsid + " and Rmid=" + Rmid;
            return DbHelperSQL.FindNum(mysql);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.TxtFormBack model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TxtFormBack(");
			strSql.Append("Rmid,Rsnum,Rsid,Rwords,Rtime,Rip,Rscore,Ryear,Rterm,Rgrade,Rclass,Ragree)");
			strSql.Append(" values (");
			strSql.Append("@Rmid,@Rsnum,@Rsid,@Rwords,@Rtime,@Rip,@Rscore,@Ryear,@Rterm,@Rgrade,@Rclass,@Ragree)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Rmid", SqlDbType.Int,4),
					new SqlParameter("@Rsnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Rsid", SqlDbType.Int,4),
					new SqlParameter("@Rwords", SqlDbType.NText),
					new SqlParameter("@Rtime", SqlDbType.DateTime),
					new SqlParameter("@Rip", SqlDbType.NVarChar,50),
					new SqlParameter("@Rscore", SqlDbType.Int,4),
					new SqlParameter("@Ryear", SqlDbType.Int,4),
					new SqlParameter("@Rterm", SqlDbType.Int,4),
					new SqlParameter("@Rgrade", SqlDbType.Int,4),
					new SqlParameter("@Rclass", SqlDbType.Int,4),
					new SqlParameter("@Ragree", SqlDbType.Int,4)};
			parameters[0].Value = model.Rmid;
			parameters[1].Value = model.Rsnum;
			parameters[2].Value = model.Rsid;
			parameters[3].Value = model.Rwords;
			parameters[4].Value = model.Rtime;
			parameters[5].Value = model.Rip;
			parameters[6].Value = model.Rscore;
			parameters[7].Value = model.Ryear;
			parameters[8].Value = model.Rterm;
			parameters[9].Value = model.Rgrade;
			parameters[10].Value = model.Rclass;
			parameters[11].Value = model.Ragree;

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

        public void UpdateAllScore(string Ryear,string Rgrade,string Rclass,string Rmid, int Rscore)
        {
            string mysql = "update TxtFormBack set Rscore=" + Rscore + " where Rmid=" + Rmid + " and Ryear=" + Ryear + " and Rgrade=" + Rgrade + " and Rclass=" + Rclass;
            DbHelperSQL.ExecuteSql(mysql);
        }

        public void UpdateScore(int Rid,int Rscore)
        {
            string mysql = "update TxtFormBack set Rscore=Rscore+" + Rscore + " where Rid=" + Rid;
            DbHelperSQL.ExecuteSql(mysql);
        }
        public void UpdateAgree(int Rid)
        {
            string mysql = "update TxtFormBack set Ragree=Ragree+1 where Rid=" + Rid;
            DbHelperSQL.ExecuteSql(mysql);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.TxtFormBack model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TxtFormBack set ");
			strSql.Append("Rmid=@Rmid,");
			strSql.Append("Rsnum=@Rsnum,");
			strSql.Append("Rsid=@Rsid,");
			strSql.Append("Rwords=@Rwords,");
			strSql.Append("Rtime=@Rtime,");
			strSql.Append("Rip=@Rip,");
			strSql.Append("Rscore=@Rscore,");
			strSql.Append("Ryear=@Ryear,");
			strSql.Append("Rterm=@Rterm,");
			strSql.Append("Rgrade=@Rgrade,");
			strSql.Append("Rclass=@Rclass,");
			strSql.Append("Ragree=@Ragree");
			strSql.Append(" where Rid=@Rid");
			SqlParameter[] parameters = {
					new SqlParameter("@Rmid", SqlDbType.Int,4),
					new SqlParameter("@Rsnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Rsid", SqlDbType.Int,4),
					new SqlParameter("@Rwords", SqlDbType.NText),
					new SqlParameter("@Rtime", SqlDbType.DateTime),
					new SqlParameter("@Rip", SqlDbType.NVarChar,50),
					new SqlParameter("@Rscore", SqlDbType.Int,4),
					new SqlParameter("@Ryear", SqlDbType.Int,4),
					new SqlParameter("@Rterm", SqlDbType.Int,4),
					new SqlParameter("@Rgrade", SqlDbType.Int,4),
					new SqlParameter("@Rclass", SqlDbType.Int,4),
					new SqlParameter("@Ragree", SqlDbType.Int,4),
					new SqlParameter("@Rid", SqlDbType.Int,4)};
			parameters[0].Value = model.Rmid;
			parameters[1].Value = model.Rsnum;
			parameters[2].Value = model.Rsid;
			parameters[3].Value = model.Rwords;
			parameters[4].Value = model.Rtime;
			parameters[5].Value = model.Rip;
			parameters[6].Value = model.Rscore;
			parameters[7].Value = model.Ryear;
			parameters[8].Value = model.Rterm;
			parameters[9].Value = model.Rgrade;
			parameters[10].Value = model.Rclass;
			parameters[11].Value = model.Ragree;
			parameters[12].Value = model.Rid;

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
        /// 更新内容 Rwords, Rtime,Rid
        /// </summary>
        public bool UpdateContent(LearnSite.Model.TxtFormBack model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TxtFormBack set ");
            strSql.Append("Rwords=@Rwords,");
            strSql.Append("Rtime=@Rtime ");
            strSql.Append(" where Rid=@Rid and Rscore=0");
            SqlParameter[] parameters = {
					new SqlParameter("@Rwords", SqlDbType.NText),
					new SqlParameter("@Rtime", SqlDbType.DateTime),
					new SqlParameter("@Rid", SqlDbType.Int,4)};
            parameters[0].Value = model.Rwords;
            parameters[1].Value = model.Rtime;
            parameters[2].Value = model.Rid;

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
		public bool Delete(int Rid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TxtFormBack ");
			strSql.Append(" where Rid=@Rid");
			SqlParameter[] parameters = {
					new SqlParameter("@Rid", SqlDbType.Int,4)
			};
			parameters[0].Value = Rid;

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
		public bool DeleteList(string Ridlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TxtFormBack ");
			strSql.Append(" where Rid in ("+Ridlist + ")  ");
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
		public LearnSite.Model.TxtFormBack GetModel(int Rid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Rid,Rmid,Rsnum,Rsid,Rwords,Rtime,Rip,Rscore,Ryear,Rterm,Rgrade,Rclass,Ragree from TxtFormBack ");
			strSql.Append(" where Rid=@Rid");
			SqlParameter[] parameters = {
					new SqlParameter("@Rid", SqlDbType.Int,4)
			};
			parameters[0].Value = Rid;

			LearnSite.Model.TxtFormBack model=new LearnSite.Model.TxtFormBack();
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
		public LearnSite.Model.TxtFormBack DataRowToModel(DataRow row)
		{
			LearnSite.Model.TxtFormBack model=new LearnSite.Model.TxtFormBack();
			if (row != null)
			{
				if(row["Rid"]!=null && row["Rid"].ToString()!="")
				{
					model.Rid=int.Parse(row["Rid"].ToString());
				}
				if(row["Rmid"]!=null && row["Rmid"].ToString()!="")
				{
					model.Rmid=int.Parse(row["Rmid"].ToString());
				}
				if(row["Rsnum"]!=null)
				{
					model.Rsnum=row["Rsnum"].ToString();
				}
				if(row["Rsid"]!=null && row["Rsid"].ToString()!="")
				{
					model.Rsid=int.Parse(row["Rsid"].ToString());
				}
				if(row["Rwords"]!=null)
				{
					model.Rwords=row["Rwords"].ToString();
				}
				if(row["Rtime"]!=null && row["Rtime"].ToString()!="")
				{
					model.Rtime=DateTime.Parse(row["Rtime"].ToString());
				}
				if(row["Rip"]!=null)
				{
					model.Rip=row["Rip"].ToString();
				}
				if(row["Rscore"]!=null && row["Rscore"].ToString()!="")
				{
					model.Rscore=int.Parse(row["Rscore"].ToString());
				}
				if(row["Ryear"]!=null && row["Ryear"].ToString()!="")
				{
					model.Ryear=int.Parse(row["Ryear"].ToString());
				}
				if(row["Rterm"]!=null && row["Rterm"].ToString()!="")
				{
					model.Rterm=int.Parse(row["Rterm"].ToString());
				}
				if(row["Rgrade"]!=null && row["Rgrade"].ToString()!="")
				{
					model.Rgrade=int.Parse(row["Rgrade"].ToString());
				}
				if(row["Rclass"]!=null && row["Rclass"].ToString()!="")
				{
					model.Rclass=int.Parse(row["Rclass"].ToString());
				}
				if(row["Ragree"]!=null && row["Ragree"].ToString()!="")
				{
					model.Ragree=int.Parse(row["Ragree"].ToString());
				}
			}
			return model;
		}

        /// <summary>
        /// 获得该表单本班数据列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Rmid"></param>
        /// <returns></returns>
        public DataTable GetClassTxtFormScore(int Sgrade, int Sclass, int Rmid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  Rsnum as Snum,Rscore as Score ");
            strSql.Append(" FROM TxtFormBack ");
            string strWhere = " Rmid=" + Rmid + " and Rgrade=" + Sgrade + " and Rclass=" + Sclass + " order by Rsnum ";
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListclass(string Rgrade, string Rclass, string Rmid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Rid,Rmid,Rsnum,Rsid,Rwords,Rtime,Rip,Rscore,Ryear,Rterm,Rgrade,Rclass,Ragree,Sname ");
            strSql.Append(" FROM TxtFormBack,Students ");
            string strWhere = " Rsid=Sid and Rmid=" + Rmid + " and Sgrade=" + Rgrade + " and Sclass=" + Rclass + " order by Rtime asc";
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataTable GetUndo(int  Rgrade, int  Rclass, int  Rmid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sname ");
            strSql.Append(" FROM Students ");
            strSql.Append(" where  Sid not in (");
            strSql.Append(" select Rsid FROM TxtFormBack where Rmid=@Rmid  and Rgrade=@Rgrade  and Rclass=@Rclass ");
            strSql.Append(") and Sgrade=@Rgrade  and Sclass=@Rclass ");

            SqlParameter[] parameters = {
					new SqlParameter("@Rgrade", SqlDbType.Int,4),                                        
					new SqlParameter("@Rclass", SqlDbType.Int,4),
					new SqlParameter("@Rmid", SqlDbType.Int,4)};
            parameters[0].Value = Rgrade;
            parameters[1].Value = Rclass;
            parameters[2].Value = Rmid;

            return DbHelperSQL.Query(strSql.ToString(),parameters).Tables[0];        
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Rid,Rmid,Rsnum,Rsid,Rwords,Rtime,Rip,Rscore,Ryear,Rterm,Rgrade,Rclass,Ragree ");
			strSql.Append(" FROM TxtFormBack ");
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
			strSql.Append(" Rid,Rmid,Rsnum,Rsid,Rwords,Rtime,Rip,Rscore,Ryear,Rterm,Rgrade,Rclass,Ragree ");
			strSql.Append(" FROM TxtFormBack ");
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
			strSql.Append("select count(1) FROM TxtFormBack ");
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
				strSql.Append("order by T.Rid desc");
			}
			strSql.Append(")AS Row, T.*  from TxtFormBack T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}


        /// <summary>
        /// 获取某学生学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowStuDoneBackCids(string Snum, int Rterm, int Rgrade)
        {
            string mysql = "SELECT DISTINCT Mcid FROM TxtForm,TxtFormBack where  Mid=Rmid and  Rsnum='" + Snum + "' and Rterm=" + Rterm + " and Rgrade=" + Rgrade;
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int n = dt.Rows.Count;
            if (n > 0)
            {
                string strtemp = "";
                for (int i = 0; i < n; i++)
                {
                    strtemp = strtemp + dt.Rows[i]["Mcid"].ToString() + ",";
                }
                return strtemp;
            }
            else
            {
                return "";
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
			parameters[0].Value = "TxtFormBack";
			parameters[1].Value = "Rid";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

