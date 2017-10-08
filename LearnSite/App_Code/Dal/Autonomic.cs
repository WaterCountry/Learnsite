using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:Autonomic
	/// </summary>
	public partial class Autonomic
	{
		public Autonomic()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Aid", "Autonomic"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Aid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Autonomic");
			strSql.Append(" where Aid=@Aid");
			SqlParameter[] parameters = {
					new SqlParameter("@Aid", SqlDbType.Int,4)
			};
			parameters[0].Value = Aid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsCheck(int Aid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Autonomic");
            strSql.Append(" where Aid=@Aid and Acheck=1");
            SqlParameter[] parameters = {
					new SqlParameter("@Aid", SqlDbType.Int,4)
			};
            parameters[0].Value = Aid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public int Exists(int Asid,int Afid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Aid from Autonomic");
            strSql.Append(" where Asid=@Asid and Afid=@Afid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Asid", SqlDbType.Int,4),
                    new SqlParameter("@Afid", SqlDbType.Int,4)
			};
            parameters[0].Value = Asid;
            parameters[1].Value = Afid;

            return DbHelperSQL.FindNum(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据作品记录
        /// Asid,Anum,Aname,Ayid,Afid,Atype,Afilename,Aurl,Alength,Adate,Aip,Ayear,Agrade,Aclass,Aterm,Aoffice
        /// </summary>
        public int AddAutonomic(LearnSite.Model.Autonomic model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Autonomic(");
            strSql.Append("Asid,Anum,Aname,Ayid,Afid,Atype,Afilename,Aurl,Alength,Adate,Aip,Ayear,Agrade,Aclass,Aterm,Aoffice)");
            strSql.Append(" values (");
            strSql.Append("@Asid,@Anum,@Aname,@Ayid,@Afid,@Atype,@Afilename,@Aurl,@Alength,@Adate,@Aip,@Ayear,@Agrade,@Aclass,@Aterm,@Aoffice)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Asid", SqlDbType.Int,4),
					new SqlParameter("@Anum", SqlDbType.NVarChar,50),
					new SqlParameter("@Aname", SqlDbType.NVarChar,50),
					new SqlParameter("@Ayid", SqlDbType.Int,4),
					new SqlParameter("@Afid", SqlDbType.Int,4),
					new SqlParameter("@Atype", SqlDbType.NVarChar,50),
					new SqlParameter("@Afilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Aurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Alength", SqlDbType.Int,4),
					new SqlParameter("@Adate", SqlDbType.DateTime),
					new SqlParameter("@Aip", SqlDbType.NVarChar,50),
					new SqlParameter("@Ayear", SqlDbType.Int,4),
					new SqlParameter("@Agrade", SqlDbType.Int,4),
					new SqlParameter("@Aclass", SqlDbType.Int,4),
					new SqlParameter("@Aterm", SqlDbType.Int,4),
					new SqlParameter("@Aoffice", SqlDbType.Bit,1)};
            parameters[0].Value = model.Asid;
            parameters[1].Value = model.Anum;
            parameters[2].Value = model.Aname;
            parameters[3].Value = model.Ayid;
            parameters[4].Value = model.Afid;
            parameters[5].Value = model.Atype;
            parameters[6].Value = model.Afilename;
            parameters[7].Value = model.Aurl;
            parameters[8].Value = model.Alength;
            parameters[9].Value = model.Adate;
            parameters[10].Value = model.Aip;
            parameters[11].Value = model.Ayear;
            parameters[12].Value = model.Agrade;
            parameters[13].Value = model.Aclass;
            parameters[14].Value = model.Aterm;
            parameters[15].Value = model.Aoffice;

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
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Autonomic model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Autonomic(");
			strSql.Append("Asid,Anum,Aname,Ayid,Afid,Atype,Afilename,Aurl,Alength,Ascore,Adate,Aip,Avote,Aegg,Acheck,Aself,Agood,Ayear,Agrade,Aclass,Aterm,Ahit,Aoffice,Aflash,Aerror)");
			strSql.Append(" values (");
			strSql.Append("@Asid,@Anum,@Aname,@Ayid,@Afid,@Atype,@Afilename,@Aurl,@Alength,@Ascore,@Adate,@Aip,@Avote,@Aegg,@Acheck,@Aself,@Agood,@Ayear,@Agrade,@Aclass,@Aterm,@Ahit,@Aoffice,@Aflash,@Aerror)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Asid", SqlDbType.Int,4),
					new SqlParameter("@Anum", SqlDbType.NVarChar,50),
					new SqlParameter("@Aname", SqlDbType.NVarChar,50),
					new SqlParameter("@Ayid", SqlDbType.Int,4),
					new SqlParameter("@Afid", SqlDbType.Int,4),
					new SqlParameter("@Atype", SqlDbType.NVarChar,50),
					new SqlParameter("@Afilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Aurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Alength", SqlDbType.Int,4),
					new SqlParameter("@Ascore", SqlDbType.Int,4),
					new SqlParameter("@Adate", SqlDbType.DateTime),
					new SqlParameter("@Aip", SqlDbType.NVarChar,50),
					new SqlParameter("@Avote", SqlDbType.Int,4),
					new SqlParameter("@Aegg", SqlDbType.SmallInt,2),
					new SqlParameter("@Acheck", SqlDbType.Bit,1),
					new SqlParameter("@Aself", SqlDbType.NVarChar,200),
					new SqlParameter("@Agood", SqlDbType.Bit,1),
					new SqlParameter("@Ayear", SqlDbType.Int,4),
					new SqlParameter("@Agrade", SqlDbType.Int,4),
					new SqlParameter("@Aclass", SqlDbType.Int,4),
					new SqlParameter("@Aterm", SqlDbType.Int,4),
					new SqlParameter("@Ahit", SqlDbType.Int,4),
					new SqlParameter("@Aoffice", SqlDbType.Bit,1),
					new SqlParameter("@Aflash", SqlDbType.Bit,1),
					new SqlParameter("@Aerror", SqlDbType.Bit,1)};
			parameters[0].Value = model.Asid;
			parameters[1].Value = model.Anum;
			parameters[2].Value = model.Aname;
			parameters[3].Value = model.Ayid;
			parameters[4].Value = model.Afid;
			parameters[5].Value = model.Atype;
			parameters[6].Value = model.Afilename;
			parameters[7].Value = model.Aurl;
			parameters[8].Value = model.Alength;
			parameters[9].Value = model.Ascore;
			parameters[10].Value = model.Adate;
			parameters[11].Value = model.Aip;
			parameters[12].Value = model.Avote;
			parameters[13].Value = model.Aegg;
			parameters[14].Value = model.Acheck;
			parameters[15].Value = model.Aself;
			parameters[16].Value = model.Agood;
			parameters[17].Value = model.Ayear;
			parameters[18].Value = model.Agrade;
			parameters[19].Value = model.Aclass;
			parameters[20].Value = model.Aterm;
			parameters[21].Value = model.Ahit;
			parameters[22].Value = model.Aoffice;
			parameters[23].Value = model.Aflash;
			parameters[24].Value = model.Aerror;

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
        /// 更新一条数据 Ascore,Aself
        /// </summary>
        public bool UpdateScoreSelf(int Afid, string Anum, int Ascore, string Aself)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update Autonomic set ");
            strSql.Append(" Acheck=1,");
            if (Ascore == 12)
                strSql.Append(" Agood=1,");
            strSql.Append(" Ascore=@Ascore,");
            strSql.Append(" Aself=@Aself");
            strSql.Append(" where Afid=@Afid and Anum=@Anum");
            SqlParameter[] parameters = {
					new SqlParameter("@Afid", SqlDbType.Int,4),
					new SqlParameter("@Anum", SqlDbType.NVarChar,50),
					new SqlParameter("@Ascore", SqlDbType.Int,4),
					new SqlParameter("@Aself", SqlDbType.NVarChar,200)   
                                        };
            parameters[0].Value = Afid;
            parameters[1].Value = Anum;
            parameters[2].Value = Ascore;
            parameters[3].Value = Aself;

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
        /// 更新一条数据 Atype Afilename Aurl Alength Adate Aflash Aid 
        /// </summary>
        public bool UpdateAutonomic(string Atype,string Afilename,string Aurl,int Alength,DateTime Adate,bool Aflash,int Aid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Autonomic set ");
            strSql.Append("Atype=@Atype,");
            strSql.Append("Afilename=@Afilename,");
            strSql.Append("Aurl=@Aurl,");
            strSql.Append("Alength=@Alength,");
            strSql.Append("Adate=@Adate,");
            strSql.Append("Aflash=@Aflash");
            strSql.Append(" where Aid=@Aid");
            SqlParameter[] parameters = {
					new SqlParameter("@Atype", SqlDbType.NVarChar,50),
					new SqlParameter("@Afilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Aurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Alength", SqlDbType.Int,4),
					new SqlParameter("@Adate", SqlDbType.DateTime),
					new SqlParameter("@Aflash", SqlDbType.Bit,1),
					new SqlParameter("@Aid", SqlDbType.Int,4)};
            parameters[0].Value = Atype;
            parameters[1].Value = Afilename;
            parameters[2].Value = Aurl;
            parameters[3].Value = Alength;
            parameters[4].Value = Adate;
            parameters[5].Value = Aflash;
            parameters[6].Value = Aid;

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
		public bool Update(LearnSite.Model.Autonomic model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Autonomic set ");
			strSql.Append("Asid=@Asid,");
			strSql.Append("Anum=@Anum,");
			strSql.Append("Aname=@Aname,");
			strSql.Append("Ayid=@Ayid,");
			strSql.Append("Afid=@Afid,");
			strSql.Append("Atype=@Atype,");
			strSql.Append("Afilename=@Afilename,");
			strSql.Append("Aurl=@Aurl,");
			strSql.Append("Alength=@Alength,");
			strSql.Append("Ascore=@Ascore,");
			strSql.Append("Adate=@Adate,");
			strSql.Append("Aip=@Aip,");
			strSql.Append("Avote=@Avote,");
			strSql.Append("Aegg=@Aegg,");
			strSql.Append("Acheck=@Acheck,");
			strSql.Append("Aself=@Aself,");
			strSql.Append("Agood=@Agood,");
			strSql.Append("Ayear=@Ayear,");
			strSql.Append("Agrade=@Agrade,");
			strSql.Append("Aclass=@Aclass,");
			strSql.Append("Aterm=@Aterm,");
			strSql.Append("Ahit=@Ahit,");
			strSql.Append("Aoffice=@Aoffice,");
			strSql.Append("Aflash=@Aflash,");
			strSql.Append("Aerror=@Aerror");
			strSql.Append(" where Aid=@Aid");
			SqlParameter[] parameters = {
					new SqlParameter("@Asid", SqlDbType.Int,4),
					new SqlParameter("@Anum", SqlDbType.NVarChar,50),
					new SqlParameter("@Aname", SqlDbType.NVarChar,50),
					new SqlParameter("@Ayid", SqlDbType.Int,4),
					new SqlParameter("@Afid", SqlDbType.Int,4),
					new SqlParameter("@Atype", SqlDbType.NVarChar,50),
					new SqlParameter("@Afilename", SqlDbType.NVarChar,50),
					new SqlParameter("@Aurl", SqlDbType.NVarChar,200),
					new SqlParameter("@Alength", SqlDbType.Int,4),
					new SqlParameter("@Ascore", SqlDbType.Int,4),
					new SqlParameter("@Adate", SqlDbType.DateTime),
					new SqlParameter("@Aip", SqlDbType.NVarChar,50),
					new SqlParameter("@Avote", SqlDbType.Int,4),
					new SqlParameter("@Aegg", SqlDbType.SmallInt,2),
					new SqlParameter("@Acheck", SqlDbType.Bit,1),
					new SqlParameter("@Aself", SqlDbType.NVarChar,200),
					new SqlParameter("@Agood", SqlDbType.Bit,1),
					new SqlParameter("@Ayear", SqlDbType.Int,4),
					new SqlParameter("@Agrade", SqlDbType.Int,4),
					new SqlParameter("@Aclass", SqlDbType.Int,4),
					new SqlParameter("@Aterm", SqlDbType.Int,4),
					new SqlParameter("@Ahit", SqlDbType.Int,4),
					new SqlParameter("@Aoffice", SqlDbType.Bit,1),
					new SqlParameter("@Aflash", SqlDbType.Bit,1),
					new SqlParameter("@Aerror", SqlDbType.Bit,1),
					new SqlParameter("@Aid", SqlDbType.Int,4)};
			parameters[0].Value = model.Asid;
			parameters[1].Value = model.Anum;
			parameters[2].Value = model.Aname;
			parameters[3].Value = model.Ayid;
			parameters[4].Value = model.Afid;
			parameters[5].Value = model.Atype;
			parameters[6].Value = model.Afilename;
			parameters[7].Value = model.Aurl;
			parameters[8].Value = model.Alength;
			parameters[9].Value = model.Ascore;
			parameters[10].Value = model.Adate;
			parameters[11].Value = model.Aip;
			parameters[12].Value = model.Avote;
			parameters[13].Value = model.Aegg;
			parameters[14].Value = model.Acheck;
			parameters[15].Value = model.Aself;
			parameters[16].Value = model.Agood;
			parameters[17].Value = model.Ayear;
			parameters[18].Value = model.Agrade;
			parameters[19].Value = model.Aclass;
			parameters[20].Value = model.Aterm;
			parameters[21].Value = model.Ahit;
			parameters[22].Value = model.Aoffice;
			parameters[23].Value = model.Aflash;
			parameters[24].Value = model.Aerror;
			parameters[25].Value = model.Aid;

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
		public bool Delete(int Aid)
		{			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Autonomic ");
			strSql.Append(" where Aid=@Aid");
			SqlParameter[] parameters = {
					new SqlParameter("@Aid", SqlDbType.Int,4)
			};
			parameters[0].Value = Aid;

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
        public bool Delbynum(int Afid, string Anum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Autonomic ");
            strSql.Append(" where Afid=@Afid and Anum=@Anum");
            SqlParameter[] parameters = {
					new SqlParameter("@Afid", SqlDbType.Int,4),
					new SqlParameter("@Anum", SqlDbType.NVarChar,50)};
            parameters[0].Value = Afid;
            parameters[1].Value = Anum;

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
		public bool DeleteList(string Aidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Autonomic ");
			strSql.Append(" where Aid in ("+Aidlist + ")  ");
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
        /// <param name="Asid"></param>
        /// <param name="Afid"></param>
        /// <returns></returns>
        public LearnSite.Model.Autonomic GetModel(int Asid,int Afid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Aid,Asid,Anum,Aname,Ayid,Afid,Atype,Afilename,Aurl,Alength,Ascore,Adate,Aip,Avote,Aegg,Acheck,Aself,Agood,Ayear,Agrade,Aclass,Aterm,Ahit,Aoffice,Aflash,Aerror from Autonomic ");
            strSql.Append(" where Asid=@Asid and Afid=@Afid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Asid", SqlDbType.Int,4),                    
					new SqlParameter("@Afid", SqlDbType.Int,4)
			};
            parameters[0].Value = Asid;
            parameters[1].Value = Afid;

            LearnSite.Model.Autonomic model = new LearnSite.Model.Autonomic();
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
		public LearnSite.Model.Autonomic GetModel(int Aid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Aid,Asid,Anum,Aname,Ayid,Afid,Atype,Afilename,Aurl,Alength,Ascore,Adate,Aip,Avote,Aegg,Acheck,Aself,Agood,Ayear,Agrade,Aclass,Aterm,Ahit,Aoffice,Aflash,Aerror from Autonomic ");
			strSql.Append(" where Aid=@Aid");
			SqlParameter[] parameters = {
					new SqlParameter("@Aid", SqlDbType.Int,4)
			};
			parameters[0].Value = Aid;

			LearnSite.Model.Autonomic model=new LearnSite.Model.Autonomic();
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
		public LearnSite.Model.Autonomic DataRowToModel(DataRow row)
		{
			LearnSite.Model.Autonomic model=new LearnSite.Model.Autonomic();
			if (row != null)
			{
				if(row["Aid"]!=null && row["Aid"].ToString()!="")
				{
					model.Aid=int.Parse(row["Aid"].ToString());
				}
				if(row["Asid"]!=null && row["Asid"].ToString()!="")
				{
					model.Asid=int.Parse(row["Asid"].ToString());
				}
				if(row["Anum"]!=null)
				{
					model.Anum=row["Anum"].ToString();
				}
				if(row["Aname"]!=null)
				{
					model.Aname=row["Aname"].ToString();
				}
				if(row["Ayid"]!=null && row["Ayid"].ToString()!="")
				{
					model.Ayid=int.Parse(row["Ayid"].ToString());
				}
				if(row["Afid"]!=null && row["Afid"].ToString()!="")
				{
					model.Afid=int.Parse(row["Afid"].ToString());
				}
				if(row["Atype"]!=null)
				{
					model.Atype=row["Atype"].ToString();
				}
				if(row["Afilename"]!=null)
				{
					model.Afilename=row["Afilename"].ToString();
				}
				if(row["Aurl"]!=null)
				{
					model.Aurl=row["Aurl"].ToString();
				}
				if(row["Alength"]!=null && row["Alength"].ToString()!="")
				{
					model.Alength=int.Parse(row["Alength"].ToString());
				}
				if(row["Ascore"]!=null && row["Ascore"].ToString()!="")
				{
					model.Ascore=int.Parse(row["Ascore"].ToString());
				}
				if(row["Adate"]!=null && row["Adate"].ToString()!="")
				{
					model.Adate=DateTime.Parse(row["Adate"].ToString());
				}
				if(row["Aip"]!=null)
				{
					model.Aip=row["Aip"].ToString();
				}
				if(row["Avote"]!=null && row["Avote"].ToString()!="")
				{
					model.Avote=int.Parse(row["Avote"].ToString());
				}
				if(row["Aegg"]!=null && row["Aegg"].ToString()!="")
				{
					model.Aegg=int.Parse(row["Aegg"].ToString());
				}
				if(row["Acheck"]!=null && row["Acheck"].ToString()!="")
				{
					if((row["Acheck"].ToString()=="1")||(row["Acheck"].ToString().ToLower()=="true"))
					{
						model.Acheck=true;
					}
					else
					{
						model.Acheck=false;
					}
				}
				if(row["Aself"]!=null)
				{
					model.Aself=row["Aself"].ToString();
				}
				if(row["Agood"]!=null && row["Agood"].ToString()!="")
				{
					if((row["Agood"].ToString()=="1")||(row["Agood"].ToString().ToLower()=="true"))
					{
						model.Agood=true;
					}
					else
					{
						model.Agood=false;
					}
				}
				if(row["Ayear"]!=null && row["Ayear"].ToString()!="")
				{
					model.Ayear=int.Parse(row["Ayear"].ToString());
				}
				if(row["Agrade"]!=null && row["Agrade"].ToString()!="")
				{
					model.Agrade=int.Parse(row["Agrade"].ToString());
				}
				if(row["Aclass"]!=null && row["Aclass"].ToString()!="")
				{
					model.Aclass=int.Parse(row["Aclass"].ToString());
				}
				if(row["Aterm"]!=null && row["Aterm"].ToString()!="")
				{
					model.Aterm=int.Parse(row["Aterm"].ToString());
				}
				if(row["Ahit"]!=null && row["Ahit"].ToString()!="")
				{
					model.Ahit=int.Parse(row["Ahit"].ToString());
				}
				if(row["Aoffice"]!=null && row["Aoffice"].ToString()!="")
				{
					if((row["Aoffice"].ToString()=="1")||(row["Aoffice"].ToString().ToLower()=="true"))
					{
						model.Aoffice=true;
					}
					else
					{
						model.Aoffice=false;
					}
				}
				if(row["Aflash"]!=null && row["Aflash"].ToString()!="")
				{
					if((row["Aflash"].ToString()=="1")||(row["Aflash"].ToString().ToLower()=="true"))
					{
						model.Aflash=true;
					}
					else
					{
						model.Aflash=false;
					}
				}
				if(row["Aerror"]!=null && row["Aerror"].ToString()!="")
				{
					if((row["Aerror"].ToString()=="1")||(row["Aerror"].ToString().ToLower()=="true"))
					{
						model.Aerror=true;
					}
					else
					{
						model.Aerror=false;
					}
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
			strSql.Append("select Aid,Asid,Anum,Aname,Ayid,Afid,Atype,Afilename,Aurl,Alength,Ascore,Adate,Aip,Avote,Aegg,Acheck,Aself,Agood,Ayear,Agrade,Aclass,Aterm,Ahit,Aoffice,Aflash,Aerror ");
			strSql.Append(" FROM Autonomic ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获得数据列表获取最优秀的20条作品
        /// </summary>
        public DataSet GetListGoodByYid(int Yid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct top 20 Aid,Aname,Atype,Afilename,Aurl,Ascore,Adate,Agood,Ahit,Ftitle ");
            strSql.Append(" FROM Autonomic,Soft ");
            strSql.Append(" where Ayid=@Yid and Afid=Fid and Agood=1");
            strSql.Append(" order by Adate desc,Agood desc");
            SqlParameter[] parameters = {
					new SqlParameter("@Yid", SqlDbType.Int,4)
			};
            parameters[0].Value = Yid;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByYid(int Yid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct Aid,Aname,Atype,Afilename,Aurl,Ascore,Adate,Agood,Ahit,Ftitle ");
            strSql.Append(" FROM Autonomic,Soft ");
            strSql.Append(" where Ayid=@Yid and Afid=Fid");
            strSql.Append(" order by Adate desc,Agood desc");
            SqlParameter[] parameters = {
					new SqlParameter("@Yid", SqlDbType.Int,4)
			};
            parameters[0].Value = Yid;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得有作品提交的分类项数据列表，按序号和编号排序
        /// </summary>
        public DataTable GetListCategory()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct  Yid,Ytitle,Ysort ");
            strSql.Append(" FROM SoftCategory,Autonomic ");
            strSql.Append(" where Yid=Ayid  ");
            strSql.Append(" order by Ysort ASC,Yid ASC ");

            return DbHelperSQL.Query(strSql.ToString()).Tables[0]; 
        }
        /// <summary>
        /// 根据Yid分类编号，获得num条数据列表
        /// Aid,Aname,Atype,Aurl,Adate,Agood,Ftitle
        /// </summary>
        public DataTable GetListTop(int Ayid, int num)
        {
            StringBuilder strSql = new StringBuilder();
            string aa = "select  TOP " + num + "  Aid,Aname,Atype,Aurl,Adate,Agood,Ftitle ";
            strSql.Append(aa);
            strSql.Append(" FROM Autonomic,Soft ");
            strSql.Append(" WHERE Ayid=@Ayid and Afid=Fid ");
            strSql.Append(" ORDER BY Aid desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@Ayid", SqlDbType.Int,4)
			};
            parameters[0].Value = Ayid;

            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
        }
        /// <summary>
        /// 根据Sid学生编号，获得所有作品数据列表
        /// Aid,Aurl,Ftitle
        /// </summary>
        public DataTable GetMyList(int Asid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Aid,Aurl,Ftitle ");
            strSql.Append(" FROM Autonomic,Soft ");
            strSql.Append(" WHERE Asid=@Asid and Afid=Fid ");
            strSql.Append(" ORDER BY Aid desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@Asid", SqlDbType.Int,4)
			};
            parameters[0].Value = Asid;

            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
        }
        /// <summary>
        /// 获取展示用的学生未评自学作品列表
        /// </summary>
        /// <param name="Afid"></param>
        /// <returns></returns>
        public DataTable GetListCircleNomic(int Afid)
        {
            string mysql = "select Aname,Aurl from Autonomic where Acheck=0 and Afid="+Afid+" order by Aid desc";
            return DbHelperSQL.Query(mysql).Tables[0];
        }
        /// <summary>
        /// 获取自学作品的学分和评语
        /// </summary>
        /// <param name="Afid"></param>
        /// <param name="Anum"></param>
        /// <returns></returns>
        public string[] GetScoreSelf(int Afid, string Anum)
        {
            string[] tem = { "", "" };
            string mysql = "select Ascore,Aself from Autonomic where Acheck=1 and Afid=" + Afid + " and Anum='" + Anum + "'";
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Ascore"] != null)
                    tem[0] = dt.Rows[0]["Ascore"].ToString();
                if (dt.Rows[0]["Aself"] != null)
                    tem[1] = dt.Rows[0]["Aself"].ToString();
            }
            return tem;
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
			strSql.Append(" Aid,Asid,Anum,Aname,Ayid,Afid,Atype,Afilename,Aurl,Alength,Ascore,Adate,Aip,Avote,Aegg,Acheck,Aself,Agood,Ayear,Agrade,Aclass,Aterm,Ahit,Aoffice,Aflash,Aerror ");
			strSql.Append(" FROM Autonomic ");
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
			strSql.Append("select count(1) FROM Autonomic ");
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
				strSql.Append("order by T.Aid desc");
			}
			strSql.Append(")AS Row, T.*  from Autonomic T ");
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
			parameters[0].Value = "Autonomic";
			parameters[1].Value = "Aid";
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

