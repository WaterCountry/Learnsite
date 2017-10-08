using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            int Qgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int Qclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            OpenJump(Qgrade, Qclass);//跳转选择
        }
        else
        {
            if (!IsPostBack)
            {
                verChecking();//增加数据库检测
                ShowFoot();
                Btnlogin.Attributes["onClick"] = "return doubleCheck()";
                this.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle();
            }
        }
    }
    private void ShowFoot()
    {
        DateTime dt3 = DateTime.Now;
        string pp = LearnSite.Common.Computer.MyIp();// Page.Request.UserHostAddress;
        DateTime dt4 = DateTime.Now;
        if (Request.QueryString["mySnum"] != null)
        {
            string mysnum=Request.QueryString["mySnum"].ToString();
            TextBoxuser.Text = mysnum;
            TextBoxpwd.Focus();
            LearnSite.BLL.Signin gbll = new LearnSite.BLL.Signin();
            if(!gbll.IsSameIp(mysnum,pp))
            {
                Labelmsg.Text = "您上次不是从这台电脑登录学习平台?<br/>随意换机会给他人带来不便，请理解!";
            }
        }
        Labelterm.Text = LearnSite.Common.XmlHelp.GetTerm();
        int loginm = LearnSite.Common.XmlHelp.LoginMode();//获取登录方式 0表示个人密码方式登录 1表示班级密码方式登录


        DateTime dt5 = DateTime.Now;
        Labelhostname.Text = GetHostNameMy(pp);
        DateTime dt6 = DateTime.Now;
        Labelip.Text = pp;
        Labelloadtime.Text = "IP：" + LearnSite.Common.Computer.DatagoneMilliseconds(dt3, dt4) + "毫秒&nbsp;&nbsp;" + "&nbsp;&nbsp;主机名：" + LearnSite.Common.Computer.DatagoneMilliseconds(dt5, dt6) + "毫秒&nbsp;";

        if (loginm == 1)
        {
            Labelversion.Text = "『班级模式』";
        }
        else
        {
            Labelversion.Text = "『个人模式』";
        }
    }
    /// <summary>
    /// 获取IP对应机器名，返回主机名
    /// </summary>
    /// <param name="Pip"></param>
    /// <returns></returns>
    private string GetHostNameMy(string aPip)
    {
        string msg = "否";//表示不自动获取主机名
        if (!string.IsNullOrEmpty(aPip))
        {
            LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
            LearnSite.Model.Computers cmodel = new LearnSite.Model.Computers();
            cmodel = cbll.GetModelByIp(aPip);

            bool autohostname = LearnSite.Common.XmlHelp.GetAutoHostName();//自动取主机名开关
            if (cmodel != null)//如果存在
            {
                msg = cmodel.Pmachine;//获取主机名
                if (!cmodel.Plock && autohostname)//如果未锁定，更新主机名并锁定
                {
                    string newMachine = LearnSite.Common.Computer.GetGuestHost(aPip);
                    cbll.UpdateByPid(cmodel.Pid, newMachine);
                    msg = newMachine;//返回新主机名
                }
            }
            else
            {
                if (autohostname)
                {
                    LearnSite.Model.Computers newmodel = new LearnSite.Model.Computers();//会丢失？不能过早定义？
                    newmodel.Pip = aPip;
                    newmodel.Plock = true;
                    string addMachine = LearnSite.Common.Computer.GetGuestHost(aPip);
                    newmodel.Pmachine = addMachine;
                    newmodel.Pdate = DateTime.Now;
                    cbll.Add(newmodel);
                    msg = addMachine;
                }
            }
        }
        else
        {
            msg = "空";//表示获取不到IP，反回主机名为空
        }
        return msg;
    }

    protected void Btnlogin_Click(object sender, EventArgs e)
    {
        bool issamenet = LearnSite.Common.XmlHelp.GetLogin();
        bool checkresult = LearnSite.Common.Computer.IsSameNet();
        if (issamenet)
        {
            if (checkresult)
                LoginCode();//如果受限制，且在同网段内，则允许访问
            else
            {
                Labelmsg.Text = "『许可访问→内网√』";
                TextBoxuser.Text = "";
                TextBoxpwd.Text = "";
            }
        }
        else
        {
            LoginCode();
        }       
    }

    private void LoginCode()
    {
        string Snum = TextBoxuser.Text.Trim();
        string Spwd = TextBoxpwd.Text.Trim();
        string lbip = Labelip.Text;
        string msg = "";
        if (Snum != "" && Spwd != "")
        {
            if (LearnSite.Common.WordProcess.IsNum(Snum) && LearnSite.Common.WordProcess.IsEnNum(Spwd))
            {
                LearnSite.Model.Students model = new LearnSite.Model.Students();
                LearnSite.BLL.Students bll = new LearnSite.BLL.Students();
                int loginm = LearnSite.Common.XmlHelp.LoginMode();//获取登录方式 0表示个人密码方式登录 1表示班级密码方式登录
                if (loginm == 1)
                {
                    if (bll.ExistsLogin(Snum, Spwd))
                    {
                        model = bll.SnumGetModel(Snum);//查询该学号和班级密码的学生是否存在，存在返回实体，不存在返回null
                    }
                    else
                    {
                        model = null;
                    }
                }
                else
                {
                    if (bll.ExistsLoginSelf(Snum, Spwd))
                    {
                        model = bll.GetStudentModel(Snum, Spwd);//查询该学号密码学生是否存在，存在返回实体，不存在返回null
                    }
                    else
                    {
                        model = null;
                    }
                }

                if (model != null)
                {
                    int Qgrade = model.Sgrade.Value;
                    int Qclass = model.Sclass.Value;
                    int Qsid = model.Sid;
                    string Qname = model.Sname;
                    int Qsyear = model.Syear.Value;
                    int Qterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
                    if (LearnSite.Common.XmlHelp.GetSingleLogin())//如果是单点登录
                    {
                        if (!LearnSite.Common.App.IsLogin(Snum))//如果不在线
                        {
                            if (LearnSite.Common.CookieHelp.SetStudentCookies(model, Labelip.Text))//写cookies
                            {
                                DateTime LoginTime = DateTime.Now;
                                LearnSite.BLL.Signin gbll = new LearnSite.BLL.Signin();
                                gbll.SigninToday(Snum, LoginTime, lbip, Qgrade, Qterm, Qsid, Qname, Qclass, Qsyear);//签到  
                                Btnlogin.Enabled = false;
                                LearnSite.Common.App.AppKickUserRemove(Snum);//将踢除列表中的学号去掉（2011-9-20修）
                                LearnSite.Common.App.AppUserAdd(Snum);//给网站全局变量列表中增加该用户
                                System.Threading.Thread.Sleep(200);
                                OpenJump(Qgrade, Qclass);//跳转选择
                            }
                            else
                            {
                                msg = "请不要换机，否则无法登录！";
                            }
                        }
                        else
                        {
                            msg = "该用户已经在其他电脑登录！<br/>请询问老师或查看当前是否是你自己的学号!";
                        }
                    }
                    else
                    {
                        if (LearnSite.Common.CookieHelp.SetStudentCookies(model, lbip))//写cookies
                        {
                            DateTime LoginTime = DateTime.Now;
                            LearnSite.BLL.Signin gbll = new LearnSite.BLL.Signin();
                            gbll.SigninToday(Snum, LoginTime, lbip, Qgrade, Qterm, Qsid, Qname, Qclass, Qsyear);//签到  
                            Btnlogin.Enabled = false;
                            System.Threading.Thread.Sleep(200);
                            OpenJump(Qgrade, Qclass);//跳转选择
                        }
                        else
                        {
                            msg = "请不要换机，否则无法登录！";
                        }
                    }
                }
                else
                {
                    string msgstr = "『当前为班级密码模式』";
                    if (loginm == 0)
                        msgstr = "『当前为个人密码模式』";
                    msg = "用户名或密码错误！" + msgstr;
                    TextBoxuser.Text = "";
                    TextBoxpwd.Text = "";
                }

            }
            else
            {
                msg = "用户名或密码含有非法字符，学号必须为数字";
            }
        }
        Labelmsg.Text = msg;
    }

    private void OpenJump(int Sgrade, int Sclass)
    {
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        string Cid = rm.IsRopenRcid(Sgrade, Sclass);

        if (string.IsNullOrEmpty(Cid))
        {
            Response.Redirect("~/Student/myinfo.aspx", false);//如果返回Cid为空
        }
        else
        {
            string myurl = "~/Student/showcourse.aspx?Cid=" + Cid;//快速模式为真，且返回Cid不为空
            Response.Redirect(myurl, true);
        }
    }
    private void verChecking()
    {
        if (!LearnSite.DBUtility.UpdateGrade.TableCheck())
        {
            string ch = "您的数据库未创建或版本更新，现在将跳到更新程序UpGrade.aspx，请执行更新，不影响原有数据！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
            Response.Redirect("~/UpGrade.aspx", true);
        }
    }
}
