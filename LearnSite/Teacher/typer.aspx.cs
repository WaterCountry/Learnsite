using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_typer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "打字文章浏览页面";
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                ListTypeArticle();
            }
            ButtonClearFinger.Attributes["OnClick"] = "return confirm('您确定删除英文指法成绩吗？');";
            ButtonClearType.Attributes["OnClick"] = "return confirm('您确定删除中文指法成绩吗？');";
            ButtonClearThis.Attributes["OnClick"] = "return confirm('您确定删除此速度以上的中文指法成绩吗？');";
        }
    }

    private void ListTypeArticle()
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        if (Session[Hid+"TyperPageIndex"] != null)
            GVType.PageIndex = Int32.Parse(Session[Hid+"TyperPageIndex"].ToString());
        LearnSite.BLL.Typer bll = new LearnSite.BLL.Typer();
        GVType.DataSource = bll.GetListArticle();
        GVType.DataBind();
    }
    protected void GVType_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        Session[Hid+"TyperPageIndex"] = newPageIndex;
        ListTypeArticle();
    }
    protected void GVType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVType.PageIndex * GVType.PageSize + e.Row.RowIndex + 1);
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
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/typeadd.aspx";
        Response.Redirect(url, false);
    }
    protected void ButtonClearType_Click(object sender, EventArgs e)
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        if (!string.IsNullOrEmpty(Hid))
        {
            int rhid = Int32.Parse(Hid);
            LearnSite.BLL.Ptyper pbll = new LearnSite.BLL.Ptyper();
            pbll.DeleteMyAll(rhid);

            ButtonClearType.Enabled = false;
            labelmsg.Text = "清除所教班级学生中文打字成绩成功!";
        }
    }
    protected void ButtonClearThis_Click(object sender, EventArgs e)
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        if (!string.IsNullOrEmpty(Hid))
        {
            int rhid = Int32.Parse(Hid);
            int OverPscore = int.Parse(DDLpscore.SelectedValue);
            LearnSite.BLL.Ptyper pbll = new LearnSite.BLL.Ptyper();
            pbll.DeleteOverScore(OverPscore,rhid);
            ButtonClearThis.Enabled = false;
            labelmsg.Text = "清除所教班级学生超过指定速度的打字成绩成功!";
        }
    }
    protected void ButtonClearFinger_Click(object sender, EventArgs e)
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        if (!string.IsNullOrEmpty(Hid))
        {
            int rhid = Int32.Parse(Hid);

            LearnSite.BLL.Pfinger fbll = new LearnSite.BLL.Pfinger();
            fbll.ClearMyTb(rhid);

            ButtonClearType.Enabled = false;
            labelmsg.Text = "清除所教班级学生指法打字成绩成功!";
        }
    }
    protected void BtnTypeSet_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/typerset.aspx";
        Response.Redirect(url, false);
    }
}
