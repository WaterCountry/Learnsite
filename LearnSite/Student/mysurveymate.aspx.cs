using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_mysurveymate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                showclassmate();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void showclassmate()
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Mid"] != null)
        {
            int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
            int mid = Int32.Parse(Request.QueryString["Mid"].ToString());
            int syear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
            int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
            Mitem.InnerHtml ="<br />"+ HttpUtility.HtmlDecode(mbll.GetMitem(mid));

            LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
            Repeater1.DataSource = fbll.ShowItemClassMate(syear, sgrade, sclass, vid, mid);
            Repeater1.DataBind();
        }
    }
}