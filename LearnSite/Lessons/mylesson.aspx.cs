using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lessons_mylesson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsTeacher())
        {
            if (!IsPostBack)
            {
                Teachers();//首次加载执行
                Grade();
                GetTerm();
                if (Session["lessongrade"] == null)
                    Session["lessongrade"] = DDLgrade.SelectedValue;
                else
                    DDLgrade.SelectedValue = Session["lessongrade"].ToString();
                if (Session["lessonterm"] == null)
                    Session["lessonterm"] = DDLterm.SelectedValue;
                else
                    DDLterm.SelectedValue = Session["lessonterm"].ToString();
                if (Session["lessonhid"] == null)
                    Session["lessonhid"] = DDLhid.SelectedValue;
                else
                    DDLhid.SelectedValue = Session["lessonhid"].ToString();
                ShowLession();
            }
        }
    }
    private void ShowLession()
    {
        if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (Session["lessongrade"] != null && Session["lessonterm"] != null && Session["lessonhid"] != null)
            {
                int Cobj = int.Parse(Session["lessongrade"].ToString());
                int Cterm = int.Parse(Session["lessonterm"].ToString());
                int Chid = int.Parse(Session["lessonhid"].ToString());
                LearnSite.BLL.Courses bll = new LearnSite.BLL.Courses();
                GVCourse.DataSource = bll.GetListLession(Cterm, Cobj, Chid);
                GVCourse.DataBind();
            }
        }
    }

    protected void GVCourse_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["lessongrade"] = DDLgrade.SelectedValue;
        ShowLession();
    }
    private void Grade()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetAllGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
    }
    private void Teachers()
    {
        LearnSite.BLL.Teacher tb = new LearnSite.BLL.Teacher();
        DDLhid.DataSource = tb.GetListHidHname();
        DDLhid.DataTextField = "Hnick";
        DDLhid.DataValueField = "Hid";
        DDLhid.DataBind();
    }
    protected void DDLterm_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["lessonterm"] = DDLterm.SelectedValue;
        ShowLession();
    }
    private void GetTerm()
    {
        DDLterm.SelectedValue = LearnSite.Common.XmlHelp.GetIntTerm().ToString();
    }
    protected void DDLhid_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["lessonhid"] = DDLhid.SelectedValue;
        ShowLession();
    }
}
