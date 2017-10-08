using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// SurveyClass
	/// </summary>
	public partial class SurveyClass
	{
		private readonly LearnSite.DAL.SurveyClass dal=new LearnSite.DAL.SurveyClass();
		public SurveyClass()
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
		public bool Exists(int Yid)
		{
			return dal.Exists(Yid);
		}                
        /// <summary>
        /// 是否存在该班级记录
        /// </summary>
        public int ExistsClass(int Yyear, int Ygrade, int Yclass, int Yterm, int Yvid)
        {
            return dal.ExistsClass(Yyear, Ygrade, Yclass, Yterm, Yvid);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.SurveyClass model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.SurveyClass model)
		{
			return dal.Update(model);
		}
                
        /// <summary>
        /// 更新一条数据，部分字段更新
        /// </summary>
        public bool UpdateClass(LearnSite.Model.SurveyClass model)
        {
            return dal.UpdateClass(model);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Yid)
		{
			
			return dal.Delete(Yid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Yidlist )
		{
			return dal.DeleteList(Yidlist );
		}
                
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.SurveyClass GetModelByClass(int Yyear, int Ygrade, int Yclass, int Yterm, int Ycid, int Yvid)
        {
            return dal.GetModelByClass(Yyear, Ygrade, Yclass, Yterm, Ycid, Yvid);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.SurveyClass GetModel(int Yid)
		{
			
			return dal.GetModel(Yid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.SurveyClass GetModelByCache(int Yid)
		{
			
			string CacheKey = "SurveyClassModel-" + Yid;
			object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Yid);
					if (objModel != null)
					{
						int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
						LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.SurveyClass)objModel;
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
		public List<LearnSite.Model.SurveyClass> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.SurveyClass> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.SurveyClass> modelList = new List<LearnSite.Model.SurveyClass>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.SurveyClass model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.SurveyClass();
					if(dt.Rows[n]["Yid"]!=null && dt.Rows[n]["Yid"].ToString()!="")
					{
						model.Yid=int.Parse(dt.Rows[n]["Yid"].ToString());
					}
					if(dt.Rows[n]["Yyear"]!=null && dt.Rows[n]["Yyear"].ToString()!="")
					{
						model.Yyear=int.Parse(dt.Rows[n]["Yyear"].ToString());
					}
					if(dt.Rows[n]["Ygrade"]!=null && dt.Rows[n]["Ygrade"].ToString()!="")
					{
						model.Ygrade=int.Parse(dt.Rows[n]["Ygrade"].ToString());
					}
					if(dt.Rows[n]["Yclass"]!=null && dt.Rows[n]["Yclass"].ToString()!="")
					{
						model.Yclass=int.Parse(dt.Rows[n]["Yclass"].ToString());
					}
					if(dt.Rows[n]["Yterm"]!=null && dt.Rows[n]["Yterm"].ToString()!="")
					{
						model.Yterm=int.Parse(dt.Rows[n]["Yterm"].ToString());
					}
					if(dt.Rows[n]["Ycid"]!=null && dt.Rows[n]["Ycid"].ToString()!="")
					{
						model.Ycid=int.Parse(dt.Rows[n]["Ycid"].ToString());
					}
					if(dt.Rows[n]["Yvid"]!=null && dt.Rows[n]["Yvid"].ToString()!="")
					{
						model.Yvid=int.Parse(dt.Rows[n]["Yvid"].ToString());
					}
					if(dt.Rows[n]["Yselect"]!=null && dt.Rows[n]["Yselect"].ToString()!="")
					{
					model.Yselect=dt.Rows[n]["Yselect"].ToString();
					}
					if(dt.Rows[n]["Ycount"]!=null && dt.Rows[n]["Ycount"].ToString()!="")
					{
					model.Ycount=dt.Rows[n]["Ycount"].ToString();
					}
					if(dt.Rows[n]["Yscore"]!=null && dt.Rows[n]["Yscore"].ToString()!="")
					{
						model.Yscore=int.Parse(dt.Rows[n]["Yscore"].ToString());
					}
					if(dt.Rows[n]["Ydate"]!=null && dt.Rows[n]["Ydate"].ToString()!="")
					{
						model.Ydate=DateTime.Parse(dt.Rows[n]["Ydate"].ToString());
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

