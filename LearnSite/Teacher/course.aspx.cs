using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_course : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案页面";
            string termset = LearnSite.Common.XmlHelp.GetTerm();
            Labelmsg.Text = "后台默认当前为：第" + termset + "学期";
            DDLterm.SelectedValue = termset;
            Grade();
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                if (Session[Hid + "grade"] != null)
                    DDLgrade.SelectedValue = Session[Hid + "grade"].ToString();
            }
            CoursesLimit();            
        }
    }

    private void CoursesLimit()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            LearnSite.BLL.Courses course = new LearnSite.BLL.Courses();
            int Cobj = Int32.Parse(DDLgrade.SelectedValue);
            int Cterm = Int32.Parse(DDLterm.SelectedValue);
            GVCourse.DataSource = course.GetLimitHidList(Cobj, Cterm, Int32.Parse(Hid));
            if (Session[Hid + "pageindex"] != null)
                GVCourse.PageIndex = Int32.Parse(Session[Hid + "pageindex"].ToString());
            GVCourse.DataBind();
        }
    }
    protected void GVCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Anthem.GridView theGrid = sender as Anthem.GridView;  // refer to the GridView
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
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            Session[Hid + "pageindex"] = theGrid.PageIndex;
        }
        CoursesLimit();
    }
    private void Grade()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetAllCourseGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
    }

    protected void Btnadd_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/coursecreate.aspx?Cgrade=" + DDLgrade.SelectedValue;
        Response.Redirect(url, false);
    }
    protected void GVCourse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string Cid = GVCourse.DataKeys[e.Row.RowIndex].Value.ToString();
            HyperLink hl = (HyperLink)e.Row.FindControl("Hl");
            string jsstr = "tshow('" + Cid + "');";
            hl.Attributes.Add("onclick", jsstr);
            LinkButton lkbtn = (LinkButton)e.Row.FindControl("LbtnCold");
            string jslk = "return confirm('您确定要将此学案转移到学案仓库中保留吗？');";
            lkbtn.Attributes.Add("onclick", jslk);
            LinkButton lbp = (LinkButton)e.Row.FindControl("LbtnCpublish");
            if (lbp.Text == "True")
                lbp.Text = "已发布";
            else
                lbp.Text = "否";
            LinkButton lbg = (LinkButton)e.Row.FindControl("LbtnCgood");
            if (lbg.Text == "True")
                lbg.Text = "显示";
            else
                lbg.Text = "隐藏";
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
    protected void Btnimport_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            Session[Hid + "importgrade"] = DDLgrade.SelectedValue;
            Session[Hid + "grade"] = DDLgrade.SelectedValue;
            Session[Hid + "pageindex"] = 0;
        }
        string url = "~/Teacher/courseimport.aspx?Cgrade=" + DDLgrade.SelectedValue;
        Response.Redirect(url, false);
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            Session[Hid + "grade"] = DDLgrade.SelectedValue;
            Session[Hid + "pageindex"] = 0;
        }
        CoursesLimit();
    }
    protected void DDLterm_SelectedIndexChanged(object sender, EventArgs e)
    {
        CoursesLimit();
    }
    protected void GVCourse_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pCid = e.CommandArgument.ToString();
        if (e.CommandName == "Cp")
        {
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            cbll.UpdateCpublish(Int32.Parse(pCid));
            CoursesLimit();
        }
        if (e.CommandName == "Cg")
        {
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            cbll.UpdateCgood(Int32.Parse(pCid));
            CoursesLimit();
        }
        if (e.CommandName == "Cu")
        {
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            cbll.UpdateCold(Int32.Parse(pCid));
            CoursesLimit();
        }
    }
    protected void Btnold_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            Session[Hid + "grade"] = DDLgrade.SelectedValue;
        }
        string url = "~/Teacher/courseold.aspx?Cgrade=" + DDLgrade.SelectedValue;
        Response.Redirect(url, false);
    }
}
