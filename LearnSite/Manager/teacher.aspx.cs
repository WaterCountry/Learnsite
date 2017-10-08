using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_teacher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "教师管理页面";
            CountAll();
            ShowTeacher();
        }
    }
    private void CountAll()
    {
        LearnSite.BLL.Teacher tbll = new LearnSite.BLL.Teacher();
        tbll.UpdateHcountAll(); 
    }
    private void ShowTeacher()
    {
        LearnSite.BLL.Teacher tbll= new LearnSite.BLL.Teacher();
        GVTeacher.DataSource = tbll.GetTeacherList();
        GVTeacher.DataBind();
    }
    protected void GVTeacher_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        ShowTeacher();
    }
    protected void GVTeacher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVTeacher.PageIndex * GVTeacher.PageSize + e.Row.RowIndex + 1);
            LinkButton lbtn = (LinkButton)e.Row.FindControl("LinkButtonDel");
            lbtn.Attributes.Add("onclick", "return   confirm('您确定要删除这位老师吗？');");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool thisHpermiss = bool.Parse(((Label)e.Row.FindControl("LabelHpermiss")).Text);
            if (thisHpermiss)
            {
                ((HyperLink)e.Row.FindControl("HyperLinkRoom")).Visible = false;
            }
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Manager/teacheradd.aspx", false);
    }
    protected void GVTeacher_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "D")
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.mngCookieNname] != null)
            {
                string myhid = Request.Cookies[LearnSite.Common.CookieHelp.mngCookieNname].Values["Hid"].ToString();
                string hid = e.CommandArgument.ToString();
                if (myhid != hid)
                {
                    LearnSite.BLL.Teacher tbll = new LearnSite.BLL.Teacher();
                    int res = tbll.DownTeacher(Int32.Parse(hid));//给该老师账号做删除标志
                    LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
                    rbll.ClearRhid(Int32.Parse(hid));//清除该教师所有班级选择
                    if (res == 0)
                        LearnSite.Common.WordProcess.Alert("找不到该教师账号！", this.Page);
                    else
                        ShowTeacher();//刷新列表
                }
                else
                {
                    LearnSite.Common.WordProcess.Alert("提示：不能删除当前登录账号！", this.Page);
                }
            }
        }
    }
}
