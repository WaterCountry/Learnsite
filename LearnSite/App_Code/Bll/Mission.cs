using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Mission 的摘要说明。
	/// </summary>
	public class Mission
	{
		private readonly LearnSite.DAL.Mission dal=new LearnSite.DAL.Mission();
		public Mission()
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
        /// 获取活动序号最大值
        /// </summary>
        /// <param name="Mcid"></param>
        /// <returns></returns>
        public int GetMaxMsort(int Mcid)
        {
            return dal.GetMaxMsort(Mcid);
        }
                
        /// <summary>
        /// 获取比当前活动序号小的最大可提交活动序号，无则返回0
        /// </summary>
        /// <param name="Mcid"></param>
        /// <returns></returns>
        public int GetLastMaxMsort(int Mcid,int Msort)
        {
            return dal.GetLastMaxMsort(Mcid,Msort);
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Mid)
		{
			return dal.Exists(Mid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Mission model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据(无mcid)
		/// </summary>
		public void Update(LearnSite.Model.Mission model)
		{
			dal.Update(model);
		}
                
        /// <summary>
        /// 更改导航次序 updown 值0向上 减，1向下　增
        /// </summary>
        /// <param name="Mid"></param>
        /// <param name="updown"></param>
        public void UpdateMsort(int Mid, bool updown)
        {
            dal.UpdateMsort(Mid, updown);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Mid)
		{
			
			dal.Delete(Mid);
		}
                
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteMission(int Mid)
        {
            dal.DeleteMission(Mid);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Mission GetModel(int Mid)
		{
			
			return dal.GetModel(Mid);
		}

                /// <summary>
        /// 从查询表中得到一个Mission对象实体,Tsort为Table记录序号（从0开始）
        /// </summary>
        public LearnSite.Model.Mission GetTableModel(DataTable dt, int Tsort)
        {
            return dal.GetTableModel(dt, Tsort);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Mission GetModelByCache(int Mid)
		{
			
			string CacheKey = "MissionModel-" + Mid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Mid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Mission)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// 获得无内容任务列表
        /// </summary>
        public DataSet GetListNoContent(string strWhere)
        {
            return dal.GetListNoContent(strWhere);
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
		public List<LearnSite.Model.Mission> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Mission> DataTableToList(DataTable dt)
		{
            List<LearnSite.Model.Mission> modelList = new List<LearnSite.Model.Mission>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LearnSite.Model.Mission model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new LearnSite.Model.Mission();
                    if (dt.Rows[n]["Mid"].ToString() != "")
                    {
                        model.Mid = int.Parse(dt.Rows[n]["Mid"].ToString());
                    }
                    model.Mtitle = dt.Rows[n]["Mtitle"].ToString();
                    if (dt.Rows[n]["Mcid"].ToString() != "")
                    {
                        model.Mcid = int.Parse(dt.Rows[n]["Mcid"].ToString());
                    }
                    model.Mcontent = dt.Rows[n]["Mcontent"].ToString();
                    if (dt.Rows[n]["Mdate"].ToString() != "")
                    {
                        model.Mdate = DateTime.Parse(dt.Rows[n]["Mdate"].ToString());
                    }
                    if (dt.Rows[n]["Mhit"].ToString() != "")
                    {
                        model.Mhit = int.Parse(dt.Rows[n]["Mhit"].ToString());
                    }
                    model.Mfiletype = dt.Rows[n]["Mfiletype"].ToString();
                    if (dt.Rows[n]["Mupload"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Mupload"].ToString() == "1") || (dt.Rows[n]["Mupload"].ToString().ToLower() == "true"))
                        {
                            model.Mupload = true;
                        }
                        else
                        {
                            model.Mupload = false;
                        }
                    }
                    if (dt.Rows[n]["Msort"].ToString() != "")
                    {
                        model.Msort = int.Parse(dt.Rows[n]["Msort"].ToString());
                    }
                    if (dt.Rows[n]["Mpublish"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Mpublish"].ToString() == "1") || (dt.Rows[n]["Mpublish"].ToString().ToLower() == "true"))
                        {
                            model.Mpublish = true;
                        }
                        else
                        {
                            model.Mpublish = false;
                        }
                    }
                    if (dt.Rows[n]["Mgroup"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Mgroup"].ToString() == "1") || (dt.Rows[n]["Mgroup"].ToString().ToLower() == "true"))
                        {
                            model.Mgroup = true;
                        }
                        else
                        {
                            model.Mgroup = false;
                        }
                    }
                    if (dt.Rows[n]["Mgid"] != null && dt.Rows[n]["Mgid"].ToString() != "")
                    {
                        model.Mgid = int.Parse(dt.Rows[n]["Mgid"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }                
        /// <summary>
        /// 获取活动作品上传类型
        /// </summary>
        /// <param name="Mid"></param>
        /// <returns></returns>
        public string GetMfiletype(int Mid)
        {
            return dal.GetMfiletype(Mid);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}
        /// <summary>
        /// 获得指定学案的已发布的任务列表,无内容
        /// </summary>
        /// <param name="Mcid"></param>
        /// <returns></returns>
        public DataSet GetMission(int Mcid)
        {
            string strWhere = "Mcid="+Mcid+" and Mpublish=1  order by Msort";
            return GetListNoContent(strWhere);
        }

        /// <summary>
        /// 获得指定学案的所有任务列表,无内容
        /// </summary>
        /// <param name="Mcid"></param>
        /// <returns></returns>
        public DataSet GetMissions(int Mcid)
        {
            string strWhere = "Mcid=" + Mcid + " and Mdelete<>1  order by Msort";
            return GetListNoContent(strWhere);
        }
                
        /// <summary>
        /// 获得无内容任务列表（有标题）
        /// </summary>
        public DataSet GetListMission(int Mcid)
        {
            return dal.GetListMission(Mcid);
        }
                
        /// <summary>
        /// 获取标题
        /// </summary>
        /// <param name="Mid"></param>
        /// <returns></returns>
        public string GetMissionTitle(int Mid)
        {
            return dal.GetMissionTitle(Mid);
        }
                
        /// <summary>
        /// 获取Mgid
        /// </summary>
        /// <param name="Mid"></param>
        /// <returns></returns>
        public string GetMissionGid(int Mid)
        {
            return dal.GetMissionGid(Mid);
        }
        /// <summary>
        /// 获得指定mid的一条任务记录
        /// </summary>
        /// <param name="Mid"></param>
        /// <returns></returns>
        public DataSet GetMissionDetail(int Mid)
        {
            string strWhere = "Mid=" + Mid;
            return GetList(strWhere);
        }

        /// <summary>
        /// 获得指定Mcid的所有任务记录    （多条详细活动记录）
        /// </summary>
        /// <param name="Mid"></param>
        /// <returns></returns>
        public DataSet GetMissionDetails(int Mcid)
        {
            string strWhere = "Mcid=" + Mcid+" and Mdelete=0 order by Msort asc";
            return GetList(strWhere);
        }
        /// <summary>
        /// 是否存在该活动记录
        /// </summary>
        public bool MsortExists(int Mcid, int Msort)
        {
            return dal.MsortExists(Mcid, Msort);
        }
        /// <summary>
        /// 获得该学案的活动次序
        /// </summary>
        /// <param name="Mcid"></param>
        /// <returns></returns>
        public DataSet GetMsort(int Mcid)
        {
            return dal.GetMsort(Mcid);
        }
                
        /// <summary>
        /// 获得该学案有作品提交的活动序号
        /// </summary>
        /// <param name="Mcid"></param>
        /// <returns></returns>
        public DataSet GetUploadMsort(int Mcid)
        {
            return dal.GetUploadMsort(Mcid);
        }
                
        /// <summary>
        /// 获得该学案有作品提交的编号和标题
        /// </summary>
        /// <param name="Mcid"></param>
        /// <returns></returns>
        public DataSet GetUploadMidMtitle(int Mcid)
        {
            return dal.GetUploadMidMtitle(Mcid);
        }                
        /// <summary>
        /// 获得该学案所有活动编号和标题
        /// </summary>
        /// <param name="Mcid"></param>
        /// <returns></returns>
        public DataSet GetMidMtitle(int Mcid)
        {
            return dal.GetMidMtitle(Mcid);
        }
        /// <summary>
        /// 初始化Mgroup字段null为false
        /// </summary>
        public void InitMgroup()
        {
            dal.InitMgroup();
        }
                        
        /// <summary>
        /// 任务活动状态：发布或回收
        /// </summary>
        /// <param name="Mid"></param>
        public void UpdateMpublish(int Mid)
        {
            dal.UpdateMpublish(Mid);
        }
                       
        /// <summary>
        /// 初始化，数据库更新用
        /// </summary>
        public void UpdateMgid()
        {
            dal.UpdateMgid();
        }
        public void InitMdelete()
        {
            dal.InitMdelete();
        }
        //初始化字段值为0
        public void InitMcategory()
        {
            dal.InitMcategory();
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

