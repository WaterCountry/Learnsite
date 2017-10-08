using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_signin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "签到浏览页面";
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                GradeClass();
                ShowSigin();
            }
        }
    }
    protected void GVSignin_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        ShowSigin();
    }
    protected void GVSignin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVSignin.PageIndex * GVSignin.PageSize + e.Row.RowIndex + 1);
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

    private void ShowSigin()
    {
        int Qgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Qclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Signin sign = new LearnSite.BLL.Signin();
        GVSignin.DataSource = sign.GetSignClass(Qgrade, Qclass);
        GVSignin.DataBind();
    }
    private void GradeClass()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        if (Session[Hid+"grade"] != null)
        {
            DDLgrade.SelectedValue = Session[Hid+"grade"].ToString();
        }
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        DDLclass.DataSource = rm.GetLimitClass(Rgrade);
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
        if (Session[Hid+"class"] != null)
        {
            DDLclass.SelectedValue = Session[Hid+"class"].ToString();
        }
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLgrade.SelectedItem != null)
        {
            int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            DDLclass.DataSource = rm.GetLimitClass(Rgrade);
            DDLclass.DataBind();
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            Session[Hid+"grade"] = DDLgrade.SelectedValue;
            Session[Hid+"class"] = DDLclass.SelectedValue;
            GVSignin.PageIndex = 0;
            ShowSigin();
        }
    }

    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        Session[Hid+"grade"] = DDLgrade.SelectedValue;
        Session[Hid+"class"] = DDLclass.SelectedValue;
        GVSignin.PageIndex = 0;
        ShowSigin();
    }
    protected void BtnExcel_Click(object sender, EventArgs e)
    {
        int sgrade=Int32.Parse(DDLgrade.SelectedValue);
        int sclass=Int32.Parse(DDLclass.SelectedValue);
        int sterm=Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
        LearnSite.BLL.Signin sgbll = new LearnSite.BLL.Signin();
        sgbll.SignExcel(sgrade, sclass, sterm);
    }
    protected void BtnExcelNoSign_Click(object sender, EventArgs e)
    {
        int sgrade = Int32.Parse(DDLgrade.SelectedValue);
        int sclass = Int32.Parse(DDLclass.SelectedValue);
        int sterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
        LearnSite.BLL.NotSign nbll = new LearnSite.BLL.NotSign();
        nbll.NotSignExcel(sgrade, sclass, sterm);
    }
}
