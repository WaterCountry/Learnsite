using System;
using System.Collections.Generic;
using System.Web;

namespace LearnSite.Common
{
    /// <summary>
    ///CookieHelp 的摘要说明
    /// </summary>
    public class CookieHelp
    {
        public CookieHelp()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 对字符串进行url编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string UrlEncode(string str)
        {
            return HttpContext.Current.Server.UrlEncode(str);
        }
        public static string cfx = LearnSite.Common.XmlHelp.GetCookiesFix();
        public static string serverName = LearnSite.DBUtility.DbLinkEdit.serverNameFix();
        public static string tempcfx = cfx + serverName;
        public static string stuCookieNname = "S" + tempcfx;
        public static string teaCookieNname = "T" + tempcfx;
        public static string mngCookieNname = "M" + tempcfx;

        public static bool SetStudentCookies(LearnSite.Model.Students stumod, string logip)
        {
            bool isset = false;
            if (stumod != null)
            {
                LearnSite.BLL.Signin gbll = new BLL.Signin();
                LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
                if (rm.IsLoginLock(stumod.Sgrade.Value, stumod.Sclass.Value))
                {
                    if (gbll.IsSameIp(stumod.Snum, logip))
                    {
                        //如果全班IP登录锁定，如果IP不变 写cookies
                        isset = SetStuCookie(stumod, logip);
                    }
                }
                else
                {
                    //如果全班IP不锁定，写cookies
                    isset = SetStuCookie(stumod, logip);
                }
            }
            return isset;
        }
        /// <summary>
        /// 设置学生的cookies值
        /// </summary>
        /// <param name="stmodel"></param>
        ///Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Sattitude,Sape
        public static bool SetStuCookie(LearnSite.Model.Students stmodel, string LoginIp)
        {
            if (stmodel != null)
            {
                LearnSite.BLL.Room rm = new LearnSite.BLL.Room();

                string Rhid = rm.GetRoomRhid(stmodel.Sgrade.Value, stmodel.Sclass.Value);
                DateTime LoginTime = DateTime.Now;
                LearnSite.BLL.Webstudy wbll = new LearnSite.BLL.Webstudy();
                string FtpPwd = wbll.FindWebFtpPwd(stmodel.Snum);

                string ThisTerm = LearnSite.Common.XmlHelp.GetTerm();

                HttpCookie StuCookie = new HttpCookie(stuCookieNname);
                if (stmodel.Sex == null)
                    stmodel.Sex = "无";
                if (stmodel.Sscore == null)
                    stmodel.Sscore = 0;
                if (stmodel.Squiz == null)
                    stmodel.Squiz = 0;
                if (stmodel.Sattitude == null)
                    stmodel.Sattitude = 0;
                if (stmodel.Sape == null)
                    stmodel.Sape = "无";
                if (stmodel.Sgroup == null)
                    stmodel.Sgroup = 0;

                StuCookie.Values.Add("Sid", stmodel.Sid.ToString());
                StuCookie.Values.Add("Snum", UrlEncode(stmodel.Snum));
                StuCookie.Values.Add("Syear", stmodel.Syear.ToString());
                StuCookie.Values.Add("Sgrade", stmodel.Sgrade.ToString());
                StuCookie.Values.Add("Sclass", stmodel.Sclass.ToString());
                StuCookie.Values.Add("Sname", UrlEncode(stmodel.Sname.Trim()));
                StuCookie.Values.Add("Spwd", Common.WordProcess.GetMD5_8bit(stmodel.Spwd));
                StuCookie.Values.Add("Sex", UrlEncode(stmodel.Sex));
                StuCookie.Values.Add("Sscore", stmodel.Sscore.ToString());
                StuCookie.Values.Add("Squiz", stmodel.Squiz.ToString());
                StuCookie.Values.Add("Sattitude", stmodel.Sattitude.ToString());
                StuCookie.Values.Add("Sape", UrlEncode(stmodel.Sape));
                StuCookie.Values.Add("Sgroup", stmodel.Sgroup.ToString());
                StuCookie.Values.Add("LoginTime", LoginTime.ToString());
                StuCookie.Values.Add("LoginIp", LoginIp);
                StuCookie.Values.Add("Ftppwd", FtpPwd);
                StuCookie.Values.Add("ThisTerm", ThisTerm);
                StuCookie.Values.Add("Rhid", Rhid);
                StuCookie.Values.Add("RankImage", UrlEncode(LearnSite.Common.Rank.RankImage(stmodel.Sscore.Value + stmodel.Sattitude.Value, true)));
                StuCookie.Values.Add("Ss", Common.WordProcess.GetMD5_8bit(stmodel.Snum));

                string str = LearnSite.Common.XmlHelp.GetStudentCookiesPeriod();
                StuCookie.Expires = StudentCookiesPeriod(str);
                StuCookie.Path = "/";
                StuCookie.HttpOnly = true;
                HttpContext.Current.Response.AppendCookie(StuCookie);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 清除教师的cookie值
        /// </summary>
        public static void ClearManagerCookies()
        {
            if (HttpContext.Current.Request.Cookies[mngCookieNname] != null)
            {
                HttpCookie mOldCookie = HttpContext.Current.Request.Cookies[mngCookieNname];
                mOldCookie.Expires = DateTime.Now.AddYears(-20);//将这个Cookie过期掉
                HttpContext.Current.Response.AppendCookie(mOldCookie);
            }
        }
        /// <summary>
        /// 清除教师的cookie值
        /// </summary>
        public static void ClearTeacherCookies()
        {
            if (HttpContext.Current.Request.Cookies[teaCookieNname] != null)
            {
                HttpCookie tOldCookie = HttpContext.Current.Request.Cookies[teaCookieNname];
                tOldCookie.Expires = DateTime.Now.AddYears(-20);//将这个Cookie过期掉
                HttpContext.Current.Response.AppendCookie(tOldCookie);
            }
        }
        /// <summary>
        /// 清除学生的cookie值
        /// </summary>
        public static void ClearStudentCookies()
        {
            if (HttpContext.Current.Request.Cookies[stuCookieNname] != null)
            {
                HttpCookie sOldCookie = HttpContext.Current.Request.Cookies[stuCookieNname];
                sOldCookie.Expires = DateTime.Now.AddYears(-20);//将这个Cookie过期掉
                HttpContext.Current.Response.AppendCookie(sOldCookie);
            }
        }

        public static bool IsStudentLogin()
        {
            if (HttpContext.Current.Request.Cookies[stuCookieNname] != null)
            {
                string mySnum = HttpContext.Current.Request.Cookies[stuCookieNname].Values["Snum"].ToString();
                if (!string.IsNullOrEmpty(mySnum))
                {
                    string ss = HttpContext.Current.Request.Cookies[stuCookieNname].Values["Ss"].ToString();
                    if (ss == Common.WordProcess.GetMD5_8bit(mySnum))
                        return true;
                    else
                    {
                        ClearStudentCookies();
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static void KickStudent()
        {
            if (HttpContext.Current.Request.Cookies[stuCookieNname] != null)  //增加cookies判斷 (2011-9-27修）
            {
                if (LearnSite.Common.XmlHelp.GetSingleLogin())//如果是单点登录
                {
                    string mySnum = HttpContext.Current.Request.Cookies[stuCookieNname].Values["Snum"].ToString();
                    if (LearnSite.Common.App.Iskick(mySnum))  //如果学生学号在踢除全局变量中
                    {
                        LearnSite.Common.App.AppKickUserRemove(mySnum);//将踢除列表中的学号去掉（2011-9-15修）
                        ClearStudentCookies();  //则清除该项学生的页面cookies，要求重登录。
                        int millsed = DateTime.Now.Millisecond;
                        string rurl = "~/index.aspx?kick=" + mySnum + "&mill=" + millsed.ToString();
                        System.Threading.Thread.Sleep(300);
                        HttpContext.Current.Response.Redirect(rurl, false);
                    }
                }
            }
        }
        /// <summary>
        /// 修改cookies集合中的某项cookie的值
        /// </summary>
        /// <param name="cookiesName">cookies集合名称</param>
        /// <param name="itemName">某项cookie名称</param>
        /// <param name="newValue">赋值内容</param>
        public static void EditCookiesItem(string cookiesName, string itemName, string newValue)
        {
            HttpCookie cok = HttpContext.Current.Request.Cookies[cookiesName];
            if (cok != null)
            {
                cok.Values.Set(itemName, newValue);
                string str = LearnSite.Common.XmlHelp.GetStudentCookiesPeriod();
                if (str != "0")
                {
                    cok.Expires = StudentCookiesPeriod(str);
                }
                HttpContext.Current.Response.AppendCookie(cok);
            }
        }
        /// <summary>
        /// 判断是否存在cookies，不存在则跳转到登录窗口
        /// </summary>
        public static void JudgeTeacherCookies()
        {
            if (HttpContext.Current.Request.Cookies[teaCookieNname] == null)//如果没登录，跳出
            {
                HttpContext.Current.Response.Redirect("~/Teacher/index.aspx", true);
            }
            else
            {
                string hs = HttpContext.Current.Request.Cookies[teaCookieNname]["Hs"].ToString();
                string hid = HttpContext.Current.Request.Cookies[teaCookieNname]["Hid"].ToString();
                if (hs == Common.WordProcess.GetMD5_8bit(hid.ToString()))
                {
                    LearnSite.BLL.Room rbll = new BLL.Room();
                    if (!rbll.ExistMyClass(Int32.Parse(hid)))
                    {
                        //如果没有任教班级，则始终跳转至信息页面
                        HttpContext.Current.Response.Redirect("~/Teacher/infomation.aspx", true);
                    }
                }
                else
                {
                    ClearTeacherCookies();//非法cookies，清除再跳转
                    System.Threading.Thread.Sleep(500);
                    HttpContext.Current.Response.Redirect("~/Teacher/index.aspx", true);
                }
            }
        }

        /// <summary>
        /// （学案列表）简单判断是否存在cookies，不存在则跳转到登录窗口
        /// </summary>
        public static void IsTeacherCookies()
        {
            if (HttpContext.Current.Request.Cookies[teaCookieNname] == null)//如果没登录，跳出
            {
                HttpContext.Current.Response.Redirect("~/Teacher/index.aspx", true);
            }
        }

        /// <summary>
        /// 如果没登录，跳转到登录首页
        /// 如果登录，但权限为假，也跳转到教师首页
        /// </summary>
        public static void JudgeIsAdmin()
        {
            if (HttpContext.Current.Request.Cookies[mngCookieNname] == null)//没登录跳出
            {
                HttpContext.Current.Response.Redirect("~/Teacher/index.aspx", true);
            }
            else
            {
                string hs = HttpContext.Current.Request.Cookies[mngCookieNname]["Hs"].ToString();
                string hid = HttpContext.Current.Request.Cookies[mngCookieNname]["Hid"].ToString();
                if (hs != Common.WordProcess.GetMD5(hid.ToString()))
                {
                    ClearManagerCookies();//非法cookies，清除再跳转
                    Others.ClearClientPageCache();
                    System.Threading.Thread.Sleep(500);
                    HttpContext.Current.Response.Redirect("~/Teacher/index.aspx", true);
                }
            }
        }
        /// <summary>
        /// 根据权限设置教师或管理员cookies
        /// </summary>
        /// <param name="tcmodel"></param>
        /// <param name="permiss"></param>
        /// <returns></returns>
        public static bool SetTMCookies(LearnSite.Model.Teacher tcmodel, bool permiss)
        {
            if (permiss)
                return SetManagerCookies(tcmodel);
            else
                return SetTeacherCookies(tcmodel);
        }

        /// <summary>
        /// 设置教师的cookies值
        /// </summary>
        /// <param name="Model"></param>
        private static bool SetTeacherCookies(LearnSite.Model.Teacher tcmodel)
        {
            if (tcmodel != null)
            {
                HttpCookie TCookies = new HttpCookie(teaCookieNname);
                TCookies.Values.Add("Hid", tcmodel.Hid.ToString());
                TCookies.Values.Add("Hname", UrlEncode(tcmodel.Hname));
                TCookies.Values.Add("Hnick", UrlEncode(tcmodel.Hnick));
                TCookies.Values.Add("Hroom", UrlEncode(tcmodel.Hroom));
                TCookies.Values.Add("Hpermiss", tcmodel.Hpermiss.ToString());
                TCookies.Values.Add("Hs", Common.WordProcess.GetMD5_8bit(tcmodel.Hid.ToString()));

                TCookies.Expires = DateTime.Now.AddDays(1);
                TCookies.Path = "/";
                TCookies.HttpOnly = true;
                HttpContext.Current.Response.AppendCookie(TCookies);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置管理员的cookies值
        /// </summary>
        /// <param name="Model"></param>
        private static bool SetManagerCookies(LearnSite.Model.Teacher tcmodel)
        {
            if (tcmodel != null)
            {
                HttpCookie TCookies = new HttpCookie(mngCookieNname);
                TCookies.Values.Add("Hid", tcmodel.Hid.ToString());
                TCookies.Values.Add("Hname", UrlEncode(tcmodel.Hname));
                TCookies.Values.Add("Hnick", UrlEncode(tcmodel.Hnick));
                TCookies.Values.Add("Hroom", UrlEncode(tcmodel.Hroom));
                TCookies.Values.Add("Hpermiss", tcmodel.Hpermiss.ToString());
                TCookies.Values.Add("Hs", Common.WordProcess.GetMD5(tcmodel.Hid.ToString()));

                TCookies.Expires = DateTime.Now.AddDays(1);
                TCookies.Path = "/";
                TCookies.HttpOnly = true;
                HttpContext.Current.Response.AppendCookie(TCookies);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 如果有登录，则返回1
        /// </summary>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static bool IsTeacher()
        {
            if (HttpContext.Current.Request.Cookies[teaCookieNname] != null)
            {
                string hs = HttpContext.Current.Request.Cookies[teaCookieNname]["Hs"].ToString();
                string hid = HttpContext.Current.Request.Cookies[teaCookieNname]["Hid"].ToString();
                if (hs == Common.WordProcess.GetMD5_8bit(hid.ToString()))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// 跳回首页
        /// </summary>
        public static void JudgeStudentCookies()
        {
            HttpContext.Current.Response.Redirect("~/index.aspx", true);
        }

        public static string SetMainPageTitle()
        {
            string str = LearnSite.Common.XmlHelp.SiteTitle() + "--->";
            return str;
        }
        public static string GetVersion()
        {
            string copyright = " LearnSite" + LearnSite.Common.WordProcess.NewVersion() + " ";
            return copyright;
        }

        private static DateTime StudentCookiesPeriod(string str)
        {
            DateTime myPeriod = DateTime.MinValue;
            switch (str)
            {
                case "1":
                    myPeriod = DateTime.Now.AddHours(2);
                    break;
                case "2":
                    myPeriod = DateTime.Now.AddHours(4);
                    break;
                case "3":
                    myPeriod = DateTime.Now.AddHours(8);
                    break;
                case "4":
                    myPeriod = DateTime.Now.AddHours(12);
                    break;
                case "5":
                    myPeriod = DateTime.MaxValue;
                    break;
            }
            return myPeriod;
        }
        /// <summary>
        /// 模拟该班级学生登录cookies设置
        /// </summary>
        /// <param name="Sgrade"></param>
        /// <param name="Sclass"></param>
        public static void SimulationStudentCookies(int Sgrade, int Sclass)
        {
            if (HttpContext.Current.Request.Cookies[teaCookieNname] != null)
            {
                string Rhid = HttpContext.Current.Request.Cookies[teaCookieNname].Values["Hid"].ToString();
                LearnSite.BLL.Students stubll = new BLL.Students();
                LearnSite.Model.Students sModel = new LearnSite.Model.Students();
                string sss = Rhid + Sgrade.ToString() + Sclass.ToString();
                sModel.Sid = 0 - Int32.Parse(sss);
                string Syear = stubll.GetYear(Sgrade);
                sModel.Syear =Int32.Parse( Syear);
                sModel.Snum = "s"+Rhid + Syear.ToString() +Sgrade.ToString()+ Sclass.ToString();
                sModel.Sgrade = Sgrade;
                sModel.Sclass = Sclass;
                sModel.Sname = "学生" + Sgrade.ToString() + Sclass.ToString() ;
                sModel.Spwd = "12345678";
                sModel.Sex = "男";
                sModel.Sscore = 100;
                sModel.Squiz = 50;
                sModel.Sattitude = 25;
                sModel.Sape = "A";
                sModel.Sgroup = 0;

                DateTime LoginTime = DateTime.Now;
                string LoginIp = LearnSite.Common.Computer.GetGuestIP();
                string FtpPwd = LearnSite.Common.WordProcess.GetMD5_Nbit(Rhid, 18);
                string ThisTerm = LearnSite.Common.XmlHelp.GetTerm();
                HttpCookie StuCookie = new HttpCookie(stuCookieNname);

                StuCookie.Values.Add("Sid", sModel.Sid.ToString());
                StuCookie.Values.Add("Snum", UrlEncode(sModel.Snum));
                StuCookie.Values.Add("Syear", sModel.Syear.ToString());
                StuCookie.Values.Add("Sgrade", sModel.Sgrade.ToString());
                StuCookie.Values.Add("Sclass", sModel.Sclass.ToString());
                StuCookie.Values.Add("Sname", UrlEncode(sModel.Sname));
                StuCookie.Values.Add("Spwd", sModel.Spwd.ToString());
                StuCookie.Values.Add("Sex", UrlEncode(sModel.Sex));
                StuCookie.Values.Add("Sscore", sModel.Sscore.ToString());
                StuCookie.Values.Add("Squiz", sModel.Squiz.ToString());
                StuCookie.Values.Add("Sattitude", sModel.Sattitude.ToString());
                StuCookie.Values.Add("Sape", sModel.Sape.ToString());
                StuCookie.Values.Add("Sgroup", sModel.Sgroup.ToString());
                StuCookie.Values.Add("LoginTime", LoginTime.ToString());
                StuCookie.Values.Add("LoginIp", LoginIp);
                StuCookie.Values.Add("Ftppwd", FtpPwd);
                StuCookie.Values.Add("ThisTerm", ThisTerm);
                StuCookie.Values.Add("Rhid", Rhid);
                StuCookie.Values.Add("RankImage", UrlEncode(LearnSite.Common.Rank.RankImage(sModel.Sscore.Value + sModel.Sattitude.Value,true)));
                StuCookie.Values.Add("Ss", Common.WordProcess.GetMD5_8bit(sModel.Snum));

                string str = LearnSite.Common.XmlHelp.GetStudentCookiesPeriod();
                if (str != "0")
                {
                    StuCookie.Expires = StudentCookiesPeriod(str);
                    StuCookie.Path = "/";
                    StuCookie.HttpOnly = true;
                }
                HttpContext.Current.Response.AppendCookie(StuCookie);
                LearnSite.Common.App.AppUserAdd(sModel.Snum);
            }
        }
    }
}