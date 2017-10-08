using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_mysurveyrank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            Btnreturn.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
            if (!IsPostBack)
            {
                showFscore();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void showFscore()
    {
        if (Request.QueryString["Vid"] != null)
        {
            int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
            LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
            GridViewclass.DataSource = fbll.GetClassFscore(sgrade, sclass, vid);
            GridViewclass.DataBind();
            LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            LearnSite.Model.Survey model = new LearnSite.Model.Survey();
            model = vbll.GetModel(vid);
            Labeltitle.Text = HttpUtility.HtmlDecode(model.Vtitle);
            if (GridViewclass.Rows.Count < 1)
            {
                Labelmsg.Text = "暂无排行！";
            }
        }
    }
    protected void GridViewclass_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
}