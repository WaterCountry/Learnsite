using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// SurveyFeedback
	/// </summary>
	public partial class SurveyFeedback
	{
		private readonly LearnSite.DAL.SurveyFeedback dal=new LearnSite.DAL.SurveyFeedback();
		public SurveyFeedback()
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
		public bool Exists(int Fid)
		{
			return dal.Exists(Fid);
		}
                
        /// <summary>
        /// 是否存在该记录，不存在返回-1024
        /// </summary>
        public int ExistsScore(int Fvid, string Fnum)
        {
            return dal.ExistsScore(Fvid, Fnum);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.SurveyFeedback model)
		{
			return dal.Add(model);
		}
        /// <summary>
        /// 批量更新调查结果表中的调查类型
        /// </summary>
        /// <param name="Fvid"></param>
        /// <param name="Fvtype"></param>
        /// <returns></returns>
        public bool UpdateFvtype(int Fvid, int Fvtype)
        {
            return dal.UpdateFvtype(Fvid, Fvtype);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.SurveyFeedback model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Fid)
		{
			
			return dal.Delete(Fid);
		}
                
        /// <summary>
        /// 删除一个班级的调查记录
        /// </summary>
        public int DelClass(int Fgrade, int Fclass, int Fyear)
        {
            return dal.DelClass(Fgrade, Fclass, Fyear);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Fidlist )
		{
			return dal.DeleteList(Fidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.SurveyFeedback GetModel(int Fid)
		{
			
			return dal.GetModel(Fid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.SurveyFeedback GetModelByCache(int Fid)
		{
			
			string CacheKey = "SurveyFeedbackModel-" + Fid;
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
			return (LearnSite.Model.SurveyFeedback)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}                
        /// <summary>
        /// 已参加调查的本班同学人数
        /// </summary>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <param name="Fcid"></param>
        /// <param name="Fvid"></param>
        /// <returns></returns>
        public int GetSurveyStu(int Fgrade, int Fclass, int Fcid, int Fvid)
        {
            return dal.GetSurveyStu(Fgrade, Fclass, Fcid, Fvid);
        }
        /// <summary>
        /// 未参加调查的本班同学列表
        /// </summary>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <param name="Fcid"></param>
        /// <param name="Fvid"></param>
        /// <returns></returns>
        public string GetNoSurveyStu(int Fgrade, int Fclass, int Fcid, int Fvid)
        {
            return dal.GetNoSurveyStu(Fgrade, Fclass, Fcid, Fvid);
        }
        /// <summary>
        /// 将本班的学生调查选项合并到字符串中  
        /// </summary>
        /// <param name="Fyear"></param>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <param name="Fterm"></param>
        /// <param name="Fvid"></param>
        /// <returns></returns>
        public string GetClassFselect(int Fgrade, int Fclass, int Fvid)
        {
            return dal.GetClassFselect(Fgrade, Fclass, Fvid);
        }                
        /// <summary>
        /// 获取班级所有选中项平均分值
        /// </summary>
        /// <param name="Fyear"></param>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <param name="Fterm"></param>
        /// <param name="Fvid"></param>
        /// <returns></returns>
        public int GetClassYscore(int Fyear, int Fgrade, int Fclass, int Fterm, int Fvid)
        {
            return dal.GetClassYscore(Fyear, Fgrade, Fclass, Fterm, Fvid);
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
		public List<LearnSite.Model.SurveyFeedback> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.SurveyFeedback> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.SurveyFeedback> modelList = new List<LearnSite.Model.SurveyFeedback>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.SurveyFeedback model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.SurveyFeedback();
					if(dt.Rows[n]["Fid"]!=null && dt.Rows[n]["Fid"].ToString()!="")
					{
						model.Fid=int.Parse(dt.Rows[n]["Fid"].ToString());
					}
					if(dt.Rows[n]["Fnum"]!=null && dt.Rows[n]["Fnum"].ToString()!="")
					{
					model.Fnum=dt.Rows[n]["Fnum"].ToString();
					}
					if(dt.Rows[n]["Fyear"]!=null && dt.Rows[n]["Fyear"].ToString()!="")
					{
						model.Fyear=int.Parse(dt.Rows[n]["Fyear"].ToString());
					}
					if(dt.Rows[n]["Fgrade"]!=null && dt.Rows[n]["Fgrade"].ToString()!="")
					{
						model.Fgrade=int.Parse(dt.Rows[n]["Fgrade"].ToString());
					}
					if(dt.Rows[n]["Fclass"]!=null && dt.Rows[n]["Fclass"].ToString()!="")
					{
						model.Fclass=int.Parse(dt.Rows[n]["Fclass"].ToString());
					}
					if(dt.Rows[n]["Fterm"]!=null && dt.Rows[n]["Fterm"].ToString()!="")
					{
						model.Fterm=int.Parse(dt.Rows[n]["Fterm"].ToString());
					}
					if(dt.Rows[n]["Fcid"]!=null && dt.Rows[n]["Fcid"].ToString()!="")
					{
						model.Fcid=int.Parse(dt.Rows[n]["Fcid"].ToString());
					}
					if(dt.Rows[n]["Fvid"]!=null && dt.Rows[n]["Fvid"].ToString()!="")
					{
                        model.Fvid = int.Parse(dt.Rows[n]["Fvid"].ToString());
					}
					if(dt.Rows[n]["Fvtype"]!=null && dt.Rows[n]["Fvtype"].ToString()!="")
					{
						model.Fvtype=int.Parse(dt.Rows[n]["Fvtype"].ToString());
					}
					if(dt.Rows[n]["Fselect"]!=null && dt.Rows[n]["Fselect"].ToString()!="")
					{
					model.Fselect=dt.Rows[n]["Fselect"].ToString();
					}
					if(dt.Rows[n]["Fscore"]!=null && dt.Rows[n]["Fscore"].ToString()!="")
					{
						model.Fscore=int.Parse(dt.Rows[n]["Fscore"].ToString());
					}
					if(dt.Rows[n]["Fdate"]!=null && dt.Rows[n]["Fdate"].ToString()!="")
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
        /// 获取本班测验结果（包含调查结果）Fscore,Snum,Sname,Sgrade,Sclass
        /// </summary>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <param name="Fvid"></param>
        /// <returns></returns>
        public DataSet GetClassFscore(int Fgrade, int Fclass, int Fvid)
        {
            return dal.GetClassFscore(Fgrade, Fclass, Fvid);
        }
                
        /// <summary>
        /// 获取本班调查成绩
        /// </summary>
        /// <param name="Fvid"></param>
        /// <param name="Fgrade"></param>
        /// <param name="Fclass"></param>
        /// <returns></returns>
        public DataTable GetClassScore(int Fvid, int Fgrade, int Fclass)
        {
            return dal.GetClassScore(Fvid, Fgrade, Fclass);
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
        /// 获取当前班级学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowFeedbackCids(int Fgrade, int Fclass, int Fterm, int Fyear)
        {
            return dal.ShowFeedbackCids(Fgrade, Fclass, Fterm, Fyear);
        }
        /// <summary>
        /// 获取某学生学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowStuFeedbackCids(string Snum, int Fterm, int Fgrade)
        {
            return dal.ShowStuFeedbackCids(Snum,Fterm,Fgrade);
        }
                
        /// <summary>
        /// 获取该调查同选项的同班同学Key为学号，Value为姓名
        /// </summary>
        /// <param name="Syear"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Vid"></param>
        /// <param name="Mid"></param>
        /// <returns></returns>
        public Hashtable ShowItemClassMate(int Syear, int Sgrade, int Sclass, int Vid, int Mid)
        {
            return dal.ShowItemClassMate(Syear, Sgrade, Sclass, Vid, Mid);
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

