using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类StudentsExcel。
	/// </summary>
	public class StudentsExcel
	{
		public StudentsExcel()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Sid", "StudentsExcel"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Sid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from StudentsExcel");
			strSql.Append(" where Sid=@Sid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4)};
			parameters[0].Value = Sid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.StudentsExcel model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StudentsExcel(");
            strSql.Append("Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape)");
            strSql.Append(" values (");
            strSql.Append("@Snum,@Syear,@Sgrade,@Sclass,@Sname,@Spwd,@Sex,@Saddress,@Sphone,@Sparents,@Sheadtheacher,@Sscore,@Squiz,@Sattitude,@Sape)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Snum", SqlDbType.NVarChar,50),
					new SqlParameter("@Syear", SqlDbType.Int,4),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),
					new SqlParameter("@Sname", SqlDbType.NVarChar,50),
					new SqlParameter("@Spwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NVarChar,2),
					new SqlParameter("@Saddress", SqlDbType.NVarChar,200),
					new SqlParameter("@Sphone", SqlDbType.NVarChar,50),
					new SqlParameter("@Sparents", SqlDbType.NVarChar,50),
					new SqlParameter("@Sheadtheacher", SqlDbType.NVarChar,50),
					new SqlParameter("@Sscore", SqlDbType.Int,4),
					new SqlParameter("@Squiz", SqlDbType.Int,4),
					new SqlParameter("@Sattitude", SqlDbType.Int,4),
					new SqlParameter("@Sape", SqlDbType.NVarChar,1)};
            parameters[0].Value = model.Snum;
            parameters[1].Value = model.Syear;
            parameters[2].Value = model.Sgrade;
            parameters[3].Value = model.Sclass;
            parameters[4].Value = model.Sname;
            parameters[5].Value = model.Spwd;
            parameters[6].Value = model.Sex;
            parameters[7].Value = model.Saddress;
            parameters[8].Value = model.Sphone;
            parameters[9].Value = model.Sparents;
            parameters[10].Value = model.Sheadtheacher;
            parameters[11].Value = model.Sscore;
            parameters[12].Value = model.Squiz;
            parameters[13].Value = model.Sattitude;
            parameters[14].Value = model.Sape;

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
        /// 导入一条数据
        /// </summary>
        public int AddFromExcelDs(LearnSite.Model.StudentsExcel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StudentsExcel(");
            strSql.Append("Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher)");
            strSql.Append(" values (");
            strSql.Append("@Snum,@Syear,@Sgrade,@Sclass,@Sname,@Spwd,@Sex,@Saddress,@Sphone,@Sparents,@Sheadtheacher)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Snum", SqlDbType.NVarChar,50),
					new SqlParameter("@Syear", SqlDbType.Int,4),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),
					new SqlParameter("@Sname", SqlDbType.NVarChar,50),
					new SqlParameter("@Spwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NVarChar,2),
					new SqlParameter("@Saddress", SqlDbType.NVarChar,200),
					new SqlParameter("@Sphone", SqlDbType.NVarChar,50),
					new SqlParameter("@Sparents", SqlDbType.NVarChar,50),
					new SqlParameter("@Sheadtheacher", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Snum;
            parameters[1].Value = model.Syear;
            parameters[2].Value = model.Sgrade;
            parameters[3].Value = model.Sclass;
            parameters[4].Value = model.Sname;
            parameters[5].Value = model.Spwd;
            parameters[6].Value = model.Sex;
            parameters[7].Value = model.Saddress;
            parameters[8].Value = model.Sphone;
            parameters[9].Value = model.Sparents;
            parameters[10].Value = model.Sheadtheacher;

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
		public void Update(LearnSite.Model.StudentsExcel model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StudentsExcel set ");
            strSql.Append("Snum=@Snum,");
            strSql.Append("Syear=@Syear,");
            strSql.Append("Sgrade=@Sgrade,");
            strSql.Append("Sclass=@Sclass,");
            strSql.Append("Sname=@Sname,");
            strSql.Append("Spwd=@Spwd,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Saddress=@Saddress,");
            strSql.Append("Sphone=@Sphone,");
            strSql.Append("Sparents=@Sparents,");
            strSql.Append("Sheadtheacher=@Sheadtheacher,");
            strSql.Append("Sscore=@Sscore,");
            strSql.Append("Squiz=@Squiz,");
            strSql.Append("Sattitude=@Sattitude,");
            strSql.Append("Sape=@Sape");
            strSql.Append(" where Sid=@Sid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4),
					new SqlParameter("@Snum", SqlDbType.NVarChar,50),
					new SqlParameter("@Syear", SqlDbType.Int,4),
					new SqlParameter("@Sgrade", SqlDbType.Int,4),
					new SqlParameter("@Sclass", SqlDbType.Int,4),
					new SqlParameter("@Sname", SqlDbType.NVarChar,50),
					new SqlParameter("@Spwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NVarChar,2),
					new SqlParameter("@Saddress", SqlDbType.NVarChar,200),
					new SqlParameter("@Sphone", SqlDbType.NVarChar,50),
					new SqlParameter("@Sparents", SqlDbType.NVarChar,50),
					new SqlParameter("@Sheadtheacher", SqlDbType.NVarChar,50),
					new SqlParameter("@Sscore", SqlDbType.Int,4),
					new SqlParameter("@Squiz", SqlDbType.Int,4),
					new SqlParameter("@Sattitude", SqlDbType.Int,4),
					new SqlParameter("@Sape", SqlDbType.NVarChar,1)};
            parameters[0].Value = model.Sid;
            parameters[1].Value = model.Snum;
            parameters[2].Value = model.Syear;
            parameters[3].Value = model.Sgrade;
            parameters[4].Value = model.Sclass;
            parameters[5].Value = model.Sname;
            parameters[6].Value = model.Spwd;
            parameters[7].Value = model.Sex;
            parameters[8].Value = model.Saddress;
            parameters[9].Value = model.Sphone;
            parameters[10].Value = model.Sparents;
            parameters[11].Value = model.Sheadtheacher;
            parameters[12].Value = model.Sscore;
            parameters[13].Value = model.Squiz;
            parameters[14].Value = model.Sattitude;
            parameters[15].Value = model.Sape;


			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Sid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from StudentsExcel ");
			strSql.Append(" where Sid=@Sid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4)};
			parameters[0].Value = Sid;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.StudentsExcel GetModel(int Sid)
		{

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Sid,Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape from StudentsExcel ");
            strSql.Append(" where Sid=@Sid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Sid", SqlDbType.Int,4)};
            parameters[0].Value = Sid;

            LearnSite.Model.StudentsExcel model = new LearnSite.Model.StudentsExcel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Sid"].ToString() != "")
                {
                    model.Sid = int.Parse(ds.Tables[0].Rows[0]["Sid"].ToString());
                }
                model.Snum = ds.Tables[0].Rows[0]["Snum"].ToString();
                if (ds.Tables[0].Rows[0]["Syear"].ToString() != "")
                {
                    model.Syear = int.Parse(ds.Tables[0].Rows[0]["Syear"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sgrade"].ToString() != "")
                {
                    model.Sgrade = int.Parse(ds.Tables[0].Rows[0]["Sgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sclass"].ToString() != "")
                {
                    model.Sclass = int.Parse(ds.Tables[0].Rows[0]["Sclass"].ToString());
                }
                model.Sname = ds.Tables[0].Rows[0]["Sname"].ToString();
                model.Spwd = ds.Tables[0].Rows[0]["Spwd"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                model.Saddress = ds.Tables[0].Rows[0]["Saddress"].ToString();
                model.Sphone = ds.Tables[0].Rows[0]["Sphone"].ToString();
                model.Sparents = ds.Tables[0].Rows[0]["Sparents"].ToString();
                model.Sheadtheacher = ds.Tables[0].Rows[0]["Sheadtheacher"].ToString();
                if (ds.Tables[0].Rows[0]["Sscore"].ToString() != "")
                {
                    model.Sscore = int.Parse(ds.Tables[0].Rows[0]["Sscore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Squiz"].ToString() != "")
                {
                    model.Squiz = int.Parse(ds.Tables[0].Rows[0]["Squiz"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sattitude"].ToString() != "")
                {
                    model.Sattitude = int.Parse(ds.Tables[0].Rows[0]["Sattitude"].ToString());
                }
                model.Sape = ds.Tables[0].Rows[0]["Sape"].ToString();
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
            strSql.Append("select Sid,Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape ");
            strSql.Append(" FROM StudentsExcel ");
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
            strSql.Append(" Sid,Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Squiz,Sattitude,Sape ");
            strSql.Append(" FROM StudentsExcel ");
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
			parameters[0].Value = "StudentsExcel";
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

