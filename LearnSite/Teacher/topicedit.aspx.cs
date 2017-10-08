using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_topicedit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.QueryString["Tcid"] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案讨论主题添加页面";
                showtopic();
            }
        }
        else
        {
            Response.Redirect("~/Teacher/course.aspx", false);
        }
    }
    protected string myCid()
    {
        if (Request.QueryString["Tcid"] != null)
        {
            return Request.QueryString["Tcid"].ToString();
        }
        else
        {
            return "";
        }
    }
    private void showtopic()
    {
        if (Request.QueryString["Tid"] != null)
        {
            int tid = Int32.Parse(Request.QueryString["Tid"]);
            LearnSite.BLL.TopicDiscuss bll = new LearnSite.BLL.TopicDiscuss();
            LearnSite.Model.TopicDiscuss model = new LearnSite.Model.TopicDiscuss();
            model = bll.GetModel(tid);
            Texttitle.Text = model.Ttitle;
            mcontent.InnerText = HttpUtility.HtmlDecode(model.Tcontent);
            CheckClose.Checked = model.Tclose;
        }
    }
    protected void Btnedit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Tcid"] != null)
        {
            string fckstr = mcontent.InnerText;
            if (Texttitle.Text != "" && fckstr != "")
            {
                string hidstr = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                int Tcid = Int32.Parse(Request.QueryString["Tcid"].ToString());
                int Tid = Int32.Parse(Request.QueryString["Tid"].ToString());
                LearnSite.BLL.TopicDiscuss bll = new LearnSite.BLL.TopicDiscuss();

                string Ttitle = Texttitle.Text.Trim();
                string Tcontent = HttpUtility.HtmlEncode(fckstr);
                bool Tclose = CheckClose.Checked;
                bll.UpdateTopic(Tid, Ttitle, Tcontent, Tclose);

                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();

                lmodel.Lcid = Tcid;
                lmodel.Lxid = Tid;
                lmodel.Ltype = 3;
                lmodel.Ltitle = Texttitle.Text.Trim();
                lbll.UpdateLtitle(lmodel);

                System.Threading.Thread.Sleep(200);
                string url = "~/Teacher/topicshow.aspx?Tid=" + Tid;
                Response.Redirect(url, false);
            }
            else
            {
                Labelmsg.Text = "内容及标题不能为空！";
            }
        }
    }
    protected void BtnCourse_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Tid"] != null)
        {
            string Tid = Request.QueryString["Tid"].ToString();
            string url = "~/Teacher/topicshow.aspx?Tid=" + Tid;
            Response.Redirect(url, false);
        }
    }
}