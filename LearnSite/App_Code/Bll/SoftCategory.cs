using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// SoftCategory
	/// </summary>
	public partial class SoftCategory
	{
		private readonly LearnSite.DAL.SoftCategory dal=new LearnSite.DAL.SoftCategory();
		public SoftCategory()
		{}
		#region  BasicMethod
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxYsort()
        {
            return dal.GetMaxYsort();
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.SoftCategory model)
		{
			return dal.Add(model);
		}
        /// <summary>
        /// 更新分类名称
        /// </summary>
        /// <param name="Yid"></param>
        /// <param name="Ytitle"></param>
        /// <returns></returns>
        public bool UpdateYtitle(int Yid, string Ytitle)
        {
            return dal.UpdateYtitle(Yid, Ytitle);
        }
        /// <summary>
        /// 更新一条数据//这个更新不了，不知何原因
        /// </summary>
        public bool Update(int Yid, string Ytitle)
        {
            return dal.Update(Yid, Ytitle);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.SoftCategory model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Yid)
		{
			
			return dal.Delete(Yid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.SoftCategory GetModel(int Yid)
		{
			
			return dal.GetModel(Yid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.SoftCategory GetModelByCache(int Yid)
		{
			
			string CacheKey = "SoftCategoryModel-" + Yid;
			object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Yid);
					if (objModel != null)
					{
						int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
						LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.SoftCategory)objModel;
		}
                /// <summary>
        /// 更改导航次序 updown 值0向上 减，1向下　增
        /// </summary>
        /// <param name="Lid"></param>
        /// <param name="updown"></param>
        public void UpdateYsort(int Yid, bool updown)
        {
            dal.UpdateYsort(Yid, updown);
        }
        /// <summary>
        /// 初始化序号
        /// </summary>
        public void initYsort()
        {
            dal.initYsort();
        }
        /// <summary>
        /// 获得数据列表按序号和编号排序
        /// </summary>
        public DataSet GetListOrder()
        {
            return dal.GetListOrder();
        }                
        /// <summary>
        /// 获得数据列表按序号和编号排序
        /// </summary>
        public DataSet GetListCategory()
        {
            return dal.GetListCategory();
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
		public List<LearnSite.Model.SoftCategory> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.SoftCategory> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.SoftCategory> modelList = new List<LearnSite.Model.SoftCategory>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.SoftCategory model;
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
        /// 获取标题
        /// </summary>
        /// <param name="Yid"></param>
        /// <returns></returns>
        public string GetTitle(int Yid)
        {
            return dal.GetTitle(Yid);
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

		#endregion  ExtensionMethod
	}
}

