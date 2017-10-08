using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// Survey
	/// </summary>
	public partial class Survey
	{
		private readonly LearnSite.DAL.Survey dal=new LearnSite.DAL.Survey();
		public Survey()
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
		public bool Exists(int Vid)
		{
			return dal.Exists(Vid);
		}
                
        /// <summary>
        /// 增加一条数据Vcid,Vhid,Vtitle,Vcontent,Vtype,Vclose,Vpoint,Vdate
        /// </summary>
        public int Addsurvey(LearnSite.Model.Survey model)
        {
            return dal.Addsurvey(model);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Survey model)
		{
			return dal.Add(model);
		}
        /// <summary>
        /// 开关调查,取反
        /// </summary>
        /// <param name="Vid"></param>
        /// <returns></returns>
        public bool UpdateVclose(int Vid)
        {
            return dal.UpdateVclose(Vid);
        }                
        /// <summary>
        /// 关闭调查
        /// </summary>
        /// <param name="Vid"></param>
        public void SetClose(int Vhid)
        {
            dal.SetClose(Vhid);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.Survey model)
		{
			return dal.Update(model);
		}                
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateSurvey(LearnSite.Model.Survey model)
        {
            return dal.UpdateSurvey(model);
        }                
        /// <summary>
        /// 统计调查卷下的试题数
        /// </summary>
        /// <param name="Vid"></param>
        /// <returns></returns>
        public bool Updatevtotal(int Vid)
        {
            return dal.Updatevtotal(Vid);
        }                
        /// <summary>
        /// 统计调查卷下的试题总分
        /// </summary>
        /// <param name="Vid"></param>
        /// <returns></returns>
        public bool Updatevscore(int Vid)
        {
            return dal.Updatevscore(Vid);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Vid)
		{
			
			return dal.Delete(Vid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Vidlist )
		{
			return dal.DeleteList(Vidlist );
		}
                
        /// <summary>
        /// 返回调查名称Vtitle
        /// </summary>
        /// <param name="Vid"></param>
        /// <returns></returns>
        public string GetVtitle(int Vid)
        {
            return dal.GetVtitle(Vid);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Survey GetModel(int Vid)
		{
			
			return dal.GetModel(Vid);
		}
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.Survey GetModel(DataTable dt, int Tsort)
        {
            return dal.GetModel(dt, Tsort);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.Survey GetModelByCache(int Vid)
		{
			
			string CacheKey = "SurveyModel-" + Vid;
			object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Vid);
					if (objModel != null)
					{
						int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
						LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Survey)objModel;
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByVcid(int Vcid)
        {
            string strWhere = " Vcid="+Vcid+" order by Vpoint asc, Vid asc";
            return GetList(strWhere);
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
		public List<LearnSite.Model.Survey> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Survey> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Survey> modelList = new List<LearnSite.Model.Survey>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Survey model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.Survey();
					if(dt.Rows[n]["Vid"]!=null && dt.Rows[n]["Vid"].ToString()!="")
					{
						model.Vid=int.Parse(dt.Rows[n]["Vid"].ToString());
					}
					if(dt.Rows[n]["Vcid"]!=null && dt.Rows[n]["Vcid"].ToString()!="")
					{
						model.Vcid=int.Parse(dt.Rows[n]["Vcid"].ToString());
					}
					if(dt.Rows[n]["Vhid"]!=null && dt.Rows[n]["Vhid"].ToString()!="")
					{
						model.Vhid=int.Parse(dt.Rows[n]["Vhid"].ToString());
					}
					if(dt.Rows[n]["Vtitle"]!=null && dt.Rows[n]["Vtitle"].ToString()!="")
					{
					model.Vtitle=dt.Rows[n]["Vtitle"].ToString();
					}
					if(dt.Rows[n]["Vcontent"]!=null && dt.Rows[n]["Vcontent"].ToString()!="")
					{
					model.Vcontent=dt.Rows[n]["Vcontent"].ToString();
					}
					if(dt.Rows[n]["Vtype"]!=null && dt.Rows[n]["Vtype"].ToString()!="")
					{
						model.Vtype=int.Parse(dt.Rows[n]["Vtype"].ToString());
					}
					if(dt.Rows[n]["Vtotal"]!=null && dt.Rows[n]["Vtotal"].ToString()!="")
					{
						model.Vtotal=int.Parse(dt.Rows[n]["Vtotal"].ToString());
					}
					if(dt.Rows[n]["Vscore"]!=null && dt.Rows[n]["Vscore"].ToString()!="")
					{
						model.Vscore=int.Parse(dt.Rows[n]["Vscore"].ToString());
					}
					if(dt.Rows[n]["Vaverage"]!=null && dt.Rows[n]["Vaverage"].ToString()!="")
					{
						model.Vaverage=int.Parse(dt.Rows[n]["Vaverage"].ToString());
					}
					if(dt.Rows[n]["Vclose"]!=null && dt.Rows[n]["Vclose"].ToString()!="")
					{
						if((dt.Rows[n]["Vclose"].ToString()=="1")||(dt.Rows[n]["Vclose"].ToString().ToLower()=="true"))
						{
						model.Vclose=true;
						}
						else
						{
							model.Vclose=false;
						}
					}
					if(dt.Rows[n]["Vpoint"]!=null && dt.Rows[n]["Vpoint"].ToString()!="")
					{
						if((dt.Rows[n]["Vpoint"].ToString()=="1")||(dt.Rows[n]["Vpoint"].ToString().ToLower()=="true"))
						{
						model.Vpoint=true;
						}
						else
						{
							model.Vpoint=false;
						}
					}
					if(dt.Rows[n]["Vdate"]!=null && dt.Rows[n]["Vdate"].ToString()!="")
					{
						model.Vdate=DateTime.Parse(dt.Rows[n]["Vdate"].ToString());
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

