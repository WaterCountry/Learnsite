using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_typerclass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                showtitle();
                GradeClass();
                showtyper();
            }
        }
    }
    private void GradeClass()
    {
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
        DDLgrade.SelectedValue = Sgrade;

        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        DDLclass.DataSource = rm.GetClass();
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
        DDLclass.SelectedValue = Sclass;
    }
    private void showtitle()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["Tid"]))
        {
            int Ptid = Int32.Parse(Request.QueryString["Tid"].ToString());
            LearnSite.BLL.Typer tbll = new LearnSite.BLL.Typer();
            string title = tbll.GetTitle(Ptid);
            Labeltitle.Text = "《" + title + "》";
        }
    }
    private void showtyper()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["Tid"]))
        {
            int Ptid=Int32.Parse(Request.QueryString["Tid"].ToString());
            string sgrade = DDLgrade.SelectedValue;
            string sclass = DDLclass.SelectedValue;
            if (!string.IsNullOrEmpty(sgrade) && !string.IsNullOrEmpty(sclass))
            {
                LearnSite.BLL.Ptyper pbll=new LearnSite.BLL.Ptyper();
                GVTyper.DataSource = pbll.ShowClassTidScore(Int32.Parse(sgrade), Int32.Parse(sclass), Ptid);
                GVTyper.DataBind();
            }
        }
    }
    protected void GVTyper_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        showtyper();
    }
    protected void GVTyper_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVTyper.PageIndex * GVTyper.PageSize + e.Row.RowIndex + 1);
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
        GVTyper.PageIndex = 0;
        showtyper();
    }
    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVTyper.PageIndex = 0;
        showtyper();
    }
}