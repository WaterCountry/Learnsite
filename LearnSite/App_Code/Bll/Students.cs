using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Students 的摘要说明。
	/// </summary>
	public class Students
	{
		private readonly LearnSite.DAL.Students dal=new LearnSite.DAL.Students();
		public Students()
		{}
		#region  成员方法
        
        /// <summary>
        /// 初始化作业加分总计
        /// </summary>
        public void initSwdscore()
        {
            dal.initSwdscore();
        }
               
        /// <summary>
        /// 初始化中文拼音分数总计
        /// </summary>
        public void initSchinese()
        {
            dal.initSchinese();
        }
        /// <summary>
        /// 初始化表单分数总计
        /// </summary>
        public void initStxtform()
        {
            dal.initStxtform();
        }
		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

                /// <summary>
        /// 得到最大值学号Snum（先判断班级，再年级，再全校）
        /// </summary>
        public long GetMaxSnum(int Sgrade,int Sclass)
        {
            return dal.GetMaxSnum(Sgrade,Sclass);
        }
                
        /// <summary>
        /// 是否存在该学号
        /// </summary>
        public bool ExistsSnum(string Snum)
        {
            return dal.ExistsSnum(Snum);
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
		public int  Add(LearnSite.Model.Students model)
		{
			return dal.Add(model);
		}

                /// <summary>
        /// 增加一位学生
        /// </summary>
        public int AddStudent(LearnSite.Model.Students model)
        {
            return dal.AddStudent(model);
        }
        public void UpdateMyClassPwd(int Sgrade, int Sclass)
        {
            dal.UpdateMyClassPwd(Sgrade, Sclass);
        }
        /// <summary>
        /// 更新该学号学生的密码
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Spwd"></param>
        public void UpdatePwd(string Snum, string Spwd)
        {
            dal.UpdatePwd(Snum, Spwd);
        }
        /// <summary>
        /// 更新该Sid学生的密码
        /// </summary>
        /// <param name="Sid"></param>
        /// <param name="Spwd"></param>
        public void UpdateSidPwd(string Sid, string Spwd)
        {
            dal.UpdateSidPwd(Sid, Spwd);
        }        
        /// <summary>
        /// 更新该学号学生的性别
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Sex"></param>
        public void UpdateSex(string Snum, string Sex)
        {
            dal.UpdateSex(Snum, Sex);
        }                
        /// <summary>
        /// 对该生换班
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sname"></param>
        /// <returns></returns>
        public bool UpdateDivide(int Sgrade, int Sclass, string Sname)
        {
            return dal.UpdateDivide(Sgrade, Sclass, Sname);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Students model)
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
        /// 根据年级和班级 随机得到该班某个学生一个对象实体
        /// </summary>
        public LearnSite.Model.Students GetRndModel(int Sgrade, int Sclass)
        {
            return dal.GetRndModel(Sgrade, Sclass);
        }
        /// <summary>
        /// 根据学号和密码 得到学生一个对象实体
        /// </summary>
        public LearnSite.Model.Students GetStudentModel(string Snum, string Spwd)
        {
          return  dal.GetStudentModel(Snum, Spwd);
        }
                
        /// <summary>
        /// 根据学号，得到一个对象实体
        /// </summary>
        public LearnSite.Model.Students SnumGetModel(string Snum)
        {
            return dal.SnumGetModel(Snum);
        }
                
        /// <summary>
        /// 根据自动编号返回姓名
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public string GetSnameBySid(int Sid)
        {
            return dal.GetSnameBySid(Sid);
        }
                
        /// <summary>
        /// 根据学号返回姓名
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public string GetSnameBySnum(string Snum)
        {
            return dal.GetSnameBySnum(Snum);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Students GetModel(int Sid)
		{
			
			return dal.GetModel(Sid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Students GetModelByCache(int Sid)
		{
			
			string CacheKey = "StudentsModel-" + Sid;
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
			return (LearnSite.Model.Students)objModel;
		}

        /// <summary>
        /// 获得作品总评数据列表
		/// </summary>
        public DataSet GetListTerm(int Sgrade, int Sclass)
        {
            return dal.GetListTerm(Sgrade, Sclass);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
                /// <summary>
        /// 获得条件数据列表
        /// </summary>
        public DataSet GetSqlList(string strSql)
        {
           return dal.GetSqlList(strSql);
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
		public List<LearnSite.Model.Students> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Students> DataTableToList(DataTable dt)
		{
            List<LearnSite.Model.Students> modelList = new List<LearnSite.Model.Students>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LearnSite.Model.Students model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new LearnSite.Model.Students();
                    if (dt.Rows[n]["Sid"].ToString() != "")
                    {
                        model.Sid = int.Parse(dt.Rows[n]["Sid"].ToString());
                    }
                    model.Snum = dt.Rows[n]["Snum"].ToString();
                    if (dt.Rows[n]["Syear"].ToString() != "")
                    {
                        model.Syear = int.Parse(dt.Rows[n]["Syear"].ToString());
                    }
                    if (dt.Rows[n]["Sgrade"].ToString() != "")
                    {
                        model.Sgrade = int.Parse(dt.Rows[n]["Sgrade"].ToString());
                    }
                    if (dt.Rows[n]["Sclass"].ToString() != "")
                    {
                        model.Sclass = int.Parse(dt.Rows[n]["Sclass"].ToString());
                    }
                    model.Sname = dt.Rows[n]["Sname"].ToString();
                    model.Spwd = dt.Rows[n]["Spwd"].ToString();
                    model.Sex = dt.Rows[n]["Sex"].ToString();
                    model.Saddress = dt.Rows[n]["Saddress"].ToString();
                    model.Sphone = dt.Rows[n]["Sphone"].ToString();
                    model.Sparents = dt.Rows[n]["Sparents"].ToString();
                    model.Sheadtheacher = dt.Rows[n]["Sheadtheacher"].ToString();
                    if (dt.Rows[n]["Sscore"].ToString() != "")
                    {
                        model.Sscore = int.Parse(dt.Rows[n]["Sscore"].ToString());
                    }
                    if (dt.Rows[n]["Squiz"].ToString() != "")
                    {
                        model.Squiz = int.Parse(dt.Rows[n]["Squiz"].ToString());
                    }
                    if (dt.Rows[n]["Sattitude"].ToString() != "")
                    {
                        model.Sattitude = int.Parse(dt.Rows[n]["Sattitude"].ToString());
                    }
                    model.Sape = dt.Rows[n]["Sape"].ToString();
                    if (dt.Rows[n]["Swscore"].ToString() != "")
                    {
                        model.Swscore = int.Parse(dt.Rows[n]["Swscore"].ToString());
                    }
                    if (dt.Rows[n]["Stscore"].ToString() != "")
                    {
                        model.Stscore = int.Parse(dt.Rows[n]["Stscore"].ToString());
                    }
                    if (dt.Rows[n]["Sallscore"].ToString() != "")
                    {
                        model.Sallscore = int.Parse(dt.Rows[n]["Sallscore"].ToString());
                    }
                    if (dt.Rows[n]["Spscore"].ToString() != "")
                    {
                        model.Spscore = int.Parse(dt.Rows[n]["Spscore"].ToString());
                    }
                    if (dt.Rows[n]["Sgroup"].ToString() != "")
                    {
                        model.Sgroup = int.Parse(dt.Rows[n]["Sgroup"].ToString());
                    }
                    if (dt.Rows[n]["Sleader"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Sleader"].ToString() == "1") || (dt.Rows[n]["Sleader"].ToString().ToLower() == "true"))
                        {
                            model.Sleader = true;
                        }
                        else
                        {
                            model.Sleader = false;
                        }
                    }
                    if (dt.Rows[n]["Svote"].ToString() != "")
                    {
                        model.Svote = int.Parse(dt.Rows[n]["Svote"].ToString());
                    }
                    if (dt.Rows[n]["Sgscore"].ToString() != "")
                    {
                        model.Sgscore = int.Parse(dt.Rows[n]["Sgscore"].ToString());
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
        /// 获得当前班级学生表现数据列表
        /// </summary>
        /// <param name="Syear"></param>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataTable GetListSattitude(int Syear, int Sgrade, int Sclass)
        {
            return dal.GetListSattitude(Syear, Sgrade, Sclass);
        }
        /// <summary>
        /// 获得单个学生数据
        /// </summary>
        public DataSet GetOneStudent(int Sid)
        {
            return dal.GetOneStudent(Sid);
        }

        /// <summary>
        /// 批量更新所有学期总积分
        /// </summary>
        public void TeamScores()
        {
            dal.TeamScores();
        }
        public void TotalSgscore(string gstudnets, int Cobj, int Cterm)
        {
            dal.TotalSgscore(gstudnets, Cobj, Cterm);
        }
        /// <summary>
        /// 批量更新所教班级当前学期小组合作分
        /// </summary>
        public void ThisTeamGroupScores()
        {
            dal.ThisTeamGroupScores();
        }      
        /// <summary>
        /// 批量更新所教班级当前学期作品总积分和表现总积分、调查测验分//ISNULL  COALESCE
        /// </summary>
        public void ThisTeamScoresNew()
        {
            dal.ThisTeamScoresNew();
        }
        /// <summary>
        /// 批量更新该班级当前学期作品总积分和表现总积分（批量group方法）
        /// </summary>
        public void ThisClassTeamScoresNew(int Sgrade, int Sclass)
        {
            dal.ThisClassTeamScoresNew(Sgrade, Sclass);
        }

        /// <summary>
        /// 终结性评定
        /// </summary>
        /// <param name="perA"></param>
        /// <param name="perE"></param>
        public void TermAPE(int perA, int perE)
        {
            dal.TermAPE(perA,perE);
        }

              /// <summary>
        /// 最终成绩评定导出Excel表
        /// </summary>
        public void TermExcel()
        {
            dal.TermExcel();
        }
                /// <summary>
        /// 学生表基本数据导出Excel
        /// </summary>
        public void StudentsToExcel()
        {
            dal.StudentsToExcel();
        }
                /// <summary>
        /// 获得学生管理页数据列表
        /// </summary>
        public DataSet GetListStudents(int Sgrade, int Sclass)
        {
            return dal.GetListStudents(Sgrade, Sclass);
        }        
        /// <summary>
        /// 获得本班学生学号和姓名数据列表
        /// </summary>
        public DataSet GetStudentsSnumSname(int Sgrade, int Sclass)
        {
            return dal.GetStudentsSnumSname(Sgrade, Sclass);
        }
        /// <summary>
        /// 获得本年级所有学生列表Snum,Sclass,Sname
        /// </summary>
        public DataTable GetStudentsSnumSname(int Sgrade)
        {
            return dal.GetStudentsSnumSname(Sgrade);
        }
        /// <summary>
        /// 获得全体学生的学年列表
        /// </summary>
        public DataSet GetAllYears()
        {
            return dal.GetAllYears();
        }

                /// <summary>
        /// 从学生表得到该年份的记录数
        /// </summary>
        /// <param name="Syear"></param>
        /// <returns></returns>
        public int FindCount(int Syear)
        {
            return dal.FindCount(Syear);
        }

        /// <summary>
        /// 学生表所有学生年级都升一级，并删除班级表中不存在班级的学生
        /// </summary>
        public void Upgrade()
        {
            dal.Upgrade();
        }

        /// <summary>
        /// 获得该年级的入学年份
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public string GetYear(int Sgrade)
        {
            return dal.GetYear(Sgrade);
        }
        /// <summary>
        /// 获得该年级的入学年份
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public int GetYear(int Sgrade, int Sclass)
        {
            return dal.GetYear(Sgrade, Sclass);
        }

        /// <summary>
        /// 根据年级、班级获得姓名和学号
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet GetNameNum(int Sgrade, int Sclass)
        {
            return dal.GetNameNum(Sgrade, Sclass);
        }

        /// <summary>
        /// 显示本年级积分最高的50条记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="GridViewscore"></param>
        public  DataTable  ShowTopScore(int Sgrade)
        {
            return dal.ShowTopScore(Sgrade).Tables[0];
        }
        
        /// <summary>
        /// 显示本班级所有积分记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet ShowMyclassScore(int Sgrade, int Sclass)
        {
            return dal.ShowMyclassScore(Sgrade, Sclass);
        }
                
        /// <summary>
        /// 查询学生表的记录总数
        /// </summary>
        /// <returns></returns>
        public int GetCounts()
        {
          return  dal.GetCounts();
        }

                /// <summary>
        /// 更新该学号学生的测验成绩
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Squiz"></param>
        public void SetSquiz(int Rsid, int Squiz)
        {
            dal.SetSquiz(Rsid, Squiz);
        }
               
        /// <summary>
        /// 将所有学生的测验统计成绩更新为其测验最高分
        /// </summary>
        public void UpdateBestSquiz()
        {
            dal.UpdateBestSquiz();
        }
        /// <summary>
        /// 获得本年级测验成绩最高的50条记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public DataSet TopGradeQuiz(int Sgrade)
        {
            return dal.TopGradeQuiz(Sgrade);
        }
        /// <summary>
        /// 获得本班级测验成绩所有记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet TopClassQuiz(int Sgrade, int Sclass)
        {
            return dal.TopClassQuiz(Sgrade, Sclass);
        }

                /// <summary>
        /// 获得我的测验平均成绩
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public string MySquiz(string Snum)
        {
            return dal.MySquiz(Snum);
        }
        /// <summary>
        /// 根据学号和班级密码，判断该学号是否存在
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Rpwd"></param>
        /// <returns></returns>
        public bool ExistsLogin(string Snum, string Rpwd)
        {
            return dal.ExistsLogin(Snum, Rpwd);
        }       
        /// <summary>
        /// 根据学号和个人密码，判断该学号是否存在
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Spwd"></param>
        /// <returns></returns>
        public bool ExistsLoginSelf(string Snum, string Spwd)
        {
            return dal.ExistsLoginSelf(Snum, Spwd);
        }
                
        /// <summary>
        /// 更新学生表中网页制作成绩
        /// </summary>
        public void UpdateWebScore()
        {
            dal.UpdateWebScore();
        }
                
        /// <summary>
        /// 登录账号教师所教班级按设定百分比计算总分
        /// </summary>
        /// <param name="persscore"></param>
        /// <param name="persquiz"></param>
        /// <param name="perswscore"></param>
        /// <param name="perstscore"></param>
        public void UpdateAllScore(int persscore, int persquiz, int perswscore, int perstscore, int perattitude, int persurvey)
        {
            dal.UpdateAllScore(persscore, persquiz, perswscore, perstscore,perattitude,persurvey);
        }        
        /// <summary>
        /// 更新学生表的打字成绩
        /// </summary>
        public void UpdateStscore()
        {
            dal.UpdateStscore();
        }

        /// <summary>
        /// 根据学号，更新班级
        /// </summary>
        /// <param name="Sclass"></param>
        /// <param name="Snum"></param>
        public void UpdateStuclass(int Sclass, string Snum)
        {
            dal.UpdateStuclass(Sclass, Snum);
        }
                
        /// <summary>
        /// 根据教师自动编号Hid，返回所教班级的Syear,Sclass数据集
        /// </summary>
        /// <param name="Rhid"></param>
        /// <returns></returns>
        public DataSet TeacherSyearSclass(int hid)
        {
            return dal.TeacherSyearSclass(hid);
        }                
        /// <summary>
        /// 将所教班级的学生密码如果为原初始化密码则更新转换为姓名拼音缩写（如果转换的不是字母或数字，则不更新密码）
        /// 返回所教学生总数和转换成功的总数
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="Spwd"></param>
        public string SpwdToSpell(int hid, string Spwd)
        {
            return dal.SpwdToSpell(hid, Spwd);
        }
               
        /// <summary>
        /// 初始化Sleader值，数据库升级时用
        /// </summary>
        public void InitSleader()
        {
            dal.InitSleader();
        }
                
        /// <summary>
        /// 任命或卸任组长
        /// </summary>
        /// <param name="Snum"></param>
        public void ChangeSleader(int Sid)
        {
            dal.ChangeSleader(Sid);
        }       
        /// <summary>
        /// 获取班级所有小组队长信息
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataSet ClassGroup(int Sgrade, int Sclass)
        {
            return dal.ClassGroup(Sgrade, Sclass);
        }        
        /// <summary>
        /// 获取本小组成员名单
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sgroup"></param>
        /// <returns></returns>
        public string GroupMember(int Sgrade, int Sclass, int Sgroup)
        {
            return dal.GroupMember(Sgrade, Sclass, Sgroup);
        }        
        /// <summary>
        /// 更新该学号的小组号
        /// 如果原组长已卸任则加入新小组
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Sgroup"></param>
        public void AddThisGroup(string Snum, int Sgroup)
        {
            dal.AddThisGroup(Snum, Sgroup);
        }
        public void AddThisGroup(int Sid, int Sgroup)
        {
            dal.AddThisGroup(Sid, Sgroup);
        }
        /// <summary>
        /// 获取本班本小组人数
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sgroup"></param>
        /// <returns></returns>
        public int GetGroupCount(int Sgrade, int Sclass, int Sgroup)
        {
            return dal.GetGroupCount(Sgrade, Sclass, Sgroup);
        }
                
        /// <summary>
        ///  将该学号非组长同学退组
        /// </summary>
        /// <param name="Snum"></param>
        public void QuitThitGroup(string Snum)
        {
            dal.QuitThitGroup(Snum);
        }   
           /// <summary>
        /// 将该学号非组长同学退组
        /// </summary>
        /// <param name="Sid"></param>
        public void QuitThitGroup(int Sid)
        {
            dal.QuitThitGroup(Sid);
        }
        /// <summary>
        /// 是否组长
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public bool IsLeader(string Snum)
        {
            return dal.IsLeader(Snum);
        }
                
        /// <summary>
        /// 是否组长
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public bool IsLeaderSid(int Sid)
        {
            return dal.IsLeaderSid(Sid);
        }
                
        /// <summary>
        /// 获取小组名称
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public string GetSgtitle(int Sgroup)
        {
            return dal.GetSgtitle(Sgroup);
        }
                
        /// <summary>
        /// 更新小组名称
        /// </summary>
        /// <param name="Sid"></param>
        /// <param name="Sgtitle"></param>
        /// <returns></returns>
        public int UpdateSgtitle(int Sid, string Sgtitle)
        {
            return dal.UpdateSgtitle(Sid, Sgtitle);
        }
        /// <summary>
        /// 根据学号获取同组成员的所有学号
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public string GroupSnum(string Snum)
        {
            return dal.GroupSnum(Snum);
        }
                
        /// <summary>
        /// 获取该学号年级
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public int GetSgrade(string Snum)
        {
            return dal.GetSgrade(Snum);
        }
                        
        /// <summary>
        /// 根据学号，修改姓名
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Sname"></param>
        /// <returns></returns>
        public int ChangeSname(string Snum, string Sname)
        {
            return dal.ChangeSname(Snum, Sname);
        }
        /// <summary>
        /// 初始化指法成绩字段
        /// </summary>
        /// <returns></returns>
        public int InitSfscore()
        {
            return dal.InitSfscore();
        }
        /// <summary>
        /// 初始化课堂调查测验成绩
        /// </summary>
        /// <returns></returns>
        public int InitSvscore()
        {
            return dal.InitSvscore();
        }
        /// <summary>
        /// 更新指法成绩
        /// </summary>
        /// <returns></returns>
        public int UpdateSfscore()
        {
            return dal.UpdateSfscore();
        }
                
        /// <summary>
        /// 更新中文拼音成绩
        /// </summary>
        /// <returns></returns>
        public int UpdateSchinese()
        {
            return dal.UpdateSchinese();
        }
        /// <summary>
        /// 统计前，先清空学生成绩
        /// </summary>
        public void ClearAllScores()
        {
            dal.ClearAllScores();
        }
        /// <summary>
        /// 返回队长姓名
        /// </summary>
        /// <param name="Snum"></param>
        /// <returns></returns>
        public string GetLeader(int Sid)
        {
            return dal.GetLeader(Sid);
        }
        public string GetLeaderByGroup(int Sgroup)
        {
            return dal.GetLeaderByGroup(Sgroup);
        }
        /// <summary>
        /// 解除分组
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public int NoGroup(int Sgrade, int Sclass)
        {
            return dal.NoGroup(Sgrade, Sclass);
        }
                
        /// <summary>
        /// 获取当前班级学号集合用,分隔
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowClassSnums(int Sgrade, int Sclass)
        {
            return dal.ShowClassSnums(Sgrade, Sclass);
        }
                
        /// <summary>
        /// 获取当前班级学生编号集合用,分隔
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public string ShowClassSids(int Sgrade, int Sclass)
        {
            return dal.ShowClassSids(Sgrade, Sclass);
        }                
        /// <summary>
        /// 获取当前年级学生编号集合用,分隔
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <returns></returns>
        public string ShowGradeSids(int Sgrade)
        {
            return dal.ShowGradeSids(Sgrade);
        }
        /// <summary>
        /// 获取该班级的人数
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public int CountClassMate(int Sgrade, int Sclass)
        {
            return dal.CountClassMate(Sgrade, Sclass);
        }
        /// <summary>
        /// 清除该班级的所有学生记录
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public int DeleteClassMate(int Sgrade, int Sclass)
        {
            return dal.DeleteClassMate(Sgrade, Sclass);
        }
                
        /// <summary>
        /// 汇总表小组统计
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="dttotal"></param>
        /// <returns></returns>
        public DataTable groupscores(int Sgrade, int Sclass, DataTable dttotal, int Gcid)
        {
            return dal.groupscores(Sgrade, Sclass, dttotal,Gcid);
        }
                
        /// <summary>
        /// 获取未参组班级内学生
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <returns></returns>
        public DataTable NoGroupStudents(int Sgrade, int Sclass, string sort)
        {
            return dal.NoGroupStudents(Sgrade, Sclass, sort);
        }                
        /// <summary>
        /// 获取本小组成员
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sgroup"></param>
        /// <returns></returns>
        public DataTable GroupMembers(int Sgrade, int Sclass, int Sgroup)
        {
            return dal.GroupMembers(Sgrade, Sclass, Sgroup);
        }                   
        /// <summary>
        /// 获取本小组网盘成员（包括组长）Sid,Snum,Sname
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sgroup"></param>
        /// <returns></returns>
        public DataTable GroupDiskMembers(int Sgrade, int Sclass, int Sgroup)
        {
            return dal.GroupDiskMembers(Sgrade, Sclass, Sgroup);
        }
        /// <summary>
        /// 获取本组内的个人作品平均分
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sgroup"></param>
        /// <returns></returns>
        public string MyGroupSscores(int Sgrade, int Sclass, int Sgroup)
        {
            return dal.MyGroupSscores(Sgrade, Sclass, Sgroup);
        }
                
        /// <summary>
        /// 初始化小组名称为组长姓名（条件：小组名称为空）
        /// </summary>
        /// <returns></returns>
        public int InitSgtitle()
        {
            return dal.InitSgtitle();
        }
                
        /// <summary>
        /// 根据Sid获取组号
        /// </summary>
        /// <param name="Sid"></param>
        /// <returns></returns>
        public int GetSgroup(int Sid)
        {
            return dal.GetSgroup(Sid);
        }
        public void UpdateKaoxu(string kaoxu, string Sname)
        {
            dal.UpdateKaoxu(kaoxu, Sname);
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

