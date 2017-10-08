using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// Chinese
	/// </summary>
	public partial class Chinese
	{
		private readonly LearnSite.DAL.Chinese dal=new LearnSite.DAL.Chinese();
		public Chinese()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LearnSite.Model.Chinese model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.Chinese model)
		{
			return dal.Update(model);
		}

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Nid)
        {

            return dal.Delete(Nid);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.Chinese GetModel(int Nid)
        {

            return dal.GetModel(Nid);
        }

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.Chinese GetModelByCache(int Nid)
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "ChineseModel-" + Nid;
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
			return (LearnSite.Model.Chinese)objModel;
		}
                        
        /// <summary>
        /// 获得数据列表,不包含Ncontent内容
        /// </summary>
        public DataSet GetListTitle()
        {
            return dal.GetListTitle("");
        }
                
        /// <summary>
        /// 将指定拼音文章Nid绑定到datalist中
        /// </summary>
        /// <param name="DLTid"></param>
        public DataSet ShowAllNid(string Nids)
        {
            return dal.ShowAllNid(Nids);
        }
        /// <summary>
        /// 获取指定的拼音词语内容，如果Nid为空则自动返回一条内容
        /// </summary>
        /// <param name="Nid"></param>
        /// <returns></returns>
        public string GetContent(string Nid)
        {
            return dal.GetContent(Nid);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        /// <summary>
        /// 获取一篇词语文章
        /// </summary>
        /// <param name="Nid"></param>
        /// <returns></returns>
        public DataTable GetOneChinese(int Nid)
        {
            string strWhere = " Nid="+Nid;
            return dal.GetList(strWhere).Tables[0];
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
		public List<LearnSite.Model.Chinese> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Chinese> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Chinese> modelList = new List<LearnSite.Model.Chinese>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Chinese model;
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

