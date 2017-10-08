using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
using System.Web;
using System.IO;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Works 的摘要说明。
	/// </summary>
	public class Works
	{
		private readonly LearnSite.DAL.Works dal=new LearnSite.DAL.Works();
		public Works()
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
		public bool Exists(int Wid)
		{
			return dal.Exists(Wid);
		}
                 /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsWcid(int Wcid)
        {
            return dal.ExistsWcid(Wcid);
        }
        /// <summary>
        /// 是否存在该学号任务作品
        /// </summary>
        public bool ExistsMyMissonWork(int Wmid, string Wnum)
        {
            return dal.ExistsMyMissonWork(Wmid, Wnum);
        }
               
        /// <summary>
        /// 是否存在该学号上一个任务作品,Wmsort为上一个可提交任务序号
        /// </summary>
        public bool ExistsMyFirstWork(int Wcid, string Wnum, int Wmsort)
        {
            return dal.ExistsMyFirstWork(Wcid, Wnum, Wmsort);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Works model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Works model)
		{
			dal.Update(model);
		}
        /// <summary>
        /// 初始化新添加字段Woffice
        /// </summary>
        /// <returns></returns>
        public int UpdateWoffice()
        {
            return dal.UpdateWoffice();
        }  
                
        /// <summary>
        /// 初始化新添加字段Wfscore 互评的平均分
        /// </summary>
        /// <returns></returns>
        public void InitWfscore()
        {
            dal.InitWfscore();
        }
                
        /// <summary>
        /// 更新字段Wfscore
        /// </summary>
        /// <returns></returns>
        public void UpdateWfscore(int Wid, int Wfscore)
        {
            dal.UpdateWfscore(Wid, Wfscore);
        }
                
        /// <summary>
        /// 获取Wfscore
        /// </summary>
        /// <returns></returns>
        public int GetWfscore(int Wid)
        {
            return dal.GetWfscore(Wid);
        }
        /// <summary>
        /// 清除该班级该活动作品的异常转换标志
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wid"></param>
        /// <param name="Wcid"></param>
        public void ClearWflasherror(int Sgrade, int Sclass, int Wmid, int Wcid)
        {
            dal.ClearWflasherror(Sgrade, Sclass, Wmid, Wcid);
        }
        /// <summary>
        /// 更新一条数据,给一个作品评价(参数传送 Wid,Wscore, Wcheck)
        /// </summary>
        public int ScoreOneWork(LearnSite.Model.Works model)
        {
           return dal.ScoreOneWork(model);
        }

        /// <summary>
        /// 更新指定Wid作品的积分,不用数据类型
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wscore"></param>
        public void ScoreWork(int Wid, int Wscore)
        {
            dal.ScoreWork(Wid, Wscore);
        }
                
        /// <summary>
        /// 获取本班本活动学分列表
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wgrade"></param>
        /// <param name="Wclass"></param>
        /// <returns></returns>
        public DataTable getScoreList(int Wmid, int Wgrade, int Wclass)
        {
            return dal.getScoreList(Wmid, Wgrade, Wclass);
        }
        /// <summary>
        /// 设置指定Wid作品的评价状态和积分为零
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wcheck"></param>
        public void CancleScoreWork(int Wid, bool Wcheck)
        {
            dal.CancleScoreWork(Wid, Wcheck);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Wid)
		{
			
			dal.Delete(Wid);
		}
                
        /// <summary>
        /// 删除一个班级的作业记录
        /// </summary>
        public int DelClass(int Wgrade, int Wclass, int Wyear)
        {
            return dal.DelClass(Wgrade, Wclass, Wyear);
        }
        /// <summary>
        /// 清除几年前的未推荐的作品记录
        /// </summary>
        /// <param name="Wyear"></param>
        public int DeleteOldyear(int Wyear)
        {
            return dal.DeleteOldyear(Wyear);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Works GetModel(int Wid)
		{
			
			return dal.GetModel(Wid);
		}
                
        /// <summary>
        /// 学生和活动编号得到一个对象实体
        /// </summary>
        public LearnSite.Model.Works GetModelByStu(int Mid, string Snum)
        {
            return dal.GetModelByStu(Mid, Snum);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Works GetModelByCache(int Wid)
		{
			
			string CacheKey = "WorksModel-" + Wid;
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
			return (LearnSite.Model.Works)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        public string GetWid(string Wmid, string Wnum)
        {
            return dal.GetWid(Wmid, Wnum);
        }
        /// <summary>
        /// 根据学生表的年级、班级(不影响班级升学)
        /// 多表联合查询作品，返回dataset
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wcheck"></param>
        /// <returns></returns>
        public DataSet GetListWcheckWork(int Wcid, int Sgrade, int Sclass, int Wmid, bool Wcheck,string sort)
        {
            return dal.GetListWcheckWork(Wcid, Sgrade, Sclass, Wmid, Wcheck, sort);
        }                
        /// <summary>
        /// 显示所教班级该学案所有未评作品
        /// select Wid,Wnum,Wmid,Wmsort,Wurl,Wtype,Wscore,Wtime,Wvote,Wcheck,Wself,Wcan,Wgood,Sname,Sgrade,Sclass,Mtitle
        /// </summary>
        /// <param name="Wcid"></param>
        /// <returns></returns>
        public DataTable GetListNoWcheckWork(int Wcid, int Wclass)
        {
            return dal.GetListNoWcheckWork(Wcid, Wclass);
        }
         /// <summary>
        /// 显示所教班级该学案所有未评班级
        /// select Sclass
        /// </summary>
        /// <param name="Wcid"></param>
        /// <returns></returns>
        public DataTable GetListNoWcheckClass(int Wcid)
        {
            return dal.GetListNoWcheckClass(Wcid);
        }
        /// <summary>
        /// 显示所教某班级该学案所有未评作品
        /// select Wid,Wnum,Wmid,Wmsort,Wurl,Wtype,Wscore,Wtime,Wvote,Wcheck,Wself,Wcan,Wgood,Sname,Sgrade,Sclass,Mtitle
        /// </summary>
        /// <param name="Wcid"></param>
        /// <returns></returns>
        public DataTable GetListNoWcheckWork(int Wcid, int Wgrade, int Wclass)
        {
            return dal.GetListNoWcheckWork(Wcid, Wgrade, Wclass);
        } 
        /// <summary>
        /// 显示所教学案所有未评作品的班级
        /// Wgrade,Wclass
        /// </summary>
        /// <param name="Wcid"></param>
        /// <returns></returns>
        public DataTable GetListNoWcheckWclass(int Wcid)
        {
            return dal.GetListNoWcheckWclass(Wcid);
        }
        /// <summary>
        /// 根据学生生的年级、班级(不影响班级升学)
        /// 设置该班本学案未评价的活动全部积分为10
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        public void WorkSetA(int Wcid, int Sgrade, int Sclass, int Wmsort)
        {
            dal.WorkSetA(Wcid, Sgrade, Sclass, Wmsort);
        }
        /// <summary>
        /// 设置该班本学案未评价的活动全部积分为6
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmsort"></param>
        public void WorkSetP(int Wcid, int Sgrade, int Sclass, int Wmsort)
        {
            dal.WorkSetP(Wcid, Sgrade, Sclass, Wmsort);
        }
                /// <summary>
        /// 根据学生生的年级、班级(不影响班级升学)
        /// 设置该班本学案未评价的活动全部积分为Wscore
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wscore"></param>
        public void WorkSetScore(int Wcid, int Sgrade, int Sclass, int Wmid, int Wscore)
        {
            dal.WorkSetScore(Wcid, Sgrade, Sclass, Wmid, Wscore);
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
		public List<LearnSite.Model.Works> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<LearnSite.Model.Works> DataTableToList(DataTable dt)
        {
            List<LearnSite.Model.Works> modelList = new List<LearnSite.Model.Works>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LearnSite.Model.Works model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new LearnSite.Model.Works();
                    if (dt.Rows[n]["Wid"].ToString() != "")
                    {
                        model.Wid = int.Parse(dt.Rows[n]["Wid"].ToString());
                    }
                    model.Wnum = dt.Rows[n]["Wnum"].ToString();
                    if (dt.Rows[n]["Wcid"].ToString() != "")
                    {
                        model.Wcid = int.Parse(dt.Rows[n]["Wcid"].ToString());
                    }
                    if (dt.Rows[n]["Wmid"].ToString() != "")
                    {
                        model.Wmid = int.Parse(dt.Rows[n]["Wmid"].ToString());
                    }
                    if (dt.Rows[n]["Wmsort"].ToString() != "")
                    {
                        model.Wmsort = int.Parse(dt.Rows[n]["Wmsort"].ToString());
                    }
                    model.Wfilename = dt.Rows[n]["Wfilename"].ToString();
                    model.Wurl = dt.Rows[n]["Wurl"].ToString();
                    if (dt.Rows[n]["Wlength"].ToString() != "")
                    {
                        model.Wlength = int.Parse(dt.Rows[n]["Wlength"].ToString());
                    }
                    if (dt.Rows[n]["Wscore"].ToString() != "")
                    {
                        model.Wscore = int.Parse(dt.Rows[n]["Wscore"].ToString());
                    }
                    if (dt.Rows[n]["Wdate"].ToString() != "")
                    {
                        model.Wdate = DateTime.Parse(dt.Rows[n]["Wdate"].ToString());
                    }
                    model.Wip = dt.Rows[n]["Wip"].ToString();
                    model.Wtime = dt.Rows[n]["Wtime"].ToString();
                    if (dt.Rows[n]["Wvote"].ToString() != "")
                    {
                        model.Wvote = int.Parse(dt.Rows[n]["Wvote"].ToString());
                    }
                    if (dt.Rows[n]["Wegg"].ToString() != "")
                    {
                        model.Wegg = int.Parse(dt.Rows[n]["Wegg"].ToString());
                    }
                    if (dt.Rows[n]["Wcheck"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Wcheck"].ToString() == "1") || (dt.Rows[n]["Wcheck"].ToString().ToLower() == "true"))
                        {
                            model.Wcheck = true;
                        }
                        else
                        {
                            model.Wcheck = false;
                        }
                    }
                    model.Wself = dt.Rows[n]["Wself"].ToString();
                    if (dt.Rows[n]["Wcan"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Wcan"].ToString() == "1") || (dt.Rows[n]["Wcan"].ToString().ToLower() == "true"))
                        {
                            model.Wcan = true;
                        }
                        else
                        {
                            model.Wcan = false;
                        }
                    }
                    if (dt.Rows[n]["Wgood"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Wgood"].ToString() == "1") || (dt.Rows[n]["Wgood"].ToString().ToLower() == "true"))
                        {
                            model.Wgood = true;
                        }
                        else
                        {
                            model.Wgood = false;
                        }
                    }
                    model.Wtype = dt.Rows[n]["Wtype"].ToString();
                    if (dt.Rows[n]["Wgrade"].ToString() != "")
                    {
                        model.Wgrade = int.Parse(dt.Rows[n]["Wgrade"].ToString());
                    }
                    if (dt.Rows[n]["Wterm"].ToString() != "")
                    {
                        model.Wterm = int.Parse(dt.Rows[n]["Wterm"].ToString());
                    }
                    if (dt.Rows[n]["Whit"].ToString() != "")
                    {
                        model.Whit = int.Parse(dt.Rows[n]["Whit"].ToString());
                    }
                    if (dt.Rows[n]["Wlscore"].ToString() != "")
                    {
                        model.Wlscore = int.Parse(dt.Rows[n]["Wlscore"].ToString());
                    }
                    if (dt.Rows[n]["Wlemotion"].ToString() != "")
                    {
                        model.Wlemotion = int.Parse(dt.Rows[n]["Wlemotion"].ToString());
                    }
                    if (dt.Rows[n]["Woffice"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Woffice"].ToString() == "1") || (dt.Rows[n]["Woffice"].ToString().ToLower() == "true"))
                        {
                            model.Woffice = true;
                        }
                        else
                        {
                            model.Woffice = false;
                        }
                    }
                    if (dt.Rows[n]["Wflash"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Wflash"].ToString() == "1") || (dt.Rows[n]["Wflash"].ToString().ToLower() == "true"))
                        {
                            model.Wflash = true;
                        }
                        else
                        {
                            model.Wflash = false;
                        }
                    }
                    if (dt.Rows[n]["Werror"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Werror"].ToString() == "1") || (dt.Rows[n]["Werror"].ToString().ToLower() == "true"))
                        {
                            model.Wflash = true;
                        }
                        else
                        {
                            model.Wflash = false;
                        }
                    }
                    if (dt.Rows[n]["Wfscore"].ToString() != "")
                    {
                        model.Wfscore = int.Parse(dt.Rows[n]["Wfscore"].ToString());
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
        /// 显示本年级优秀作品的50条记录
        /// </summary>
        /// <param name="Wgrade"></param>
        /// <param name="GridViewwork"></param>
        public DataTable ShowBestWork(int Sgrade, int Syear, int Sterm)
        {
            return dal.ShowBestWork(Sgrade, Syear, Sterm).Tables[0];
        }

        /// <summary>
        /// 显示我的所有作品
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public DataTable ShowMywork(string Snum)
        {
            return dal.ShowMywork(Snum);
        }                
        /// <summary>
        /// 显示我本年级本学期的所有作品
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wgrade"></param>
        /// <param name="Wterm"></param>
        /// <returns></returns>
        public DataSet ShowThisTermWorks(string Wnum, int Wgrade, int Wterm)
        {
            return dal.ShowThisTermWorks(Wnum, Wgrade, Wterm);
        }
         /// <summary>
        /// 显示我本年级本学期的所有作品
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wgrade"></param>
        /// <param name="Wterm"></param>
        /// <returns></returns>
        public DataSet ShowThisTermWorksCircle(string Wnum, int Wgrade, int Wterm)
        {
            return dal.ShowThisTermWorksCircle(Wnum, Wgrade, Wterm);
        }
        /// <summary>
        /// 显示我的所有作品
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public DataTable ShowMyAllWorks(string Wnum)
        {
            return dal.ShowMyAllWorks(Wnum);
        }
        /// <summary>
        /// 列表我有所作品的学案活动代号和分值
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public DataSet ShowMyworkScore(string Wnum)
        {
            return ShowMyworkScore(Wnum);
        }
        /// <summary>
        /// 根据作品Wid获得学案名称
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public string GetCtitle(int Wid)
        {
            return dal.GetCtitle(Wid);
        }
                
        /// <summary>
        /// 阅读量Whit加1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWhit(int Wid)
        {
            dal.UpdateWhit(Wid);
        }

        /// <summary>
        /// 投票Wvote加1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWvote(int Wid)
        {
            dal.UpdateWvote(Wid);
        }
        /// <summary>
        /// 投票Wegg减1
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWegg(int Wid)
        {
            dal.UpdateWegg(Wid);
        }
        public void UpdateWegg(int Wmid, string Wnum)
        {
            dal.UpdateWegg(Wmid, Wnum);
        }
        /// <summary>
        /// 获得本作品Wegg
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public int GetWegg(int Wid)
        {
           return dal.GetWegg(Wid);
        }
                
        /// <summary>
        /// 获得本作品Wegg
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public int GetWegg(int Wmid, string Wnum)
        {
            return dal.GetWegg(Wmid, Wnum);
        }
                
        /// <summary>
        /// 获得本作品Wegg
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public int GetWegg(int Wmid, int Wsid)
        {
            return dal.GetWegg(Wmid, Wsid);//引用了自己造成页面崩溃；并引起网站应用程序池停止
        }
        /// <summary>
        /// 显示该学案该学号完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string HowCidWorks(int Wcid, string Wnum)
        {
            return dal.HowCidWorks(Wcid, Wnum);
        }
                /// <summary>
        /// 显示该学案该学号完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public int CountCidWorks(int Wcid, string Wnum)
        {
            return dal.CountCidWorks(Wcid, Wnum);
        }
                /// <summary>
        /// 显示该学案该学号完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string HowCidWorks(int Wcid, int Wsid)
        {
            return dal.HowCidWorks(Wcid, Wsid);
        }
        /// <summary>
        /// 显示该学案本班完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Snums"></param>
        /// <returns></returns>
        public string HowCourseWorks(int Wcid, string Snums)
        {
            return dal.HowCourseWorks(Wcid, Snums);
        }

        /// <summary>
        /// 显示该学案本班完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string HowCourseWorks(int Wcid, int Sgrade, int Sclass)
        {
            return dal.HowCourseWorks(Wcid, Sgrade, Sclass);
        }
        /// <summary>
        /// 显示该学案本任务本班完成作品的数量
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmsort"></param>
        /// <returns></returns>
        public string HowWorks(int Syear, int Sgrade, int Sclass, int Wmid)
        {
            return dal.HowWorks(Syear,Sgrade, Sclass, Wmid);
        }

        /// <summary>
        /// 根据学号，获得本学案的作品列表，只返回Wid,Wmsort,Wurl,Wscore,Wip,Wcheck
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public DataSet MyHowWorks(int Wcid, string Wnum)
        {
            return dal.MyHowWorks(Wcid, Wnum);
        }
        /// <summary>
        /// 根据学号和活动mid，获得本活动的作品列表，只返回Wid,Wmsort,Wurl,Wscore,Wip,Wcheck,Wself,Wcan
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public DataSet MyMissonWorks(int Wmid, string Wnum)
        {
            return dal.MyMissonWorks(Wmid, Wnum);
        }
        /// <summary>
        /// 获得该学案本任务本班完成作品
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataTable ShowMissionWorks(int Syear, int Sgrade, int Sclass, int Wmid)
        {
            return dal.ShowMissionWorks(Syear, Sgrade, Sclass, Wmid);
        }

        /// <summary>
        /// 获得该学案本任务本班完成组内作品
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataTable ShowMissionWorksGroup(int Sgrade, int Sclass, int Wmid,int Sgroup)
        {
            return dal.ShowMissionWorksGroup(Sgrade, Sclass, Wmid,Sgroup);
        }

        /// <summary>
        /// 查询今天本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl,Wvote,Wscore,Qwork</returns>
        public DataSet ShowTodayWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {            
            return dal.ShowTodayWorks(Sgrade, Sclass, Wcid, Wmid);
        }
        /// <summary>
        /// 查询本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl,Wvote,Wscore,Qwork</returns>
        public DataSet ShowClassWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            return dal.ShowClassWorks(Sgrade, Sclass, Wcid, Wmid);
        }             
        /// <summary>
        /// 查询本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl,Wvote,Wscore,Qwork</returns>
        public DataTable ShowClassWorksBySort(int Sgrade, int Sclass, int Wmid, string Sort)
        {
            return dal.ShowClassWorksBySort(Sgrade, Sclass, Wmid, Sort);
        }
        /// <summary>
        /// 查询本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl</returns>
        public DataTable ShowCircleWorksSelect(int Sgrade, int Sclass, int Wcid, int Wmid,int Wscore)
        {
            switch (Wscore)
            {
                case 12:
                    return dal.ShowCircleGood(Sgrade, Sclass, Wcid, Wmid);
                case 10:
                    return dal.ShowCircleScore(Sgrade, Sclass, Wcid, Wmid,Wscore);
                default:
                return dal.ShowCircleWorks(Sgrade, Sclass, Wcid, Wmid);            
            }
        }

        /// <summary>
        /// 查询本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl</returns>
        public DataTable ShowCircleWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            return dal.ShowCircleWorks(Sgrade, Sclass, Wcid, Wmid);
        }
        /// <summary>
        /// 查询本班本任务作品列表,flash专用
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public DataSet ShowClassFlashWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            return dal.ShowClassFlashWorks(Sgrade, Sclass, Wcid, Wmid);
        }  
        /// <summary>
        /// 查询本班本任务作品列表,office专用
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public DataSet ShowClassOfficeWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            return dal.ShowClassOfficeWorks(Sgrade, Sclass, Wcid, Wmid);
        }
        /// <summary>
        /// 查询本班本任务作品列表,Photo专用
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public DataSet ShowClassPhotoWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            return dal.ShowClassPhotoWorks(Sgrade, Sclass, Wcid, Wmid);
        }                
        /// <summary>
        /// 查询本班本任务作品列表,Swf专用
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public DataSet ShowClassSwfWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            return dal.ShowClassWtypeWorks(Sgrade, Sclass, Wcid, Wmid,"swf");
        }
        /// <summary>
        /// 查询本班本任务作品列表,htm专用
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public DataSet ShowClasshtmWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            return dal.ShowClassWtypeWorks(Sgrade, Sclass, Wcid, Wmid,"htm");
        }
        /// <summary>
        /// 查询本班本任务作品列表,通用
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wtype"></param>
        /// <returns></returns>
        public DataSet ShowClassWtypeWorks(int Sgrade, int Sclass, int Wcid, int Wmid,string Wtype)
        {
            return dal.ShowClassWtypeWorks(Sgrade, Sclass, Wcid, Wmid, Wtype);
        }
        /// <summary>
        /// 查询本班本任务未完成学生姓名列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataTable ShowClassNoWorks(int Syear, int Sgrade, int Sclass, int Wmid)
        {
            return dal.ShowClassNoWorks(Syear, Sgrade, Sclass,Wmid);
        }
                /// <summary>
        /// 查询今天本班本任务未完成学生姓名列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataSet ShowTodayNotWorks(int Syear, int Sgrade, int Sclass, int Wmid)
        {
            return dal.ShowTodayNotWorks(Syear, Sgrade, Sclass, Wmid);
        }
        /// <summary>
        /// 查询今天本班本任务未完成学生姓名列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataSet ShowTodayNoWorks(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            return dal.ShowTodayNoWorks(Sgrade, Sclass, Wcid, Wmid);
        }
        /// <summary>
        /// 查询今天本班学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string GetTodayCid(int Sgrade, int Sclass,int Syear)
        {
            return dal.GetTodayCid(Sgrade, Sclass,Syear);
        }
        /// <summary>
        /// 查找本班本学案本任务本机Ip 已经完成的学号
       /// </summary>
       /// <param name="Sgrade"></param>
       /// <param name="Sclass"></param>
       /// <param name="Wcid"></param>
       /// <param name="Wmid"></param>
       /// <param name="Wip"></param>
       /// <returns></returns>
        public string IpWorkDoneSnum(int Sgrade, int Sclass, int Wcid, int Wmid, string Wip)
        {
            return dal.IpWorkDoneSnum(Sgrade, Sclass, Wcid, Wmid, Wip);
        }                
        /// <summary>
        /// 根据学号和活动编号返回作品链接
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public string WorkUrl(string Wnum, int Wmid)
        {
            return dal.WorkUrl(Wnum, Wmid);
        }

        /// <summary>
        /// 判断该学号本任务作品是否提交并记录,返回Wid的值
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public string WorkDone(string Wnum, int Wcid, int Wmid)
        {
            return dal.WorkDone(Wnum, Wcid, Wmid);

        }
                
        /// <summary>
        /// 判断该学号本任务作品是否提交评价
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public bool WorkDoneChecked(string Wnum, int Wcid, int Wmid)
        {
            return dal.WorkDoneChecked(Wnum, Wcid, Wmid);
        }
        /// <summary>
        /// 检查该作品是否已经评分,是则返回真
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public  bool IsChecked(int Wid)
        {
            return dal.IsChecked(Wid);
        }
        /// <summary>
        /// 作品提交， 更新一条数据
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wurl"></param>
        /// <param name="Wfilename"></param>
        /// <param name="Wlength"></param>
        /// <param name="Wdate"></param>
        /// <param name="Wcan"></param>
        public void UpdateWorkUp(int Wid, string Wurl, string Wfilename, int Wlength, DateTime Wdate, bool Wcan, string Wthumbnail)
        {
            dal.UpdateWorkUp(Wid, Wurl, Wfilename, Wlength, Wdate, Wcan, Wthumbnail);
        }
        /// <summary>
        /// 作品提交， 更新一条数据
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wurl"></param>
        /// <param name="Wfilename"></param>
        /// <param name="Wlength"></param>
        /// <param name="Wdate"></param>
        /// <param name="Wcan"></param>
        public void UpdateWorkUp(int Wid, string Wurl, string Wfilename, int Wlength, DateTime Wdate, bool Wcan, string Wthumbnail, string Wtitle)
        {
            dal.UpdateWorkUp(Wid, Wurl, Wfilename, Wlength, Wdate, Wcan, Wthumbnail,Wtitle);
        }

        /// <summary>
        ///作品提交 增加一条数据
        ///(Wnum, Wcid,Wmid,Wmsort, Wfilename,Wtype, Wurl,Wlength, Wdate, Wip, Wtime)
        /// </summary>
        /// 
        public int AddWorkUp(LearnSite.Model.Works model)
        {
           return dal.AddWorkUp(model);
        }
                
        /// <summary>
        /// 更新自我评价
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wself"></param>
        public void UpdateWself(int Wid, string Wself)
        {
            dal.UpdateWself(Wid,Wself);    
        }
        /// <summary>
        /// 更新是否要求教师评价作品
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <param name="Wcan"></param>
        public void UpdateWcan(int Wmid, int Wnum, bool Wcan)
        {
            dal.UpdateWcan(Wmid, Wnum, Wcan);
        }        
        /// <summary>
        /// 最佳作品推荐字段自动取反
        /// </summary>
        /// <param name="Wid"></param>
        public void UpdateWgood(int Wid)
        {
            dal.UpdateWgood(Wid);
        }
                
        /// <summary>
        /// 最佳作品推荐字段设置为真
        /// </summary>
        /// <param name="Wid"></param>
        public void WgoodBest(int Wid)
        {
            dal.WgoodBest(Wid);
        }
        /// <summary>
        /// 最佳作品推荐字段设置为假
        /// </summary>
        /// <param name="Wid"></param>
        public void WgoodNormal(int Wid)
        {
            dal.WgoodNormal(Wid);
        }
                /// <summary>
        /// 获得该学案最佳作品列表Wid,Sname,Wurl,Wvote,Wgood
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public DataSet ShowCourseBestWorks(int Wcid, int Sgrade)
        {
            return dal.ShowCourseBestWorks(Wcid, Sgrade);
        }
                  
        /// <summary>
        /// 将所教班级所有未评作品的评分都设置为P，即6分
        /// </summary>
        public void WorkNoScoreSetP()
        {
            dal.WorkNoScoreSetP();
        }
        /// <summary>
        /// 显示本学案未评数
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public string ShowNotCheckCounts(int Wcid, int Sgrade)
        {
            return dal.ShowNotCheckCounts(Wcid, Sgrade);
        }

        /// <summary>
        /// 显示该学案本任务本班未评数
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public string ClassNotCheckWorks(int Wcid, int Sgrade, int Sclass, int Wmid)
        {
            return dal.ClassNotCheckWorks(Wcid, Sgrade, Sclass, Wmid);
        }
                
        /// <summary>
        /// 获取今天作业的平均分
        /// </summary>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public int GetTodayWorkScores(string Wnum)
        {
            return dal.GetTodayWorkScores(Wnum);
        }
                
        /// <summary>
        /// 教师未评时可给组内成员作品评分
        /// </summary>
        /// <param name="Wid"></param>
        /// <param name="Wlscore"></param>
        public void Updatelscore(int Wid, int Wlscore)
        {
            dal.Updatelscore(Wid, Wlscore);
        }   
        /// <summary>
        ///  给学生作品打分
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <param name="Wlscore"></param>
        public void Updatemscore(int Wmid, string Wnum, int Wlscore)
        {
            dal.Updatemscore(Wmid, Wnum, Wlscore);
        }
         /// <summary>
        /// 查询本班本任务作品列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <returns>Wid,Sname,Wurl</returns>
        public DataTable ShowCircleGood(int Sgrade, int Sclass, int Wcid, int Wmid)
        {
            return dal.ShowCircleGood(Sgrade, Sclass, Wcid, Wmid);
        }
        /// <summary>
        /// 给学生作品打分并评语
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <param name="Wlscore"></param>
        /// <param name="Wself"></param>
        public void Updatemscoreself(int Wmid, string Wnum, int Wlscore, string Wself, int Wdscore)
        {
            dal.Updatemscoreself(Wmid, Wnum, Wlscore, Wself, Wdscore);
        }

        /// <summary>
        /// 给学生作品打分并评语
        /// </summary>
        /// <param name="Wcid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <param name="Wlscore"></param>
        /// <param name="Wself"></param>
        /// <param name="Wdscore"></param>
        public void Updatemscoreself(string Wcid, string Wmid, string Wnum, int Wlscore, string Wself, int Wdscore)
        {
            dal.Updatemscoreself(Wcid,Wmid, Wnum, Wlscore, Wself,Wdscore);
        }
        /// <summary>
        /// 根据学号和任务ID返回成绩值
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string GetmyScore(int Wmid, string Wnum)
        {
            return dal.GetmyScore(Wmid, Wnum);
        }                
        ///<summary>
        /// 删除该学号该活动的作品
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        public void Delmywork(int Wmid, string Wnum)
        {
            dal.Delmywork(Wmid,Wnum);
        }
        /// <summary>
        /// 根据学号和任务ID返回成绩值和评语
        /// </summary>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string[] GetmyScoreWself(int Wmid, string Wnum)
        {
            return dal.GetmyScoreWself(Wmid, Wnum);
        }                
        /// <summary>
        /// 根据学号和任务ID返回成绩值和评语
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="Wmid"></param>
        /// <param name="Wnum"></param>
        /// <returns></returns>
        public string[] GetmyScoreWself(string Cid, string Wmid, string Wnum)
        {
            return dal.GetmyScoreWself(Cid,Wmid,Wnum);
        }
        /// <summary>
        /// 获取本组内同学作品
        /// </summary>
        /// <param name="Sgroup"></param>
        /// <param name="Wmid"></param>
        /// <returns></returns>
        public DataSet GetGroupWorks(int Sgroup, int Wmid)
        {
            return dal.GetGroupWorks(Sgroup, Wmid);
        }
               
        /// <summary>
        /// 显示本学期我未学外的某课所有优秀作品
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Cobj"></param>
        /// <param name="Cid"></param>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public DataSet ShowAllGood(string Wnum, int Cobj, int Cid, int Sgrade)
        {
            return dal.ShowAllGood(Wnum, Cobj, Cid, Sgrade);
        }
                
        /// <summary>
        /// 显示该学案的优秀推荐作品
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public DataSet ShowCourseGoodWorks(int Cid)
        {
            return dal.ShowCourseGoodWorks(Cid);
        }
                
        /// <summary>
        /// 获取当前班级学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowDoneWorkCids(int Sgrade, int Sclass, int Wterm, int Wyear)
        {
            return dal.ShowDoneWorkCids(Sgrade,Sclass, Wterm, Wyear);
        }
                
        /// <summary>
        /// 获取Wurl
        /// </summary>
        /// <param name="Wid"></param>
        /// <returns></returns>
        public string GetWorkWurl(int Wid)
        {
            return dal.GetWorkWurl(Wid);
        }
        /// <summary>
        /// 获取某学生学过的学案Cid
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowStuDoneWorkCids(string Snum, int Wterm, int Wgrade)
        {
            return dal.ShowStuDoneWorkCids(Snum,Wterm,Wgrade);
        }
                
        /// <summary>
        /// 获取最新的作品评语
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public string[] ShowLastWorkSelf(int Sid)
        {
            return dal.ShowLastWorkSelf(Sid);
        }                
        /// <summary>
        /// 获取该学生最新的作业列表8个记录
        /// </summary>
        /// <param name="Sid"></param>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public DataTable ShowLastWorks(int Sid, int Sgrade, int Term, int Cid)
        {
            return dal.ShowLastWorks(Sid, Sgrade, Term, Cid);
        }
        
        public DataTable ShowWyears()
        {
            return dal.ShowWyears();
        }

        public DataTable ShowWgrades(int Wyear)
        {
            return dal.ShowWgrades(Wyear);
        }

        public DataTable ShowWclass(int Wyear, int Wgrade)
        {
            return dal.ShowWclass(Wyear, Wgrade);
        }
        public DataTable ShowWclassWcids(int Wyear, int Wgrade, int Wclass, int Wterm)
        {
            return dal.ShowWclassWcids(Wyear, Wgrade, Wclass, Wterm);
        }
        public DataTable ShowWclassWmids(int Wyear, int Wgrade, int Wclass, int Wterm, int Wcid)
        {
            return dal.ShowWclassWmids(Wyear, Wgrade, Wclass, Wterm, Wcid);
        }
        public DataTable ShowWclassWorks(int Wyear, int Wgrade, int Wclass, int Wterm,int Wmid)
        {
            return dal.ShowWclassWorks(Wyear, Wgrade, Wclass, Wterm, Wmid);
        }
                
        /// <summary>
        /// 获取所有得分12的优秀作品列表
        /// Wid,Wcid,Wmid,Wurl,Wname,Wgrade,Wclass,Wyear,Wtype,Wscore,Wdate,Ctitle
        /// </summary>
        /// <returns></returns>
        public DataTable GetListGoodWorks()
        {
            return dal.GetListGoodWorks();
        }
        /// <summary>
        /// Wid,Wurl,Wname,Wscore
        /// </summary>
        /// <param name="Wcid"></param>
        /// <returns></returns>
        public DataTable GetCourseWorks(int Wcid)
        {
            return dal.GetCourseWorks(Wcid);
        }
        public string GetHtmMid(int Wcid, string Wnum)
        {
            return dal.GetHtmMid(Wcid, Wnum);
        }
        public string GetHtmCid(string Wnum)
        {
            return dal.GetHtmCid(Wnum);
        }
        /// <summary>
        /// 初始化加分值
        /// </summary>
        public void initWdscore()
        {
            dal.initWdscore();
        }

        /// <summary>
        /// 标记缩略图
        /// </summary>
        /// <param name="Wnum"></param>
        /// <param name="Wmid"></param>
        /// <param name="imgurl"></param>
        public void upWthumbnail(string Wnum, string Wmid, string imgurl)
        {
            dal.upWthumbnail(Wnum, Wmid,imgurl);
        }

        public void SaveProject(string id)
        {
            string[] arrayid = id.Split('-');
            string Wcid = arrayid[0];
            string Wmid = arrayid[1];

            int Wsid = Int32.Parse(HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
            string Wnum = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            string Wname = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sname"].ToString();
            string Wgrade = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
            string Wclass = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
            string Wyear = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
            string Wterm = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString();
            string Wip = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
            string Wtime = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["LoginTime"].ToString();
            DateTime Wdate = DateTime.Now;

            string MySavePath = LearnSite.Common.WorkUpload.GetWurl(Wyear, Wgrade, Wclass, Wcid, Wmid);//获得作品保存路径（如果不存在，自动创建）
            string RndTime = LearnSite.Common.WordProcess.GetRandomNum(99).ToString();
            string OnlyFileName = Wnum + "_" + Wcid + "_" + Wmid + "_" + RndTime;
            string NewFileName = OnlyFileName + ".sb2";
            string Wurl = MySavePath + "/" + NewFileName;
            string SaveFile = HttpContext.Current.Server.MapPath(Wurl);

            string NewThumbnail = OnlyFileName + ".jpg";
            string Wthumbnail = MySavePath + "/" + NewThumbnail;
            string thumbnailpath = HttpContext.Current.Server.MapPath(Wthumbnail);
            int len = 0;
            string title = "未命名" + ".sb2";
            try
            {
                HttpPostedFile pngf = HttpContext.Current.Request.Files[0];
                Stream streampng = pngf.InputStream;
                Image image = Image.FromStream(streampng);
                image.Save(thumbnailpath, System.Drawing.Imaging.ImageFormat.Jpeg);

                HttpPostedFile sbf = HttpContext.Current.Request.Files[1];
                len = sbf.ContentLength;
                title = HttpUtility.HtmlEncode(sbf.FileName) + ".sb2";
                sbf.SaveAs(SaveFile);

                image.Dispose();
            }
            catch (Exception ec)
            {
                LearnSite.Common.Log.Addlog("保存出错信息", ec.StackTrace );// ec.StackTrace可以出详细信息
            }

            bool checkcan = true;

            BLL.Works bll = new BLL.Works();
            string Wid = bll.WorkDone(Wnum, Int32.Parse(Wcid), Int32.Parse(Wmid));//返回空字符表示不存在该记录
            //记录到数据库
            if (Wid != "")
            {
                bll.UpdateWorkUp(Int32.Parse(Wid), Wurl, NewFileName, len, Wdate, checkcan, Wthumbnail,title);//更新Wfilename, Wurl,Wlength, Wdate
            }
            else
            {
                //Wnum,Wcid,Wmid,Wmsort,Wfilename,Wtype,Wurl,Wlength,Wdate,Wip
                //,Wtime,Wegg,Wgrade,Wterm,Woffice,Wflash,Wsid,Wclass,Wname,Wyear
                Model.Works wmodel = new Model.Works();
                wmodel.Wnum = Wnum;
                wmodel.Wcid = Int32.Parse(Wcid);
                wmodel.Wmid = Int32.Parse(Wmid);
                wmodel.Wmsort = 5;
                wmodel.Wfilename = NewFileName;
                wmodel.Wtype = "sb2";
                wmodel.Wurl = Wurl;
                wmodel.Wlength = len;
                wmodel.Wdate = Wdate;
                wmodel.Wip = Wip;
                wmodel.Wtime = Wtime;
                wmodel.Wcan = checkcan;
                wmodel.Wcheck = false;
                wmodel.Wegg = 12;//设定票数为12张
                wmodel.Whit = 0;
                wmodel.Wgrade = Int32.Parse(Wgrade);
                wmodel.Wterm = Int32.Parse(Wterm);
                wmodel.Woffice = false;
                wmodel.Wsid = Wsid;
                wmodel.Wclass = Int32.Parse(Wclass);
                wmodel.Wname = HttpUtility.UrlDecode(Wname);
                wmodel.Wyear = Int32.Parse(Wyear);
                wmodel.Wflash = false;
                wmodel.Werror = false;
                wmodel.Wthumbnail = Wthumbnail;
                wmodel.Wtitle = title;
                bll.AddWorkUp(wmodel);//添加作品提交记录
                BLL.Signin sn = new BLL.Signin();
                sn.UpdateQwork(Wsid, Int32.Parse(Wcid));//更新今天签到表中的作品数量
            }

        }
        private int FindPos(string source, string word)
        {
            Regex regex = new Regex(word);
            Match mc;
            mc = regex.Match(source);
            return mc.Index;
        }
        public void SaveThumbnail(string id, byte[] pngfile)
        {
            string[] arrayid = id.Split('-');
            string Wcid = arrayid[0];
            string Wmid = arrayid[1];

            int Wsid = Int32.Parse(HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
            string Wnum = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            string Wgrade = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
            string Wclass = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
            string Wyear = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
            string Wterm = HttpContext.Current.Request.Cookies[Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString();
            DateTime Pdate = DateTime.Now;
            string MySavePath = Common.WorkUpload.GetWurl(Wyear, Wgrade, Wclass, Wcid, Wmid);//获得作品保存路径（如果不存在，自动创建）
            string OnlyFileName = Wnum + "_" + Wcid + "_" + Wmid;
            string NewFileName = OnlyFileName + ".png";
            string Wurl = MySavePath + "/" + NewFileName;
            string SaveFile = HttpContext.Current.Server.MapPath(Wurl);

            Works bll = new Works();
            bll.upWthumbnail(Wnum, Wmid, Wurl);//标记缩略图
            
            //------------------------------------
            MemoryStream ms = new MemoryStream();
            ms.Write(pngfile,0, pngfile.Length);
            Image image = Image.FromStream(ms);
            
            //Bitmap bm = new Bitmap(image, new Size(160, 120));
            //bm.Save(SaveFile);
           // ------------------------------------
            image.Save(SaveFile);

            //byte[] pngfile = context.Request.BinaryRead(context.Request.TotalBytes);
            ////FileStream fs = new FileStream(SaveFile, FileMode.Create);
            ////fs.Write(pngfile, 0, pngfile.Length);//保存缩略图
           //// fs.Close();
           //// fs.Dispose();
        }

        public void SworkToBytes(string id)
        {
            string sburl = "~/Statics/Cat.sb2";

            if (id.Contains("-"))
            {
                string[] arrayid = id.Split('-');
                string Wcid = arrayid[0];
                string Wmid = arrayid[1];
                string Wnum = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
                Model.Works work = new Model.Works();
                work = GetModelByStu(Int32.Parse(Wmid), Wnum);
                if (work != null)
                {
                    sburl = work.Wurl;
                }
                else
                {
                    LearnSite.Model.Mission model = new LearnSite.Model.Mission();
                    LearnSite.BLL.Mission mn = new LearnSite.BLL.Mission();
                    model = mn.GetModel(Int32.Parse(Wmid));
                    if (model != null)
                    {
                        sburl = model.Mexample;
                    }
                }
            }
            else
            {
                sburl= GetWorkWurl(Int32.Parse(id));
            }

            string sbpath = HttpContext.Current.Server.MapPath(sburl);
            //获取文件的二进制数据。
            byte[] datas = System.IO.File.ReadAllBytes(sbpath);
            //将二进制数据写入到输出流中。
            HttpContext.Current.Response.OutputStream.Write(datas, 0, datas.Length);
        }

        public int GetSbCount()
        {
            return dal.GetSbCount();
        }

        public DataTable GetSb(int page, int sbcount)
        {
            return dal.GetSb(page,sbcount);
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

