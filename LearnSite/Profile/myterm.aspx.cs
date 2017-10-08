using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profile_myterm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                showTermScore();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    public void showTermScore()
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            string mynum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            LearnSite.BLL.TermTotal tbll = new LearnSite.BLL.TermTotal();
            DLterm.DataSource = tbll.GetSnumTermList(mynum);
            DLterm.DataBind();
        }
    }
}