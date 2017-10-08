using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
namespace LearnSite.Common
{
    /// <summary>
    ///App 的摘要说明
    /// </summary>
    public class App
    {
        public App()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 返回全局变量数目和初始化值
        /// </summary>
        /// <returns></returns>
        public static string AppCounts()
        {
            string initstr = "";
            ArrayList list = HttpContext.Current.Application.Get("LearnSite_User_List") as ArrayList;
            ArrayList kick = HttpContext.Current.Application.Get("LearnSite_User_kick") as ArrayList;

            if (list != null)
            {
                int counts = list.Count;
                if (counts > 0)
                {
                    initstr = initstr + " 在线:" + list.Count.ToString();
                }
            }
            if (kick != null)
            {
                int kcounts = kick.Count;
                if (kcounts > 0)
                {
                    string tempstr = "";
                    foreach (string ch in kick)
                    {
                        tempstr = tempstr + ch + ",";
                    }
                    initstr = initstr + " 踢除:" + tempstr.TrimEnd(',');
                }
            }
            return initstr;
        }

        public static bool IsLogin(string strUserId)
        {
            bool rb = false;
            HttpContext.Current.Application.Lock();
            ArrayList list = HttpContext.Current.Application.Get("LearnSite_User_List") as ArrayList;
            if (list != null)
            {
                if (list.IndexOf(strUserId) > -1)
                    rb = true;//如果存在，则返回真
            }
            HttpContext.Current.Application.UnLock();
            return rb;
        }
        public static bool Iskick(string strUserId)
        {
            bool rb = false;
            HttpContext.Current.Application.Lock();
            ArrayList kick = HttpContext.Current.Application.Get("LearnSite_User_kick") as ArrayList;
            if (kick != null)
            {
                if (kick.IndexOf(strUserId) > -1)
                    rb = true;//如果存在，则返回真
            }
            HttpContext.Current.Application.UnLock();
            return rb;
        }
        public static void AppUserAdd(string strUserId)
        {
            HttpContext.Current.Application.Lock();
            ArrayList list = HttpContext.Current.Application.Get("LearnSite_User_List") as ArrayList;
            if (list == null)
            {
                list = new ArrayList();
                list.Add(strUserId);
            }
            else
            {
                if (list.IndexOf(strUserId) < 0)
                    list.Add(strUserId);//如果不存在，则添加
            }
            HttpContext.Current.Application.Add("LearnSite_User_List", list);
            HttpContext.Current.Application.UnLock();
        }

        public static void AppUserRemove(string strUserId)
        {
            HttpContext.Current.Application.Lock();
            if (HttpContext.Current.Application["LearnSite_User_List"] != null)
            {
                ArrayList list = HttpContext.Current.Application.Get("LearnSite_User_List") as ArrayList;
                if (list != null)
                {
                    if (list.IndexOf(strUserId) > -1)
                    {
                        list.Remove(strUserId);
                        HttpContext.Current.Application.Add("LearnSite_User_List", list);
                    }
                }
            }
            HttpContext.Current.Application.UnLock();
        }
        /// <summary>
        /// 匹配移除
        /// </summary>
        /// <param name="strUserId"></param>
        public static void AppUserMatchRemove(string strUserId)
        {
            HttpContext.Current.Application.Lock();
            ArrayList list = HttpContext.Current.Application.Get("LearnSite_User_List") as ArrayList;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    foreach (string ch in list)
                    {
                        if (ch.IndexOf(strUserId) > -1)
                            list.Remove(strUserId);
                    }
                    HttpContext.Current.Application.Add("LearnSite_User_List", list);
                }
            }
            HttpContext.Current.Application.UnLock();
        }
        /// <summary>
        /// 教师退出时，匹配移除当前上课班级学生
        /// </summary>
        /// <param name="strUserId"></param>
        public static void CurrentClassRemove(int Rhid)
        {
            HttpContext.Current.Application.Lock();
            ArrayList list = HttpContext.Current.Application.Get("LearnSite_User_List") as ArrayList;
            ArrayList kick = HttpContext.Current.Application.Get("LearnSite_User_kick") as ArrayList;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    BLL.Room rbll = new BLL.Room();
                    ArrayList students = rbll.GetCurrentClassSnum(Rhid);
                    if (students != null)
                    {
                        foreach (string stu in students)
                        {

                            if (list.IndexOf(stu) > -1)
                                list.Remove(stu);

                            if (kick.IndexOf(stu) > -1)
                                kick.Remove(stu);
                        }
                    }
                    HttpContext.Current.Application.Add("LearnSite_User_List", list);
                    //  HttpContext.Current.Application.Add("LearnSite_User_Kick", kick);//退出时都移除两个列表
                }
            }
            HttpContext.Current.Application.UnLock();
        }

        public static void AppKickUserAdd(string strUserId)
        {
            HttpContext.Current.Application.Lock();
            ArrayList kick = HttpContext.Current.Application.Get("LearnSite_User_Kick") as ArrayList;
            if (kick == null)
            {
                kick = new ArrayList();
                kick.Add(strUserId);
            }
            else
            {
                if (kick.IndexOf(strUserId) < 0)
                    kick.Add(strUserId);//如果不存在则添加            
            }
            HttpContext.Current.Application.Add("LearnSite_User_Kick", kick);
            HttpContext.Current.Application.UnLock();
        }

        public static void AppKickUserRemove(string strUserId)
        {
            HttpContext.Current.Application.Lock();
            if (HttpContext.Current.Application["LearnSite_User_Kick"] != null)
            {
                ArrayList kick = HttpContext.Current.Application.Get("LearnSite_User_Kick") as ArrayList;
                if (kick != null)
                {
                    int kcount = kick.Count;
                    if (kcount > 0)
                    {
                        for (int i = 0; i < kcount; i++)
                        {
                            if (kick.IndexOf(strUserId) > -1)
                            {
                                kick.Remove(strUserId);
                            }
                        }
                    }
                }
                HttpContext.Current.Application.Add("LearnSite_User_Kick", kick);
            }
            HttpContext.Current.Application.UnLock();
        }


        /// <summary>
        /// 匹配踢除该班级学生
        /// </summary>
        /// <param name="strUserId"></param>
        public static void GradeClassRemove(int Sgrade, int Sclass)
        {
            HttpContext.Current.Application.Lock();
            ArrayList list = HttpContext.Current.Application.Get("LearnSite_User_List") as ArrayList;
            ArrayList kick = HttpContext.Current.Application.Get("LearnSite_User_kick") as ArrayList;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    BLL.Room rbll = new BLL.Room();
                    ArrayList students = rbll.GetGradeClassSnum(Sgrade, Sclass);
                    if (students != null)
                    {
                        foreach (string stu in students)
                        {
                            if (list.IndexOf(stu) > -1)
                            {
                                list.Remove(stu);//如果存在，则移除

                                if (kick == null)
                                {
                                    kick = new ArrayList();
                                    kick.Add(stu);
                                }
                                else
                                {
                                    if (kick.IndexOf(stu) < 0)
                                        kick.Add(stu);//如果不存在则添加            
                                }
                            }
                        }
                    }
                    HttpContext.Current.Application.Add("LearnSite_User_List", list);
                    HttpContext.Current.Application.Add("LearnSite_User_Kick", kick);
                }
            }
            HttpContext.Current.Application.UnLock();
        }
    }
}