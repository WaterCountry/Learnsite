using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// ListMenu
	/// </summary>
	public partial class ListMenu
	{
		private readonly LearnSite.DAL.ListMenu dal=new LearnSite.DAL.ListMenu();
		public ListMenu()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.ListMenu model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.ListMenu model)
		{
			return dal.Update(model);
		}                
        /// <summary>
        /// 更改导航次序 updown 值0向上 减，1向下　增
        /// </summary>
        /// <param name="Lid"></param>
        /// <param name="updown"></param>
        public void UpdateLsort(int Lid, bool updown)
        {
            dal.UpdateLsort(Lid, updown);
        }  
                 
        /// <summary>
        /// 将活动的序号跟导航表的序号保持同步
        /// </summary>
        /// <param name="Lcid"></param>
        /// <param name="Lxid"></param>
        public void UpdateMissonListMene(int Lcid, int Lxid)
        {
            dal.UpdateMissonListMene(Lcid, Lxid);
        }
         /// <summary>
        /// 关联更新一条数据 
        /// 条件Lcid,Lxid,Ltype 更新Ltitle
        /// </summary>
        public bool UpdateLtitle(LearnSite.Model.ListMenu model)
        {
            return dal.UpdateLtitle(model);
        }                
        /// <summary>
        /// 关联更新一条数据 
        /// 条件Lid 更新Lshow,Ltitle,Ltype
        /// </summary>
        public bool UpdateMenuMission(LearnSite.Model.ListMenu model)
        {
            return dal.UpdateMenuMission(model);
        }
        /// <summary>
        /// 关联更新一条数据 
        /// 条件Lcid,Lxid,Ltype 更新Lshow,Ltitle
        /// </summary>
        public bool UpdateMenuThree(LearnSite.Model.ListMenu model)
        {
            return dal.UpdateMenuThree(model);
        }
        /// <summary>
        /// 条件联动更新一条部分数据
        ///Lid,Lshow,Ltitle
        /// </summary>
        public bool UpdateMenu(LearnSite.Model.ListMenu model)
        {
           return dal.UpdateMenu(model);
        }
        /// <summary>
        /// 获取序号最大值
        /// </summary>
        /// <param name="Mcid"></param>
        /// <returns></returns>
        public int GetMaxLsort(int Lcid)
        {
            return dal.GetMaxLsort(Lcid);
        }
        /// <summary>
        /// 初始化序号
        /// </summary>
        /// <param name="Lcid"></param>
        public void Lsortnew(int Lcid)
        {
            dal.Lsortnew(Lcid);
        }
        public void UpdateLshow(int Lcid, int Lxid)
        {
            dal.UpdateLshow(Lcid, Lxid);
        }
        public void UpdateLshow(int Lid)
        {
            dal.UpdateLshow(Lid);
        }
        public void OpenLshow(int Lid)
        {
            dal.OpenLshow(Lid);
        }
        public void CloseLshow(int Lid)
        {
            dal.CloseLshow(Lid);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Lid)
		{
			
			return dal.Delete(Lid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Lidlist )
		{
			return dal.DeleteList(Lidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.ListMenu GetModel(int Lid)
		{
			
			return dal.GetModel(Lid);
		}
                
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.ListMenu GetModel(DataTable dt, int Tsort)
        {
            return dal.GetModel(dt, Tsort);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.ListMenu GetModelByCache(int Lid)
		{
			
			string CacheKey = "ListMenuModel-" + Lid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Lid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.ListMenu)objModel;
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
		public List<LearnSite.Model.ListMenu> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.ListMenu> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.ListMenu> modelList = new List<LearnSite.Model.ListMenu>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.ListMenu model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.ListMenu();
					if(dt.Rows[n]["Lid"]!=null && dt.Rows[n]["Lid"].ToString()!="")
					{
						model.Lid=int.Parse(dt.Rows[n]["Lid"].ToString());
					}
					if(dt.Rows[n]["Lcid"]!=null && dt.Rows[n]["Lcid"].ToString()!="")
					{
						model.Lcid=int.Parse(dt.Rows[n]["Lcid"].ToString());
					}
					if(dt.Rows[n]["Lsort"]!=null && dt.Rows[n]["Lsort"].ToString()!="")
					{
						model.Lsort=int.Parse(dt.Rows[n]["Lsort"].ToString());
					}
					if(dt.Rows[n]["Ltype"]!=null && dt.Rows[n]["Ltype"].ToString()!="")
					{
						model.Ltype=int.Parse(dt.Rows[n]["Ltype"].ToString());
					}
					if(dt.Rows[n]["Lxid"]!=null && dt.Rows[n]["Lxid"].ToString()!="")
					{
						model.Lxid=int.Parse(dt.Rows[n]["Lxid"].ToString());
					}
					if(dt.Rows[n]["Lshow"]!=null && dt.Rows[n]["Lshow"].ToString()!="")
					{
						if((dt.Rows[n]["Lshow"].ToString()=="1")||(dt.Rows[n]["Lshow"].ToString().ToLower()=="true"))
						{
						model.Lshow=true;
						}
						else
						{
							model.Lshow=false;
						}
					}
					if(dt.Rows[n]["Ltitle"]!=null && dt.Rows[n]["Ltitle"].ToString()!="")
					{
					model.Ltitle=dt.Rows[n]["Ltitle"].ToString();
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
        /// 将旧学案生成新导航
        /// </summary>
        public void initbuildmenu()
        {
            dal.initbuildmenu();
        }
        /// <summary>
        /// 将导入学案生成新导航
        /// </summary>
        public void importmenu(int Cid)
        {
            dal.importmenu(Cid);
        }
                
        /// <summary>
        /// 将导入学案生成的新导航的序号跟旧导航序号一致
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Cid"></param>
        public void importupsort(DataTable dt, int Cid)
        {
            dal.importupsort(dt, Cid);
        }
        /// <summary>
        ///  返回该学案的导航列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataSet GetMenu(int cid)
        {
            return dal.GetMenu(cid);
        }
                
        /// <summary>
        /// 返回该学案的显示的导航列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataSet GetShowedMenu(int cid)
        {
            return dal.GetShowedMenu(cid);
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

