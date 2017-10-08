using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类Flection。
	/// </summary>
	public class Flection
	{
		public Flection()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录Fid
		/// </summary>
		public bool Exists(int Fid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Flection");
			strSql.Append(" where Fid=@Fid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4)};
			parameters[0].Value = Fid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该记录Fcid
        /// </summary>
        public bool ExistsFcid(int Fcid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Flection");
            strSql.Append(" where Fcid=@Fcid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Fcid", SqlDbType.Int,4)};
            parameters[0].Value = Fcid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Flection model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Flection(");
            strSql.Append("Fcid,Fhid,Fcontent,Fdate)");
            strSql.Append(" values (");
            strSql.Append("@Fcid,@Fhid,@Fcontent,@Fdate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Fcid", SqlDbType.Int,4),
					new SqlParameter("@Fhid", SqlDbType.Int,4),
					new SqlParameter("@Fcontent", SqlDbType.NText),
					new SqlParameter("@Fdate", SqlDbType.DateTime)};
            parameters[0].Value = model.Fcid;
            parameters[1].Value = model.Fhid;
            parameters[2].Value = model.Fcontent;
            parameters[3].Value = model.Fdate;

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
		public void Update(LearnSite.Model.Flection model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Flection set ");
            strSql.Append("Fcontent=@Fcontent");
            strSql.Append(" where Fcid=@Fcid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Fcid", SqlDbType.Int,4),
					new SqlParameter("@Fcontent", SqlDbType.NText)};

            parameters[0].Value = model.Fcid;
            parameters[1].Value = model.Fcontent;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Fid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Flection ");
			strSql.Append(" where Fid=@Fid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4)};
			parameters[0].Value = Fid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Flection GetModel(int Fcid)
		{

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Fid,Fcid,Fhid,Fcontent,Fdate from Flection ");
            strSql.Append(" where Fcid=@Fcid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Fcid", SqlDbType.Int,4)};
            parameters[0].Value = Fcid;

            LearnSite.Model.Flection model = new LearnSite.Model.Flection();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Fid"].ToString() != "")
                {
                    model.Fid = int.Parse(ds.Tables[0].Rows[0]["Fid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fcid"].ToString() != "")
                {
                    model.Fcid = int.Parse(ds.Tables[0].Rows[0]["Fcid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fhid"].ToString() != "")
                {
                    model.Fhid = int.Parse(ds.Tables[0].Rows[0]["Fhid"].ToString());
                }
                model.Fcontent = ds.Tables[0].Rows[0]["Fcontent"].ToString();
                if (ds.Tables[0].Rows[0]["Fdate"].ToString() != "")
                {
                    model.Fdate = DateTime.Parse(ds.Tables[0].Rows[0]["Fdate"].ToString());
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
            strSql.Append("select Fid,Fcid,Fhid,Fcontent,Fdate ");
            strSql.Append(" FROM Flection ");
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
            strSql.Append(" Fid,Fcid,Fhid,Fcontent,Fdate ");
            strSql.Append(" FROM Flection ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            parameters[0].Value = "Flection";
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
