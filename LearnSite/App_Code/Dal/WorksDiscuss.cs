using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类WorksDiscuss。
	/// </summary>
	public class WorksDiscuss
	{
		public WorksDiscuss()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Did)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from WorksDiscuss");
			strSql.Append(" where Did=@Did ");
			SqlParameter[] parameters = {
					new SqlParameter("@Did", SqlDbType.Int,4)};
			parameters[0].Value = Did;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsDiscuss(int Dwid,string Dsnum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WorksDiscuss");
            strSql.Append(" where Dwid=@Dwid and Dsnum=@Dsnum");
            SqlParameter[] parameters = {
					new SqlParameter("@Dwid", SqlDbType.Int,4),
                    new SqlParameter("@Dsnum", SqlDbType.NVarChar,50)};
            parameters[0].Value = Dwid;
            parameters[1].Value = Dsnum;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.WorksDiscuss model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into WorksDiscuss(");
			strSql.Append("Dwid,Dsnum,Dwords,Dtime,Dip)");
			strSql.Append(" values (");
			strSql.Append("@Dwid,@Dsnum,@Dwords,@Dtime,@Dip)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Dwid", SqlDbType.Int,4),
					new SqlParameter("@Dsnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Dwords", SqlDbType.NText),
					new SqlParameter("@Dtime", SqlDbType.DateTime),
					new SqlParameter("@Dip", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Dwid;
			parameters[1].Value = model.Dsnum;
			parameters[2].Value = model.Dwords;
			parameters[3].Value = model.Dtime;
			parameters[4].Value = model.Dip;

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
		public void Update(LearnSite.Model.WorksDiscuss model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update WorksDiscuss set ");
			strSql.Append("Dwid=@Dwid,");
			strSql.Append("Dsnum=@Dsnum,");
			strSql.Append("Dwords=@Dwords,");
			strSql.Append("Dtime=@Dtime,");
			strSql.Append("Dip=@Dip");
			strSql.Append(" where Did=@Did ");
			SqlParameter[] parameters = {
					new SqlParameter("@Did", SqlDbType.Int,4),
					new SqlParameter("@Dwid", SqlDbType.Int,4),
					new SqlParameter("@Dsnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Dwords", SqlDbType.NText),
					new SqlParameter("@Dtime", SqlDbType.DateTime),
					new SqlParameter("@Dip", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Did;
			parameters[1].Value = model.Dwid;
			parameters[2].Value = model.Dsnum;
			parameters[3].Value = model.Dwords;
			parameters[4].Value = model.Dtime;
			parameters[5].Value = model.Dip;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Did)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WorksDiscuss ");
			strSql.Append(" where Did=@Did ");
			SqlParameter[] parameters = {
					new SqlParameter("@Did", SqlDbType.Int,4)};
			parameters[0].Value = Did;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.WorksDiscuss GetModel(int Did)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Did,Dwid,Dsnum,Dwords,Dtime,Dip from WorksDiscuss ");
			strSql.Append(" where Did=@Did ");
			SqlParameter[] parameters = {
					new SqlParameter("@Did", SqlDbType.Int,4)};
			parameters[0].Value = Did;

			LearnSite.Model.WorksDiscuss model=new LearnSite.Model.WorksDiscuss();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Did"].ToString()!="")
				{
					model.Did=int.Parse(ds.Tables[0].Rows[0]["Did"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Dwid"].ToString()!="")
				{
					model.Dwid=int.Parse(ds.Tables[0].Rows[0]["Dwid"].ToString());
				}
				model.Dsnum=ds.Tables[0].Rows[0]["Dsnum"].ToString();
				model.Dwords=ds.Tables[0].Rows[0]["Dwords"].ToString();
				if(ds.Tables[0].Rows[0]["Dtime"].ToString()!="")
				{
					model.Dtime=DateTime.Parse(ds.Tables[0].Rows[0]["Dtime"].ToString());
				}
				model.Dip=ds.Tables[0].Rows[0]["Dip"].ToString();
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
			strSql.Append("select Did,Dwid,Dsnum,Dwords,Dtime,Dip ");
			strSql.Append(" FROM WorksDiscuss ");
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
			strSql.Append(" Did,Dwid,Dsnum,Dwords,Dtime,Dip ");
			strSql.Append(" FROM WorksDiscuss ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获得该作品的所有评论
        /// </summary>
        public DataSet GetDiscussList(int Dwid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" Did,Dwid,Dsnum,Dwords,Dtime,Dip,Sname ");
            strSql.Append(" FROM WorksDiscuss,Students ");
            strSql.Append(" where Dsnum=Snum and Dwid=@Dwid" );
            strSql.Append(" order by Dtime ASC");

            SqlParameter[] parameters = {
					new SqlParameter("@Dwid", SqlDbType.Int,4)};
            parameters[0].Value = Dwid;

            return DbHelperSQL.Query(strSql.ToString(),parameters);
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
			parameters[0].Value = "WorksDiscuss";
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

