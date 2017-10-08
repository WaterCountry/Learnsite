
using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// TxtForm
	/// </summary>
	public partial class TxtForm
	{
		private readonly LearnSite.DAL.TxtForm dal=new LearnSite.DAL.TxtForm();
		public TxtForm()
		{}
		#region  BasicMethod

        public string GetMtitle(int Mid)
        {
            return dal.GetMtitle(Mid);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.TxtForm model)
		{
			return dal.Add(model);
		}

		/// <summary>
        /// 更新一条数据 Mtitle,Mcontent,Mdate,Mpublish,Mid
        /// </summary>
		public bool Update(LearnSite.Model.TxtForm model)
		{
			return dal.Update(model);
		}
        /// <summary>
        /// 表单状态：发布或回收
        /// </summary>
        /// <param name="Mid"></param>
        public void UpdateMpublish(int Mid)
        {
            dal.UpdateMpublish(Mid);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Mid)
		{
			
			return dal.Delete(Mid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.TxtForm GetModel(int Mid)
		{
			
			return dal.GetModel(Mid);
		}
        		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public LearnSite.Model.TxtForm DataRowToModel(DataRow row)
        {
            return dal.DataRowToModel(row);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.TxtForm GetModelByCache(int Mid)
		{
			
			string CacheKey = "TxtFormModel-" + Mid;
			object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Mid);
					if (objModel != null)
					{
						int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
						LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.TxtForm)objModel;
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
		public List<LearnSite.Model.TxtForm> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.TxtForm> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.TxtForm> modelList = new List<LearnSite.Model.TxtForm>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.TxtForm model;
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

		#endregion  ExtensionMethod
	}
}

