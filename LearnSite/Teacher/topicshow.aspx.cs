using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_topicshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "主题讨论浏览页面";
            ShowTopic();
            if (Request.QueryString["Cold"] != null)
            {
                BtnEdit.Enabled = false;
            }
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/courseshow.aspx?Cid=" + LabelMcid.Text;
        if (Request.QueryString["Cold"] != null)
        {
            url = url + "&Cold=T";
        }
        Response.Redirect(url, false);
    }
    private void ShowTopic()
    {
        if (Request.QueryString["Tid"] != null)
        {
            int Tid = Int32.Parse(Request.QueryString["Tid"].ToString());
            LearnSite.BLL.TopicDiscuss bll = new LearnSite.BLL.TopicDiscuss();
            LearnSite.Model.TopicDiscuss model = new LearnSite.Model.TopicDiscuss();
            model = bll.GetModel(Tid);
            Labeltid.Text = model.Tid.ToString();
            LabelTtitle.Text = model.Ttitle;
            LabelMcid.Text = model.Tcid.ToString();
            LabelTdate.Text = model.Tdate.ToString();
            bool isClose = model.Tclose;
            if (isClose)
            {
                Btnclock.ImageUrl = "~/Images/clockred.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "讨论暂停！";
            }
            else
            {
                Btnclock.ImageUrl = "~/Images/clock.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "讨论开启！";
            }
            Tcontent.InnerHtml = HttpUtility.HtmlDecode(model.Tcontent);
        }
    }
    protected void BtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        string url = "~/Teacher/topicedit.aspx?Tcid=" + LabelMcid.Text + "&Tid=" + Labeltid.Text;
        Response.Redirect(url, false);
    }
    protected void Btnclock_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["Tid"] != null)
        {
            int tid = Int32.Parse(Request.QueryString["Tid"].ToString());
            LearnSite.BLL.TopicDiscuss tdbll = new LearnSite.BLL.TopicDiscuss();
            tdbll.UpdateTclose(tid);//更新
            System.Threading.Thread.Sleep(500);
            ShowTopic();
        }
    }
}