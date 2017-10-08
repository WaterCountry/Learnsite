using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_studentshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                if (Request.QueryString["Sid"] != null)
                {
                    ShowStudent();
                }
            }
        }
    }
    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        int Sid =Int32.Parse( Request.QueryString["Sid"].ToString());
        string url = "~/Teacher/studentedit.aspx?Sid=" + Sid ; 
        Response.Redirect(url, false);
    }

    private void ShowStudent()
    {
        int Sid = Int32.Parse(Request.QueryString["Sid"].ToString());
        LearnSite.BLL.Students stu = new LearnSite.BLL.Students();
        Repeater1.DataSource = stu.GetOneStudent(Sid);
        Repeater1.DataBind();
    }


}
