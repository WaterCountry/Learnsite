using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
using System.Web;
namespace LearnSite.BLL
{
	/// <summary>
	/// Pchinese
	/// </summary>
	public partial class Pchinese
	{
		private readonly LearnSite.DAL.Pchinese dal=new LearnSite.DAL.Pchinese();
		public Pchinese()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}
                
        /// <summary>
        /// 是否存在该记录，不存在则添加
        /// </summary>
        public bool NoExistsAdd(string Psnum, int Pyear, int Pgrade, int Pclass, int Psid, int Pterm)
        {
            return dal.NoExistsAdd(Psnum, Pyear, Pgrade, Pclass, Psid, Pterm);
        }
                /// <summary>
        /// 返回０为无效，１为更新记录，２为插入记录
        /// </summary>
        /// <param name="Psid"></param>
        /// <param name="Psnum"></param>
        /// <param name="Pyear"></param>
        /// <param name="Pgrade"></param>
        /// <param name="Pclass"></param>
        /// <param name="Pterm"></param>
        /// <param name="Papple"></param>
        /// <param name="Pspeed"></param>
        /// <returns></returns>
        public int UpdateTotalAppleNew(int Psid, string Psnum, int Pyear, int Pgrade, int Pclass, int Pterm, int Papple, int Pspeed)
        {
            return dal.UpdateTotalAppleNew(Psid, Psnum, Pyear, Pgrade, Pclass, Pterm, Papple, Pspeed);
        }
        public int UpdateChineseType(int Papple, int Pspeed)
        {
            int Psid = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
            string Psnum = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            int Pgrade = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int Pclass = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            int Pyear = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
            int Pterm = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString());
            DateTime Pdate = DateTime.Now;
            Model.Pchinese pchinese = new Model.Pchinese();
            pchinese.Psid = Psid;
            pchinese.Psnum = Psnum;
            pchinese.Pgrade = Pgrade;
            pchinese.Pclass = Pclass;
            pchinese.Pyear = Pyear;
            pchinese.Pterm = Pterm;
            pchinese.Pdate = Pdate;
            pchinese.Ptotal = Papple;
            pchinese.Pspeed = Pspeed;
            pchinese.Papple = Papple;
            pchinese.Pdegree = 2;
            int res = dal.UpdateTotal(pchinese);
            if (res > 0)
                return res;
            else
            {
                dal.Add(pchinese);
                return Papple;
            }
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Pid)
		{
			return dal.Exists(Pid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Pchinese model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.Pchinese model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Pid)
		{
			
			return dal.Delete(Pid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Pchinese GetModel(int Pid)
		{
			
			return dal.GetModel(Pid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.Pchinese GetModelByCache(int Pid)
		{
			
			string CacheKey = "PchineseModel-" + Pid;
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
			return (LearnSite.Model.Pchinese)objModel;
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
		public List<LearnSite.Model.Pchinese> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Pchinese> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Pchinese> modelList = new List<LearnSite.Model.Pchinese>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Pchinese model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
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
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod
                /// <summary>
        /// 获取某学号收集的苹果总数
        /// </summary>
        /// <param name="Psnum"></param>
        /// <returns></returns>
        public int GetTotalApple(string Psnum)
        {
            return dal.GetTotalApple(Psnum);
        }

        /// <summary>
        /// 更新苹果总数
        /// </summary>
        /// <param name="Psnum"></param>
        /// <param name="Papple"></param>
        /// <param name="Pspeed"></param>
        /// <param name="Pterm"></param>
        /// <param name="Pgrade"></param>
        public void UpdateTotalApple(string Psnum, int Papple, int Pspeed, int Pterm, int Pgrade)
        {
            dal.UpdateTotalApple(Psnum, Papple, Pspeed, Pterm,Pgrade);
        }
        /// <summary>
        /// 获取所有拼音成绩
        /// </summary>
        /// <param name="area"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Pterm"></param>
        /// <returns></returns>
        public DataTable ShowAllChineseApple(string area, int Sgrade, int Sclass, int Pterm)
        {
            return dal.ShowAllChineseApple(area, Sgrade, Sclass,Pterm);
        }
		#endregion  ExtensionMethod
	}
}

