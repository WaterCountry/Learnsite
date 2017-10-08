using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// House
	/// </summary>
	public partial class House
	{
		private readonly LearnSite.DAL.House dal=new LearnSite.DAL.House();
		public House()
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
		public bool Exists(int Hid)
		{
			return dal.Exists(Hid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.House model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.House model)
		{
			return dal.Update(model);
		}
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateHseat(int Hid, string Hseat)
        {
            return dal.UpdateHseat(Hid, Hseat);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Hid)
		{
			
			return dal.Delete(Hid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Hidlist )
		{
			return dal.DeleteList(Hidlist );
		}
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.House GetModel(string Hname)
        {
            return dal.GetModel(Hname);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.House GetModel(int Hid)
		{
			
			return dal.GetModel(Hid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.House GetModelByCache(int Hid)
		{
			
			string CacheKey = "HouseModel-" + Hid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Hid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.House)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
                
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListHouse()
        {
            return dal.GetListHouse();
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public string GetHseat(int Hid)
        {
            return dal.GetHseat(Hid);
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
		public List<LearnSite.Model.House> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.House> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.House> modelList = new List<LearnSite.Model.House>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.House model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.House();
					if(dt.Rows[n]["Hid"].ToString()!="")
					{
						model.Hid=int.Parse(dt.Rows[n]["Hid"].ToString());
					}
					model.Hname=dt.Rows[n]["Hname"].ToString();
					model.Hseat=dt.Rows[n]["Hseat"].ToString();
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
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

