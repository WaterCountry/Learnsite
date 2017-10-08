using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// SurveyQuestion
	/// </summary>
	public partial class SurveyQuestion
	{
		private readonly LearnSite.DAL.SurveyQuestion dal=new LearnSite.DAL.SurveyQuestion();
		public SurveyQuestion()
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
		public bool Exists(int Qid)
		{
			return dal.Exists(Qid);
		}
                
        /// <summary>
        /// 是否存在该调查卷试题记录
        /// </summary>
        public bool ExistsByQvid(int Qvid)
        {
            return dal.ExistsByQvid(Qvid);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.SurveyQuestion model)
		{
			return dal.Add(model);
		}                
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateQtitle(int Qid, string Qtitle)
        {
            return dal.UpdateQtitle(Qid, Qtitle);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.SurveyQuestion model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Qid)
		{
			
			return dal.Delete(Qid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Qidlist )
		{
			return dal.DeleteList(Qidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.SurveyQuestion GetModel(int Qid)
		{
			
			return dal.GetModel(Qid);
		}
                
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.SurveyQuestion GetModel(DataTable dt, int Tsort)
        {
            return dal.GetModel(dt, Tsort);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.SurveyQuestion GetModelByCache(int Qid)
		{
			
			string CacheKey = "SurveyQuestionModel-" + Qid;
			object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Qid);
					if (objModel != null)
					{
						int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
						LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.SurveyQuestion)objModel;
		}
        public DataSet GetListByQvid(int Qvid)
        {
            string strWhere = " Qvid="+Qvid+" order by Qid asc";
            return GetList(strWhere);        
        }
                
        /// <summary>
        /// 根据ＩＤ返回调查题目
        /// </summary>
        /// <param name="Qid"></param>
        /// <returns></returns>
        public string GetTitle(int Qid)
        {
            return dal.GetTitle(Qid);
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
		public List<LearnSite.Model.SurveyQuestion> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.SurveyQuestion> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.SurveyQuestion> modelList = new List<LearnSite.Model.SurveyQuestion>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.SurveyQuestion model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.SurveyQuestion();
					if(dt.Rows[n]["Qid"]!=null && dt.Rows[n]["Qid"].ToString()!="")
					{
						model.Qid=int.Parse(dt.Rows[n]["Qid"].ToString());
					}
					if(dt.Rows[n]["Qvid"]!=null && dt.Rows[n]["Qvid"].ToString()!="")
					{
						model.Qvid=int.Parse(dt.Rows[n]["Qvid"].ToString());
					}
					if(dt.Rows[n]["Qcid"]!=null && dt.Rows[n]["Qcid"].ToString()!="")
					{
						model.Qcid=int.Parse(dt.Rows[n]["Qcid"].ToString());
					}
					if(dt.Rows[n]["Qtitle"]!=null && dt.Rows[n]["Qtitle"].ToString()!="")
					{
					model.Qtitle=dt.Rows[n]["Qtitle"].ToString();
					}
					if(dt.Rows[n]["Qcount"]!=null && dt.Rows[n]["Qcount"].ToString()!="")
					{
						model.Qcount=int.Parse(dt.Rows[n]["Qcount"].ToString());
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

