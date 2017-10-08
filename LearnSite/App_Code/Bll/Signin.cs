using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
using System.Collections;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Signin 的摘要说明。
	/// </summary>
	public class Signin
	{
		private readonly LearnSite.DAL.Signin dal=new LearnSite.DAL.Signin();
		public Signin()
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
		public bool Exists(int Qid)
		{
			return dal.Exists(Qid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Signin model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Signin model)
		{
			dal.Update(model);
		}
                /// <summary>
        /// 更新该学号今天的作品数量
        /// </summary>
        /// <param name="Qnum"></param>
        /// <param name="Qwork"></param>
        public void UpdateQwork(int Qsid, int Wcid)
        {
            dal.UpdateQwork(Qsid,Wcid);
        }
                /// <summary>
        /// 更新一条数据（给学生学习表现评分）
        /// </summary>
        public void UpdateAttitude(int Qid, int Qattitude, string Qnote, int Qcid)
        {
            dal.UpdateAttitude(Qid, Qattitude, Qnote,Qcid);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Qid)
		{
			
			dal.Delete(Qid);
		}
                
        /// <summary>
        /// 清除几年前的签到记录
        /// </summary>
        /// <param name="Wyear"></param>
        public int DeleteOldyear(int Wyear)
        {
            return dal.DeleteOldyear(Wyear);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Signin GetModel(int Qid)
		{
			
			return dal.GetModel(Qid);
		}
                
        /// <summary>
        /// 根据学号得到一个对象实体，最近的签到记录
        /// </summary>
        public LearnSite.Model.Signin GetModelm(string Qnum)
        {
            return dal.GetModelm(Qnum);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Signin GetModelByCache(int Qid)
		{
			
			string CacheKey = "SigninModel-" + Qid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Qid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Signin)objModel;
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
		public List<LearnSite.Model.Signin> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<LearnSite.Model.Signin> DataTableToList(DataTable dt)
        {
            List<LearnSite.Model.Signin> modelList = new List<LearnSite.Model.Signin>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LearnSite.Model.Signin model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new LearnSite.Model.Signin();
                    if (dt.Rows[n]["Qid"].ToString() != "")
                    {
                        model.Qid = int.Parse(dt.Rows[n]["Qid"].ToString());
                    }
                    model.Qnum = dt.Rows[n]["Qnum"].ToString();
                    if (dt.Rows[n]["Qattitude"].ToString() != "")
                    {
                        model.Qattitude = int.Parse(dt.Rows[n]["Qattitude"].ToString());
                    }
                    if (dt.Rows[n]["Qdate"].ToString() != "")
                    {
                        model.Qdate = DateTime.Parse(dt.Rows[n]["Qdate"].ToString());
                    }
                    if (dt.Rows[n]["Qyear"].ToString() != "")
                    {
                        model.Qyear = int.Parse(dt.Rows[n]["Qyear"].ToString());
                    }
                    if (dt.Rows[n]["Qmonth"].ToString() != "")
                    {
                        model.Qmonth = int.Parse(dt.Rows[n]["Qmonth"].ToString());
                    }
                    if (dt.Rows[n]["Qday"].ToString() != "")
                    {
                        model.Qday = int.Parse(dt.Rows[n]["Qday"].ToString());
                    }
                    model.Qweek = dt.Rows[n]["Qweek"].ToString();
                    model.Qip = dt.Rows[n]["Qip"].ToString();
                    model.Qmachine = dt.Rows[n]["Qmachine"].ToString();
                    model.Qnote = dt.Rows[n]["Qnote"].ToString();
                    if (dt.Rows[n]["Qwork"].ToString() != "")
                    {
                        model.Qwork = int.Parse(dt.Rows[n]["Qwork"].ToString());
                    }
                    if (dt.Rows[n]["Qgrade"].ToString() != "")
                    {
                        model.Qgrade = int.Parse(dt.Rows[n]["Qgrade"].ToString());
                    }
                    if (dt.Rows[n]["Qterm"].ToString() != "")
                    {
                        model.Qterm = int.Parse(dt.Rows[n]["Qterm"].ToString());
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
        /// 学生界面今天签到显示
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Qyear"></param>
        /// <param name="Qmonth"></param>
        /// <param name="Qday"></param>
        /// <returns></returns>
        public DataTable  OnlineToday(int Sgrade, int Sclass, int Qyear, int Qmonth, int Qday)
        {
          return  dal.OnlineToday(Sgrade, Sclass, Qyear, Qmonth, Qday).Tables[0];
        }
        
        /// <summary>
        /// 查询班级签到列表
        /// </summary>
        /// <param name="Qgrade"></param>
        /// <param name="Qclass"></param>
        /// <returns></returns>
        public DataSet GetSignClass(int Sgrade, int Sclass)
        {
           return dal.GetSignClass(Sgrade, Sclass);
        }

         /// <summary>
        /// 查询班级某年某月某日的详细 已签到记录11111111
        /// </summary>
        /// <param name="Qgrade"></param>
        /// <param name="Qclass"></param>
        /// <param name="Qyear"></param>
        /// <param name="Qmonth"></param>
        /// <param name="Qday"></param>
        public DataSet Signclassdetail(int Sgrade, int Sclass, int Qyear, int Qmonth, int Qday)
        {
            return dal.Signclassdetail(Sgrade, Sclass, Qyear, Qmonth, Qday);
        }
        public DataSet SignclassdetailSort(int Sgrade, int Sclass, int Qyear, int Qmonth, int Qday, int sort)
        {
            return dal.SignclassdetailSort(Sgrade, Sclass, Qyear, Qmonth, Qday, sort);
        }
        /// <summary>
        /// 某班级签到导出到Excel
        /// </summary>
        public void SignExcel(int Sgrade, int Sclass, int Qterm)
        {
            dal.SignExcel(Sgrade, Sclass, Qterm);
        }
        /// <summary>
        /// 根据学号查询签到记录，按年级、学期排序
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public DataSet SignSnumdetail(string Snum)
        {
            return dal.SignSnumdetail(Snum);
        }

        /// <summary>
        /// 查询班级某年某月某日的详细 未签到记录0000000
        /// </summary>
        /// <param name="Qgrade"></param>
        /// <param name="Qclass"></param>
        /// <param name="Qyear"></param>
        /// <param name="Qmonth"></param>
        /// <param name="Qday"></param>
        public DataSet NoSignclassdetail(int Sgrade, int Sclass, int Qyear, int Qmonth, int Qday)
        {
            return dal.NoSignclassdetail(Sgrade, Sclass, Qyear, Qmonth, Qday);
        }

                
        /// <summary>
        /// 获取今天签到的同学
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataTable GetSiginStudents(int Sgrade, int Sclass)
        {
            return dal.GetSiginStudents(Sgrade, Sclass);
        }
        /// <summary>
        /// 查询开始上课页面，班级签到列表
        /// </summary>
        /// <param name="Qgrade"></param>
        /// <param name="Qclass"></param>
        /// <param name="Qyear"></param>
        /// <param name="Qmonth"></param>
        /// <param name="Qday"></param>
        /// <returns></returns>
        public DataTable  StartSignClass(int Sgrade, int Sclass, int Qyear, int Qmonth, int Qday, string signsort)
        {
            return dal.StartSignClass(Sgrade, Sclass,Qyear,Qmonth,Qday,signsort).Tables[0];
        }

        /// <summary>
        /// 查询开始上课页面，班级签到列表
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Qyear"></param>
        /// <param name="Qmonth"></param>
        /// <param name="Qday"></param>
        /// <param name="signsort"></param>
        /// <param name="hid"></param>
        /// <returns></returns>
        public SeatCollect StartSignTable(int Sgrade, int Sclass, int Qyear, int Qmonth, int Qday, string pcroom)
        {
            SeatCollect sct = new SeatCollect();
            sct.Dt = dal.StartSignClass(Sgrade, Sclass, Qyear, Qmonth, Qday, "1").Tables[0];
            sct.Online = sct.Dt.Rows.Count;//在线人数
            sct.Column = 8;//列数
            if (!string.IsNullOrEmpty(pcroom) && sct.Online > 0)
            {
                //Qid,Qip, Qnum, Qname,Sleader,Sgroup,Sgtitle,left(Qmachine,12) as QmachineShort,right(Qdate,8) as Qdate,Qattitude,Qnote,Qwork,Qgroup,Qgscore
                DataTable studt = sct.Dt;
                Computers cbll = new LearnSite.BLL.Computers();
                SeatCollect cmpsct = cbll.GetSeat(pcroom);//select Pip,Pmachine,Px,Py from Computers
                sct.Column = cmpsct.Column;
                DataTable cmpdt = cmpsct.Dt;
                int scount = studt.Rows.Count;
                int pcount = cmpdt.Rows.Count;
                ArrayList empty = new ArrayList();
                if (scount > 0 && pcount > 0)
                {
                    studt.Columns.Add("Px", typeof(int));
                    studt.Columns.Add("Py", typeof(int));//增加坐标字段
                    for (int i = 0; i < pcount; i++)
                    {
                        string pip = cmpdt.Rows[i][0].ToString();
                        bool find = false;
                        int tempj = -1;
                        for (int j = 0; j < scount; j++)
                        {
                            string sip = studt.Rows[j][1].ToString();
                            if (pip.Equals(sip))
                            {
                                find = true;//如果相等，说明找到，跳出循环
                                tempj = j;
                                break;
                            }
                        }
                        if (!find)
                        {
                            empty.Add(i); //如果找不到，则收集序号
                        }
                        else
                        {
                            if (tempj > -1) //如果找到，则给学生表增加坐标位置
                            {
                                studt.Rows[tempj][13] = cmpdt.Rows[i][2];
                                studt.Rows[tempj][14] = cmpdt.Rows[i][3];
                            }
                        }
                    }

                    for (int k = 0; k < empty.Count; k++)
                    {
                        int emindex = Int32.Parse(empty[k].ToString());
                        //select Pip,Pmachine,Px,Py from Computers
                        string pm = cmpdt.Rows[emindex][1].ToString();
                        int px = Int32.Parse(cmpdt.Rows[emindex][2].ToString());
                        int py = Int32.Parse(cmpdt.Rows[emindex][3].ToString());
                        DataRow dr = studt.NewRow();
                        //select distinct Qid,Qip, Qnum, Qname,Sleader,Sgroup,Sgtitle,Qattitude,left(Qmachine,12) as QmachineShort,Qnote,Qwork,Qgroup,Qgscore
                        dr[0] = 0;
                        dr[1] = "";
                        dr[2] = "";
                        dr[3] = "";
                        dr[4] = false;
                        dr[5] = 0;
                        dr[6] = "";
                        dr[7] = 0;
                        dr[8] = pm;
                        dr[9] = "";
                        dr[10] = 0;
                        dr[11] = 0;
                        dr[12] = 0;
                        dr[13] = px;
                        dr[14] = py;
                        studt.Rows.Add(dr);
                    }
                    DataView dv = studt.DefaultView;
                    dv.Sort = "Px Asc , Py Asc";
                    DataTable dt = dv.ToTable();
                    sct.Dt = dt;
                }
            }
            return sct;
        }
                
        
        /// <summary>
        /// 查询开始上课页面，班级没有签到列表
        /// </summary>
        /// <param name="Qgrade"></param>
        /// <param name="Qclass"></param>
        /// <param name="Qyear"></param>
        /// <param name="Qmonth"></param>
        /// <param name="Qday"></param>
        /// <returns></returns>
        public DataTable StartNoSignClass(int Sgrade, int Sclass, int Syear, int Qyear, int Qmonth, int Qday)
        {
            return dal.StartNoSignClass(Sgrade, Sclass,Syear,Qyear,Qmonth,Qday);
        }
                /// <summary>
        /// 查询开始上课页面，班级没有签到列表
        /// </summary>
        /// <param name="Qgrade"></param>
        /// <param name="Qclass"></param>
        /// <param name="Qyear"></param>
        /// <param name="Qmonth"></param>
        /// <param name="Qday"></param>
        /// <returns></returns>
        public DataTable StartNoSignClassTwo(int Sgrade, int Sclass, int Syear, int Qyear, int Qmonth, int Qday)
        {
            return dal.StartNoSignClassTwo(Sgrade, Sclass, Syear, Qyear, Qmonth, Qday);
        }
        /// <summary>
        /// 返回本班今天作品未提交的学生 Qid, Qnum, Sname,Sscore
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet GetNoWorkStudents(int Sgrade, int Sclass)
        {
            int Qyear=DateTime.Now.Year;
            int Qmonth=DateTime.Now.Month;
            int Qday=DateTime.Now.Day;
            int Qwork = 0;

            return dal.GetNoWorkStudents(Sgrade, Sclass, Qyear, Qmonth, Qday, Qwork);
        }
        /// <summary>
        /// 在签到表中删除学生表中不存在班级的学生
        /// </summary>
        public void Upgrade()
        {
            dal.Upgrade();
        }

                /// <summary>
        ///签到，如果今天的签到记录不存在则增加一条
        ///签到参数：学号、日期、IP
        /// 
        ///模传递参数Qnum,Qdate,Qyear,Qmonth,Qday,Qweek,Qip
        /// </summary>
        /// <param name="smodel"></param>
        public int SigninToday(string Qnum, DateTime Qdate, string Qip, int Qgrade, int Qterm, int Qsid, string Qname, int Qclass, int Qsyear)
        {
           return dal.SigninToday(Qnum, Qdate, Qip,Qgrade,Qterm,Qsid,Qname,Qclass,Qsyear);
        }
                
        /// <summary>
        /// 判断当前登录的IP是否与最近日期登录的IP一致
        /// </summary>
        /// <param name="Qnum"></param>
        /// <param name="LoginIp"></param>
        /// <returns></returns>
        public bool IsSameIp(string Qnum, string LoginIp)
        {
            return dal.IsSameIp(Qnum, LoginIp);
        }
                        
        /// <summary>
        /// 获取最近三个月内本机登录过的学生姓名
        /// </summary>
        /// <param name="Qip"></param>
        /// <returns></returns>
        public DataSet GetIpStudents(string Qip)
        {
            return dal.GetIpStudents(Qip);
        }
        /// <summary>
        /// 更新组评价
        /// </summary>
        /// <param name="Sgroup"></param>
        /// <param name="Qgroup"></param>
        /// <param name="Qgscore"></param>
        /// <returns></returns>
        public int UpdateSgroup(int Sgroup, string Qgroup, int Qgscore, int Qcid)
        {
            return dal.UpdateSgroup(Sgroup, Qgroup, Qgscore,Qcid);
        }
                
        /// <summary>
        /// 删除该班级的签到记录
        /// </summary>
        /// <param name="Qgrade"></param>
        /// <param name="Qclass"></param>
        /// <param name="Qsyear"></param>
        /// <returns></returns>
        public int DelSignClass(int Qgrade, int Qclass, int Qsyear)
        {
            return dal.DelSignClass(Qgrade, Qclass, Qsyear);
        }
        /// <summary>
        /// 获取最近一个月所教班级签到学生的姓名和IP列表
        /// Qsid,Qgrade,Qclass,Qname,Qip
        /// </summary>
        /// <param name="Rhid"></param>
        /// <param name="months"></param>
        /// <returns></returns>
        public DataTable GetQnameQip(int Rhid, int weeks)
        {
            return dal.GetQnameQip(Rhid, weeks);
        }
                
        /// <summary>
        /// 获得该本班本课的课堂表现
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Qcid"></param>
        /// <returns></returns>
        public DataTable GetClassListQattitude(int Sgrade, int Sclass, int Qcid)
        {
            return dal.GetClassListQattitude(Sgrade, Sclass, Qcid);
        }
                
        /// <summary>
        /// 获取组长的小组表现分
        /// </summary>
        /// <param name="Qnum"></param>
        /// <param name="Qcid"></param>
        /// <returns></returns>
        public string GetLeaderQgroup(string Qnum, int Qcid)
        {
            return dal.GetLeaderQgroup(Qnum, Qcid);
        }
                
        /// <summary>
        /// 获取组长的小组表现分
        /// </summary>
        /// <param name="Qsid"></param>
        /// <param name="Qcid"></param>
        /// <returns></returns>
        public int GetLeaderQgroup(int Qsid, int Qcid)
        {
            return dal.GetLeaderQgroup(Qsid, Qcid);
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

