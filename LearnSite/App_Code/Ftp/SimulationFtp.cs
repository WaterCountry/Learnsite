using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
namespace LearnSite.Ftp
{
    /// <summary>
    ///SimulationFtp 的摘要说明
    /// </summary>
    public class SimulationFtp
    {
        public SimulationFtp()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 创建教师模拟学生角色制作网页时的ftp空间和账号，每个班级一个
        /// </summary>
        /// <returns></returns>
        public static int TeacherSlftp(string teapwd)
        {
            int initreg = 0;
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                int Rhid = Int32.Parse(HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString());
                LearnSite.BLL.Students stubll=new BLL.Students();
                LearnSite.BLL.Webstudy wbll = new BLL.Webstudy();
                DataSet ds = stubll.TeacherSyearSclass(Rhid);
                int rcount = ds.Tables[0].Rows.Count;                
                if (rcount > 0)
                {
                    for (int i = 0; i < rcount; i++)
                    {
                        if (ds.Tables[0].Rows[i]["Syear"].ToString() != "" && ds.Tables[0].Rows[i]["Sclass"].ToString() != "")
                        {
                            string Syear = ds.Tables[0].Rows[i]["Syear"].ToString();
                            string Sgrade = ds.Tables[0].Rows[i]["Sgrade"].ToString();
                            string Sclass = ds.Tables[0].Rows[i]["Sclass"].ToString();
                            string Snum ="s"+ Rhid + Syear.ToString() + Sgrade.ToString() + Sclass.ToString();
                            string Wurl="~/FtpSpace/"+Syear+"/"+Sgrade+"/"+Snum;//2008/1/201211006";
                            if (LearnSite.Ftp.Reg.RegsaveFtp(Snum, teapwd, Syear, Sclass))
                            {
                                initreg++;//注册成功，则计数
                            }
                            else
                            {
                                LearnSite.Ftp.Reg.Updatepwd(Snum, teapwd);//更新为新密码，为了兼容修正
                            }
                            if (wbll.ExistsWnum(Snum))
                            {
                                wbll.UpdateWpwd2(Snum, teapwd);//如果存在，则更新模拟密码
                            }
                            else
                            {
                                wbll.AddSimulation(Snum, teapwd, Wurl);//在webstudy表中增加一个模拟账号
                            }
                            LearnSite.Ftp.Disk.CreateOneDir(Syear, Sclass, Snum);
                        }
                    }
                }
            }
            return initreg;
        }
    }
}