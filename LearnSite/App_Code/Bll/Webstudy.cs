using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Webstudy 的摘要说明。
	/// </summary>
	public class Webstudy
	{
		private readonly LearnSite.DAL.Webstudy dal=new LearnSite.DAL.Webstudy();
		public Webstudy()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Wid)
		{
			return dal.Exists(Wid);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsWnum(string Wnum)
        {
            return dal.ExistsWnum(Wnum);
        }
                
        /// <summary>
        /// 增加一条数据模拟学生账号
        /// </summary>
        public int AddSimulation(string Wnum, string Wpwd, string Wurl)
        {
            return dal.AddSimulation(Wnum, Wpwd, Wurl);
        }
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wpwd"></param>
        public void AddOne(string Wnum, string Wpwd)
        {
            dal.AddOne(Wnum, Wpwd);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Webstudy model)
		{
			return dal.Add(model);
		}

                /// <summary>
        /// 根据网站编号，给该网站加分
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wscore"></param>
        public void UpdateOne(int Wid, int Wscore)
        {
            dal.UpdateOne(Wid, Wscore);
        }
        /// <summary>
        /// 更新某个学号的ftp密码跟学号密码一致
        /// </summary>
        /// <param name="Wnum"></param>
        public void UpdateWpwd(string Wnum)
        {
            dal.UpdateWpwd(Wnum);
        }
        /// <summary>
        /// 更新某个学号的ftp登录密码
        /// </summary>
        /// <param name="Wnum"></param>
        public void UpdateWpwd2(string Wnum, string Wpwd)
        {
            dal.UpdateWpwd2(Wnum, Wpwd);
        }                
        /// <summary>
        /// 更新網站評價狀態，取反
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWcheck(string Wid)
        {
            dal.UpdateWcheck(Wid);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Webstudy model)
		{
			dal.Update(model);
		}

                /// <summary>
        /// 将该班级所有未评的网站全评为P，即6分
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        public void UpdateOneClass(int Sgrade, int Sclass)
        {
            dal.UpdateOneClass(Sgrade, Sclass);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Wid)
		{
			
			dal.Delete(Wid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Webstudy GetModel(int Wid)
		{
			
			return dal.GetModel(Wid);
		}

                /// <summary>
        /// 根据学号，得到一个对象实体
        /// </summary>
        public LearnSite.Model.Webstudy GetModelByWnum(string Wnum)
        {
            return dal.GetModelByWnum(Wnum);
        }

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Webstudy GetModelByCache(int Wid)
		{
			
			string CacheKey = "WebstudyModel-" + Wid;
			object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Wid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Webstudy)objModel;
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
		public List<LearnSite.Model.Webstudy> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Webstudy> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Webstudy> modelList = new List<LearnSite.Model.Webstudy>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Webstudy model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.Webstudy();
					if(dt.Rows[n]["Wid"].ToString()!="")
					{
						model.Wid=int.Parse(dt.Rows[n]["Wid"].ToString());
					}
					model.Wnum=dt.Rows[n]["Wnum"].ToString();
					model.Wpwd=dt.Rows[n]["Wpwd"].ToString();
					if(dt.Rows[n]["Wvote"].ToString()!="")
					{
						model.Wvote=int.Parse(dt.Rows[n]["Wvote"].ToString());
					}
					if(dt.Rows[n]["Wegg"].ToString()!="")
					{
						model.Wegg=int.Parse(dt.Rows[n]["Wegg"].ToString());
					}
					if(dt.Rows[n]["Wscore"].ToString()!="")
					{
						model.Wscore=int.Parse(dt.Rows[n]["Wscore"].ToString());
					}
					if(dt.Rows[n]["Wcheck"].ToString()!="")
					{
						if((dt.Rows[n]["Wcheck"].ToString()=="1")||(dt.Rows[n]["Wcheck"].ToString().ToLower()=="true"))
						{
						model.Wcheck=true;
						}
						else
						{
							model.Wcheck=false;
						}
					}
					if(dt.Rows[n]["WquotaCurrent"].ToString()!="")
					{
						model.WquotaCurrent=int.Parse(dt.Rows[n]["WquotaCurrent"].ToString());
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
        ///从学生表读取Webstudy表中不存在的数据，插入Webstudy表中
        /// </summary>
        public void AddAll()
        {
            dal.AddAll();
        }

                /// <summary>
        /// 获得条件数据列表
        /// </summary>
        public DataSet GetListWeb(int Sgrade, int Sclass)
        {
            return dal.GetListWeb(Sgrade, Sclass);
        }

                /// <summary>
        /// 重置所上课班级网站投票次数
        /// </summary>
        public void ResetWegg(int eggs)
        {
            dal.ResetWegg(eggs);
        }

        /// <summary>
        /// 学年升级，删除Webstudy中学号不在Students的记录
        /// </summary>
        public void Upgrade()
        {
            dal.Upgrade();
        }


        /// <summary>
        /// 给网站投票加1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWvote(int Wid)
        {
            dal.UpdateWvote(Wid);
        }
        /// <summary>
        /// 给网站蛋数减1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateMyWegg(string Wnum)
        {
            dal.UpdateMyWegg(Wnum);
        }
        /// <summary>
        /// 获取网站投票数
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public int GetMyWegg(string Wnum)
        {
            return dal.GetMyWegg(Wnum);
        }
        /// <summary>
        /// 获得本班级网站列表，返回Sname,Snum,Wid,Wvote,Wscore,Wupdate
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet GetAllSite(int Sgrade, int Sclass, string Mynum)
        {
            return dal.GetAllSite(Sgrade, Sclass,Mynum);
        }
        /// <summary>
        /// 查询WebFtp用户表密码
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string FindWebFtpPwd(string Wnum)
        {
            return dal.FindWebFtpPwd(Wnum);
        }
        public void WebSiteUpdateCheck(string htmlname)
        {
            dal.WebSiteUpdateCheck(htmlname);
        }
        /// <summary>
        /// 更新网站更新日期空间占用大小
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="updatetime"></param>
        public void UpdateWebTimeSize(string Wnum, DateTime Wupdate, int WquotaCurrent)
        {
            dal.UpdateWebTimeSize(Wnum, Wupdate, WquotaCurrent);
        }
        /// <summary>
        /// 更新网站更新日期
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="updatetime"></param>
        public void UpdateWebTime(string Wnum, string updatetime)
        {
            dal.UpdateWebTime(Wnum, updatetime);
        }
                
        /// <summary>
        ///  更新网站空间占用大小
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="WquotaCurrent"></param>
        public void UpdateWebSize(string Wnum, int WquotaCurrent)
        {
            dal.UpdateWebSize(Wnum, WquotaCurrent);
        }
               
        /// <summary>
        /// 将无网站制作内容的得票清零
        /// </summary>
        public void ClearNoSiteVote()
        {
            dal.ClearNoSiteVote();
        }
        /// <summary>
        /// 显示指定数量的网站得票排行列表
        /// </summary>
        /// <param name="TopNum"></param>
        /// <returns></returns>
        public DataSet WebTopShow(int TopNum)
        {
           return dal.WebTopShow(TopNum);
        }
                
        /// <summary>
        /// 重新生成Webstudy表中Wurl
        /// </summary>
        public void WebUpdateWurl(int Hid)
        {
            dal.WebUpdateWurl(Hid);
        }                
        /// <summary>
        /// 清除该班级的webstudy表中的记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        public void DelWebClass(int Sgrade, int Sclass)
        {
            dal.DelWebClass(Sgrade, Sclass);
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

