using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:House
	/// </summary>
	public partial class House
	{
		public House()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Hid", "House"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Hid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from House");
			strSql.Append(" where Hid=@Hid");
			SqlParameter[] parameters = {
					new SqlParameter("@Hid", SqlDbType.Int,4)
};
			parameters[0].Value = Hid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.House model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into House(");
			strSql.Append("Hname,Hseat)");
			strSql.Append(" values (");
			strSql.Append("@Hname,@Hseat)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Hname", SqlDbType.NVarChar,50),
					new SqlParameter("@Hseat", SqlDbType.NText)};
			parameters[0].Value = model.Hname;
			parameters[1].Value = model.Hseat;

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
        public bool UpdateHseat(int Hid, string Hseat)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update House set ");
            strSql.Append("Hseat=@Hseat");
            strSql.Append(" where Hid=@Hid");
            SqlParameter[] parameters = {
					new SqlParameter("@Hseat", SqlDbType.NText),
					new SqlParameter("@Hid", SqlDbType.Int,4)};
            parameters[0].Value = Hseat;
            parameters[1].Value = Hid;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.House model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update House set ");
			strSql.Append("Hname=@Hname,");
			strSql.Append("Hseat=@Hseat");
			strSql.Append(" where Hid=@Hid");
			SqlParameter[] parameters = {
					new SqlParameter("@Hname", SqlDbType.NVarChar,50),
					new SqlParameter("@Hseat", SqlDbType.NText),
					new SqlParameter("@Hid", SqlDbType.Int,4)};
			parameters[0].Value = model.Hname;
			parameters[1].Value = model.Hseat;
			parameters[2].Value = model.Hid;

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
		public bool Delete(int Hid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from House ");
			strSql.Append(" where Hid=@Hid");
			SqlParameter[] parameters = {
					new SqlParameter("@Hid", SqlDbType.Int,4)
};
			parameters[0].Value = Hid;

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
		public bool DeleteList(string Hidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from House ");
			strSql.Append(" where Hid in ("+Hidlist + ")  ");
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
        public LearnSite.Model.House GetModel(string Hname)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Hid,Hname,Hseat from House ");
            strSql.Append(" where Hname=@Hname");
            SqlParameter[] parameters = {
					new SqlParameter("@Hname", SqlDbType.NVarChar,50)
};
            parameters[0].Value = Hname;

            LearnSite.Model.House model = new LearnSite.Model.House();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Hid"].ToString() != "")
                {
                    model.Hid = int.Parse(ds.Tables[0].Rows[0]["Hid"].ToString());
                }
                model.Hname = ds.Tables[0].Rows[0]["Hname"].ToString();
                model.Hseat = ds.Tables[0].Rows[0]["Hseat"].ToString();
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
		public LearnSite.Model.House GetModel(int Hid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Hid,Hname,Hseat from House ");
			strSql.Append(" where Hid=@Hid");
			SqlParameter[] parameters = {
					new SqlParameter("@Hid", SqlDbType.Int,4)
};
			parameters[0].Value = Hid;

			LearnSite.Model.House model=new LearnSite.Model.House();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Hid"].ToString()!="")
				{
					model.Hid=int.Parse(ds.Tables[0].Rows[0]["Hid"].ToString());
				}
				model.Hname=ds.Tables[0].Rows[0]["Hname"].ToString();
				model.Hseat=ds.Tables[0].Rows[0]["Hseat"].ToString();
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
			strSql.Append("select Hid,Hname,Hseat ");
			strSql.Append(" FROM House ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListHouse()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Hid,Hname ");
            strSql.Append(" FROM House ");
            strSql.Append(" order by Hid asc");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public string GetHseat(int Hid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Hseat ");
            strSql.Append(" FROM House ");
            strSql.Append(" where Hid=" + Hid);
            return DbHelperSQL.FindString(strSql.ToString());
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
			strSql.Append(" Hid,Hname,Hseat ");
			strSql.Append(" FROM House ");
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
			parameters[0].Value = "House";
			parameters[1].Value = "Hid";
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

