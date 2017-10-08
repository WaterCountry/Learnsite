using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_program : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (Request.QueryString["Mid"] != null && Request.QueryString["Cid"] != null)
            {
                LearnSite.Common.CookieHelp.KickStudent();
                if (!IsPostBack)
                {
                    ShowMission();
                    ShowIpWorkDone();
                }
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void ShowMission()
    {
        string Mid = Request.QueryString["Mid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Mid))
        {
            LearnSite.Model.Mission model = new LearnSite.Model.Mission();
            LearnSite.BLL.Mission mn = new LearnSite.BLL.Mission();

            model = mn.GetModel(Int32.Parse(Mid));
            if (model != null)
            {
                LabelMtitle.Text = model.Mtitle;
                Mcontent.InnerHtml = HttpUtility.HtmlDecode(model.Mcontent);
                string exampleurl = model.Mexample;
                string ext = LearnSite.Common.WordProcess.getext(exampleurl);
                if (!ext.Contains("sb"))
                {
                    BtnScratch.Visible = false;
                    Labelmsg.Text = ext + "请检查学案的实例是否为Scratch格式（sb,sb2,sbx）";
                }
            }
            else
            {
                Mcontent.InnerHtml = "此学案活动不存在！";
            }
        }

    }

    private void ShowIpWorkDone()
    {
        string Sname = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString();
        string Snum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        string Wcid = Request.QueryString["Cid"].ToString();
        string Wmid = Request.QueryString["Mid"].ToString();
        VoteLink.NavigateUrl = "~/Student/myevaluate.aspx?Mid=" + Wmid + "&Cid=" + Wcid;

        LearnSite.BLL.Works bll = new LearnSite.BLL.Works();
        LearnSite.Model.Works work = new LearnSite.Model.Works();
        work = bll.GetModelByStu(Int32.Parse(Wmid), Snum);
        BtnScratch.Text = "开始编写";
        Thumbnail.ImageUrl = "~/Images/thumbnail.png";
        if (work != null)
        {
            string Wurl = work.Wurl;
            string Wthumbnail = work.Wthumbnail;
            if (!string.IsNullOrEmpty(Wthumbnail))
            {
                Thumbnail.ImageUrl = Wthumbnail + "?temp=" + DateTime.Now.Millisecond.ToString();
                Wtitle.Text = HttpUtility.HtmlDecode(work.Wtitle);
                string urlid = work.Wid.ToString();
                Thumbnail.Attributes["OnClick"] = "scratchShare(" + urlid + ")";
            }
            bool IsCheck = work.Wcheck;
            if (IsCheck)
            {
                Labelmsg.Text = "您的作品已评分!<br/>您不可以重新编写！";
                BtnScratch.Visible = false;
            }
            else
            {
                Labelmsg.Text = "您的作品还未评分!<br/>您可以重新修改提交！";
                BtnScratch.Text = "继续编写";
            }
        }
        if (Snum.StartsWith("s"))
            BtnBegin.Visible = true;
        else
            BtnBegin.Visible = false;
    }
    protected void BtnScratch_Click(object sender, EventArgs e)
    {
        int Qgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int Qclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        string Wcid = Request.QueryString["Cid"].ToString();
        string Wmid = Request.QueryString["Mid"].ToString();
        string url = "~/Student/programing.aspx?Mid=" + Wmid + "&Cid=" + Wcid;
        string Snum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        if (Snum.StartsWith("s"))
            Response.Redirect(url);
        else
        {
            LearnSite.BLL.Room bll = new LearnSite.BLL.Room();
            bool isBegin = bll.IsRscratch(Qgrade, Qclass);
            if (isBegin)
                Response.Redirect(url);
            else
                LearnSite.Common.WordProcess.Alert("编程未开始，请仔细听讲技术关键点！", this.Page);
        }
    }

    protected void BtnBegin_Click(object sender, EventArgs e)
    {   int Qgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int Qclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        LearnSite.BLL.Room bll = new LearnSite.BLL.Room();
        bool isBegin = bll.IsRscratch(Qgrade, Qclass);
        if (isBegin)
        {
            bll.updateRscratch(Qgrade, Qclass, false);
            Labelscratch.Text = "编程未开始!";
        }
        else
        {
            Labelscratch.Text = "编程已开始!";
            bll.updateRscratch(Qgrade, Qclass, true);
        }
    }
}