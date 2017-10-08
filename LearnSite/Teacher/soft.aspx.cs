using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_soft : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "资源浏览页面";
            GetCategory();
            ShowSoftList();
        }
    }
    private void GetCategory()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            LearnSite.BLL.SoftCategory ybll = new LearnSite.BLL.SoftCategory();
            ddlcategory.DataSource = ybll.GetListCategory();
            ddlcategory.DataTextField = "Ytitle";
            ddlcategory.DataValueField = "Yid";
            ddlcategory.DataBind();
            if (Session[Hid + "SoftCateGory"] != null)
            {
                int index = Convert.ToInt32(Session[Hid + "SoftCateGory"]);
                if (index > 0 && index < ddlcategory.Items.Count)
                    ddlcategory.SelectedIndex = index;
            }
        }
    }
    private void ShowSoftList()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            if (Session[Hid + "SoftPageIndex"] != null)
                GVSource.PageIndex = Int32.Parse(Session[Hid + "SoftPageIndex"].ToString());  
            string Fyid=ddlcategory.SelectedValue;
            if (!string.IsNullOrEmpty(Fyid))
            {
                LearnSite.BLL.Soft sf = new LearnSite.BLL.Soft();
                GVSource.DataSource = sf.GetSoftList(Hid, Fyid);
                GVSource.DataBind();
            }
        }
    }
    protected void GVSource_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            Session[Hid + "SoftPageIndex"] = newPageIndex;
        }
        ShowSoftList();
    }
    protected void GVSource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVSource.PageIndex * GVSource.PageSize + e.Row.RowIndex + 1);
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

    protected void GVSource_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Change"))
        {
            string myFid = e.CommandArgument.ToString();
            LearnSite.BLL.Soft sbll = new LearnSite.BLL.Soft();
            sbll.UpdateFhide(Int32.Parse(myFid));
            System.Threading.Thread.Sleep(500);
            ShowSoftList();
        }
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSoftList();
        GVSource.PageIndex = 0;
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            Session[Hid + "SoftCateGory"] = ddlcategory.SelectedIndex;
        }
    }
}
