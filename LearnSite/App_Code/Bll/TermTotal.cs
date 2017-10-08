using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类TermTotal 的摘要说明。
	/// </summary>
	public class TermTotal
	{
		private readonly LearnSite.DAL.TermTotal dal=new LearnSite.DAL.TermTotal();
		public TermTotal()
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
		public bool Exists(int Tid)
		{
			return dal.Exists(Tid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.TermTotal model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.TermTotal model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Tid)
		{
			
			dal.Delete(Tid);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.TermTotal GetModel(int Tid)
		{
			
			return dal.GetModel(Tid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.TermTotal GetModelByCache(int Tid)
		{
			
			string CacheKey = "TermTotalModel-" + Tid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Tid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.TermTotal)objModel;
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
		public List<LearnSite.Model.TermTotal> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.TermTotal> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.TermTotal> modelList = new List<LearnSite.Model.TermTotal>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.TermTotal model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.TermTotal();
					if(dt.Rows[n]["Tid"].ToString()!="")
					{
						model.Tid=int.Parse(dt.Rows[n]["Tid"].ToString());
					}
					model.Tnum=dt.Rows[n]["Tnum"].ToString();
					if(dt.Rows[n]["Tterm"].ToString()!="")
					{
						model.Tterm=int.Parse(dt.Rows[n]["Tterm"].ToString());
					}
					if(dt.Rows[n]["Tgrade"].ToString()!="")
					{
						model.Tgrade=int.Parse(dt.Rows[n]["Tgrade"].ToString());
					}
					if(dt.Rows[n]["Tscore"].ToString()!="")
					{
						model.Tscore=int.Parse(dt.Rows[n]["Tscore"].ToString());
					}
					if(dt.Rows[n]["Tgscore"].ToString()!="")
					{
						model.Tgscore=int.Parse(dt.Rows[n]["Tgscore"].ToString());
					}
					if(dt.Rows[n]["Tquiz"].ToString()!="")
					{
						model.Tquiz=int.Parse(dt.Rows[n]["Tquiz"].ToString());
					}
					if(dt.Rows[n]["Tattitude"].ToString()!="")
					{
						model.Tattitude=int.Parse(dt.Rows[n]["Tattitude"].ToString());
					}
					if(dt.Rows[n]["Twscore"].ToString()!="")
					{
						model.Twscore=int.Parse(dt.Rows[n]["Twscore"].ToString());
					}
					if(dt.Rows[n]["Ttscore"].ToString()!="")
					{
						model.Ttscore=int.Parse(dt.Rows[n]["Ttscore"].ToString());
					}
					if(dt.Rows[n]["Tpscore"].ToString()!="")
					{
						model.Tpscore=int.Parse(dt.Rows[n]["Tpscore"].ToString());
					}
					if(dt.Rows[n]["Tallscore"].ToString()!="")
					{
						model.Tallscore=int.Parse(dt.Rows[n]["Tallscore"].ToString());
					}
					model.Tape=dt.Rows[n]["Tape"].ToString();
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
        /// 生成学期统计表
        /// </summary>
        public void TermScore()
        {
            dal.TermScore();
        }
        /// <summary>
        /// 获取该学号的每学期成绩列表
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public DataSet GetSnumTermList(string Snum)
        {
            return dal.GetSnumList(Snum);
        }
        /// <summary>
        /// 获取某年级所有学期综合评定成绩
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="GradeArray"></param>
        /// <returns></returns>
        private DataTable GetOverallMerit(int Tyear, string GradeArray)
        {
            DataTable stuDt = null;
            if (GradeArray != "")
            {
                stuDt = dal.GetTyearStuTwo(Tyear);//获取本年级所有学生，按班级、学号排序 Tnum,Tclass,Tname
                if (stuDt != null)
                {
                    string[] grades = GradeArray.Split(',');
                    int gcount = grades.Length;

                    for (int i = 0; i < gcount; i++)
                    {
                        string grade = grades[i];
                        string gtermone = "g" + grade + "m1";
                        string gtermtwo = "g" + grade + "m2";
                        stuDt.Columns.Add(gtermone, typeof(string));
                        stuDt.Columns.Add(gtermtwo, typeof(string));
                    }
                    DataTable ApeDt = dal.GetGradeAllScores(Tyear);//获取字段Tnum,Tgrade,Tterm,Tape,Tname
                    if (ApeDt != null)
                    {
                        int scount = stuDt.Rows.Count;
                        for (int j = 0; j < scount; j++)
                        {
                            string tnum = stuDt.Rows[j][1].ToString();
                            DataView dv = ApeDt.DefaultView;
                            dv.RowFilter = "Tnum='" + tnum + "'";
                            DataTable dtsnum = dv.ToTable();//Tnum,Tgrade,Tterm,Tape,Sname
                            int dtmcount = dtsnum.Rows.Count;
                            for (int k = 0; k < dtmcount; k++)
                            {
                                string tgrade = dtsnum.Rows[k][1].ToString();
                                string tterm = dtsnum.Rows[k][2].ToString();
                                string tape = dtsnum.Rows[k][3].ToString();
                                string clmname = "g" + tgrade + "m" + tterm;
                                //如果新增列名中包含此学期则添加
                                if (stuDt.Columns.Contains(clmname))
                                    stuDt.Rows[j][clmname] = tape;//将此学期评定赋给新表相应行的列中(关键语句)
                            }
                        }
                        //生成新表内容后，改列名，以便生成自动绑定字段标题 原始列名为Tnum,Tclass,Tname按班级、学号排序
                        int lcount = stuDt.Columns.Count;//获取当前表列名数
                        for (int l = 4; l < lcount; l++)//从生成的列名开始
                        {
                            string clmtitle = stuDt.Columns[l].ColumnName;
                            clmtitle = clmtitle.Substring(1);//去掉首字母
                            string[] clms = clmtitle.Split('m');
                            string lgrade = clms[0];
                            string lterm = clms[1];
                            string chineseGrade = Common.WordProcess.ChineseNum(lgrade);
                            string chineseTerm = Common.WordProcess.ChineseTerm(lterm);
                            stuDt.Columns[l].ColumnName = chineseGrade + chineseTerm;
                        }
                        stuDt.Columns[0].ColumnName = "序号";
                        stuDt.Columns[1].ColumnName = "学号";
                        stuDt.Columns[2].ColumnName = "班级";
                        stuDt.Columns[3].ColumnName = "姓名";
                    }
                }
            }
            return stuDt;
        }

        public DataTable GetMerit(int Tyear, string GradeArray)
        {
            DataTable dm = null;
            if (GradeArray != "")
            {
                GradeArray = GradeArray.Substring(1);
                dm = GetOverallMerit(Tyear, GradeArray);
                if (dm != null)
                {
                    int clmcounts = dm.Columns.Count;//获取原始列数 原始列名为Snum,Sclass,Sname
                    string allape = "Totel";//新增综合评定统计列名
                    dm.Columns.Add(allape, typeof(string));
                    int dcount = dm.Rows.Count;
                    for (int i = 0; i < dcount; i++)
                    {
                        string apeset = "";
                        for (int j = 3; j < clmcounts; j++)
                        {
                            apeset = apeset + dm.Rows[i][j].ToString();
                        }
                        string Aset = "";
                        string Pset = "";
                        string Eset = "";
                        if (apeset != "")
                        {
                            int Acount = Common.WordProcess.charCount(apeset, 'A');
                            if (Acount > 0)
                                Aset = Acount.ToString() + "A";
                            int Pcount = Common.WordProcess.charCount(apeset, 'P');
                            if (Pcount > 0)
                                Pset = Pcount.ToString() + "P";
                            int Ecount = Common.WordProcess.charCount(apeset, 'E');
                            if (Ecount > 0)
                                Eset = Ecount.ToString() + "E";
                        }
                        dm.Rows[i][allape] = Aset + Pset + Eset;
                    }
                    dm.Columns[allape].ColumnName = "合计";
                }
            }
            return dm;
        }
        /// <summary>
        ///  获取当前班级在各年级各学期的期末成绩
        /// </summary>
        /// <param name="Tyear"></param>
        /// <param name="Tgrade"></param>
        /// <param name="Tclass"></param>
        /// <param name="Tterm"></param>
        /// <returns></returns>
        public DataSet GetGradeTermScore(int Tyear, int Tgrade, int Tclass, int Tterm)
        {
            return dal.GetGradeTermScore(Tyear, Tgrade, Tclass, Tterm);
        }

        /// <summary>
        /// 导出该年级在某年级时某学期的成绩评定
        /// </summary>
        /// <param name="Tyear"></param>
        /// <param name="Tgrade"></param>
        /// <param name="Tterm"></param>
        public void TotalTermExcel(int Tyear, int Tgrade, int Tterm)
        {
            dal.TotalTermExcel(Tyear, Tgrade, Tterm);
        }
                
        /// <summary>
        /// 初始化新增字段TyearTclassTname
        /// </summary>
        /// <returns></returns>
        public int initTyearTclassTname()
        {
            return dal.initTyearTclassTname();
        }
                
        /// <summary>
        /// 获取入学年度列表
        /// </summary>
        /// <returns></returns>
        public DataTable TyearList()
        {
            return dal.TyearList();
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

