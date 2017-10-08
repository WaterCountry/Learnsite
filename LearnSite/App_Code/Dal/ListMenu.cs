using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using LearnSite.DBUtility;//Please add references
namespace LearnSite.DAL
{
	/// <summary>
	/// 数据访问类:ListMenu
	/// </summary>
	public partial class ListMenu
	{
		public ListMenu()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.ListMenu model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ListMenu(");
			strSql.Append("Lcid,Lsort,Ltype,Lxid,Lshow,Ltitle)");
			strSql.Append(" values (");
			strSql.Append("@Lcid,@Lsort,@Ltype,@Lxid,@Lshow,@Ltitle)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Lcid", SqlDbType.Int,4),
					new SqlParameter("@Lsort", SqlDbType.Int,4),
					new SqlParameter("@Ltype", SqlDbType.Int,4),
					new SqlParameter("@Lxid", SqlDbType.Int,4),
					new SqlParameter("@Lshow", SqlDbType.Bit,1),
					new SqlParameter("@Ltitle", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Lcid;
			parameters[1].Value = model.Lsort;
			parameters[2].Value = model.Ltype;
			parameters[3].Value = model.Lxid;
			parameters[4].Value = model.Lshow;
			parameters[5].Value = model.Ltitle;

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
        /// 更改导航次序 updown 值0向上 减，1向下　增
        /// </summary>
        /// <param name="Lid"></param>
        /// <param name="updown"></param>
        public void UpdateLsort(int Lid, bool updown)
        {
            string mysql = "update ListMenu set Lsort=Lsort-1 where Lid=" + Lid;
            if (updown)
                mysql = "update ListMenu set Lsort=Lsort+1 where Lid=" + Lid;
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 将活动的序号跟导航表的序号保持同步
        /// </summary>
        /// <param name="Lcid"></param>
        /// <param name="Lxid"></param>
        public void UpdateMissonListMene(int Lcid, int Lxid)
        {
            string mysql = "update Mission set Msort=Lsort from Mission,ListMenu where Mcid=" + Lcid + " and Mid=" + Lxid + " and Mcid=Lcid and Mid=Lxid";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 初始化序号
        /// </summary>
        /// <param name="Lcid"></param>
        public void Lsortnew(int Lcid)
        {
            string mysql = "select Lid from ListMenu where Lcid=" + Lcid + " order by Lsort";
            DataTable dt = DbHelperSQL.Query(mysql).Tables[0];
            int cn = dt.Rows.Count;
            if (cn > 0)
            {
                for (int i = 0; i < cn; i++)
                {
                    string lid = dt.Rows[i][0].ToString();
                    int ls = i + 1;
                    string sql = "update ListMenu set Lsort= " + ls + " where Lid=" + lid;
                    DbHelperSQL.ExecuteSql(sql);
                }
            }
        }
        public void UpdateLshow(int Lcid,int Lxid)
        {
            string strSql = "update ListMenu set Lshow=Lshow^1 where Lcid=" + Lcid +" and Lxid="+Lxid;
            DbHelperSQL.ExecuteSql(strSql);
        }
        public void UpdateLshow(int Lid)
        {
            string strSql = "update ListMenu set Lshow=Lshow^1 where Lid=" + Lid;
            DbHelperSQL.ExecuteSql(strSql);
        }
        public void OpenLshow(int Lid)
        {
            string strSql = "update ListMenu set Lshow=1 where Lid=" + Lid;
            DbHelperSQL.ExecuteSql(strSql);
        }
        public void CloseLshow(int Lid)
        {
            string strSql = "update ListMenu set Lshow=0 where Lid=" + Lid;
            DbHelperSQL.ExecuteSql(strSql);
        }
        /// <summary>
        /// 获取序号最大值
        /// </summary>
        /// <param name="Mcid"></param>
        /// <returns></returns>
        public int GetMaxLsort(int Lcid)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(Lsort) from ListMenu ");
            strSql.Append(" where Lcid=@Lcid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Lcid", SqlDbType.Int,4)};
            parameters[0].Value = Lcid;
            string strfind = DbHelperSQL.FindString(strSql.ToString(), parameters);
            if (strfind != "")
                result = Int32.Parse(strfind);
            return result;
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.ListMenu model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ListMenu set ");
			strSql.Append("Lcid=@Lcid,");
			strSql.Append("Lsort=@Lsort,");
			strSql.Append("Ltype=@Ltype,");
			strSql.Append("Lxid=@Lxid,");
			strSql.Append("Lshow=@Lshow,");
			strSql.Append("Ltitle=@Ltitle");
			strSql.Append(" where Lid=@Lid");
			SqlParameter[] parameters = {
					new SqlParameter("@Lcid", SqlDbType.Int,4),
					new SqlParameter("@Lsort", SqlDbType.Int,4),
					new SqlParameter("@Ltype", SqlDbType.Int,4),
					new SqlParameter("@Lxid", SqlDbType.Int,4),
					new SqlParameter("@Lshow", SqlDbType.Bit,1),
					new SqlParameter("@Ltitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Lid", SqlDbType.Int,4)};
			parameters[0].Value = model.Lcid;
			parameters[1].Value = model.Lsort;
			parameters[2].Value = model.Ltype;
			parameters[3].Value = model.Lxid;
			parameters[4].Value = model.Lshow;
			parameters[5].Value = model.Ltitle;
			parameters[6].Value = model.Lid;

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
        /// 关联更新一条数据 
        /// 条件Lcid,Lxid,Ltype 更新Ltitle
        /// </summary>
        public bool UpdateLtitle(LearnSite.Model.ListMenu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ListMenu set ");
            strSql.Append("Ltitle=@Ltitle");
            strSql.Append(" where Lcid=@Lcid and Lxid=@Lxid and Ltype=@Ltype");
            SqlParameter[] parameters = {
					new SqlParameter("@Lcid", SqlDbType.Int,4),
					new SqlParameter("@Ltype", SqlDbType.Int,4),
					new SqlParameter("@Lxid", SqlDbType.Int,4),
					new SqlParameter("@Ltitle", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Lcid;
            parameters[1].Value = model.Ltype;
            parameters[2].Value = model.Lxid;
            parameters[3].Value = model.Ltitle;

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
        /// 关联更新一条数据 
        /// 条件Lid 更新Lshow,Ltitle,Ltype
        /// </summary>
        public bool UpdateMenuMission(LearnSite.Model.ListMenu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ListMenu set ");
            strSql.Append("Lshow=@Lshow,");
            strSql.Append("Ltitle=@Ltitle,");
            strSql.Append("Ltype=@Ltype");
            strSql.Append(" where Lid=@Lid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Lid", SqlDbType.Int,4),
					new SqlParameter("@Ltype", SqlDbType.Int,4),
					new SqlParameter("@Lshow", SqlDbType.Bit,1),
					new SqlParameter("@Ltitle", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Lid;
            parameters[1].Value = model.Ltype;
            parameters[2].Value = model.Lshow;
            parameters[3].Value = model.Ltitle;

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
        /// 关联更新一条数据 
        /// 条件Lcid,Lxid,Ltype 更新Lshow,Ltitle
        /// </summary>
        public bool UpdateMenuThree(LearnSite.Model.ListMenu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ListMenu set ");
            strSql.Append("Lshow=@Lshow,");
            strSql.Append("Ltitle=@Ltitle");
            strSql.Append(" where Lcid=@Lcid and Lxid=@Lxid and Ltype=@Ltype");
            SqlParameter[] parameters = {
					new SqlParameter("@Lcid", SqlDbType.Int,4),
					new SqlParameter("@Ltype", SqlDbType.Int,4),
					new SqlParameter("@Lxid", SqlDbType.Int,4),
					new SqlParameter("@Lshow", SqlDbType.Bit,1),
					new SqlParameter("@Ltitle", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Lcid;
            parameters[1].Value = model.Ltype;
            parameters[2].Value = model.Lxid;
            parameters[3].Value = model.Lshow;
            parameters[4].Value = model.Ltitle;

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
        /// 条件联动更新一条部分数据
        ///Lid,Lshow,Ltitle
        /// </summary>
        public bool UpdateMenu(LearnSite.Model.ListMenu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ListMenu set ");
            strSql.Append("Lshow=@Lshow,");
            strSql.Append("Ltitle=@Ltitle");
            strSql.Append(" where Lid=@Lid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Lid", SqlDbType.Int,4),
					new SqlParameter("@Lshow", SqlDbType.Bit,1),
					new SqlParameter("@Ltitle", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Lid;
            parameters[1].Value = model.Lshow;
            parameters[2].Value = model.Ltitle;

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
		public bool Delete(int Lid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ListMenu ");
			strSql.Append(" where Lid=@Lid");
			SqlParameter[] parameters = {
					new SqlParameter("@Lid", SqlDbType.Int,4)
			};
			parameters[0].Value = Lid;

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
		public bool DeleteList(string Lidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ListMenu ");
			strSql.Append(" where Lid in ("+Lidlist + ")  ");
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
		public LearnSite.Model.ListMenu GetModel(int Lid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Lid,Lcid,Lsort,Ltype,Lxid,Lshow,Ltitle from ListMenu ");
			strSql.Append(" where Lid=@Lid");
			SqlParameter[] parameters = {
					new SqlParameter("@Lid", SqlDbType.Int,4)
			};
			parameters[0].Value = Lid;

			LearnSite.Model.ListMenu model=new LearnSite.Model.ListMenu();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Lid"]!=null && ds.Tables[0].Rows[0]["Lid"].ToString()!="")
				{
					model.Lid=int.Parse(ds.Tables[0].Rows[0]["Lid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Lcid"]!=null && ds.Tables[0].Rows[0]["Lcid"].ToString()!="")
				{
					model.Lcid=int.Parse(ds.Tables[0].Rows[0]["Lcid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Lsort"]!=null && ds.Tables[0].Rows[0]["Lsort"].ToString()!="")
				{
					model.Lsort=int.Parse(ds.Tables[0].Rows[0]["Lsort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ltype"]!=null && ds.Tables[0].Rows[0]["Ltype"].ToString()!="")
				{
					model.Ltype=int.Parse(ds.Tables[0].Rows[0]["Ltype"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Lxid"]!=null && ds.Tables[0].Rows[0]["Lxid"].ToString()!="")
				{
					model.Lxid=int.Parse(ds.Tables[0].Rows[0]["Lxid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Lshow"]!=null && ds.Tables[0].Rows[0]["Lshow"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Lshow"].ToString()=="1")||(ds.Tables[0].Rows[0]["Lshow"].ToString().ToLower()=="true"))
					{
						model.Lshow=true;
					}
					else
					{
						model.Lshow=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Ltitle"]!=null && ds.Tables[0].Rows[0]["Ltitle"].ToString()!="")
				{
					model.Ltitle=ds.Tables[0].Rows[0]["Ltitle"].ToString();
				}
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
        public LearnSite.Model.ListMenu GetModel(DataTable dt, int Tsort)
        {
            LearnSite.Model.ListMenu model = new LearnSite.Model.ListMenu();
            int Count = dt.Rows.Count;
            if (Count > 0)
            {
                if (Tsort < Count)
                {
                    if (dt.Rows[Tsort]["Lid"] != null && dt.Rows[Tsort]["Lid"].ToString() != "")
                    {
                        model.Lid = int.Parse(dt.Rows[Tsort]["Lid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Lcid"] != null && dt.Rows[Tsort]["Lcid"].ToString() != "")
                    {
                        model.Lcid = int.Parse(dt.Rows[Tsort]["Lcid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Lsort"] != null && dt.Rows[Tsort]["Lsort"].ToString() != "")
                    {
                        model.Lsort = int.Parse(dt.Rows[Tsort]["Lsort"].ToString());
                    }
                    if (dt.Rows[Tsort]["Ltype"] != null && dt.Rows[Tsort]["Ltype"].ToString() != "")
                    {
                        model.Ltype = int.Parse(dt.Rows[Tsort]["Ltype"].ToString());
                    }
                    if (dt.Rows[Tsort]["Lxid"] != null && dt.Rows[Tsort]["Lxid"].ToString() != "")
                    {
                        model.Lxid = int.Parse(dt.Rows[Tsort]["Lxid"].ToString());
                    }
                    if (dt.Rows[Tsort]["Lshow"] != null && dt.Rows[Tsort]["Lshow"].ToString() != "")
                    {
                        if ((dt.Rows[Tsort]["Lshow"].ToString() == "1") || (dt.Rows[Tsort]["Lshow"].ToString().ToLower() == "true"))
                        {
                            model.Lshow = true;
                        }
                        else
                        {
                            model.Lshow = false;
                        }
                    }
                    if (dt.Rows[Tsort]["Ltitle"] != null && dt.Rows[Tsort]["Ltitle"].ToString() != "")
                    {
                        model.Ltitle = dt.Rows[Tsort]["Ltitle"].ToString();
                    }
                    return model;
                }
                else
                {
                    return null;
                }
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
			strSql.Append("select Lid,Lcid,Lsort,Ltype,Lxid,Lshow,Ltitle ");
			strSql.Append(" FROM ListMenu ");
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
			strSql.Append(" Lid,Lcid,Lsort,Ltype,Lxid,Lshow,Ltitle ");
			strSql.Append(" FROM ListMenu ");
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
			strSql.Append("select count(1) FROM ListMenu ");
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
				strSql.Append("order by T.Lid desc");
			}
			strSql.Append(")AS Row, T.*  from ListMenu T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 将旧学案生成新导航
        /// </summary>
        public void initbuildmenu()
        {
            // 类型：活动1，调查2，讨论3
            string sql = "select Mid,Mtitle,Mcid,Msort,Mpublish from Mission where Mdelete=0";
            DataTable cdt = DbHelperSQL.Query(sql).Tables[0];
            int ccount = cdt.Rows.Count;
            Model.ListMenu lmode = new Model.ListMenu();
            BLL.ListMenu lbll = new BLL.ListMenu();
            if (ccount > 0)
            {
                for (int i = 0; i < ccount; i++)
                {
                    int mid = Int32.Parse(cdt.Rows[i]["Mid"].ToString());
                    string mtitle = cdt.Rows[i]["Mtitle"].ToString();
                    int mcid = Int32.Parse(cdt.Rows[i]["Mcid"].ToString());
                    int msort = Int32.Parse(cdt.Rows[i]["Msort"].ToString());
                    bool mpublish = bool.Parse(cdt.Rows[i]["Mpublish"].ToString());
                    lmode.Lcid = mcid;
                    lmode.Lsort = msort;
                    lmode.Ltype = 1;
                    lmode.Lxid = mid;
                    lmode.Lshow = mpublish;
                    lmode.Ltitle = mtitle;
                    lbll.Add(lmode);//将所有活动都添加到导航中
                }
            }

            string sqltwo = "select Vid,Vtitle,Vcid,Vclose from Survey ";
            DataTable sdt = DbHelperSQL.Query(sqltwo).Tables[0];
            int scount = sdt.Rows.Count;
            if (scount > 0)
            {
                for (int i = 0; i < scount; i++)
                {
                    int vid = Int32.Parse(sdt.Rows[i]["Vid"].ToString());
                    string vtitle = sdt.Rows[i]["Vtitle"].ToString();
                    int vcid = Int32.Parse(sdt.Rows[i]["Vcid"].ToString());
                    bool vclose = bool.Parse(sdt.Rows[i]["Vclose"].ToString());
                    lmode.Lcid = vcid;
                    lmode.Lsort = 6;
                    lmode.Ltype = 2;
                    lmode.Lxid = vid;
                    lmode.Lshow = !vclose;
                    lmode.Ltitle = vtitle;
                    lbll.Add(lmode);//将所有调查都添加到导航中
                }
            }

            string sqlthree = "select Tid,Ttitle,Tcid,Tclose from TopicDiscuss";
            DataTable tdt = DbHelperSQL.Query(sqlthree).Tables[0];
            int tcount = tdt.Rows.Count;
            if (tcount > 0)
            {
                for (int i = 0; i < tcount; i++)
                {
                    int tid = Int32.Parse(tdt.Rows[i]["Tid"].ToString());
                    string ttitle = tdt.Rows[i]["Ttitle"].ToString();
                    int tcid = Int32.Parse(tdt.Rows[i]["Tcid"].ToString());
                    bool tclose = bool.Parse(tdt.Rows[i]["Tclose"].ToString());
                    lmode.Lcid = tcid;
                    lmode.Lsort = 8;
                    lmode.Ltype = 3;
                    lmode.Lxid = tid;
                    lmode.Lshow = !tclose;
                    lmode.Ltitle = ttitle;
                    lbll.Add(lmode);//将所有讨论都添加到导航中
                }
            }

        }

        /// <summary>
        /// 将导入学案生成新导航
        /// </summary>
        public void importmenu(int Cid)
        {
            // 类型：活动1，调查2，讨论3，表单４，编程5，阅读6
            string sql = "select * from Mission where Mdelete=0 and Mcid=" + Cid;
            DataTable cdt = DbHelperSQL.Query(sql).Tables[0];
            int ccount = cdt.Rows.Count;
            Model.ListMenu lmode = new Model.ListMenu();
            BLL.ListMenu lbll = new BLL.ListMenu();
            if (ccount > 0)
            {
                for (int i = 0; i < ccount; i++)
                {
                    int mid = Int32.Parse(cdt.Rows[i]["Mid"].ToString());
                    string mtitle = cdt.Rows[i]["Mtitle"].ToString();
                    int mcid = Int32.Parse(cdt.Rows[i]["Mcid"].ToString());
                    int msort = Int32.Parse(cdt.Rows[i]["Msort"].ToString());
                    bool mpublish = bool.Parse(cdt.Rows[i]["Mpublish"].ToString());
                    int mcategory = Int32.Parse(cdt.Rows[i]["Mcategory"].ToString());
                    lmode.Lcid = mcid;
                    lmode.Lsort = msort;
                    lmode.Ltype = 1;
                    if (mcategory == 1) lmode.Ltype = 6;
                    if (mcategory == 2) lmode.Ltype = 5;
                    lmode.Lxid = mid;
                    lmode.Lshow = mpublish;
                    lmode.Ltitle = mtitle;
                    lbll.Add(lmode);//将所有活动都添加到导航中
                }
            }

            string sqltwo = "select * from Survey where Vcid="+Cid;
            DataTable sdt = DbHelperSQL.Query(sqltwo).Tables[0];
            int scount = sdt.Rows.Count;
            if (scount > 0)
            {
                for (int i = 0; i < scount; i++)
                {
                    int vid = Int32.Parse(sdt.Rows[i]["Vid"].ToString());
                    string vtitle = sdt.Rows[i]["Vtitle"].ToString();
                    int vcid = Int32.Parse(sdt.Rows[i]["Vcid"].ToString());
                    bool vclose = bool.Parse(sdt.Rows[i]["Vclose"].ToString());
                    lmode.Lcid = vcid;
                    lmode.Lsort = 6;
                    lmode.Ltype = 2;
                    lmode.Lxid = vid;
                    lmode.Lshow = !vclose;
                    lmode.Ltitle = vtitle;
                    lbll.Add(lmode);//将所有调查都添加到导航中
                }
            }

            string sqlthree = "select * from TopicDiscuss where Tcid="+Cid;
            DataTable tdt = DbHelperSQL.Query(sqlthree).Tables[0];
            int tcount = tdt.Rows.Count;
            if (tcount > 0)
            {
                for (int i = 0; i < tcount; i++)
                {
                    int tid = Int32.Parse(tdt.Rows[i]["Tid"].ToString());
                    string ttitle = tdt.Rows[i]["Ttitle"].ToString();
                    int tcid = Int32.Parse(tdt.Rows[i]["Tcid"].ToString());
                    bool tclose = bool.Parse(tdt.Rows[i]["Tclose"].ToString());
                    lmode.Lcid = tcid;
                    lmode.Lsort = 8;
                    lmode.Ltype = 3;
                    lmode.Lxid = tid;
                    lmode.Lshow = !tclose;
                    lmode.Ltitle = ttitle;
                    lbll.Add(lmode);//将所有讨论都添加到导航中
                }
            }

            string sqlfour = "select  * from TxtForm where Mcid=" + Cid;
            DataTable fdt = DbHelperSQL.Query(sqlfour).Tables[0];
            int fcount = fdt.Rows.Count;
            if (fcount > 0)
            {
                for (int i = 0; i < fcount; i++)
                {
                    int mid = Int32.Parse(fdt.Rows[i]["Mid"].ToString());
                    string mtitle = fdt.Rows[i]["Mtitle"].ToString();
                    int mcid = Int32.Parse(fdt.Rows[i]["Mcid"].ToString());
                    bool mpublish = bool.Parse(fdt.Rows[i]["Mpublish"].ToString());
                    lmode.Lcid = mcid;
                    lmode.Lsort = 9;
                    lmode.Ltype = 4;
                    lmode.Lxid = mid;
                    lmode.Lshow = mpublish;
                    lmode.Ltitle = mtitle;
                    lbll.Add(lmode);//将所有讨论都添加到导航中
                }
            }
        }
        /// <summary>
        /// 将导入学案生成的新导航的序号跟旧导航序号一致
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Cid"></param>
        public void importupsort(DataTable dt, int Cid)
        {
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    string ltitle = dt.Rows[i]["Ltitle"].ToString();
                    int lsort = Int32.Parse(dt.Rows[i]["Lsort"].ToString());
                    updatebyLtitle(ltitle, Cid, lsort);                
                }            
            }        
        }
        /// <summary>
        /// 更新该学案导航指定目栏名称的序号
        /// </summary>
        /// <param name="ltitle"></param>
        /// <param name="Cid"></param>
        /// <param name="newsort"></param>
        private void updatebyLtitle(string ltitle, int Cid, int newsort)
        {
            string mysql = "update ListMenu set Lsort=" + newsort + " where Lcid=" + Cid + " and Ltitle='" + ltitle+"'";
            DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// 返回该学案的导航列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataSet GetMenu(int cid)
        {
            string mysql = "select * from ListMenu where Lcid="+cid +" order by Lsort asc";
            return DbHelperSQL.Query(mysql);
        }

        /// <summary>
        /// 返回该学案的显示的导航列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataSet GetShowedMenu(int cid)
        {
            string mysql = "select * from ListMenu where Lcid=" + cid + " and Lshow=1 order by Lsort asc";
            return DbHelperSQL.Query(mysql);
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
			parameters[0].Value = "ListMenu";
			parameters[1].Value = "Lid";
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

