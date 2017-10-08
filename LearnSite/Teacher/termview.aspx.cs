using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_termview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "查询各年级各学期的期末成绩评定";
            Grade();
            showTermScores();
        }
    }
    protected void Btnshow_Click(object sender, EventArgs e)
    {
        showTermScores();
    }

    private void showTermScores()
    {
        string ty = DDLYear.SelectedValue;
        string tg = DDLgrade.SelectedValue;
        string ts = DDLclass.SelectedValue;
        string tt = DDLterm.SelectedValue;
        if (ty != "" && tg != "" && ts != "" && tt != "")
        {
            int tyear = Int32.Parse(ty);
            int tgrade = Int32.Parse(tg);
            int tclass = Int32.Parse(ts);
            int tterm = Int32.Parse(tt);
            LearnSite.BLL.TermTotal tbll = new LearnSite.BLL.TermTotal();
            GVTermScore.DataSource = tbll.GetGradeTermScore(tyear, tgrade, tclass, tterm);
            GVTermScore.DataBind();
        }
    }

    private void Grade()
    {
        LearnSite.BLL.TermTotal tbll = new LearnSite.BLL.TermTotal();
        DDLYear.DataSource = tbll.TyearList();
        DDLYear.DataTextField = "Tyear";
        DDLYear.DataValueField = "Tyear";
        DDLYear.DataBind();

        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetAllGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();


        DDLclass.DataSource = room.GetClass();
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();

    }
    protected void GVTermScore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
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
    protected void Btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/termscores.aspx", false);
    }
    protected void BtnExcel_Click(object sender, EventArgs e)
    {
        string ty = DDLYear.SelectedValue;
        string tg = DDLgrade.SelectedValue;
        string tt = DDLterm.SelectedValue;
        if (ty != "" && tg != "" && tt != "")
        {
            int tyear = Int32.Parse(ty);
            int tgrade = Int32.Parse(tg);
            int tterm = Int32.Parse(tt);
            LearnSite.BLL.TermTotal tbll = new LearnSite.BLL.TermTotal();
            tbll.TotalTermExcel(tyear, tgrade, tterm);
        }
    }
    protected void Btnmerit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/overallmerit.aspx", false);
    }
    protected void DDLYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        showTermScores();
    }
}