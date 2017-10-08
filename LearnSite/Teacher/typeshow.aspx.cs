using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_typeshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "打字文章显示页面";
            if (Request.QueryString["Tid"] != null)
            {
                ShowType();
            }
            else
            {
                Response.Redirect("~/Teacher/typer.aspx", false);
            }
        }
    }

    private void ShowType()
    {
        if (Request.QueryString["Tid"] != null)
        {
            int Tid = Int32.Parse(Request.QueryString["Tid"].ToString());
            LearnSite.BLL.Typer typerbll = new LearnSite.BLL.Typer();
            Repeater1.DataSource = typerbll.GetOneArticle(Tid);
            Repeater1.DataBind();
        }
    }

    protected void BtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["Tid"] != null)
        {
            string Tid = Request.QueryString["Tid"].ToString();
            string url = "~/Teacher/typeedit.aspx?Tid=" + Tid;
            Response.Redirect(url, false);
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/typer.aspx";
        Response.Redirect(url, false);
    }
}
