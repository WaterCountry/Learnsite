using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Quiz 的摘要说明。
	/// </summary>
	public class Quiz
	{
		private readonly LearnSite.DAL.Quiz dal=new LearnSite.DAL.Quiz();
		public Quiz()
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
		public bool Exists(int Qid)
		{
			return dal.Exists(Qid);
		}
                
        /// <summary>
        /// 初始化正确和错误统计
        /// </summary>
        public void initQuizRW()
        {
            dal.initQuizRW();
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Quiz model)
		{
			return dal.Add(model);
		}
                
        public void UpdateQright(string Quizrights)
        {
            dal.UpdateQright(Quizrights);
        }
        public void UpdateQwrong(string Quizwrongs)
        {
            dal.UpdateQwrong(Quizwrongs);
        }
        public void UpdateQaccuracy(string Quizqid)
        {
            dal.UpdateQaccuracy(Quizqid);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Quiz model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Qid)
		{
			
			dal.Delete(Qid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Quiz GetModel(int Qid)
		{
			
			return dal.GetModel(Qid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Quiz GetModelByCache(int Qid)
		{
			
			string CacheKey = "QuizModel-" + Qid;
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
			return (LearnSite.Model.Quiz)objModel;
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
		public List<LearnSite.Model.Quiz> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<LearnSite.Model.Quiz> DataTableToList(DataTable dt)
        {
            List<LearnSite.Model.Quiz> modelList = new List<LearnSite.Model.Quiz>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LearnSite.Model.Quiz model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new LearnSite.Model.Quiz();
                    if (dt.Rows[n]["Qid"].ToString() != "")
                    {
                        model.Qid = int.Parse(dt.Rows[n]["Qid"].ToString());
                    }
                    if (dt.Rows[n]["Qtype"].ToString() != "")
                    {
                        model.Qtype = int.Parse(dt.Rows[n]["Qtype"].ToString());
                    }
                    model.Question = dt.Rows[n]["Question"].ToString();
                    model.Qanswer = dt.Rows[n]["Qanswer"].ToString();
                    model.Qanalyze = dt.Rows[n]["Qanalyze"].ToString();
                    if (dt.Rows[n]["Qscore"].ToString() != "")
                    {
                        model.Qscore = int.Parse(dt.Rows[n]["Qscore"].ToString());
                    }
                    model.Qclass = dt.Rows[n]["Qclass"].ToString();
                    if (dt.Rows[n]["Qselect"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Qselect"].ToString() == "1") || (dt.Rows[n]["Qselect"].ToString().ToLower() == "true"))
                        {
                            model.Qselect = true;
                        }
                        else
                        {
                            model.Qselect = false;
                        }
                    }
                    if (dt.Rows[n]["Qright"].ToString() != "")
                    {
                        model.Qright = int.Parse(dt.Rows[n]["Qright"].ToString());
                    }
                    if (dt.Rows[n]["Qwrong"].ToString() != "")
                    {
                        model.Qwrong = int.Parse(dt.Rows[n]["Qwrong"].ToString());
                    }
                    if (dt.Rows[n]["Qaccuracy"].ToString() != "")
                    {
                        model.Qaccuracy = int.Parse(dt.Rows[n]["Qaccuracy"].ToString());
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
        /// 根据试题类型返回查询dataset
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByQtype(int Qtype)
        {
          return  dal.GetListByQtype(Qtype);
        }
                
        /// <summary>
        /// 根据试题类型和学案类型返回查询dataset
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByQtypeQclass(int Qtype, string Qclass)
        {
            return dal.GetListByQtypeQclass(Qtype, Qclass);
        }
                
        /// <summary>
        /// 根据试题类型，试题数量，返回随机查询dataset
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByQtypeNum(int Qtype, int num, string selectclass)
        {
            return dal.GetListByQtypeNum(Qtype, num, selectclass);
        }
        /// <summary>
        /// 根据数组查询返回试题
        /// </summary>
        /// <param name="quizQid"></param>
        /// <returns></returns>
        public DataSet GetListByQidArray(string[] quizQid)
        {
            return dal.GetListByQidArray(quizQid);
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

