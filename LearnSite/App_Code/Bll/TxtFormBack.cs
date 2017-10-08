
using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
using System.Web;
namespace LearnSite.BLL
{
	/// <summary>
	/// TxtFormBack
	/// </summary>
	public partial class TxtFormBack
	{
		private readonly LearnSite.DAL.TxtFormBack dal=new LearnSite.DAL.TxtFormBack();
		public TxtFormBack()
		{}
		#region  BasicMethod

        /// <summary>
        ///判断是否已提交
        /// </summary>
        /// <param name="Rsid"></param>
        /// <param name="Rmid"></param>
        /// <returns></returns>
        public int GetRid(string Rsid, string Rmid)
        {
            return dal.GetRid(Rsid, Rmid);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.TxtFormBack model)
		{
			return dal.Add(model);
		}

        public void UpdateAllScore(string Ryear, string Rgrade, string Rclass, string Rmid, int Rscore)
        {
            dal.UpdateAllScore(Ryear, Rgrade, Rclass, Rmid, Rscore);
        }
        public void UpdateScore(int Rid, int Rscore)
        {
            dal.UpdateScore(Rid, Rscore);
        }
        public void UpdateAgree(int Rid)
        {
            dal.UpdateAgree(Rid);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.TxtFormBack model)
		{
			return dal.Update(model);
		}
                
        /// <summary>
        /// 更新内容 Rwords, Rtime,Rid
        /// </summary>
        public bool UpdateContent(LearnSite.Model.TxtFormBack model)
        {
            return dal.UpdateContent(model);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Rid)
		{
			
			return dal.Delete(Rid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.TxtFormBack GetModel(int Rid)
		{
			
			return dal.GetModel(Rid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.TxtFormBack GetModelByCache(int Rid)
		{
			
			string CacheKey = "TxtFormBackModel-" + Rid;
			object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Rid);
					if (objModel != null)
					{
						int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
						LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.TxtFormBack)objModel;
		}

         /// <summary>
        /// 获得该表单本班数据列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Rmid">对应表单的自动编号Mid</param>
        /// <returns></returns>
        public DataTable GetClassTxtFormScore(int Sgrade, int Sclass, int Rmid)
        {
            return dal.GetClassTxtFormScore(Sgrade, Sclass, Rmid);
        }

        /// <summary>
        /// 获取该班级的表单列表
        /// </summary>
        /// <param name="Rgrade"></param>
        /// <param name="Rclass"></param>
        /// <param name="Rmid"></param>
        /// <returns></returns>
        public DataSet GetListclass(string Rgrade, string Rclass, string Rmid)
        {
            return dal.GetListclass(Rgrade, Rclass, Rmid);
        }

        public string GetUndoStus(int Rgrade, int Rclass, int Rmid)
        {
            string stus = "未填写同学：";
            DataTable dt = dal.GetUndo(Rgrade, Rclass, Rmid);
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    stus = stus + dt.Rows[i][0].ToString();
                    if (i < count - 1)
                        stus += "，";
                }
            }
            return stus;
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
		public List<LearnSite.Model.TxtFormBack> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.TxtFormBack> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.TxtFormBack> modelList = new List<LearnSite.Model.TxtFormBack>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.TxtFormBack model;
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
        /// 获取某学生学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowStuDoneBackCids(string Snum, int Rterm, int Rgrade)
        {
            return dal.ShowStuDoneBackCids(Snum, Rterm, Rgrade);
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
        /// 保存表单数据
        /// </summary>
        /// <returns></returns>
        public string SaveFormContent()
        {
            string Mid = HttpContext.Current.Request.QueryString["Mid"].ToString();
            string word = HttpContext.Current.Request["Word"].ToString();
            string msg = "提交失败!";
            if (HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname] != null)
            {
                if (Common.WordProcess.IsNum(Mid))
                {
                    string Syear = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
                    string Sgrade = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
                    string Sclass = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
                    string Wsid = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
                    string Wip = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
                    string Sname = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sname"].ToString();
                    string Snum = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
                    string Wterm = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString();
                    Model.TxtFormBack rmodel = new Model.TxtFormBack();
                    BLL.TxtFormBack rbll = new BLL.TxtFormBack();
                    rmodel.Rmid = Int32.Parse(Mid);
                    rmodel.Ragree = 0;
                    rmodel.Rclass = Int32.Parse(Sclass);
                    rmodel.Rgrade = Int32.Parse(Sgrade);
                    rmodel.Rip = Wip;
                    rmodel.Rscore = 0;
                    rmodel.Rsid = Int32.Parse(Wsid);
                    rmodel.Rsnum = Snum;
                    rmodel.Rterm = Int32.Parse(Wterm);
                    rmodel.Rtime = DateTime.Now;
                    rmodel.Rwords = HttpUtility.HtmlEncode(word);
                    rmodel.Ryear = Int32.Parse(Syear);
                    int Rid = rbll.GetRid(Wsid, Mid);
                    rmodel.Rid = Rid;
                    if (Rid > 0)
                    {
                        if (rbll.UpdateContent(rmodel)) //如果提交过表单，再更新提交的内容
                            msg = "修改内容成功!";
                        else
                            msg = "老师已评价，不可修改!";
                    }
                    else
                    {
                        rbll.Add(rmodel);//否则提交表单内容
                        msg = "提交内容成功!";
                    }
                }
                else
                {
                    msg = "Mid参数格式错误!";
                }
            }
            else
            {
                msg = "未登录，无权限！cookies无效";
            }
            return msg;        
        }

		#endregion  ExtensionMethod
	}
}

