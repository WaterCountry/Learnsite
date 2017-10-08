
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:ShareDisk
	/// </summary>
	public partial class ShareDisk
	{
		public ShareDisk()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Kid", "ShareDisk"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Kid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ShareDisk");
			strSql.Append(" where Kid=@Kid");
			SqlParameter[] parameters = {
					new SqlParameter("@Kid", SqlDbType.Int,4)
			};
			parameters[0].Value = Kid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LearnSite.Model.ShareDisk model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ShareDisk(");
            strSql.Append("Kown,Kyear,Kgrade,Kclass,Kgroup,Knum,Kname,Kfilename,Kfsize,Kfurl,Kftpe,Kfdate)");
            strSql.Append(" values (");
            strSql.Append("@Kown,@Kyear,@Kgrade,@Kclass,@Kgroup,@Knum,@Kname,@Kfilename,@Kfsize,@Kfurl,@Kftpe,@Kfdate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Kown", SqlDbType.Bit,1),
					new SqlParameter("@Kyear", SqlDbType.Int,4),
					new SqlParameter("@Kgrade", SqlDbType.Int,4),
					new SqlParameter("@Kclass", SqlDbType.Int,4),
					new SqlParameter("@Kgroup", SqlDbType.Int,4),
					new SqlParameter("@Knum", SqlDbType.NVarChar,50),
					new SqlParameter("@Kname", SqlDbType.NVarChar,50),
					new SqlParameter("@Kfilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Kfsize", SqlDbType.Int,4),
					new SqlParameter("@Kfurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Kftpe", SqlDbType.NVarChar,50),
					new SqlParameter("@Kfdate", SqlDbType.DateTime)};
            parameters[0].Value = model.Kown;
            parameters[1].Value = model.Kyear;
            parameters[2].Value = model.Kgrade;
            parameters[3].Value = model.Kclass;
            parameters[4].Value = model.Kgroup;
            parameters[5].Value = model.Knum;
            parameters[6].Value = model.Kname;
            parameters[7].Value = model.Kfilename;
            parameters[8].Value = model.Kfsize;
            parameters[9].Value = model.Kfurl;
            parameters[10].Value = model.Kftpe;
            parameters[11].Value = model.Kfdate;

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
        public bool Update(LearnSite.Model.ShareDisk model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShareDisk set ");
            strSql.Append("Kown=@Kown,");
            strSql.Append("Kyear=@Kyear,");
            strSql.Append("Kgrade=@Kgrade,");
            strSql.Append("Kclass=@Kclass,");
            strSql.Append("Kgroup=@Kgroup,");
            strSql.Append("Knum=@Knum,");
            strSql.Append("Kname=@Kname,");
            strSql.Append("Kfilename=@Kfilename,");
            strSql.Append("Kfsize=@Kfsize,");
            strSql.Append("Kfurl=@Kfurl,");
            strSql.Append("Kftpe=@Kftpe,");
            strSql.Append("Kfdate=@Kfdate");
            strSql.Append(" where Kid=@Kid");
            SqlParameter[] parameters = {
					new SqlParameter("@Kown", SqlDbType.Bit,1),
					new SqlParameter("@Kyear", SqlDbType.Int,4),
					new SqlParameter("@Kgrade", SqlDbType.Int,4),
					new SqlParameter("@Kclass", SqlDbType.Int,4),
					new SqlParameter("@Kgroup", SqlDbType.Int,4),
					new SqlParameter("@Knum", SqlDbType.NVarChar,50),
					new SqlParameter("@Kname", SqlDbType.NVarChar,50),
					new SqlParameter("@Kfilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Kfsize", SqlDbType.Int,4),
					new SqlParameter("@Kfurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Kftpe", SqlDbType.NVarChar,50),
					new SqlParameter("@Kfdate", SqlDbType.DateTime),
					new SqlParameter("@Kid", SqlDbType.Int,4)};
            parameters[0].Value = model.Kown;
            parameters[1].Value = model.Kyear;
            parameters[2].Value = model.Kgrade;
            parameters[3].Value = model.Kclass;
            parameters[4].Value = model.Kgroup;
            parameters[5].Value = model.Knum;
            parameters[6].Value = model.Kname;
            parameters[7].Value = model.Kfilename;
            parameters[8].Value = model.Kfsize;
            parameters[9].Value = model.Kfurl;
            parameters[10].Value = model.Kftpe;
            parameters[11].Value = model.Kfdate;
            parameters[12].Value = model.Kid;

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
		public bool Delete(int Kid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ShareDisk ");
			strSql.Append(" where Kid=@Kid");
			SqlParameter[] parameters = {
					new SqlParameter("@Kid", SqlDbType.Int,4)
			};
			parameters[0].Value = Kid;

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
        /// 删除一条数据，暂时采用些方法，下学期换上面的
        /// </summary>
        public bool Deletefile(string Knum,string Kfurl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ShareDisk ");
            strSql.Append(" where Knum=@Knum and Kfurl=@Kfurl");
            SqlParameter[] parameters = {
					new SqlParameter("@Knum", SqlDbType.NVarChar,50),
					new SqlParameter("@Kfurl", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = Knum;
            parameters[1].Value =Kfurl;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Kidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ShareDisk ");
			strSql.Append(" where Kid in ("+Kidlist + ")  ");
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
        public LearnSite.Model.ShareDisk GetModel(int Kid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Kid,Kown,Kyear,Kgrade,Kclass,Kgroup,Knum,Kname,Kfilename,Kfsize,Kfurl,Kftpe,Kfdate from ShareDisk ");
            strSql.Append(" where Kid=@Kid");
            SqlParameter[] parameters = {
					new SqlParameter("@Kid", SqlDbType.Int,4)
			};
            parameters[0].Value = Kid;

            LearnSite.Model.ShareDisk model = new LearnSite.Model.ShareDisk();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public LearnSite.Model.ShareDisk DataRowToModel(DataRow row)
        {
            LearnSite.Model.ShareDisk model = new LearnSite.Model.ShareDisk();
            if (row != null)
            {
                if (row["Kid"] != null && row["Kid"].ToString() != "")
                {
                    model.Kid = int.Parse(row["Kid"].ToString());
                }
                if (row["Kown"] != null && row["Kown"].ToString() != "")
                {
                    if ((row["Kown"].ToString() == "1") || (row["Kown"].ToString().ToLower() == "true"))
                    {
                        model.Kown = true;
                    }
                    else
                    {
                        model.Kown = false;
                    }
                }
                if (row["Kyear"] != null && row["Kyear"].ToString() != "")
                {
                    model.Kyear = int.Parse(row["Kyear"].ToString());
                }
                if (row["Kgrade"] != null && row["Kgrade"].ToString() != "")
                {
                    model.Kgrade = int.Parse(row["Kgrade"].ToString());
                }
                if (row["Kclass"] != null && row["Kclass"].ToString() != "")
                {
                    model.Kclass = int.Parse(row["Kclass"].ToString());
                }
                if (row["Kgroup"] != null && row["Kgroup"].ToString() != "")
                {
                    model.Kgroup = int.Parse(row["Kgroup"].ToString());
                }
                if (row["Knum"] != null)
                {
                    model.Knum = row["Knum"].ToString();
                }
                if (row["Kname"] != null)
                {
                    model.Kname = row["Kname"].ToString();
                }
                if (row["Kfilename"] != null)
                {
                    model.Kfilename = row["Kfilename"].ToString();
                }
                if (row["Kfsize"] != null && row["Kfsize"].ToString() != "")
                {
                    model.Kfsize = int.Parse(row["Kfsize"].ToString());
                }
                if (row["Kfurl"] != null)
                {
                    model.Kfurl = row["Kfurl"].ToString();
                }
                if (row["Kftpe"] != null)
                {
                    model.Kftpe = row["Kftpe"].ToString();
                }
                if (row["Kfdate"] != null && row["Kfdate"].ToString() != "")
                {
                    model.Kfdate = DateTime.Parse(row["Kfdate"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Kid,Kown,Kyear,Kgrade,Kclass,Kgroup,Knum,Kname,Kfilename,Kfsize,Kfurl,Kftpe,Kfdate ");
            strSql.Append(" FROM ShareDisk ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetSnumList(string Knum)
        {
            string today = DateTime.Now.ToShortDateString();
            string strWhere = "Kown=0 and Knum='" + Knum + "' and Kfdate>'" + today + "'";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Kid,Kown,Kyear,Kgrade,Kclass,Kgroup,Knum,Kname,Kfilename,LEFT([Kfilename],8) as KfnameShort,Kfsize,Kfurl,Kftpe,Kfdate ");
            strSql.Append(" FROM ShareDisk ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetSgroupList(string Kgroup)
        {
            string today = DateTime.Now.ToShortDateString();
            string strWhere = "Kown=1 and Kgroup='" + Kgroup + "' and Kfdate>'" + today + "'";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Kid,Kown,Kyear,Kgrade,Kclass,Kgroup,Knum,Kname,Kfilename,LEFT([Kfilename],8) as KfnameShort,Kfsize,Kfurl,Kftpe,Kfdate ");
            strSql.Append(" FROM ShareDisk ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
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
            strSql.Append(" Kid,Kown,Kyear,Kgrade,Kclass,Kgroup,Knum,Kname,Kfilename,Kfsize,Kfurl,Kftpe,Kfdate ");
            strSql.Append(" FROM ShareDisk ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM ShareDisk ");
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
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Kid desc");
            }
            strSql.Append(")AS Row, T.*  from ShareDisk T ");
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
			parameters[0].Value = "ShareDisk";
			parameters[1].Value = "Kid";
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

