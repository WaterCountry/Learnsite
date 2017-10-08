using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// GaugeFeedback
	/// </summary>
	public partial class GaugeFeedback
	{
		private readonly LearnSite.DAL.GaugeFeedback dal=new LearnSite.DAL.GaugeFeedback();
		public GaugeFeedback()
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
        /// 是否存在该记录
        /// </summary>
        public bool ExistsFnum(int Fwid, int Fsid)
        {
            return dal.ExistsFnum(Fwid, Fsid);
        }
                
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="Fgid">评价标准ID</param>
        /// <param name="Fselect">评价选项Mid</param>
        /// <returns></returns>
        public bool ExistsFselect(int Fgid, string Fselect)
        {
            return dal.ExistsFselect(Fgid, Fselect);
        }
                
        /// <summary>
        /// 求某个作品互评的平均分
        /// </summary>
        /// <param name="Fwid"></param>
        /// <returns></returns>
        public int AvgFwid(int Fwid)
        {
            return dal.AvgFwid(Fwid);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.GaugeFeedback model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.GaugeFeedback model)
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
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Fidlist )
		{
			return dal.DeleteList(Fidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.GaugeFeedback GetModel(int Fid)
		{
			
			return dal.GetModel(Fid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.GaugeFeedback GetModelByCache(int Fid)
		{
			
			string CacheKey = "GaugeFeedbackModel-" + Fid;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Fid);
					if (objModel != null)
					{
						int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.GaugeFeedback)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}        
        /// <summary>
        /// 返回本作品的所有互评选项
        /// </summary>
        /// <param name="Fwid"></param>
        /// <returns></returns>
        public string GetWorkFeedback(int Fwid)
        {
            return dal.GetWorkFeedback(Fwid);
        }
            
        /// <summary>
        /// 返回自己的评价
        /// </summary>
        /// <param name="Fwid"></param>
        /// <param name="Fnum"></param>
        /// <returns></returns>
        public string GetMyFeedback(int Fwid, string Fnum)
        {
            return dal.GetMyFeedback(Fwid, Fnum);
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
		public List<LearnSite.Model.GaugeFeedback> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.GaugeFeedback> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.GaugeFeedback> modelList = new List<LearnSite.Model.GaugeFeedback>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.GaugeFeedback model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.GaugeFeedback();
					if(dt.Rows[n]["Fid"]!=null && dt.Rows[n]["Fid"].ToString()!="")
					{
						model.Fid=int.Parse(dt.Rows[n]["Fid"].ToString());
					}
					if(dt.Rows[n]["Fnum"]!=null && dt.Rows[n]["Fnum"].ToString()!="")
					{
					model.Fnum=dt.Rows[n]["Fnum"].ToString();
					}
					if(dt.Rows[n]["Fgrade"]!=null && dt.Rows[n]["Fgrade"].ToString()!="")
					{
						model.Fgrade=int.Parse(dt.Rows[n]["Fgrade"].ToString());
					}
					if(dt.Rows[n]["Fclass"]!=null && dt.Rows[n]["Fclass"].ToString()!="")
					{
						model.Fclass=int.Parse(dt.Rows[n]["Fclass"].ToString());
					}
					if(dt.Rows[n]["Fcid"]!=null && dt.Rows[n]["Fcid"].ToString()!="")
					{
						model.Fcid=int.Parse(dt.Rows[n]["Fcid"].ToString());
					}
					if(dt.Rows[n]["Fmid"]!=null && dt.Rows[n]["Fmid"].ToString()!="")
					{
						model.Fmid=int.Parse(dt.Rows[n]["Fmid"].ToString());
					}
					if(dt.Rows[n]["Fwid"]!=null && dt.Rows[n]["Fwid"].ToString()!="")
					{
						model.Fwid=int.Parse(dt.Rows[n]["Fwid"].ToString());
					}
					if(dt.Rows[n]["Fgid"]!=null && dt.Rows[n]["Fgid"].ToString()!="")
					{
						model.Fgid=int.Parse(dt.Rows[n]["Fgid"].ToString());
					}
					if(dt.Rows[n]["Fselect"]!=null && dt.Rows[n]["Fselect"].ToString()!="")
					{
					model.Fselect=dt.Rows[n]["Fselect"].ToString();
					}
					if(dt.Rows[n]["Fscore"]!=null && dt.Rows[n]["Fscore"].ToString()!="")
					{
						model.Fscore=int.Parse(dt.Rows[n]["Fscore"].ToString());
					}
					if(dt.Rows[n]["Fgood"]!=null && dt.Rows[n]["Fgood"].ToString()!="")
					{
						if((dt.Rows[n]["Fgood"].ToString()=="1")||(dt.Rows[n]["Fgood"].ToString().ToLower()=="true"))
						{
						model.Fgood=true;
						}
						else
						{
							model.Fgood=false;
						}
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

