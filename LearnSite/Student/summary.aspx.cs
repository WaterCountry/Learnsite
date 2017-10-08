using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                showSummary();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void showSummary()
    {
        if (Request.QueryString["Scid"] != null)
        {
            int Sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int Sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            int Scid = Int32.Parse(Request.QueryString["Scid"].ToString());
            LearnSite.Model.Summary smodel = new LearnSite.Model.Summary();
            LearnSite.BLL.Summary sbll = new LearnSite.BLL.Summary();
            string hid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Rhid"].ToString();

            if (!string.IsNullOrEmpty(hid))
            {
                LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
                Label1.Text = cbll.GetTitle(Scid);
                smodel = sbll.GetModelByClass(Scid, Int32.Parse(hid), Sgrade, Sclass);
                if (smodel != null)
                {
                    // LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
                    if (smodel.Sshow.Value)
                    {
                        contents.InnerHtml = HttpUtility.HtmlDecode(smodel.Scontent);
                    }
                    else
                    {
                        contents.InnerHtml = "隐藏内容！";
                    }
                    Label6.Text = smodel.Sdate.ToString();
                }
                else
                {
                    contents.InnerHtml = "老师还未填写总结！";
                }
            }
            string Syear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
            string Snum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();

            string teasnum = "s" + hid + Syear + Sgrade.ToString() + Sclass.ToString();
            if (teasnum == Snum)
            {
                BtnEdit.Enabled = true;
                BtnEdit.ToolTip = "修改或添加总结！";
            }
        }
    }
    protected void BtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        string Scid = Request.QueryString["Scid"].ToString();
        string smurl = "~/Student/summaryedit.aspx?Scid="+Scid;
        Response.Redirect(smurl, true);
    }
}
