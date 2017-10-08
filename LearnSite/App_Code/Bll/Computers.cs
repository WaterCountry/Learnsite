using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Model;
using System.Collections;
namespace LearnSite.BLL
{
	/// <summary>
	/// Computers
	/// </summary>
	public class Computers
	{
		private readonly LearnSite.DAL.Computers dal=new LearnSite.DAL.Computers();
		public Computers()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

        public void initPxy()
        {
            dal.initPxy();
        }
        /// <summary>
        /// 获取电脑室名称
        /// </summary>
        /// <returns></returns>
        public DataTable CmpRoom()
        {
            return dal.CmpRoom();
        }
        /// <summary>
        /// 根据老师获取电脑室的ＩＰ、主机名和坐标
        /// select Pip,Pmachine,Px,Py from Computers
        /// </summary>
        /// <param name="Ph"></param>
        /// <returns></returns>
        public SeatCollect GetSeat(string Pm)
        {
            SeatCollect sct = new SeatCollect();
            DataTable dt = dal.GetSeat(Pm);
            int count = dt.Rows.Count;
            sct.Dt = dt;
            sct.Online = count;
            sct.Column = 8;
            if (count > 6)
            {
                int beginX = Int32.Parse(dt.Rows[0][2].ToString());//获取初始X坐标
                ArrayList arrayX = new ArrayList();
                arrayX.Add(beginX);

                #region    标准化X坐标  同列偏移60内 自动垂直对齐  dt

                for (int i = 0; i < count; i++)
                {
                    int Px = Int32.Parse(dt.Rows[i][2].ToString());//读取某IP的X坐标
                    if (Px > beginX + 60)
                    {
                        beginX = Px;//得到当前X坐标
                        arrayX.Add(beginX);
                    }
                    else
                    {
                        dt.Rows[i][2] = beginX; //如果小于差值则全部设置为当前值
                    }
                }
                #endregion

                DataView dv = dt.DefaultView;
                dv.Sort = "Py  Asc";
                DataTable dt2 = dv.ToTable();//得到按Y坐标排序的表
                int beginY = Int32.Parse(dt2.Rows[0][3].ToString());//获取初始Y坐标
                ArrayList arrayY = new ArrayList();
                arrayY.Add(beginY);

                #region    标准化Y坐标   同行偏移60内 自动水平对齐 dt2

                for (int i = 0; i < count; i++)
                {
                    int Py = Int32.Parse(dt2.Rows[i][3].ToString());//读取某IP的X坐标
                    if (Py > beginY + 60)
                    {
                        beginY = Py;//得到当前Y坐标
                        arrayY.Add(beginY);
                    }
                    else
                    {
                        dt2.Rows[i][3] = beginY; //如果小于差值则全部设置为当前值
                    }
                }
                #endregion

                #region 查询标准位置，在表中存在不存在，不存在则添加空位
                int empty = 0;
                for (int i = 0; i < arrayX.Count; i++)
                {
                    int x = Int32.Parse(arrayX[i].ToString());
                    for (int j = 0; j < arrayY.Count; j++)
                    {
                        int y = Int32.Parse(arrayY[j].ToString());
                        //查询标准位置，在表中存在不存在，不存在则添加记录
                        if (dt2.Select("Px=" + x + " and Py=" + y).Length < 1)
                        {
                            empty++;
                            DataRow dr = dt2.NewRow();//Pip,Pmachine,Px,Py 
                            dr[0] = "";
                            dr[1] = "";
                            dr[2] = x;
                            dr[3] = y;
                            dt2.Rows.Add(dr);
                        }
                    }
                }
                #endregion

                DataView dvok = dt2.DefaultView;
                dvok.Sort = "Px Asc , Py Asc";

                DataTable dt3 = dvok.ToTable();//得到最终标准格式位置表（带空位）
                dt.Dispose();
                dt2.Dispose();
                sct.Column = arrayX.Count;
                sct.Dt = dt3;
            }
            return sct;
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Pid)
		{
			return dal.Exists(Pid);
		}
                
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsIp(string Pip)
        {
            return dal.ExistsIp(Pip);
        }                
        /// <summary>
        /// 返回主机名
        /// </summary>
        public string GetmachineByIp(string Pip)
        {
            return dal.GetmachineByIp(Pip);
        }
        /// <summary>
        /// 是否有未绑定的主机名IP
        /// </summary>
        /// <returns></returns>
        public string ExistPlock(string Pip)
        {
            return dal.ExistPlock(Pip);
        }
		/// <summary>
        /// 增加一条数据Pip,Pmachine,Plock,Pdate
		/// </summary>
		public int  Add(LearnSite.Model.Computers model)
		{
			return dal.Add(model);
		}
        /// <summary>
        /// 增加一条数据Pip,Pmachine,Plock,Pdate,Px,Py,Pm
        /// </summary>
        public int AddModel(LearnSite.Model.Computers model)
        {
            return dal.AddModel(model);
        }
        /// <summary>
        /// 将表中Plock值取反，更新
        /// </summary>
        /// <param name="Pid"></param>
        public void UpLock(int Pid)
        {
            dal.UpLock(Pid);
        }
        /// <summary>
        /// 将表中Plock值为0，即解锁
        /// </summary>
        public void UnLockAll()
        {
            dal.UnLockAll();
        }
                
