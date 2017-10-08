using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profile_myclass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                showclass();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    protected void Btnclass_Click(object sender, EventArgs e)
    {
        if (DDLclass.SelectedValue != null && DDLclass.SelectedValue != Labelclass.Text)
        {
            string Studnetnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            string Syear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
            int selectclass = Int32.Parse(DDLclass.SelectedValue);
            LearnSite.BLL.Students bll = new LearnSite.BLL.Students();
            bll.UpdateStuclass(selectclass, Studnetnum);

            //如果转班，再生成ftp目录，并修改ftp账号中的 Access和HomeDir值（传递Syear,Sclass,Snum）
            LearnSite.Ftp.Disk.CreateOneDir(Syear, DDLclass.SelectedValue, Studnetnum);
            LearnSite.Ftp.Reg.RegEditFtp(Studnetnum, Syear, DDLclass.SelectedValue);

            LearnSite.Common.CookieHelp.ClearStudentCookies();
            string msg = "转班成功，请重新登录！";
            LearnSite.Common.WordProcess.Alert(msg, this.Page);
           // this.Response.Write(" <script language=javascript>alert('修改成功！');window.window.location.href='myinfo.aspx';</script> ");
        }
        else
        {
            Labelstr.Text = "不能转班！";
        }
    }
    private void showclass()
    {
        int sclass=Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        Labelclass.Text = sclass.ToString();
        int sgrade =Int32.Parse( Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        if (rbll.GetRclassedit(sgrade,sclass))
        {
            Btnclass.Enabled = true;

            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            DDLclass.DataSource = rm.GetLimitAllClass(sgrade);
            DDLclass.DataTextField = "Rclass";
            DDLclass.DataValueField = "Rclass";
            DDLclass.DataBind();
        }
        else
        {
            Btnclass.Enabled = false;
            Labelstr.Text = "限制转班";
        }
    }
}