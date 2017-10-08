using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//请先添加引用
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类Soft。
	/// </summary>
	public class Soft
	{
		public Soft()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Fid", "Soft"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Fid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Soft");
			strSql.Append(" where Fid=@Fid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4)};
			parameters[0].Value = Fid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Soft model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Soft(");
            strSql.Append("Ftitle,Fcontent,Furl,Fhit,Fdate,Ffiletype,Fclass,Fhide,Fopen,Fhid,Fyid,Fup)");
            strSql.Append(" values (");
            strSql.Append("@Ftitle,@Fcontent,@Furl,@Fhit,@Fdate,@Ffiletype,@Fclass,@Fhide,@Fopen,@Fhid,@Fyid,@Fup)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Ftitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Fcontent", SqlDbType.NText),
					new SqlParameter("@Furl", SqlDbType.NVarChar,200),
					new SqlParameter("@Fhit", SqlDbType.Int,4),
					new SqlParameter("@Fdate", SqlDbType.DateTime),
					new SqlParameter("@Ffiletype", SqlDbType.NVarChar,50),
					new SqlParameter("@Fclass", SqlDbType.NVarChar,50),
					new SqlParameter("@Fhide", SqlDbType.Bit,1),
					new SqlParameter("@Fopen", SqlDbType.Int,4),
                    new SqlParameter("@Fhid", SqlDbType.Int,4),
                    new SqlParameter("@Fyid", SqlDbType.Int,4),
                    new SqlParameter("@Fup", SqlDbType.Bit,1)};
            parameters[0].Value = model.Ftitle;
            parameters[1].Value = model.Fcontent;
            parameters[2].Value = model.Furl;
            parameters[3].Value = model.Fhit;
            parameters[4].Value = model.Fdate;
            parameters[5].Value = model.Ffiletype;
            parameters[6].Value = model.Fclass;
            parameters[7].Value = model.Fhide;
            parameters[8].Value = model.Fopen;
            parameters[9].Value = model.Fhid;
            parameters[10].Value = model.Fyid;
            parameters[11].Value = model.Fup;

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
        public bool Update(LearnSite.Model.Soft model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Soft set ");
            strSql.Append("Ftitle=@Ftitle,");
            strSql.Append("Fcontent=@Fcontent,");
            strSql.Append("Furl=@Furl,");
            strSql.Append("Fhit=@Fhit,");
            strSql.Append("Fdate=@Fdate,");
            strSql.Append("Ffiletype=@Ffiletype,");
            strSql.Append("Fclass=@Fclass,");
            strSql.Append("Fhide=@Fhide,");
            strSql.Append("Fopen=@Fopen,");
            strSql.Append("Fhid=@Fhid,");
            strSql.Append("Fyid=@Fyid,");
            strSql.Append("Fup=@Fup");
            strSql.Append(" where Fid=@Fid");
            SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4),
					new SqlParameter("@Ftitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Fcontent", SqlDbType.NText),
					new SqlParameter("@Furl", SqlDbType.NVarChar,200),
					new SqlParameter("@Fhit", SqlDbType.Int,4),
					new SqlParameter("@Fdate", SqlDbType.DateTime),
					new SqlParameter("@Ffiletype", SqlDbType.NVarChar,50),
					new SqlParameter("@Fclass", SqlDbType.NVarChar,50),
					new SqlParameter("@Fhide", SqlDbType.Bit,1),
					new SqlParameter("@Fopen", SqlDbType.Int,4),
                    new SqlParameter("@Fhid", SqlDbType.Int,4),
                    new SqlParameter("@Fyid", SqlDbType.Int,4),
                    new SqlParameter("@Fup", SqlDbType.Bit,1)};
            parameters[0].Value = model.Fid;
            parameters[1].Value = model.Ftitle;
            parameters[2].Value = model.Fcontent;
            parameters[3].Value = model.Furl;
            parameters[4].Value = model.Fhit;
            parameters[5].Value = model.Fdate;
            parameters[6].Value = model.Ffiletype;
            parameters[7].Value = model.Fclass;
            parameters[8].Value = model.Fhide;
            parameters[9].Value = model.Fopen;
            parameters[10].Value = model.Fhid;
            parameters[11].Value = model.Fyid;
            parameters[12].Value = model.Fup;

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
        public bool Delete(int Fid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Soft ");
            strSql.Append(" where Fid=@Fid");
            SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4)};
            parameters[0].Value = Fid;

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
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Soft GetModel(int Fid)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Fid,Ftitle,Fcontent,Furl,Fhit,Fdate,Ffiletype,Fclass,Fhide,Fopen,Fhid,Fyid from Soft ");
            strSql.Append(" where Fid=@Fid");
            SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4)
};
            parameters[0].Value = Fid;

            LearnSite.Model.Soft model = new LearnSite.Model.Soft();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Fid"].ToString() != "")
                {
                    model.Fid = int.Parse(ds.Tables[0].Rows[0]["Fid"].ToString());
                }
                model.Ftitle = ds.Tables[0].Rows[0]["Ftitle"].ToString();
                model.Fcontent = ds.Tables[0].Rows[0]["Fcontent"].ToString();
                model.Furl = ds.Tables[0].Rows[0]["Furl"].ToString();
                if (ds.Tables[0].Rows[0]["Fhit"].ToString() != "")
                {
                    model.Fhit = int.Parse(ds.Tables[0].Rows[0]["Fhit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fdate"].ToString() != "")
                {
                    model.Fdate = DateTime.Parse(ds.Tables[0].Rows[0]["Fdate"].ToString());
                }
                model.Ffiletype = ds.Tables[0].Rows[0]["Ffiletype"].ToString();
                model.Fclass = ds.Tables[0].Rows[0]["Fclass"].ToString();
                if (ds.Tables[0].Rows[0]["Fhide"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fhide"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fhide"].ToString().ToLower() == "true"))
                    {
                        model.Fhide = true;
                    }
                    else
                    {
                        model.Fhide = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fopen"].ToString() != "")
                {
                    model.Fopen = int.Parse(ds.Tables[0].Rows[0]["Fopen"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fhid"].ToString() != "")
                {
                    model.Fhid = int.Parse(ds.Tables[0].Rows[0]["Fhid"].ToString());
                } 
                if (ds.Tables[0].Rows[0]["Fyid"].ToString() != "")
                {
                    model.Fyid = int.Parse(ds.Tables[0].Rows[0]["Fyid"].ToString());
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
            strSql.Append("select Fid,Ftitle,Fcontent,Furl,Fhit,Fdate,Ffiletype,Fclass,Fhide,Fopen,Fhid,Fyid ");
            strSql.Append(" FROM Soft ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得资源列表，无简介
        /// </summary>
        public DataSet GetSoftList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Fid,Ftitle,Furl,Fhit,Fdate,Ffiletype,Fclass,Fhide,Fopen,Fhid,Fyid ");
            strSql.Append(" FROM Soft ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Fid DESC");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 资源是否发布状态更新
        /// </summary>
        /// <param name="Fid"></param>
        public void UpdateFhide(int Fid)
        {
            string strsql = "update Soft set Fhide=Fhide^1 where Fid="+Fid;
            DbHelperSQL.ExecuteSql(strsql);
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
            strSql.Append(" Fid,Ftitle,Fcontent,Furl,Fhit,Fdate,Ffiletype,Fclass,Fhide,Fopen,Fhid,Fyid ");
			strSql.Append(" FROM Soft ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获取有提交作品的本分类资源列表
        /// </summary>
        /// <param name="Fyid"></param>
        /// <returns></returns>
        public DataTable GetListnomic(int Fyid)
        {
            string mysql = "select Fid,Ftitle from Soft where Fup=1 and Fyid="+Fyid+" order by Fyid asc";
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        /// 获得有作品提交的分类项数据列表，按序号和编号排序
        /// </summary>
        public DataTable GetListCategory()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct  Yid,Ytitle,Ysort ");
            strSql.Append(" FROM SoftCategory,Soft ");
            strSql.Append(" where Yid=Fyid and Fup=1  ");
            strSql.Append(" order by Ysort ASC,Yid ASC ");

            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
        /// <summary>
        /// 是否可下载资源（是否限制下载时间）
        /// </summary>
        /// <returns></returns>
        public  bool IsDownCan()
        {
            bool candown = false;
            int downtime = LearnSite.Common.XmlHelp.GetDowntime()-1;
            int passtime =LearnSite.Common.Computer.TimePassed();
            if (LearnSite.Common.XmlHelp.DowncanIs())
            {
                if (passtime > downtime )
                {
                    candown = true;
                }
            }
            else
            {
                candown = true;//如果不限制下载
            }            
            return candown;
        }

        /// <summary>
        /// 更新点击率
        /// </summary>
        /// <param name="Fid"></param>
        /// <param name="Fhit"></param>
        public void UpdateFhit(int Fid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Soft set ");
            strSql.Append("Fhit=Fhit+1");
            strSql.Append(" where Fid=@Fid");
            SqlParameter[] parameters = {
					new SqlParameter("@Fid", SqlDbType.Int,4)};
            parameters[0].Value = Fid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该类别的资源
        /// </summary>
        /// <param name="Fyid"></param>
        /// <returns></returns>
        public bool ExistYid(int Fyid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Soft");
            strSql.Append(" where Fyid=@Fyid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Fyid", SqlDbType.Int,4)};
            parameters[0].Value = Fyid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
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
			parameters[0].Value = "Soft";
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

