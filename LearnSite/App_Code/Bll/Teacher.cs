using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
using System.Web;
namespace LearnSite.BLL
{
	/// <summary>
	/// 业务逻辑类Teacher 的摘要说明。
	/// </summary>
	public class Teacher
	{
		private readonly LearnSite.DAL.Teacher dal=new LearnSite.DAL.Teacher();
		public Teacher()
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
        /// 记录教师上课的电脑室名称
        /// </summary>
        /// <param name="Hid"></param>
        /// <param name="Hroom"></param>
        public void updateHroom(int Hid, string Hroom)
        {
            dal.updateHroom(Hid, Hroom);
        }
        /// <summary>
        /// 初始化昵称
        /// </summary>
        public void initHnick()
        {
            dal.initHnick();
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Hid)
		{
			return dal.Exists(Hid);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsHname(string Hname)
        {
            return dal.ExistsHname(Hname);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Teacher model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(LearnSite.Model.Teacher model)
		{
			dal.Update(model);
		}
                
        /// <summary>
        /// 给所有老师统计学案数
        /// </summary>
        public void UpdateHcountAll()
        {
            dal.UpdateHcountAll();
        }
        /// <summary>
        /// 给一位老师统计学案数
        /// </summary>
        /// <param name="hid"></param>
        public void UpdateHcount(string hid)
        {
            dal.UpdateHcount(hid);
        }
                
        /// <summary>
        ///根据Hid 更新密码
        /// </summary>
        /// <param name="Hid"></param>
        /// <param name="Hpwd"></param>
        public void UpdatePwd(int Hid, string Hpwd)
        {
            dal.UpdatePwd(Hid, Hpwd);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Hid)
		{			
			dal.Delete(Hid);
		}
                
        /// <summary>
        /// 删除标志一条数据
        /// </summary>
        public int DownTeacher(int Hid)
        {
            return dal.DownTeacher(Hid);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Teacher GetModel(int Hid)
		{
			
			return dal.GetModel(Hid);
		}

               /// <summary>
        /// 根据姓名和密码得到一个教师对象实体，如果不存在返回null
        /// </summary>
        public LearnSite.Model.Teacher GetTeacherModel(string Hname, string Hpwd)
        {
            return dal.GetTeacherModel(Hname, Hpwd);
        }

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public LearnSite.Model.Teacher GetModelByCache(int Hid)
		{
			
			string CacheKey = "TeacherModel-" + Hid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Hid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Teacher)objModel;
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
		public List<LearnSite.Model.Teacher> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Teacher> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Teacher> modelList = new List<LearnSite.Model.Teacher>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Teacher model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.Teacher();
					if(dt.Rows[n]["Hid"].ToString()!="")
					{
						model.Hid=int.Parse(dt.Rows[n]["Hid"].ToString());
					}
					model.Hname=dt.Rows[n]["Hname"].ToString();
					model.Hpwd=dt.Rows[n]["Hpwd"].ToString();
					if(dt.Rows[n]["Hpermiss"].ToString()!="")
					{
						if((dt.Rows[n]["Hpermiss"].ToString()=="1")||(dt.Rows[n]["Hpermiss"].ToString().ToLower()=="true"))
						{
						model.Hpermiss=true;
						}
						else
						{
							model.Hpermiss=false;
						}
					}
					model.Hnote=dt.Rows[n]["Hnote"].ToString();
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
        /// 获取未删除标志教师账号
        /// </summary>
        /// <returns></returns>
        public DataSet GetTeacherList()
        {
            return GetList(" Hdelete=0 ");
        }
        /// <summary>
        /// 获得教师ID和姓名列表
        /// </summary>
        public DataSet GetListHidHname()
        {
            return dal.GetListHidHname();
        }
        /// <summary>
        /// 设置该教师的学生空间当前教学子目录
        /// </summary>
        /// <param name="Hpath"></param>
        /// <param name="Hid"></param>
        public void SetHpath(string Hpath, int Hid)
        {
            dal.SetHpath(Hpath, Hid);
        }
        /// <summary>
        /// 返回该教师的学生空间当前教学子目录，无则返回空字符""
        /// </summary>
        /// <param name="Hid"></param>
        /// <returns></returns>
        public string GetHpath(string Hid)
        {
            return dal.GetHpath(Hid);
        }
                
        /// <summary>
        /// 返回该教师的学生空间当前教学子目录，无则返回空字符""
        /// </summary>
        /// <param name="Hid"></param>
        /// <returns></returns>
        public string GetHpathfroStu(int Sgrade, int Sclass)
        {
            return dal.GetHpathfroStu(Sgrade, Sclass);
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

