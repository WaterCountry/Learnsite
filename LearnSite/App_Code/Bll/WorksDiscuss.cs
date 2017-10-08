using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类WorksDiscuss 的摘要说明。
	/// </summary>
	public class WorksDiscuss
	{
		private readonly LearnSite.DAL.WorksDiscuss dal=new LearnSite.DAL.WorksDiscuss();
		public WorksDiscuss()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Did)
		{
			return dal.Exists(Did);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsDiscuss(int Dwid, string Dsnum)
        {
            return dal.ExistsDiscuss(Dwid, Dsnum);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.WorksDiscuss model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.WorksDiscuss model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Did)
		{
			
			dal.Delete(Did);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.WorksDiscuss GetModel(int Did)
		{
			
			return dal.GetModel(Did);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.WorksDiscuss GetModelByCache(int Did)
		{
			
			string CacheKey = "WorksDiscussModel-" + Did;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Did);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.WorksDiscuss)objModel;
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
		public List<LearnSite.Model.WorksDiscuss> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.WorksDiscuss> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.WorksDiscuss> modelList = new List<LearnSite.Model.WorksDiscuss>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.WorksDiscuss model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.WorksDiscuss();
					if(dt.Rows[n]["Did"].ToString()!="")
					{
						model.Did=int.Parse(dt.Rows[n]["Did"].ToString());
					}
					if(dt.Rows[n]["Dwid"].ToString()!="")
					{
						model.Dwid=int.Parse(dt.Rows[n]["Dwid"].ToString());
					}
					model.Dsnum=dt.Rows[n]["Dsnum"].ToString();
					model.Dwords=dt.Rows[n]["Dwords"].ToString();
					if(dt.Rows[n]["Dtime"].ToString()!="")
					{
						model.Dtime=DateTime.Parse(dt.Rows[n]["Dtime"].ToString());
					}
					model.Dip=dt.Rows[n]["Dip"].ToString();
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
        /// 获得该作品的所有评论
        /// </summary>
        public DataSet GetDiscussList(int Dwid)
        {
           return dal.GetDiscussList(Dwid);
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

