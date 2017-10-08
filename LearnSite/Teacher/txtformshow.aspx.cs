using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_txtformshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案表单显示页面";
            if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null)
            {
                showtxtform();
                if (Request.QueryString["Cold"] != null)
                {
                    BtnEdit.Enabled = false;
                }
            }
            else
            {
                Response.Redirect("~/Teacher/course.aspx", false);
            }
        }
    }
    private void showtxtform()
    {
        string Mcid = Request.QueryString["Mcid"].ToString();
        string Mid = Request.QueryString["Mid"].ToString();


        LearnSite.Model.TxtForm tmodel = new LearnSite.Model.TxtForm();
        LearnSite.BLL.TxtForm tbll = new LearnSite.BLL.TxtForm();

        tmodel = tbll.GetModel(Int32.Parse(Mid));

        if (tmodel != null)
        {
            LabelMtitle.Text = tmodel.Mtitle;
            CheckPublish.Checked = tmodel.Mpublish;
            Mcontent.InnerHtml = HttpUtility.HtmlDecode(tmodel.Mcontent);
            LabelMdate.Text = tmodel.Mdate.ToString();
        }
    }
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Mcid"] != null)
        {
            string Cid = Request.QueryString["Mcid"].ToString();
            string url = "~/Teacher/CourseShow.aspx?Cid=" + Cid;
            if (Request.QueryString["Cold"] != null)
            {
                url = url + "&Cold=T";
            }
            Response.Redirect(url, false);
        }
    }
    protected void BtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null)
        {
            string Mcid = Request.QueryString["Mcid"].ToString();
            string Mid = Request.QueryString["Mid"].ToString();
            string url = "~/Teacher/txtformedit.aspx?Mcid=" + Mcid + "&Mid=" + Mid;
            Response.Redirect(url, false);
        }
    }
    protected void BtnReturnSmall_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["Mcid"] != null)
        {
            string Cid = Request.QueryString["Mcid"].ToString();
            string url = "~/Teacher/CourseShow.aspx?Cid=" + Cid;
            if (Request.QueryString["Cold"] != null)
            {
                url = url + "&Cold=T";
            }
            Response.Redirect(url, false);
        }
    }
}