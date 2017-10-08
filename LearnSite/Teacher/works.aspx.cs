using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_works : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "作品浏览页面";
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                Session["term"] = LearnSite.Common.XmlHelp.GetTerm();
                Labelmsg.Text = "第" + Session["term"].ToString() + "学期";
                Grade();
                string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                if (Session[Hid + "grade"] != null)
                {
                    DDLgrade.SelectedValue = Session[Hid + "grade"].ToString();
                }
                else
                {
                    DDLgrade.SelectedIndex = 0;
                    Session[Hid + "grade"] = DDLgrade.SelectedValue;
                }
                CourseList();
            }
        }
    }

    private void CourseList()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();            
            LearnSite.BLL.Courses course = new LearnSite.BLL.Courses();
            string Sgrade = DDLgrade.SelectedValue;
            if (LearnSite.Common.WordProcess.IsNum(Sgrade))
            {
                int grade = Int32.Parse(Sgrade);
                int term = Int32.Parse(Session["term"].ToString());
                int Chid = Int32.Parse(Hid);
                GVCourse.DataSource = course.GetLimitHidList(grade, term, Chid);
                if (Session[Hid + "pageindex"] != null)
                    GVCourse.PageIndex = Int32.Parse(Session[Hid + "pageindex"].ToString());
                GVCourse.DataBind();
            }
        }
    }
    private void Grade()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
    }

    protected void GVCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;  // refer to the GridView
        int newPageIndex = 0;

        if (-2 == e.NewPageIndex)
        { // when click the "GO" Button
            TextBox txtNewPageIndex = null;

            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   // refer to the TextBox with the NewPageIndex value
            }

            if (null != txtNewPageIndex)
            {

                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
            }
        }
        else
        {  // when click the first, last, previous and next Button
            newPageIndex = e.NewPageIndex;
        }

        // check to prevent form the NewPageIndex out of the range
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        theGrid.PageIndex = newPageIndex;
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        Session[Hid+"pageindex"] = theGrid.PageIndex;
        CourseList();
        GVCourse.DataBind();
    }
    protected void Btnterm_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/termscores.aspx", false);
    }
    protected void GVCourse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Wcid =Int32.Parse( e.Row.Cells[0].Text);
            int Sgrade = Int32.Parse(DDLgrade.SelectedValue);
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            HyperLink hl= (HyperLink)e.Row.FindControl("HlNoCheck");
            hl.Text = wbll.ShowNotCheckCounts(Wcid, Sgrade);
            if (hl.Text != "0")
            {
                hl.NavigateUrl = "worknoscore.aspx?Cid=" + Wcid.ToString();
            }
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
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        Session[Hid+"grade"] = DDLgrade.SelectedValue;
        Session[Hid+"pageindex"] = 0;
        CourseList();
    }
}
