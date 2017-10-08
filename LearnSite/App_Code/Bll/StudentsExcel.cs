using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类StudentsExcel 的摘要说明。
	/// </summary>
	public class StudentsExcel
	{
		private readonly LearnSite.DAL.StudentsExcel dal=new LearnSite.DAL.StudentsExcel();
		public StudentsExcel()
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
		public bool Exists(int Sid)
		{
			return dal.Exists(Sid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.StudentsExcel model)
		{
			return dal.Add(model);
		}

               /// <summary>
        /// 导入一条数据,model中无Sscore,Sattitude,Sape
        /// </summary>
        public int AddFromExcelDs(LearnSite.Model.StudentsExcel model)
        {
          return  dal.AddFromExcelDs(model);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.StudentsExcel model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Sid)
		{
			
			dal.Delete(Sid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.StudentsExcel GetModel(int Sid)
		{
			
			return dal.GetModel(Sid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.StudentsExcel GetModelByCache(int Sid)
		{
			
			string CacheKey = "StudentsExcelModel-" + Sid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Sid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.StudentsExcel)objModel;
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
		public List<LearnSite.Model.StudentsExcel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.StudentsExcel> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.StudentsExcel> modelList = new List<LearnSite.Model.StudentsExcel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.StudentsExcel model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.StudentsExcel();
					if(dt.Rows[n]["Sid"].ToString()!="")
					{
						model.Sid=int.Parse(dt.Rows[n]["Sid"].ToString());
					}
					model.Snum=dt.Rows[n]["Snum"].ToString();
					if(dt.Rows[n]["Syear"].ToString()!="")
					{
						model.Syear=int.Parse(dt.Rows[n]["Syear"].ToString());
					}
					if(dt.Rows[n]["Sgrade"].ToString()!="")
					{
						model.Sgrade=int.Parse(dt.Rows[n]["Sgrade"].ToString());
					}
					if(dt.Rows[n]["Sclass"].ToString()!="")
					{
						model.Sclass=int.Parse(dt.Rows[n]["Sclass"].ToString());
					}
					model.Sname=dt.Rows[n]["Sname"].ToString();
					model.Spwd=dt.Rows[n]["Spwd"].ToString();
					model.Sex=dt.Rows[n]["Sex"].ToString();
					model.Saddress=dt.Rows[n]["Saddress"].ToString();
					model.Sphone=dt.Rows[n]["Sphone"].ToString();
					model.Sparents=dt.Rows[n]["Sparents"].ToString();
					model.Sheadtheacher=dt.Rows[n]["Sheadtheacher"].ToString();
					if(dt.Rows[n]["Sscore"].ToString()!="")
					{
						model.Sscore=int.Parse(dt.Rows[n]["Sscore"].ToString());
					}
                    if (dt.Rows[n]["Squiz"].ToString() != "")
                    {
                        model.Squiz = int.Parse(dt.Rows[n]["Squiz"].ToString());
                    }
					if(dt.Rows[n]["Sattitude"].ToString()!="")
					{
						model.Sattitude=int.Parse(dt.Rows[n]["Sattitude"].ToString());
					}
					model.Sape=dt.Rows[n]["Sape"].ToString();
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
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法
	}
}

