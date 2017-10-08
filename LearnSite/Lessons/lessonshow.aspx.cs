using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lessons_lessonshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.IsTeacherCookies();
        LinkBtn.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
        if (!IsPostBack)
            ShowLessonDetails();
    }
    private void ShowLessonDetails()
    {
        int Cid =Int32.Parse( Request.QueryString["Cid"].ToString());
        LearnSite.BLL.Courses csbll = new LearnSite.BLL.Courses();
        Repeater1.DataSource = csbll.GetCourseDetail(Cid);
        Repeater1.DataBind();

        LearnSite.BLL.Mission msbll = new LearnSite.BLL.Mission();
        Repeater2.DataSource = msbll.GetMissionDetails(Cid);
        Repeater2.DataBind();

        LearnSite.BLL.Flection fcbll = new LearnSite.BLL.Flection();
        Repeater3.DataSource = fcbll.GetListCid(Cid);
        Repeater3.DataBind();
    }
    public string toChineseNum(string num)
    {
        return LearnSite.Common.WordProcess.ChineseNum(num);
    }
}
