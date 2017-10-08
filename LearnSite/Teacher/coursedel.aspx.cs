using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_coursedel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();

        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案删除页面";
            if (Request.QueryString["Cid"] != null)
            {
                LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
                LabelID.Text = cbll.GetTitle(Int32.Parse(Request.QueryString["Cid"].ToString()));
                ButtonDel.Enabled = true;
            }
            else
            {
                ButtonDel.Enabled = false;
            }
        }
    }
    protected void ButtonDel_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"] != null)
        {
            int Cid = Int32.Parse(Request.QueryString["Cid"].ToString());
            int Chid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString());
            LearnSite.BLL.Courses coursebll = new LearnSite.BLL.Courses();
            coursebll.DeleteCourse(Cid, Chid);
            System.Threading.Thread.Sleep(500);
            Response.Redirect("~/Teacher/course.aspx", false);
        }
    }
    protected void ButtonCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/course.aspx", false);
    }
}
