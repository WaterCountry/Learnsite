using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            Response.Redirect("~/Teacher/infomation.aspx", false);
        }
        else
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.mngCookieNname] != null)
            {
                Response.Redirect("~/Manager/index.aspx", false);
            }
        }
        if (!IsPostBack)
        {
            this.Page.Title = "教师登录页面";
            Textname.Focus();
            verChecking();
        }
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
                Textname.Text = "";
                Textpwd.Text = "";
                LearnSite.Common.WordProcess.Alert("许可访问为：仅内网访问!", this.Page);
            }
        }
        else
        {
            LoginCode();
        }
    }
    /// <summary>
    /// 教师平台
    /// </summary>
    private void LoginCode()
    {
        string aaaastr = "teacherlogintime";
        string Hname = Textname.Text.Trim();
        string Hpwd = Textpwd.Text.Trim();
        if (Hname != "" && Hpwd != "")
        {
            LearnSite.Model.Teacher Tmodel = new LearnSite.Model.Teacher();
            LearnSite.BLL.Teacher Tbll = new LearnSite.BLL.Teacher();
            Tmodel = Tbll.GetTeacherModel(Hname, Hpwd);
            if (Tmodel != null)
            {
                cookieJump(Tmodel, aaaastr);//登录cookie设置和跳转
            }

            else
            {
                Labelmsg.Text = "用户名或密码错误！";
                Textname.Text = "";
                Textpwd.Text = "";
            }
        }
        else
        {
            Labelmsg.Text = "输入不能为空！";
        }
    }
    /// <summary>
    /// 登录cookie设置和跳转
    /// </summary>
    /// <param name="Tmodel"></param>
    /// <param name="aaaastr"></param>
    private void cookieJump(LearnSite.Model.Teacher Tmodel,string aaaastr)
    {
        bool hpermiss = Tmodel.Hpermiss;
        if (LearnSite.Common.CookieHelp.SetTMCookies(Tmodel, hpermiss))
        {
            System.Threading.Thread.Sleep(200);//设置cookies后，延时200毫秒
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                Session[Hid + aaaastr] = DateTime.Now.ToString();
                Response.Redirect("~/Teacher/infomation.aspx", false);
            }
            else
            {
                if (Request.Cookies[LearnSite.Common.CookieHelp.mngCookieNname] != null)
                {
                    Response.Redirect("~/Manager/index.aspx", false);
                }
                else
                {
                    string msg = "本机cookies设置失效，无法登录！";
                    Labelmsg.Text = msg;
                }

            }
        }  
    }

    /// <summary>
    /// 检测数据库和表
    /// </summary>
    private void verChecking()
    {
        if (!LearnSite.DBUtility.SqlHelper.DatabaseExist())
        {
            string cc = "你的数据库连接不上，现在将跳转到更新程序UpGrade.aspx，修改后请执行更新！";
            LearnSite.Common.WordProcess.Alert(cc, this.Page);
            Response.Redirect("~/UpGrade.aspx", false);
        }
        else
        {
            if (!LearnSite.DBUtility.UpdateGrade.VersionCheck())
            {
                string ch = "您的数据库未更新，现在将跳到更新程序UpGrade.aspx，请执行更新，不影响原有数据！";
                LearnSite.Common.WordProcess.Alert(ch, this.Page);
                Response.Redirect("~/UpGrade.aspx", false);
            }
        }
    }
}
