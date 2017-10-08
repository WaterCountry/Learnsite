using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "系统设置页面";
    }
    protected void Btnlogout_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.mngCookieNname] != null)
        { 
            LearnSite.Common.CookieHelp.ClearManagerCookies();
            LearnSite.Common.Others.ClearClientPageCache();
        }
        System.Threading.Thread.Sleep(300);
        Response.Redirect("~/Teacher/index.aspx", false);
    }
}
