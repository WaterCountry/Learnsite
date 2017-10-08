using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_autonomiccategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["yid"] != null)
            {
                int yid = Int32.Parse(Request.QueryString["yid"].ToString());
                LearnSite.BLL.SoftCategory ybll = new LearnSite.BLL.SoftCategory();
                GridView1.Caption = ybll.GetTitle(yid);
            }
            ListAll(); 
        }
    }

    private void Listgood()
    {
        if (Request.QueryString["yid"] != null)
        {
            int yid = Int32.Parse(Request.QueryString["yid"].ToString());
            LearnSite.BLL.Autonomic abll = new LearnSite.BLL.Autonomic();
            RepMy.DataSource = abll.GetListGoodByYid(yid);
            RepMy.DataBind();
            
        }    
    }
    protected string GetdownUrl(string aurl)
    {
        aurl = "~/Plugins/download.aspx?Id" + LearnSite.Common.EnDeCode.Encrypt(aurl+"&True","ls");
        return aurl;
    }
    protected string GetfileType(string atype)
    {
        atype = "~/Images/FileType/" + atype + ".gif"; ;
        return atype;
    }
    private void ListAll()
    {
        if (Request.QueryString["yid"] != null)
        {
            int yid = Int32.Parse(Request.QueryString["yid"].ToString());
            LearnSite.BLL.Autonomic abll = new LearnSite.BLL.Autonomic();
            GridView1.DataSource = abll.GetListByYid(yid);
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GridView1.PageIndex * GridView1.PageSize + e.Row.RowIndex + 1);
            CheckBox cb=(CheckBox)e.Row.FindControl("CheckBoxgood");
            Label lb = (Label)e.Row.FindControl("LabelAdate");
            Image img = (Image)e.Row.FindControl("Imagegood");
            img.ImageUrl = LearnSite.Common.WordProcess.ReturnDaysGoneImg(DateTime.Parse(lb.Text));
            if (cb.Checked)
                img.ImageUrl = LearnSite.Common.WordProcess.ReturnGoodImg(true);
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        ListAll();
    }
    protected string Strcut(string str)
    {
        if (str.Length > 20)
            return LearnSite.Common.WordProcess.CnCutString(str, 20, "...");
        else
            return str;
    }
    protected string Viewurl(string url)
    {
        string urlfat = "../Plugins/download.aspx?Id" + LearnSite.Common.EnDeCode.Encrypt(url+"&True","ls");
        return urlfat;
    }
}