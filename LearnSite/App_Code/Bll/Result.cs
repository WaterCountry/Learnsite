using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Result 的摘要说明。
	/// </summary>
	public class Result
	{
		private readonly LearnSite.DAL.Result dal=new LearnSite.DAL.Result();
		public Result()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Rid)
		{
			return dal.Exists(Rid);
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsBynumdate(int Rsid, DateTime Rdate)
        {
            return dal.ExistsBynumdate(Rsid, Rdate);
        }
                
        /// <summary>
        /// 更新今天测验成绩，返回影响的行数
        /// </summary>
        /// <param name="Rnum"></param>
        /// <param name="Rscore"></param>
        /// <param name="Rdate"></param>
        /// <returns></returns>
        public int UpdateGood(string Rnum, int Rscore, DateTime Rdate)
        {
            return dal.UpdateGood(Rnum, Rscore, Rdate);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Result model)
		{
			return dal.Add(model);
		}
        /// <summary>
        /// 更新一条数据中的Rscore，Rhistory，Rwrong
        /// </summary>
        public bool UpdateToday(LearnSite.Model.Result model)
        {
            return dal.UpdateToday(model);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Result model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Rid)
		{
			
			dal.Delete(Rid);
		}
                
        /// <summary>
        /// 清除几年前的测验记录
        /// </summary>
        /// <param name="Wyear"></param>
        public int DeleteOldyear(int Wyear)
        {
            return dal.DeleteOldyear(Wyear);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Result GetModel(int Rid)
		{
			
			return dal.GetModel(Rid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Result GetModelByCache(int Rid)
		{
			
			string CacheKey = "ResultModel-" + Rid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Rid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Result)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

                /// <summary>
        /// 获得该学号的测验数据列表
        /// </summary>
        /// <param name="Rnum"></param>
        /// <returns></returns>
        public DataSet GetListScore(string Rnum)
        {
            return dal.GetListScore(Rnum);
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
		public List<LearnSite.Model.Result> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Result> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Result> modelList = new List<LearnSite.Model.Result>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Result model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.Result();
					if(dt.Rows[n]["Rid"].ToString()!="")
					{
						model.Rid=int.Parse(dt.Rows[n]["Rid"].ToString());
					}
					model.Rnum=dt.Rows[n]["Rnum"].ToString();
					if(dt.Rows[n]["Rscore"].ToString()!="")
					{
						model.Rscore=int.Parse(dt.Rows[n]["Rscore"].ToString());
					}
					if(dt.Rows[n]["Rdate"].ToString()!="")
					{
						model.Rdate=DateTime.Parse(dt.Rows[n]["Rdate"].ToString());
					}
                    model.Rhistory = dt.Rows[n]["Rhistory"].ToString();
                    model.Rwrong = dt.Rows[n]["Rwrong"].ToString();
                    if (dt.Rows[n]["Rgrade"].ToString() != "")
                    {
                        model.Rgrade = int.Parse(dt.Rows[n]["Rgrade"].ToString());
                    } 
                    if (dt.Rows[n]["Rterm"].ToString() != "")
                    {
                        model.Rterm = int.Parse(dt.Rows[n]["Rterm"].ToString());
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
        /// 获取本班今天常识测验排行榜
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet GetListTodayScore(int Sgrade, int Sclass)
        {
            return dal.GetListTodayScore(Sgrade, Sclass);
        }
                
        /// <summary>
        /// 获取本班今天常识测验未进行的同学姓名
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string GetListTodayNoScore(int Sgrade, int Sclass)
        {
            return dal.GetListTodayNoScore(Sgrade, Sclass);
        }
        /// <summary>
        /// 根据学号求测验的总平均值
        /// </summary>
        /// <param name="Rnum"></param>
        /// <returns></returns>
        public void GetAverage(int Rsid, int Rgrade, int Rterm)
        {
            dal.GetAverage(Rsid,Rgrade,Rterm);
        }        
        /// <summary>
        /// 根据学号求测验的最高值
        /// </summary>
        /// <param name="Rnum"></param>
        /// <returns></returns>
        public int GetMax(int Rsid, string Rgrade, string Rterm)
        {
            return dal.GetMax(Rsid, Rgrade, Rterm);
        }
        /// <summary>
        /// 获取本次测验试卷编号记录
        /// </summary>
        /// <param name="Rid"></param>
        /// <returns></returns>
        public string GetRhistory(int Rid)
        {
            return dal.GetRhistory(Rid);
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

