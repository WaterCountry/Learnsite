using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_programshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "编程主题页面";
            if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null)
            {
                showmission();
            }
            else
            {
                Response.Redirect("~/Teacher/course.aspx", false);
            }
        }
    }
    private void showmission()
    {
        string Mcid = Request.QueryString["Mcid"].ToString();
        string Mid = Request.QueryString["Mid"].ToString();


        LearnSite.Model.Mission model = new LearnSite.Model.Mission();
        LearnSite.BLL.Mission mn = new LearnSite.BLL.Mission();

        model = mn.GetModel(Int32.Parse(Mid));
        if (model != null)
        {
            LabelMtitle.Text = model.Mtitle;
            Mcontent.InnerHtml = HttpUtility.HtmlDecode(model.Mcontent);
            string sburl = model.Mexample;
            Hlexample.NavigateUrl = sburl;
            Hlexample.Text = LearnSite.Common.WordProcess.getshortfname(sburl);
            CheckPublish.Checked = model.Mpublish;
            CheckMicoWorld.Checked = model.Microworld;
            LabelMdate.Text = model.Mdate.ToString();
            LabelMfiletype.Text = model.Mfiletype;
            ImageType.ImageUrl = "~/Images/FileType/" + LabelMfiletype.Text.ToLower() + ".gif";
            int Mgid = model.Mgid.Value;
            if (Mgid != 0)
                HLMgid.NavigateUrl = "~/Teacher/GaugeItem.aspx?Gid=" + Mgid.ToString();
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
            string url = "~/Teacher/programedit.aspx?Mcid=" + Mcid + "&Mid=" + Mid;
            Response.Redirect(url, false);
        }
    }
}