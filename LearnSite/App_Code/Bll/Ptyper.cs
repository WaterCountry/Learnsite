using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
using System.Web;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Ptyper 的摘要说明。
	/// </summary>
	public class Ptyper
	{
		private readonly LearnSite.DAL.Ptyper dal=new LearnSite.DAL.Ptyper();
		public Ptyper()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Pid)
		{
			return dal.Exists(Pid);
		}

                /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsPsnum(int Psid)
        {
            return dal.ExistsPsnum(Psid);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Ptyper model)
		{
			return dal.Add(model);
		}
        /// <summary>
        /// 将成绩更新到学生表中
        /// </summary>
        /// <param name="Psid"></param>
        /// <param name="Pscore"></param>
        public void UpdateStscore(int Psid, int Pscore)
        {
            dal.UpdateStscore(Psid,Pscore);
        }
		/// <summary>
		/// 更新一条数据，根据学号更新
		/// </summary>
        public int Update(LearnSite.Model.Ptyper model)
        {
            return dal.Update(model);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Pid)
		{
			
			dal.Delete(Pid);
		}        
        /// <summary>
        /// 清空所有打字记录
        /// </summary>
        public int DeleteAll()
        {
           return dal.DeleteAll();
        }        
        /// <summary>
        /// 清空所教班级学生的打字记录
        /// </summary>
        public void DeleteMyAll(int Rhid)
        {
            dal.DeleteMyAll(Rhid);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Ptyper GetModel(int Pid)
		{
			
			return dal.GetModel(Pid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Ptyper GetModelByCache(int Pid)
		{
			
			string CacheKey = "PtyperModel-" + Pid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Pid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Ptyper)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Ptyper> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Ptyper> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Ptyper> modelList = new List<LearnSite.Model.Ptyper>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Ptyper model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.Ptyper();
					if(dt.Rows[n]["Pid"].ToString()!="")
					{
						model.Pid=int.Parse(dt.Rows[n]["Pid"].ToString());
					}
					if(dt.Rows[n]["Ptid"].ToString()!="")
					{
						model.Ptid=int.Parse(dt.Rows[n]["Ptid"].ToString());
					}
					model.Psnum=dt.Rows[n]["Psnum"].ToString();
					if(dt.Rows[n]["Pscore"].ToString()!="")
					{
						model.Pscore=int.Parse(dt.Rows[n]["Pscore"].ToString());
					}
					if(dt.Rows[n]["Pdate"].ToString()!="")
					{
						model.Pdate=DateTime.Parse(dt.Rows[n]["Pdate"].ToString());
					}
					model.Pip=dt.Rows[n]["Pip"].ToString();
					if(dt.Rows[n]["Ptype"].ToString()!="")
					{
						model.Ptype=int.Parse(dt.Rows[n]["Ptype"].ToString());
					}
                    if (dt.Rows[n]["Pdegree"].ToString() != "")
                    {
                        model.Ptype = int.Parse(dt.Rows[n]["Pdegree"].ToString());
                    }
                    if (dt.Rows[n]["Pgrade"].ToString() != "")
                    {
                        model.Pgrade = int.Parse(dt.Rows[n]["Pgrade"].ToString());
                    }
                    if (dt.Rows[n]["Pterm"].ToString() != "")
                    {
                        model.Pterm = int.Parse(dt.Rows[n]["Pterm"].ToString());
                    }
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

        /// <summary>
        /// 显示前60本篇打字英雄榜记录
        /// </summary>
        /// <param name="Tid"></param>
        /// <returns></returns>
        public DataSet ShowTypeScore(int Tid)
        {
            return dal.ShowTypeScore(Tid);
        }
        /// <summary>
        ///  显示本班本篇文章打字英雄榜记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public DataSet ShowClassTidScore(int Sgrade, int Sclass, int Ptid)
        {
            return dal.ShowClassTidScore(Sgrade, Sclass, Ptid);
        }
        /// <summary>
        ///  显示本班打字英雄榜记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public DataSet ShowClassTypeScore(int Sgrade, int Sclass)
        {
            return dal.ShowClassTypeScore(Sgrade, Sclass);
        }
        /// <summary>
        /// 显示年级段前100名打字英雄榜记录
        /// </summary>
        /// <param name="GVtyper"></param>
        public DataSet ShowAllTypeScore(int Sgrade)
        {
            return dal.ShowAllTypeScore(Sgrade);
        }
               
        /// <summary>
        /// 显示全校打字英雄榜记录
        /// </summary>
        /// <param name="GVtyper"></param>
        public DataSet ShowSchoolTypeScore()
        {
            return dal.ShowSchoolTypeScore();
        }
        /// <summary>
        /// 检验今天本班本机除本人外是否有别人账号打过字
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Pip"></param>
        /// <returns></returns>
        public bool ExistPtyper(int Sid, int Sgrade, int Sclass, string Pip)
        {
            return dal.ExistPtyper(Sid, Sgrade, Sclass, Pip);
        }
        /// <summary>
        /// 清除所教班级超过指定速度的打字成绩
        /// </summary>
        /// <param name="Pscore"></param>
        public void DeleteOverScore(int Pscore, int Rhid)
        {
            dal.DeleteOverScore(Pscore,Rhid);
        }
        /// <summary>
        /// 显示年级段打字英雄榜记录
        /// </summary>
        /// <param name="GVtyper"></param>
        public DataSet ShowTopTypeScore(int Sgrade, int ntop)
        {
            return dal.ShowTopTypeScore(Sgrade, ntop);
        }
                
        /// <summary>
        /// 显示全校打字英雄榜记录
        /// </summary>
        /// <param name="GVtyper"></param>
        public DataSet ShowSchoolTopTypeScore(int nTop)
        {
            return dal.ShowSchoolTopTypeScore(nTop);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

        public string Savemytype(string Ptid, string TypeScore)
        {
            string msgss = "-1";
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname] != null)
            {
                int Psid = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
                string Psnum = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
                string Pip = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
                int Sgrade = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
                int Sclass = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
                int Pterm = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString());
                bool workiplimit = LearnSite.Common.XmlHelp.GetWorkIpLimit();
                if (workiplimit)
                {
                    LearnSite.BLL.Ptyper pbll = new LearnSite.BLL.Ptyper();
                    if (!pbll.ExistPtyper(Psid, Sgrade, Sclass, Pip))
                    {
                        msgss = SaveTypeRecord(Int32.Parse(Ptid), Psid, Psnum, Pip, Sgrade, Pterm, TypeScore);
                    }
                    else
                    {
                        msgss = "-2";
                    }
                }
                else
                {
                    msgss = SaveTypeRecord(Int32.Parse(Ptid), Psid, Psnum, Pip, Sgrade, Pterm, TypeScore);
                }
            }
            return msgss;
        }



        private string SaveTypeRecord(int Ptid, int Psid, string Psnum, string Pip, int Pgrade, int Pterm, string TypeScore)
        {
            int Pscore = Int32.Parse(TypeScore.Trim());

            DateTime Pdate = DateTime.Now;
            LearnSite.Model.Ptyper pmodel = new LearnSite.Model.Ptyper();
            pmodel.Pdate = Pdate;
            pmodel.Pip = Pip;
            pmodel.Pscore = Pscore;
            pmodel.Psnum = Psnum;
            pmodel.Ptid = Ptid;
            pmodel.Ptype = 1;
            pmodel.Pdegree = GetPdegree(Pscore);
            pmodel.Pgrade = Pgrade;
            pmodel.Pterm = Pterm;
            pmodel.Psid = Psid;
            LearnSite.BLL.Ptyper pt = new LearnSite.BLL.Ptyper();
            int res = -1;
            if (pt.ExistsPsnum(Psid))
            {
                res = pt.Update(pmodel);
            }
            else
            {
                res = pt.Add(pmodel);
            }
            if (res > 0)
            {
                pt.UpdateStscore(Psid, Pscore);
                res = 1;
            }
            return res.ToString();
        }
        private int GetPdegree(int Pscore)
        {
            int scoreCovent = 0;
            if (Pscore / 100 > 0)
            {
                scoreCovent = 100 + (Pscore / 100) * 10 + (Pscore % 100) / 10;//打字速度超过100字/分的积分处理方法
            }
            else
            {
                scoreCovent = Pscore;
            }
            return scoreCovent;
        }
		#endregion  成员方法
	}
}

