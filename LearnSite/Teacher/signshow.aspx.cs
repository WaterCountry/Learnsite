using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_signshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "签到详细页面";
            ShowSignin();
            ShowNoSign();
        }
    }
    protected void ButtonReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/signin.aspx", false);
    }

    protected void GVSignin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
    protected void GVNoSign_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Qyear = Int32.Parse(Request.QueryString["Qyear"].ToString());
            int Qmonth = Int32.Parse(Request.QueryString["Qmonth"].ToString());
            int Qday = Int32.Parse(Request.QueryString["Qday"].ToString());
            string Nnum = e.Row.Cells[1].Text;
            LearnSite.BLL.NotSign bll = new LearnSite.BLL.NotSign();
            string Nnote = bll.GetNoteThisday(Nnum,Qyear,Qmonth,Qday);
            e.Row.Cells[10].Text = Nnote;
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
    private void ShowSignin()
    {
        if (Request.QueryString["Sgrade"] != null)
        {
            int Sgrade =Int32.Parse( Request.QueryString["Sgrade"].ToString());
            int Sclass =Int32.Parse( Request.QueryString["Sclass"].ToString());
            int Qyear =Int32.Parse( Request.QueryString["Qyear"].ToString());
            int Qmonth =Int32.Parse( Request.QueryString["Qmonth"].ToString());
            int Qday =Int32.Parse( Request.QueryString["Qday"].ToString());
            int sort = RBtnList.SelectedIndex;
            LearnSite.BLL.Signin sign = new LearnSite.BLL.Signin();
            GVSignin.DataSource = sign.SignclassdetailSort(Sgrade, Sclass, Qyear, Qmonth, Qday, sort);
            GVSignin.DataBind();
            Labelsignin.Text = "[" + GVSignin.Rows.Count.ToString() + "位]";
        }
    }

    private void ShowNoSign()
    {
        if (Request.QueryString["Sgrade"] != null)
        {
            int Sgrade = Int32.Parse(Request.QueryString["Sgrade"].ToString());
            int Sclass = Int32.Parse(Request.QueryString["Sclass"].ToString());
            int Qyear = Int32.Parse(Request.QueryString["Qyear"].ToString());
            int Qmonth = Int32.Parse(Request.QueryString["Qmonth"].ToString());
            int Qday = Int32.Parse(Request.QueryString["Qday"].ToString());
            LearnSite.BLL.Signin sign = new LearnSite.BLL.Signin();            
            GVNoSign.DataSource = sign.NoSignclassdetail(Sgrade, Sclass, Qyear, Qmonth, Qday);
            GVNoSign.DataBind();
            Labelnosign.Text = "[" + GVNoSign.Rows.Count.ToString() + "位]";
        }
    }

    protected void RBtnList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSignin();
        ShowNoSign();
    }
}
