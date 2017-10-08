using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Typer 的摘要说明。
	/// </summary>
	public class Typer
	{
		private readonly LearnSite.DAL.Typer dal=new LearnSite.DAL.Typer();
		public Typer()
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
		public bool Exists(int Tid)
		{
			return dal.Exists(Tid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Typer model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Typer model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Tid)
		{
			
			dal.Delete(Tid);
		}
                
        /// <summary>
        /// 根据编号获取文章标题
        /// </summary>
        /// <param name="Tid"></param>
        /// <returns></returns>
        public string GetTitle(int Tid)
        {
            return dal.GetTitle(Tid);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Typer GetModel(int Tid)
		{
			
			return dal.GetModel(Tid);
		}
        /// <summary>
        /// 随机得到一个对象实体
        /// </summary>
        public LearnSite.Model.Typer GetModelRnd(string tids)
        {
            return dal.GetModelRnd(tids);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Typer GetModelByCache(int Tid)
		{
			
			string CacheKey = "TyperModel-" + Tid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Tid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Typer)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

                /// <summary>
        /// 获得数据列表,不包含Tcontent内容
        /// </summary>
        public DataSet GetListArticle(string strWhere)
        {
            return dal.GetListArticle(strWhere);
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
		public List<LearnSite.Model.Typer> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Typer> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Typer> modelList = new List<LearnSite.Model.Typer>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Typer model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.Typer();
					if(dt.Rows[n]["Tid"].ToString()!="")
					{
						model.Tid=int.Parse(dt.Rows[n]["Tid"].ToString());
					}
					if(dt.Rows[n]["Ttype"].ToString()!="")
					{
						model.Ttype=int.Parse(dt.Rows[n]["Ttype"].ToString());
					}
					if(dt.Rows[n]["Tuse"].ToString()!="")
					{
						model.Tuse=int.Parse(dt.Rows[n]["Tuse"].ToString());
					}
					model.Ttitle=dt.Rows[n]["Ttitle"].ToString();
					model.Tcontent=dt.Rows[n]["Tcontent"].ToString();
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
        /// 获得指定Tid的文章
        /// </summary>
        /// <param name="Tid"></param>
        /// <returns></returns>
        public DataSet GetOneArticle(int Tid)
        {
            string strWhere = " Tid="+Tid;
            return GetList(strWhere);
        }
                /// <summary>
        /// 获得数据列表,不包含Tcontent内容的打字文章列表
        /// </summary>
        public DataSet GetListArticle()
        {
            return GetListArticle("");
        }
        /// <summary>
        /// 将打字文章Tid绑定到datalist中
        /// </summary>
        /// <param name="DLTid"></param>
        public DataSet ShowAllTid()
        {
            return dal.ShowAllTid();
        }
        /// <summary>
        /// 将指定打字文章Tid绑定到datalist中
        /// </summary>
        /// <param name="DLTid"></param>
        public DataSet ShowAllTid(string tids)
        {
            return dal.ShowAllTid(tids);
        }
        /// <summary>
        /// 获取所有文章标题Tid, Ttitle
        /// </summary>
        /// <returns></returns>
        public DataTable ShowAllTitle()
        {
            return dal.ShowAllTitle();
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

