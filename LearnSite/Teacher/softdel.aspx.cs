using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_softdel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "资源删除页面";
            Labelurl.Text = Request.QueryString["Furl"].ToString();
        }
    }
    protected void LinkBtnDel_Click(object sender, EventArgs e)
    {
        int Fid = Int32.Parse(Request.QueryString["Fid"].ToString());
        LearnSite.BLL.Soft bll = new LearnSite.BLL.Soft();
        bll.Delete(Fid);
        System.Threading.Thread.Sleep(1000);
        string url = "~/Teacher/soft.aspx";
        Response.Redirect(url, false);
    }
    protected void LinkBtncancel_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/soft.aspx";
        Response.Redirect(url, false);
    }
}
