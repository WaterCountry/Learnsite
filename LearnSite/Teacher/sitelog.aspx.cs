using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Teacher_sitelog : System.Web.UI.Page
{
    private string sitename = LearnSite.Common.XmlHelp.SiteTitle();
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            showLog();
        }
    }

    private void showLog()
    {
        GvLog.DataSource = LearnSite.Common.Log.FileList();
        GvLog.DataBind();
        if (GvLog.Rows.Count < 1)
        {
            Labelmsg.Text = "当前未发现异常情况记录！";
        }
    }    
}