using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lessons_precourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.IsTeacherCookies();
        if (Request.QueryString["Cid"] != null)
        {
            if (!IsPostBack)
            {
                ShowCourse();
            }
        }
    }
    private void ShowCourse()
    {
        string Cid = Request.QueryString["Cid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Cid))
        {
            LearnSite.Model.Courses model = new LearnSite.Model.Courses();
            LearnSite.BLL.Courses cs = new LearnSite.BLL.Courses();
            model = cs.GetModel(Int32.Parse(Cid));
            if (model != null)
            {
                LabelCtitle.Text = model.Ctitle;
                LabelCdate.Text = model.Cdate.ToString();
                LabelCclass.Text = model.Cclass;
                LabelCobj.Text = model.Cobj.ToString();
                LabelCterm.Text = model.Cterm.ToString();
                LabelCks.Text = model.Cks.ToString();
                Ccontent.InnerHtml = HttpUtility.HtmlDecode(model.Ccontent);
            }
            else
            {
                Ccontent.InnerHtml = "此学案不存在！";
            }
           LabelWelcome.Text ="您好，欢迎开始本课旅程！";    
        }
    }
}
