using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// Gauge
	/// </summary>
	public partial class Gauge
	{
		private readonly LearnSite.DAL.Gauge dal=new LearnSite.DAL.Gauge();
		public Gauge()
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
		public bool Exists(int Gid)
		{
			return dal.Exists(Gid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Gauge model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.Gauge model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Gid)
		{
			
			return dal.Delete(Gid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Gidlist )
		{
			return dal.DeleteList(Gidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Gauge GetModel(int Gid)
		{
			
			return dal.GetModel(Gid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.Gauge GetModelByCache(int Gid)
		{
			
			string CacheKey = "GaugeModel-" + Gid;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Gid);
					if (objModel != null)
					{
						int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Gauge)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
                        
        /// <summary>
        /// 获取标题
        /// </summary>
        /// <param name="Gid"></param>
        /// <returns></returns>
        public string GetGtitle(int Gid)
        {
            return dal.GetGtitle(Gid);
        }

        /// <summary>
        /// 返回该学案类型该教师的自定义量化评价标准的Gid,Gtitle
        /// </summary>
        /// <param name="Gtype"></param>
        /// <param name="Ghid"></param>
        /// <returns></returns>
        public DataTable GetListGauge(int Ghid)
        {
            return dal.GetListGauge(Ghid);
        }
        /// <summary>
        /// 返回该老师添加的评价量规
        /// </summary>
        /// <param name="Ghid"></param>
        /// <returns></returns>
        public DataSet GetTeacherList(string Ghid)
        {
            string strWhere = "Ghid="+Ghid+" order by Gdate desc";
            return GetList(strWhere);
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
		public List<LearnSite.Model.Gauge> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Gauge> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Gauge> modelList = new List<LearnSite.Model.Gauge>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Gauge model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.Gauge();
					if(dt.Rows[n]["Gid"]!=null && dt.Rows[n]["Gid"].ToString()!="")
					{
						model.Gid=int.Parse(dt.Rows[n]["Gid"].ToString());
					}
					if(dt.Rows[n]["Ghid"]!=null && dt.Rows[n]["Ghid"].ToString()!="")
					{
						model.Ghid=int.Parse(dt.Rows[n]["Ghid"].ToString());
					}
					if(dt.Rows[n]["Gtype"]!=null && dt.Rows[n]["Gtype"].ToString()!="")
					{
					model.Gtype=dt.Rows[n]["Gtype"].ToString();
					}
					if(dt.Rows[n]["Gtitle"]!=null && dt.Rows[n]["Gtitle"].ToString()!="")
					{
					model.Gtitle=dt.Rows[n]["Gtitle"].ToString();
					}
					if(dt.Rows[n]["Gcount"]!=null && dt.Rows[n]["Gcount"].ToString()!="")
					{
						model.Gcount=int.Parse(dt.Rows[n]["Gcount"].ToString());
					}
					if(dt.Rows[n]["Gdate"]!=null && dt.Rows[n]["Gdate"].ToString()!="")
					{
						model.Gdate=DateTime.Parse(dt.Rows[n]["Gdate"].ToString());
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

