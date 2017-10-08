using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_allsite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                ShowGrade();
                ShowClass();
                ShowSite();
            }
            ShowTop();
        }

        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }

    }
    protected void GVSite_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVSite.PageIndex * GVSite.PageSize + e.Row.RowIndex + 1);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
    protected void GVSite_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;
        int newPageIndex = 0;
        if (-2 == e.NewPageIndex)
        {
            TextBox txtNewPageIndex = null;

            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }

            if (null != txtNewPageIndex)
            {

                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            newPageIndex = e.NewPageIndex;
        }
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        theGrid.PageIndex = newPageIndex;
        ShowTop();
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowClass();
        ShowSite();
    }
    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSite();
    }

    private void ShowGrade()
    {
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        DDLgrade.DataSource = rbll.GetAllGrade();//获取年级
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
    }
    private void ShowClass()
    {
        if (DDLgrade.SelectedValue != "")
        {
            int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            DDLclass.DataSource = rm.GetClass();
            DDLclass.DataTextField = "Rclass";
            DDLclass.DataValueField = "Rclass";
            DDLclass.DataBind();
        }
        else
        {
            Labelmsg.Text = "当前没有班级制作网页！";
        }
    }
    private void ShowSite()
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            string Wnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            string MySgrade = DDLgrade.SelectedValue;
            string MySclass = DDLclass.SelectedValue;
            if (MySgrade != "" && MySclass != "")
            {
                LearnSite.BLL.Webstudy ws = new LearnSite.BLL.Webstudy();
                DataListvote.DataSource = ws.GetAllSite(Int32.Parse(MySgrade), Int32.Parse(MySclass), Wnum);//绑定全班数据
                DataListvote.DataBind();
            }
        }
    }
    private void ShowTop()
    {
        if (DDLgrade.SelectedValue != "")
        {
            LearnSite.BLL.Webstudy wbll = new LearnSite.BLL.Webstudy();
            GVSite.DataSource = wbll.WebTopShow(60);
            GVSite.DataBind();
        }
    }

}
