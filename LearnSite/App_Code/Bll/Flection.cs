using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Flection 的摘要说明。
	/// </summary>
	public class Flection
	{
		private readonly LearnSite.DAL.Flection dal=new LearnSite.DAL.Flection();
		public Flection()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Fid)
		{
			return dal.Exists(Fid);
		}

        /// <summary>
        ///根据Fcid 是否存在该记录
        /// </summary>
        public bool ExistsFcid(int Fcid)
        {
            return dal.ExistsFcid(Fcid);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Flection model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Flection model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Fid)
		{
			
			dal.Delete(Fid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Flection GetModel(int Fcid)
		{
			
			return dal.GetModel(Fcid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Flection GetModelByCache(int Fid)
		{
			
			string CacheKey = "FlectionModel-" + Fid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Fid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Flection)objModel;
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
		public List<LearnSite.Model.Flection> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Flection> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Flection> modelList = new List<LearnSite.Model.Flection>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Flection model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.Flection();
					if(dt.Rows[n]["Fid"].ToString()!="")
					{
						model.Fid=int.Parse(dt.Rows[n]["Fid"].ToString());
					}
					if(dt.Rows[n]["Fcid"].ToString()!="")
					{
						model.Fcid=int.Parse(dt.Rows[n]["Fcid"].ToString());
					}
					if(dt.Rows[n]["Fhid"].ToString()!="")
					{
						model.Fhid=int.Parse(dt.Rows[n]["Fhid"].ToString());
					}
					model.Fcontent=dt.Rows[n]["Fcontent"].ToString();
					if(dt.Rows[n]["Fdate"].ToString()!="")
					{
						model.Fdate=DateTime.Parse(dt.Rows[n]["Fdate"].ToString());
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
        /// 获得指定Fcid的详细记录
        /// </summary>
        /// <param name="Fcid"></param>
        /// <returns></returns>
        public DataSet GetListCid(int Fcid)
        {
            string strWhere = " Fcid="+Fcid;
            return GetList(strWhere);
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

