using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_typechineseshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "拼音词语显示页面";
            if (Request.QueryString["Nid"] != null)
            {
                ShowChinese();
            }
            else
            {
                Response.Redirect("~/Teacher/typechinese.aspx", false);
            }
        }
    }

    private void ShowChinese()
    {
        if (Request.QueryString["Nid"] != null)
        {
            int Nid = Int32.Parse(Request.QueryString["Nid"].ToString());
            LearnSite.BLL.Chinese cbll = new LearnSite.BLL.Chinese();
            Repeater1.DataSource = cbll.GetOneChinese(Nid);
            Repeater1.DataBind();
        }
    }

    protected void BtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["Nid"] != null)
        {
            string Nid = Request.QueryString["Nid"].ToString();
            string url = "~/Teacher/typechineseedit.aspx?Nid=" + Nid;
            Response.Redirect(url, false);
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/typechinese.aspx";
        Response.Redirect(url, false);
    }
}