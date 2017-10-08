using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类NotSign 的摘要说明。
	/// </summary>
	public class NotSign
	{
		private readonly LearnSite.DAL.NotSign dal=new LearnSite.DAL.NotSign();
		public NotSign()
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
		public bool Exists(int Nid)
		{
			return dal.Exists(Nid);
		}
        /// <summary>
        /// 是否存在今天未签到记录
        /// </summary>
        public bool ExistsToday(string Nnum)
        {
            return dal.ExistsToday(Nnum);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.NotSign model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.NotSign model)
		{
			dal.Update(model);
		}
        /// <summary>
        /// 更新备注一条数据
        /// </summary>
        public void UpdateNote(string Nnum, string Nnote)
        {
            dal.UpdateNote(Nnum, Nnote);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Nid)
		{
			
			dal.Delete(Nid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.NotSign GetModel(int Nid)
		{
			
			return dal.GetModel(Nid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.NotSign GetModelByCache(int Nid)
		{
			
			string CacheKey = "NotSignModel-" + Nid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Nid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.NotSign)objModel;
		}                
        /// <summary>
        /// 获取今天未签到备注
        /// </summary>
        /// <param name="Nnum"></param>
        /// <returns></returns>
        public string GetNoteToday(string Nnum)
        {
            return dal.GetNoteToday(Nnum);
        }
               
        /// <summary>
        /// 获取某天未签到备注
        /// </summary>
        /// <param name="Nnum"></param>
        /// <returns></returns>
        public string GetNoteThisday(string Nnum, int Nyear, int Nmonth, int Nday)
        {
            return dal.GetNoteThisday(Nnum, Nyear, Nmonth, Nday);
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
		public List<LearnSite.Model.NotSign> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.NotSign> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.NotSign> modelList = new List<LearnSite.Model.NotSign>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.NotSign model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.NotSign();
					if(dt.Rows[n]["Nid"].ToString()!="")
					{
						model.Nid=int.Parse(dt.Rows[n]["Nid"].ToString());
					}
					model.Nnum=dt.Rows[n]["Nnum"].ToString();
					if(dt.Rows[n]["Ndate"].ToString()!="")
					{
						model.Ndate=DateTime.Parse(dt.Rows[n]["Ndate"].ToString());
					}
					if(dt.Rows[n]["Nyear"].ToString()!="")
					{
						model.Nyear=int.Parse(dt.Rows[n]["Nyear"].ToString());
					}
					if(dt.Rows[n]["Nmonth"].ToString()!="")
					{
						model.Nmonth=int.Parse(dt.Rows[n]["Nmonth"].ToString());
					}
					if(dt.Rows[n]["Nday"].ToString()!="")
					{
						model.Nday=int.Parse(dt.Rows[n]["Nday"].ToString());
					}
					model.Nweek=dt.Rows[n]["Nweek"].ToString();
					model.Nnote=dt.Rows[n]["Nnote"].ToString();
                    if (dt.Rows[n]["Ngrade"].ToString() != "")
                    {
                        model.Ngrade = int.Parse(dt.Rows[n]["Ngrade"].ToString());
                    }
                    if (dt.Rows[n]["Nterm"].ToString() != "")
                    {
                        model.Nterm = int.Parse(dt.Rows[n]["Nterm"].ToString());
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
        /// 根据学号获取缺席记录列表，按日期排序
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public DataSet NotSignSnumdetail(string Snum)
        {
            return dal.NotSignSnumdetail(Snum);
        }        
        /// <summary>
        /// 增加字段初始化
        /// </summary>
        public void upgradefix()
        {
            dal.upgradefix();
        }
                
        /// <summary>
        /// 某班级签到导出到Excel
        /// </summary>
        public void NotSignExcel(int Sgrade, int Sclass, int Nterm)
        {
            dal.NotSignExcel(Sgrade, Sclass, Nterm);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法
	}
}

