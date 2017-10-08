using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:Computers
	/// </summary>
	public class Computers
	{
		public Computers()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Pid", "Computers"); 
		}
        public void initPxy()
        {
            string mysql = "update Computers set Px=0,Py=0 where Px is null";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 获取电脑室名称
        /// </summary>
        /// <returns></returns>
        public DataTable CmpRoom()
        {
            string mysql = "select distinct Pm from Computers where Pm is not null";
            return DbHelperSQL.Query(mysql).Tables[0];
        }

        /// <summary>
        /// 根据老师获取电脑室的ＩＰ、主机名和坐标
        /// select Pip,Pmachine,Px,Py from Computers
        /// </summary>
        /// <param name="Ph"></param>
        /// <returns></returns>
        public DataTable GetSeat(string Pm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Pip,Pmachine,Px,Py from Computers");
            strSql.Append(" where Pm=@Pm ");
            strSql.Append(" order by Px asc");
            SqlParameter[] parameters = {
					new SqlParameter("@Pm",SqlDbType.NVarChar,50)};
            parameters[0].Value = Pm;

            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsIp(string Pip)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Computers");
            strSql.Append(" where Pip=@Pip ");
            SqlParameter[] parameters = {
					new SqlParameter("@Pip", SqlDbType.NVarChar,50)};
            parameters[0].Value = Pip;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录，返回主机名
        /// </summary>
        public string GetmachineByIp(string Pip)
        {
            string strSql = "select top 1 Pmachine from Computers where Pip='" + Pip + "' order by Pid desc";
            return DbHelperSQL.FindString(strSql);
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Pid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Computers");
			strSql.Append(" where Pid=@Pid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4)};
			parameters[0].Value = Pid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否有未绑定的主机名IP
        /// </summary>
        /// <returns></returns>
        public string ExistPlock(string Pip)
        {
            string mysql = "select Pid from Computers where Plock=0 and Pip='"+Pip +"'";
            return DbHelperSQL.FindString(mysql);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Computers model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Computers(");
            strSql.Append("Pip,Pmachine,Plock,Pdate)");
            strSql.Append(" values (");
            strSql.Append("@Pip,@Pmachine,@Plock,@Pdate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Pip", SqlDbType.NVarChar,50),
					new SqlParameter("@Pmachine", SqlDbType.NVarChar,50),
					new SqlParameter("@Plock", SqlDbType.Bit,1),
					new SqlParameter("@Pdate", SqlDbType.DateTime)};
            parameters[0].Value = model.Pip;
            parameters[1].Value = model.Pmachine;
            parameters[2].Value = model.Plock;
            parameters[3].Value = model.Pdate;

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
        public int AddModel(LearnSite.Model.Computers model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Computers(");
            strSql.Append("Pip,Pmachine,Plock,Pdate,Px,Py,Pm)");
            strSql.Append(" values (");
            strSql.Append("@Pip,@Pmachine,@Plock,@Pdate,@Px,@Py,@Pm)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Pip", SqlDbType.NVarChar,50),
					new SqlParameter("@Pmachine", SqlDbType.NVarChar,50),
					new SqlParameter("@Plock", SqlDbType.Bit,1),
					new SqlParameter("@Pdate", SqlDbType.DateTime),
					new SqlParameter("@Px", SqlDbType.Int,4),
					new SqlParameter("@Py", SqlDbType.Int,4),
					new SqlParameter("@Pm", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Pip;
            parameters[1].Value = model.Pmachine;
            parameters[2].Value = model.Plock;
            parameters[3].Value = model.Pdate;
            parameters[4].Value = model.Px;
            parameters[5].Value = model.Py;
            parameters[6].Value = model.Pm;

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
        /// 将表中Plock值取反，更新
        /// </summary>
        /// <param name="Pid"></param>
        public void UpLock(int Pid)
        {
            string mysql = "update Computers set Plock=Plock^1 where Pid="+Pid;
            DbHelperSQL.ExecuteSql(mysql);
        }

        /// <summary>
        /// 将表中Plock值为0，即解锁
        /// </summary>
        public void UnLockAll()
        {
            string mysql = "update Computers set Plock=0 ";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 将表中Plock值为0，即解锁
        /// </summary>
        public void OnLockAll()
        {
            string mysql = "update Computers set Plock=1 ";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 根据IP更新电脑室名和电脑坐标
        /// </summary>
        public bool UpdateIpPxPy(string Pip, int Px, int Py, string Pm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Computers set ");
            strSql.Append("Px=@Px,Py=@Py,Pm=@Pm ");
            strSql.Append(" where Pip=@Pip");
            SqlParameter[] parameters = {
					new SqlParameter("@Pip", SqlDbType.NVarChar,50),
					new SqlParameter("@Px", SqlDbType.Int,4),                    
					new SqlParameter("@Py", SqlDbType.Int,4),                      
					new SqlParameter("@Pm", SqlDbType.NVarChar,50)};
            parameters[0].Value = Pip;
            parameters[1].Value = Px;
            parameters[2].Value = Py;
            parameters[3].Value = Pm;

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
        /// 根据IP更新主机名
        /// </summary>
        public bool UpdateIp(string Pip,string Pmachine)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Computers set ");
            strSql.Append("Pmachine=@Pmachine ");
            strSql.Append(" where Pip=@Pip");
            SqlParameter[] parameters = {
					new SqlParameter("@Pip", SqlDbType.NVarChar,50),
					new SqlParameter("@Pmachine", SqlDbType.NVarChar,50)};
            parameters[0].Value = Pip;
            parameters[1].Value = Pmachine;

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
        /// 根据Pid更新主机名
        /// </summary>
        public bool UpdateByPid(int Pid,string Pmachine)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Computers set Plock=1,");
            strSql.Append("Pmachine=@Pmachine ");
            strSql.Append(" where Pid=@Pid");
            SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@Pmachine", SqlDbType.NVarChar,50)};
            parameters[0].Value = Pid;
            parameters[1].Value = Pmachine;

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
		public bool Update(LearnSite.Model.Computers model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Computers set ");
			strSql.Append("Pip=@Pip,");
			strSql.Append("Pmachine=@Pmachine,");
			strSql.Append("Plock=@Plock,");
			strSql.Append("Pdate=@Pdate");
			strSql.Append(" where Pid=@Pid");
			SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@Pip", SqlDbType.NVarChar,50),
					new SqlParameter("@Pmachine", SqlDbType.NVarChar,50),
					new SqlParameter("@Plock", SqlDbType.Bit,1),
					new SqlParameter("@Pdate", SqlDbType.DateTime)};
			parameters[0].Value = model.Pid;
			parameters[1].Value = model.Pip;
			parameters[2].Value = model.Pmachine;
			parameters[3].Value = model.Plock;
			parameters[4].Value = model.Pdate;

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
        /// 根据Pid更新一条数据Pmachine,Plock,Pdate
        /// </summary>
        public bool UpdateMachine(LearnSite.Model.Computers model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Computers set ");
            strSql.Append("Pmachine=@Pmachine,");
            strSql.Append("Plock=@Plock,");
            strSql.Append("Pdate=@Pdate");
            strSql.Append(" where Pid=@Pid");
            SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@Pmachine", SqlDbType.NVarChar,50),
					new SqlParameter("@Plock", SqlDbType.Bit,1),
					new SqlParameter("@Pdate", SqlDbType.DateTime)};
            parameters[0].Value = model.Pid;
            parameters[1].Value = model.Pmachine;
            parameters[2].Value = model.Plock;
            parameters[3].Value = model.Pdate;

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
        /// 删除所有数据
        /// </summary>
        public bool DeleteAll()
        {
            string strSql = "delete from Computers ";
            int rows = DbHelperSQL.ExecuteSql(strSql);
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
        /// 删除该日期之前的记录
        /// </summary>
        /// <param name="pdate"></param>
        /// <returns></returns>
        public bool DeleteThis(string pdate)
        {
            string strSql = "delete from Computers where Pdate<'" + pdate + "'";
            int rows = DbHelperSQL.ExecuteSql(strSql);
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
		public bool Delete(int Pid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Computers ");
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
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Pidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Computers ");
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
		public LearnSite.Model.Computers GetModel(int Pid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Pid,Pip,Pmachine,Plock,Pdate from Computers ");
			strSql.Append(" where Pid=@Pid");
			SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4)
};
			parameters[0].Value = Pid;

			LearnSite.Model.Computers model=new LearnSite.Model.Computers();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Pid"].ToString()!="")
				{
					model.Pid=int.Parse(ds.Tables[0].Rows[0]["Pid"].ToString());
				}
				model.Pip=ds.Tables[0].Rows[0]["Pip"].ToString();
				model.Pmachine=ds.Tables[0].Rows[0]["Pmachine"].ToString();
				if(ds.Tables[0].Rows[0]["Plock"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Plock"].ToString()=="1")||(ds.Tables[0].Rows[0]["Plock"].ToString().ToLower()=="true"))
					{
						model.Plock=true;
					}
					else
					{
						model.Plock=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Pdate"].ToString()!="")
				{
					model.Pdate=DateTime.Parse(ds.Tables[0].Rows[0]["Pdate"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 根据Pip得到一个对象实体
        /// </summary>
        public LearnSite.Model.Computers GetModelByIp(string Pip)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Pid,Pip,Pmachine,Plock,Pdate from Computers ");
            strSql.Append(" where Pip=@Pip");
            SqlParameter[] parameters = {
					new SqlParameter("@Pip",  SqlDbType.NVarChar,50)
};
            parameters[0].Value = Pip;

            LearnSite.Model.Computers model = new LearnSite.Model.Computers();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Pid"].ToString() != "")
                {
                    model.Pid = int.Parse(ds.Tables[0].Rows[0]["Pid"].ToString());
                }
                model.Pip = ds.Tables[0].Rows[0]["Pip"].ToString();
                model.Pmachine = ds.Tables[0].Rows[0]["Pmachine"].ToString();
                if (ds.Tables[0].Rows[0]["Plock"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Plock"].ToString() == "1") || (ds.Tables[0].Rows[0]["Plock"].ToString().ToLower() == "true"))
                    {
                        model.Plock = true;
                    }
                    else
                    {
                        model.Plock = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Pdate"].ToString() != "")
                {
                    model.Pdate = DateTime.Parse(ds.Tables[0].Rows[0]["Pdate"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Pid,Pip,Pmachine,Plock,Pdate ");
			strSql.Append(" FROM Computers ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获得数据列表，条件排序
        /// </summary>
        public DataSet GetListOrder(string strorder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Pid,Pip,Pmachine,Plock,Pdate,Pm ");
            strSql.Append(" FROM Computers ");
            if (strorder.Trim() != "")
            {
                strSql.Append(strorder);
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
			strSql.Append(" Pid,Pip,Pmachine,Plock,Pdate ");
			strSql.Append(" FROM Computers ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获取IP与主机名对应表
        /// </summary>
        /// <returns></returns>
        public DataTable GetPipPmachine()
        {
            string sql = "select Pip,Pmachine from Computers order by Pip asc,Pid desc";
            return DbHelperSQL.Query(sql).Tables[0];
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
			parameters[0].Value = "Computers";
			parameters[1].Value = "";
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

