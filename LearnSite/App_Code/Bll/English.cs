using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
using System.Web;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类English 的摘要说明。
	/// </summary>
	public class English
	{
		private readonly LearnSite.DAL.English dal=new LearnSite.DAL.English();
		public English()
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
		public bool Exists(int Eid)
		{
			return dal.Exists(Eid);
		}
                
        /// <summary>
        /// 统计相应级别的单词数
        /// </summary>
        /// <param name="Elevel"></param>
        /// <returns></returns>
        public int CountLevel(int Elevel)
        {
            return dal.CountLevel(Elevel);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.English model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(LearnSite.Model.English model)
        {
            return dal.Update(model);
        }
                
        /// <summary>
        /// 将该词标记为相应级别
        /// </summary>
        /// <param name="Eword"></param>
        /// <param name="Elevel"></param>
        public bool UpdateElevel(string Eword, int Elevel)
        {
            return dal.UpdateElevel(Eword, Elevel);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Eid)
		{
			
			dal.Delete(Eid);
		}
                
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteAll()
        {
            dal.DeleteAll();
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.English GetModel(int Eid)
		{
			
			return dal.GetModel(Eid);
		}                
        /// <summary>
        /// 随机获取一个单词实体
        /// </summary>
        /// <returns></returns>
        public LearnSite.Model.English GetRndModel(int Elevel)
        {
            return dal.GetRndModel(Elevel);
        }
        /// <summary>
        /// 获取随机单词并对编号单词意思进行分隔
        /// </summary>
        /// <param name="Elevel"></param>
        /// <returns></returns>
        public string RndWord(int Elevel)
        {
            string str = "";
            Model.English emodel = new Model.English();
            emodel = dal.GetRndModel(Elevel);
            if (emodel != null)
            {
                str = emodel.Eid + "|" + emodel.Eword + "|" + emodel.Emeaning;
            }
            return str;
        }

        public string NextWord(int Eid, int Elevel)
        {
            string str = "";
            Model.English emodel = new Model.English();
            emodel = dal.GetNextModel(Eid, Elevel);
            if (emodel != null)
            {
                str = emodel.Eid + "|" + emodel.Eword + "|" + emodel.Emeaning;
            }
            return str;
        }

        /// <summary>
        /// 随机获取一个单词实体
        /// </summary>
        /// <returns></returns>
        public LearnSite.Model.English GetNextModel(int Eid,int Elevel)
        {
            return dal.GetNextModel(Eid,Elevel);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.English GetModelByCache(int Eid)
		{
			
			string CacheKey = "EnglishModel-" + Eid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Eid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.English)objModel;
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
		public List<LearnSite.Model.English> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<LearnSite.Model.English> DataTableToList(DataTable dt)
        {
            List<LearnSite.Model.English> modelList = new List<LearnSite.Model.English>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LearnSite.Model.English model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new LearnSite.Model.English();
                    if (dt.Rows[n]["Eid"].ToString() != "")
                    {
                        model.Eid = int.Parse(dt.Rows[n]["Eid"].ToString());
                    }
                    model.Eword = dt.Rows[n]["Eword"].ToString();
                    model.Emeaning = dt.Rows[n]["Emeaning"].ToString();
                    if (dt.Rows[n]["Elevel"].ToString() != "")
                    {
                        model.Elevel = int.Parse(dt.Rows[n]["Elevel"].ToString());
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
        /// 获取某等级所有单词和意思
        /// </summary>
        /// <returns></returns>
        public string GetLevelwords(int Elevel)
        {
            return dal.GetLevelwords(Elevel);
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

