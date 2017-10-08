using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
using System.Web;
namespace LearnSite.BLL
{
	/// <summary>
	/// Autonomic
	/// </summary>
	public partial class Autonomic
	{
		private readonly LearnSite.DAL.Autonomic dal=new LearnSite.DAL.Autonomic();
		public Autonomic()
		{}
		#region  BasicMethod

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
        public bool ExistsCheck(int Aid)
        {
            return dal.ExistsCheck(Aid);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public int Exists(int Asid, int Afid)
        {
            return dal.Exists(Asid, Afid);
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Aid)
		{
			return dal.Exists(Aid);
		}
        /// <summary>
        /// 增加一条数据作品记录
        /// Asid,Anum,Aname,Ayid,Afid,Atype,Afilename,Aurl,Alength,Adate,Aip,Ayear,Agrade,Aclass,Aterm,Aoffice
        /// </summary>
        public int AddAutonomic(LearnSite.Model.Autonomic model)
        {
            return dal.AddAutonomic(model);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Autonomic model)
		{
			return dal.Add(model);
		}
        /// <summary>
        /// 更新一条数据Atype Afilename Aurl Alength Adate Aflash Aid 
        /// </summary>
        public bool UpdateAutonomic(string Atype, string Afilename, string Aurl, int Alength, DateTime Adate, bool Aflash, int Aid)
        {
            return dal.UpdateAutonomic(Atype,Afilename,Aurl,Alength,Adate,Aflash,Aid);
        }
        /// <summary>
        /// 更新一条数据 Ascore,Aself
        /// </summary>
        public bool UpdateScoreSelf(int Afid, string Anum, int Ascore, string Aself)
        {
            return dal.UpdateScoreSelf(Afid, Anum, Ascore, Aself);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.Autonomic model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Aid)
		{			
			return dal.Delete(Aid);
		} 
        /// <summary>
        /// 删除一条数据 
        /// </summary>
        public bool Delbynum(int Afid, string Anum)
        {
            return dal.Delbynum(Afid, Anum);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="Asid"></param>
        /// <param name="Afid"></param>
        /// <returns></returns>
        public LearnSite.Model.Autonomic GetModel(int Asid, int Afid)
        {
            return dal.GetModel(Asid, Afid);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Autonomic GetModel(int Aid)
		{
			
			return dal.GetModel(Aid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.Autonomic GetModelByCache(int Aid)
		{
			
			string CacheKey = "AutonomicModel-" + Aid;
			object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Aid);
					if (objModel != null)
					{
						int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
						LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Autonomic)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        /// <summary>
        /// 获得有作品提交的分类项数据列表，按序号和编号排序
        /// </summary>
        public DataTable GetListCategory()
        {
            return dal.GetListCategory();
        }
        /// <summary>
        /// 根据Yid分类编号，获得num条数据列表
        /// Aid,Aname,Atype,Aurl,Adate,Agood,Ftitle
        /// </summary>
        public string GetListTopHtml(int Ayid, int num, string css,int len)
        {
            string html = "<ul>\r\n";
            DataTable dt = dal.GetListTop(Ayid, num);
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    string aname = HttpUtility.UrlDecode(dt.Rows[i][1].ToString());
                    string atype = dt.Rows[i][2].ToString();
                    string typeimg = LearnSite.Common.WordProcess.GetFileTypeImg(atype);
                    string aurl = dt.Rows[i][3].ToString();
                    string aurlfat ="../Plugins/download.aspx?Id=" +LearnSite.Common.EnDeCode.Encrypt(aurl+"&True","ls");
                    DateTime adate = ((DateTime)dt.Rows[i][4]);
                    bool agood = (bool)(dt.Rows[i][5]);
                    string agoodimg = Common.WordProcess.GetGoodImg(agood);
                    string ftitle = dt.Rows[i][6].ToString();
                    string ftitlefat = Common.WordProcess.CnCutString(ftitle, len, "");
                    string tip = "";
                    if (ftitle.Length > ftitlefat.Length)
                    {
                        tip = " title='" + ftitle + "'";
                        ftitlefat += "...";
                    }
                    string adatefat=adate.ToString("yyyy-MM-dd");                    
                    string imgurl = Common.WordProcess.GetDaysGoneImg(adate);
                    string msg = "<li class='" + css + "'>&nbsp;" + typeimg + "<a href='" + aurlfat + "' " + tip + " target='_blank'>" + ftitlefat + "</a>" + imgurl + agoodimg+ "&nbsp;" + aname  + "&nbsp;" + adatefat + "</li>";
                    html += msg;
                }
            }
            else
            {
                string msg = "&nbsp;";
                html += msg;
            }
            html += "</ul>";
            return html;
        }
        /// <summary>
        /// 根据Sid学生编号，获得所有作品数据列表
        /// Aid,Aurl,Ftitle
        /// </summary>
        public DataTable GetMyList(int Asid)
        {
            return dal.GetMyList(Asid);
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
		public List<LearnSite.Model.Autonomic> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Autonomic> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Autonomic> modelList = new List<LearnSite.Model.Autonomic>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Autonomic model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
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
        /// 获取自学作品的学分和评语
        /// </summary>
        /// <param name="Afid"></param>
        /// <param name="Anum"></param>
        /// <returns></returns>
        public string[] GetScoreSelf(int Afid, string Anum)
        {
            return dal.GetScoreSelf(Afid, Anum);
        }
        /// <summary>
        /// 获取展示用的学生未评自学作品列表
        /// </summary>
        /// <param name="Afid"></param>
        /// <returns></returns>
        public DataTable GetListCircleNomic(int Afid)
        {
            return dal.GetListCircleNomic(Afid);
        }
       /// <summary>
        /// 获得数据列表获取最优秀的20条作品
        /// </summary>
        public DataSet GetListGoodByYid(int Yid)
        {
            return dal.GetListGoodByYid(Yid);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetListByYid(int Yid)
        {
            return dal.GetListByYid(Yid).Tables[0];
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
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

