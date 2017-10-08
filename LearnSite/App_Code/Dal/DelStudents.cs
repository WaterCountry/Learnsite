using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:DelStudents
	/// </summary>
	public class DelStudents
	{
		public DelStudents()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Did", "DelStudents"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Did)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DelStudents");
			strSql.Append(" where Did=@Did");
			SqlParameter[] parameters = {
					new SqlParameter("@Did", SqlDbType.Int,4)
			};
			parameters[0].Value = Did;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该学号
        /// </summary>
        public bool ExistsDnum(string Dnum)
        {
            string strSql = "select count(1) from DelStudents where Dnum='" + Dnum + "'";
            return DbHelperSQL.Exists(strSql);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.DelStudents model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DelStudents(");
			strSql.Append("Dnum,Dyear,Dgrade,Dclass,Dname,Dsex,Daddress,Dphone,Dparents,Dheadtheacher)");
			strSql.Append(" values (");
			strSql.Append("@Dnum,@Dyear,@Dgrade,@Dclass,@Dname,@Dsex,@Daddress,@Dphone,@Dparents,@Dheadtheacher)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Dnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Dyear", SqlDbType.Int,4),
					new SqlParameter("@Dgrade", SqlDbType.Int,4),
					new SqlParameter("@Dclass", SqlDbType.Int,4),
					new SqlParameter("@Dname", SqlDbType.NVarChar,50),
					new SqlParameter("@Dsex", SqlDbType.NVarChar,2),
					new SqlParameter("@Daddress", SqlDbType.NVarChar,200),
					new SqlParameter("@Dphone", SqlDbType.NVarChar,50),
					new SqlParameter("@Dparents", SqlDbType.NVarChar,50),
					new SqlParameter("@Dheadtheacher", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Dnum;
			parameters[1].Value = model.Dyear;
			parameters[2].Value = model.Dgrade;
			parameters[3].Value = model.Dclass;
			parameters[4].Value = model.Dname;
			parameters[5].Value = model.Dsex;
			parameters[6].Value = model.Daddress;
			parameters[7].Value = model.Dphone;
			parameters[8].Value = model.Dparents;
			parameters[9].Value = model.Dheadtheacher;

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
		public bool Update(LearnSite.Model.DelStudents model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DelStudents set ");
			strSql.Append("Dnum=@Dnum,");
			strSql.Append("Dyear=@Dyear,");
			strSql.Append("Dgrade=@Dgrade,");
			strSql.Append("Dclass=@Dclass,");
			strSql.Append("Dname=@Dname,");
			strSql.Append("Dsex=@Dsex,");
			strSql.Append("Daddress=@Daddress,");
			strSql.Append("Dphone=@Dphone,");
			strSql.Append("Dparents=@Dparents,");
			strSql.Append("Dheadtheacher=@Dheadtheacher");
			strSql.Append(" where Did=@Did");
			SqlParameter[] parameters = {
					new SqlParameter("@Dnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Dyear", SqlDbType.Int,4),
					new SqlParameter("@Dgrade", SqlDbType.Int,4),
					new SqlParameter("@Dclass", SqlDbType.Int,4),
					new SqlParameter("@Dname", SqlDbType.NVarChar,50),
					new SqlParameter("@Dsex", SqlDbType.NVarChar,2),
					new SqlParameter("@Daddress", SqlDbType.NVarChar,200),
					new SqlParameter("@Dphone", SqlDbType.NVarChar,50),
					new SqlParameter("@Dparents", SqlDbType.NVarChar,50),
					new SqlParameter("@Dheadtheacher", SqlDbType.NVarChar,50),
					new SqlParameter("@Did", SqlDbType.Int,4)};
			parameters[0].Value = model.Dnum;
			parameters[1].Value = model.Dyear;
			parameters[2].Value = model.Dgrade;
			parameters[3].Value = model.Dclass;
			parameters[4].Value = model.Dname;
			parameters[5].Value = model.Dsex;
			parameters[6].Value = model.Daddress;
			parameters[7].Value = model.Dphone;
			parameters[8].Value = model.Dparents;
			parameters[9].Value = model.Dheadtheacher;
			parameters[10].Value = model.Did;

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
		public bool Delete(int Did)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DelStudents ");
			strSql.Append(" where Did=@Did");
			SqlParameter[] parameters = {
					new SqlParameter("@Did", SqlDbType.Int,4)
			};
			parameters[0].Value = Did;

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
		public bool DeleteList(string Didlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DelStudents ");
			strSql.Append(" where Did in ("+Didlist + ")  ");
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
        public LearnSite.Model.DelStudents GetModel(int Did)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Did,Dnum,Dyear,Dgrade,Dclass,Dname,Dsex,Daddress,Dphone,Dparents,Dheadtheacher from DelStudents ");
            strSql.Append(" where Did=@Did");
            SqlParameter[] parameters = {
					new SqlParameter("@Did", SqlDbType.Int,4)
			};
            parameters[0].Value = Did;

            LearnSite.Model.DelStudents model = new LearnSite.Model.DelStudents();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Did"] != null && ds.Tables[0].Rows[0]["Did"].ToString() != "")
                {
                    model.Did = int.Parse(ds.Tables[0].Rows[0]["Did"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Dnum"] != null && ds.Tables[0].Rows[0]["Dnum"].ToString() != "")
                {
                    model.Dnum = ds.Tables[0].Rows[0]["Dnum"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Dyear"] != null && ds.Tables[0].Rows[0]["Dyear"].ToString() != "")
                {
                    model.Dyear = int.Parse(ds.Tables[0].Rows[0]["Dyear"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Dgrade"] != null && ds.Tables[0].Rows[0]["Dgrade"].ToString() != "")
                {
                    model.Dgrade = int.Parse(ds.Tables[0].Rows[0]["Dgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Dclass"] != null && ds.Tables[0].Rows[0]["Dclass"].ToString() != "")
                {
                    model.Dclass = int.Parse(ds.Tables[0].Rows[0]["Dclass"].ToString());
                }
                model.Dname = ds.Tables[0].Rows[0]["Dname"].ToString();
                model.Dsex = ds.Tables[0].Rows[0]["Dsex"].ToString();
                model.Daddress = ds.Tables[0].Rows[0]["Daddress"].ToString();
                model.Dphone = ds.Tables[0].Rows[0]["Dphone"].ToString();
                model.Dparents = ds.Tables[0].Rows[0]["Dparents"].ToString();
                model.Dheadtheacher = ds.Tables[0].Rows[0]["Dheadtheacher"].ToString();

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
			strSql.Append("select Did,Dnum,Dyear,Dgrade,Dclass,Dname,Dsex,Daddress,Dphone,Dparents,Dheadtheacher ");
			strSql.Append(" FROM DelStudents ");
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
			strSql.Append(" Did,Dnum,Dyear,Dgrade,Dclass,Dname,Dsex,Daddress,Dphone,Dparents,Dheadtheacher ");
			strSql.Append(" FROM DelStudents ");
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
			strSql.Append("select count(1) FROM DelStudents ");
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
				strSql.Append("order by T.Did desc");
			}
			strSql.Append(")AS Row, T.*  from DelStudents T ");
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
			parameters[0].Value = "DelStudents";
			parameters[1].Value = "Did";
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

