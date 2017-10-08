using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profile_mychange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            ShowStudent();
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    private void ShowStudent()
    {
        int Sid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
        LearnSite.BLL.Students stu = new LearnSite.BLL.Students();        
        Repeater1.DataSource = stu.GetOneStudent(Sid);
        Repeater1.DataBind();
    }
}