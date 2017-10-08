using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// GaugeItem
	/// </summary>
	public partial class GaugeItem
	{
		private readonly LearnSite.DAL.GaugeItem dal=new LearnSite.DAL.GaugeItem();
		public GaugeItem()
		{}
		#region  Method

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
		public bool Exists(int Mid)
		{
			return dal.Exists(Mid);
		}                
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsMgid(int Mgid)
        {
            return dal.ExistsMgid(Mgid);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.GaugeItem model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.GaugeItem model)
		{
			return dal.Update(model);
		}
        /// <summary>
        /// 更新一条数据 注意： model只赋值Mitem,Mscore,Mid
        /// </summary>
        public bool UpdateMitem(LearnSite.Model.GaugeItem model)
        {
            return dal.UpdateMitem(model);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Mid)
		{
			
			return dal.Delete(Mid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Midlist )
		{
			return dal.DeleteList(Midlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.GaugeItem GetModel(int Mid)
		{
			
			return dal.GetModel(Mid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.GaugeItem GetModelByCache(int Mid)
		{
			
			string CacheKey = "GaugeItemModel-" + Mid;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Mid);
					if (objModel != null)
					{
						int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.GaugeItem)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}                
        /// <summary>
        /// 获取自评选项
        /// </summary>
        /// <param name="Fwid"></param>
        /// <param name="Fnum"></param>
        /// <returns></returns>
        public DataSet GetMySelfMitems(string Fselect)
        {
            return dal.GetMySelfMitems(Fselect);
        }
        public DataSet GetListMgid(string Mgid)
        {
            string strWhere = "Mgid="+Mgid +" order by Msort asc";
            return GetList(strWhere);
        }
                
        /// <summary>
        /// 当活动中未指定互评评价标准时，自动选取相应作品类型中的第一条评价标准
        /// </summary>
        public DataSet GetListAutoGtype(string Gtype)
        {
            return dal.GetListAutoGtype(Gtype);
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
		public List<LearnSite.Model.GaugeItem> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.GaugeItem> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.GaugeItem> modelList = new List<LearnSite.Model.GaugeItem>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.GaugeItem model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.GaugeItem();
					if(dt.Rows[n]["Mid"]!=null && dt.Rows[n]["Mid"].ToString()!="")
					{
						model.Mid=int.Parse(dt.Rows[n]["Mid"].ToString());
					}
					if(dt.Rows[n]["Mgid"]!=null && dt.Rows[n]["Mgid"].ToString()!="")
					{
						model.Mgid=int.Parse(dt.Rows[n]["Mgid"].ToString());
					}
					if(dt.Rows[n]["Mitem"]!=null && dt.Rows[n]["Mitem"].ToString()!="")
					{
					model.Mitem=dt.Rows[n]["Mitem"].ToString();
					}
					if(dt.Rows[n]["Mscore"]!=null && dt.Rows[n]["Mscore"].ToString()!="")
					{
						model.Mscore=int.Parse(dt.Rows[n]["Mscore"].ToString());
					}
					if(dt.Rows[n]["Msort"]!=null && dt.Rows[n]["Msort"].ToString()!="")
					{
						model.Msort=int.Parse(dt.Rows[n]["Msort"].ToString());
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
        public DataSet GetListMitems(int Mid)
        {
            return dal.GetListMitems(Mid);
        }
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

