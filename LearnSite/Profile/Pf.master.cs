using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_Profile_Pf : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                ShowStudent();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void ShowStudent()
    {
        int mySid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
        LearnSite.BLL.Students dbll = new LearnSite.BLL.Students();
        sleadername.Text = Server.UrlDecode(dbll.GetLeader(mySid));
        string ssex = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sex"].ToString());
        string murl = LearnSite.Common.Photo.GetStudentPhotoUrl(snum.Text, ssex);
        Imageface.ImageUrl = murl + "?temp=" + DateTime.Now.Millisecond.ToString();

        Labelip.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
        snum.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString());
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        sclass.Text = Sgrade + "." + Sclass + "班";
        sname.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString());

        sscore.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sscore"].ToString();
        sattitude.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sattitude"].ToString();
        LabelRank.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["RankImage"].ToString());
        int myscores = int.Parse(sscore.Text);
        LabelRank.ToolTip = "你当前的等级为：" + myscores / 3 + "级  加速升级中…";
    }
}
