using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// TopicReply
	/// </summary>
	public class TopicReply
	{
		private readonly LearnSite.DAL.TopicReply dal=new LearnSite.DAL.TopicReply();
		public TopicReply()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists()
		{
			return dal.Exists();
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.TopicReply model)
		{
			return dal.Add(model);
		}
        /// <summary>
        /// 给回复评分
        /// </summary>
        /// <param name="Rid"></param>
        public bool Lessscore(int Rid)
        {
            return dal.Lessscore(Rid);

        }
        /// <summary>
        /// 给回复评分
        /// </summary>
        /// <param name="Rid"></param>
        public bool Updatescore(int Rid)
        {
            return dal.Updatescore(Rid);
        }
        public bool InitReditagree()
        {
            return dal.InitReditagree();
        }
        /// <summary>
        /// 给本班所有回复评分+2
        /// </summary>
        /// <param name="Rid"></param>
        public int UpdateAllscore(int Rtid, int Rgrade, int Rclass, int Ryear)
        {
            return dal.UpdateAllscore(Rtid, Rgrade, Rclass, Ryear);
        }
        /// <summary>
        /// 给该贴子加禁言标记
        /// </summary>
        /// <param name="Rid"></param>
        public bool UpdateBan(int Rid)
        {
            return dal.UpdateBan(Rid);
        }
        /// <summary>
        /// 给该贴子允许修改
        /// </summary>
        /// <param name="Rid"></param>
        public bool UpdateEdit(int Rid)
        {
            return dal.UpdateEdit(Rid);
        }  
        /// <summary>
        /// 给该贴子点赞
        /// </summary>
        /// <param name="Rid"></param>
        /// <returns></returns>
        public bool UpdateAgree(int Rid)
        {
            return dal.UpdateAgree(Rid);
        }
        /// <summary>
        /// 判断该学号本题回复是否被禁言过
        /// </summary>
        /// <param name="Rtid"></param>
        /// <param name="Rsnum"></param>
        /// <returns></returns>
        public bool Isban(int Rtid, int Rsid)
        {
            return dal.Isban(Rtid, Rsid);
        }     
         /// <summary>
        /// 判断该学号本题回复是否允许回复修改
        /// </summary>
        /// <param name="Rtid"></param>
        /// <param name="Rsid"></param>
        /// <returns></returns>
        public bool Isedit(int Rtid, int Rsid)
        {
            return dal.Isedit(Rtid, Rsid);
        }
        /// <summary>
        /// 判断该学号本题回复次数
        /// </summary>
        /// <param name="Rtid"></param>
        /// <param name="Rsnum"></param>
        /// <returns></returns>
        public int ReplyCount(int Rtid, int Rsid)
        {
            return dal.ReplyCount(Rtid, Rsid);
        }
                
        /// <summary>
        /// 更新老师总结
        /// </summary>
        /// <param name="Rtid"></param>
        /// <param name="Rnum"></param>
        /// <param name="Rwords"></param>
        /// <returns></returns>
        public bool UpdateTeacher(int Rtid, int Rsid, string Rwords)
        {
            return dal.UpdateTeacher(Rtid, Rsid, Rwords);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.TopicReply model)
		{
			return dal.Update(model);
		}
        /// <summary>
        ///Rid 更新一条数据Rwords Rtime Redit Ragree
        /// </summary>
        public bool UpdateOne(LearnSite.Model.TopicReply model)
        {
            return dal.UpdateOne(model);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Rid)
		{
			
			return dal.Delete(Rid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Ridlist )
		{
			return dal.DeleteList(Ridlist );
		}
                
        /// <summary>
        /// 删除一个班级的讨论记录
        /// </summary>
        public int DelClass(int Rgrade, int Rclass, int Ryear)
        {
            return dal.DelClass(Rgrade, Rclass, Ryear);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.TopicReply GetModel(int Rid)
		{
			
			return dal.GetModel(Rid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.TopicReply GetModelByCache(int Rid)
		{
			
			string CacheKey = "TopicReplyModel-" + Rid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Rid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.TopicReply)objModel;
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
		public List<LearnSite.Model.TopicReply> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.TopicReply> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.TopicReply> modelList = new List<LearnSite.Model.TopicReply>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.TopicReply model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.TopicReply();
					if(dt.Rows[n]["Rid"].ToString()!="")
					{
						model.Rid=int.Parse(dt.Rows[n]["Rid"].ToString());
					}
					if(dt.Rows[n]["Rtid"].ToString()!="")
					{
						model.Rtid=int.Parse(dt.Rows[n]["Rtid"].ToString());
					}
					model.Rsnum=dt.Rows[n]["Rsnum"].ToString();
					model.Rwords=dt.Rows[n]["Rwords"].ToString();
					if(dt.Rows[n]["Rtime"].ToString()!="")
					{
						model.Rtime=DateTime.Parse(dt.Rows[n]["Rtime"].ToString());
					}
					model.Rip=dt.Rows[n]["Rip"].ToString();
					if(dt.Rows[n]["Rscore"].ToString()!="")
					{
						model.Rscore=int.Parse(dt.Rows[n]["Rscore"].ToString());
					}
					if(dt.Rows[n]["Rban"].ToString()!="")
					{
						if((dt.Rows[n]["Rban"].ToString()=="1")||(dt.Rows[n]["Rban"].ToString().ToLower()=="true"))
						{
						model.Rban=true;
						}
						else
						{
							model.Rban=false;
						}
					}
                    if (dt.Rows[n]["Rgrade"].ToString() != "")
                    {
                        model.Rgrade = int.Parse(dt.Rows[n]["Rgrade"].ToString());
                    }
                    if (dt.Rows[n]["Rterm"].ToString() != "")
                    {
                        model.Rterm = int.Parse(dt.Rows[n]["Rterm"].ToString());
                    }
                    if (dt.Rows[n]["Rcid"].ToString() != "")
                    {
                        model.Rcid = int.Parse(dt.Rows[n]["Rcid"].ToString());
                    }
                    if (dt.Rows[n]["Rclass"].ToString() != "")
                    {
                        model.Rclass = int.Parse(dt.Rows[n]["Rclass"].ToString());
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
        /// 获得该主题本班回复数据列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Rtid"></param>
        /// <returns></returns>
        public DataSet GetClassList(int Sgrade, int Sclass, int Rtid)
        {
            return dal.GetClassList(Sgrade, Sclass, Rtid);
        }
         /// <summary>
        /// 获取本班未回复的同学姓名列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Rtid"></param>
        /// <returns></returns>
        public string GetNoReplay(int Sgrade, int Sclass, int Rtid)
        {
            string stus = "未发表同学：";
             DataTable dt=dal.GetNoReplay(Sgrade, Sclass, Rtid);
            int count=dt.Rows.Count;
             if ( count> 0)
             {
                 for (int i = 0; i < count; i++)
                 {
                     stus = stus + dt.Rows[i][0].ToString();
                     if (i < count - 1)
                         stus += "，";
                 }             
             }
             return stus;
        }
        /// <summary>
        /// 获得该主题本班回复数据列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Rtid"></param>
        /// <returns></returns>
        public DataTable GetClassListScore(int Sgrade, int Sclass, int Rtid)
        {
            return dal.GetClassListScore(Sgrade, Sclass, Rtid);
        }
        /// <summary>
        /// 获取教师模拟学生回复作为总结
        /// </summary>
        /// <param name="Rtid"></param>
        /// <param name="Rsnum"></param>
        /// <returns></returns>
        public string getteareply(int Rtid, string Rsnum)
        {
            return dal.getteareply(Rtid, Rsnum);
        }
        /// <summary>
        /// 初始化新增Rcid字段
        /// </summary>
        public void InitRcid()
        {
            dal.InitRcid();
        }
        /// <summary>
        /// 初始化新增Rclass字段
        /// </summary>
        public void InitRclass()
        {
            dal.InitRclass();
        }
                
        /// <summary>
        /// 获取当前班级学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowDoneReplyCids(int Rgrade, int Rclass, int Rterm, int Ryear)
        {
            return dal.ShowDoneReplyCids(Rgrade, Rclass, Rterm, Ryear);
        }
        /// <summary>
        /// 获取某学生学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowStuDoneReplyCids(string Snum, int Rterm, int Rgrade)
        {
            return dal.ShowStuDoneReplyCids(Snum,Rterm,Rgrade);
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

