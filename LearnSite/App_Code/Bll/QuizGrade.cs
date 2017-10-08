using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类QuizGrade 的摘要说明。
	/// </summary>
	public class QuizGrade
	{
		private readonly LearnSite.DAL.QuizGrade dal=new LearnSite.DAL.QuizGrade();
		public QuizGrade()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Qid)
		{
			return dal.Exists(Qid);
		}               
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public string ExistsQobj(int Qobj,int Qhid)
        {
            return dal.ExistsQobj(Qobj,Qhid);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.QuizGrade model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.QuizGrade model)
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
        /// 删除Qhid空字段记录数据
        /// </summary>
        public void DeleteNull()
        {
            dal.DeleteNull();
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.QuizGrade GetModel(int Qid)
		{
			
			return dal.GetModel(Qid);
		}
         /// <summary>
        /// Qobj根据年级得到一个对象实体
        /// </summary>
        public LearnSite.Model.QuizGrade GetModelByQobj(int Qobj)
        {
            return dal.GetModelByQobj(Qobj);
        }
                
        /// <summary>
        /// 根据年级，教师编号得到一个对象实体
        /// </summary>
        public LearnSite.Model.QuizGrade GetModelByQobjQhid(int Qobj, int Qhid)
        {
            return dal.GetModelByQobjQhid(Qobj, Qhid);
        }                
        /// <summary>
        /// 根据年级，班级得到一个对象实体
        /// </summary>
        public LearnSite.Model.QuizGrade GetModelByQobjRclass(int Qobj, int Rclass)
        {
            return dal.GetModelByQobjRclass(Qobj, Rclass);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.QuizGrade GetModelByCache(int Qid)
		{
			
			string CacheKey = "QuizGradeModel-" + Qid;
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
			return (LearnSite.Model.QuizGrade)objModel;
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
		public List<LearnSite.Model.QuizGrade> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<LearnSite.Model.QuizGrade> DataTableToList(DataTable dt)
        {
            List<LearnSite.Model.QuizGrade> modelList = new List<LearnSite.Model.QuizGrade>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LearnSite.Model.QuizGrade model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new LearnSite.Model.QuizGrade();
                    if (dt.Rows[n]["Qid"].ToString() != "")
                    {
                        model.Qid = int.Parse(dt.Rows[n]["Qid"].ToString());
                    }
                    if (dt.Rows[n]["Qobj"].ToString() != "")
                    {
                        model.Qobj = int.Parse(dt.Rows[n]["Qobj"].ToString());
                    }
                    model.Qclass = dt.Rows[n]["Qclass"].ToString();
                    if (dt.Rows[n]["Qhid"].ToString() != "")
                    {
                        model.Qhid = int.Parse(dt.Rows[n]["Qhid"].ToString());
                    }
                    if (dt.Rows[n]["Qonly"].ToString() != "")
                    {
                        model.Qonly = int.Parse(dt.Rows[n]["Qonly"].ToString());
                    }
                    if (dt.Rows[n]["Qmore"].ToString() != "")
                    {
                        model.Qmore = int.Parse(dt.Rows[n]["Qmore"].ToString());
                    }
                    if (dt.Rows[n]["Qjudge"].ToString() != "")
                    {
                        model.Qjudge = int.Parse(dt.Rows[n]["Qjudge"].ToString());
                    }
                    if (dt.Rows[n]["Qopen"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Qopen"].ToString() == "1") || (dt.Rows[n]["Qopen"].ToString().ToLower() == "true"))
                        {
                            model.Qopen = true;
                        }
                        else
                        {
                            model.Qopen = false;
                        }
                    }
                    if (dt.Rows[n]["Qanswer"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Qanswer"].ToString() == "1") || (dt.Rows[n]["Qanswer"].ToString().ToLower() == "true"))
                        {
                            model.Qanswer = true;
                        }
                        else
                        {
                            model.Qanswer = false;
                        }
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
        /// 根据Qobj查询Qclass
        /// </summary>
        /// <param name="Qobj"></param>
        /// <returns></returns>
        public string GetQclass(int Qobj)
        {
            return dal.GetQclass(Qobj);
        }
                
        /// <summary>
        /// 获取年级测验答案显示开关
        /// </summary>
        /// <param name="Qobj"></param>
        /// <param name="Rclass"></param>
        /// <returns></returns>
        public bool GetQanswer(int Qobj, int Rclass)
        {
            return dal.GetQanswer(Qobj, Rclass);
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

