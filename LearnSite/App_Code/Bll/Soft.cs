using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Soft 的摘要说明。
	/// </summary>
	public class Soft
	{
		private readonly LearnSite.DAL.Soft dal=new LearnSite.DAL.Soft();
		public Soft()
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
		public bool Exists(int Fid)
		{
			return dal.Exists(Fid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Soft model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Soft model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Fid)
		{
			
			dal.Delete(Fid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Soft GetModel(int Fid)
		{
			
			return dal.GetModel(Fid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Soft GetModelByCache(int Fid)
		{
			
			string CacheKey = "SoftModel-" + Fid;
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
			return (LearnSite.Model.Soft)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList()
		{
			return dal.GetList("");
		}
                        
        /// <summary>
        /// 获得所有资源列表，无简介
        /// </summary>
        public DataSet GetSoftList(string Fhid)
        {
            string strWhere = " Fhid is null or Fhid=-1 or Fhid=" + Fhid;
            return dal.GetSoftList(strWhere);
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
		public List<LearnSite.Model.Soft> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Soft> DataTableToList(DataTable dt)
		{
            List<LearnSite.Model.Soft> modelList = new List<LearnSite.Model.Soft>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LearnSite.Model.Soft model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new LearnSite.Model.Soft();
                    if (dt.Rows[n]["Fid"].ToString() != "")
                    {
                        model.Fid = int.Parse(dt.Rows[n]["Fid"].ToString());
                    }
                    model.Ftitle = dt.Rows[n]["Ftitle"].ToString();
                    model.Fcontent = dt.Rows[n]["Fcontent"].ToString();
                    model.Furl = dt.Rows[n]["Furl"].ToString();
                    if (dt.Rows[n]["Fhit"].ToString() != "")
                    {
                        model.Fhit = int.Parse(dt.Rows[n]["Fhit"].ToString());
                    }
                    if (dt.Rows[n]["Fdate"].ToString() != "")
                    {
                        model.Fdate = DateTime.Parse(dt.Rows[n]["Fdate"].ToString());
                    }
                    model.Ffiletype = dt.Rows[n]["Ffiletype"].ToString();
                    model.Fclass = dt.Rows[n]["Fclass"].ToString();
                    if (dt.Rows[n]["Fhide"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Fhide"].ToString() == "1") || (dt.Rows[n]["Fhide"].ToString().ToLower() == "true"))
                        {
                            model.Fhide = true;
                        }
                        else
                        {
                            model.Fhide = false;
                        }
                    }
                    if (dt.Rows[n]["Fopen"].ToString() != "")
                    {
                        model.Fopen = int.Parse(dt.Rows[n]["Fopen"].ToString());
                    } 
                    if (dt.Rows[n]["Fhid"].ToString() != "")
                    {
                        model.Fhid = int.Parse(dt.Rows[n]["Fhid"].ToString());
                    }
                    if (dt.Rows[n]["Fyid"].ToString() != "")
                    {
                        model.Fyid = int.Parse(dt.Rows[n]["Fyid"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        /// <summary>
        /// 获取有提交作品的本分类资源列表
        /// </summary>
        /// <param name="Fyid"></param>
        /// <returns></returns>
        public DataTable GetListnomic(int Fyid)
        {
            return dal.GetListnomic(Fyid);
        }
        /// <summary>
        /// 获得有作品提交的分类项数据列表，按序号和编号排序
        /// </summary>
        public DataTable GetListCategory()
        {
            return dal.GetListCategory();
        }

        /// <summary>
        /// 获得所有资源列表，无简介
        /// </summary>
        public DataTable GetSoftList(string Fhid, string Fyid)
        {
            if (String.IsNullOrEmpty(Fhid) || String.IsNullOrEmpty(Fyid.ToString()))
                return null;
            string strWhere = " Fyid=" + Fyid + " and ( Fhid is null or Fhid=-1 or Fhid=" + Fhid + ") ";
            return dal.GetSoftList(strWhere).Tables[0];
        }
        /// <summary>
        /// 学生平台获得发布的资源列表，无简介
        /// </summary>
        public DataTable GetShowSoftList(string Fhid, int Fyid)
        {
            if (String.IsNullOrEmpty(Fhid) || String.IsNullOrEmpty(Fyid.ToString()))
                return null;
            string strWhere = " Fyid=" + Fyid + " and  (Fhide is null or Fhide=0) and (Fhid is null or Fhid=-1 or Fhid=" + Fhid + ")";
            return dal.GetSoftList(strWhere).Tables[0];
        }   
        /// <summary>
        /// 资源是否发布状态更新
        /// </summary>
        /// <param name="Fid"></param>
        public void UpdateFhide(int Fid)
        {
            dal.UpdateFhide(Fid);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}
        /// <summary>
        /// 返回资源上传的路径
        /// </summary>
        /// <returns></returns>
       public static string SoftUploadPath()
        {
            string thePath = "~/Download/";
            if (System.Web.Configuration.WebConfigurationManager.AppSettings["Download"] != null)
                thePath = System.Web.Configuration.WebConfigurationManager.AppSettings["Download"].ToString();
            if (!thePath.EndsWith("/"))
                thePath += "/";
           return thePath;
        }


       /// <summary>
       /// 是否可下载资源（是否限制下载时间）
       /// </summary>
       /// <returns></returns>
       public bool IsDownCan()
       {
           return dal.IsDownCan();
       }

       /// <summary>
       /// 更新点击率,点击数加1
       /// </summary>
       /// <param name="Fid"></param>
       /// <param name="Fhit"></param>
       public void UpdateFhit(int Fid)
       {
           dal.UpdateFhit(Fid);
       }                
        /// <summary>
        /// 是否存在该类别的资源
        /// </summary>
        /// <param name="Fyid"></param>
        /// <returns></returns>
       public bool ExistYid(int Fyid)
       {
           return dal.ExistYid(Fyid);
       }
		#endregion  成员方法
	}
}