        /// <summary>
        /// 将表中Plock值为0，即解锁
        /// </summary>
        public void OnLockAll()
        {
            dal.OnLockAll();
        }
        /// <summary>
        /// 根据IP更新电脑室名和电脑坐标
        /// </summary>
        public bool UpdateIpPxPy(string Pip, int Px, int Py, string Pm)
        {
            return dal.UpdateIpPxPy(Pip, Px, Py, Pm);
        }
        /// <summary>
        /// 根据Pid更新一条数据Pmachine,Plock,Pdate
        /// </summary>
        public bool UpdateMachine(LearnSite.Model.Computers model)
        {
            return UpdateMachine(model);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.Computers model)
		{
			return dal.Update(model);
		}                
        /// <summary>
        /// 根据IP更新主机名，不存在则添加
        /// </summary>
        public bool UpdateIp(string Pip, string Pmachine)
        {
            bool isok = false;
            if (!ExistsIp(Pip))
            {
                Model.Computers cmodel = new Model.Computers();
                cmodel.Pdate = DateTime.Now;
                cmodel.Pip = Pip;
                cmodel.Plock = true;
                cmodel.Pmachine = Pmachine;
                BLL.Computers cbll = new Computers();
                if (cbll.Add(cmodel) > 0)
                {
                    isok = true;
                }
            }
            else
            {
                isok = dal.UpdateIp(Pip, Pmachine);
            }
            return isok;
        }
                        
        /// <summary>
        /// 根据Pid更新主机名并锁定
        /// </summary>
        public bool UpdateByPid(int Pid, string Pmachine)
        {
            return dal.UpdateByPid(Pid, Pmachine);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Pid)
		{
			
			return dal.Delete(Pid);
		}
                
        /// <summary>
        /// 删除所有数据
        /// </summary>
        public bool DeleteAll()
        {
            return dal.DeleteAll();
        }
                
        /// <summary>
        /// 删除该日期之前的记录
        /// </summary>
        /// <param name="pdate"></param>
        /// <returns></returns>
        public bool DeleteThis(string pdate)
        {
            return dal.DeleteThis(pdate);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Pidlist )
		{
			return dal.DeleteList(Pidlist );
		}
                /// <summary>
        /// 根据Pip得到一个对象实体
        /// </summary>
        public LearnSite.Model.Computers GetModelByIp(string Pip)
        {
            return dal.GetModelByIp(Pip);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Computers GetModel(int Pid)
		{
			
			return dal.GetModel(Pid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.Computers GetModelByCache(int Pid)
		{
			
			string CacheKey = "ComputersModel-" + Pid;
            object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Pid);
					if (objModel != null)
					{
                        int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Computers)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// 获得数据列表，条件排序
        /// </summary>
        public DataSet GetListOrderBy(string str)
        {
            string strOrder = "";
            switch (str)
            {
                case "1":
                    strOrder=" order by Pip asc";
                    break;
                case "2":
                    strOrder = " order by Pmachine asc";
                    break;
                case "3":
                    strOrder = " order by Pdate asc";
                    break;
            }
            return dal.GetListOrder(strOrder);
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
		public List<LearnSite.Model.Computers> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Computers> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Computers> modelList = new List<LearnSite.Model.Computers>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Computers model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.Computers();
					if(dt.Rows[n]["Pid"].ToString()!="")
					{
						model.Pid=int.Parse(dt.Rows[n]["Pid"].ToString());
					}
					model.Pip=dt.Rows[n]["Pip"].ToString();
					model.Pmachine=dt.Rows[n]["Pmachine"].ToString();
					if(dt.Rows[n]["Plock"].ToString()!="")
					{
						if((dt.Rows[n]["Plock"].ToString()=="1")||(dt.Rows[n]["Plock"].ToString().ToLower()=="true"))
						{
						model.Plock=true;
						}
						else
						{
							model.Plock=false;
						}
					}
					if(dt.Rows[n]["Pdate"].ToString()!="")
					{
						model.Pdate=DateTime.Parse(dt.Rows[n]["Pdate"].ToString());
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
        /// 获取IP与主机名对应表
        /// </summary>
        /// <returns></returns>
        public DataTable GetPipPmachine()
        {
            return dal.GetPipPmachine();
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

