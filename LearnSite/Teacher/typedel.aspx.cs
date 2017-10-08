using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_typedel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "打字文章删除页面";
            if (Request.QueryString["Tid"] != null)
            {
                LabelTid.Text = Request.QueryString["Tid"].ToString();
            }
            else
            {
                Response.Redirect("~/Teacher/typer.aspx", false);
            }
        }
    }
    protected void LinkBtnDel_Click(object sender, EventArgs e)
    {
        int Tid = Int32.Parse(Request.QueryString["Tid"].ToString());
        LearnSite.BLL.Typer typebll = new LearnSite.BLL.Typer();
        typebll.Delete(Tid);
        System.Threading.Thread.Sleep(1000);
        string url = "~/Teacher/typer.aspx";
        Response.Redirect(url, false);
    }
    protected void LinkBtncancel_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/typer.aspx";
        Response.Redirect(url, false);
    }
}
